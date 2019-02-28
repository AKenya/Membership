Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.Xpo.DB
Imports DevExpress.ExpressApp.Model

<DefaultClassOptions()> _
Public Class ReceiveIdps
    Inherits BaseObject
    Private NewSession As Boolean = False
    Private fStartBatchNo, fQuantity As Integer
    Private Shared Property con As New Constants
    Public Shared Property uow As UnitOfWork
    Public Shared Sub GetUow()
        If uow Is Nothing Then
            Dim sqlconn = MSSqlConnectionProvider.GetConnectionString(con.server, con.user, con.password, con.DB)
            Dim dl As IDataLayer = XpoDefault.GetDataLayer(sqlconn, AutoCreateOption.DatabaseAndSchema)
            uow = New UnitOfWork(dl)
        End If
    End Sub
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        If Not IsLoading Then
            NewSession = True
        End If
    End Sub
    <Appearance("DisableStartBatchNo", Enabled:=False)> _
    Public Property StartBatchNo() As Integer
        Get
            Return fStartBatchNo
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("StartBatchNo", fStartBatchNo, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <ImmediatePostData()> _
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            If SetPropertyValue("Quantity", fQuantity, value) Then
                OnChanged("LastBatchNo")
            End If
        End Set
    End Property

    <PersistentAlias("StartBatchNo + Quantity-1")> _
    Public ReadOnly Property LastBatchNo() As Integer
        Get
            Dim tempObject As Object = EvaluateAlias("LastBatchNo")
            If tempObject IsNot Nothing Then
                Return CDbl(tempObject)
            Else
                Return 0
            End If
        End Get
    End Property

    <Persistent("ModifiedOn"), ValueConverter(GetType(UtcDateTimeConverter))> _
    Protected _ModifiedOn As DateTime = DateTime.Now
    <VisibleInListView(False)> _
    <PersistentAlias("_ModifiedOn"), ModelDefaultAttribute("EditMask", "G"), ModelDefaultAttribute("DisplayFormat", "{0:G}")> _
    Public ReadOnly Property ModifiedOn() As DateTime
        Get
            Return _ModifiedOn
        End Get
    End Property
    Friend Overridable Sub UpdateModifiedOn()
        UpdateModifiedOn(DateTime.Now)
    End Sub
    Friend Overridable Sub UpdateModifiedOn(ByVal [date] As DateTime)
        _ModifiedOn = [date]
        Save()
    End Sub
    Protected Overrides Sub OnChanged(ByVal propertyName As String, _
    ByVal oldValue As Object, ByVal newValue As Object)
        MyBase.OnChanged(propertyName, oldValue, newValue)
        If propertyName = "Quantity" Then
            UpdateModifiedOn()
        End If
    End Sub

    <Persistent("CreatedBy")> _
    Private _CreatedBy As User
    <VisibleInListView(False)> _
    <PersistentAlias("_CreatedBy")> _
    Public Property CreatedBy() As User
        Get
            Return _CreatedBy
        End Get
        Friend Set(ByVal value As User)
            _CreatedBy = value
        End Set
    End Property
    Private changeHistory As XPCollection(Of AuditDataItemPersistent)
    Public ReadOnly Property AuditTrail() As XPCollection(Of AuditDataItemPersistent)
        Get
            If changeHistory Is Nothing Then
                changeHistory = AuditedObjectWeakReference.GetAuditTrail(Session, Me)
            End If
            Return changeHistory
        End Get
    End Property
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        _CreatedBy = Session.GetObjectByKey(Of User)(SecuritySystem.CurrentUserId)
        ' Place here your initialization code.
        If StartBatchNo = 0 Then
            Try
                Dim IDPOrderNewNo As Integer = GenerateNumbers.GetStartingIdp
                StartBatchNo = IDPOrderNewNo
            Catch ex As Exception
                Throw
            End Try
        End If
    End Sub
    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        Dim NewIDPNo = StartBatchNo
        If Quantity > 0 And NewSession = True Then
            Try
                GetUow()
                For i As Integer = StartBatchNo To LastBatchNo
                    Dim app As New IDP_Allocation(uow) With {.IDPNumber = NewIDPNo, .Status = IDP_Allocation.IdpState.UnAllocated}
                    app.Save()
                    NewIDPNo += 1
                Next i
                uow.CommitTransaction()
            Catch ex As Exception
                Throw
            End Try
            uow.Dispose()
        End If
    End Sub
End Class