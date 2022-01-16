Imports MySql.Data.MySqlClient
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32
Public Class Pengembalian

    Public Shared totalDurasi As Integer
    Public Shared biayaTambahan As Integer
    Public Shared merek As String
    Public Shared durasiAwal As Integer
    Public Shared sewaa As Integer
    Public Shared idPengembalian As Integer
    Public Shared totalsewaa As Integer
    Public Shared sewatotal As Integer

    Private Sub Pengembalian_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        connect()
        Try
            merek = Sewa.DataGridView1.SelectedRows(0).Cells(2).Value.ToString()
            sqlQuery = "select hargaSewaMobil from dbpenyewaanmobil.mobil where merekMobil='" & merek & "'"
            sqlCmd = New MySqlCommand(sqlQuery, sqlConn)
            sqlDr = sqlCmd.ExecuteReader
            While sqlDr.Read
                sewaa = sqlDr("hargaSewaMobil")
            End While
        Catch ex As Exception

        End Try
        sqlDr.Close()

        durasiAwal = Integer.Parse(Sewa.DataGridView1.SelectedRows(0).Cells(4).Value)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        totalDurasi = Integer.Parse(TextBox1.Text)
        biayaTambahan = (totalDurasi - durasiAwal) * sewaa
        TextBox2.Text = biayaTambahan
        sewatotal = Sewa.totalsewa
        totalsewaa = sewatotal + biayaTambahan
        idPengembalian = Sewa.idS
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If biayaTambahan <= 0 Then
            Try
                connect()
                'sqlQuery = "update penyewa set " & "nikPenyewa='" & TextBox1.Text & "', " & "namaPenyewa='" & TextBox2.Text & "', " & "alamatPenyewa='" & TextBox3.Text & "' " &
                '           "WHERE idPenyewa='" & idS & "'"

                'sqlQuery = "update sewa set " & "penyewa='" & Penyewa & "'," & "statusSewa='" & status & "'," & "rencanaPinjam='" & durasi & "'," &
                '                                   "tanggalPinjam='" & masuk & "'," & " tanggalKembali='" & keluar & "'," & " totalSewa='" & totalsewa & "'," &
                '                                   " totalBayar='" & totalbayar & "'," & " biayaKelebihan='" & kembalian & "'," & "merekSewa='" & merek & "'" &
                '                                   "WHERE idSewa='" & idS & "'"

                sqlQuery = "update sewa set " & " totalSewa='" & totalsewaa & "'," & "statusSewa= 'Tidak Sedang disewa' ," & " biayaKelebihan= 0 " &
                                                "WHERE idSewa='" & Sewa.idS & "'"


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
        Else
            Try
                connect()
                'sqlQuery = "update penyewa set " & "nikPenyewa='" & TextBox1.Text & "', " & "namaPenyewa='" & TextBox2.Text & "', " & "alamatPenyewa='" & TextBox3.Text & "' " &
                '           "WHERE idPenyewa='" & idS & "'"

                'sqlQuery = "update sewa set " & "penyewa='" & Penyewa & "'," & "statusSewa='" & status & "'," & "rencanaPinjam='" & durasi & "'," &
                '                                   "tanggalPinjam='" & masuk & "'," & " tanggalKembali='" & keluar & "'," & " totalSewa='" & totalsewa & "'," &
                '                                   " totalBayar='" & totalbayar & "'," & " biayaKelebihan='" & kembalian & "'," & "merekSewa='" & merek & "'" &
                '                                   "WHERE idSewa='" & idS & "'"

                sqlQuery = "update sewa set " & "statusSewa= 'Tidak Sedang disewa' ," & " biayaKelebihan='" & biayaTambahan & "'" &
                                                "WHERE idSewa='" & idPengembalian & "'"


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

        End If
        Call CType(Sewa.DataGridView1.DataSource, DataTable).Rows.Clear()
        Sewa.updateTable()
        Me.Close()
    End Sub
End Class