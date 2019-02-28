Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.Xpo.Metadata

<ImageName("BO_List")> _
<DefaultClassOptions()> _
Public Class DriverCertification
    Inherits BaseObject
    Private fCertificateNo, fCourse, fTraineeName, fOrganization, fCellNo, fDLNo, fReceiptNo, fPeriod, fValidUpto, fTopicsCovered, fDeletionReason As String
    Private fLeadTrainer, fTrainer1, fTrainer2, fTrainer3 As Trainers
    Private fDlClass As DLClass
    Private fDrivingExperience As Int16
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <Appearance("TrainingCertificateRule", AppearanceItemType:="ViewItem", Context:="DetailView", Enabled:=False, TargetItems:="*")> _
    Protected Function DisableAssessment() As Boolean
        Return Not (ModifiedOn > DateAdd(DateInterval.Day, -7, Today))
    End Function

    <Appearance("CertNoDisabled", Criteria:="CertificateNo!=?", Enabled:=False)> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property CertificateNo() As String
        Get
            Return fCertificateNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CertificateNo", fCertificateNo, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property Course() As String
        Get
            Return fCourse
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Course", fCourse, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property TraineeName() As String
        Get
            Return fTraineeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("TraineeName", fTraineeName, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property Organization() As String
        Get
            Return fOrganization
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Organization", fOrganization, value)
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
    Public Property DLNo() As String
        Get
            Return fDLNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("DLNo", fDLNo, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property DLClass() As DLClass
        Get
            Return fDlClass
        End Get
        Set(ByVal value As DLClass)
            SetPropertyValue("DlClass", fDlClass, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property DrivingExperience() As Int16
        Get
            Return fDrivingExperience
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("DrivingExperience", fDrivingExperience, value)
        End Set
    End Property


    <RuleRequiredField()> _
    Public Property LeadTrainer() As Trainers
        Get
            Return fLeadTrainer
        End Get
        Set(ByVal value As Trainers)
            SetPropertyValue("LeadTrainer", fLeadTrainer, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property Trainer1() As Trainers
        Get
            Return fTrainer1
        End Get
        Set(ByVal value As Trainers)
            SetPropertyValue("Trainer1", fTrainer1, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property Trainer2() As Trainers
        Get
            Return fTrainer2
        End Get
        Set(ByVal value As Trainers)
            SetPropertyValue("Trainer2", fTrainer2, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    Public Property Trainer3() As Trainers
        Get
            Return fTrainer3
        End Get
        Set(ByVal value As Trainers)
            SetPropertyValue("Trainer3", fTrainer3, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <RuleRequiredField()> _
    Public Property Period() As String
        Get
            Return fPeriod
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Period", fPeriod, value)
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
    Public Property ValidUpto() As String
        Get
            Return fValidUpto
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ValidUpto", fValidUpto, value)
        End Set
    End Property
    <Size(2048)> _
    <RuleRequiredField()> _
    Public Property TopicsCovered As String
        Get
            Return fTopicsCovered
        End Get
        Set(ByVal value As String)
            SetPropertyValue("TopicsCovered", fTopicsCovered, value)
        End Set
    End Property
    <Size(2048)> _
    <Appearance("CertDeletionReasonDisabled", Enabled:=False)> _
    Public Property DeletionReason As String
        Get
            Return fDeletionReason
        End Get
        Set(ByVal value As String)
            SetPropertyValue("DeletionReason", fDeletionReason, value)
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
        If String.Compare(propertyName, "CertificateNo", False) = 0 Or String.Compare(propertyName, "TraineeName", False) = 0 Then
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
    <Association("DDE-DriverCert", GetType(DefensiveDrivingEvaluation))> _
    Public ReadOnly Property DrivingEvaluation() As XPCollection
        Get
            Return GetCollection("DrivingEvaluation")
        End Get
    End Property
    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
    End Sub
End Class
