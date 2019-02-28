Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Xpo.Metadata

<DefaultClassOptions()> _
Public Class SuppliersList
    Inherits BaseObject
    Private fCompanyName, fCertficateNo, fCellNo, fAlternativeCell, fContactPerson, fRemarks As String
    'Private fCategory As CategoryGroups
    Private fApproved, fSuspended As Boolean
    Private fApprovedOn, fSuspendedOn, fNextReviewDate As Date
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Property CompanyName() As String
        Get
            Return fCompanyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CompanyName", fCompanyName, value)
        End Set
    End Property

    Public Property CertficateNo() As String
        Get
            Return fCertficateNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CertficateNo", fCertficateNo, value)
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

    Public Property AlternativeCell() As String
        Get
            Return fAlternativeCell
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AlternativeCell", fAlternativeCell, value)
        End Set
    End Property

    Public Property ContactPerson() As String
        Get
            Return fContactPerson
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ContactPerson", fContactPerson, value)
        End Set
    End Property

    Public Property Approved() As Boolean
        Get
            Return fApproved
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("Approved", fApproved, value)
        End Set
    End Property

    Public Property ApprovedOn() As Date
        Get
            Return fApprovedOn
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("ApprovedOn", fApprovedOn, value)
        End Set
    End Property

    Public Property Suspended() As Boolean
        Get
            Return fSuspended
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue("Suspended", fSuspended, value)
        End Set
    End Property
    Public Property SuspendedOn() As Date
        Get
            Return fSuspendedOn
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("SuspendedOn", fSuspendedOn, value)
        End Set
    End Property
    Public Property NextReviewDate() As Date
        Get
            Return fNextReviewDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("NextReviewDate", fNextReviewDate, value)
        End Set
    End Property
    <Size(400)> _
    Public Property Remarks() As String
        Get
            Return fRemarks
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Remarks", fRemarks, value)
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
        If String.Compare(propertyName, "CompanyName", False) = 0 Or String.Compare(propertyName, "CertficateNo", False) = 0 Or String.Compare(propertyName, "CellNo", False) = 0 Or String.Compare(propertyName, "AlternativeCell", False) = 0 Then
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

    <Association("CategoryGroups-Suppliers", GetType(CategoryGroups))> _
    Public ReadOnly Property Category() As XPCollection
        Get
            Return GetCollection("Category")
        End Get
    End Property
End Class
