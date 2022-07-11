Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Runtime.InteropServices

<ToolboxBitmap(GetType(Switch), "Switch")> _
Public MustInherit Class ThemeControl
    Inherits Control

#Region " Initialization "

    Protected N As Graphics, K As Bitmap
    Sub New()
        SetStyle(DirectCast(139270, ControlStyles), True)
        K = New Bitmap(1, 1)
        N = Graphics.FromImage(K)
    End Sub

    Sub AllowTransparent()
        SetStyle(ControlStyles.Opaque, False)
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
    End Sub

    Overrides Property Text() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal v As String)
            MyBase.Text = v
            Invalidate()
        End Set
    End Property
#End Region

#Region " Mouse Handling "

    Protected Enum State As Byte
        MouseNone = 0
        MouseOver = 1
        MouseDown = 2
    End Enum

    Protected MouseState As State
    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        ChangeMouseState(State.MouseNone)
        MyBase.OnMouseLeave(e)
    End Sub
    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        ChangeMouseState(State.MouseOver)
        MyBase.OnMouseEnter(e)
    End Sub
    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        ChangeMouseState(State.MouseOver)
        MyBase.OnMouseUp(e)
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then ChangeMouseState(State.MouseDown)
        MyBase.OnMouseDown(e)
    End Sub
    Private Sub ChangeMouseState(ByVal e As State)
        MouseState = e
        Invalidate()
    End Sub

#End Region

#Region " Convienence "

    MustOverride Sub PaintHook()
    Protected NotOverridable Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If Width = 0 OrElse Height = 0 Then Return
        PaintHook()
        e.Graphics.DrawImage(K, 0, 0)
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        If Not Width = 0 AndAlso Not Height = 0 Then
            K = New Bitmap(Width, Height)
            N = Graphics.FromImage(K)
            Invalidate()
        End If
        MyBase.OnSizeChanged(e)
    End Sub

    Private _NoRounding As Boolean
    Property NoRounding() As Boolean
        Get
            Return _NoRounding
        End Get
        Set(ByVal v As Boolean)
            _NoRounding = v
            Invalidate()
        End Set
    End Property

    Private _Image As Image
    Property Image() As Image
        Get
            Return _Image
        End Get
        Set(ByVal value As Image)
            _Image = value
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    ReadOnly Property ImageWidth() As Integer
        Get
            If _Image Is Nothing Then Return 0
            Return _Image.Width
        End Get
    End Property

    <Category("ToolsBox Herramienta")> _
    ReadOnly Property ImageTop() As Integer
        Get
            If _Image Is Nothing Then Return 0
            Return Height \ 2 - _Image.Height \ 2
        End Get
    End Property

    Private _Size As Size
    Private _Rectangle As Rectangle
    Private _Gradient As LinearGradientBrush
    Private _Brush As SolidBrush

    Protected Sub DrawCorners(ByVal c As Color, ByVal rect As Rectangle)
        If _NoRounding Then Return

        K.SetPixel(rect.X, rect.Y, c)
        K.SetPixel(rect.X + (rect.Width - 1), rect.Y, c)
        K.SetPixel(rect.X, rect.Y + (rect.Height - 1), c)
        K.SetPixel(rect.X + (rect.Width - 1), rect.Y + (rect.Height - 1), c)
    End Sub
    Protected Sub DrawBorders(ByVal p1 As Pen, ByVal p2 As Pen, ByVal rect As Rectangle)
        N.DrawRectangle(p1, rect.X, rect.Y, rect.Width - 1, rect.Height - 1)
        N.DrawRectangle(p2, rect.X + 1, rect.Y + 1, rect.Width - 3, rect.Height - 3)
    End Sub
    Protected Sub DrawText(ByVal a As HorizontalAlignment, ByVal c As Color, ByVal x As Integer)
        DrawText(a, c, x, 0)
    End Sub
    Protected Sub DrawText(ByVal a As HorizontalAlignment, ByVal c As Color, ByVal x As Integer, ByVal y As Integer)
        If String.IsNullOrEmpty(Text) Then Return
        _Size = N.MeasureString(Text, Font).ToSize
        _Brush = New SolidBrush(c)

        Select Case a
            Case HorizontalAlignment.Left
                N.DrawString(Text, Font, _Brush, x, Height \ 2 - _Size.Height \ 2 + y)
            Case HorizontalAlignment.Right
                N.DrawString(Text, Font, _Brush, Width - _Size.Width - x, Height \ 2 - _Size.Height \ 2 + y)
            Case HorizontalAlignment.Center
                N.DrawString(Text, Font, _Brush, Width \ 2 - _Size.Width \ 2 + x, Height \ 2 - _Size.Height \ 2 + y)
        End Select
    End Sub
    Protected Sub DrawIcon(ByVal a As HorizontalAlignment, ByVal x As Integer)
        DrawIcon(a, x, 0)
    End Sub
    Protected Sub DrawIcon(ByVal a As HorizontalAlignment, ByVal x As Integer, ByVal y As Integer)
        If _Image Is Nothing Then Return
        Select Case a
            Case HorizontalAlignment.Left
                N.DrawImage(_Image, x, Height \ 2 - _Image.Height \ 2 + y)
            Case HorizontalAlignment.Right
                N.DrawImage(_Image, Width - _Image.Width - x, Height \ 2 - _Image.Height \ 2 + y)
            Case HorizontalAlignment.Center
                N.DrawImage(_Image, Width \ 2 - _Image.Width \ 2, Height \ 2 - _Image.Height \ 2)
        End Select
    End Sub
    Protected Sub DrawGradient(ByVal c1 As Color, ByVal c2 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal angle As Single)
        _Rectangle = New Rectangle(x, y, width, height)
        _Gradient = New LinearGradientBrush(_Rectangle, c1, c2, angle)
        N.FillRectangle(_Gradient, _Rectangle)
    End Sub
