Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.Model

<DefaultClassOptions()> _
Public Class Recruiter
    Inherits BaseObject
    Private fName, fIDNumber, fPinNumber, fCellNo, fEmail, fDepartment As String
    Private fDateEnrolled As Date
    Private fBranch As Branch
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
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Name", fName, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property IDNumber() As String
        Get
            Return fIDNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue("IDNumber", fIDNumber, value)
        End Set
    End Property

    Public Property PinNumber() As String
        Get
            Return fPinNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PinNumber", fPinNumber, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property CellNumber() As String
        Get
            Return fCellNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CellNo", fCellNo, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property Email() As String
        Get
            Return fEmail
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Email", fEmail, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property DateEnrolled() As Date
        Get
            Return fDateEnrolled
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("DateEnrolled", fDateEnrolled, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property Branch() As Branch
        Get
            Return fBranch
        End Get
        Set(ByVal value As Branch)
            SetPropertyValue("Branch", fBranch, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property Department() As String
        Get
            Return fDepartment
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Department", fDepartment, value)
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
        If propertyName = "EmployerName" Or propertyName = "CellNo" Or propertyName = "MembershipNo" Then
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
