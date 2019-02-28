Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.Model

<DefaultClassOptions()> _
Public Class DeletionDetails
    Inherits BaseObject
    Private fDeletionlistNo As Integer
    Private fMemberNo, fReason As String
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <Appearance("DisableDeletionlistNo", Enabled:=False)> _
    <RuleRequiredField()> _
    Public Property DeletionlistNo() As Integer
        Get
            Return fDeletionlistNo
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("DeletionlistNo", fDeletionlistNo, value)
        End Set
    End Property
    <Appearance("DisableMemberNoDel", Enabled:=False)> _
    Public Property MemberNo() As String
        Get
            Return fMemberNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("MemberNo", fMemberNo, value)
        End Set
    End Property
    <Size(200)> _
    <RuleRequiredField(TargetCriteria:="MemberNo!=?")> _
    Public Property Reason() As String
        Get
            Return fReason
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Reason", fReason, value)
        End Set
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
        If propertyName = "Reason" Then
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
    End Sub
    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        Try
            If DeletionlistNo = 0 And fReason Is Nothing = False Then
                Dim DeletionlistNoNew As Integer = GenerateNumbers.GetfDeletionlistNo
                DeletionlistNo = DeletionlistNoNew
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub
End Class
