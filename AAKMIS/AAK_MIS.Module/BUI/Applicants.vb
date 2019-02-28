Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Model

<DefaultClassOptions()> _
Public Class Applicants
    Inherits BaseObject
    Private fRefNo, fName, fIdNo, fCellNo, fAlternateCellNo, fEmail, fDlNo, fCurrentEmployer, fReceiptNo As String
    Private fSex As Sexs
    Private fMstatus As MStatus
    Private fDOB As Date
    Private fAge, fDrivingExperience As Integer
    Private fNationality As Country
    Private fEducationLevel As EducationLevel
    Private fDLClass As DLClass
    Private fDriveType As DType
    Private fJobTitle As JobTitle
    Private fCurrentSalary, fExpectedSalary As Double
    Private fTimeAvailable As AvailableSpan
    Private fStatus As AppStatus
    Private fOrder As Order
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        ' This constructor is used when an object is loaded from a persistent storage.
        ' Do not place any code here or place it only when the IsLoading property is false:
    End Sub
    <RuleUniqueValue("", DefaultContexts.Save)> _
    <Appearance("RefNo", Enabled:=False)> _
    Public Property RefNo() As String
        Get
            Return fRefNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ReferenceNumber", fRefNo, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <Appearance("NameFontIsRed", Criteria:="Status!='Available'", FontColor:="Red"), _
    Appearance("NameFontIsGreen", Criteria:="Status='Available'", FontColor:="Green")> _
    Public Property ApplicantName() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ApplicantName", fName, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property Sex() As Sexs
        Get
            Return fSex
        End Get
        Set(ByVal value As Sexs)
            SetPropertyValue("Sex", fSex, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property MaritalStatus() As MStatus
        Get
            Return fMstatus
        End Get
        Set(ByVal value As MStatus)
            SetPropertyValue("Sex", fMstatus, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <ImmediatePostData()> _
    Public Property DOB() As Date
        Get
            Return fDOB
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("DOB", fDOB, value)
        End Set
    End Property
    ReadOnly tempObject As Integer
    <Appearance("Age", Enabled:=False)> _
    Public Property Age() As Integer
        Get
            If (Today > DOB And Year(DOB) > 1) Then
                Return (Year(Today) - Year(DOB))
                fAge = CDbl(tempObject)
            Else
                Return fAge
            End If
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("Age", fAge, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    Public Property Nationality() As Country
        Get
            Return fNationality
        End Get
        Set(ByVal value As Country)
            SetPropertyValue("Nationality", fNationality, value)
        End Set
    End Property

    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property IDNoPPNo() As String
        Get
            Return fIdNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("IDNumber", fIdNo, value)
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
    <VisibleInListView(False)> _
    Public Property AlternateCellNo() As String
        Get
            Return fAlternateCellNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AlternateCellNo", fAlternateCellNo, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property Email() As String
        Get
            Return fEmail
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Email", fEmail, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property EducationLevel() As EducationLevel
        Get
            Return fEducationLevel
        End Get
        Set(ByVal value As EducationLevel)
            SetPropertyValue("EducationLevel", fEducationLevel, value)
        End Set
    End Property

    Public Property DLClass() As DLClass
        Get
            Return fDLClass
        End Get
        Set(ByVal value As DLClass)
            SetPropertyValue("DLClasses", fDLClass, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property DrivingLicenseNo() As String
        Get
            Return fDlNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("DlNo", fDlNo, value)
        End Set
    End Property

    Public Property DriveType() As DType
        Get
            Return fDriveType
        End Get
        Set(ByVal value As DType)
            SetPropertyValue("DriveType", fDriveType, value)
        End Set
    End Property

    Public Property DrivingExperience() As Integer
        Get
            Return fDrivingExperience
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("DrivingExperience", fDrivingExperience, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property CurrentEmployer() As String
        Get
            Return fCurrentEmployer
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CurrentEmployer", fCurrentEmployer, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property CurrentJobTitle() As JobTitle
        Get
            Return fJobTitle
        End Get
        Set(ByVal value As JobTitle)
            SetPropertyValue("CurrentJobTitle", fJobTitle, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property CurrentSalary() As Double
        Get
            Return fCurrentSalary
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("CurrentSalary", fCurrentSalary, value)
        End Set
    End Property

    Public Property ExpectedSalary() As Double
        Get
            Return fExpectedSalary
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("ExpectedSalary", fExpectedSalary, value)
        End Set
    End Property
    <Appearance("Status", Enabled:=False)> _
    Public Property Status() As AppStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As AppStatus)
            SetPropertyValue("Status", fStatus, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property TimeAvailable() As AvailableSpan
        Get
            Return fTimeAvailable
        End Get
        Set(ByVal value As AvailableSpan)
            SetPropertyValue("TimeAvailable", fTimeAvailable, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property ReceiptNo() As String
        Get
            Return fReceiptNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ReceiptNo", fReceiptNo, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <Appearance("OrderNoDisable", Enabled:=False)> _
    <Association("Order-Applicants", GetType(Order))> _
    Public Property Order() As Order
        Get
            Return fOrder
        End Get
        Set(ByVal value As Order)
            SetPropertyValue("Order", fOrder, value)
        End Set
    End Property

    Private fFile As FileData
    <VisibleInListView(False)> _
    <Aggregated(), ExpandObjectMembers(ExpandObjectMembers.Never)> _
    Public Property CoverLetter() As FileData
        Get
            Return fFile
        End Get
        Set(ByVal value As FileData)
            SetPropertyValue("File", fFile, value)
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
        If propertyName = "ApplicantName" Or propertyName = "Status" Or propertyName = "IDNumber" Then
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

    <Association("Applicants-Courses", GetType(Courses))> _
    Public ReadOnly Property Courses() As XPCollection
        Get
            Return GetCollection("Courses")
        End Get
    End Property

    <Association("Applicants-Languages", GetType(Languages))> _
    Public ReadOnly Property Languages() As XPCollection
        Get
            Return GetCollection("Languages")
        End Get
    End Property

    <Association("Applicants-Employment", GetType(Employment))> _
    Public ReadOnly Property Employments() As XPCollection
        Get
            Return GetCollection("Employments")
        End Get
    End Property
    <Aggregated(), Association("Applicants-Applicant_Docs", GetType(Applicant_Docs))> _
    Public ReadOnly Property Documents() As XPCollection(Of Applicant_Docs)
        Get
            Return GetCollection(Of Applicant_Docs)("Documents")
        End Get
    End Property
    Public Enum Sexs
        Female
        Male
    End Enum

    Public Enum MStatus
        Married
        NotMarried
        NotApplicable
    End Enum

    Public Enum DType
        Automatic
        Manual
        Both
    End Enum

    Public Enum AvailableSpan
        Immediately
        OneWeek
        TwoWeeks
        ThreeWeeks
        OneMonth
    End Enum
    Public Enum AppStatus
        Available
        Engaged
        Disqualified
    End Enum

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        _CreatedBy = Session.GetObjectByKey(Of User)(SecuritySystem.CurrentUserId)
        ' Place here your initialization code.
    End Sub
    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If RefNo Is Nothing Then
            Dim RefNoNew As Integer = GenerateNumbers.GetNewApplicantRefNo
            RefNo = RefNoNew.ToString
            Save()
        End If
    End Sub
End Class