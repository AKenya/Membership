Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Model

Public Class SafariAssessment
    Inherits BaseObject
    Private fBSB, fBDL As Int16
    Private fVCA, fVCB, fVCC, fVCD, fVCE, fVCF, fVCG, fVCH, fVCI, fVCJ, fVCK, fVCL, fVCM, fVCN As Int16
    Private fTDA, fTDB, fTDC, fTDD, fTDE, fTDF, fTDG, fTDH, fTDI, fTDJ As Int16
    Private fBRA, fBRB, fBRC, fBRD, fBRE, fBRF, fBRG, fBRH, fBRI, fBRJ, fBRK, fBRL, fBRM, fBRN, fBRO, fBRP, fBRQ As Int16
    Private fMRA, fMRB, fMRC, fMRD, fMRE, fMRF, fMRG, fMRH As Int16
    Private fULA, fULB As Int16
    Private fAWA, fAWB, fAWC, fAWD As Int16
    Private fHCA, fHCB, fHCC As Int16
    Private fScore, fRemarks As String

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        ' This constructor is used when an object is loaded from a persistent storage.
        ' Do not place any code here or place it only when the IsLoading property is false.
    End Sub
    <Appearance("AdvancedAssessment", AppearanceItemType:="ViewItem", Context:="DetailView", Enabled:=False, TargetItems:="*")> _
    Protected Function DisablePayments() As Boolean
        Return Not (ModifiedOn > DateAdd(DateInterval.Day, -7, Today))
    End Function
    <VisibleInListView(False)> _
    Public Property BSB As Int16
        Get
            Return fBSB
        End Get
        Set(value As Int16)
            SetPropertyValue("BSB", fBSB, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property BDL As Int16
        Get
            Return fBDL
        End Get
        Set(value As Int16)
            SetPropertyValue("BSD", fBDL, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property VCA As Int16
        Get
            Return fVCA
        End Get
        Set(value As Int16)
            SetPropertyValue("VCA", fVCA, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property VCB As Int16
        Get
            Return fVCB
        End Get
        Set(value As Int16)
            SetPropertyValue("VCB", fVCB, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property VCC As Int16
        Get
            Return fVCC
        End Get
        Set(value As Int16)
            SetPropertyValue("VCC", fVCC, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property VCD As Int16
        Get
            Return fVCD
        End Get
        Set(value As Int16)
            SetPropertyValue("VCD", fVCD, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property VCE As Int16
        Get
            Return fVCE
        End Get
        Set(value As Int16)
            SetPropertyValue("VCE", fVCE, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property VCF As Int16
        Get
            Return fVCF
        End Get
        Set(value As Int16)
            SetPropertyValue("VCF", fVCF, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property VCG As Int16
        Get
            Return fVCG
        End Get
        Set(value As Int16)
            SetPropertyValue("VCG", fVCG, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property VCH As Int16
        Get
            Return fVCH
        End Get
        Set(value As Int16)
            SetPropertyValue("VCH", fVCH, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property VCI As Int16
        Get
            Return fVCI
        End Get
        Set(value As Int16)
            SetPropertyValue("VCI", fVCI, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property VCJ As Int16
        Get
            Return fVCJ
        End Get
        Set(value As Int16)
            SetPropertyValue("VCJ", fVCJ, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property VCK As Int16
        Get
            Return fVCK
        End Get
        Set(value As Int16)
            SetPropertyValue("VCK", fVCK, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property VCL As Int16
        Get
            Return fVCL
        End Get
        Set(value As Int16)
            SetPropertyValue("VCL", fVCL, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property VCM As Int16
        Get
            Return fVCM
        End Get
        Set(value As Int16)
            SetPropertyValue("VCM", fVCM, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property VCN As Int16
        Get
            Return fVCN
        End Get
        Set(value As Int16)
            SetPropertyValue("VCN", fVCN, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property TDA As Int16
        Get
            Return fTDA
        End Get
        Set(value As Int16)
            SetPropertyValue("TDA", fTDA, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property TDB As Int16
        Get
            Return fTDB
        End Get
        Set(value As Int16)
            SetPropertyValue("TDB", fTDB, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property TDC As Int16
        Get
            Return fTDC
        End Get
        Set(value As Int16)
            SetPropertyValue("TDC", fTDC, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property TDD As Int16
        Get
            Return fTDD
        End Get
        Set(value As Int16)
            SetPropertyValue("TDD", fTDD, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property TDE As Int16
        Get
            Return fTDE
        End Get
        Set(value As Int16)
            SetPropertyValue("TDE", fTDE, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property TDF As Int16
        Get
            Return fTDF
        End Get
        Set(value As Int16)
            SetPropertyValue("TDF", fTDF, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property TDG As Int16
        Get
            Return fTDG
        End Get
        Set(value As Int16)
            SetPropertyValue("TDG", fTDG, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property TDH As Int16
        Get
            Return fTDH
        End Get
        Set(value As Int16)
            SetPropertyValue("TDH", fTDH, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property TDI As Int16
        Get
            Return fTDI
        End Get
        Set(value As Int16)
            SetPropertyValue("TDI", fTDI, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property TDJ As Int16
        Get
            Return fTDJ
        End Get
        Set(value As Int16)
            SetPropertyValue("TDJ", fTDJ, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    Public Property BRA As Int16
        Get
            Return fBRA
        End Get
        Set(value As Int16)
            SetPropertyValue("BRA", fBRA, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property BRB As Int16
        Get
            Return fBRB
        End Get
        Set(value As Int16)
            SetPropertyValue("BRB", fBRB, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property BRC As Int16
        Get
            Return fBRC
        End Get
        Set(value As Int16)
            SetPropertyValue("BRC", fBRC, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property BRD As Int16
        Get
            Return fBRD
        End Get
        Set(value As Int16)
            SetPropertyValue("BRD", fBRD, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property BRE As Int16
        Get
            Return fBRE
        End Get
        Set(value As Int16)
            SetPropertyValue("BRE", fBRE, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property BRF As Int16
        Get
            Return fBRF
        End Get
        Set(value As Int16)
            SetPropertyValue("BRF", fBRF, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property BRG As Int16
        Get
            Return fBRG
        End Get
        Set(value As Int16)
            SetPropertyValue("BRG", fBRG, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property BRH As Int16
        Get
            Return fBRH
        End Get
        Set(value As Int16)
            SetPropertyValue("BRH", fBRH, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property BRI As Int16
        Get
            Return fBRI
        End Get
        Set(value As Int16)
            SetPropertyValue("BRI", fBRI, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property BRJ As Int16
        Get
            Return fBRJ
        End Get
        Set(value As Int16)
            SetPropertyValue("BRJ", fBRJ, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property BRK As Int16
        Get
            Return fBRK
        End Get
        Set(value As Int16)
            SetPropertyValue("BRK", fBRK, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property BRL As Int16
        Get
            Return fBRL
        End Get
        Set(value As Int16)
            SetPropertyValue("BRL", fBRL, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property BRM As Int16
        Get
            Return fBRM
        End Get
        Set(value As Int16)
            SetPropertyValue("BRM", fBRM, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property BRN As Int16
        Get
            Return fBRN
        End Get
        Set(value As Int16)
            SetPropertyValue("BRN", fBRN, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property BRO As Int16
        Get
            Return fBRO
        End Get
        Set(value As Int16)
            SetPropertyValue("BRO", fBRO, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property BRP As Int16
        Get
            Return fBRP
        End Get
        Set(value As Int16)
            SetPropertyValue("BRP", fBRP, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property BRQ As Int16
        Get
            Return fBRQ
        End Get
        Set(value As Int16)
            SetPropertyValue("BRQ", fBRQ, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    Public Property MRA As Int16
        Get
            Return fMRA
        End Get
        Set(value As Int16)
            SetPropertyValue("MRA", fMRA, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property MRB As Int16
        Get
            Return fMRB
        End Get
        Set(value As Int16)
            SetPropertyValue("MRB", fMRB, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property MRC As Int16
        Get
            Return fMRC
        End Get
        Set(value As Int16)
            SetPropertyValue("MRC", fMRC, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property MRD As Int16
        Get
            Return fMRD
        End Get
        Set(value As Int16)
            SetPropertyValue("MRD", fMRD, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property MRE As Int16
        Get
            Return fMRE
        End Get
        Set(value As Int16)
            SetPropertyValue("MRE", fMRE, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property MRF As Int16
        Get
            Return fMRF
        End Get
        Set(value As Int16)
            SetPropertyValue("MRF", fMRF, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property MRG As Int16
        Get
            Return fMRG
        End Get
        Set(value As Int16)
            SetPropertyValue("MRG", fMRG, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property MRH As Int16
        Get
            Return fMRH
        End Get
        Set(value As Int16)
            SetPropertyValue("MRH", fMRH, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    Public Property ULA As Int16
        Get
            Return fULA
        End Get
        Set(value As Int16)
            SetPropertyValue("ULA", fULA, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property ULB As Int16
        Get
            Return fULB
        End Get
        Set(value As Int16)
            SetPropertyValue("ULB", fULB, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    Public Property AWA As Int16
        Get
            Return fAWA
        End Get
        Set(value As Int16)
            SetPropertyValue("AWA", fAWA, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property AWB As Int16
        Get
            Return fAWB
        End Get
        Set(value As Int16)
            SetPropertyValue("AWB", fAWB, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property AWC As Int16
        Get
            Return fAWC
        End Get
        Set(value As Int16)
            SetPropertyValue("AWC", fAWC, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property AWD As Int16
        Get
            Return fAWD
        End Get
        Set(value As Int16)
            SetPropertyValue("AWD", fAWD, value)
        End Set
    End Property

    <VisibleInListView(False)> _
    Public Property HCA As Int16
        Get
            Return fHCA
        End Get
        Set(value As Int16)
            SetPropertyValue("HCA", fHCA, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property HCB As Int16
        Get
            Return fHCB
        End Get
        Set(value As Int16)
            SetPropertyValue("HCB", fHCB, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property HCC As Int16
        Get
            Return fHCC
        End Get
        Set(value As Int16)
            SetPropertyValue("HCC", fHCC, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <Appearance("DisableScore", Enabled:=False)> _
    Public Property Score As String
        Get
            Return fScore
        End Get
        Set(value As String)
            SetPropertyValue("Score", fScore, value)
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
End Class
