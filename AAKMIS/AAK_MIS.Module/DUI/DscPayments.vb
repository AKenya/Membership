Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.Data.Filtering

' With XPO, the business model is declared by classes (so-called Persistent Objects) that will define the database structure, and consequently, the user interface (http://documentation.devexpress.com/#Xaf/CustomDocument2600).

' Specify various UI options for your persistent class and its properties using a declarative approach via built-in attributes (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
'<ImageName("BO_Contact")> _
'<XafDefaultProperty("PersistentProperty")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewAndDetailView, True, NewItemRowPosition.Top)> _
<ImageName("BO_Sale")> _
<DefaultClassOptions()> _
Public Class DscPayments
    Inherits BaseObject
    Private fFeePayable, fAmount, fBalance As Double
    Private fModeOfPayment As PaymentMode
    Private fROffice As Branch
    Private fDatePaid As Date
    Private fReceiptNo, fChequeNo, fEFTCode As String
    Private fMemberNo As StudentManager

    <Appearance("DisablePayments", AppearanceItemType:="ViewItem", Context:="DetailView", Enabled:=False, TargetItems:="*")> _
    Protected Function DisablePayments() As Boolean
        Return Not (ModifiedOn > DateAdd(DateInterval.Day, -2, Today))
    End Function

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        ' This constructor is used when an object is loaded from a persistent storage.
        ' Do not place any code here or place it only when the IsLoading property is false:
    End Sub

#Region "Properties and Colections"
    <VisibleInListView(False)> _
    <Appearance("indmemdisabled", Enabled:=False)> _
    <Association("StudentMgr-DscPayments", GetType(StudentManager))> _
    Public Property MemberNumber() As StudentManager
        Get
            Return fMemberNo
        End Get
        Set(ByVal value As StudentManager)
            SetPropertyValue("MemberNumber", fMemberNo, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso value IsNot Nothing Then
                If Convert.ToInt32(Session.Evaluate(Of Branch)(CriteriaOperator.Parse("Branch.IsNrbBranch"), CriteriaOperator.Parse("BranchName=?", value.Branch))) = True Then
                    fFeePayable = value.DLClass.FeePerLessonNrb * value.LessonsApplied
                End If

            End If
        End Set
    End Property
#End Region
    '<Appearance("DisableFeePay", Criteria:="HasInvoice='True'", enabled:=False)> _
    <Appearance("DisableFeePay", enabled:=False)> _
    Public Property FeePayable() As Double
        Get
            Return fFeePayable
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

    Dim tempObject As Object
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
    <VisibleInListView(False)> _
    <Appearance("Paymodenumber", Visibility:=ViewItemVisibility.Hide, Criteria:="ModeOfPayment!='CHEQUE'")> _
    Public Property ChequeNumber() As String
        Get
            Return fChequeNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ChequeNumber", fChequeNo, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <Appearance("EFTCode", Visibility:=ViewItemVisibility.Hide, Criteria:="ModeOfPayment!='EFT'")> _
    Public Property EFTCode() As String
        Get
            Return fEFTCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("EFTCode", fEFTCode, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property ReceiptingOffice() As Branch
        Get
            Return fROffice
        End Get
        Set(ByVal value As Branch)
            SetPropertyValue("ROffice", fROffice, value)
        End Set
    End Property

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
        If String.Compare(propertyName, "Amount", False) = 0 Or String.Compare(propertyName, "ReceiptingOffice", False) = 0 Or String.Compare(propertyName, "ReceiptNo", False) = 0 Then
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
        If Amount <= 0 Then
            Throw New RequiredValueMissing()
        End If
        If DatePaid > Today Then
            Throw New IncorrectDates
        End If
    End Sub
End Class