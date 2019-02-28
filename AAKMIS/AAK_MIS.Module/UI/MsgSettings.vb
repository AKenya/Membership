Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp

<DefaultClassOptions()> _
Public Class MsgSettings
    Inherits BaseObject
    Private fMessage As String
    Private fTarget As GroupTargets
    Private fScheduledTime As DateTime
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        _CreatedBy = Session.GetObjectByKey(Of User)(SecuritySystem.CurrentUserId)
    End Sub
    Public Property Target As GroupTargets
        Get
            Return fTarget
        End Get
        Set(ByVal value As GroupTargets)
            SetPropertyValue("Target", fTarget, value)
        End Set
    End Property
    Public Property ScheduledTime As DateTime
        Get
            Return fScheduledTime
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("ScheduledTime", fScheduledTime, value)
        End Set
    End Property
    <Size(400)> _
    <VisibleInListView(False)> _
    Public Property Message As String
        Get
            Return fMessage
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Message", fMessage, value)
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
        If propertyName = "Message" Or propertyName = "Target" Then
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

    Public Enum GroupTargets
        ValidMembers
        ArrearsMembers
        LapsesMembers
        NewAndRenewals
    End Enum
End Class
