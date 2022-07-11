Imports System.Drawing.Drawing2D
Imports ToolsBox.Controller
Imports System.ComponentModel


Class ThemedTextbox : Inherits TextBox
    Public State As MouseState = MouseState.None
    Public Pal As Palette
    Protected Overrides Sub OnPaintBackground(ByVal e As System.Windows.Forms.PaintEventArgs)
    End Sub
    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over
        Invalidate()
    End Sub
    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub
    Sub New()
        MyBase.New()
        MinimumSize = New Size(20, 20)
        ForeColor = Color.FromArgb(146, 149, 152)
        Font = New Font("Segoe UI", 10.0F)
        DoubleBuffered = True
        Pal = New Palette
        Pal.ColHighest = Color.FromArgb(100, 110, 120)
        Pal.ColHigh = Color.FromArgb(65, 70, 75)
        Pal.ColMed = Color.FromArgb(40, 42, 45)
        Pal.ColDim = Color.FromArgb(30, 32, 35)
        Pal.ColDark = Color.FromArgb(15, 17, 19)
        BackColor = Pal.ColDim
    End Sub
End Class
Class PVButton : Inherits ThemedControl
    Sub New()
        MyBase.New()
        Font = New Font("Trebuchet MS", 10.0F)
    End Sub
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        MyBase.OnPaint(e)
        Try
            BackColor = Me.Parent.BackColor
        Catch ex As Exception : End Try
        G.Clear(BackColor)
        G.SmoothingMode = SmoothingMode.HighQuality

        '| Drawing the button
        Dim ButtonRect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim Roundness As Integer = 4
        If Size.Width <= 30 And Size.Height <= 30 Then
            Roundness = 2
        End If
        Dim ButtonPath As GraphicsPath = RoundRect(ButtonRect, Roundness)
        Dim ButtonHighlightPath As GraphicsPath = RoundRect(New Rectangle(ButtonRect.X, ButtonRect.Y + 1, ButtonRect.Width, ButtonRect.Height - 2), 4)
        Select Case State
            Case MouseState.None
                G.FillPath(New SolidBrush(Color.FromArgb(100, Pal.ColDim)), ButtonPath)
                G.FillPath(New SolidBrush(Pal.ColDim), ButtonPath)
                FillGradientBeam(G, Color.FromArgb(20, Color.Black), Color.FromArgb(20, Pal.ColHighest), ButtonRect, GradientAlignment.Vertical)
            Case MouseState.Over
                G.FillPath(New SolidBrush(Color.FromArgb(255, Pal.ColDim)), ButtonPath)
                G.FillPath(New SolidBrush(Color.FromArgb(Pal.ColDim.R + 10, Pal.ColDim.G + 10, Pal.ColDim.B + 10)), ButtonPath)
                FillGradientBeam(G, Color.FromArgb(20, Color.Black), Color.FromArgb(20, Pal.ColHighest), ButtonRect, GradientAlignment.Vertical)
            Case MouseState.Down
                G.FillPath(New SolidBrush(Color.FromArgb(70, Pal.ColDim)), ButtonPath)
                G.FillPath(New SolidBrush(Pal.ColDim), ButtonPath)
                G.FillPath(New SolidBrush(Color.FromArgb(50, Pal.ColDark)), ButtonPath)
                FillGradientBeam(G, Color.FromArgb(35, Color.Black), Color.FromArgb(14, Pal.ColHighest), ButtonRect, GradientAlignment.Vertical)
        End Select
        If State = MouseState.Down Then
            ButtonHighlightPath = RoundRect(New Rectangle(ButtonRect.X, ButtonRect.Y + 1, ButtonRect.Width, ButtonRect.Height - 1), 4)
            G.DrawPath(New Pen(Color.FromArgb(100, Color.Black), 3), ButtonHighlightPath)
            DrawTextWithShadow(G, ButtonRect, Text, Font, HorizontalAlignment.Center, Color.FromArgb(200, Pal.ColHighest), Color.Black)

        Else
            G.DrawPath(New Pen(Color.FromArgb(60, Pal.ColHighest)), ButtonHighlightPath)
            DrawTextWithShadow(G, ButtonRect, Text, Font, HorizontalAlignment.Center, Color.FromArgb(120, Color.WhiteSmoke), Color.Black)

        End If
        G.DrawPath(Pens.Black, ButtonPath)

    End Sub
