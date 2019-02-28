Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports System.Data.SqlClient
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.DC

<XafDefaultProperty("StemNumber")> _
Public Class CorporateStem
    Inherits BaseObject
    Private fStemNo, fName, fCertRegNo, fPostAddress, fPostCode, fTown, fHseNo, flocation, fStreet, fTel, fCellNo, fEmail As String
    Private Property fMClasification As MemberClassification
    Private fNoOfPeople As Nullable(Of Integer) = Nothing
    Private fInceptionDate, fJoiningDate As Date
    Private fMemStatus As MemStatus
    Private fMembershipType As MembershipType
    Private fAnnMonth As Month
    Private roleName As String
    Private ReadOnly NewSession As Boolean
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        If Not IsLoading Then
            NewSession = True
        End If
        Dim usr As User = SecuritySystem.CurrentUser
        For Each role In usr.Roles
            roleName = role.Name
            Exit For
        Next
    End Sub

    <Appearance("NameFontIsRed", Criteria:="MembershipStatus='Lapsed'", FontColor:="Red"), _
    Appearance("NameFontIsAmber", Criteria:="MembershipStatus='Arrears'", FontColor:="Orange"), _
    Appearance("NameFontIsGreen", Criteria:="MembershipStatus='Valid'", FontColor:="Green")> _
    <Appearance("StemNumberDisabled", Enabled:=False)> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    <Indexed(Unique:=True)> _
    Public Property StemNumber() As String
        Get
            Return fStemNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("StemNumber", fStemNo, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Name", fName, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property CertRegNo() As String
        Get
            Return fCertRegNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CertRegNo", fCertRegNo, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property InceptionDate() As Date
        Get
            Return fInceptionDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("InceptionDate", fInceptionDate, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property Profession() As LineOfDuty
    Public ReadOnly Property MembershipStatus() As MemStatus
        Get
            If (ExpiryDate.Date < Date.Today) And (ExpiryDate.Date >= Date.Today.AddMonths(-12)) Then
                fMemStatus = CorporateStem.MemStatus.Arrears
            ElseIf (ExpiryDate.Date < DateTime.Now.AddMonths(-12)) And (ExpiryDate.Date >= DateTime.Now.AddMonths(-24)) Then
                fMemStatus = CorporateStem.MemStatus.Lapsed
            ElseIf (ExpiryDate.Date < DateTime.Now.AddMonths(-24)) And (ExpiryDate.Date <> Nothing) Then
                fMemStatus = CorporateStem.MemStatus.Archived
            ElseIf (ExpiryDate.Date >= Date.Today) Then
                fMemStatus = CorporateStem.MemStatus.Valid
            ElseIf (ExpiryDate.Date = Nothing) Then
                fMemStatus = CorporateStem.MemStatus.Arrears
            End If
            Return fMemStatus
        End Get
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public ReadOnly Property NoOfPeople() As Integer
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fNoOfPeople.HasValue Then
                UpdateMembersCount(False)
            End If
            Return fNoOfPeople
        End Get
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property PostAddress() As String
        Get
            Return fPostAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PostalAddress", fPostAddress, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property PostCode() As String
        Get
            Return fPostCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PostCode", fPostCode, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property Town() As String
        Get
            Return fTown
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Town", fTown, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property Location() As String
        Get
            Return flocation
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Location", flocation, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property Street() As String
        Get
            Return fStreet
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Street", fStreet, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property HouseNumber() As String
        Get
            Return fHseNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("HseNo", fHseNo, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property Telephone() As String
        Get
            Return fTel
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Telephone", fTel, value)
        End Set
    End Property

    <RuleRequiredField()> _
    Public Property CellNumber() As String
        Get
            Return fCellNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CellNo", fCellNo, value)
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
    Public Property SocialMedia() As Media
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property JoiningDate() As Date
        Get
            Return fJoiningDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("JoiningDate", fJoiningDate, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property AniversaryMonth() As Month
        Get
            Return fAnnMonth
        End Get
        Set(ByVal value As Month)
            SetPropertyValue("AniversaryMonth", fAnnMonth, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <ImmediatePostData()> _
    <RuleRequiredField()> _
    <Association("Corporate-MembershipType", GetType(MembershipType))> _
    Public Property MembershipType As MembershipType
        Get
            Return fMembershipType
        End Get
        Set(ByVal value As MembershipType)
            If SetPropertyValue("MembershipType", fMembershipType, value) Then
                MemberCategory = Nothing
            End If
        End Set
    End Property
    Private fMemberCategory As MemberCategory
    <RuleRequiredField()> _
    <DataSourceProperty("MembershipType.Category", DataSourcePropertyIsNullMode.SelectNothing)> _
    Public Property MemberCategory() As MemberCategory
        Get
            Return fMemberCategory
        End Get
        Set(ByVal value As MemberCategory)
            SetPropertyValue("MemberCategory", fMemberCategory, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property MemberClasification() As MemberClassification
        Get
            Return fMClasification
        End Get
        Set(ByVal value As MemberClassification)
            SetPropertyValue("MemberClassification", fMClasification, value)
        End Set
    End Property

    <RuleRequiredField()> _
    <Association("CorporateStem-Branch", GetType(CorporateStem))> _
    Public Property Branch As Branch
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    <Appearance("RecruiterDisabled", Criteria:="RecuitedBy!=? && roleName!='MEMBERSHIP ADMIN'", Enabled:=False)> _
    Public Property RecuitedBy As Recruiter
    Protected _EffectiveDate As Date
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property EffectiveDate() As Date
        Get
            Return _EffectiveDate
        End Get
        Set(value As Date)
            SetPropertyValue("EffectiveDate", _EffectiveDate, value)
        End Set
    End Property
    Protected _ExpiryDate As Date
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property ExpiryDate() As Date
        Get
            Return _ExpiryDate
        End Get
        Set(value As Date)
            SetPropertyValue("ExpiryDate", _ExpiryDate, value)
        End Set
    End Property
    <VisibleInListView(False)> _
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
        If String.Compare(propertyName, "StemNumber", False) = 0 Or String.Compare(propertyName, "Name", False) = 0 Or String.Compare(propertyName, "JoiningDate", False) = 0 Then
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
#Region "Properties and Colections"
    <Association("CorMem-IndMembers", GetType(MemberRegistration))> _
    Public ReadOnly Property StemMembers() As XPCollection
        Get
            Return GetCollection("StemMembers")
        End Get
    End Property

    <Association("CorMem-Vehicles", GetType(Vehicles))> _
    Public ReadOnly Property MemberVehicles() As XPCollection
        Get
            Return GetCollection("MemberVehicles")
        End Get
    End Property

    <Association("CorMem-Payments", GetType(Payments))> _
    Public ReadOnly Property MemberPayments() As XPCollection
        Get
            Return GetCollection("MemberPayments")
        End Get
    End Property
    <Association("CorMem-RescueDetails", GetType(RescueDetails))> _
    Public ReadOnly Property Rescue() As XPCollection
        Get
            Return GetCollection("Rescue")
        End Get
    End Property
#End Region
    Public Enum MemStatus
        Valid
        Arrears
        Lapsed
        Archived
    End Enum
    Public Enum Month
        None = 0
        January = 1
        February = 2
        March = 3
        April = 4
        May = 5
        June = 6
        July = 7
        August = 8
        September = 9
        October = 10
        November = 11
        December = 12
    End Enum
    Protected Overrides Sub OnSaved()
        MyBase.OnSaved()
        If NewSession = True Then
            insertMemberNumber()
            Reload()
        End If
    End Sub
    Public Sub UpdateMembersCount(ByVal forceChangeEvents As Boolean)
        Dim oldMembersCount = fNoOfPeople
        fNoOfPeople = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("StemMembers.Count")))
        If forceChangeEvents Then
            OnChanged("NoOfPeople", oldMembersCount, fNoOfPeople)
        End If
    End Sub
    Private Shared Sub insertMemberNumber()
        Try
            Using connection As New SqlConnection(Constants.ConnectionString)
                connection.Open()
                Using cm = connection.CreateCommand
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "Sp_NextStemNo"
                    cm.ExecuteNonQuery()
                End Using
                connection.Close()
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
