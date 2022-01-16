Imports MySql.Data.MySqlClient
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32
Public Class JenisMobil

    Public Shared idHapusj As Integer
    Public Shared jenis As String
    'Public Shared sqlQueryj As String
    'Public sqlCmdj As New MySqlCommand
    Private Sub updateTable()
        connect()

        Dim sqlDtj As New DataTable

        sqlCmd.Connection = sqlConn
        sqlCmd.CommandText = "select idJenis as ID, namaJenis as 'Jenis Mobil', lokasiPembuatan as 'Dibuat di'
                              from dbpenyewaanmobil.jenismobil"
        'sqlCmd.CommandText = "SELECT idMobil FROM dbpenyewaanmobil.mobil EXCEPT SELECT fotoMobil FROM dbpenyewaanmobil.mobil"

        sqlDr = sqlCmd.ExecuteReader
        sqlDtj.Load(sqlDr)
        sqlDr.Close()
        sqlConn.Close()
        DataGridView1.DataSource = sqlDtj

    End Sub
    Private Sub JenisMobil_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        updateTable()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        connect()
        'sqlCmd.Connection = sqlConn
        Try
            sqlQuery = "insert into jenismobil(namaJenis, lokasiPembuatan) 
                        value('" & TextBox1.Text & "','" & TextBox2.Text & "')"

            sqlCmd = New MySqlCommand(sqlQuery, sqlConn)

            Dim x As Integer
            x = sqlCmd.ExecuteNonQuery()
            If x > 0 Then
                MessageBox.Show("berhasil", "Mysql Connector", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("gagal")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try

        'sqlDr = sqlCmd.ExecuteReader
        sqlConn.Close()
        Call CType(DataGridView1.DataSource, DataTable).Rows.Clear()
        updateTable()

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            idHapusj = DataGridView1.SelectedRows(0).Cells(0).Value
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
    'Try
    '    idHapusj = DataGridView1.SelectedRows(0).Cells(0).Value
    'Catch ex As Exception

    'End Try
    'End Sub

    'Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
    '    Try
    '        idHapusj = DataGridView1.SelectedRows(0).Cells(0).Value
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim iExit As DialogResult
        iExit = MessageBox.Show("Yakin mau hapus? ", "Mysql Connector", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        jenis = TextBox1.Text.ToString()
        connect()
        Try

            sqlQuery = "delete from dbpenyewaanmobil.jenismobil where idJenis='" & idHapusj & "'"
            sqlCmd = New MySqlCommand(sqlQuery, sqlConn)

            Dim x As Integer
            x = sqlCmd.ExecuteNonQuery()
            If x > 0 Then
                MessageBox.Show("berhasil", "Mysql Connector", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("gagal")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try


        'For Each row As DataGridViewRow In DataGridView1.SelectedRows
        '    DataGridView1.Rows.Remove(row)
        'Next
        Call CType(DataGridView1.DataSource, DataTable).Rows.Clear()
        updateTable()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call CType(DataGridView1.DataSource, DataTable).Rows.Clear()
        Me.Close()
    End Sub
End Class