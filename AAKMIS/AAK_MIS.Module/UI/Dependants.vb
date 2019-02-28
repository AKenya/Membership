Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.DC
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp

<ImageName("BO_Department")> _
<XafDefaultProperty("Name")> _
<DefaultListViewOptions(MasterDetailMode.ListViewAndDetailView, True, NewItemRowPosition.Top)> _
<DefaultClassOptions()> _
Public Class Dependants
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Private fMemberNo As MemberRegistration
    Private fName, fIDNumber As String
    Private fRelationship As Relation
    <Association("Dependants-MemReg", GetType(MemberRegistration))> _
    Public Property MemberNo() As MemberRegistration
        Get
            Return fMemberNo
        End Get
        Set(ByVal value As MemberRegistration)
            SetPropertyValue("MemberNo", fMemberNo, value)
        End Set
    End Property
    <XafDisplayName("Dependant's Name")> _
    <Index(0)> _
    <VisibleInListView(True), VisibleInDetailView(False), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Name", fName, value)
        End Set
    End Property
    <RuleUniqueValue("", DefaultContexts.Save)> _
    <RuleRequiredField()> _
    Public Property IDNumber() As String
        Get
            Return fIDNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue("IDNumber", fIDNumber, value)
        End Set
    End Property
    Public Property Relationship() As Relation
        Get
            Return fRelationship
        End Get
        Set(ByVal value As Relation)
            SetPropertyValue("Relationship", fRelationship, value)
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
        If String.Compare(propertyName, "Name", False) = 0 Or String.Compare(propertyName, "IDNumber", False) = 0 Or String.Compare(propertyName, "Relationship", False) = 0 Then
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
    Public Enum VehicleUse
        PrivateUse
        Commercial
    End Enum
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        _CreatedBy = Session.GetObjectByKey(Of User)(SecuritySystem.CurrentUserId)
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
    End Sub
    Public Enum Relation
        Spouse
        Child
    End Enum
End Class
