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
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.DC
<ImageName("BO_Customer")> _
<XafDefaultProperty("MemberNumber")>
Public Class MemberRegistration
    Inherits BaseObject
    Private fTitle As TTitles
    Private fMemberNo, fOldNumber, fMemberName, fCorporateName, fPinNo, fIDPPNumber, _
        fContactPerson, fDesignation, fPostAddress, fPostCode, fTown, flocation, _
        fStreet, fHseNo, fTel, fCellNo, fEmail, fCardSerialNo, fCardPrintTime, fPolicyNo, fVehicleNo, fBlacklistReason As String
    Private fGender As Ggender
    Private fDOB, fJoiningDate, fPolicyExpDate As Date
    Private fNationality As Country
    Private fMaritalStatus As MStatus
    Private fMStatus As MemStatus
    Private fCorporateNo As MemberRegistration
    Private fAnnMonth As Month
    Private fSocialMedia As Media
    Private fMembershipType As MembershipType
    Private fVehiclesCount As Nullable(Of Integer) = Nothing
    Private ReadOnly NewSession As Boolean
    Private _IsBlacklisted, fHasFreeTow, fHasFreeIDP, fHasFullMechanical, fHasAirEvacuation, fHasFreeGroundAmbulance, fHasFreeWheelAlignment, _
        fHasPreInsValuation As Boolean
    Private ReadOnly roleName As String
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

    <Appearance("MemberNumberDisabled", Enabled:=False)> _
    <Indexed(Unique:=True)> _
    Public Property MemberNumber() As String
        Get
            Return fMemberNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("MemberNumber", fMemberNo, value)
        End Set
    End Property

    <Appearance("DisableOldNumber", Enabled:=False)> _
    Public Property OldNumber() As String
        Get
            Return fOldNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue("OldNumber", fOldNumber, value)
        End Set
    End Property


    <VisibleInListView(False)> _
    <ImmediatePostData()> _
    <RuleRequiredField()> _
    <Association("IndividualRegistration-MembershipType", GetType(MembershipType))> _
    Public Property MembershipType As MembershipType
        Get
            Return fMembershipType
        End Get
        Set(ByVal value As MembershipType)
            If SetPropertyValue("MembershipType", fMembershipType, value) Then
                Category = Nothing
            End If
        End Set
    End Property
    Private fCategory As MemberCategory
    <RuleRequiredField()> _
    <DataSourceCriteria("[Status]='Active'")> _
    <DataSourceProperty("MembershipType.Category", DataSourcePropertyIsNullMode.SelectNothing)> _
    Public Property Category() As MemberCategory
        Get
            Return fCategory
        End Get
        Set(ByVal value As MemberCategory)
            SetPropertyValue("Category", fCategory, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <Appearance("HideTitle", Criteria:="MembershipType='CORPORATE'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    Public Property Title() As TTitles
        Get
            Return fTitle
        End Get
        Set(ByVal value As TTitles)
            SetPropertyValue("Title", fTitle, value)
        End Set
    End Property

    <RuleRequiredField()> _
    Public Property MemberName() As String
        Get
            Return fMemberName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("MemberName", fMemberName, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <Appearance("HideGender", Criteria:="MembershipType='CORPORATE'", visibility:=ViewItemVisibility.Hide)> _
    Public Property Gender() As Ggender
        Get
            Return fGender
        End Get
        Set(ByVal value As Ggender)
            SetPropertyValue("Gender", fGender, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property DOB() As Date
        Get
            Return fDOB
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("DOB", fDOB, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property Nationality() As Country
        Get
            Return fNationality
        End Get
        Set(ByVal value As Country)
            SetPropertyValue("Nationality", fNationality, value)
        End Set
    End Property

    <RuleRequiredField()> _
    Public Property IDPPNumber() As String
        Get
            Return fIDPPNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue("IDPPNumber", fIDPPNumber, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property Profession As Profession
    <VisibleInListView(False)> _
    Private _isCorporateMember As Boolean

    <VisibleInListView(False)> _
    <ImmediatePostData()> _
    <Appearance("HideIsCorporate", Criteria:="MembershipType='CORPORATE'", visibility:=ViewItemVisibility.Hide)> _
    Public Property IsCorporateMember() As Boolean
        Get
            Return _isCorporateMember
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("IsCorporate", _isCorporateMember, value)
            If IsCorporateMember = True Then
                _isScheme = False
            End If
        End Set
    End Property
    Private _isScheme As Boolean

    <VisibleInListView(False)> _
    <ImmediatePostData()> _
    <Appearance("HideIsScheme", Criteria:="MembershipType='CORPORATE'", visibility:=ViewItemVisibility.Hide)> _
    Public Property IsScheme() As Boolean
        Get
            Return _isScheme
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("IsScheme", _isScheme, value)
            If IsScheme = True Then
                _isCorporateMember = False
                fCorporateNo = Nothing
            End If
        End Set
    End Property

    Private _IsCompany As Boolean
    <VisibleInListView(False)> _
    <ImmediatePostData()> _
    <Appearance("HideIsCompany", Criteria:="MembershipType='CORPORATE'", visibility:=ViewItemVisibility.Hide)> _
    Public Property IsCompany() As Boolean
        Get
            Return _IsCompany
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("IsCompany", _IsCompany, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <Appearance("HideMaritalStatus", Criteria:="IsScheme='True' Or MembershipType='CORPORATE'", visibility:=ViewItemVisibility.Hide)> _
    Public Property MaritalStatus() As MStatus
        Get
            Return fMaritalStatus
        End Get
        Set(ByVal value As MStatus)
            SetPropertyValue("MatitalStatus", fMaritalStatus, value)
        End Set
    End Property

    <Appearance("CorporateNameDisabled", Enabled:=False)>
    <VisibleInListView(False)> _
    <Appearance("HideCorporateName", Criteria:="IsCorporateMember='False'", visibility:=ViewItemVisibility.Hide)> _
    Public Property CorporateName() As String
        Get
            Return fCorporateName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CorporateName", fCorporateName, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <RuleRequiredField(TargetCriteria:="IsCorporateMember='True'")> _
    <Appearance("HideDisableCorporateMember", Criteria:="IsCorporateMember='False'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    Public Property CorporateMembershipNo() As MemberRegistration
        Get
            Return fCorporateNo
        End Get
        Set(ByVal value As MemberRegistration)
            SetPropertyValue("CorporateMember", fCorporateNo, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    Public Property PINNumber() As String
        Get
            Return fPinNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PINNumber", fPinNo, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <Appearance("HideContactPerson", Criteria:="MembershipType!='CORPORATE'", visibility:=ViewItemVisibility.Hide)> _
    Public Property ContactPerson() As String
        Get
            Return fContactPerson
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ContactPerson", fContactPerson, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <Appearance("HideDesignation", Criteria:="MembershipType!='CORPORATE'", visibility:=ViewItemVisibility.Hide)> _
    Public Property Designation() As String
        Get
            Return fDesignation
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Designation", fDesignation, value)
        End Set
    End Property

    Public ReadOnly Property MembershipStatus() As MemStatus
        Get
            If NewSession = False And Category IsNot Nothing Then
                If (ExpiryDate.Date < Date.Today) And (ExpiryDate.Date >= Date.Today.AddMonths(-Category.Span)) Then
                    fMStatus = MemberRegistration.MemStatus.Arrears
                ElseIf (ExpiryDate.Date < DateTime.Now.AddMonths(-Category.Span)) And (ExpiryDate.Date >= DateTime.Now.AddMonths(-2 * Category.Span)) Then
                    fMStatus = MemberRegistration.MemStatus.Lapsed
                ElseIf (ExpiryDate.Date < DateTime.Now.AddMonths(-2 * Category.Span)) And (ExpiryDate.Date <> Nothing) Then
                    fMStatus = MemberRegistration.MemStatus.Archived
                ElseIf (ExpiryDate.Date >= Date.Today) Then
                    fMStatus = MemberRegistration.MemStatus.Valid
                ElseIf (ExpiryDate.Date = Nothing) Then
                    fMStatus = MemberRegistration.MemStatus.Arrears
                End If
            End If
            Return fMStatus
        End Get
    End Property

    <VisibleInListView(False)> _
    Public ReadOnly Property NoOfVehicles() As Integer
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fVehiclesCount.HasValue Then
                UpdateVehiclesCount(False)
            End If
            Return fVehiclesCount
        End Get
    End Property

    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property PostAddress() As String
        Get
            Return fPostAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PostAddress", fPostAddress, value)
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

    <RuleRequiredField()> _
    <VisibleInListView(False)> _
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
            SetPropertyValue("HouseNumber", fHseNo, value)
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
    <RuleRegularExpression("", DefaultContexts.Save, "^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")> _
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
        Get
            Return fSocialMedia
        End Get
        Set(ByVal value As Media)
            SetPropertyValue("SocialMedia", fSocialMedia, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <Appearance("JoiningDateDisabled", Enabled:=False)> _
    Public Property JoiningDate() As Date
        Get
            Return fJoiningDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("JoiningDate", fJoiningDate, value)
        End Set
    End Property

    <Appearance("AnniversaryMonthDisabled", Enabled:=False)> _
    Public Property AnniversaryMonth() As Month
        Get
            If NewSession = True Then
                fAnnMonth = DatePart(DateInterval.Month, JoiningDate)
            End If
            Return fAnnMonth
        End Get
        Set(ByVal value As Month)
            SetPropertyValue("AnniversaryMonth", fAnnMonth, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <Association("IndividualRegistration-MemberClassification", GetType(MemberClassification))> _
    Public Property MemberClassification As MemberClassification

    <RuleRequiredField()> _
    <Association("IndividualRegistration-Branch", GetType(Branch))> _
    Public Property Branch As Branch

    <VisibleInListView(False)> _
    <Appearance("DisableCardsSerial", Enabled:=False)> _
    Public Property CardSerialNo() As String
        Get
            Return fCardSerialNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CardSerialNo", fCardSerialNo, value)
        End Set
    End Property
    <VisibleInListView(False)> _
<Appearance("DisableCardPrintTime", Enabled:=False)> _
    Public Property CardPrintTime() As String
        Get
            Return fCardPrintTime
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CardPrintTime", fCardPrintTime, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <Appearance("HidePolicyNo", Criteria:="IsScheme='False'", visibility:=ViewItemVisibility.Hide)> _
    Public Property PolicyNumber() As String
        Get
            Return fPolicyNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PolicyNumber", fPolicyNo, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <RuleRequiredField("VehicleNumberRqd", DefaultContexts.Save, TargetCriteria:="[<MemberRegistration>][^.Category.VehicleRqd]='True'")> _
    Public Property VehicleNumber() As String
        Get
            Return fVehicleNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("VehicleNumber", fVehicleNo, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <Appearance("HidePolicyDate", Criteria:="IsScheme='False'", visibility:=ViewItemVisibility.Hide)> _
    <RuleRequiredField("PolicyExpDateRqd", DefaultContexts.Save, TargetCriteria:="IsScheme='True'")> _
    Public Property PolicyExpDate() As Date
        Get
            Return fPolicyExpDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("PolicyExpDate", fPolicyExpDate, value)
        End Set
    End Property
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <Appearance("NameBackColor", Criteria:="Blacklisted='True'", BackColor:="Red"), _
    Appearance("BlacklistDisabled", Enabled:=False)> _
    Public Property Blacklisted() As Boolean
        Get
            Return _IsBlacklisted
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("Blacklisted", _IsBlacklisted, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    <Appearance("RecruiterDisabled", Criteria:="RecruitedBy!=? && roleName!='MEMBERSHIP ADMIN'", Enabled:=False)> _
    Public Property RecruitedBy As Recruiter

    <VisibleInListView(False)> _
    <Appearance("DisableBlacklist", Enabled:=False)> _
    Public Property BlacklistReason() As String
        Get
            Return fBlacklistReason
        End Get
        Set(ByVal value As String)
            SetPropertyValue("BlacklistReason", fBlacklistReason, value)
        End Set
    End Property

    Private fFreeTowBalance As Nullable(Of Int16) = Nothing

    Public ReadOnly Property FreeTowBalance() As Nullable(Of Int16)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fFreeTowBalance.HasValue Then
                UpdateFreeTows(False)
            End If
            Return fFreeTowBalance
        End Get
    End Property

    Public ReadOnly Property HasFreeTow() As Boolean
        Get
            If NewSession = False And Category IsNot Nothing Then
                Try
                    If String.Compare(Category.Code, "LIF", False) = 0 Or String.Compare(Category.Code, "SPL", False) = 0 Then
                        Dim AnvDay = DatePart(DateInterval.Day, JoiningDate)
                        Dim AnvMon = DatePart(DateInterval.Month, JoiningDate)
                        Dim AnvYr = DatePart(DateInterval.Year, Date.Now)
                        Dim CurAnvDate As Date = Convert.ToDateTime(String.Format("{0}/{1}/{2}", AnvDay, AnvMon, AnvYr))
                        Dim AnvDate As Date
                        If CurAnvDate <= Date.Now Then
                            AnvDate = CurAnvDate
                        ElseIf CurAnvDate > Date.Now Then
                            AnvDate = DateAdd(DateInterval.Year, -1, CurAnvDate)
                        End If
                        Dim NextAnvDate As Date = DateAdd(DateInterval.Year, 1, AnvDate)
                        Dim TowsCount As Nullable(Of Integer) = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("[<RescueDetails>][MemberNo = ^.This && IsFreeTow='True' && RescueDate between(?,?)].Count()", AnvDate, NextAnvDate)))
                        fHasFreeTow = If((NextAnvDate >= Today And TowsCount < Category.MaxFreeTows), True, False)
                    ElseIf (ExpiryDate >= Today) Then
                        Dim TowsCount As Nullable(Of Integer) = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("[<RescueDetails>][MemberNo = ^.This && IsFreeTow='True' && RescueDate between(?,?)].Count()", EffectiveDate, ExpiryDate)))
                        If TowsCount < Category.MaxFreeTows Then
                            fHasFreeTow = True
                        Else
                            fHasFreeTow = False
                        End If
                    Else
                        fHasFreeTow = False
                    End If
                Catch ex As Exception
                    Throw
                End Try
            End If
            Return fHasFreeTow
        End Get
    End Property
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

    <VisibleInListView(False)> _
    <Appearance("DisableExpiryDate", Enabled:=False)> _
    Public Property ExpiryDate() As Date
        Get
            Return _ExpiryDate
        End Get
        Set(value As Date)
            SetPropertyValue("ExpiryDate", _ExpiryDate, value)
        End Set
    End Property
    Protected _FreeTowDate As Date
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property FreeTowDate() As Date
        Get
            Return _FreeTowDate
        End Get
        Set(value As Date)
            SetPropertyValue("FreeTowDate", _FreeTowDate, value)
        End Set
    End Property
    Public ReadOnly Property HasFreeIDP() As Boolean
        Get
            If NewSession = False And Category IsNot Nothing Then
                Try
                    If String.Compare(Category.Code, "LIF", False) = 0 Or String.Compare(Category.Code, "SPL", False) = 0 Then
                        Dim AnvDay = DatePart(DateInterval.Day, JoiningDate)
                        Dim AnvMon = DatePart(DateInterval.Month, JoiningDate)
                        Dim AnvYr = DatePart(DateInterval.Year, Date.Now)
                        Dim CurAnvDate As Date = Convert.ToDateTime(String.Format("{0}/{1}/{2}", AnvDay, AnvMon, AnvYr))
                        Dim AnvDate As Date
                        If CurAnvDate <= Date.Now Then
                            AnvDate = CurAnvDate
                        ElseIf CurAnvDate > Date.Now Then
                            AnvDate = DateAdd(DateInterval.Year, -1, CurAnvDate)
                        Else
                            AnvDate = ModifiedOn
                        End If
                        Dim NextAnvDate As Date = DateAdd(DateInterval.Year, 1, AnvDate)
                        Dim FreeIDPCount As Nullable(Of Integer) = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("[<IDP_Details>][MemberNumber = ^.This && IsFreeIDP='True' && IDPIssueDate between(?,?)].Count()", AnvDate, NextAnvDate)))

                        fHasFreeIDP = If((NextAnvDate >= Today And FreeIDPCount < Category.FreeIDPs), True, False)
                    ElseIf (ExpiryDate >= Today) Then
                        Dim FreeIDPCount As Nullable(Of Integer) = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("[<IDP_Details>][MemberNumber = ^.This && IsFreeIDP='True' && IDPIssueDate between(?,?)].Count()", EffectiveDate, ExpiryDate)))
                        If FreeIDPCount < Category.FreeIDPs Then
                            fHasFreeIDP = True
                        Else
                            fHasFreeIDP = False
                        End If
                    Else
                        fHasFreeIDP = False
                    End If
                Catch ex As Exception
                    Throw
                End Try
            End If
            Return fHasFreeIDP
        End Get
    End Property
    <VisibleInListView(False)> _
    Public ReadOnly Property HasFullMechanical() As Boolean
        Get
            If NewSession = False And Category IsNot Nothing Then
                Try
                    If String.Compare(Category.Code, "LIF", False) = 0 Or String.Compare(Category.Code, "SPL", False) = 0 Then
                        Dim AnvDay = DatePart(DateInterval.Day, JoiningDate)
                        Dim AnvMon = DatePart(DateInterval.Month, JoiningDate)
                        Dim AnvYr = DatePart(DateInterval.Year, Date.Now)
                        Dim CurAnvDate As Date = Convert.ToDateTime(String.Format("{0}/{1}/{2}", AnvDay, AnvMon, AnvYr))
                        Dim AnvDate As Date
                        If CurAnvDate <= Date.Now Then
                            AnvDate = CurAnvDate
                        ElseIf CurAnvDate > Date.Now Then
                            AnvDate = DateAdd(DateInterval.Year, -1, CurAnvDate)
                        Else
                            AnvDate = ModifiedOn
                        End If
                        Dim NextAnvDate As Date = DateAdd(DateInterval.Year, 1, AnvDate)
                        Dim FreeFullMechanicalCount As Nullable(Of Integer) = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("[<Voucher>][MemberNo = ^.This && FullMechanical='True' && ModifiedOn between(?,?)].Count()", AnvDate, NextAnvDate)))

                        fHasFullMechanical = If((NextAnvDate >= Today And FreeFullMechanicalCount < Category.MaxFullMechVal), True, False)
                    ElseIf (ExpiryDate >= Today) Then
                        Dim FreeFullMechanicalCount As Nullable(Of Integer) = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("[<Voucher>][MemberNo = ^.This && FullMechanical='True' && ModifiedOn between(?,?)].Count()", EffectiveDate, ExpiryDate)))
                        If FreeFullMechanicalCount < Category.MaxFullMechVal Then
                            fHasFullMechanical = True
                        Else
                            fHasFullMechanical = False
                        End If
                    Else
                        fHasFullMechanical = False
                    End If
                Catch ex As Exception
                    Throw
                End Try
            End If
            Return fHasFullMechanical
        End Get
    End Property
    <VisibleInListView(False)> _
    Public ReadOnly Property HasPreInsValuation() As Boolean
        Get
            If NewSession = False And Category IsNot Nothing Then
                    Try
                        If String.Compare(Category.Code, "LIF", False) = 0 Or String.Compare(Category.Code, "SPL", False) = 0 Then
                            Dim AnvDay = DatePart(DateInterval.Day, JoiningDate)
                            Dim AnvMon = DatePart(DateInterval.Month, JoiningDate)
                            Dim AnvYr = DatePart(DateInterval.Year, Date.Now)
                            Dim CurAnvDate As Date = Convert.ToDateTime(String.Format("{0}/{1}/{2}", AnvDay, AnvMon, AnvYr))
                            Dim AnvDate As Date
                            If CurAnvDate <= Date.Now Then
                                AnvDate = CurAnvDate
                            ElseIf CurAnvDate > Date.Now Then
                                AnvDate = DateAdd(DateInterval.Year, -1, CurAnvDate)
                            Else
                                AnvDate = ModifiedOn
                            End If
                            Dim NextAnvDate As Date = DateAdd(DateInterval.Year, 1, AnvDate)
                            Dim PreInsValuationCount As Nullable(Of Integer) = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("[<Voucher>][MemberNo = ^.This && PreInsValuation='True' && ModifiedOn between(?,?)].Count()", AnvDate, NextAnvDate)))
                            fHasPreInsValuation = If((NextAnvDate >= Today And PreInsValuationCount < Category.PreInsVal), True, False)
                        ElseIf (ExpiryDate >= Today) Then
                            Dim PreInsValuationCount As Nullable(Of Integer) = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("[<Voucher>][MemberNo = ^.This && PreInsValuation='True' && ModifiedOn between(?,?)].Count()", EffectiveDate, ExpiryDate)))
                            If PreInsValuationCount < Category.PreInsVal Then
                                fHasPreInsValuation = True
                            Else
                                fHasPreInsValuation = False
                            End If
                        Else
                            fHasPreInsValuation = False
                        End If
                    Catch ex As Exception
                        Throw
                End Try
            End If
            Return fHasPreInsValuation
        End Get
    End Property
    Public ReadOnly Property HasFreeGroundAmbulance() As Boolean
        Get
            If NewSession = False And Category IsNot Nothing Then
                Try
                    If String.Compare(Category.Code, "LIF", False) = 0 Or String.Compare(Category.Code, "SPL", False) = 0 Then
                        Dim AnvDay = DatePart(DateInterval.Day, JoiningDate)
                        Dim AnvMon = DatePart(DateInterval.Month, JoiningDate)
                        Dim AnvYr = DatePart(DateInterval.Year, Date.Now)
                        Dim CurAnvDate As Date = Convert.ToDateTime(String.Format("{0}/{1}/{2}", AnvDay, AnvMon, AnvYr))
                        Dim AnvDate As Date
                        If CurAnvDate <= Date.Now Then
                            AnvDate = CurAnvDate
                        ElseIf CurAnvDate > Date.Now Then
                            AnvDate = DateAdd(DateInterval.Year, -1, CurAnvDate)
                        Else
                            AnvDate = ModifiedOn
                        End If
                        Dim NextAnvDate As Date = DateAdd(DateInterval.Year, 1, AnvDate)
                        Dim FreeGroundAmbulanceCount As Nullable(Of Integer) = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("[<RescueDetails>][MemberNo = ^.This && IsFreeGroundAmbulance='True' && ModifiedOn between(?,?)].Count()", AnvDate, NextAnvDate)))

                        fHasFreeGroundAmbulance = If((NextAnvDate >= Today And FreeGroundAmbulanceCount < Category.GroundAmbulance), True, False)
                    ElseIf (ExpiryDate >= Today) Then
                        Dim FreeGroundAmbulanceCount As Nullable(Of Integer) = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("[<RescueDetails>][MemberNo = ^.This && IsFreeGroundAmbulance='True' && ModifiedOn between(?,?)].Count()", EffectiveDate, ExpiryDate)))
                        If FreeGroundAmbulanceCount < Category.GroundAmbulance Then
                            fHasFreeGroundAmbulance = True
                        Else
                            fHasFreeGroundAmbulance = False
                        End If
                    Else
                        fHasFreeGroundAmbulance = False
                    End If
                Catch ex As Exception
                    Throw
                End Try
            End If
            Return fHasFreeGroundAmbulance
        End Get
    End Property
    Public ReadOnly Property HasFreeWheelAlignment() As Boolean
        Get
            If NewSession = False And Category IsNot Nothing Then
                Try
                    If String.Compare(Category.Code, "LIF", False) = 0 Or String.Compare(Category.Code, "SPL", False) = 0 Then
                        Dim AnvDay = DatePart(DateInterval.Day, JoiningDate)
                        Dim AnvMon = DatePart(DateInterval.Month, JoiningDate)
                        Dim AnvYr = DatePart(DateInterval.Year, Date.Now)
                        Dim CurAnvDate As Date = Convert.ToDateTime(String.Format("{0}/{1}/{2}", AnvDay, AnvMon, AnvYr))
                        Dim AnvDate As Date
                        If CurAnvDate <= Date.Now Then
                            AnvDate = CurAnvDate
                        ElseIf CurAnvDate > Date.Now Then
                            AnvDate = DateAdd(DateInterval.Year, -1, CurAnvDate)
                        Else
                            AnvDate = ModifiedOn
                        End If
                        Dim NextAnvDate As Date = DateAdd(DateInterval.Year, 1, AnvDate)
                        Dim FreeWheelAlignmentCount As Nullable(Of Integer) = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("[<Voucher>][MemberNo = ^.This && WheelAlignment='True' && ModifiedOn between(?,?)].Count()", AnvDate, NextAnvDate)))
                        fHasFreeWheelAlignment = If((NextAnvDate >= Today And FreeWheelAlignmentCount < Category.WheelAlignment), True, False)
                    ElseIf (ExpiryDate >= Today) Then
                        Dim FreeWheelAlignmentCount As Nullable(Of Integer) = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("[<Voucher>][MemberNo = ^.This && WheelAlignment='True' && ModifiedOn between(?,?)].Count()", EffectiveDate, ExpiryDate)))
                        If FreeWheelAlignmentCount < Category.WheelAlignment Then
                            fHasFreeWheelAlignment = True
                        Else
                            fHasFreeWheelAlignment = False
                        End If
                    Else
                        fHasFreeWheelAlignment = False
                    End If
                Catch ex As Exception
                    Throw
                End Try
            End If
            Return fHasFreeWheelAlignment
        End Get
    End Property
    Public ReadOnly Property HasAirEvacuation() As Boolean
        Get
            If NewSession = False And Category IsNot Nothing Then
                Try
                    If String.Compare(Category.Code, "LIF", False) = 0 Or String.Compare(Category.Code, "SPL", False) = 0 Then
                        Dim AnvDay = DatePart(DateInterval.Day, JoiningDate)
                        Dim AnvMon = DatePart(DateInterval.Month, JoiningDate)
                        Dim AnvYr = DatePart(DateInterval.Year, Date.Now)
                        Dim CurAnvDate As Date = Convert.ToDateTime(String.Format("{0}/{1}/{2}", AnvDay, AnvMon, AnvYr))
                        Dim AnvDate As Date
                        If CurAnvDate <= Date.Now Then
                            AnvDate = CurAnvDate
                        ElseIf CurAnvDate > Date.Now Then
                            AnvDate = DateAdd(DateInterval.Year, -1, CurAnvDate)
                        Else
                            AnvDate = ModifiedOn
                        End If
                        Dim NextAnvDate As Date = DateAdd(DateInterval.Year, 1, AnvDate)
                        Dim AirEvacuationCount As Nullable(Of Integer) = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("[<RescueDetails>][MemberNo = ^.This && IsFreeAirEvacuation='True' && RescueDate between(?,?)].Count()", AnvDate, NextAnvDate)))
                        fHasAirEvacuation = If((NextAnvDate >= Today And AirEvacuationCount < Category.AirEvacuation), True, False)
                    ElseIf (ExpiryDate >= Today) Then
                        Dim AirEvacuationCount As Nullable(Of Integer) = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("[<RescueDetails>][MemberNo = ^.This && IsFreeAirEvacuation='True' && RescueDate between(?,?)].Count()", EffectiveDate, ExpiryDate)))
                        If AirEvacuationCount < Category.AirEvacuation Then
                            fHasAirEvacuation = True
                        Else
                            fHasAirEvacuation = False
                        End If
                    Else
                        fHasAirEvacuation = False
                    End If
                Catch ex As Exception
                    Throw
                End Try
            End If
            Return fHasAirEvacuation
        End Get
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
        If String.Compare(propertyName, "MemberNumber", False) = 0 Or String.Compare(propertyName, "MemberName", False) = 0 Then
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
    Private _RequestedBy As String
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property RequestedBy() As String
        Get
            Return _RequestedBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue("RequestedBy", _RequestedBy, value)
        End Set
    End Property
    Private fSchemeVehicle As String
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property SchemeVehicle() As String
        Get
            Return fSchemeVehicle
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SchemeVehicle", fSchemeVehicle, value)
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
        fJoiningDate = Date.Today
        MaritalStatus = MStatus.NotApplicable
    End Sub

