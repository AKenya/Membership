Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation

<DefaultClassOptions()> _
Public Class Languages
    Inherits BaseObject
    Private fLanguage As Language
    Private fCompetencyLevel As CompetencyLevels
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        ' This constructor is used when an object is loaded from a persistent storage.
        ' Do not place any code here or place it only when the IsLoading property is false:
        ' if (!IsLoading){
        '   It is now OK to place your initialization code here.
        ' }
        ' or as an alternative, move your initialization code into the AfterConstruction method.
    End Sub

    <Association("Applicants-Languages", GetType(Applicants))> _
    Public Property Applicants As Applicants
    <RuleRequiredField()> _
    Public Property Language() As Language
        Get
            Return fLanguage
        End Get
        Set(ByVal value As Language)
            SetPropertyValue("Language", fLanguage, value)
        End Set
    End Property
    Public Property CompetencyLevel() As CompetencyLevels
        Get
            Return fCompetencyLevel
        End Get
        Set(ByVal value As CompetencyLevels)
            SetPropertyValue("CompetencyLevel", fCompetencyLevel, value)
        End Set
    End Property

    Public Enum CompetencyLevels
        Native
        Fluent
    End Enum

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place here your initialization code.
    End Sub
End Class
