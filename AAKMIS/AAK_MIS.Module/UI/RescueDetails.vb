Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Xpo.DB
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.DC

<XafDefaultProperty("JobNumber")> _
Public Class RescueDetails
    Inherits BaseObject
    Private fJobNo, fCallerName, fCallerCellNo, fNonMemberVehicle, fExactLocationFrom, _
        fExactLocationTo, fNonExistingVehicle, fExplanation, fJobDetails, fPaymentRefCode As String
    Private fMemberNo As MemberRegistration
    Private fNonMemberNo As NonMember
    Private fRescueOffice As Branch
    Private fCallDate, fRescueDate, fBookedTime As Date
    Private fMemberVehicle As Vehicles
    Private fVehicleType As VehicleTypes
    Private fVehicleMake As Make
    Private fVehicleModel As VehicleModel
    Private fRescueDriver, fRescueAssistant, fPatrolMan As RescueTeam
    Private fRescueType As RescueType
    Private fRescueVehicle As RescueVehicles
    Private fAgent As Agent
    Private fRescueRegionFrom, fRescueRegionTo As RescueRegions
    Private fRescueLocationFrom, fRescueLocationTo As RescueLocations
    Private fRoadsideCallFee, fFreeTowCost As Double
    Private fFeeChargeble As Double
    Private fReason As CancelReasons
    Private fServiceType As Servc
    Private ReadOnly period As String
    Private Shared Property con As New Constants
    Public Shared Property uow As UnitOfWork
    Private fFreeTowDistance As Int16
    Private fRescueIncident As RescueIncidences
    Public Shared Sub GetUow()
        If uow Is Nothing Then
            Dim sqlconn = MSSqlConnectionProvider.GetConnectionString(con.server, con.user, con.password, con.DB)
            Dim dl As IDataLayer = XpoDefault.GetDataLayer(sqlconn, AutoCreateOption.DatabaseAndSchema)
            uow = New UnitOfWork(dl)
        End If
    End Sub
    <Appearance("DisableRescueDetails", AppearanceItemType:="ViewItem", Context:="DetailView", Enabled:=False, TargetItems:="*")> _
    Protected Function DisableRescueDetails() As Boolean
        Return (Not (ModifiedOn > DateAdd(DateInterval.Day, -7, Today)))
    End Function
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        If Not IsLoading Then
            period = GenerateForeignFields.ThePeriod
        End If
    End Sub
    <Appearance("DisableJobNumber", Enabled:=False)> _
    <RuleUniqueValue("JobNumberUnique", DefaultContexts.Save)> _
    Public Property JobNumber As String
        Get
            Return fJobNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("JobNumber", fJobNo, value)
        End Set
    End Property
#Region "Properties and Colections"
    <Appearance("HideIndMemberNo", Criteria:="MemberNo=?", visibility:=ViewItemVisibility.Hide)> _
    <Appearance("indmemdisabled", Enabled:=False)> _
    <Association("IndReg-RescueDetails", GetType(MemberRegistration))> _
    Public Property MemberNo() As MemberRegistration
        Get
            Return fMemberNo
        End Get
        Set(ByVal value As MemberRegistration)
            SetPropertyValue("MemberNo", fMemberNo, value)
        End Set
    End Property

    <Appearance("NonMemdisabled", Enabled:=False)> _
    <Appearance("HideNonMemberNo", Criteria:="NonMember=?", visibility:=ViewItemVisibility.Hide)> _
    <Association("NonMem-RescueDetails", GetType(NonMember))> _
    Public Property NonMember() As NonMember
        Get
            Return fNonMemberNo
        End Get
        Set(ByVal value As NonMember)
            SetPropertyValue("NonMember", fNonMemberNo, value)
        End Set
    End Property
