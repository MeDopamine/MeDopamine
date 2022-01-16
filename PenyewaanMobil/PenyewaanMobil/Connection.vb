Imports MySql.Data.MySqlClient
Module Connection
    Public sqlConn As New MySqlConnection
    Public sqlCmd As New MySqlCommand
    Public sqlDr As MySqlDataReader
    Public sqlDt As New DataTable
    Public sqlDa As New MySqlDataAdapter
    Public sqlQuery As String
    Public imgPath As String
    Public arrImage() As Byte
    Public ofd As FileDialog

    Dim server As String = "localhost"
    Dim username As String = "root"
    Dim password As String = ""
    Dim database As String = "dbPenyewaanMobil"

    Dim result As Boolean


    Private bitmap As Bitmap

    Public Function connect() As Boolean
        Try
            If sqlConn.State = ConnectionState.Closed Then

                sqlConn.ConnectionString = "server = " + server + ";" + "user id=" + username + ";" + "password=" + password + ";" + "database = " + database + "; Convert Zero Datetime=True"
                sqlConn.Open()
                result = True

            End If
        Catch ex As Exception

            result = False

        End Try

        Return result
    End Function
End Module
