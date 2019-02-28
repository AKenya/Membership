Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.DC

<XafDefaultProperty("EducationLevel")> _
Public Class EducationLevel
    Inherits BaseObject
    Private fELevel As String
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <RuleRequiredField()> _
<RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property EducationLevel() As String
        Get
            Return fELevel
        End Get
        Set(ByVal value As String)
            SetPropertyValue("EducationLevel", fELevel, value)
        End Set
    End Property

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place here your initialization code.
    End Sub
End Class