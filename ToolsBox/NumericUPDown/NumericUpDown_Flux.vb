Option Strict On

Imports System, System.IO, System.Collections.Generic
Imports System.Drawing, System.Drawing.Drawing2D
Imports System.ComponentModel, System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Drawing.Imaging
Imports System.Reflection
Imports ToolsBox.Controller
Imports ToolsBox.Utils



<ToolboxBitmap(GetType(NumericUpDown), "NumericUpDown")> _
Public Class NumericUpDown_Flux
    Inherits ThemeControl154
    Private pntMouseLocation As Point
    Private _TextBackColor As Color
    Private _TextAlign As HorizontalAlignment = HorizontalAlignment.Left

    <Category("ToolsBox Herramienta")> _
    Public Property TextAlign() As HorizontalAlignment
        Get
            Return _TextAlign
        End Get
        Set(ByVal value As HorizontalAlignment)
            _TextAlign = value
            If Base IsNot Nothing Then
                Base.TextAlign = value
            End If
        End Set
    End Property

    Private _ReadOnly As Boolean
    <Category("ToolsBox Herramienta")> _
    Public Property [ReadOnly]() As Boolean
        Get
            Return _ReadOnly
        End Get
        Set(ByVal value As Boolean)
            _ReadOnly = value
            If Base IsNot Nothing Then
                Base.ReadOnly = value
            End If
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property Value As Integer

        Get
            If MyBase.Text = "" Then
                Return 0
            ElseIf MyBase.Text.StartsWith("InfluxNumericUpDown") Then
                Return 0
            Else
                Return CInt(MyBase.Text)
            End If
        End Get
        Set(ByVal v As Integer)
            MyBase.Text = v.ToString
            If Base IsNot Nothing Then
                If v >= _MinValue And v <= _MaxValue Then
                    Base.Text = v.ToString
                ElseIf v < _MinValue Then
                    Base.Text = _MinValue.ToString
                    Base.Select(Base.Text.Length, 0)
                ElseIf v > _MaxValue Then
                    Base.Text = _MaxValue.ToString
                    Base.Select(Base.Text.Length, 0)
                End If
            End If
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Overrides Property Font As Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As Font)
            MyBase.Font = value
            If Base IsNot Nothing Then
                Base.Font = value
                Base.Location = New Point(3, 5)
                Base.Width = Width - 6

            End If
        End Set
    End Property

    Private _MaxValue As Integer = 100
    <Category("ToolsBox Herramienta")> _
    Public Property MaxValue() As Integer
        Get
            Return _MaxValue
        End Get
        Set(ByVal value As Integer)
            _MaxValue = value
            If Base IsNot Nothing Then
                Dim intBaseValue As Integer = CInt(Base.Text)
                If intBaseValue > value Then
                    Base.Text = value.ToString
                End If
            End If
        End Set
    End Property

    Private _MinValue As Integer = 0
    <Category("ToolsBox Herramienta")> _
    Public Property MinValue() As Integer
        Get
            Return _MinValue
        End Get
        Set(ByVal value As Integer)
            _MinValue = value
            If Base IsNot Nothing Then
                Dim intBaseValue As Integer = CInt(Base.Text)
                If intBaseValue < value Then
                    Base.Text = value.ToString
                End If
            End If
        End Set
    End Property

    Private _ForeColor As Color = Color.FromArgb(229, 229, 229)
    <Category("ToolsBox Herramienta")> _
    Public Property ForeColorText() As Color
        Get
            Return _ForeColor
        End Get
        Set(ByVal value As Color)
            _ForeColor = value
        End Set
    End Property

    Private _RoundedBorder As Integer = 0
    <Category("ToolsBox Herramienta")> _
    Public Property BorderRounded() As Integer
        Get
            Return _RoundedBorder
        End Get
        Set(ByVal value As Integer)
            If value > 50 Then _RoundedBorder = 50 Else _RoundedBorder = value
        End Set
    End Property

    Private _BackColor As Color = Color.FromArgb(73, 73, 73)
    <Category("ToolsBox Herramienta")> _
    Public Property BackColor() As Color
        Get
            Return _BackColor
        End Get
        Set(ByVal value As Color)
            _BackColor = value
        End Set
    End Property

    Private _ButtonColor As Color = Color.FromArgb(77, 77, 77)
    <Category("ToolsBox Herramienta")> _
    Public Property ButtonColor() As Color
        Get
            Return _ButtonColor
        End Get
        Set(ByVal value As Color)
            _ButtonColor = value
        End Set
    End Property

    Private _CursorButtonColor As Color = Color.FromArgb(87, 87, 87)
    <Category("ToolsBox Herramienta")> _
    Public Property ButtonColorCursor() As Color
        Get
            Return _CursorButtonColor
        End Get
        Set(ByVal value As Color)
            _CursorButtonColor = value
        End Set
    End Property

    Private _OuterBorder As Color = Color.FromArgb(81, 81, 81)
    <Category("ToolsBox Herramienta")> _
    Public Property BorderOuter() As Color
        Get
            Return _OuterBorder
        End Get
        Set(ByVal value As Color)
            _OuterBorder = value
        End Set
    End Property

    Private _InnerBorder As Color = Color.FromArgb(60, 60, 60)
    <Category("ToolsBox Herramienta")> _
    Public Property BorderInner() As Color
        Get
            Return _InnerBorder
        End Get
        Set(ByVal value As Color)
            _InnerBorder = value
        End Set
    End Property

    Protected Overrides Sub OnCreation()
        If Not Controls.Contains(Base) Then
            Controls.Add(Base)
        End If
    End Sub

    Private Base As TextBox
    Sub New()
        Base = New TextBox
        Width = 125
        ' Text = ""
        'Base.Text = ""
        Value = 0
        Base.Font = Font
        Base.Text = Value.ToString
        Base.ReadOnly = _ReadOnly
        Base.ForeColor = _ForeColor
        _TextBackColor = _BackColor
        Base.BorderStyle = BorderStyle.None
        ' MyBase.Text = Value.ToString
        Base.Location = New Point(4, 4)
        Base.Width = Width - 10

        AddHandler Base.TextChanged, AddressOf OnBaseTextChanged
        AddHandler Base.KeyDown, AddressOf OnBaseKeyDown
        AddHandler Base.LostFocus, AddressOf OnBaseFocusLost

    End Sub

    Protected Overrides Sub ColorHook()
        Base.BackColor = _BackColor
        _TextBackColor = _BackColor
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        pntMouseLocation = e.Location
        Invalidate()
    End Sub

    Private Function GetButtonBounds() As Rectangle
        Return New Rectangle(Width - 20, 3, 17, Height - 7)
    End Function

    Private Function GetUpButtonBounds() As Rectangle
        Dim ButtonBounds As Rectangle = GetButtonBounds()
        Return New Rectangle(ButtonBounds.Location, New Size(16, CInt(Height / 2) - 5))
    End Function

    Private Function GetDownButtonBounds() As Rectangle
        Dim ButtonBounds As Rectangle = GetButtonBounds()
        Return New Rectangle(ButtonBounds.Location.X, ButtonBounds.Location.Y + CInt(Height / 2) - 3, 16, CInt(Height / 2) - 5)
    End Function

    <STAThread()>
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        Application.DoEvents()
        If GetUpButtonBounds.Contains(e.Location) Then
        If Value + 1 <= _MaxValue Then
            Value += 1
            End If

        ElseIf GetDownButtonBounds.Contains(e.Location) Then
        If Value - 1 >= _MinValue Then
            Value -= 1
            End If
        End If
        MyBase.OnMouseDown(e)
        Invalidate()
        Focus()
    End Sub
    
    Protected Overrides Sub PaintHook()
        G.Clear(Color.FromArgb(77, 77, 77))

        G.SmoothingMode = SmoothingMode.HighQuality
        G.FillRectangle(New SolidBrush(_TextBackColor), New Rectangle(1, 1, Width - 3, Height - 3))
        'Draw up/down buttons

        If GetUpButtonBounds.Contains(pntMouseLocation) Then
            If State = MouseState.Down Then
                Dim ButtonGradient As New LinearGradientBrush(GetUpButtonBounds, Color.FromArgb(110, 110, 110), _CursorButtonColor, 90)
                G.FillPath(ButtonGradient, CreateRound(GetUpButtonBounds, 2))
            Else
                Dim ButtonGradient As New LinearGradientBrush(GetUpButtonBounds, Color.FromArgb(100, 100, 100), _CursorButtonColor, 90)
                G.FillPath(ButtonGradient, CreateRound(GetUpButtonBounds, 2))
            End If
        Else
            Dim ButtonGradient As New LinearGradientBrush(GetUpButtonBounds, Color.FromArgb(90, 90, 90), _ButtonColor, 90)
            G.FillPath(ButtonGradient, CreateRound(GetUpButtonBounds, 2))
        End If



        If GetDownButtonBounds.Contains(pntMouseLocation) Then
            If State = MouseState.Down Then
                Dim ButtonGradient As New LinearGradientBrush(GetUpButtonBounds, Color.FromArgb(110, 110, 110), _CursorButtonColor, 90)
                G.FillPath(ButtonGradient, CreateRound(GetDownButtonBounds, 2))
            Else
                Dim ButtonGradient As New LinearGradientBrush(GetUpButtonBounds, Color.FromArgb(100, 100, 100), _CursorButtonColor, 90)
                G.FillPath(ButtonGradient, CreateRound(GetDownButtonBounds, 2))
            End If
        Else
            Dim ButtonGradient As New LinearGradientBrush(GetUpButtonBounds, Color.FromArgb(90, 90, 90), _ButtonColor, 90)
            G.FillPath(ButtonGradient, CreateRound(GetDownButtonBounds, 2))
        End If


        'bordas botao
        G.DrawPath(New Pen(Color.FromArgb(65, 65, 65)), CreateRound(GetUpButtonBounds, 2))
        G.DrawPath(New Pen(Color.FromArgb(65, 65, 65)), CreateRound(GetDownButtonBounds, 2))

        'Draw up sign
        G.SmoothingMode = SmoothingMode.AntiAlias
        Dim pntCheckPointOneTop As New Point(GetUpButtonBounds.X + 6, GetUpButtonBounds.Y + GetUpButtonBounds.Height - 3)
        Dim CheckPointsTop() As Point = {pntCheckPointOneTop, New Point(pntCheckPointOneTop.X + 3, pntCheckPointOneTop.Y - 2), New Point(pntCheckPointOneTop.X + 6, pntCheckPointOneTop.Y)}
        If GetUpButtonBounds.Contains(pntMouseLocation) Then
            G.DrawLines(New Pen(Brushes.White), CheckPointsTop)
        Else
            G.DrawLines(New Pen(Color.FromArgb(220, 220, 220)), CheckPointsTop)
        End If

        'Draw down sign
        Dim pntCheckPointOneBottom As New Point(GetDownButtonBounds.X + 6, GetDownButtonBounds.Y + 3)
        Dim CheckPointsBottom() As Point = {pntCheckPointOneBottom, New Point(pntCheckPointOneBottom.X + 3, pntCheckPointOneBottom.Y + 2), New Point(pntCheckPointOneBottom.X + 6, pntCheckPointOneBottom.Y)}
        If GetDownButtonBounds.Contains(pntMouseLocation) Then
            G.DrawLines(New Pen(Brushes.White), CheckPointsBottom)
        Else
            G.DrawLines(New Pen(Color.FromArgb(220, 220, 220)), CheckPointsBottom)
        End If

        'Draw border
        G.DrawPath(New Pen(_OuterBorder), CreateRound(New Rectangle(0, 0, Width - 1, Height - 1), 4))
        G.DrawPath(New Pen(_InnerBorder), CreateRound(New Rectangle(1, 1, Width - 3, Height - 3), 4))

        Base.ForeColor = _ForeColor
       

        CarregaBordas(Me, _RoundedBorder, _RoundedBorder)
    End Sub
    Private Sub OnBaseFocusLost(ByVal s As Object, ByVal e As EventArgs)
        If Base.Text = "" Then
            Base.Text = "0"
            Value = 0
        End If
    End Sub
    Private Sub OnBaseTextChanged(ByVal s As Object, ByVal e As EventArgs)
        If Base.Text <> "-" Then
            If Base.Text.Contains("-") And Not Base.Text.StartsWith("-") Then
                Base.Text = Base.Text.Substring(Base.Text.IndexOf("-")) & Base.Text.Substring(0, Base.Text.IndexOf("-"))
                Base.Select(Base.Text.Length, 0)
            End If
            If Base.Text <> "" Then
                If CInt(Base.Text) <= _MaxValue Then
                    If CInt(Base.Text) >= _MinValue Then
                        Value = CInt(Base.Text)
                    Else
                        Value = _MinValue
                        Base.Text = _MinValue.ToString
                    End If
                Else
                    Value = _MaxValue
                    Base.Text = _MaxValue.ToString
                End If
            End If
        End If
    End Sub
    Private Sub OnBaseKeyDown(ByVal s As Object, ByVal e As KeyEventArgs)
        If e.Control AndAlso e.KeyCode = Keys.A Then
            Base.SelectAll()
            e.SuppressKeyPress = True
        End If
        If e.KeyCode = Keys.OemMinus Or e.KeyCode = Keys.Subtract Then
            e.SuppressKeyPress = True
            If Not Base.Text.StartsWith("-") Then
                Base.Text = "-" & Base.Text
                Base.Select(Base.Text.Length, 0)
            End If
        End If
        If Not IsNumeric(e) Then
            e.SuppressKeyPress = True
        End If
    End Sub
    Private Function IsNumeric(ByVal e As KeyEventArgs) As Boolean
        Dim k As Windows.Forms.Keys = e.KeyCode
        Return (k = Keys.NumPad0 Or k = Keys.NumPad1 Or k = Keys.NumPad2 Or k = Keys.NumPad3 Or k = Keys.NumPad4 _
                Or k = Keys.NumPad5 Or k = Keys.NumPad6 Or k = Keys.NumPad7 Or k = Keys.NumPad8 Or k = Keys.NumPad9 _
                Or (e.Shift And (k = Keys.D0 Or k = Keys.D1 Or k = Keys.D2 Or k = Keys.D3 Or k = Keys.D4 Or k = Keys.D5 Or k = Keys.D6 _
                Or k = Keys.D7 Or k = Keys.D8 Or k = Keys.D9)) Or _
                k = Keys.Back Or k = Keys.Delete Or k = Keys.Right Or k = Keys.Left Or k = Keys.OemMinus Or k = Keys.Subtract)
    End Function
    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        Base.Location = New Point(5, 4)
        Base.Width = Width - 31

        MyBase.OnResize(e)
    End Sub
End Class


