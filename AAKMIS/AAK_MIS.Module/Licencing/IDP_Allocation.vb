Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Model
Imports System.ComponentModel

<DefaultProperty("IDPNumber")> _
Public Class IDP_Allocation
    Inherits BaseObject
    Private fIDPNumber, fCancelReason As String
    Private fBranch As Branch
    Private fStatus As IdpState

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <Appearance("DisableIDPNo", Enabled:=False)> _
    <Indexed(Unique:=True)> _
    Public Property IDPNumber As String
        Get
            Return fIDPNumber
        End Get
        Set(value As String)
            SetPropertyValue("IDPNumber", fIDPNumber, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <Association("IDPsApplication-Branch", GetType(Branch))> _
    Public Property AllocatedBranch As Branch
        Get
            Return fBranch
        End Get
        Set(ByVal value As Branch)
            SetPropertyValue("AllocatedBranch", fBranch, value)
        End Set
    End Property
    <Appearance("IDPStatusDisabled", Enabled:=False)> _
    Public Property Status As IdpState
        Get
            Return fStatus
        End Get
        Set(value As IdpState)
            SetPropertyValue("Status", fStatus, value)
        End Set
    End Property
    <Appearance("CancelReasonDisabled", Enabled:=False)> _
    Public Property CancelReason As String
        Get
            Return fCancelReason
        End Get
        Set(value As String)
            SetPropertyValue("CancelReason", fCancelReason, value)
        End Set
    End Property
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
        If String.Compare(propertyName, "IDPNumber", False) = 0 Or String.Compare(propertyName, "AllocatedBranch", False) = 0 Or String.Compare(propertyName, "Status", False) = 0 Then
            UpdateModifiedOn()
        End If
    End Sub

    <Persistent("CreatedBy")> _
    Private _CreatedBy As User

    <PersistentAlias("_CreatedBy")> _
    Public Property AllocatedBy() As User
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
    Public Enum IdpState
        UnAllocated = 0
        UnIssued = 1
        Issued = 2
        Missing = 3
        Cancelled = 4
    End Enum
End Class
