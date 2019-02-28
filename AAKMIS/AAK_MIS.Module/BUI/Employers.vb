Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.Model

<DefaultClassOptions()> _
Public Class Employers
    Inherits BaseObject
    Private fEmployerName, fMembershipNo, fTelNo, fCellNo, fPostalAddress, fPostalCode, fTown, fEmailAddress, fContactPerson, fDesignation As String
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
    Public Property EmployerName() As String
        Get
            Return fEmployerName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("EmployerName", fEmployerName, value)
        End Set
    End Property

    Public Property MembershipNo() As String
        Get
            Return fMembershipNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("MembershipNo", fMembershipNo, value)
        End Set
    End Property

    Public Property TelNo() As String
        Get
            Return fTelNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("TelNo", fTelNo, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property CellNo() As String
        Get
            Return fCellNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CellNo", fCellNo, value)
        End Set
    End Property

    Public Property PostalAddress() As String
        Get
            Return fPostalAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PostalAddress", fPostalAddress, value)
        End Set
    End Property

    Public Property PostalCode() As String
        Get
            Return fPostalCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PostalCode", fPostalCode, value)
        End Set
    End Property

    Public Property Town() As String
        Get
            Return fTown
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Town", fTown, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property EmailAddress() As String
        Get
            Return fEmailAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue("EmailAddress", fEmailAddress, value)
        End Set
    End Property

    Public Property ContactPerson() As String
        Get
            Return fContactPerson
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ContactPerson", fContactPerson, value)
        End Set
    End Property

    Public Property Designation() As String
        Get
            Return fDesignation
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Designation", fDesignation, value)
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

    <Association("Employers-Order", GetType(Order))> _
    Public ReadOnly Property Order() As XPCollection
        Get
            Return GetCollection("Order")
        End Get
    End Property
    <Association("Employers-PaymentDetails", GetType(PaymentDetails))> _
    Public ReadOnly Property PaymentDetails() As XPCollection
        Get
            Return GetCollection("PaymentDetails")
        End Get
    End Property

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        _CreatedBy = Session.GetObjectByKey(Of User)(SecuritySystem.CurrentUserId)
        ' Place here your initialization code.
    End Sub
End Class
