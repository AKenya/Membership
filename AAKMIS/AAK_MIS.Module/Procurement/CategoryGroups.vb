Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Persistent.Validation
Imports System.ComponentModel

<DefaultClassOptions(), DefaultProperty("Category")> _
Public Class CategoryGroups
    Inherits BaseObject
    Private fCategory, fDetails As String
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property Category() As String
        Get
            Return fCategory
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Category", fCategory, value)
        End Set
    End Property
    <Association("CategoryGroups-Suppliers", GetType(SuppliersList))> _
    Public ReadOnly Property Suppliers() As XPCollection
        Get
            Return GetCollection("Suppliers")
        End Get
    End Property
    <Size(400)> _
    Public Property Details() As String
        Get
            Return fDetails
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Details", fDetails, value)
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
        If String.Compare(propertyName, "Category", False) = 0 Or String.Compare(propertyName, "Details", False) = 0 Then
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
    <Association("CatGroup-To-Items")> _
    Public ReadOnly Property Items() As XPCollection(Of Items)
        Get
            Return (GetCollection(Of Items)("Items"))
        End Get
    End Property
End Class
