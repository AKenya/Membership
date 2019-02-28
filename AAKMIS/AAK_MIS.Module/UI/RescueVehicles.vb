Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports AAK_MIS.Module.RescueDetails
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.DC

<XafDefaultProperty("RegNo")> _
Public Class RescueVehicles
    Inherits BaseObject
    Private fRegNo, fDescription As String
    Private fVehicleType As VehicleTypes
    Private fVehicleUse As Servc

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        ' This constructor is used when an object is loaded from a persistent storage.
        ' Do not place any code here or place it only when the IsLoading property is false:
        ' if (!IsLoading){
        '   It is now OK to place your initialization code here.
        ' }
        ' or as an alternative, move your initialization code into the AfterConstruction method.
    End Sub
    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property RegNo As String
        Get
            Return fRegNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("RegNo", fRegNo, value)
        End Set
    End Property

    <RuleRequiredField()> _
    Public Property VehicleType As VehicleTypes
        Get
            Return fVehicleType
        End Get
        Set(ByVal value As VehicleTypes)
            SetPropertyValue("VehicleType", fVehicleType, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property VehicleUse As Servc
        Get
            Return fVehicleUse
        End Get
        Set(ByVal value As Servc)
            SetPropertyValue("VehicleUse", fVehicleUse, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
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
        If propertyName = "RegNo" Or propertyName = "VehicleType" Then
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