End Class
Class PVTextbox : Inherits ThemedTextbox
    Public Property BorderColor As Color = Color.FromArgb(200, Pal.ColHighest)
    Public Property InteriorColor As Color = Color.FromArgb(150, Color.WhiteSmoke)
    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)

        BackColor = Pal.ColDark
        BorderStyle = Windows.Forms.BorderStyle.None
        Multiline = True
        Font = New Font("Trebuchet MS", 10.0F)
    End Sub
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        MyBase.OnPaint(e)

        If Not Multiline Then
            Height = 21
        End If

        G.Clear(BackColor)
        G.SmoothingMode = SmoothingMode.HighQuality

        Dim BorderRect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim BorderInnerRect As New Rectangle(1, 1, Width - 3, Height - 3)
        Dim ButtonRect As New Rectangle(5, 5, Width - 11, Height - 11)


        '| Drawing the textbox
        Dim Out1Path As GraphicsPath = RoundRect(BorderRect, 3)
        Dim Out2Path As GraphicsPath = RoundRect(BorderInnerRect, 5)
        Dim Out2LGB As New LinearGradientBrush(New Point(0, 0), New Point(0, Height), Color.FromArgb(150, Color.Black), Color.FromArgb(60, Pal.ColDim))
        G.FillRectangle(New SolidBrush(Color.FromArgb(35, Color.Black)), BorderInnerRect)
        G.DrawPath(New Pen(Out2LGB, 3), Out2Path)
        G.DrawPath(New Pen(Color.FromArgb(100, Pal.ColHighest)), Out1Path)


        'My D.drawText method seemed to cause issues.
        G.DrawString(Text, Font, New SolidBrush(InteriorColor), New Point(3, 2))
    End Sub
End Class

<ToolboxBitmap(GetType(NumericUpDown), "NumericUpDown")> _
Public Class NumericUpDown_Dark
    Inherits ThemedControl
    Private WithEvents TxtBox As New PVTextbox
    Private WithEvents BtnUp As New PVButton
    Private WithEvents BtnDown As New PVButton
    Public Property ButtonChange As Integer = 1
    Public Property Minimum As Integer = 1
    Public Property Maximum As Integer = 100

    '| Used the MouseDown event because it doesn't use a delay like the click one does.
    Protected Sub BtnUp_Down(ByVal Sender As Object, ByVal e As System.EventArgs) Handles BtnUp.MouseDown
        Value = Value + ButtonChange
    End Sub
    Protected Sub BtnDown_Down(ByVal Sender As Object, ByVal e As System.EventArgs) Handles BtnDown.MouseDown
        Value = Value - ButtonChange
    End Sub

    <Category("ToolsBox Herramienta")> _
    Public Property Value() As Integer
        Get
            Dim Ret As Integer = 0
            Try : Ret = CInt(TxtBox.Text) : Catch ex As Exception : End Try
            Return Ret
        End Get
        Set(ByVal val As Integer)
            If val <= Maximum And val >= Minimum Then
                TxtBox.Text = val
                Invalidate()
            End If
        End Set
    End Property
    Sub New()
        MyBase.New()
        Size = New Point(300, 300)
        Font = New Font("Trebuchet MS", 10.0F)
        TxtBox.Text = 0
        Controls.Add(TxtBox)
        Controls.Add(BtnUp)
        Controls.Add(BtnDown)
    End Sub
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim G As Graphics = e.Graphics
        MyBase.OnPaint(e)
        G.Clear(Me.Parent.BackColor)
        Height = 30
        BtnUp.MinimumSize = New Size(15, 15)
        BtnUp.Size = New Size(10, 10)
        BtnUp.Location = New Point(Size.Width - BtnUp.Width, 0)
        BtnUp.Font = New Font("Trebuchet MS", 10.0F)
        BtnUp.Text = "ᴧ"
        BtnDown.MinimumSize = New Size(15, 15)
        BtnDown.Size = New Size(10, 10)
        BtnDown.Location = New Point(Size.Width - BtnUp.Width, BtnUp.Size.Height)
        BtnDown.Font = New Font("Trebuchet MS", 10.0F, FontStyle.Bold)
        BtnDown.Text = "v"
        TxtBox.Location = New Point(0, 0)
        TxtBox.Multiline = True
        TxtBox.Height = 30
        TxtBox.Width = BtnUp.Location.X - 2
    End Sub
End Class




