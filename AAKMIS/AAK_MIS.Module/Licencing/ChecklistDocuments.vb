Imports DevExpress.Xpo

Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.Metadata
Imports AAK_MIS.Module.WinWebSolution.Module
Imports DevExpress.ExpressApp.Model
<ImageName("BO_Task")> _
<DefaultClassOptions()> _
Public Class ChecklistDocuments
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Private fDocumentType As Document
    Protected fIDPDetails As IDP_Details
    <Association("IdpDet-CheckListDocuments")> _
    Public Property IDPDetails() As IDP_Details
        Get
            Return fIDPDetails
        End Get
        Set(ByVal value As IDP_Details)
            SetPropertyValue("IDPDetails", fIDPDetails, value)
        End Set
    End Property
    Public Property DocumentType() As Document
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Document)
            SetPropertyValue("DocumentType", fDocumentType, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property ScannedDocs As FileDataEx
    <Persistent("ModifiedOn"), ValueConverter(GetType(UtcDateTimeConverter))> _
    Protected _ModifiedOn As DateTime = DateTime.Now
    <PersistentAlias("_ModifiedOn"), ModelDefaultAttribute("EditMask", "G"), ModelDefaultAttribute("DisplayFormat", "{0:G}")> _
    Public ReadOnly Property DateUploaded() As DateTime
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
        If propertyName = "IDPDetails" Or propertyName = "DateUploaded" Or propertyName = "DocumentType" Or propertyName = "ScannedDocs" Then
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
        fDocumentType = Document.Unknown
        _CreatedBy = Session.GetObjectByKey(Of User)(SecuritySystem.CurrentUserId)
        ' Place here your initialization code.
    End Sub
End Class