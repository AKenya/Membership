Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.Model

<DefaultClassOptions()> _
Public Class Carnet
    Inherits BaseObject
    Private fCPDNo, fIssuedBy, fUnderNo, fRegisteredIn, fIssuedAt, fYearOfMan, _
        fNetWeight, fVehicleValue, fChasisNo, fEngineNo, fNoOfCylinders, fHorsePower, _
       fType, fColor, fUpholstery, fNoOfSeats, fRadio, fSpareTyres, fHolder, fExemptions, fOtherParticulars As String
    Private fMake As Make
    Private fValidUntil, fIssueDate As Date
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Property CPDNo As String
        Get
            Return fCPDNo
        End Get
        Set(value As String)
            SetPropertyValue("CPDNo", fCPDNo, value)
        End Set
    End Property
    Public Property IssuedBy As String
        Get
            Return fIssuedBy
        End Get
        Set(value As String)
            SetPropertyValue("IssuedBy", fIssuedBy, value)
        End Set
    End Property
    Public Property ValidUntil As Date
        Get
            Return fValidUntil
        End Get
        Set(value As Date)
            SetPropertyValue("ValidUntil", fValidUntil, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property RegisteredIn As String
        Get
            Return fRegisteredIn
        End Get
        Set(value As String)
            SetPropertyValue("RegisteredIn", fRegisteredIn, value)
        End Set
    End Property
    Public Property UnderNo As String
        Get
            Return fUnderNo
        End Get
        Set(value As String)
            SetPropertyValue("UnderNo", fUnderNo, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property IssuedAt As String
        Get
            Return fIssuedAt
        End Get
        Set(value As String)
            SetPropertyValue("IssuedAt", fIssuedAt, value)
        End Set
    End Property
    Public Property IssueDate As Date
        Get
            Return fIssueDate
        End Get
        Set(value As Date)
            SetPropertyValue("IssueDate", fIssueDate, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property YearOfMan As String
        Get
            Return fYearOfMan
        End Get
        Set(value As String)
            SetPropertyValue("YearOfMan", fYearOfMan, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property NetWeight As String
        Get
            Return fNetWeight
        End Get
        Set(value As String)
            SetPropertyValue("NetWeight", fNetWeight, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property VehicleValue As String
        Get
            Return fVehicleValue
        End Get
        Set(value As String)
            SetPropertyValue("VehicleValue", fVehicleValue, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property ChasisNo As String
        Get
            Return fChasisNo
        End Get
        Set(value As String)
            SetPropertyValue("ChasisNo", fChasisNo, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property Make As Make
        Get
            Return fMake
        End Get
        Set(value As Make)
            SetPropertyValue("Make", fMake, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property EngineNo As String
        Get
            Return fEngineNo
        End Get
        Set(value As String)
            SetPropertyValue("EngineNo", fEngineNo, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property NoOfCylinders As String
        Get
            Return fNoOfCylinders
        End Get
        Set(value As String)
            SetPropertyValue("NoOfCylinders", fNoOfCylinders, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property HorsePower As String
        Get
            Return fHorsePower
        End Get
        Set(value As String)
            SetPropertyValue("HorsePower", fHorsePower, value)
        End Set
    End Property

    Public Property Type As String
        Get
            Return fType
        End Get
        Set(value As String)
            SetPropertyValue("Type", fType, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property Color As String
        Get
            Return fColor
        End Get
        Set(value As String)
            SetPropertyValue("Color", fColor, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property Upholstery As String
        Get
            Return fUpholstery
        End Get
        Set(value As String)
            SetPropertyValue("Upholstery", fUpholstery, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property NoOfSeats As String
        Get
            Return fNoOfSeats
        End Get
        Set(value As String)
            SetPropertyValue("NoOfSeats", fNoOfSeats, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property Radio As String
        Get
            Return fRadio
        End Get
        Set(value As String)
            SetPropertyValue("Radio", fRadio, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property SpareTyres As String
        Get
            Return fSpareTyres
        End Get
        Set(value As String)
            SetPropertyValue("SpareTyres", fSpareTyres, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <Size(1000)> _
    Public Property Holder As String
        Get
            Return fHolder
        End Get
        Set(value As String)
            SetPropertyValue("Holder", fHolder, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <Size(1000)> _
    Public Property Exemptions As String
        Get
            Return fExemptions
        End Get
        Set(value As String)
            SetPropertyValue("Exemptions", fExemptions, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <Size(1000)> _
    Public Property OtherParticulars As String
        Get
            Return fOtherParticulars
        End Get
        Set(value As String)
            SetPropertyValue("OtherParticulars", fOtherParticulars, value)
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
        If propertyName = "StemNumber" Or propertyName = "Name" Or propertyName = "JoiningDate" Then
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
End Class