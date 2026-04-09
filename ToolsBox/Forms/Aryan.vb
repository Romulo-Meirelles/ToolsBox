Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports ToolsBox.Controller


<ToolboxBitmap(GetType(FormAryan), "Purple.ico")>
Public Class FormAryan
    Inherits ThemeContainer151

    Protected Overrides Sub OnHandleDestroyed(ByVal e As EventArgs)
        Try
            Dock = DockStyle.None
            ParentForm.BackColor = Color.FromArgb(240, 240, 240)
            ParentForm.FormBorderStyle = FormBorderStyle.Sizable
            MyBase.OnHandleDestroyed(e)
        Catch ex As Exception
        End Try
    End Sub

    Sub New()
        TransparencyKey = Color.Fuchsia
        MoveHeight = 35

        SetColor("BackColor", Color.FromArgb(25, 25, 25))
        SetColor("BorderInner", Color.FromArgb(25, 25, 25))
        SetColor("BorderColor", Color.FromArgb(25, 25, 25))
        SetColor("TextColor", Color.Black)
    End Sub

    ' Dim C1, BC, BA, T1 As Color
    Protected Overrides Sub ColorHook()
        'C1 = GetColor("BackColor")
        'BC = GetColor("BorderColor")
        'BA = GetColor("BorderInner")
        'T1 = GetColor("TextColor")
    End Sub

    Private _Icon As Icon = My.Resources.ToolsBox

    <Category("Super Forms")>
    Public Property Icon() As Icon
        Get
            Return _Icon
        End Get
        Set(ByVal value As Icon)
            _Icon = value
            Invalidate()
        End Set
    End Property


    Private _ShowIcon As Boolean = True
    <Category("Super Forms")>
    Public Property ShowIcon() As Boolean
        Get
            Return _ShowIcon
        End Get
        Set(ByVal value As Boolean)
            _ShowIcon = value
            Invalidate()
        End Set
    End Property

    Private _TextColor As Color = Color.Black
    <Category("Super Forms")>
    Public Property TextColor() As Color
        Get
            Return _TextColor
        End Get
        Set(ByVal value As Color)
            _TextColor = value
            Invalidate()
        End Set
    End Property

    Private _TaskColor As Color = Color.FromArgb(255, 0, 0)
    <Category("Super Forms")>
    Public Property TaskColor() As Color
        Get
            Return _TaskColor
        End Get
        Set(ByVal value As Color)
            _TaskColor = value
            Invalidate()
        End Set
    End Property

    Private _TaskColor2 As Color = Color.FromArgb(255, 0, 0)
    <Category("Super Forms")>
    Public Property TaskColor2() As Color
        Get
            Return _TaskColor2
        End Get
        Set(ByVal value As Color)
            _TaskColor2 = value
            Invalidate()
        End Set
    End Property

    Private _TaskShow As Boolean = True
    <Category("Super Forms")>
    Public Property TaskShow() As Boolean
        Get
            Return _TaskShow
        End Get
        Set(ByVal value As Boolean)
            _TaskShow = value
            Invalidate()
        End Set
    End Property

    Private _BackColor As Color = Color.FromArgb(25, 25, 25)
    <Category("Super Forms")>
    Public Property BackColor_() As Color
        Get
            BackColor = _BackColor
            Return _BackColor
        End Get
        Set(ByVal value As Color)
            _BackColor = value
            Invalidate()
        End Set
    End Property

    Private _Drawborder As Color = Color.FromArgb(25, 25, 25)
    <Category("Super Forms")>
    Public Property Drawborder() As Color
        Get
            Return _Drawborder
        End Get
        Set(ByVal value As Color)
            _Drawborder = value
            Invalidate()
        End Set
    End Property

    Private _Drawborder2 As Color = Color.FromArgb(25, 25, 25)
    <Category("Super Forms")>
    Public Property Drawborder2() As Color
        Get
            Return _Drawborder2
        End Get
        Set(ByVal value As Color)
            _Drawborder2 = value
            Invalidate()
        End Set
    End Property

    Private _Drawborder3 As Color = Color.FromArgb(22, 22, 22)
    <Category("Super Forms")>
    Public Property Drawborder3() As Color
        Get
            Return _Drawborder3
        End Get
        Set(ByVal value As Color)
            _Drawborder3 = value
            Invalidate()
        End Set
    End Property

    Private _Drawborder4 As Color = Color.FromArgb(40, 40, 40)
    <Category("Super Forms")>
    Public Property Drawborder4() As Color
        Get
            Return _Drawborder4
        End Get
        Set(ByVal value As Color)
            _Drawborder4 = value
            Invalidate()
        End Set
    End Property

    Private _RetangleInside As Color = Color.FromArgb(25, 25, 25)
    <Category("Super Forms")>
    Public Property RetangleInside() As Color
        Get
            Return _RetangleInside
        End Get
        Set(ByVal value As Color)
            _RetangleInside = value
            Invalidate()
        End Set
    End Property

    Private _RetangleInside2 As Color = Color.FromArgb(35, 35, 35)
    <Category("Super Forms")>
    Public Property RetangleInside2() As Color
        Get
            Return _RetangleInside2
        End Get
        Set(ByVal value As Color)
            _RetangleInside2 = value
            Invalidate()
        End Set
    End Property

    Private _ShowRetangleInside As Boolean = True
    <Category("Super Forms")>
    Public Property ShowRetangleInside() As Boolean
        Get
            Return _ShowRetangleInside
        End Get
        Set(ByVal value As Boolean)
            _ShowRetangleInside = value
            Invalidate()
        End Set
    End Property
    Protected Overrides Sub PaintHook()
        G.Clear(_BackColor)
        If _TaskShow Then DrawGradient(_TaskColor, _TaskColor2, New Rectangle(0, 0, Width, 35), 90S)

        Dim T As New HatchBrush(HatchStyle.Trellis, _RetangleInside, _RetangleInside2)
        If _ShowRetangleInside Then G.FillRectangle(T, New Rectangle(11, 25, Width - 23, Height - 36))
        If _ShowRetangleInside Then G.DrawRectangle(New Pen(New SolidBrush(_Drawborder3)), New Rectangle(11, 25, Width - 23, Height - 36))
        If _ShowRetangleInside Then G.DrawRectangle(New Pen(New SolidBrush(_Drawborder4)), New Rectangle(12, 26, Width - 25, Height - 38))

        DrawCorners(Color.FromArgb(40, 40, 40), New Rectangle(11, 25, Width - 22, Height - 35))

        DrawBorders(New Pen(New SolidBrush(_Drawborder)), 1)
        DrawBorders(New Pen(New SolidBrush(_Drawborder2)))
        DrawCorners(TransparencyKey)

        If _ShowIcon = True Then
            DrawText(New SolidBrush(_TextColor), HorizontalAlignment.Left, 35, -3)
            G.DrawIcon(My.Resources.ToolsBox, New Rectangle(13, 5, 16, 16))

        Else

            DrawText(New SolidBrush(_TextColor), HorizontalAlignment.Left, 15, -3)
        End If
    End Sub
End Class
