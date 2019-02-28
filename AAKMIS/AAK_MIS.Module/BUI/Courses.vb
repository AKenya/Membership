Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation

<DefaultClassOptions()> _
Public Class Courses
    Inherits BaseObject
    Private fCourseTitle As Course
    Private fExaminingBody As String
    Private fPointsAttained As Integer
    Private fAssessmentDate As Date
    Private fRating As Ratings
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        ' This constructor is used when an object is loaded from a persistent storage.
        ' Do not place any code here or place it only when the IsLoading property is false:
        ' if (!IsLoading){
        '   It is now OK to place your initialization code here.
        ' }
        ' or as an alternative, move your initialization code into the AfterConstruction method.
    End Sub

    <Association("Applicants-Courses", GetType(Applicants))> _
    Public Property Applicants As Applicants
    <RuleRequiredField()> _
    Public Property CourseTitle() As Course
        Get
            Return fCourseTitle
        End Get
        Set(ByVal value As Course)
            SetPropertyValue("CourseTitle", fCourseTitle, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property ExaminingBody() As String
        Get
            Return fExaminingBody
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ExaminingBody", fExaminingBody, value)
        End Set
    End Property
    Public Property PointsAttained() As Integer
        Get
            Return fPointsAttained
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("PointsAttained", fPointsAttained, value)
        End Set
    End Property
    Public Property AssessmentDate() As Date
        Get
            Return fAssessmentDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("AssessmentDate", fAssessmentDate, value)
        End Set
    End Property
    Public Property Rating() As Ratings
        Get
            Return fRating
        End Get
        Set(ByVal value As Ratings)
            SetPropertyValue("Rating", fRating, value)
        End Set
    End Property

    Public Enum Ratings
        FAIL
        AVERAGE
        PASS
        INCOMPLETE
        ONGOING
    End Enum
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Rating = Ratings.PASS
        ' Place here your initialization code.
    End Sub
End Class