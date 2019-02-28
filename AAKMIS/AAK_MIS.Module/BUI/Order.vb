Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.DC

<XafDefaultProperty("OrderNo")> _
<Persistent()> _
Public Class Order
    Inherits BaseObject
    Private fOrderNo, fGradeAttained As String
    Private fOrderDate, fDateRequired As Date
    Private fJobTerms As Terms
    Private fDriverType As DriverTypes
    Private fNoRequired, fExperience, fMinAge, fMaxAge As Integer
    Private fMinEducationLevel As EducationLevel
    Private fDriveType As Applicants.DType
    Private fGender As Gennder
    Private ReadOnly NewSession As Boolean = False
    Private fOrderStatus As OrderStatus
    Private fSalaryOffer As Double
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        ' This constructor is used when an object is loaded from a persistent storage.
        ' Do not place any code here or place it only when the IsLoading property is false:
        If Not IsLoading Then
            NewSession = True
        End If
    End Sub
    <RuleRequiredField()> _
    <Association("Employers-Order", GetType(Employers))> _
    Public Property Employers As Employers
    <Appearance("OrderNo", Enabled:=False)> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property OrderNo() As String
        Get
            Return fOrderNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("OrderNo", fOrderNo, value)
        End Set
    End Property

    Public Property OrderDate() As Date
        Get
            Return fOrderDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("OrderDate", fOrderDate, value)
        End Set
    End Property

    Public Property DateRequired() As Date
        Get
            Return fDateRequired
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("DateRequired", fDateRequired, value)
        End Set
    End Property

    Public Property JobTerms() As Terms
        Get
            Return fJobTerms
        End Get
        Set(ByVal value As Terms)
            SetPropertyValue("JobTerms", fJobTerms, value)
        End Set
    End Property

    Public Property DriverType() As DriverTypes
        Get
            Return fDriverType
        End Get
        Set(ByVal value As DriverTypes)
            SetPropertyValue("DriverType", fDriverType, value)
        End Set
    End Property

    Public Property NoRequired() As Integer
        Get
            Return fNoRequired
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("NoRequired", fNoRequired, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property MinEducationLevel() As EducationLevel
        Get
            Return fMinEducationLevel
        End Get
        Set(ByVal value As EducationLevel)
            SetPropertyValue("MinEducationLevel", fMinEducationLevel, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property GradeAttained() As String
        Get
            Return fGradeAttained
        End Get
        Set(ByVal value As String)
            SetPropertyValue("GradeAttained", fGradeAttained, value)
        End Set
    End Property

    Public Property Experience() As Integer
        Get
            Return fExperience
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("Experience", fExperience, value)
        End Set
    End Property
    Public Property SalaryOffer() As Double
        Get
            Return fSalaryOffer
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("SalaryOffer", fSalaryOffer, value)
        End Set
    End Property

    Public Property DriveType() As Applicants.DType
        Get
            Return fDriveType
        End Get
        Set(ByVal value As Applicants.DType)
            SetPropertyValue("Experience", fDriveType, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property MinAge() As Integer
        Get
            Return fMinAge
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("MinAge", fMinAge, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property MaxAge() As Integer
        Get
            Return fMaxAge
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("MaxAge", fMaxAge, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property Gender() As Gennder
        Get
            Return fGender
        End Get
        Set(ByVal value As Gennder)
            SetPropertyValue("Gender", fGender, value)
        End Set
    End Property
    <Appearance("StatusDisabled", Enabled:=False)> _
    Public Property Status As OrderStatus
        Get
            Return fOrderStatus
        End Get
        Set(ByVal value As OrderStatus)
            SetPropertyValue("OrderStatus", fOrderStatus, value)
        End Set
    End Property
    Private fDescription As String
    <VisibleInListView(False)> _
    Public Property Description As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Description", fDescription, value)
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
        If propertyName = "NoRequired" Or propertyName = "Experience" Or propertyName = "OrderStatus" Then
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
    Public Enum Terms
        Permanent
        Contract
        Casual
    End Enum
    Public Enum Gennder
        Male
        Female
        Both
    End Enum
    Public Enum OrderStatus
        Pending = 0
        Processed = 1
        Suspended = 2
    End Enum
    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If OrderNo Is Nothing Then

            Dim OrderNoN As Integer = GenerateNumbers.GetNewOrderNumber
            OrderNo = OrderNoN.ToString
            Save()
        End If
    End Sub

#Region "Properties and Colections"
    <Association("Order-Applicants", GetType(Applicants))> _
    Public ReadOnly Property Applicants() As XPCollection
        Get
            Return GetCollection("Applicants")
        End Get
    End Property
#End Region
End Class
