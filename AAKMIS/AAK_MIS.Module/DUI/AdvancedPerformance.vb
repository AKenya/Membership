Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation

<DefaultClassOptions()> _
Public Class AdvancedPerformance
    Inherits BaseObject
    Private fName As String
    Private fPoints As Double
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        ' This constructor is used when an object is loaded from a persistent storage.
        ' Do not place any code here or place it only when the IsLoading property is false.
    End Sub

    <RuleRequiredField()> _
    <RuleUniqueValue("", DefaultContexts.Save)> _
    Public Property Name As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Name", fName, value)
        End Set
    End Property
    Public Property Points As Double
        Get
            Return fPoints
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("Points", fPoints, value)
        End Set
    End Property
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place here your initialization code.
    End Sub
End Class
