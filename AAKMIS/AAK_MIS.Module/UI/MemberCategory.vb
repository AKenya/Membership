Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp.ConditionalAppearance

<XafDefaultProperty("Category")> _
Public Class MemberCategory
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Private fCategory, fCode As String
    Private fAmount, fNewFee, fAdditionalFee As Double
    Private fSpan, fMinVeh, fMaxVeh, fNoOfDependants, fMaxIDPs, fMaxFullMechVal, fPreInsVal, fWheelAlignment, _
        fGroundAmbulance, fAirEvacuation, fFreeTows, fFreeIDPs, fMaxFreeTows, fCategoryVehicles As Int16
    Private fIsScheme, fIsInvoice, fIsSpouseEligible, fVehicleRqd As Boolean
    Private fMembershipType As MembershipType
    Private fExpiry As SchemeExpiries
    Private fStatus As CategoryStatus
    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property Category() As String
        Get
            Return fCategory
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Category", fCategory, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <RuleUniqueValue("Code", DefaultContexts.Save)> _
    <Index(0)> _
    Public Property Code As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Code", fCode, value)
        End Set
    End Property
    <RuleRequiredField()> _
<Association("MembershipType-To-MemberCategory")> _
    Public Property MembershipType() As MembershipType
        Get
            Return fMembershipType
        End Get
        Set(ByVal value As MembershipType)
            SetPropertyValue("MembershipType", fMembershipType, value)
        End Set
    End Property
    Public Property RenewalFee As Double
        Get
            Return fAmount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("Amount", fAmount, value)
        End Set
    End Property
    Public Property NewFee As Double
        Get
            Return fNewFee
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("NewFee", fNewFee, value)
        End Set
    End Property
    Public Property Span As Int16
        Get
            Return fSpan
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("Span", fSpan, value)
        End Set
    End Property
    Public Property NoOfDependants As Int16
        Get
            Return fNoOfDependants
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("NoOfDependants", fNoOfDependants, value)
        End Set
    End Property
    Public Property MinVehicles As Int16
        Get
            Return fMinVeh
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("MinVehicles", fMinVeh, value)
        End Set
    End Property
    Public Property MaxVehicles As Int16
        Get
            Return fMaxVeh
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("MaxVehicles", fMaxVeh, value)
        End Set
    End Property
    Public Property CategoryVehicles As Int16
        Get
            Return fCategoryVehicles
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("CategoryVehicles", fCategoryVehicles, value)
        End Set
    End Property
    <Appearance("DisableMaxFreeTows", enabled:=False)> _
    Public Property MaxFreeTows As Int16
        Get
            Return fMaxFreeTows
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("MaxFreeTows", fMaxFreeTows, value)
        End Set
    End Property
    Public Property AdditionalFee As Double
        Get
            Return fAdditionalFee
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("AdditionalFee", fAdditionalFee, value)
        End Set
    End Property
    Public Property FreeTows As Int16
        Get
            Return fFreeTows
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("FreeTows", fFreeTows, value)
        End Set
    End Property

    Public Property MaxIDPs As Int16
        Get
            Return fMaxIDPs
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("MaxIDPs", fMaxIDPs, value)
        End Set
    End Property

    Public Property MaxFullMechVal As Int16
        Get
            Return fMaxFullMechVal
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("MaxFullMechVal", fMaxFullMechVal, value)
        End Set
    End Property
    Public Property PreInsVal As Int16
        Get
            Return fPreInsVal
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("PreInsVal", fPreInsVal, value)
        End Set
    End Property
    Public Property WheelAlignment As Int16
        Get
            Return fWheelAlignment
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("WheelAlignment", fWheelAlignment, value)
        End Set
    End Property
    Public Property GroundAmbulance As Int16
        Get
            Return fGroundAmbulance
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("GroundAmbulance", fGroundAmbulance, value)
        End Set
    End Property
    Public Property AirEvacuation As Int16
        Get
            Return fAirEvacuation
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("AirEvacuation", fAirEvacuation, value)
        End Set
    End Property

    Public Property FreeIDPs As Int16
        Get
            Return fFreeIDPs
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("FreeIDP", fFreeIDPs, value)
        End Set
    End Property
    Public Property Expiry As SchemeExpiries
        Get
            Return fExpiry
        End Get
        Set(ByVal value As SchemeExpiries)
            SetPropertyValue("Expiry", fExpiry, value)
        End Set
    End Property
    Public Property IsScheme As Boolean
        Get
            Return fIsScheme
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("IsScheme", fIsScheme, value)
        End Set
    End Property

    Public Property IsInvoicable As Boolean
        Get
            Return fIsInvoice
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("IsInvoice", fIsInvoice, value)
        End Set
    End Property
    Public Property SpouseEligible As Boolean
        Get
            Return fIsSpouseEligible
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("SpouseEligible", fIsSpouseEligible, value)
        End Set
    End Property

    Public Property VehicleRqd As Boolean
        Get
            Return fVehicleRqd
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("VehicleRqd", fVehicleRqd, value)
        End Set
    End Property
    Public Property Status As CategoryStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As CategoryStatus)
            SetPropertyValue("Status", fStatus, value)
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
        If propertyName = "Category" Or propertyName = "Code" Or propertyName = "Amount" Then
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
    Public Enum SchemeExpiries
        Fixed
        Dynamic
    End Enum
    Public Enum CategoryStatus
        Active
        Inactive
    End Enum
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        _CreatedBy = Session.GetObjectByKey(Of User)(SecuritySystem.CurrentUserId)
        ' Place here your initialization code.
    End Sub
End Class
