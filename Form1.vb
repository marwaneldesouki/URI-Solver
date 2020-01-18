Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class Form1
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        WindowState = FormWindowState.Minimized
    End Sub
    Function check(str As String)
        Dim httpWebRequest As HttpWebRequest = CType(WebRequest.Create("https://mamun-amin.blogspot.com/p/uri.html"), HttpWebRequest)
        httpWebRequest.Proxy = Nothing
        Using streamReader As StreamReader = New StreamReader(httpWebRequest.GetResponse().GetResponseStream())
            Dim prompt As String = streamReader.ReadToEnd()
            If (prompt.Contains(str.ToString)) Then
                Label5.ForeColor = Color.Green
                Label5.Text = "Status: Your Code is Founded"
                Me.Refresh()
                Thread.Sleep(200)
                Label5.ForeColor = Color.Red
                Label5.Text = "Status: Wait> Finding The Answer"
                grab(str)
            Else
                MessageBox.Show("Your Code Is Not exsist in our DB" + vbCrLf + "Plz for Your code again", "URI Solver")
                Label5.Text = "Status: Not Found"

            End If

        End Using
    End Function

    Function grab(strx As String)
        Dim httpWebRequest As HttpWebRequest = CType(WebRequest.Create("https://mamun-amin.blogspot.com/search?q=" + strx), HttpWebRequest)
        httpWebRequest.Proxy = Nothing
        Using streamReader As StreamReader = New StreamReader(httpWebRequest.GetResponse().GetResponseStream())
            Dim prompt As String = streamReader.ReadToEnd()
            regx(prompt)

        End Using
    End Function
    Function regx(xstr As String)

        Dim r As New Regex("<a href='(.*?)'>Solution of URI " + TextBox1.Text)
        Dim matchess As MatchCollection = r.Matches(xstr)

        For Each itemcode As Match In matchess
            ans(itemcode.Groups(1).Value)
        Next





    End Function
    Function ans(str As String)
        Dim httpWebRequest As HttpWebRequest = CType(WebRequest.Create(str), HttpWebRequest)
        httpWebRequest.Proxy = Nothing
        Using streamReader As StreamReader = New StreamReader(httpWebRequest.GetResponse().GetResponseStream())
            Dim prompt As String = streamReader.ReadToEnd()
            regxans(prompt.ToString)

        End Using
    End Function
    Dim zz As String
    Function regxans(str As String)
        Dim r As New Regex("<pre class=""c"" name=""code"">([^<]*)</pre>")
        Dim matchess As MatchCollection = r.Matches(str)

        For Each itemcode As Match In matchess
            zz = itemcode.Groups(1).Value


        Next
        zz = zz.Replace("amp;", "")
        zz = zz.Replace("&lt;", "<")
        zz = zz.Replace("&gt;", ">")
        zz = zz.Replace("/**Bismillahir Rahmanir Rahim.**/", "")


        TextBox2.Text = zz
        Label5.Text = "Status: Complete"
        Label5.ForeColor = Color.Green
    End Function
    Dim xx As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox2.Clear()

        If (TextBox1.Text = "" Or TextBox1.Text = "Problem ID?") Then
            MessageBox.Show("Fill The Box Plz")
        Else
            Label5.ForeColor = Color.Red
            Label5.Text = "Status: Wait >Checking Your Code"


            check(TextBox1.Text)
        End If


    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        End
    End Sub
    Private isMouseDown As Boolean
    Private mouseOffset As Point
    Private Sub Label1_MouseDown(sender As Object, e As MouseEventArgs) Handles Label1.MouseDown
        Dim flag As Boolean = e.Button = MouseButtons.Left
        If flag Then

            Me.mouseOffset = New Point(0 - e.X, 0 - e.Y)
            Me.isMouseDown = True
        End If
    End Sub

    Private Sub Label1_MouseMove(sender As Object, e As MouseEventArgs) Handles Label1.MouseMove
        Dim flag As Boolean = Me.isMouseDown
        If flag Then
            Dim mousePosition As Point = Control.MousePosition
            mousePosition.Offset(Me.mouseOffset.X, Me.mouseOffset.Y)
            Me.Location = mousePosition
        End If
    End Sub

    Private Sub Label1_MouseUp(sender As Object, e As MouseEventArgs) Handles Label1.MouseUp
        Dim flag As Boolean = e.Button = MouseButtons.Left
        If flag Then
            Me.isMouseDown = False
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox2.Clear()
        TextBox1.Text = "Problem ID?"
        Label5.Text = "Status: ..."
        Label5.ForeColor = Color.Silver
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MessageBox.Show("Coded by Deso" + vbCr + "IG: Marwaneldesouki" + vbCrLf + "GITHUB: Marwaneldesouki", "About")
    End Sub
End Class
