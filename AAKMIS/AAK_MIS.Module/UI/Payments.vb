Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Xpo.DB
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Model

<DefaultClassOptions()> _
Public Class Payments
    Inherits BaseObject
    Private fAmount, fBalance As Double
    Private fFeePayable As Nullable(Of Double) = Nothing
    Private fModeOfPayment As PaymentMode
    Private fROffice As Branch
    Private fDatePaid As Date
    Private fExpiryDate As Date
    Private fPolicyExpiryDate, fEffectiveDate As Date
    Private fReceiptNo, fInvoiceNo, fChequeNo As String
    Private fRecruiter As Recruiter
    Private fMemberNo As MemberRegistration
    Private ReadOnly NewSession As Boolean
    Private ReadOnly roleName As String
    Private Shared Property con As New Constants
    Public Shared Property uow As UnitOfWork
    Public Shared Sub GetUow()
        If uow Is Nothing Then
            Dim sqlconn = MSSqlConnectionProvider.GetConnectionString(con.server, con.user, con.password, con.DB)
            Dim dl = XpoDefault.GetDataLayer(sqlconn, AutoCreateOption.DatabaseAndSchema)
            uow = New UnitOfWork(dl)
        End If
    End Sub
    <Appearance("DisablePayments", AppearanceItemType:="ViewItem", Context:="DetailView", Enabled:=False, TargetItems:="*")> _
    Protected Function DisablePayments() As Boolean
        Return (Not (ModifiedOn > DateAdd(DateInterval.Day, -7, Today))) Or (MemberNo.Category.Status = MemberCategory.CategoryStatus.Inactive)
    End Function

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        If Not IsLoading Then
            NewSession = True
        Else
            GetUow()
        End If
        Dim usr As User = SecuritySystem.CurrentUser
        For Each role In usr.Roles
            roleName = role.Name
            Exit For
        Next
    End Sub

#Region "Properties and Colections"
    <VisibleInListView(False)> _
    <Appearance("indmemdisabled", Enabled:=False)> _
    <Appearance("HideIndMemberNo", Criteria:="MemberNo=?", visibility:=ViewItemVisibility.Hide)> _
    <Association("IndReg-Payments", GetType(MemberRegistration))> _
    Public Property MemberNo() As MemberRegistration
        Get
            Return fMemberNo
        End Get
        Set(ByVal value As MemberRegistration)
            SetPropertyValue("MemberNo", fMemberNo, value) 
        End Set
    End Property
