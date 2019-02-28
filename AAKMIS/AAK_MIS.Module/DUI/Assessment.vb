Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.DC

<ImageName("BO_List")> _
<XafDefaultProperty("ReportNo")> _
Public Class Assessment
    Inherits AssessmentDetails
    Private fReportNo, fDriverName, fDLNo, fClientName, fReceiptNo As String
    Private fIssuingOffice As Branch
    Private fExpiryDate, fAssessmentDate As Date
    Private fDlClass As DLClass
    Private fExaminer As Examiners
    Private fVehicleUsed As AAVehicle
    Private fAmount As Double

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <Appearance("AssessmentRule", AppearanceItemType:="ViewItem", Context:="DetailView", Enabled:=False, TargetItems:="*")> _
    Protected Function DisableAssessment() As Boolean
        Return Not (ModifiedOn > DateAdd(DateInterval.Day, -7, Today))
    End Function
    <Appearance("RptNoDisabled", Enabled:=False)> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property ReportNo() As String
        Get
            Return fReportNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ReportNo", fReportNo, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property DriverName() As String
        Get
            Return fDriverName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("DriverName", fDriverName, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property IssuingOffice() As Branch
        Get
            Return fIssuingOffice
        End Get
        Set(ByVal value As Branch)
            SetPropertyValue("IssuingOffice", fIssuingOffice, value)
        End Set
    End Property

    <RuleRequiredField()> _
    Public Property DLNo() As String
        Get
            Return fDLNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("DLNo", fDLNo, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property ExpiryDate() As Date
        Get
            Return fExpiryDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("ExpiryDate", fExpiryDate, value)
        End Set
    End Property

    <RuleRequiredField()> _
    Public Property DLClass() As DLClass
        Get
            Return fDlClass
        End Get
        Set(ByVal value As DLClass)
            SetPropertyValue("DlClass", fDlClass, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property ClientName() As String
        Get
            Return fClientName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ClientName", fClientName, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property Examiner() As Examiners
        Get
            Return fExaminer
        End Get
        Set(ByVal value As Examiners)
            SetPropertyValue("Examiner", fExaminer, value)
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
    Public Property AssessmentDate() As Date
        Get
            Return fAssessmentDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("AssessmentDate", fAssessmentDate, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property ReceiptNo() As String
        Get
            Return fReceiptNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ReceiptNo", fReceiptNo, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property Amount() As Double
        Get
            Return fAmount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("Amount", fAmount, value)
        End Set
    End Property

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place here your initialization code.
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If AssessmentDate > Today Or ExpiryDate < Today Then
            Throw New IncorrectDates
        End If
        If ReportNo Is Nothing Then
            Try
                Dim RptNoNew As Integer = GenerateNumbers.GetAssessmentRptNo
                ReportNo = RptNoNew
            Catch ex As Exception
                Throw
            End Try

        End If
    End Sub
End Class
Public Class AssessmentDetails
    Inherits BaseObject
    Private fRemarks As String
    Private VarTotal As Double
    Private VarOutof As Double
    Private fPointsAttained As String
    Private fBA, fBB, fBC, fVA, fVB, fVC, fVD, fVE, fVF, fTA, fTB, fTC, fTD, fTE, fTF, fTG, fTH, fTI, _
   fSA, fPA, fHA, fHB, fHC, fAA, fAB, fAC, fAD, FAE, fAF, fAG As Performance

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <RuleRequiredField()> _
    Public Property BA() As Performance
        Get
            Return fBA
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("BA", fBA, value)
        End Set
    End Property

    <RuleRequiredField()> _
    Public Property BB() As Performance
        Get
            Return fBB
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("BB", fBB, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property BC() As Performance
        Get
            Return fBC
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("BC", fBC, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property VA() As Performance
        Get
            Return fVA
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("VA", fVA, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property VB() As Performance
        Get
            Return fVB
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("VB", fVB, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property VC() As Performance
        Get
            Return fVC
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("VC", fVC, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property VD() As Performance
        Get
            Return fVD
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("CD", fVD, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property VE() As Performance
        Get
            Return fVE
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("VE", fVE, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property VF() As Performance
        Get
            Return fVF
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("VF", fVF, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property TA() As Performance
        Get
            Return fTA
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("TA", fTA, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property TB() As Performance
        Get
            Return fTB
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("TB", fTB, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property TC() As Performance
        Get
            Return fTC
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("TC", fTC, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property TD() As Performance
        Get
            Return fTD
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("TD", fTD, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property TE() As Performance
        Get
            Return fTE
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("TE", fTE, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property TF() As Performance
        Get
            Return fTF
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("TF", fTF, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property TG() As Performance
        Get
            Return fTG
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("TG", fTG, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property TH() As Performance
        Get
            Return fTH
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("TH", fTH, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property TI() As Performance
        Get
            Return fTI
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("TI", fTI, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property SA() As Performance
        Get
            Return fSA
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("SA", fSA, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property PA() As Performance
        Get
            Return fPA
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("PA", fPA, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property HA() As Performance
        Get
            Return fHA
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("HA", fHA, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property HB() As Performance
        Get
            Return fHB
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("HB", fHB, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property HC() As Performance
        Get
            Return fHC
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("HC", fHC, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property AA() As Performance
        Get
            Return fAA
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("AA", fAA, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property AB() As Performance
        Get
            Return fAB
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("AB", fAB, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property AC() As Performance
        Get
            Return fAC
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("AC", fAC, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property AD() As Performance
        Get
            Return fAD
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("AD", fAD, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property AE() As Performance
        Get
            Return FAE
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("AE", FAE, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property AF() As Performance
        Get
            Return fAF
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("AF", fAF, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property AG() As Performance
        Get
            Return fAG
        End Get
        Set(ByVal value As Performance)
            SetPropertyValue("AG", fAG, value)
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
        If propertyName = "Examiner" Or propertyName = "CellNo" Or propertyName = "DriverName" Then
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

    Protected Overrides Sub OnSaving()
        Try
            If BB.Name = "N/A" And BC.Name = "N/A" And TE.Name = "N/A" Then
                VarOutof = 27
                VarTotal = BA.Points + VA.Points + VB.Points + VC.Points + VD.Points + VE.Points + VF.Points + TA.Points + TB.Points + TC.Points + TD.Points + TF.Points + TG.Points + TH.Points + TI.Points + SA.Points + PA.Points + HA.Points + HB.Points + HC.Points + AA.Points + AB.Points + AC.Points + AD.Points + AE.Points + AF.Points + AG.Points
                PointsAttained = VarTotal.ToString
            ElseIf TE.Name = "N/A" Then
                VarOutof = 29
                VarTotal = BA.Points + BB.Points + BC.Points + VA.Points + VB.Points + VC.Points + VD.Points + VE.Points + VF.Points + TA.Points + TB.Points + TC.Points + TD.Points + TF.Points + TG.Points + TH.Points + TI.Points + SA.Points + PA.Points + HA.Points + HB.Points + HC.Points + AA.Points + AB.Points + AC.Points + AD.Points + AE.Points + AF.Points + AG.Points
                PointsAttained = VarTotal.ToString
            Else
                VarOutof = 30
                VarTotal = BA.Points + BB.Points + BC.Points + VA.Points + VB.Points + VC.Points + VD.Points + VE.Points + VF.Points + TA.Points + TB.Points + TC.Points + TD.Points + TE.Points + TF.Points + TG.Points + TH.Points + TI.Points + SA.Points + PA.Points + HA.Points + HB.Points + HC.Points + AA.Points + AB.Points + AC.Points + AD.Points + AE.Points + AF.Points + AG.Points
                PointsAttained = VarTotal.ToString
            End If
            PointsAttained = (String.Format("{0}/{1}", VarTotal, VarOutof))
        Catch ex As Exception
            Throw
        End Try
        MyBase.OnSaving()
    End Sub

End Class