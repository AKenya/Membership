
Public Class Constants
    Public Property server = "AAKENYASVR2"
    Public Property DB = "AAK_MIS"
    Public Property user = "sa"
    Public Property password = "@m1m1n1msom1"
    Public Shared Function ConnectionString() As String
        Return String.Format("Data Source={0}; Initial Catalog={1}; User Id={2}; Password={3}; Persist Security Info=true", "AAKENYASVR2", "AAK_MIS", "sa", "@m1m1n1msom1")
    End Function
End Class
