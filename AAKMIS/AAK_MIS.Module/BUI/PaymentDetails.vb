Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.DC
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Model

<DefaultClassOptions()> _
<DomainLogic(GetType(PaymentCharges))> _
Public Class PaymentDetails
    Inherits BaseObject
    Private fFeePayable, fAmount, fBalance, fDiscount As Double
    Private fDatePaid As Date
    Private fModeOfPayment As PaymentMode
    Private fChequeNo, fReceiptNo As String
    Private fSelectedNo As Integer
    Private fOrderNo As Order
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <RuleRequiredField()> _
    <Appearance("EmployerDisabled", Enabled:=False)> _
    <Association("Employers-PaymentDetails", GetType(Employers))> _
    Public Property Employers As Employers
    <RuleRequiredField()> _
    <DataSourceProperty("Employers.Order", DataSourcePropertyIsNullMode.SelectAll)> _
    Public Property OrderNo() As Order
        Get
            Return fOrderNo
        End Get
        Set(ByVal value As Order)
            SetPropertyValue("OrderNo", fOrderNo, value)
        End Set
    End Property

    <RuleRequiredField()> _
    <ImmediatePostData()> _
    Public Property SelectedNo() As Integer
        Get
            Return fSelectedNo
        End Get
        Set(ByVal value As Integer)
            If SetPropertyValue("SelectedNo", fSelectedNo, value) Then
                OnChanged("FeePayable")
            End If
        End Set
    End Property
    <ImmediatePostData()> _
    Public Property Discount() As Double
        Get
            Return fDiscount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("Discount", fDiscount, value)
        End Set
    End Property

    <Appearance("FeePayableDisabled", Enabled:=False)> _
    <PersistentAlias("SelectedNo * 10000-(SelectedNo * 10000 * Discount/100)")> _
    Public Property FeePayable() As Double
        Get
            tempObjects = EvaluateAlias("FeePayable")
            If tempObjects IsNot Nothing Then
                Return CDbl(tempObjects)
                fFeePayable = CDbl(tempObjects)
            Else
                Return fFeePayable
            End If
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("FeePayable", fFeePayable, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <ImmediatePostData()> _
    Public Property Amount() As Double
        Get
            Return fAmount
        End Get
        Set(ByVal value As Double)
            If SetPropertyValue("Amount", fAmount, value) Then
                OnChanged("Balance")
            End If
        End Set
    End Property

    Dim tempObject, tempObjects As Object
    <Appearance("Balancedisabled", Enabled:=False)> _
    <PersistentAlias("FeePayable - Amount")> _
    Public Property Balance() As Double
        Get
            tempObject = EvaluateAlias("Balance")
            If tempObject IsNot Nothing Then
                Return CDbl(tempObject)
                fBalance = CDbl(tempObject)
            Else
                Return fBalance
            End If
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("Balance", fBalance, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property DatePaid() As Date
        Get
            Return fDatePaid
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("DatePaid", fDatePaid, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <ImmediatePostData()> _
    Public Property ModeOfPayment() As PaymentMode
        Get
            Return fModeOfPayment
        End Get
        Set(ByVal value As PaymentMode)
            SetPropertyValue("ModeOfPayment", fModeOfPayment, value)
        End Set
    End Property
    <Appearance("Paymodenumber", Visibility:=ViewItemVisibility.Hide, Criteria:="ModeOfPayment!='CHEQUE'")> _
    Public Property ChequeNumber() As String
        Get
            Return fChequeNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ChequeNumber", fChequeNo, value)
        End Set
    End Property
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property ReceiptNo() As String
        Get
            Return fReceiptNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ReceiptNo", fReceiptNo, value)
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
        If propertyName = "Amount" Or propertyName = "DatePaid" Or propertyName = "ReceiptNo" Then
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

End Class
