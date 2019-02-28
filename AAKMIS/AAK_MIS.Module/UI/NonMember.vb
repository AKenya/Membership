Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.DC

<XafDefaultProperty("NonMemberNo")> _
Public Class NonMember
    Inherits BaseObject
    Private fNonMemberNo, fClientNames, fMobileNo, fPostalAddress, fTown, fEmail, period As String
    Private fBranch As Branch
    Private ReadOnly NewSession As Boolean
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <Appearance("NonMemberNoDisabled", Enabled:=False)> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property NonMemberNo As String
        Get
            Return fNonMemberNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("NonMemberNo", fNonMemberNo, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property ClientNames As String
        Get
            Return fClientNames
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ClientNames", fClientNames, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property MobileNo As String
        Get
            Return fMobileNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("MobileNo", fMobileNo, value)
        End Set
    End Property
    Public Property PostalAddress As String
        Get
            Return fPostalAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PostalAddress", fPostalAddress, value)
        End Set
    End Property
    Public Property Town As String
        Get
            Return fTown
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Town", fTown, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property Branch As Branch
        Get
            Return fBranch
        End Get
        Set(ByVal value As Branch)
            SetPropertyValue("Branch", fBranch, value)
        End Set
    End Property
    Public Property Email As String
        Get
            Return fEmail
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Email", fEmail, value)
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
        If propertyName = "ClientNames" Or propertyName = "MobileNo" Or propertyName = "Branch" Then
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
        period = GenerateForeignFields.ThePeriod
    End Sub
    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        Try
            If NonMemberNo Is Nothing Then
                Dim NonMemberNoNoNew As Integer = GenerateNumbers.GetNewNonMemberNo
                If NonMemberNoNoNew < 10 Then
                    NonMemberNo = String.Format("NM/{0}/00{1}", period, NonMemberNoNoNew)
                ElseIf NonMemberNoNoNew < 100 Then
                    NonMemberNo = String.Format("NM/{0}/0{1}", period, NonMemberNoNoNew)
                Else
                    NonMemberNo = String.Format("NM/{0}/{1}", period, NonMemberNoNoNew)
                End If
                Save()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    <Association("NonMem-RescueDetails", GetType(RescueDetails))> _
    Public ReadOnly Property Rescue() As XPCollection
        Get
            Return GetCollection("Rescue")
        End Get
    End Property
End Class
