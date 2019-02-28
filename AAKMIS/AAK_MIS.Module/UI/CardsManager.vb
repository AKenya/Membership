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
Public Class CardsManager
    Inherits BaseObject
    Private fMemberNo, fRequestedBy, fSerialNo, fBranch, fMemberName As String
    Private fStatus As CardsStatus

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    <Appearance("DisableBranch", Enabled:=False)> _
    Public Property Branch() As String
        Get
            Return fBranch
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Branch", fBranch, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <Appearance("DisableMemberNo", Enabled:=False)> _
    Public Property MemberNo() As String
        Get
            Return fMemberNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("MemberNo", fMemberNo, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <Appearance("DisableMemberName", Enabled:=False)> _
    Public Property MemberName() As String
        Get
            Return fMemberName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("MemberName", fMemberName, value)
        End Set
    End Property
    <Appearance("DisableRequestedBy", Enabled:=False)> _
    Public Property RequestedBy() As String
        Get
            Return fRequestedBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue("RequestedBy", fRequestedBy, value)
        End Set
    End Property
    <RuleRequiredField("Serial Required", DefaultContexts.Save, "You must provide a Serial No")> _
    <RuleUniqueValue("Serial unique", DefaultContexts.Save)> _
    Public Property SerialNo() As String
        Get
            Return fSerialNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SerialNo", fSerialNo, value)
        End Set
    End Property
    <Appearance("DisabledStatus", Criteria:="Status='Used'", Enabled:=False)> _
    Public Property Status() As CardsStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As CardsStatus)
            SetPropertyValue("Status", fStatus, value)
        End Set
    End Property
    Public Enum CardsStatus
        Collected = 0
        Delivered = 1
        Posted = 2
    End Enum
    <Persistent("ModifiedOn"), ValueConverter(GetType(UtcDateTimeConverter))> _
    Protected _ModifiedOn As DateTime = DateTime.Now
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
        If String.Compare(propertyName, "MemberNo", False) = 0 Or String.Compare(propertyName, "SerialNo", False) = 0 Or String.Compare(propertyName, "Status", False) = 0 Then
            UpdateModifiedOn()
        End If
    End Sub

    <Persistent("CreatedBy")> _
    Private _CreatedBy As User
    <PersistentAlias("_CreatedBy")> _
    Public Property CreatedBy() As User
        Get
            Return _CreatedBy
        End Get
        Friend Set(ByVal value As User)
            SetPropertyValue("CreatedBy", _CreatedBy, value)
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
    End Sub
    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
    End Sub
End Class
