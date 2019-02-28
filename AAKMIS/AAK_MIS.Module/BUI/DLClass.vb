Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.Model

<DefaultClassOptions()> _
Public Class DLClass
    Inherits BaseObject
    Private fClassName As String
    Private fMembershipAmount, fUseOfAAVehicle, fFeePerLessonNrb, fFeePerLessonOthers As Int16
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        ' This constructor is used when an object is loaded from a persistent storage.
        ' Do not place any code here or place it only when the IsLoading property is false:
        ' if (!IsLoading){
        '   It is now OK to place your initialization code here.
        ' }
        ' or as an alternative, move your initialization code into the AfterConstruction method.
    End Sub
    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property ClassName() As String
        Get
            Return fClassName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ClassName", fClassName, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property MembershipAmount As Int16
        Get
            Return fMembershipAmount
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("MembershipAmount", fMembershipAmount, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property UseOfAAVehicle As Int16
        Get
            Return fUseOfAAVehicle
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("UseOfAAVehicle", fUseOfAAVehicle, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property FeePerLessonNrb() As Int16
        Get
            Return fFeePerLessonNrb
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("FeePerLessonNrb", fFeePerLessonNrb, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property FeePerLessonOthers() As Int16
        Get
            Return fFeePerLessonOthers
        End Get
        Set(ByVal value As Int16)
            SetPropertyValue("FeePerLessonOthers", fFeePerLessonOthers, value)
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
        If propertyName = "ClassName" Then
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