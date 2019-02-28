Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.ConditionalAppearance

<DefaultClassOptions()> _
Public Class ReqestedItemsList
    Inherits FileAttachmentBase
    Private fItemName As Items
    Private fBranch, fDepartment, fApprovedBy As String
    Private fQuantityRequested, fQuantityReceived, fBalance As Int16
    Private fCategory As CategoryGroups
    Private fStatus As RequestStatus
    Private fReceivedBy As User
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <ImmediatePostData()> _
    <RuleRequiredField()> _
    Public Property Category() As CategoryGroups
        Get
            Return fCategory
        End Get
        Set(ByVal value As CategoryGroups)
            SetPropertyValue("Category", fCategory, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <DataSourceProperty("Category.Items", DataSourcePropertyIsNullMode.SelectNothing)> _
    Public Property ItemName() As Items
        Get
            Return fItemName
        End Get
        Set(ByVal value As Items)
            SetPropertyValue("ItemName", fItemName, value)
        End Set
    End Property

    Public Property QuantityRequested() As Int16
        Get
            Return fQuantityRequested
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("QuantityRequested", fQuantityRequested, value)
        End Set
    End Property
    Public Property QuantityReceived() As Int16
        Get
            Return fQuantityReceived
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("QuantityReceived", fQuantityReceived, value)
        End Set
    End Property
    <Appearance("BalanceDisabled", enabled:=False)> _
    Public Property Balance() As Int16
        Get
            Return fBalance
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("Balance", fBalance, value)
        End Set
    End Property
    Public Property Branch() As String
        Get
            Return fBranch
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Branch", fBranch, value)
        End Set
    End Property

    Public Property Department() As String
        Get
            Return fDepartment
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Department", fDepartment, value)
        End Set
    End Property

    Public Property ApprovedBy() As String
        Get
            Return fApprovedBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ApprovedBy", fApprovedBy, value)
        End Set
    End Property

    <Appearance("StatusIsRed", Criteria:="Status='Suspended'||Status='Cancelled'", FontColor:="Red"), _
    Appearance("StatusIsGreen", Criteria:="Status='Issued'", FontColor:="Green")> _
    <RuleRequiredField()> _
    Public Property Status() As RequestStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As RequestStatus)
            SetPropertyValue("Status", fStatus, value)
        End Set
    End Property
    Public Property ReceivedBy() As User
        Get
            Return fReceivedBy
        End Get
        Set(ByVal value As User)
            SetPropertyValue("ReceivedBy", fReceivedBy, value)
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
        If String.Compare(propertyName, "ItemName", False) = 0 Or String.Compare(propertyName, "Category", False) = 0 Or String.Compare(propertyName, "Quantity", False) = 0 Or String.Compare(propertyName, "Department", False) = 0 Then
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
    End Sub

    Public Enum RequestStatus
        Pending
        Issued
        Cancelled
        Suspended
    End Enum
End Class
