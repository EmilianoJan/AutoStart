Public Class Form1

    Dim Programas As New List(Of String)
    Dim AIniciar As String
    Dim AppDir As String
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim a As New Process

        Dim proceso As New ProcessStartInfo
        With proceso
            .Arguments = ""
            .FileName = "c:\Users\Public\Desktop\Arduino.lnk"

        End With
        a.StartInfo = proceso
        a.Start()
        'a.WaitForExit()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckProgramas()
        CargarTiempo()
    End Sub

    Private Sub CargarTiempo()
        AppDir = AppDomain.CurrentDomain.BaseDirectory 'System.Reflection.Assembly.GetExecutingAssembly().Location
        If My.Computer.FileSystem.FileExists(AppDir & "\gua.Date") = True Then
            Dim valor As String = My.Computer.FileSystem.ReadAllText(AppDir & "\gua.Date")
            TextBox1.Text = valor
        End If
    End Sub

    Private Sub CheckProgramas()
        Dim dire() As String = My.Computer.FileSystem.GetFiles("c:\Users\Public\Desktop").ToArray

        For Each ele In dire
            If Strings.Replace(ele, "desktop.ini", "") = ele Then
                Dim boton As New Button
                Dim texto As String = Strings.Replace(ele, "c:\Users\Public\Desktop\", "")
                texto = Strings.Mid(texto, 1, Strings.Len(texto) - 4)

                Dim imagen As Bitmap = System.Drawing.Icon.ExtractAssociatedIcon(ele).ToBitmap

                With boton
                    .Text = texto
                    .Height = 60
                    .Width = 120
                    .Visible = True
                    .Image = imagen
                    .TextAlign = ContentAlignment.BottomCenter
                    .ImageAlign = ContentAlignment.TopCenter
                    .Tag = Programas.Count
                End With
                AddHandler boton.Click, AddressOf ClickBotones
                FlowLayoutPanel1.Controls.Add(boton)
                Programas.Add(ele)
            End If
        Next
    End Sub

    Private Sub ClickBotones(sender As Object, e As EventArgs)
        Dim boto As Button = sender
        AIniciar = Programas(boto.Tag)
        My.Computer.FileSystem.WriteAllText(AppDir & "\gua.Date", TextBox1.Text, False)
        Timer1.Interval = Val(TextBox1.Text) * 1000
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Dim a As New Process
        Dim proceso As New ProcessStartInfo
        With proceso
            .Arguments = ""
            .FileName = AIniciar
        End With
        a.StartInfo = proceso
        a.Start()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class