#End Region
    <RuleRequiredField()> _
    Public ReadOnly Property Controller As User
        Get
            Return _CreatedBy
        End Get
    End Property
    <RuleRequiredField()> _
    Public Property RescueOffice As Branch
        Get
            Return fRescueOffice
        End Get
        Set(ByVal value As Branch)
            SetPropertyValue("RescueOffice", fRescueOffice, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property CallerName As String
        Get
            Return fCallerName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CallerName", fCallerName, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property CallerCellNo As String
        Get
            Return fCallerCellNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CallerCellNo", fCallerCellNo, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <ModelDefaultAttribute("EditMask", "G"), ModelDefaultAttribute("DisplayFormat", "{0:G}")> _
    Public Property CallDate As Date
        Get
            Return fCallDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("CallDate", fCallDate, value)
        End Set
    End Property
    Private _ContinueJob As Boolean
    <VisibleInListView(False)> _
    <ImmediatePostData()> _
    Public Property ContinueJob() As Boolean
        Get
            Return _ContinueJob
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("ContinueJob", _ContinueJob, value)
        End Set
    End Property
    Private _BookJob As Boolean
    <Appearance("NameBackColor", Criteria:="BookJob='True'", BackColor:="Red")> _
    <ImmediatePostData()> _
    Public Property BookJob() As Boolean
        Get
            Return _BookJob
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("BookJob", _BookJob, value)
        End Set
    End Property
    Private _CancelJob As Boolean
    <VisibleInListView(False)> _
    <ImmediatePostData()> _
    Public Property CancelJob() As Boolean
        Get
            Return _CancelJob
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("CancelJob", _CancelJob, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <ImmediatePostData()> _
    <Appearance("HideCancelReason", Criteria:="CancelJob='False'", visibility:=ViewItemVisibility.Hide)> _
    Public Property Reason As CancelReasons
        Get
            Return fReason
        End Get
        Set(ByVal value As CancelReasons)
            SetPropertyValue("Reason", fReason, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField(TargetCriteria:="BookJob='True'")> _
    <ModelDefaultAttribute("EditMask", "G"), ModelDefaultAttribute("DisplayFormat", "{0:G}")> _
    <Appearance("HideBookedTime", Criteria:="BookJob='False'", visibility:=ViewItemVisibility.Hide)> _
    Public Property BookedTime As DateTime
        Get
            Return fBookedTime
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("BookedTime", fBookedTime, value)
        End Set
    End Property
    <Size(400)> _
    <VisibleInListView(False)> _
    <RuleRequiredField(TargetCriteria:="Reason='Others'")> _
    <Appearance("HideExplanation", Criteria:="Reason='MemberManaged' Or Reason='MemberNotLocated' Or Reason='UnKnown'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <Appearance("HideExplanationJob", Criteria:="CancelJob='False'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    Public Property CancellingExplanation As String
        Get
            Return fExplanation
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CancellingExplanation", fExplanation, value)
        End Set
    End Property
    <Size(400)> _
    <VisibleInListView(False)> _
    <RuleRequiredField(TargetCriteria:="BookJob='True'")> _
    <Appearance("HideRemarks", Criteria:="BookJob='False'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    Public Property JobDetails As String
        Get
            Return fJobDetails
        End Get
        Set(ByVal value As String)
            SetPropertyValue("JobDetails", fJobDetails, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <ImmediatePostData()> _
    <Appearance("HideContinueJob", Criteria:="ContinueJob='False'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    Public Property ServiceType As Servc
        Get
            Return fServiceType
        End Get
        Set(ByVal value As Servc)
            SetPropertyValue("ServiceType", fServiceType, value)
        End Set
    End Property

    <RuleRequiredField("", DefaultContexts.Save, TargetCriteria:="NonExistingVehicle=? AND NonMember=?")> _
    <Appearance("HideVehicleRegNo", Criteria:="MemberNo=? OR NonExistingVehicle!=?", visibility:=ViewItemVisibility.Hide)> _
    <ImmediatePostData()> _
    <DataSourceProperty("MemberNo.Vehicles", DataSourcePropertyIsNullMode.SelectNothing)> _
    Public Property MemberVehicle As Vehicles
        Get
            Return fMemberVehicle
        End Get
        Set(ByVal value As Vehicles)
            SetPropertyValue("MemberVehicle", fMemberVehicle, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField("", DefaultContexts.Save, TargetCriteria:="MemberVehicle=? AND NonMember=?")> _
    <Appearance("HideVehicleNonNo", Criteria:="MemberNo=? OR MemberVehicle!=?", visibility:=ViewItemVisibility.Hide)> _
    <ImmediatePostData()> _
    Public Property NonExistingVehicle As String
        Get
            Return fNonExistingVehicle
        End Get
        Set(ByVal value As String)
            SetPropertyValue("NonExistingVehicle", fNonExistingVehicle, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField("", DefaultContexts.Save, TargetCriteria:="NonMember!=?")> _
    <Appearance("HideVehicleRegNon", Criteria:="NonMember=?", visibility:=ViewItemVisibility.Hide)> _
    Public Property NonMemberVehicle As String
        Get
            Return fNonMemberVehicle
        End Get
        Set(ByVal value As String)
            SetPropertyValue("NonMemberVehicle", fNonMemberVehicle, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    Public Property VehicleType As VehicleTypes
        Get
            Return fVehicleType
        End Get
        Set(ByVal value As VehicleTypes)
            SetPropertyValue("VehicleType", fVehicleType, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <ImmediatePostData()> _
    Public Property VehicleMake As Make
        Get
            Return fVehicleMake
        End Get
        Set(ByVal value As Make)
            SetPropertyValue("VehicleMake", fVehicleMake, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <DataSourceProperty("VehicleMake.VehicleModel", DataSourcePropertyIsNullMode.SelectNothing)> _
    Public Property VehicleModel As VehicleModel
        Get
            Return fVehicleModel
        End Get
        Set(ByVal value As VehicleModel)
            SetPropertyValue("VehicleModel", fVehicleModel, value)
        End Set
    End Property

    Private _isAgent As Boolean
    <VisibleInListView(False)> _
    <Appearance("HideIsAgentContinueJob", Criteria:="ContinueJob='False'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <ImmediatePostData()> _
    Public Property IsAgent() As Boolean
        Get
            Return _isAgent
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("IsAgent", _isAgent, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <Appearance("HidePatrolManContinueJob", Visibility:=ViewItemVisibility.Hide, Criteria:="ContinueJob='False' Or ServiceType!='Rescue'")> _
    Public Property PatrolMan As RescueTeam
        Get
            Return fPatrolMan
        End Get
        Set(ByVal value As RescueTeam)
            SetPropertyValue("PatrolMan", fPatrolMan, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <RuleRequiredField("RqdRescueDriver", DefaultContexts.Save, TargetCriteria:="ServiceType ='TowingAndRecovery' And ContinueJob='True' And IsAgent!='True'")> _
    <Appearance("HideRescueDriverContinueJob", Criteria:="ContinueJob='False'Or ServiceType!='TowingAndRecovery' Or IsAgent='True'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    Public Property RescueDriver As RescueTeam
        Get
            Return fRescueDriver
        End Get
        Set(ByVal value As RescueTeam)
            SetPropertyValue("RescueDriver", fRescueDriver, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <Appearance("HideRescueAssistantContinueJob", Criteria:="ContinueJob='False'Or ServiceType!='TowingAndRecovery' Or IsAgent='True'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    Public Property RescueAssistant As RescueTeam
        Get
            Return fRescueAssistant
        End Get
        Set(ByVal value As RescueTeam)
            SetPropertyValue("RescueAssistant", fRescueAssistant, value)
        End Set
    End Property
    <RuleRequiredField("RqdRescueDate", DefaultContexts.Save, TargetCriteria:="ContinueJob ='True'")> _
    <Appearance("HideRescueDateContinueJob", Criteria:="ContinueJob='False'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <ModelDefaultAttribute("EditMask", "G"), ModelDefaultAttribute("DisplayFormat", "{0:G}")> _
    Public Property RescueDate As Date
        Get
            Return fRescueDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("RescueDate", fRescueDate, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField("RqdRescueType", DefaultContexts.Save, TargetCriteria:="ServiceType ='TowingAndRecovery' And ContinueJob='True'")> _
    <Appearance("HideRescueTypeContinueJob", Criteria:="ContinueJob='False'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    Public Property RescueType As RescueType
        Get
            Return fRescueType
        End Get
        Set(ByVal value As RescueType)
            SetPropertyValue("RescueType", fRescueType, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField("RqdRescueIn", DefaultContexts.Save, TargetCriteria:="ServiceType !='TowingAndRecovery' And ContinueJob='True'")> _
    <Appearance("HideRescueInContinueJob", Criteria:="ContinueJob='False'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    Public Property RescueIncident As RescueIncidences
        Get
            Return fRescueIncident
        End Get
        Set(ByVal value As RescueIncidences)
            SetPropertyValue("RescueIncident", fRescueIncident, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <RuleRequiredField("RqdRescueVehicle", DefaultContexts.Save, TargetCriteria:="ServiceType ='TowingAndRecovery' And ContinueJob='True' And IsAgent!='True'")> _
    <Appearance("HideRescueVehicleContinueJob", Criteria:="ContinueJob='False'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    Public Property RescueVehicle As RescueVehicles
        Get
            Return fRescueVehicle
        End Get
        Set(ByVal value As RescueVehicles)
            SetPropertyValue("RescueVehicle", fRescueVehicle, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <RuleRequiredField("", DefaultContexts.Save, TargetCriteria:="IsAgent='True'")> _
    <Appearance("HideAgentNameContinueJob", Criteria:="ContinueJob='False'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <Appearance("HideAgentName", Criteria:="IsAgent='False'", visibility:=ViewItemVisibility.Hide)> _
    Public Property AgentName As Agent
        Get
            Return fAgent
        End Get
        Set(ByVal value As Agent)
            SetPropertyValue("Agent", fAgent, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <RuleRequiredField("RqdRegionFrom", DefaultContexts.Save, TargetCriteria:="ServiceType ='TowingAndRecovery' And ContinueJob='True'")> _
    <Appearance("HideRescueRegionFContinueJob", Criteria:="ContinueJob='False' Or ServiceType!='TowingAndRecovery'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <ImmediatePostData()> _
    <Association("RescueDetails-RescueRegionsFrom", GetType(RescueRegions))> _
    Public Property RescueRegionFrom As RescueRegions
        Get
            Return fRescueRegionFrom
        End Get
        Set(ByVal value As RescueRegions)
            If SetPropertyValue("RescueRegionFrom", fRescueRegionFrom, value) Then
                RescueLocationFrom = Nothing
            End If
        End Set
    End Property

    <VisibleInListView(False)> _
    <RuleRequiredField("RqdLocationFrom", DefaultContexts.Save, TargetCriteria:="ServiceType ='TowingAndRecovery' And ContinueJob='True'")> _
    <Appearance("HideRescueLocationFromContinueJob", Criteria:="ContinueJob='False' Or ServiceType!='TowingAndRecovery'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <DataSourceProperty("RescueRegionFrom.RescueLocation", DataSourcePropertyIsNullMode.SelectNothing)> _
    Public Property RescueLocationFrom As RescueLocations
        Get
            Return fRescueLocationFrom
        End Get
        Set(ByVal value As RescueLocations)
            SetPropertyValue("RescueLocationFrom", fRescueLocationFrom, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <Appearance("HideExactLocaionFromContinueJob", Criteria:="ContinueJob='False' Or ServiceType!='TowingAndRecovery'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    Public Property ExactLocationFrom As String
        Get
            Return fExactLocationFrom
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ExactLocationFrom", fExactLocationFrom, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <RuleRequiredField("RqdRegionTo", DefaultContexts.Save, TargetCriteria:="ServiceType ='TowingAndRecovery' And ContinueJob='True'")> _
    <ImmediatePostData()> _
    <Appearance("HideRescueRegionToContinueJob", Criteria:="ContinueJob='False' Or ServiceType!='TowingAndRecovery'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <Association("RescueDetails-RescueRegions", GetType(RescueRegions))> _
    Public Property RescueRegionTo As RescueRegions
        Get
            Return fRescueRegionTo
        End Get
        Set(ByVal value As RescueRegions)
            If SetPropertyValue("RescueRegionTo", fRescueRegionTo, value) Then
                RescueLocationTo = Nothing
            End If
        End Set
    End Property

    <VisibleInListView(False)> _
    <RuleRequiredField("RqdLocationTo", DefaultContexts.Save, TargetCriteria:="ServiceType ='TowingAndRecovery' And ContinueJob='True'")> _
    <Appearance("HideRescueLocationToContinueJob", Criteria:="ContinueJob='False' Or ServiceType!='TowingAndRecovery'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <DataSourceProperty("RescueRegionTo.RescueLocation", DataSourcePropertyIsNullMode.SelectNothing)> _
    Public Property RescueLocationTo As RescueLocations
        Get
            Return fRescueLocationTo
        End Get
        Set(ByVal value As RescueLocations)
            SetPropertyValue("RescueLocationTo", fRescueLocationTo, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <Appearance("HideExactLocationToContinueJob", Criteria:="ContinueJob='False' Or ServiceType!='TowingAndRecovery'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    Public Property ExactLocationTo As String
        Get
            Return fExactLocationTo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ExactLocationTo", fExactLocationTo, value)
        End Set
    End Property
    <Appearance("HideTowDistContinueJob", Criteria:="(ContinueJob=='False' Or ServiceType!='TowingAndRecovery')", Enabled:=False, visibility:=ViewItemVisibility.Hide)>
    <Appearance("DisableTowDistContinueJob", Criteria:="(MemberNo.FreeTowBalance<='0')", Enabled:=False)> _
    Public Property FreeTowDistance As Int16
        Get
            Return fFreeTowDistance
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("FreeTowDistance", fFreeTowDistance, value)
        End Set
    End Property

    Private _isFreeTow As Boolean
    <Appearance("IsFreeTowFalseComm", Criteria:="MemberNo.HasFreeTow=='False'", Enabled:=False)> _
    <Appearance("HideIsFreeTowContinueJob", Criteria:="ContinueJob=='False' Or ServiceType!='TowingAndRecovery' Or MemberNo=?", visibility:=ViewItemVisibility.Hide)> _
    <ImmediatePostData()> _
    Public Property IsFreeTow() As Boolean
        Get
            Return _isFreeTow
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("IsFreeTow", _isFreeTow, value)
        End Set
    End Property
    Private _IsFreeWheelAlignment As Boolean
    <VisibleInListView(False)> _
    <Appearance("IsFreeWheelAlignmentFalseComm", Criteria:="MemberNo.HasFreeWheelAlignment=='False'", Enabled:=False)> _
    <Appearance("HideIsFreeWheelAlignment", Criteria:="ContinueJob='False' Or MemberNo=?", visibility:=ViewItemVisibility.Hide)> _
    Public Property IsFreeWheelAlignment() As Boolean
        Get
            Return _IsFreeWheelAlignment
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("IsFreeWheelAlignment", _IsFreeWheelAlignment, value)
        End Set
    End Property
    Private _isFreeAirEvacuation As Boolean
    <VisibleInListView(False)> _
    <Appearance("IsFreeAirEvacuationFalseComm", Criteria:="MemberNo.HasAirEvacuation=='False'", Enabled:=False)> _
    <Appearance("HideIsFreeAirEvacuation", Criteria:="ContinueJob='False' Or MemberNo=?", visibility:=ViewItemVisibility.Hide)> _
    Public Property IsFreeAirEvacuation() As Boolean
        Get
            Return _isFreeAirEvacuation
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("IsFreeAirEvacuation", _isFreeAirEvacuation, value)
        End Set
    End Property
    Private _isIsFreeGroundAmbulance As Boolean
    <VisibleInListView(False)> _
    <Appearance("IsFreeGroundAmbulanceFalseComm", Criteria:="MemberNo.HasFreeGroundAmbulance=='False'", Enabled:=False)> _
    <Appearance("HideIsFreeGroundAmbulance", Criteria:="ContinueJob='False' Or MemberNo=?", visibility:=ViewItemVisibility.Hide)> _
    Public Property IsFreeGroundAmbulance() As Boolean
        Get
            Return _isIsFreeGroundAmbulance
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("IsFreeGroundAmbulance", _isIsFreeGroundAmbulance, value)
        End Set
    End Property
    Private _isIsFreePostAccidentSupport As Boolean
    <VisibleInListView(False)> _
    <Appearance("HideIsFreePostAccident", Criteria:="ContinueJob='False' Or ServiceType='TowingAndRecovery' Or MemberNo=?", visibility:=ViewItemVisibility.Hide)> _
    Public Property IsFreePostAccidentSupport() As Boolean
        Get
            Return _isIsFreePostAccidentSupport
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("IsFreePostAccident", _isIsFreePostAccidentSupport, value)
        End Set
    End Property
    Private _isRoadsideCall As Boolean
    <VisibleInListView(False)> _
    <Appearance("HideIsRoasideCallContinueJob", Criteria:="ContinueJob='False' Or ServiceType!='Rescue'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <ImmediatePostData()> _
    Public Property IsRoadsideCall() As Boolean
        Get
            Return _isRoadsideCall
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("IsRoadsideCall", _isRoadsideCall, value)
        End Set
    End Property

    Private _fRecoveryCharges As Boolean
    <VisibleInListView(False)> _
    <Appearance("HideHasRecoveryChargesContinueJob", Criteria:="ContinueJob='False' Or ServiceType!='TowingAndRecovery'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <ImmediatePostData()> _
    Public Property HasRecoveryCharges() As Boolean
        Get
            Return _fRecoveryCharges
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("HasRecoveryCharges", _fRecoveryCharges, value)
        End Set
    End Property

    Private _fRescueFee As Boolean
    <Appearance("HideHasRescueFeeContinueJob", Criteria:="ContinueJob='False' Or ServiceType!='Rescue'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <VisibleInListView(False)> _
    <ImmediatePostData()> _
    Public Property HasRescueFee() As Boolean
        Get
            Return _fRescueFee
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("HasRescueFee", _fRescueFee, value)
        End Set
    End Property

    Private _fRunningCost As Boolean
    <Appearance("HideHasRunningCostContinueJob", Criteria:="ContinueJob='False'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <VisibleInListView(False)> _
    <ImmediatePostData()> _
    Public Property HasRunningCost() As Boolean
        Get
            Return _fRunningCost
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("HasRunningCost", _fRunningCost, value)
        End Set
    End Property

    Private _fTowingFee As Boolean
    <Appearance("HideHasTowingFeeContinueJob", Criteria:="ContinueJob='False' Or ServiceType!='TowingAndRecovery'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <VisibleInListView(False)> _
    <ImmediatePostData()> _
    Public Property HasTowingFee() As Boolean
        Get
            Return _fTowingFee
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("HasTowingFee", _fTowingFee, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <Appearance("HideFreeTowCost", Criteria:="IsFreeTow='False'", visibility:=ViewItemVisibility.Hide)> _
    <Appearance("HideFreeTowCostContinueJob", Criteria:="ContinueJob='False' Or ServiceType!='TowingAndRecovery'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    Public Property FreeTowCost As Double
        Get
            Return fFreeTowCost
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("FreeTowCost", fFreeTowCost, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <Appearance("HideRoadsideCallFee", Criteria:="IsRoadsideCall='False'", visibility:=ViewItemVisibility.Hide)> _
    <Appearance("HideRoadsideCallContinueJob", Criteria:="ContinueJob='False' Or ServiceType!='Rescue'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <ImmediatePostData()> _
    Public Property RoadsideCallFee As Double
        Get
            Return fRoadsideCallFee
        End Get
        Set(ByVal value As Double)
            If SetPropertyValue("RoadsideCallFee", fRoadsideCallFee, value) Then
                OnChanged("FeeChargeble")
            End If
        End Set
    End Property

    Private Property _RecoveryCharges As Double
    <VisibleInListView(False)> _
    <Appearance("HideRecoveryChargesContinueJob", Criteria:="ContinueJob='False' Or ServiceType!='TowingAndRecovery'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <Appearance("HideRecoveryCharges", Criteria:="HasRecoveryCharges='False'", visibility:=ViewItemVisibility.Hide)> _
    <ImmediatePostData()> _
    Public Property RecoveryCharges As Double
        Get
            Return _RecoveryCharges
        End Get
        Set(ByVal value As Double)
            If SetPropertyValue("RecoveryCharges", _RecoveryCharges, value) Then
                OnChanged("FeeChargeble")
            End If
        End Set
    End Property

    Private Property _RescueFee As Double
    <VisibleInListView(False)> _
    <Appearance("HideRescueFeeContinueJob", Criteria:="ContinueJob='False' Or ServiceType!='Rescue'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <Appearance("HideRescueFee", Criteria:="HasRescueFee='False'", visibility:=ViewItemVisibility.Hide)> _
    <ImmediatePostData()> _
    Public Property RescueFee As Double
        Get
            Return _RescueFee
        End Get
        Set(ByVal value As Double)
            If SetPropertyValue("RescueFee", _RescueFee, value) Then
                OnChanged("FeeChargeble")
            End If
        End Set
    End Property

    Private Property _RunningCost As Double
    <VisibleInListView(False)> _
    <Appearance("HideRunningCostContinueJob", Criteria:="ContinueJob='False'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <Appearance("HideRunningCost", Criteria:="HasRunningCost='False'", visibility:=ViewItemVisibility.Hide)> _
    <ImmediatePostData()> _
    Public Property RunningCost As Double
        Get
            Return _RunningCost
        End Get
        Set(ByVal value As Double)
            If SetPropertyValue("RunningCost", _RunningCost, value) Then
                OnChanged("FeeChargeble")
            End If
        End Set
    End Property

    Private Property _TowingFee As Double
    <VisibleInListView(False)> _
    <Appearance("HideTowingFeeContinueJob", Criteria:="ContinueJob='False' Or ServiceType!='TowingAndRecovery'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <Appearance("HideTowingFee", Criteria:="HasTowingFee='False'", visibility:=ViewItemVisibility.Hide)> _
    <ImmediatePostData()> _
    Public Property TowingFee As Double
        Get
            Return _TowingFee
        End Get
        Set(ByVal value As Double)
            If SetPropertyValue("TowingFee", _TowingFee, value) Then
                OnChanged("FeeChargeble")
            End If
        End Set
    End Property
    Dim tempObject As Object
    <VisibleInListView(False)> _
    <Appearance("DisableTotalFee", Enabled:=False)> _
    <Appearance("HideFeeChargebleContinueJob", Criteria:="ContinueJob='False'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    <PersistentAlias("RoadsideCallFee + RecoveryCharges + RescueFee + RunningCost + TowingFee")> _
    Public Property FeeChargeble As Double
        Get
            tempObject = EvaluateAlias("FeeChargeble")
            If tempObject IsNot Nothing Then
                Return CDbl(tempObject)
                fFeeChargeble = CDbl(tempObject)
            Else
                Return fFeeChargeble
            End If
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("FeeChargeble", fFeeChargeble, value)
        End Set
    End Property
    <Appearance("HideReferenceCode", Criteria:="ContinueJob='False'", visibility:=ViewItemVisibility.Hide)> _
    Public Property PaymentRefCode As String
        Get
            Return fPaymentRefCode
        End Get
        Set(value As String)
            SetPropertyValue("PaymentRefCode", fPaymentRefCode, value)
        End Set
    End Property
    Private _EntryDate As Date
    <VisibleInListView(True), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <Appearance("DisableEntryDate", enabled:=False)> _
    Public Property EntryDate As Date
        Get
            Return _EntryDate
        End Get
        Set(value As Date)
            SetPropertyValue("EntryDate", _EntryDate, value)
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
        If propertyName = "JobNumber" Or propertyName = "MemberNo" Or propertyName = "CallerName" Or propertyName = "CallerCellNo" Then
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
    Private _Checked As Boolean
    <Browsable(False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(value As Boolean)
            SetPropertyValue("Checked", _Checked, value)
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
        _EntryDate = Today.Date
        fReason = CancelReasons.Unknown
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If MemberNo IsNot Nothing Then
            If IsRoadsideCall = True And RoadsideCallFee <= 0 And MemberNo Is Nothing Then
                Throw New RequiredPropertyValueMissing(This, "RoadsideCallFee")
            End If
            If HasRecoveryCharges = True And RecoveryCharges <= 0 Then
                Throw New RequiredPropertyValueMissing(This, "RecoveryCharges")
            End If
            If HasRescueFee = True And RescueFee <= 0 Then
                Throw New RequiredPropertyValueMissing(This, "RescueFee")
            End If
            If HasRunningCost = True And RunningCost <= 0 Then
                Throw New RequiredPropertyValueMissing(This, "RunningCost")
            End If
            If HasTowingFee = True And TowingFee <= 0 Then
                Throw New RequiredPropertyValueMissing(This, "TowingFee")
            End If
            If FreeTowDistance > MemberNo.FreeTowBalance Or FreeTowDistance < 0 Then
                Throw New RequiredPropertyValueExceeding(This, "FreeTowDistance")
            End If
            If IsFreeTow = True And FreeTowCost <= 0 Then
                Throw New RequiredPropertyValueMissing(This, "FreeTowCost")
            End If
        End If
        If JobNumber Is Nothing Then
            Dim JobNumberNew As Integer = GenerateNumbers.GetNewJobNumber()
            If JobNumberNew < 10 Then
                JobNumber = String.Format("JOB/{0}/000{1}", period, JobNumberNew)
            ElseIf JobNumberNew < 100 Then
                JobNumber = String.Format("JOB/{0}/00{1}", period, JobNumberNew)
            ElseIf JobNumberNew < 1000 Then
                JobNumber = String.Format("JOB/{0}/0{1}", period, JobNumberNew)
            Else
                JobNumber = String.Format("JOB/{0}/{1}", period, JobNumberNew)
            End If
        End If
    End Sub

    Public Enum Servc
        Rescue
        TowingAndRecovery
    End Enum
    Public Enum JStatus
        Valid = 0
        Cancelled = 1
    End Enum
    Public Enum CancelReasons
        MemberCancelled = 0
        MemberManaged = 1
        MemberNotLocated = 2
        Others = 3
        Unknown = 4
    End Enum
    Public Enum RescueIncidences
        Mechanical
        Accident
        Recovery
        AccidentAndRecovery
    End Enum
End Class
Public Class RequiredPropertyValueMissing : Inherits Exception
    Public Sub New(ByVal theObject As BaseObject, ByVal theField As String)
        MyBase.New(String.Format("The Field {0} of the {1} object with id {2} must be greater than zero(0)", _
        theField, theObject.GetType().Name, theObject))
    End Sub
End Class
Public Class RequiredPropertyValueExceeding : Inherits Exception
    Public Sub New(ByVal theObject As BaseObject, ByVal theField As String)
        MyBase.New(String.Format("The Field {0} of the {1} object with id {2} must be less than free tow balance and not negative", _
                                 theField, theObject.GetType().Name, theObject))
    End Sub
End Class
