Imports MySql.Data.MySqlClient
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32

Public Class Login

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Button2.Visible = False
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Registrasi.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        connect()
        sqlCmd = New MySqlCommand("select * from registrasi where UserName='" & TextBox1.Text & "'and Password='" & TextBox2.Text & "'", sqlConn)
        sqlDr = sqlCmd.ExecuteReader
        sqlDr.Read()
        If sqlDr.HasRows Then

            Main.showContent()
            Main.LOGINToolStripMenuItem.Visible = False
            Me.Close()
            Main.Visible = True

        Else
            MessageBox.Show("Ngetik yang Benet BOSSS!!!", "System Galak", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        sqlConn.Close()
        sqlDr.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If connect() Then
            MsgBox("Berhasil")
        Else
            MsgBox("Gagal")
        End If
    End Sub

End Class
