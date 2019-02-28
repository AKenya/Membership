Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Model

<DefaultClassOptions()> _
Public Class IDPBranch_Allocation
    Inherits BaseObject
    Private fOrderNo As String
    Private fBranch As Branch
    Private ReadOnly NewSession As Boolean = False
    Private fIDPStartNo, fQuantity As Integer
    Private ReadOnly fAvailableIDPS As Integer
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        If Not IsLoading Then
            NewSession = True
            Try
                fAvailableIDPS = GenerateNumbers.GetTotalIdps(IDP_Allocation.IdpState.UnAllocated)
            Catch ex As Exception
                'MsgBox(ex.Message)
                Throw
            End Try
        End If
    End Sub
    <RuleRequiredField()> _
    <Appearance("DisableBranch", Criteria:="IDPLastNo='-1'", Enabled:=False)> _
    Public Property Branch As Branch
        Get
            Return fBranch
        End Get
        Set(value As Branch)
            SetPropertyValue("Branch", fBranch, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <Appearance("DisableOrderNo", Criteria:="IDPLastNo='-1'", Enabled:=False)> _
    Public Property OrderNo As String
        Get
            Return fOrderNo
        End Get
        Set(value As String)
            SetPropertyValue("OrderNo", fOrderNo, value)
        End Set
    End Property
    <Appearance("DisableIDPStartNo", Enabled:=False)> _
    Public Property IDPStartNo As Integer
        Get
            Return fIDPStartNo
        End Get
        Set(value As Integer)
            SetPropertyValue("IDPStartNo", fIDPStartNo, value)
        End Set
    End Property
    Public ReadOnly Property AvailableIDPS As Integer
        Get
            Return fAvailableIDPS
        End Get
    End Property
    <ImmediatePostData()> _
    <Appearance("DisableQty", Criteria:="IDPLastNo='-1'", Enabled:=False)> _
    Public Property Quantity As Integer
        Get
            Return fQuantity
        End Get
        Set(value As Integer)
            If SetPropertyValue("Quantity", fQuantity, value) Then
                OnChanged("IDPLastNo")
            End If
        End Set
    End Property

    <PersistentAlias("IDPStartNo + Quantity-1")> _
    Public ReadOnly Property IDPLastNo As Integer
        Get
            Dim tempObject As Object = EvaluateAlias("IDPLastNo")
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
        If propertyName = "Branch" Or propertyName = "OrderNo" Or propertyName = "Quantity" Then
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
        If IDPStartNo = 0 Then
            Try
                Dim IDPAllocatedNewNo As Integer = GenerateNumbers.GetStartingIdpAllocationNo(IDP_Allocation.IdpState.UnAllocated)
                IDPStartNo = IDPAllocatedNewNo
            Catch ex As Exception
                Throw
            End Try
        End If
    End Sub
    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        Dim NewIDPStartNo = IDPStartNo
        If NewSession = True Then
            If Quantity <= 0 Then
                Throw New InvalidValueAllocated()
            ElseIf Quantity > AvailableIDPS Then
                Throw New InvalidValueAllocated()
            Else
                Try
                    For i As Integer = NewIDPStartNo To IDPLastNo
                        ''Allocate IDPs to selected Branch
                        Dim Cby As User = _CreatedBy
                        Dim sqlHandler As New SQLdb
                        Dim StrQ As String = String.Format("UPDATE [dbo].[IDP_Allocation] SET [CreatedBy]='{0}',[Status]='{1}',[AllocatedBranch]='{2}' WHERE [IDPNumber]='{3}'", _
                                                                                            Cby.Oid, 1, Branch.Oid, NewIDPStartNo)
                        sqlHandler.SendNonQuery(StrQ)
                        NewIDPStartNo += 1
                    Next i
                Catch ex As Exception
                    Throw
                End Try
            End If
        End If
    End Sub
End Class
Public Class InvalidValueAllocated : Inherits Exception
    Public Sub New()
        MyBase.New("The Quantity allocated must be greater than zero(0) and less than the available Quantity in store")
    End Sub
End Class