#End Region

    <Appearance("DisableFeePay", enabled:=False)> _
    <Persistent("FeePayable")> _
    Public ReadOnly Property FeePayable() As Nullable(Of Double)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fFeePayable.HasValue Then
                UpdateTotalPayable(False)
            End If
            Return fFeePayable
        End Get
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
    <Appearance("ModeOfPaymentDisabled", Criteria:="ModeOfPayment!=? && roleName!='MEMBERSHIP ADMIN'", Enabled:=False)> _
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

    <RuleRequiredField()> _
    Public Property ReceiptingOffice() As Branch
        Get
            Return fROffice
        End Get
        Set(ByVal value As Branch)
            SetPropertyValue("ROffice", fROffice, value)
        End Set
    End Property
    <RuleUniqueValue(DefaultContexts.Save)> _
    <RuleRequiredField("", DefaultContexts.Save, TargetCriteria:="ModeOfPayment!='LPO'")> _
    <Appearance("ReceiptNoDisabled", Criteria:="ModeOfPayment='LPO'", Enabled:=False)> _
    Public Property ReceiptNo() As String
        Get
            Return fReceiptNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ReceiptNo", fReceiptNo, value)
        End Set
    End Property
    <Appearance("DisableInvoiceNo", Criteria:="!(ModeOfPayment='LPO')", Enabled:=False)> _
    Public Property InvoiceNo() As String
        Get
            Return fInvoiceNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("InvoiceNo", fInvoiceNo, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <Appearance("DisablePolicyExpiryDate", Criteria:="MemberNo.Category.Expiry='Fixed'", Enabled:=False, visibility:=ViewItemVisibility.Hide)> _
    Public Property PolicyExpiryDate() As Date
        Get
            Return fPolicyExpiryDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("PolicyExpiryDate", fPolicyExpiryDate, value)
        End Set
    End Property
    <RuleRequiredField()> _
    <ImmediatePostData()> _
    Public Property EffectiveDate() As Date
        Get
            Return fEffectiveDate
        End Get
        Set(ByVal value As Date)
            If SetPropertyValue("EffectiveDate", fEffectiveDate, value) Then
                OnChanged("ExpiryDate")
            End If
        End Set
    End Property

    Dim tempDate As Object
    <Appearance("DisableExpiryDate", Enabled:=False)> _
    <PersistentAlias("Iif(MemberNo.Category.Expiry='Fixed', ADDMONTHS(ADDDAYS(EffectiveDate,-1),MemberNo.Category.Span),PolicyExpiryDate)")> _
    Public Property ExpiryDate() As Date
        Get
            tempDate = EvaluateAlias("ExpiryDate")
            If tempObject IsNot Nothing Then
                Return CDate(tempDate)
                fExpiryDate = CDate(tempDate)
            Else
                Return fExpiryDate
            End If
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("ExpiryDate", fExpiryDate, value)
        End Set
    End Property
    <VisibleInListView(False)> _
    <Appearance("DisableRecruiter", Criteria:="Recruiter!=? && (roleName!='ADMINISTRATORS' && roleName!='MEMBERSHIP ADMIN')", Enabled:=False)> _
    <RuleRequiredField()> _
    Public Property Recruiter() As Recruiter
        Get
            Return fRecruiter
        End Get
        Set(ByVal value As Recruiter)
            SetPropertyValue("Recruiter", fRecruiter, value)
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
        If String.Compare(propertyName, "Amount", False) = 0 Or String.Compare(propertyName, "ReceiptingOffice", False) = 0 Or String.Compare(propertyName, "ReceiptNo", False) = 0 Or String.Compare(propertyName, "MemberNo", False) = 0 Then
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
    Protected Overrides Sub OnDeleting()
        MyBase.OnDeleting()
        Try
            Dim sqlHandlerDel As New SQLdb
            Dim StrQDel As String = String.Format("UPDATE MemberRegistration SET EffectiveDate = '{0}', ExpiryDate='{1}' WHERE Oid = '{2}'", Nothing, Nothing, MemberNo.Oid)
            sqlHandlerDel.SendNonQuery(StrQDel)

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If Amount <= 0 And MemberNo Is Nothing = False Then
            Throw New RequiredValueMissing()
        End If
        If MemberNo Is Nothing = False Then
            Try
                Dim sqlHandler As New SQLdb
                Dim StrQ As String = String.Format("UPDATE MemberRegistration SET EffectiveDate = '{0}', ExpiryDate='{1}' WHERE Oid = '{2}'", EffectiveDate.ToString("yyyyMMdd"), ExpiryDate.ToString("yyyyMMdd"), MemberNo.Oid)
                sqlHandler.SendNonQuery(StrQ)
            Catch ex As Exception
                Throw
            End Try
            If NewSession = True Then
                Try
                    Dim msg As String = String.Format("Dear {0}, Your AA Membership No: {1} is now activated and is valid until {2}. Contact 0709933000/ 0206979000 for more info.", MemberNo.MemberName, MemberNo.MemberNumber, ExpiryDate.ToShortDateString)
                    Dim str As String = MemberNo.CellNumber
                    Dim substr As String = "254" + str.Substring(str.Length - 9)
                    SendSms.SendSMS(substr, msg)
                Catch ex As Exception

                End Try
            End If
        End If
    End Sub


    Public Sub UpdateTotalPayable(ByVal forceChangeEvents As Boolean)
        Dim oldPayableTotal As Nullable(Of Double) = fFeePayable
        Dim tempTotal As Double
        Dim MemberPayments As Int16 = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("MemberNo.Payments.Count")))
        Try
            If MemberPayments > 0 And MemberNo.IsCorporateMember = False Then
                If MemberNo.NoOfVehicles <= MemberNo.Category.CategoryVehicles Then
                    tempTotal = MemberNo.Category.RenewalFee
                Else
                    tempTotal = MemberNo.Category.RenewalFee + (MemberNo.NoOfVehicles - MemberNo.Category.CategoryVehicles) * MemberNo.Category.AdditionalFee
                End If
            ElseIf MemberPayments = 0 And MemberNo.IsCorporateMember = False Then
                If MemberNo.NoOfVehicles <= MemberNo.Category.CategoryVehicles Then
                    tempTotal = MemberNo.Category.NewFee
                Else
                    tempTotal = MemberNo.Category.NewFee + (MemberNo.NoOfVehicles - MemberNo.Category.CategoryVehicles) * MemberNo.Category.AdditionalFee
                End If
            Else
                tempTotal = 6500 + (MemberNo.NoOfVehicles - MemberNo.Category.CategoryVehicles) * 6500
            End If
        Catch ex As Exception

        End Try
        fFeePayable = tempTotal
        If forceChangeEvents Then
            OnChanged("FeePayable", oldPayableTotal, fFeePayable)
        End If
    End Sub

End Class
Public Class RequiredValueMissing : Inherits Exception
    Public Sub New()
        MyBase.New("The amount paid for subscription of this membership must be greater than zero(0)")
    End Sub
End Class