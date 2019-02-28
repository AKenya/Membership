Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Xpo.DB
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.ConditionalAppearance
<ImageName("BO_Employee")> _
<DefaultClassOptions()> _
Public Class StudentManager
    Inherits EmergencyContacts
    Private fMemberNo, fStudentName, fIDNo, fStudentPostalAddress, fStudentPostalCode, fStudentTown, _
        fStudentCellNo, fStudentEmail, fHealthConditionDetails, fExperienceObtainedFrom As String
    Private fBranch As String
    Private fDLClass As DLClass
    Private fNationality As String
    Private fCommecementDate As Date
    Private fLessonsApplied, fAdditionalLessons As Int16
    Private fHasPreviousExperience, fHasHealthCondition As Boolean
    Private Shared Property con As New Constants
    Public Shared Property uow As UnitOfWork
    Public Shared Sub GetUow()
        If uow Is Nothing Then
            Dim sqlconn = MSSqlConnectionProvider.GetConnectionString(con.server, con.user, con.password, con.DB)
            Dim dl = XpoDefault.GetDataLayer(sqlconn, AutoCreateOption.DatabaseAndSchema)
            uow = New UnitOfWork(dl)
        End If
    End Sub
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <Appearance("StudentManagerRule", AppearanceItemType:="ViewItem", Context:="DetailView", Enabled:=False, TargetItems:="CommecementDate")> _
    Protected Function DisableAssessment() As Boolean
        Return Not (ModifiedOn > DateAdd(DateInterval.Day, -7, Today))
    End Function
    <Appearance("MemberNoDisabled", Enabled:=False)> _
    Public Property MemberNumber As String
        Get
            Return fMemberNo
        End Get
        Set(value As String)
            SetPropertyValue("MemberNumber", fMemberNo, value)
        End Set
    End Property
    <Appearance("StudentNameDisabled", Enabled:=False)> _
    <RuleRequiredField()> _
    Public Property StudentName As String
        Get
            Return fStudentName
        End Get
        Set(value As String)
            SetPropertyValue("StudentName", fStudentName, value)
        End Set
    End Property
    <Appearance("IDPNoDisabled", Criteria:="IDNumber!=?", Enabled:=False)> _
    <ImmediatePostData()> _
    <RuleRequiredField()> _
    <Indexed(Unique:=True)> _
    Public Property IDNumber As String
        Get
            Return fIDNo
        End Get
        Set(value As String)
            SetPropertyValue("IDNumber", fIDNo, value)
            If IsLoading = False And value IsNot Nothing Then
                UpdateStudentDetails(value)
            End If
        End Set
    End Property
    <VisibleInListView(False)> _
    <Appearance("PostalAddressDisabled", Enabled:=False)> _
    Public Property StudentPostalAddress As String
        Get
            Return fStudentPostalAddress
        End Get
        Set(value As String)
            SetPropertyValue("PostalAddress", fStudentPostalAddress, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <Appearance("PostalCodeDisabled", Enabled:=False)> _
    Public Property StudentPostalCode As String
        Get
            Return fStudentPostalCode
        End Get
        Set(value As String)
            SetPropertyValue("StudentPostalCode", fStudentPostalCode, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <Appearance("StudentTownDisabled", Enabled:=False)> _
    Public Property StudentTown As String
        Get
            Return fStudentTown
        End Get
        Set(value As String)
            SetPropertyValue("StudentTown", fStudentTown, value)
        End Set
    End Property
    <Appearance("StudentCellNoDisabled", Enabled:=False)> _
    Public Property StudentCellNo As String
        Get
            Return fStudentCellNo
        End Get
        Set(value As String)
            SetPropertyValue("StudentCellNo", fStudentCellNo, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <Appearance("StudentEmailDisabled", Enabled:=False)> _
    Public Property StudentEmail As String
        Get
            Return fStudentEmail
        End Get
        Set(value As String)
            SetPropertyValue("StudentEmail", fStudentEmail, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <Appearance("StudentNatioalityDisabled", Enabled:=False)> _
    Public Property Nationality As String
        Get
            Return fNationality
        End Get
        Set(value As String)
            SetPropertyValue("Nationality", fNationality, value)
        End Set
    End Property
    <Appearance("StudentBranchDisabled", Enabled:=False)> _
    <RuleRequiredField()> _
    Public Property Branch As String
        Get
            Return fBranch
        End Get
        Set(value As String)
            SetPropertyValue("Branch", fBranch, value)
        End Set
    End Property
    <Appearance("DLClassDisabled", Criteria:="DLClass!=?", Enabled:=False)> _
    Public Property DLClass As DLClass
        Get
            Return fDLClass
        End Get
        Set(value As DLClass)
            SetPropertyValue("DLClass", fDLClass, value)
        End Set
    End Property

    Public Property CommecementDate As Date
        Get
            Return fCommecementDate
        End Get
        Set(value As Date)
            SetPropertyValue("CommecementDate", fCommecementDate, value)
        End Set
    End Property
    <Appearance("LessonsAppliedDisabled", Criteria:="LessonsApplied!=0", Enabled:=False)> _
    Public Property LessonsApplied As Int16
        Get
            Return fLessonsApplied
        End Get
        Set(value As Int16)
            SetPropertyValue("LessonsApplied", fLessonsApplied, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property AdditionalLessons As Int16
        Get
            Return fAdditionalLessons
        End Get
        Set(value As Int16)
            SetPropertyValue("AdditionalLessons", fAdditionalLessons, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property HasPreviousExperience As Boolean
        Get
            Return fHasPreviousExperience
        End Get
        Set(value As Boolean)
            SetPropertyValue("HasPreviousExperience", fHasPreviousExperience, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property ExperienceObtainedFrom As String
        Get
            Return fExperienceObtainedFrom
        End Get
        Set(value As String)
            SetPropertyValue("ExperienceObtainedFrom", fExperienceObtainedFrom, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property HasHealthCondition As Boolean
        Get
            Return fHasHealthCondition
        End Get
        Set(value As Boolean)
            SetPropertyValue("HasHealthCondition", fHasHealthCondition, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <Size(200)> _
    <RuleRequiredField("HasHealthConditionRqd", DefaultContexts.Save, TargetCriteria:="HasHealthCondition='True'")> _
    Public Property HealthConditionDetails As String
        Get
            Return fHealthConditionDetails
        End Get
        Set(value As String)
            SetPropertyValue("HealthConditionDetails", fHealthConditionDetails, value)
        End Set
    End Property
    <Association("StudentMgr-DscPayments", GetType(DscPayments))> _
    Public ReadOnly Property Payments() As XPCollection
        Get
            Return GetCollection("Payments")
        End Get
    End Property
    <DevExpress.Xpo.Aggregated(), Association("StudentMgr-CompletionDocuments", GetType(CompletionDocuments))> _
    Public ReadOnly Property DSCDocuments() As XPCollection(Of CompletionDocuments)
        Get
            Return GetCollection(Of CompletionDocuments)("DSCDocuments")
        End Get
    End Property
    Public Sub UpdateStudentDetails(ByVal IdNumber As String)
        If (Not IsLoading) AndAlso (Not IsSaving) AndAlso IdNumber IsNot Nothing Then
            GetUow()
            Dim cr As CriteriaOperator = New BinaryOperator("IDPPNumber", IdNumber, BinaryOperatorType.Equal)
            Using xpc As New XPCollection(Of MemberRegistration)(uow, cr)
                For Each rec In xpc
                    fMemberNo = rec.MemberNumber
                    fStudentName = rec.MemberName
                    fStudentPostalAddress = rec.PostAddress
                    fStudentPostalCode = rec.PostCode
                    fStudentTown = rec.Town
                    fStudentCellNo = rec.CellNumber
                    fStudentEmail = rec.Email
                    Branch = rec.Branch.BranchName
                    fNationality = rec.Nationality.Name
                    Exit For
                Next
            End Using
        End If
    End Sub
End Class
Public Class EmergencyContacts
    Inherits SponsorDetails
    Private fName, fSRelationship, faddress, fPostalCode, fTown, fCellNo, fEmail As String
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <VisibleInListView(False)> _
    Public Property Name As String
        Get
            Return fName
        End Get
        Set(value As String)
            SetPropertyValue("Name", fName, value)
        End Set
    End Property
    Public Property SRelationship As String
        Get
            Return fSRelationship
        End Get
        Set(value As String)
            SetPropertyValue("SRelationship", fSRelationship, value)
        End Set
    End Property
    Public Property PostalAddress As String
        Get
            Return faddress
        End Get
        Set(value As String)
            SetPropertyValue("PostalAddress", faddress, value)
        End Set
    End Property
    Public Property PostalCode As String
        Get
            Return fPostalCode
        End Get
        Set(value As String)
            SetPropertyValue("PostalCode", fPostalCode, value)
        End Set
    End Property
    Public Property Town As String
        Get
            Return fTown
        End Get
        Set(value As String)
            SetPropertyValue("Town", fTown, value)
        End Set
    End Property
    Public Property CellNo As String
        Get
            Return fCellNo
        End Get
        Set(value As String)
            SetPropertyValue("CellNo", fCellNo, value)
        End Set
    End Property
    Public Property Email As String
        Get
            Return fEmail
        End Get
        Set(value As String)
            SetPropertyValue("Email", fEmail, value)
        End Set
    End Property
End Class
Public Class SponsorDetails
    Inherits BaseObject
    Private fSponsorName, fRelationship, fSponsorAddress, fSponsorPostalCode, fSponsorTown, fSponsorCellNo, fSponsorEmail As String
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Property SponsorName As String
        Get
            Return fSponsorName
        End Get
        Set(value As String)
            SetPropertyValue("SponsorName", fSponsorName, value)
        End Set
    End Property
    Public Property Relationship As String
        Get
            Return fRelationship
        End Get
        Set(value As String)
            SetPropertyValue("Relationship", fRelationship, value)
        End Set
    End Property
    Public Property SponsorPostalAddress As String
        Get
            Return fSponsorAddress
        End Get
        Set(value As String)
            SetPropertyValue("SponsorPostalAddress", fSponsorAddress, value)
        End Set
    End Property
    Public Property SponsorPostalCode As String
        Get
            Return fSponsorPostalCode
        End Get
        Set(value As String)
            SetPropertyValue("SponsorPostalCode", fSponsorPostalCode, value)
        End Set
    End Property
    Public Property SponsorTown As String
        Get
            Return fSponsorTown
        End Get
        Set(value As String)
            SetPropertyValue("SponsorTown", fSponsorTown, value)
        End Set
    End Property
    Public Property SponsorCellNo As String
        Get
            Return fSponsorCellNo
        End Get
        Set(value As String)
            SetPropertyValue("SponsorCellNo", fSponsorCellNo, value)
        End Set
    End Property
    Public Property SponsorEmail As String
        Get
            Return fSponsorEmail
        End Get
        Set(value As String)
            SetPropertyValue("SponsorEmail", fSponsorEmail, value)
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
        If String.Compare(propertyName, "IDNumber", False) = 0 Or String.Compare(propertyName, "Branch", False) = 0 Or String.Compare(propertyName, "CommecementDate", False) = 0 Then
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
        ' Place here your initialization code.
    End Sub
End Class