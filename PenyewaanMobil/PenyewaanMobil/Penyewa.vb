Imports MySql.Data.MySqlClient
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32
Imports System.Numerics
Public Class Penyewa

    Public Shared idP As Integer
    Public Shared nik As BigInteger

    Private Sub updateTable()
        connect()

        Dim sqlDtp As New DataTable

        sqlCmd.Connection = sqlConn
        sqlCmd.CommandText = "select idPenyewa as ID, nikPenyewa as 'NIK', namaPenyewa as 'Nama', alamatPenyewa as 'Alamat'
                              from dbpenyewaanmobil.penyewa"
        'sqlCmd.CommandText = "SELECT idMobil FROM dbpenyewaanmobil.mobil EXCEPT SELECT fotoMobil FROM dbpenyewaanmobil.mobil"

        sqlDr = sqlCmd.ExecuteReader
        sqlDtp.Load(sqlDr)
        sqlDr.Close()
        sqlConn.Close()
        DataGridView1.DataSource = sqlDtp

    End Sub
    Private Sub Penyewa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        updateTable()
        DataGridView1.Columns(0).ReadOnly = True
        DataGridView1.Columns(1).ReadOnly = True
        DataGridView1.Columns(2).ReadOnly = True
        DataGridView1.Columns(3).ReadOnly = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        connect()
        'sqlCmd.Connection = sqlConn

        'nik = BigInteger.Parse(TextBox1.Text)
        Try
            sqlQuery = "insert into penyewa(nikPenyewa, namaPenyewa, alamatPenyewa) 
                        value('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "')"

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

    'Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
    '    Try
    '        idP = DataGridView1.SelectedRows(0).Cells(0).Value
    '        TextBox1.Text = DataGridView1.SelectedRows(0).Cells(1).Value
    '        TextBox2.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString()
    '        TextBox3.Text = DataGridView1.SelectedRows(0).Cells(3).Value.ToString()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            idP = DataGridView1.SelectedRows(0).Cells(0).Value
            TextBox1.Text = DataGridView1.SelectedRows(0).Cells(1).Value
            TextBox2.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString()
            TextBox3.Text = DataGridView1.SelectedRows(0).Cells(3).Value.ToString()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'DataGridView1.SelectedRows(0).Cells(0).Value
        'nik = BigInteger.Parse(TextBox1.Text)
        Try
            connect()
            sqlQuery = "update penyewa set " & "nikPenyewa='" & TextBox1.Text & "', " & "namaPenyewa='" & TextBox2.Text & "', " & "alamatPenyewa='" & TextBox3.Text & "' " &
                       "WHERE idPenyewa='" & idP & "'"


            sqlCmd = New MySqlCommand(sqlQuery, sqlConn)
            'sqlDr = sqlCmd.ExecuteReader
            'sqlDr.Close()

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
        Call CType(DataGridView1.DataSource, DataTable).Rows.Clear()
        updateTable()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call CType(DataGridView1.DataSource, DataTable).Rows.Clear()
        Me.Close()
    End Sub
End Class