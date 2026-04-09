Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Drawing
Imports ToolsBox.Controller

<ToolboxBitmap(GetType(FormAzenis), "Purple.ico")>
Public Class FormAzenis : Inherits ContainerControl

    ' Public D As New DrawUtils
    Protected Drag As Boolean = True
    Public State As MouseState = MouseState.None
    Protected TopCap As Boolean = False
    Protected SizeCap As Boolean = False
    Public Pal As Palette
    Protected MouseP As Point = New Point(0, 0)
    Protected TopGrip As Integer
    Protected Overrides Sub OnPaintBackground(ByVal e As System.Windows.Forms.PaintEventArgs)
    End Sub
    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over
        Invalidate()
    End Sub
    Protected Overrides Sub OnHandleDestroyed(ByVal e As EventArgs)
        Try
            Dock = DockStyle.None
            ParentForm.BackColor = Color.FromArgb(240, 240, 240)
            ParentForm.FormBorderStyle = FormBorderStyle.Sizable
            MyBase.OnHandleDestroyed(e)
        Catch ex As Exception
        End Try
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down
        If e.Button = MouseButtons.Left Then
            If New Rectangle(0, 0, Width, TopGrip).Contains(e.Location) Then
                TopCap = True : MouseP = e.Location
            ElseIf Drag And New Rectangle(Width - 15, Height - 15, 15, 15).Contains(e.Location) Then
                SizeCap = True : MouseP = e.Location
            End If
        End If
    End Sub
    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over
        TopCap = False
        If Drag Then
            SizeCap = False
        End If

    End Sub
    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)

        If TopCap Then
            Parent.Location = MousePosition - MouseP
        End If
        If Drag And SizeCap Then
            MouseP = e.Location
            Parent.Size = New Size(MouseP)
            Invalidate()
        End If

    End Sub
    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub
    Sub New()
        MyBase.New()
        MinimumSize = New Size(20, 20)
        ForeColor = Color.FromArgb(146, 149, 152)
        Font = New Font("Trebuchet MS", 10.0F)
        DoubleBuffered = True
        Pal = New Palette
        Pal.ColHighest = Color.FromArgb(100, 105, 110)
        Pal.ColHigh = Color.FromArgb(65, 67, 69)
        Pal.ColMed = Color.FromArgb(40, 42, 44)
        Pal.ColDim = Color.FromArgb(30, 32, 34)
        Pal.ColDark = Color.FromArgb(15, 16, 17)
        BackColor = Pal.ColDim

        MinimumSize = New Size(305, 150)
        Dock = DockStyle.Fill
        TopGrip = 30
        Font = New Font("Segoe UI", 10.0F)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        MyBase.OnPaint(e)
        Try
            Me.ParentForm.TransparencyKey = Color.Fuchsia
            Me.ParentForm.MinimumSize = MinimumSize
            If Not Me.ParentForm.FormBorderStyle = FormBorderStyle.None Then
                Me.ParentForm.FormBorderStyle = FormBorderStyle.None
            End If
        Catch ex As Exception : End Try
        G.Clear(Me.ParentForm.TransparencyKey)

        '|====| Main shape
        Dim MainShape As GraphicsPath = RoundRect(New Rectangle(0, 0, Width - 1, Height - 1), 8)
        Dim DoStuffPath As GraphicsPath = RoundRect(New Rectangle(5, TopGrip + 2, Width - 11, Height - TopGrip - 8), 8)
        G.FillPath(New SolidBrush(Pal.ColHighest), MainShape)
        '| Interior shading (behind all else)
        Dim ColBlend As ColorBlend = New ColorBlend(3)
        Dim BlurScale As Integer = Math.Sqrt((Width * Width) + (Height * Height)) / 10
        ColBlend.Colors = {Color.Transparent, Color.FromArgb(255, Pal.ColDim), Color.FromArgb(255, Pal.ColDim)}
        ColBlend.Positions = {0, 1 / BlurScale, 1}
        '| Looks best when square. Not sure how I would approach scaling for rectangular shapes.
        DrawShadowPath(G, ColBlend, MainShape)
        '| Adds a border inside the light blending on the edges
        G.DrawPath(New Pen(Pal.ColDark), DoStuffPath)
        '|====| Top bar
        G.SmoothingMode = SmoothingMode.HighQuality
        Dim TopPath As GraphicsPath = RoundedTopRect(New Rectangle(0, 0, Width - 2, TopGrip + 3), 8)
        Dim TopPathLGB As New LinearGradientBrush(New Point(0, 0), New Point(0, TopGrip + 5), Pal.ColHighest, Color.Transparent)
        Dim LineLGB As New LinearGradientBrush(New Point(0, 0), New Point(0, TopGrip + 5), Pal.ColHighest, Color.Transparent)
        Dim LineLGBAlt As New LinearGradientBrush(New Point(0, 0), New Point(0, TopGrip + 5), Pal.ColDim, Color.Transparent)
        G.FillPath(TopPathLGB, TopPath)
        G.DrawLine(New Pen(LineLGB), New Point(9, 0), New Point(9, TopGrip + 5))
        G.DrawLine(New Pen(LineLGB), New Point(Width - 10, 0), New Point(Width - 10, TopGrip + 5))
        G.DrawLine(New Pen(Pal.ColDim), New Point(8, 0), New Point(8, TopGrip + 5))
        G.DrawLine(New Pen(Pal.ColDim), New Point(Width - 9, 0), New Point(Width - 9, TopGrip + 5))
        '|====| Top bar - Inner
        Dim BaRct As New Rectangle(15, 4, Width - 31, TopGrip * (4 / 5))
        Dim DP As GraphicsPath = RoundRect(BaRct, 5)
        Dim TextureBrush As New HatchBrush(HatchStyle.DarkDownwardDiagonal, Pal.ColMed, Color.Transparent)
        G.DrawPath(New Pen(Pal.ColDark, 3), DP)
        G.FillPath(New SolidBrush(Pal.ColDark), DP)
        G.FillPath(TextureBrush, DP)
        FillDualGradPath(G, Pal.ColDark, Color.Transparent, BaRct, DP, GradientAlignment.Horizontal)
        G.DrawPath(New Pen(Color.FromArgb(100, Pal.ColHigh)), DP)
        '|====|  Border
        G.SmoothingMode = SmoothingMode.None
        G.DrawPath(New Pen(Pal.ColDim), MainShape)
        DrawTextWithShadow(G, New Rectangle(0, 0, Width - 1, TopGrip), Text, Font, HorizontalAlignment.Center, Pal.ColHighest, Pal.ColDark)
    End Sub
End Class



