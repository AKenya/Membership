Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation

<DefaultClassOptions()> _
Public Class Employment
    Inherits BaseObject
    Private fJobTitle As JobTitle
    Private fEmployer, fLeavingReason As String
    Private fDateFrom, fDateTo As Date
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        ' This constructor is used when an object is loaded from a persistent storage.
        ' Do not place any code here or place it only when the IsLoading property is false:
        ' if (!IsLoading){
        '   It is now OK to place your initialization code here.
        ' }
        ' or as an alternative, move your initialization code into the AfterConstruction method.
    End Sub
    <Association("Applicants-Employment", GetType(Applicants))> _
    Public Property Applicants As Applicants
    <RuleRequiredField()> _
    Public Property JobTitle() As JobTitle
        Get
            Return fJobTitle
        End Get
        Set(ByVal value As JobTitle)
            SetPropertyValue("JobTitle", fJobTitle, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property Employer() As String
        Get
            Return fEmployer
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Employer", fEmployer, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property DateFrom() As Date
        Get
            Return fDateFrom
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("DateFrom", fDateFrom, value)
        End Set
    End Property
    <RuleRequiredField()> _
    Public Property DateTo() As Date
        Get
            Return fDateTo
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("DateTo", fDateTo, value)
        End Set
    End Property

    <Size(200)> _
    Public Property LeavingReason() As String
        Get
            Return fLeavingReason
        End Get
        Set(ByVal value As String)
            SetPropertyValue("LeavingReason", fLeavingReason, value)
        End Set
    End Property

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place here your initialization code.
    End Sub
End Class
