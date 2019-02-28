Imports System.Linq
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl

<DefaultClassOptions()> _
Public Class RescuePeriod
    Inherits BaseObject
    Private fRescueMonth, fRescueYear As String
    Private fResetDate As Date
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Property RescueMonth() As String
        Get
            Return fRescueMonth
        End Get
        Set(ByVal value As String)
            SetPropertyValue("RescueMonth", fRescueMonth, value)
        End Set
    End Property
    Public Property RescueYear() As String
        Get
            Return fRescueYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue("RescueYear", fRescueYear, value)
        End Set
    End Property
    Public Property ResetDate() As Date
        Get
            Return fResetDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("ResetDate", fResetDate, value)
        End Set
    End Property
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place here your initialization code.
    End Sub
End Class
