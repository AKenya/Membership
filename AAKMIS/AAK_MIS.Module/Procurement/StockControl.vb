Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.ConditionalAppearance

<DefaultClassOptions()> _
Public Class StockControl
    Inherits BaseObject
    Private fCategory As CategoryGroups
    Private fItemCode As String
    Private fOpenningStock, fStockIn, fStockOut, fBalance As Int16
    Private fSupplyFrom As SuppliersList
    Private fIssuedTo As Branch
    Private fItem As Items
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    'Private _PersistentProperty As String
    '<XafDisplayName("My Persistent Property")> _
    '<ToolTip("Specify a hint message for a property in the UI.")> _
    '<ModelDefault("EditMask", "(000)-00")> _
    '<Index(0)> _
    '<VisibleInListView(True), VisibleInDetailView(False), VisibleInLookupListView(True)> _
    '<RuleRequiredField(DefaultContexts.Save)> _
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
    '<RuleRequiredField()> _
    '<ImmediatePostData()> _
    '<DataSourceProperty("Category.Items", DataSourcePropertyIsNullMode.SelectNothing)> _
    '<Association("StockControl-Items", GetType(Items))> _
    Public Property Item As Items
        Get
            Return fItem
        End Get
        Set(ByVal value As Items)
            If SetPropertyValue("Item", fItem, value) Then
                'If Not Item Is Nothing Then
                '    fItemCode = Item.ItemCode
                'End If
            End If
        End Set
    End Property
    <Appearance("ItemCodeDisabled", Enabled:=False)> _
    Public Property ItemCode() As String
        Get
            Return fItemCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ItemCode", fItemCode, value)
        End Set
    End Property
    <Appearance("BalanceDisabled", enabled:=False)> _
    Public Property OpenningStock() As Int16
        Get
            Return fOpenningStock
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("OpenningStock", fOpenningStock, value)
        End Set
    End Property
    Public Property StockIn() As Int16
        Get
            Return fStockIn
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("StockIn", fStockIn, value)
        End Set
    End Property
    Public Property StockOut() As Int16
        Get
            Return fStockOut
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("StockOut", fStockOut, value)
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
    Public Property SupplyFrom() As SuppliersList
        Get
            Return fSupplyFrom
        End Get
        Set(ByVal value As SuppliersList)
            SetPropertyValue("SupplyFrom", fSupplyFrom, value)
        End Set
    End Property
    Public Property IssuedTo() As Branch
        Get
            Return fIssuedTo
        End Get
        Set(ByVal value As Branch)
            SetPropertyValue("IssuedTo", fIssuedTo, value)
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
        If String.Compare(propertyName, "Category", False) = 0 Or String.Compare(propertyName, "ItemCode", False) = 0 Or String.Compare(propertyName, "Item", False) = 0 Then
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
End Class
