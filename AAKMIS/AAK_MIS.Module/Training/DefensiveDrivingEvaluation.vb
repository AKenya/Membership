Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.ConditionalAppearance

<ImageName("BO_List")> _
<DefaultClassOptions()> _
Public Class DefensiveDrivingEvaluation
    Inherits BaseObject
    Private fDriverName As DriverCertification
    Private fVehicleUsed As AAVehicle
    Private fEvaluationDate As Date
    Private fVehicleType As AAVehicle.DriveTypess
    Private fBA, fBB, fVA, fVB, fGA, fGB, fGC, fGD, fSA, fDA, fDB, fDC, fDD, fDE, fDF, fDG As Performance
    Private fPointsAttained, fRemarks As String
    Private VarTotal As Double
    Private VarOutof As Double
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        ' This constructor is used when an object is loaded from a persistent storage.
        ' Do not place any code here or place it only when the IsLoading property is false.
    End Sub
    <Appearance("DisableCertNo", Enabled:=False)> _
    <RuleRequiredField()> _
    <Association("DDE-DriverCert", GetType(DriverCertification))> _
    Public Property DriverName() As DriverCertification
        Get
            Return fDriverName
        End Get
        Set(ByVal value As DriverCertification)
            SetPropertyValue("DriverName", fDriverName, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property VehicleUsed() As AAVehicle
        Get
            Return fVehicleUsed
        End Get
        Set(ByVal value As AAVehicle)
            SetPropertyValue("VehicleUsed", fVehicleUsed, value)
        End Set
    End Property

    <RuleRequiredField()> _
    Public Property EvaluationDate() As Date
        Get
            Return fEvaluationDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("Evaluation", fEvaluationDate, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property VehicleType() As AAVehicle.DriveTypess
        Get
            Return fVehicleType
        End Get
        Set(ByVal value As AAVehicle.DriveTypess)
            SetPropertyValue("VehicleType", fVehicleType, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property BA() As Performance
        Get
            Return fBA
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("BA", fBA, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property BB() As Performance
        Get
            Return fBB
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("BB", fBB, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property VA() As Performance
        Get
            Return fVA
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("VA", fVA, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property VB() As Performance
        Get
            Return fVB
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("VB", fVB, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property GA() As Performance
        Get
            Return fGA
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("GA", fGA, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property GB() As Performance
        Get
            Return fGB
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("GB", fGB, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property GC() As Performance
        Get
            Return fGC
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("GC", fGC, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property GD() As Performance
        Get
            Return fGD
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("GD", fGD, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property SA() As Performance
        Get
            Return fSA
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("SA", fSA, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property DA() As Performance
        Get
            Return fDA
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("DA", fDA, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property DB() As Performance
        Get
            Return fDB
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("DB", fDB, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property DC() As Performance
        Get
            Return fDC
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("DC", fDC, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property DD() As Performance
        Get
            Return fDD
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("DD", fDD, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property DE() As Performance
        Get
            Return fDE
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("DE", fDE, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property DF() As Performance
        Get
            Return fDF
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("DF", fDF, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property DG() As Performance
        Get
            Return fDG
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("DG", fDG, value)
        End Set
    End Property
    <Appearance("DisableMark", Enabled:=False)> _
    Public Property PointsAttained As String
        Get
            Return fPointsAttained
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PointsAttained", fPointsAttained, value)
        End Set
    End Property

    <Size(2048)> _
    Public Property Remarks As String
        Get
            Return fRemarks
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Remarks", fRemarks, value)
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
        If String.Compare(propertyName, "CertificateNo", False) = 0 Or String.Compare(propertyName, "VehicleUsed", False) = 0 Then
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
        Try
            VarOutof = 16
            VarTotal = BA.Points + BB.Points + VA.Points + VB.Points + GA.Points + GB.Points + GC.Points + GD.Points + SA.Points + DA.Points + DB.Points + DC.Points + DD.Points + DE.Points + DF.Points + DG.Points
            PointsAttained = VarTotal.ToString
            PointsAttained = (String.Format("{0}/{1}", VarTotal, VarOutof))
        Catch ex As Exception
            Throw
        End Try
        MyBase.OnSaving()
    End Sub
End Class