#End Region

End Class

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.CheckBox))> _
Public Class Switch_Light
    Inherits ThemeControl

#Region " Properties "
    Private _CheckedState As Boolean
    <Category("ToolsBox Herramienta")> _
    Public Property CheckedState() As Boolean
        Get
            Return _CheckedState
        End Get
        Set(ByVal v As Boolean)
            _CheckedState = v
            Invalidate()
        End Set
    End Property
#End Region

    Sub New()
        Size = New Size(90, 15)
        MinimumSize = New Size(16, 16)
        MaximumSize = New Size(600, 16)
        CheckedState = False
    End Sub

    Public Overrides Sub PaintHook()
        N.Clear(Me.Parent.BackColor)
        Me.ForeColor = Me.Parent.ForeColor
        Dim hb As New HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.FromArgb(20, Color.White), Color.Transparent)
        DrawGradient(Color.FromArgb(196, 196, 196), Color.FromArgb(230, 230, 230), 0, 0, 30, 15, 270S)
        DrawGradient(Color.FromArgb(35, Color.Black), Color.Transparent, 0, 0, 15, 15, 180S)
        DrawGradient(Color.FromArgb(35, Color.Black), Color.Transparent, 15, 0, 16, 15, 0S)
        N.FillRectangle(hb, 1, 1, Width, Height)
        Select Case CheckedState
            Case True
                DrawGradient(Color.FromArgb(62, 62, 62), Color.FromArgb(4, 128, 7), 0, 0, 13, 15, 90S)
                DrawGradient(Color.FromArgb(4, 128, 7), Color.FromArgb(17, 196, 21), 3, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(17, 196, 21), Color.FromArgb(4, 128, 7), 4, 4, 7, 7, 90S)
                N.DrawRectangle(Pens.LightGray, New Rectangle(0, 0, 13, 14))
            Case False
                DrawGradient(Color.FromArgb(160, 0, 0), Color.FromArgb(109, 16, 16), 15, 0, 13, 15, 90S)
                DrawGradient(Color.FromArgb(109, 16, 16), Color.FromArgb(212, 20, 20), 18, 3, 9, 9, 90S)
                DrawGradient(Color.FromArgb(212, 20, 20), Color.FromArgb(109, 16, 16), 19, 4, 7, 7, 90S)
                N.DrawRectangle(Pens.LightGray, New Rectangle(15, 0, 13, 14))
        End Select

        DrawBorders(Pens.Gray, Pens.LightGray, New Rectangle(0, 0, 30, 15))
        DrawText(HorizontalAlignment.Left, Me.ForeColor, 32, 0)
    End Sub

    Sub changeCheck() Handles Me.MouseDown
        Select Case CheckedState
            Case True
                CheckedState = False
            Case False
                CheckedState = True
        End Select
    End Sub
End Class
