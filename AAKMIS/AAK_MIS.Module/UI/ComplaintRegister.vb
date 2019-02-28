Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Model

<DefaultClassOptions()> _
Public Class ComplaintRegister
    Inherits BaseObject
    Private fType As feedbackType
    Private fMemberNo As String
    Private fFeedbackNo, fCellNo, fComments, fCause, fActionTaken, period As String
    Private fStatus As FeedStatus
    Private fActionTime As DateTime
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <Appearance("DisableFeedbackNo", Enabled:=False)> _
    Public Property FeedbackNo() As String
        Get
            Return fFeedbackNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("FeedbackNo", fFeedbackNo, value)
        End Set
    End Property
    <Appearance("DisableMemberNo", Enabled:=False)> _
    Public Property MemberNo() As String
        Get
            Return fMemberNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("MemberNo", fMemberNo, value)
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
    Public Property Type() As feedbackType
        Get
            Return fType
        End Get
        Set(ByVal value As feedbackType)
            SetPropertyValue("Type", fType, value)
        End Set
    End Property
    <Appearance("DisableStatus", Enabled:=False)> _
    Public Property Status() As FeedStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As FeedStatus)
            SetPropertyValue("Status", fStatus, value)
        End Set
    End Property
    <Appearance("DisableActionTime", Enabled:=False)> _
    Public Property ActionTime() As DateTime
        Get
            Return fActionTime
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("ActionTime", fActionTime, value)
        End Set
    End Property
    <Size(400)> _
    <RuleRequiredField()> _
    Public Property Comments() As String
        Get
            Return fComments
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Comments", fComments, value)
        End Set
    End Property
    <Size(400)> _
    <RuleRequiredField("CauseRequired", DefaultContexts.Save, TargetCriteria:="Type.Name='COMPLAINT'")> _
    Public Property Cause() As String
        Get
            Return fCause
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Cause", fCause, value)
        End Set
    End Property
    <Size(1000)> _
    Public Property ActionTaken() As String
        Get
            Return fActionTaken
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ActionTaken", fActionTaken, value)
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
        If propertyName = "Comments" Then
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
    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        Try
            If FeedbackNo Is Nothing And fComments Is Nothing = False Then
                Dim FeedbackNoNew As Integer = GenerateNumbers.GenFeedbackNo
                If FeedbackNoNew < 10 Then
                    FeedbackNo = String.Format("{0}/000{1}", period, FeedbackNoNew)
                ElseIf FeedbackNoNew < 100 Then
                    FeedbackNo = String.Format("{0}/00{1}", period, FeedbackNoNew)
                ElseIf FeedbackNoNew < 1000 Then
                    FeedbackNo = String.Format("{0}/0{1}", period, FeedbackNoNew)
                Else
                    FeedbackNo = String.Format("{0}/{1}", period, FeedbackNoNew)
                End If
                Save()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        _CreatedBy = Session.GetObjectByKey(Of User)(SecuritySystem.CurrentUserId)
        period = GenerateForeignFields.ThePeriod
        ' Place here your initialization code.
    End Sub
    Public Enum FeedStatus
        Pending
        Escalated
        Closed
    End Enum
End Class
Public Class feedbackType
    Inherits BaseObject
    Private fName As String

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <RuleRequiredField()> _
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Name", fName, value)
        End Set
    End Property
End Class