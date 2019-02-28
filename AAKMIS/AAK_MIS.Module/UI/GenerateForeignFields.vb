Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpo.DB

Public Class GenerateForeignFields
    Private Shared Property con As New Constants
    Public Shared Property uow As UnitOfWork
    Public Shared Sub GetUow()
        If uow Is Nothing Then
            Dim sqlconn = MSSqlConnectionProvider.GetConnectionString(con.server, con.user, con.password, con.DB)
            Dim dl As IDataLayer = XpoDefault.GetDataLayer(sqlconn, AutoCreateOption.DatabaseAndSchema)
            uow = New UnitOfWork(dl)
        End If
    End Sub
    Public Shared ReadOnly Property ThePeriod() As String
        Get
            uow = Nothing
            GetUow()
            Dim Period As String = ""
            Using xpc As New XPCollection(Of RescuePeriod)(uow)
                For Each rec As RescuePeriod In xpc
                    Period = String.Format("{0}/{1}", rec.RescueYear, rec.RescueMonth)
                    Exit For
                Next
            End Using
            Return Period
        End Get
    End Property
End Class