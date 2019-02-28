Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.ConditionalAppearance


<DefaultListViewOptions(MasterDetailMode.ListViewAndDetailView, True, NewItemRowPosition.Top)> _
<ImageName("BO_Contract")> _
<DefaultClassOptions()> _
Public Class Voucher
    Inherits BaseObject
    Private fVoucherNo, fComments As String
    Private ReadOnly period As String
    Private fMemberNo As MemberRegistration
    Private fFullMechanical, fPreInsValuation, fWheelAlignment As Boolean
    Private fIssuingBranch As Branch
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        If Not IsLoading Then
            period = GenerateForeignFields.ThePeriod
        End If
    End Sub
    <Appearance("DisableVouchers", AppearanceItemType:="ViewItem", Context:="DetailView", Enabled:=False, TargetItems:="*")> _
    Protected Function DisableVouchers() As Boolean
        Return (Not (ModifiedOn > DateAdd(DateInterval.Day, -7, Today)))
    End Function

    <Appearance("DisableVoucherNo", Enabled:=False)> _
    Public Property VoucherNo() As String
        Get
            Return fVoucherNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("VoucherNo", fVoucherNo, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <Appearance("Vouchermemdisabled", Enabled:=False)> _
    <Association("IndiReg-Voucher", GetType(MemberRegistration))> _
    Public Property MemberNo() As MemberRegistration
        Get
            Return fMemberNo
        End Get
        Set(ByVal value As MemberRegistration)
            SetPropertyValue("MemberNo", fMemberNo, value)
        End Set
    End Property

    <Appearance("FullMechanical", Criteria:="MemberNo.HasFullMechanical=='False'", Enabled:=False)> _
    Public Property FullMechanical() As Boolean
        Get
            Return fFullMechanical
        End Get
        Set(value As Boolean)
            SetPropertyValue("FullMechanical", fFullMechanical, value)
        End Set
    End Property

    <Appearance("PreInsValuation", Criteria:="MemberNo.HasPreInsValuation=='False'", Enabled:=False)> _
    Public Property PreInsValuation() As Boolean
        Get
            Return fPreInsValuation
        End Get
        Set(value As Boolean)
            SetPropertyValue("PreInsValuation", fPreInsValuation, value)
        End Set
    End Property

    <Appearance("WheelAlignment", Criteria:="MemberNo.HasFreeWheelAlignment=='False'", Enabled:=False)> _
    Public Property WheelAlignment() As Boolean
        Get
            Return fWheelAlignment
        End Get
        Set(value As Boolean)
            SetPropertyValue("WheelAlignment", fWheelAlignment, value)
        End Set
    End Property

    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property IssuingBranch() As Branch
        Get
            Return fIssuingBranch
        End Get
        Set(value As Branch)
            SetPropertyValue("IssuingBranch", fIssuingBranch, value)
        End Set
    End Property

    <Appearance("DisableComments", Criteria:="MemberNo.HasPreInsValuation=='False' && MemberNo.HasFullMechanical=='False' && MemberNo.HasFreeWheelAlignment=='False'", Enabled:=False)> _
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
        If String.Compare(propertyName, "VoucherNo", False) = 0 Or String.Compare(propertyName, "MemberNo", False) = 0 Or String.Compare(propertyName, "FullMechanical", False) = 0 Or String.Compare(propertyName, "PreInsValuation", False) = 0 Then
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

    End Sub
    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        Try
            If VoucherNo Is Nothing And fComments Is Nothing = False Then
                Dim VoucherNoNew As Integer = GenerateNumbers.GenVoucherNo
                If VoucherNoNew < 10 Then
                    VoucherNo = String.Format("{0}/000{1}", period, VoucherNoNew)
                ElseIf VoucherNoNew < 100 Then
                    VoucherNo = String.Format("{0}/00{1}", period, VoucherNoNew)
                ElseIf VoucherNoNew < 1000 Then
                    VoucherNo = String.Format("{0}/0{1}", period, VoucherNoNew)
                Else
                    VoucherNo = String.Format("{0}/{1}", period, VoucherNoNew)
                End If
                Save()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
