Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp.Model

<XafDefaultProperty("RegNo")> _
Public Class Vehicles
    Inherits BaseObject
    Private fRegNo As String
    Private fVehicleUse As VehicleUse
    Private fMemberNo As MemberRegistration
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
#Region "Properties and Colections"
    <Association("Vehicles-MemReg", GetType(MemberRegistration))> _
    <RuleRequiredField()> _
    Public Property MemberNo() As MemberRegistration
        Get
            Return fMemberNo
        End Get
        Set(ByVal value As MemberRegistration)
            SetPropertyValue("MemberNo", fMemberNo, value)
            If Not MemberNo Is Nothing Then
                MemberName = MemberNo.MemberName
            End If
        End Set
    End Property

#End Region
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

    <RuleUniqueValue("", DefaultContexts.Save)> _
    <RuleRequiredField()> _
    Public Property RegNo() As String
        Get
            Return fRegNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("RegNo", fRegNo, If(value Is Nothing = False, value.Replace(" ", ""), value))
        End Set
    End Property

    Public Property UseOfVehicle As VehicleUse
        Get
            Return fVehicleUse
        End Get
        Set(ByVal value As VehicleUse)
            SetPropertyValue("VehicleUse", fVehicleUse, value)
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
        If String.Compare(propertyName, "RegNo", False) = 0 Or String.Compare(propertyName, "MemberNo", False) = 0 Or String.Compare(propertyName, "CorporateMemberNo", False) = 0 Or String.Compare(propertyName, "VehicleUse", False) = 0 Then
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
    Public Enum VehicleUse
        PrivateUse
        Commercial
    End Enum
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        _CreatedBy = Session.GetObjectByKey(Of User)(SecuritySystem.CurrentUserId)
    End Sub
    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
    End Sub
End Class
