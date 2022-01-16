Imports MySql.Data.MySqlClient
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32
Public Class Sewa

    Public Shared durasi As Integer
    Public Shared totalsewa As Integer
    Public Shared kembalian As Integer
    Public Shared totalbayar As Integer
    Public Shared sewa As Integer
    Public Shared idS As Integer
    Public Shared merek As String
    Public Shared penyewa As String
    Public Shared status As String
    Public Shared keluar
    Public Shared masuk

    Public Sub updateTable()
        connect()

        Dim sqlDts As New DataTable

        sqlCmd.CommandText = "select idSewa as ID, penyewa as 'Penyewa', merekSewa as 'Tipe Mobil', statusSewa as 'Status', rencanaPinjam as 'Durasi Pinjam', 
                              tanggalPinjam as 'Tanggal Pinjam', tanggalKembali as 'Tanggal Kembali', totalSewa as 'Total Harga Sewa', totalBayar as 'Pembayaran', biayaKelebihan as 'Biaya Tambahan' 
                              from dbpenyewaanmobil.sewa"
        sqlDr = sqlCmd.ExecuteReader
        sqlDts.Load(sqlDr)
        sqlDr.Close()
        sqlConn.Close()
        DataGridView1.DataSource = sqlDts
    End Sub

    Public Sub hargaSewa()
        Try
            connect()
            merek = ComboBox2.SelectedItem().ToString()
            sqlQuery = "select hargaSewaMobil from dbpenyewaanmobil.mobil where merekMobil='" & merek & "'"
            sqlCmd = New MySqlCommand(sqlQuery, sqlConn)
            sqlDr = sqlCmd.ExecuteReader
            While sqlDr.Read
                sewa = sqlDr("hargaSewaMobil")
            End While
        Catch ex As Exception

        End Try
        sqlDr.Close()

        durasi = Integer.Parse(TextBox1.Text)
        totalsewa = durasi * sewa
        totalbayar = Integer.Parse(TextBox3.Text)
        kembalian = totalbayar - totalsewa
        TextBox2.Text = totalsewa
        TextBox4.Text = kembalian
    End Sub

    Public Sub getData()

        penyewa = ComboBox1.SelectedItem().ToString()
        status = ComboBox3.SelectedItem().ToString()
        masuk = DateTimePicker1.Value.ToString("yyy-MM-dd HH:mm:ss")
        keluar = DateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss")

    End Sub

    Private Sub Sewa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        updateTable()

        TextBox2.ReadOnly = True
        TextBox4.ReadOnly = True

        Try
            connect()

            sqlQuery = "SELECT penyewa.namaPenyewa, mobil.merekMobil
                        FROM penyewa, mobil"

            sqlCmd = New MySqlCommand(sqlQuery, sqlConn)
            sqlDr = sqlCmd.ExecuteReader

            While sqlDr.Read

                ComboBox1.Items.Add(sqlDr("namaPenyewa"))
                ComboBox2.Items.Add(sqlDr("merekMobil"))

            End While

        Catch ex As Exception

        End Try
        sqlDr.Close()

        'Try
        '    connect()
        '    sqlQuery = "select namaPenyewa from dbpenyewaanmobil.penyewa order by namaPenyewa asc"

        '    sqlCmd = New MySqlCommand(sqlQuery, sqlConn)
        '    sqlDr = sqlCmd.ExecuteReader

        '    While sqlDr.Read
        '        ComboBox1.Items.Add(sqlDr("namaPenyewa"))
        '    End While
        'Catch ex As Exception

        'End Try
        'sqlDr.Close()

        'Try
        '    connect()
        '    sqlQuery = "select merekMobil from dbpenyewaanmobil.mobil order by merekMobil asc"

        '    sqlCmd = New MySqlCommand(sqlQuery, sqlConn)
        '    sqlDr = sqlCmd.ExecuteReader

        '    While sqlDr.Read
        '        ComboBox2.Items.Add(sqlDr("merekMobil"))
        '    End While
        'Catch ex As Exception

        'End Try
        'sqlDr.Close()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try

            idS = DataGridView1.SelectedRows(0).Cells(0).Value
            ComboBox1.SelectedItem() = DataGridView1.SelectedRows(0).Cells(1).Value.ToString()
            ComboBox2.SelectedItem() = DataGridView1.SelectedRows(0).Cells(2).Value.ToString()
            ComboBox3.SelectedItem() = DataGridView1.SelectedRows(0).Cells(3).Value.ToString()
            TextBox1.Text = DataGridView1.SelectedRows(0).Cells(4).Value
            DateTimePicker1.Value = DataGridView1.SelectedRows(0).Cells(5).Value
            DateTimePicker2.Value = DataGridView1.SelectedRows(0).Cells(6).Value
            TextBox2.Text = DataGridView1.SelectedRows(0).Cells(7).Value
            TextBox3.Text = DataGridView1.SelectedRows(0).Cells(8).Value
            TextBox4.Text = DataGridView1.SelectedRows(0).Cells(9).Value

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        connect()
        hargaSewa()
        getData()

        If totalbayar < totalsewa Or totalbayar > totalsewa Then
            MessageBox.Show("Bayar Pake Uang Pas Aja Brok", "Kata System", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Try
                sqlQuery = "insert into sewa(penyewa, merekSewa, statusSewa, rencanaPinjam, tanggalPinjam, tanggalKembali, totalSewa, totalBayar, biayaKelebihan) 
                        value('" & penyewa & "','" & merek & "','" & status & "',
                        '" & durasi & "','" & masuk & "','" & keluar & "',
                        '" & totalsewa & "','" & totalbayar & "','" & kembalian & "')"

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

            'sqlConn.Close()
            Call CType(DataGridView1.DataSource, DataTable).Rows.Clear()
            updateTable()
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        connect()
        hargaSewa()
        getData()

        If totalbayar < totalsewa Or totalbayar > totalsewa Then
            MessageBox.Show("Bayar Pake Uang Pas Aja Brok", "Kata System", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Try
                sqlQuery = "update sewa set " & "penyewa='" & penyewa & "'," & "statusSewa='" & status & "'," & "rencanaPinjam='" & durasi & "'," &
                                               "tanggalPinjam='" & masuk & "'," & " tanggalKembali='" & keluar & "'," & " totalSewa='" & totalsewa & "'," &
                                               " totalBayar='" & totalbayar & "'," & " biayaKelebihan='" & kembalian & "'," & "merekSewa='" & merek & "'" &
                                               "WHERE idSewa='" & idS & "'"
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
            Call CType(DataGridView1.DataSource, DataTable).Rows.Clear()
            updateTable()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        hargaSewa()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Call CType(DataGridView1.DataSource, DataTable).Rows.Clear()
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Pengembalian.Show()
    End Sub
End Class