Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.DC

<XafDefaultProperty("CourseTitle")> _
Public Class Course
    Inherits BaseObject
    Private fCourseTitle As String
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property CourseTitle() As String
        Get
            Return fCourseTitle
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CourseTitle", fCourseTitle, value)
        End Set
    End Property
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place here your initialization code.
    End Sub
End Class
