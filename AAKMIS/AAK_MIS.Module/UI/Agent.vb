Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.DC

<XafDefaultProperty("AgentName")> _
Public Class Agent
    Inherits BaseObject
    Private fAgentName, fLocation, fContactPerson, fPhoneNo, fAddress, fSpeciality As String
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        ' This constructor is used when an object is loaded from a persistent storage.
        ' Do not place any code here or place it only when the IsLoading property is false:
        ' if (!IsLoading){
        '   It is now OK to place your initialization code here.
        ' }
        ' or as an alternative, move your initialization code into the AfterConstruction method.
    End Sub
    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property AgentName As String
        Get
            Return fAgentName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AgentName", fAgentName, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property Location As String
        Get
            Return fLocation
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Location", fLocation, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property ContactPerson As String
        Get
            Return fContactPerson
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ContactPerson", fContactPerson, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property PhoneNo As String
        Get
            Return fPhoneNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PhoneNo", fPhoneNo, value)
        End Set
    End Property

    Public Property Address As String
        Get
            Return fAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Address", fAddress, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property Speciality As String
        Get
            Return fSpeciality
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Speciality", fSpeciality, value)
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
        If propertyName = "AgentName" Or propertyName = "Location" Or propertyName = "ContactPerson" Then
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
