Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.Model

<DefaultClassOptions()> _
Public Class Examiners
    Inherits BaseObject
    Private fExaminerName, fCellNo As String
    Private fBranch As Branch
    Private fDateEnrolled As Date
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <RuleRequiredField()> _
    Public Property ExaminerName() As String
        Get
            Return fExaminerName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ExaminerName", fExaminerName, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property Branch() As Branch
        Get
            Return fBranch
        End Get
        Set(ByVal value As Branch)
            SetPropertyValue("Branch", fBranch, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property DateEnrolled() As Date
        Get
            Return fDateEnrolled
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("DateEnrolled", fDateEnrolled, value)
        End Set
    End Property
    Public Property CellNo() As String
        Get
            Return fCellNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CellNo", fCellNo, value)
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
        If propertyName = "ExaminerName" Or propertyName = "CellNo" Or propertyName = "Branch" Then
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