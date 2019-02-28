Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.DC

<XafDefaultProperty("BranchName")> _
Public Class Branch
    Inherits BaseObject
    Private fBranchName, fBranchCode, fTel, fAddress, fTown, fPCode, fCountyCode, fROCode As String
    Private _IsNrb As Boolean
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property BranchName() As String
        Get
            Return fBranchName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("BranchName", fBranchName, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property CountyCode() As String
        Get
            Return fCountyCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CountyCode", fCountyCode, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property ROCode() As String
        Get
            Return fROCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ROCode", fROCode, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property BranchCode() As String
        Get
            Return fBranchCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("BranchCode", fBranchCode, value)
        End Set
    End Property

    Public Property Telephone() As String
        Get
            Return fTel
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Telephone", fTel, value)
        End Set
    End Property
    Public Property PostalAddress() As String
        Get
            Return fAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Address", fAddress, value)
        End Set
    End Property
    Public Property PostalCode() As String
        Get
            Return fPCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PostalCode", fPCode, value)
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
    Private _isScheme As Boolean
    Public Property IsScheme() As Boolean
        Get
            Return _isScheme
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("IsScheme", _isScheme, value)
        End Set
    End Property
    Public Property IsNrbBranch() As Boolean
        Get
            Return _IsNrb
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("IsNrbBranch", _IsNrb, value)
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
        If propertyName = "BranchName" Or propertyName = "CountyCode" Or propertyName = "IsScheme" Then
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

    <Association("IndividualRegistration-Branch", GetType(MemberRegistration))> _
    Public ReadOnly Property Members() As XPCollection(Of MemberRegistration)
        Get
            Return GetCollection(Of MemberRegistration)("Members")
        End Get
    End Property
    <Association("IDPsDetails-Branch", GetType(IDP_Details))> _
    Public ReadOnly Property IssuedIDPDetails() As XPCollection(Of IDP_Details)
        Get
            Return GetCollection(Of IDP_Details)("IssuedIDPDetails")
        End Get
    End Property

    <Association("IDPsApplication-Branch", GetType(IDP_Allocation))> _
    Public ReadOnly Property IdpAllocationList() As XPCollection(Of IDP_Allocation)
        Get
            Return GetCollection(Of IDP_Allocation)("IdpAllocationList")
        End Get
    End Property

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        _CreatedBy = Session.GetObjectByKey(Of User)(SecuritySystem.CurrentUserId)
    End Sub
End Class
