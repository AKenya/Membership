Imports System.Data.SqlClient

Public Class SQLdb
    Dim sqlconn As String = "Data Source=(local); database=AAK_MIS; User Id=sa; Password='@m1m1n1msom1'"
    Public Sub SendNonQuery(ByVal strQuery As String)
        Dim cn As New SqlConnection(sqlconn)
        Try
            Dim cmd As New SqlCommand(strQuery, cn)
            cn.Open()
            Dim affectedRows As String = cmd.ExecuteNonQuery()

        Catch ex As Exception
        Finally
            cn.Close()
        End Try

    End Sub

    Public Function SendScalarQuery(ByVal strQuery As String) As String
        Dim result As Object = Nothing
        Dim strResult As String = Nothing
        Dim cn As New SqlConnection(sqlconn)
        Try
            Dim cmd As New SqlCommand(strQuery, cn)
            cn.Open()
            result = cmd.ExecuteScalar()
            strResult = result.ToString()

        Catch ex As Exception
        Finally
            cn.Close()
        End Try
        Return strResult
    End Function

    Public Function SendDataTableQuery(ByVal strQuery As String) As DataTable
        Dim result As New DataTable()
        Dim cn As New SqlConnection(sqlconn)
        Try
            cn.Open()
            Dim da As New SqlDataAdapter(strQuery, cn)
            da.Fill(result)
        Catch ex As Exception
        Finally
            cn.Close()
        End Try

        Return result
    End Function
End Class
