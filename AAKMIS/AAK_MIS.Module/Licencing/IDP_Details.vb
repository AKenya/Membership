Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.DC
Imports DevExpress.Persistent.Base.Security

<XafDefaultProperty("IDPNumber")> _
Public Class IDP_Details
    Inherits BaseObject
    Private fMemberNumber As MemberRegistration
    Private fClassesEndorsed, fCountriesPRqd, fPassportNo, fReceiptNo, fPlaceOfBirth, fDLNumber, fDirectorName, _
        fIDNumber, fCellNumber, fFullAddress, fCancelReason As String
    Private fIDPIssueDate, fIDPExpiryDate, fDLExpiryDate, fDOB As Date
    Private fGender As MemberRegistration.Ggender
    Private fAmount As Double
    Private fBranch As Branch
    Private fIDPNo As IDP_Allocation
    Private fNationality As Country
    Private _isFreeIDP As Boolean

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    <Appearance("IDPDetailsRule", AppearanceItemType:="ViewItem", Context:="DetailView", Enabled:=False, TargetItems:="*")> _
    Protected Function DisableIDPDetails() As Boolean
        Dim usr As User = SecuritySystem.CurrentUser
        For Each role As IRole In usr.Roles
            If String.Compare(role.Name, "ADMINISTRATORS", False) <> 0 And String.Compare(role.Name, "LICENSING ADMINISTRATOR", False) <> 0 Then
                Return Not (ModifiedOn > DateAdd(DateInterval.Day, -1, Today))
            End If
        Next
    End Function
    <Appearance("MemberNumberDisabled", Enabled:=False)> _
    <Association("IdpDet-MemReg", GetType(MemberRegistration))> _
    <RuleRequiredField()> _
    Public Property MemberNumber() As MemberRegistration
        Get
            Return fMemberNumber
        End Get
        Set(ByVal value As MemberRegistration)
            SetPropertyValue("MemberNumber", fMemberNumber, value)
            If Not fMemberNumber Is Nothing Then
                MemberName = MemberNumber.MemberName
            End If
        End Set
    End Property
    Protected fMemberName As String
    <Appearance("MemberNameDisabled", Enabled:=False)> _
    Public Property MemberName() As String
        Get
            Return fMemberName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("MemberName", fMemberName, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <RuleRequiredField(TargetCriteria:="MemberNumber.MembershipType='CORPORATE'")> _
    <Appearance("HideDName", Criteria:="MemberNumber.MembershipType!='CORPORATE'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    Public Property NameOfDirector() As String
        Get
            Return fDirectorName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("NameOfDirector", fDirectorName, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <RuleRequiredField(TargetCriteria:="MemberNumber.MembershipType='CORPORATE'")> _
    <Appearance("HideDDOB", Criteria:="MemberNumber.MembershipType!='CORPORATE'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    Public Property DirectorDOB() As Date
        Get
            Return fDOB
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("DirectorDOB", fDOB, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField(TargetCriteria:="MemberNumber.MembershipType='CORPORATE'")> _
    <Appearance("HideDIDNo", Criteria:="MemberNumber.MembershipType!='CORPORATE'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    Public Property DirectorIDNo() As String
        Get
            Return fIDNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue("DirectorIDNo", fIDNumber, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <ImmediatePostData()> _
    <Association("IDPsDetails-Branch", GetType(Branch))> _
    Public Property IssuingBranch As Branch
        Get
            Return fBranch
        End Get
        Set(ByVal value As Branch)
            If SetPropertyValue("IssuingBranch", fBranch, value) Then
                IDPNumber = Nothing
            End If
        End Set
    End Property
    <DataSourceCriteria("Department='Licensing'")> _
    <RuleRequiredField()> _
    Public Property IssuingOfficer() As User
    <RuleRequiredField()> _
    Public Property PassportNo() As String
        Get
            Return fPassportNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PassportNo", fPassportNo, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property PlaceOfBirth() As String
        Get
            Return fPlaceOfBirth
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PlaceOfBirth", fPlaceOfBirth, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property DLNumber As String
        Get
            Return fDLNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue("DLNumber", fDLNumber, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property DLExpiryDate As Date
        Get
            Return fDLExpiryDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("DLExpiryDate", fDLExpiryDate, value)
        End Set
    End Property

    <RuleRequiredField()> _
    <Appearance("IDPNoDisabled", Criteria:="IDPNumber!=?", Enabled:=False)> _
    <DataSourceProperty("IssuingBranch.IdpAllocationList", DataSourcePropertyIsNullMode.SelectNothing)> _
    Public Property IDPNumber As IDP_Allocation
        Get
            Return fIDPNo
        End Get
        Set(value As IDP_Allocation)
            SetPropertyValue("IDPNumber", fIDPNo, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField(TargetCriteria:="MemberNumber.MembershipType='CORPORATE'")> _
    <Appearance("HideGender", Criteria:="MemberNumber.MembershipType='INDIVIDUAL'", visibility:=ViewItemVisibility.Hide)> _
    Public Property Gender() As MemberRegistration.Ggender
        Get
            Return fGender
        End Get
        Set(ByVal value As MemberRegistration.Ggender)
            SetPropertyValue("Gender", fGender, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField(TargetCriteria:="MemberNumber.MembershipType='CORPORATE'")> _
    <Appearance("HideDNationality", Criteria:="MemberNumber.MembershipType='INDIVIDUAL'", visibility:=ViewItemVisibility.Hide)> _
    Public Property Nationality() As Country
        Get
            Return fNationality
        End Get
        Set(ByVal value As Country)
            SetPropertyValue("Nationality", fNationality, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField(TargetCriteria:="MemberNumber.MembershipType='CORPORATE'")> _
    <Appearance("HideDCellNumber", Criteria:="MemberNumber.MembershipType='INDIVIDUAL'", visibility:=ViewItemVisibility.Hide)> _
    Public Property CellNumber() As String
        Get
            Return fCellNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CellNumber", fCellNumber, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField(TargetCriteria:="MemberNumber.MembershipType='CORPORATE'")> _
    <Appearance("HideDFullAddress", Criteria:="MemberNumber.MembershipType='INDIVIDUAL'", visibility:=ViewItemVisibility.Hide)> _
    Public Property FullAddress() As String
        Get
            Return fFullAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue("FullAddress", fFullAddress, value)
        End Set
    End Property

    <RuleRequiredField()> _
    Public Property IDPIssueDate() As Date
        Get
            Return fIDPIssueDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("IDPIssueDate", fIDPIssueDate, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <VisibleInListView(False)> _
    Public Property IDPExpiryDate() As Date
        Get
            Return fIDPExpiryDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("IDPExpiryDate", fIDPExpiryDate, value)
        End Set
    End Property
    <Appearance("IsFreeIDPFalseComm", Criteria:="MemberNumber.HasFreeIDP=='False'", Enabled:=False)> _
    <ImmediatePostData()> _
    Public Property IsFreeIDP() As Boolean
        Get
            Return _isFreeIDP
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("IsFreeIDP", _isFreeIDP, value)
        End Set
    End Property
    <Appearance("IsReceiptNoFalse", Criteria:="IsFreeIDP=='True'", Enabled:=False)> _
    Public Property ReceiptNo() As String
        Get
            Return fReceiptNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ReceiptNo", fReceiptNo, value)
        End Set
    End Property
    <Appearance("AmountNotActive", Criteria:="IsFreeIDP=='True'", Enabled:=False)> _
    Public Property Amount() As Double
        Get
            Return fAmount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("Amount", fAmount, value)
        End Set
    End Property
    <Size(200)> _
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property ClassesEndorsed() As String
        Get
            Return fClassesEndorsed
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ClassesEndorsed", fClassesEndorsed, value)
        End Set
    End Property
    <Size(200)> _
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property CountriesPermitRequired() As String
        Get
            Return fCountriesPRqd
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CountriesPermitRequired", fCountriesPRqd, value)
        End Set
    End Property
    <Size(200)> _
    <VisibleInListView(False)> _
    <Appearance("CancelReasonDisabled", Enabled:=False)> _
    Public Property CancelReason() As String
        Get
            Return fCancelReason
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CancelReason", fCancelReason, value)
        End Set
    End Property
#Region "Properties and Colections"
    <DevExpress.Xpo.Aggregated(), Association("IdpDet-CheckListFileData", GetType(CheckListFileData))> _
    Public ReadOnly Property Check() As XPCollection(Of CheckListFileData)
        Get
            Return GetCollection(Of CheckListFileData)("Check")
        End Get
    End Property
    <DevExpress.Xpo.Aggregated(), Association("IdpDet-CheckListDocuments", GetType(ChecklistDocuments))> _
    Public ReadOnly Property CheckList() As XPCollection(Of ChecklistDocuments)
        Get
            Return GetCollection(Of ChecklistDocuments)("CheckList")
        End Get
    End Property
#End Region
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
        If String.Compare(propertyName, "IDPNumber", False) = 0 Or String.Compare(propertyName, "MemberNumber", False) = 0 Or String.Compare(propertyName, "ReceiptNo", False) = 0 Then
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
    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If (IDPIssueDate > Today Or DLExpiryDate <= Today Or IDPExpiryDate <= Today) And CancelReason Is Nothing Then
            Throw New IncorrectDates()
        End If
        If Not MemberNumber Is Nothing Then
            Try
                Dim sqlHandler As New SQLdb
                Dim StrQ As String = String.Format("UPDATE [dbo].[IDP_Allocation] SET [Status] = '{0}' where [IDPNumber] = '{1}'", 2, IDPNumber.IDPNumber)
                sqlHandler.SendNonQuery(StrQ)
            Catch ex As Exception
                Throw
            End Try
        End If
    End Sub
End Class
Public Class RequiredIDPValueMissing : Inherits Exception
    Public Sub New()
        MyBase.New("The amount paid for IDP of this membership must be greater than zero(0)")
    End Sub
End Class