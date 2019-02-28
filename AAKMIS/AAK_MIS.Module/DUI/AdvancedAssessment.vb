Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Model


<DefaultClassOptions()> _
Public Class AdvancedAssessment
    Inherits SafariCheck
    Private fReportNo, fCandidateName, fCellNo, fDLNo, fClientName, fReceiptNo As String
    Private fIssuingOffice As Branch
    Private fExpiryDate, fAssessmentDate, fFinishTime, fStartTime As Date
    Private fClassValid As DLClass
    Private fExaminer As Examiners
    Private fVehicleUsed As AAVehicle
    Private fAmount, fSpeedometerFinal, fSpeedometerInintial As Double
    Private fDuration As TimeSpan
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        ' This constructor is used when an object is loaded from a persistent storage.
        ' Do not place any code here or place it only when the IsLoading property is false.
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
    <Appearance("AssessmentAdvRule", AppearanceItemType:="ViewItem", Context:="DetailView", Enabled:=False, TargetItems:="*")> _
    Protected Function DisableAdvAssessment() As Boolean
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
    Public Property CandidateName() As String
        Get
            Return fCandidateName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CandidateName", fCandidateName, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property CellNo() As String
        Get
            Return fCellNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CellNo", fCellNo, value)
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
    Public Property ClassValid() As DLClass
        Get
            Return fClassValid
        End Get
        Set(ByVal value As DLClass)
            SetPropertyValue("ClassValid", fClassValid, value)
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
    <VisibleInListView(False)> _
    <ModelDefaultAttribute("EditMask", "t"), ModelDefaultAttribute("DisplayFormat", "{0:t}")> _
    Public Property FinishTime() As DateTime
        Get
            Return fFinishTime
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("FinishTime", fFinishTime, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <ImmediatePostData()> _
    <ModelDefaultAttribute("EditMask", "t"), ModelDefaultAttribute("DisplayFormat", "{0:t}")> _
    Public Property StartTime() As DateTime
        Get
            Return fStartTime
        End Get
        Set(ByVal value As DateTime)
            If SetPropertyValue("StartTime", fStartTime, value) Then
                OnChanged("Duration")
            End If
        End Set
    End Property
    Dim tempObject As TimeSpan
    <Appearance("DurationDisabled", Enabled:=False)> _
    <PersistentAlias("FinishTime - StartTime")> _
    Public Property Duration() As TimeSpan
        Get
            tempObject = EvaluateAlias("Duration")
            If tempObject <> Nothing Then
                Return tempObject
                fDuration = tempObject
            Else
                Return fDuration
            End If
        End Get
        Set(ByVal value As TimeSpan)
            SetPropertyValue("Duration", fDuration, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property SpeedometerFinal() As Double
        Get
            Return fSpeedometerFinal
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("SpeedometerFinal", fSpeedometerFinal, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property SpeedometerInintial() As Double
        Get
            Return fSpeedometerInintial
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("SpeedometerInintial", fSpeedometerInintial, value)
        End Set
    End Property
    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If AssessmentDate > Today Or ExpiryDate < Today Then
            Throw New IncorrectDates
        End If
        If ReportNo Is Nothing Then
            Try
                Dim RptNoNew As Integer = GenerateNumbers.GetAdvAssessmentRptNo
                If RptNoNew < 10 Then
                    ReportNo = String.Format("{0}/000{1}", DatePart(DateInterval.Year, ModifiedOn), RptNoNew)
                ElseIf RptNoNew < 100 Then
                    ReportNo = String.Format("{0}/00{1}", DatePart(DateInterval.Year, ModifiedOn), RptNoNew)
                ElseIf RptNoNew < 1000 Then
                    ReportNo = String.Format("{0}/0{1}", DatePart(DateInterval.Year, ModifiedOn), RptNoNew)
                Else
                    ReportNo = String.Format("{0}/{1}", DatePart(DateInterval.Year, ModifiedOn), RptNoNew)
                End If
            Catch ex As Exception
                Throw
            End Try

        End If
    End Sub
End Class