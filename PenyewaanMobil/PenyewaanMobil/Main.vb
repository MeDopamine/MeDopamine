Imports MySql.Data.MySqlClient
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.Win32

Public Class Main
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        updateTable()
        hideContent()
        connect()
        sqlCmd.Connection = sqlConn
        sqlCmd.CommandText = "select * from dbpenyewaanmobil.mobil"
        'sqlCmd.CommandText = "SELECT idMobil FROM dbpenyewaanmobil.mobil EXCEPT SELECT fotoMobil FROM dbpenyewaanmobil.mobil"

        sqlDr = sqlCmd.ExecuteReader
        sqlDt.Load(sqlDr)
        sqlDr.Close()
        sqlConn.Close()

        DataGridView1.Columns(0).ReadOnly = True
        DataGridView1.Columns(1).ReadOnly = True
        DataGridView1.Columns(2).ReadOnly = True
        DataGridView1.Columns(3).ReadOnly = True
        DataGridView1.Columns(4).ReadOnly = True


        'Me.MenuStrip1.Visible = False
        'Me.SewaToolStripMenuItem.Visible = False

        'If sqlDt IsNot Nothing AndAlso sqlDt.Rows.Count > 0 Then
        '    'some code
        '    Label1.Text = "Found " & sqlDt.Rows.Count.ToString & " results"
        'Else
        '    'some code
        '    Label1.Text = "Found 0 result"
        'End If

        If sqlDt.Rows.Count > 0 Then
            'some code
            'Label1.Text = "Found " & sqlDt.Rows.Count.ToString & " results"
        Else
            Me.SewaToolStripMenuItem.Visible = False
            'Label1.Text = "Found " & sqlDt.Rows.Count.ToString & " results"
        End If


    End Sub

    Private Sub hideContent()

        Me.MobilToolStripMenuItem.Visible = False
        Me.JenisMobilToolStripMenuItem.Visible = False
        Me.SewaToolStripMenuItem.Visible = False
        Me.PenyewaToolStripMenuItem.Visible = False
        Me.DataGridView1.Visible = False
        Me.Button1.Visible = False
        Me.LOGOUTToolStripMenuItem.Visible = False

    End Sub

    Public Sub showContent()

        Me.MobilToolStripMenuItem.Visible = True
        Me.JenisMobilToolStripMenuItem.Visible = True
        Me.PenyewaToolStripMenuItem.Visible = True
        Me.DataGridView1.Visible = True
        Me.Button1.Visible = True
        Me.LOGOUTToolStripMenuItem.Visible = True

    End Sub


    'Private Sub updateTable()
    '    connect()

    '    Dim sqlDth As New DataTable

    '    sqlCmd.Connection = sqlConn
    '    sqlCmd.CommandText = "select idMobil as ID, jenisMobil as 'Jenis Mobil', merekMobil as Brand,
    '                          jumlahMobil as Jumlah, hargaSewaMobil as 'Harga Sewa', tahunPembuatanMobil 'Tahun Pembuatan',
    '                          statusSewaMobil as 'Status Sewa' from dbpenyewaanmobil.mobil"
    '    'sqlCmd.CommandText = "SELECT idMobil FROM dbpenyewaanmobil.mobil EXCEPT SELECT fotoMobil FROM dbpenyewaanmobil.mobil"

    '    sqlDr = sqlCmd.ExecuteReader
    '    sqlDth.Load(sqlDr)
    '    sqlDr.Close()
    '    sqlConn.Close()
    '    DataGridView1.DataSource = sqlDth

    'End Sub

    Public Sub updateTable()
        connect()

        Dim sqlDts As New DataTable

        sqlCmd.Connection = sqlConn
        sqlCmd.CommandText = "select penyewa as 'Penyewa', merekSewa as 'Tipe Mobil', statusSewa as 'Status', rencanaPinjam as 'Durasi Sewa', 
                              tanggalPinjam as 'Tanggal Pinjam' 
                              from dbpenyewaanmobil.sewa where statusSewa='Sedang disewa'"
        sqlDr = sqlCmd.ExecuteReader
        sqlDts.Load(sqlDr)
        sqlDr.Close()
        sqlConn.Close()
        DataGridView1.DataSource = sqlDts
    End Sub

    Private Sub MobilToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MobilToolStripMenuItem.Click
        Mobil.Show()
    End Sub

    Private Sub JenisMobilToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles JenisMobilToolStripMenuItem.Click
        JenisMobil.Show()
    End Sub

    Private Sub PenyewaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenyewaToolStripMenuItem.Click
        Penyewa.Show()
    End Sub

    Private Sub SewaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SewaToolStripMenuItem.Click
        Sewa.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        updateTable()
        connect()
        sqlCmd.Connection = sqlConn
        sqlCmd.CommandText = "select * from dbpenyewaanmobil.mobil"
        'sqlCmd.CommandText = "SELECT idMobil FROM dbpenyewaanmobil.mobil EXCEPT SELECT fotoMobil FROM dbpenyewaanmobil.mobil"

        sqlDr = sqlCmd.ExecuteReader
        sqlDt.Load(sqlDr)
        sqlDr.Close()
        sqlConn.Close()

        If sqlDt.Rows.Count > 0 Then
            'some code
            Me.SewaToolStripMenuItem.Visible = True
            'Label1.Text = "Found " & sqlDt.Rows.Count.ToString & " results"
        Else
            Me.SewaToolStripMenuItem.Visible = False
            'Label1.Text = "Found " & sqlDt.Rows.Count.ToString & " results"
        End If
    End Sub

    Private Sub LOGINToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LOGINToolStripMenuItem.Click
        Login.Show()
        Me.Visible = False
    End Sub

    Private Sub LOGOUTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LOGOUTToolStripMenuItem.Click
        Application.Exit()
    End Sub

End Class