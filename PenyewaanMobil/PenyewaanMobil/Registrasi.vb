Imports MySql.Data.MySqlClient
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32
Public Class Registrasi

    Public Shared imgr As String

    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    '    If connect() Then
    '        Try
    '            sqlCmd.Connection = sqlConn
    '            sqlQuery = "insert into registrasi(UserName, Password) value('" & TextBox1.Text & "','" & TextBox2.Text & "')"

    '            sqlCmd = New MySqlCommand(sqlQuery, sqlConn)
    '            sqlDr = sqlCmd.ExecuteReader
    '            sqlConn.Close()
    '            Me.Close()
    '        Catch ex As Exception

    '        End Try

    '    End If

    'End Sub



    Private Sub Registrasi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        imgr = imgPath

        connect()
        sqlCmd.Connection = sqlConn
        sqlQuery = "insert into registrasi(UserName, Password, email, foto) value('" & TextBox1.Text & "',
                   '" & TextBox2.Text & "','" & TextBox3.Text & "','" & imgr & "')"

        sqlCmd = New MySqlCommand(sqlQuery, sqlConn)
        sqlDr = sqlCmd.ExecuteReader
        sqlConn.Close()
        Me.Close()


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try
            ofd = New OpenFileDialog()

            ofd.Title = "Open"
            ofd.Filter = "All Format|*.*"

            If ofd.ShowDialog() = DialogResult.OK Then
                imgPath = ofd.FileName
                PictureBox1.ImageLocation = imgPath
                imgr = imgPath

                PictureBox1.Load(imgr)
                PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            End If

            ofd = Nothing
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try
    End Sub
End Class