Public Class Form1
    Dim G As Graphics, x1 As Integer, y1 As Integer, x As Integer, y As Integer, _
    b1 As Integer, s1 As Integer, Sh As Boolean, B As Boolean, Ctrl As Boolean, _
    nx As Integer, ny As Integer, pnn As New Pen(Color.White, 2)
    Dim Rubber As Integer = 5
    Public Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Shift = True And B = False Then : Sh = True : b1 = y : s1 = x
        ElseIf e.Control = True Then : AutoScroll = False : Ctrl = True : End If
    End Sub
    Private Sub Form1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
        G.DrawString(e.KeyChar, Font, Brushes.White, x1 - 7.5, y1 - 12.5) : PictureBox1.Refresh()
        If e.KeyChar = "J" Or e.KeyChar = "L" Then x1 = x1 + 7.5 Else If e.KeyChar = "I" Then x1 = x1 + 5 Else If e.KeyChar = "W" Or e.KeyChar = "M" Then x1 = x1 + 12.5 Else x1 = x1 + 10
    End Sub
    Private Sub Form1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        AutoScroll = True : Sh = False : Ctrl = False
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PictureBox1.Image = New Bitmap(PictureBox1.Width, PictureBox1.Height)
        G = Graphics.FromImage(PictureBox1.Image)
        pnn.DashStyle = Drawing2D.DashStyle.DashDotDot
    End Sub

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        x1 = e.X : y1 = e.Y
    End Sub
    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        If Sh = False Or Sh And e.Button = False Then : x = e.X : y = e.Y
        Else
            If y <> e.Y Then : x = s1 : y = e.Y
            ElseIf x <> e.X Then : y = b1 : x = e.X : End If
        End If
        B = False
        If e.Button = MouseButtons.Left Then : B = True
            If Sh = False Then
                If Math.Abs(x - nx) > 8 Then
                    G.DrawLine(pnn, nx, ny, x, y)
                ElseIf Math.Abs(y - ny) > 8 Then
                    G.DrawLine(pnn, nx, ny, x, y)
                Else
                    G.DrawLine(Pens.White, x, y, x - 2, y - 2)
                End If
            Else
                Dim n11 = CInt(Math.Ceiling(Rnd() * 4)) + 1
                If x > nx And y = b1 Then
                    For i = nx To x Step n11
                        G.DrawLine(Pens.White, i, y, i - 2, y - 2)
                        n11 = CInt(Math.Ceiling(Rnd() * 4)) + 1
                    Next
                ElseIf x < nx And y = b1 Then
                    For i = x To nx Step n11
                        G.DrawLine(Pens.White, i, y, i - 2, y - 2)
                        n11 = CInt(Math.Ceiling(Rnd() * 4)) + 1
                    Next
                ElseIf y > ny And x = s1 Then
                    For i = ny To y Step n11
                        G.DrawLine(Pens.White, x, i, x - 2, i - 2)
                        n11 = CInt(Math.Ceiling(Rnd() * 4)) + 1
                    Next
                ElseIf y < ny And x = s1 Then
                    For i = y To ny Step n11
                        G.DrawLine(Pens.White, x, i, x - 2, i - 2)
                        n11 = CInt(Math.Ceiling(Rnd() * 4)) + 1
                    Next
                End If
            End If
                PictureBox1.Refresh()
            ElseIf e.Button = MouseButtons.Right Then
            G.DrawRectangle(New Pen(PictureBox1.BackColor, Rubber), x - Rubber, y - Rubber, Rubber, Rubber) : PictureBox1.Refresh()
            End If
        nx = x : ny = y
    End Sub
    Private Sub Form1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If Ctrl = True Then
            Dim F = 0, T = PictureBox1.Image
            If e.Delta > 0 Then F = 1 Else F = -1
            PictureBox1.Height = PictureBox1.Height + (100 * F)
            PictureBox1.Width = PictureBox1.Width + (20 * F)
            PictureBox1.Image = New Bitmap(PictureBox1.Width, PictureBox1.Height)
            G = Graphics.FromImage(PictureBox1.Image) : G.DrawImage(T, 0, 0)
            PictureBox1.Refresh()
        End If
    End Sub

    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        PictureBox1.Visible = False
        PictureBox2.Dock = DockStyle.Fill
        PictureBox2.Visible = True
        PictureBox2.Image = PictureBox1.Image
    End Sub
    Private Sub PictureBox2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox2.DoubleClick
        PictureBox2.Visible = False
        PictureBox2.Dock = DockStyle.None
        PictureBox1.Visible = True
    End Sub
End Class