#Region "Properties and Colections"
    <Association("Vehicles-MemReg", GetType(Vehicles))> _
    Public ReadOnly Property Vehicles() As XPCollection
        Get
            Return GetCollection("Vehicles")
        End Get
    End Property

    <Association("IndReg-Payments", GetType(Payments))> _
    Public ReadOnly Property Payments() As XPCollection
        Get
            Return GetCollection("Payments")
        End Get
    End Property

    <Association("IndReg-RescueDetails")> _
    Public ReadOnly Property RescueDetails() As XPCollection(Of RescueDetails)
        Get
            Return GetCollection(Of RescueDetails)("RescueDetails")
        End Get
    End Property
    <Association("IdpDet-MemReg", GetType(IDP_Details))> _
    Public ReadOnly Property IDPDetails() As XPCollection
        Get
            Return GetCollection("IDPDetails")
        End Get
    End Property
#End Region
    <Association("Dependants-MemReg"), DevExpress.Xpo.Aggregated()> _
    Public ReadOnly Property Dependants() As XPCollection(Of Dependants)
        Get
            Return GetCollection(Of Dependants)("Dependants")
        End Get
    End Property
    <Association("IndiReg-Voucher"), DevExpress.Xpo.Aggregated()> _
    Public ReadOnly Property Voucher() As XPCollection(Of Voucher)
        Get
            Return GetCollection(Of Voucher)("Voucher")
        End Get
    End Property
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
    Public Enum MemStatus
        Valid = 1
        Arrears = 0
        Lapsed = 2
        Archived = 4
    End Enum
    Public Enum MStatus
        Married
        NotMarried
        NotApplicable
    End Enum
    Public Enum Ggender
        None = 0
        Male = 1
        Female = 2
    End Enum
    Public Enum TTitles
        MR
        MRS
        MISS
        MS
        DR
        PROF
        ENG
    End Enum

    Public Sub UpdateVehiclesCount(ByVal forceChangeEvents As Boolean)
        Dim oldVehiclesCount As Nullable(Of Integer) = fVehiclesCount
        fVehiclesCount = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("Vehicles.Count")))
        If forceChangeEvents Then
            OnChanged("NoOfVehicles", oldVehiclesCount, fVehiclesCount)
        End If
    End Sub

    Public Sub UpdateFreeTows(ByVal forceChangeEvents As Boolean)
        Dim oldTowsTotal As Nullable(Of Int16) = fFreeTowBalance
        Dim tempTotal As Int16 = 0
        Try
            If ExpiryDate >= Today Then
                For Each detail As RescueDetails In RescueDetails
                    tempTotal += detail.FreeTowDistance
                Next detail
                fFreeTowBalance = Category.FreeTows - tempTotal
                If forceChangeEvents Then
                    OnChanged("TowsTotal", oldTowsTotal, fFreeTowBalance)
                End If
            Else
                fFreeTowBalance = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If Not CorporateMembershipNo Is Nothing Then
            CorporateName = CorporateMembershipNo.MemberName.ToString
            CorporateMembershipNo.Save()
        End If
        If IsCompany = False And DateDiff(DateInterval.Year, DOB, Today) <= 18 And Not Category.Category = "CORPORATE" Then
            Throw New InvalidProgramException("Member's age must be greater than 18 Years")
        End If
        If IsScheme = False Then
            PolicyNumber = Nothing
        End If
    End Sub
    Protected Overrides Sub OnSaved()
        MyBase.OnSaved()
        If NewSession = True Then
            insertMemberNumber()
            Reload()
        End If
        If Not String.IsNullOrEmpty(VehicleNumber) Then
            SaveVehicle()
            Vehicles.Reload()
        End If
    End Sub

    Private Sub insertMemberNumber()
        Try
            Using connection As New SqlConnection(Constants.ConnectionString)
                connection.Open()
                Using cm = connection.CreateCommand
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "Sp_NextMemberNo"
                    With cm.Parameters
                        .AddWithValue("@Branch", Branch.Oid)
                    End With
                    cm.ExecuteNonQuery()
                End Using
                connection.Close()
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub SaveVehicle()
        Try
            Using connection As New SqlConnection(Constants.ConnectionString)
                connection.Open()
                Using cm = connection.CreateCommand
                    cm.CommandType = CommandType.StoredProcedure
                    cm.CommandText = "sp_VehicleSave"
                    With cm.Parameters
                        .AddWithValue("@IsScheme", IsScheme)
                        .AddWithValue("@RegNo", VehicleNumber)
                        .AddWithValue("@MemberNo", Oid)
                        .AddWithValue("@VehUse", If(MembershipType.ToString = "INDIVIDUAL", 0, 1))
                        .AddWithValue("@MemberName", MemberName)
                        .AddWithValue("@CreatedBy", Session.GetObjectByKey(Of User)(SecuritySystem.CurrentUserId).Oid)
                    End With
                    cm.ExecuteNonQuery()
                End Using
                connection.Close()
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
