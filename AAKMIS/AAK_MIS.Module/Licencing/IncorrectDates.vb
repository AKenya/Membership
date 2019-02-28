Imports System.Linq

Public Class IncorrectDates : Inherits Exception
    Public Sub New()
        MyBase.New("Incorrect dates entered. Kindly correct the dates and try again.")
    End Sub
End Class