
Imports DevExpress.Persistent.Validation
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Base
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp

<DefaultClassOptions()> _
Public Class Items
    Inherits BaseObject
    Private fCategory As CategoryGroups
    Private fItemName, fItemCode As String
    Private fReOrderLevel As Int16
    Private fItemType As ItemTypes
    Public Sub New(ByVal session As DevExpress.Xpo.Session)
        MyBase.New(session)
    End Sub
    <RuleRequiredField()> _
    <Association("CatGroup-To-Items")> _
    Public Property Category() As CategoryGroups
        Get
            Return fCategory
        End Get
        Set(ByVal value As CategoryGroups)
            SetPropertyValue("Category", fCategory, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property ItemName() As String
        Get
            Return fItemName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ItemName", fItemName, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property ItemCode() As String
        Get
            Return fItemCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ItemCode", fItemCode, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property ItemType() As ItemTypes
        Get
            Return fItemType
        End Get
        Set(ByVal value As ItemTypes)
            SetPropertyValue("ItemType", fItemType, value)
        End Set
    End Property
    Public Property ReOrderLevel() As Int16
        Get
            Return fReOrderLevel
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("ItemCode", fReOrderLevel, value)
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
        If propertyName = "Category" Or propertyName = "ItemName" Or propertyName = "ItemCode" Then
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
    '<Association("StockControl-Items", GetType(StockControl))> _
    Public ReadOnly Property StockMovementHistory() As XPCollection(Of StockControl)
        Get
            Return GetCollection(Of StockControl)("StockMovementHistory")
        End Get
    End Property

    Public Enum ItemTypes
        Unknown
        FixedAsset
        CurrentAsset
    End Enum
End Class
