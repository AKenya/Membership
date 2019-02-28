
Imports DevExpress.Xpo

Imports DevExpress.Persistent.BaseImpl

Public Class Applicant_Docs
    Inherits FileAttachmentBase
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Private fDocumentType As DocumentType
    Protected fApplicant As Applicants
    <Persistent(), Association("Applicants-Applicant_Docs")> _
    Public Property Applicant() As Applicants
        Get
            Return fApplicant
        End Get
        Set(ByVal value As Applicants)
            SetPropertyValue("Applicant", fApplicant, value)
        End Set
    End Property
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        fDocumentType = DocumentType.Unknown
    End Sub
    Public Property DocumentType() As DocumentType
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As DocumentType)
            SetPropertyValue("DocumentType", fDocumentType, value)
        End Set
    End Property
End Class
Public Enum DocumentType
    License = 1
    GoodConduct = 2
    CurriculumVitae = 3
    Others = 4
    Unknown = 0
End Enum
