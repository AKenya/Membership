Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.DC

<XafDefaultProperty("PaymentMode")> _
Public Class PaymentMode
    Inherits BaseObject
    Private fPMode As String
    Public Sub New(ByVal session As Session)
        MyBase.New(session)

    End Sub
    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property PaymentMode() As String
        Get
            Return fPMode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PaymentMode", fPMode, value)
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
        If propertyName = "PaymentMode" Then
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
