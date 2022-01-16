Imports MySql.Data.MySqlClient
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32
Public Class Mobil

    'perpustakaan.Koleksi.namaProperty = TextBoxNama.Text.ToString()
    'perpustakaan.Koleksi.penerbitProperty = TextBoxPenerbit.Text.ToString()
    'perpustakaan.Koleksi.lokasiProperty = TextBoxLokasi.Text.ToString()
    'perpustakaan.Koleksi.TahunProperty = Integer.Parse(TextBoxTahun.Text)
    'perpustakaan.Koleksi.JenisKoleksiProperty = ComboBox1.SelectedItem().ToString()
    'perpustakaan.Koleksi.deskripsiProperty = RichTextBox1.Text.ToString()

    'perpustakaan.Koleksi.stockProperty = Integer.Parse(TextBoxStock.Text)
    'perpustakaan.Koleksi.tanggalProperty = DateTimePicker1.Value.ToShortDateString()

    Public Shared id As Integer
    Public Shared jenis As String
    Public Shared merek As String
    Public Shared jumlah As Integer
    Public Shared harga As Integer
    Public Shared tahun As Integer
    Public Shared status As String
    Public Shadows pickoleksi As String
    'Dim id As Integer = TextBox1.Text
    'Dim jenis As String = ComboBox1.SelectedItem().ToString()
    'Dim merek As String = TextBox2.Text.ToString()
    'Dim jumlah As Integer = Integer.Parse(TextBox3.Text)
    'Dim harga As Integer = Integer.Parse(TextBox4.Text)
    'Dim tahun As Integer = Integer.Parse(TextBox5.Text)
    'Dim status As String = ComboBox2.SelectedItem().ToString()
    'Dim pickoleksi As String

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        connect()

        Try
            ofd = New OpenFileDialog()

            ofd.Title = "Open"
            ofd.Filter = "All Format|*.*"

            If ofd.ShowDialog() = DialogResult.OK Then
                imgPath = ofd.FileName
                PictureBox1.ImageLocation = imgPath
                pickoleksi = imgPath

                PictureBox1.Load(pickoleksi)
                PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            End If

            ofd = Nothing
        Catch ex As Exception
            MsgBox(ex.Message.ToString())
        End Try


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        sqlDr.Close()
        id = Integer.Parse(TextBox1.Text)
        jenis = ComboBox1.SelectedItem()

        Try
            Dim memoryStream As New System.IO.MemoryStream()
            PictureBox1.Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg)
            arrImage = memoryStream.GetBuffer()
            Dim filesize As UInt32
            filesize = memoryStream.Length
            memoryStream.Close()

            connect()

            sqlQuery = "insert into mobil(idMobil, jenisMobil, merekMobil, jumlahMobil, hargaSewaMobil, tahunPembuatanMobil, statusSewaMobil, fotoMobil) 
                        value('" & id & "','" & jenis & "','" & merek &
                            "','" & jumlah & "','" & harga & "','" & tahun &
                            "','" & status & "','" & pickoleksi & "')"

            'sqlCmd.Parameters.AddWithValue("@img", arrImage)

            Dim x As Integer
            x = sqlCmd.ExecuteNonQuery()
            If x > 0 Then
                MessageBox.Show("berhasil")
            Else
                MessageBox.Show("gagal")
            End If

            sqlConn.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class