Imports System.Drawing.Drawing2D
Imports ToolsBox.Controller
Imports System.ComponentModel

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Five : Inherits Control
    Dim State As MouseState = MouseState.None
    Dim X As Integer
    Dim CloseBtn As New Rectangle(43, 2, 17, 17)
    Dim MinBtn As New Rectangle(3, 2, 17, 17)
    Dim HideBtn As New Rectangle(62, 3, 17, 17)
    Dim MaxBtn As New Rectangle(23, 2, 17, 17)
    Dim bgr As New Rectangle(0, 0, 62.5, 21)

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        If X > 3 AndAlso X < 20 Then
            FindForm.WindowState = FormWindowState.Minimized
        ElseIf X > 23 AndAlso X < 40 Then
            If FindForm.WindowState = FormWindowState.Maximized Then
                FindForm.WindowState = FormWindowState.Minimized
                FindForm.WindowState = FormWindowState.Normal
            Else
                FindForm.WindowState = FormWindowState.Minimized
                FindForm.WindowState = FormWindowState.Maximized
            End If

        ElseIf X > 43 AndAlso X < 60 Then

            If _CloseToHide = Closerme.MeExit Then
                Environment.Exit(0)
            ElseIf _CloseToHide = Closerme.MeClose Then
                MyBase.FindForm.Close()
            ElseIf _CloseToHide = Closerme.MeHide Then
                MyBase.FindForm.Hide()
            End If

        ElseIf X > 65 AndAlso X < 80 Then
            MyBase.FindForm.Hide()
        End If
        State = MouseState.Down : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        X = e.Location.X
        Invalidate()
    End Sub

    Enum Closerme As Integer
        MeExit = 0
        MeClose = 1
        MeHide = 2
    End Enum

    Private _CloseToHide As Closerme
    <Category("ToolsBox Herramienta")> _
    Public Property CloseToHide As Closerme
        Get
            Return _CloseToHide
        End Get
        Set(value As Closerme)
            _CloseToHide = value
        End Set
    End Property

    Private _BackColorReal As Color
    <Category("ToolsBox Herramienta")> _
    Property BackColorReal() As Color
        Get
            Return _BackColorReal
        End Get
        Set(ByVal value As Color)
            _BackColorReal = value
            Invalidate()
        End Set
    End Property

    Private _ColorButtonOne As Color
    <Category("ToolsBox Herramienta")> _
    Property ColorButtonOne() As Color
        Get
            Return _ColorButtonOne
        End Get
        Set(ByVal value As Color)
            _ColorButtonOne = value
            Invalidate()
        End Set
    End Property

    Private _ColorButtonTwo As Color
    <Category("ToolsBox Herramienta")> _
    Property ColorButtonTwo() As Color
        Get
            Return _ColorButtonTwo
        End Get
        Set(ByVal value As Color)
            _ColorButtonTwo = value
            Invalidate()
        End Set
    End Property

    Private _ColorItem As Color
    <Category("ToolsBox Herramienta")> _
    Property ColorItem() As Color
        Get
            Return _ColorItem
        End Get
        Set(ByVal value As Color)
            _ColorItem = value
            Invalidate()
        End Set
    End Property

    Private _ColorBorder As Color
    <Category("ToolsBox Herramienta")> _
    Property ColorBorder() As Color
        Get
            Return _ColorBorder
        End Get
        Set(ByVal value As Color)
            _ColorBorder = value
            Invalidate()
        End Set
    End Property

    Sub New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        DoubleBuffered = True
        Font = New Font("Marlett", 7)
        Me.Cursor = Cursors.Hand
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _BackColorReal = Color.Transparent
        _ColorButtonOne = Color.FromArgb(152, 151, 146)
        _ColorButtonTwo = Color.FromArgb(56, 55, 51)
        _ColorItem = Color.White 'Color.FromArgb(58, 57, 53)
        _ColorBorder = Color.DimGray
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)

        MyBase.OnPaint(e)

        G.SmoothingMode = SmoothingMode.HighQuality

        G.Clear(Me.BackColor)

        Dim bg0 As New LinearGradientBrush(bgr, _BackColorReal, _BackColorReal, 90S)
        G.FillPath(bg0, RoundRect(bgr, 10))

        Dim lgb10 As New LinearGradientBrush(MinBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
        G.FillEllipse(lgb10, MinBtn)
        G.DrawEllipse(New Pen(_ColorBorder), MinBtn)
        G.DrawString("0", Font, New SolidBrush(_ColorItem), New Rectangle(5.5, 6, 0, 0))

        Dim lgb20 As New LinearGradientBrush(MaxBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
        G.FillEllipse(lgb20, MaxBtn)
        G.DrawEllipse(New Pen(_ColorBorder), MaxBtn)
        G.DrawString("1", Font, New SolidBrush(_ColorItem), New Rectangle(26, 7, 0, 0))

        Dim lgb30 As New LinearGradientBrush(CloseBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
        G.FillEllipse(lgb30, CloseBtn)
        G.DrawEllipse(New Pen(_ColorBorder), CloseBtn)
        G.DrawString("r", Font, New SolidBrush(_ColorItem), New Rectangle(46, 7, 0, 0))

        Dim lgb00 As New LinearGradientBrush(HideBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
        G.FillEllipse(lgb00, HideBtn)
        G.DrawEllipse(New Pen(_ColorBorder), HideBtn)
        G.DrawImage(My.Resources.hide_16px, New Point(63, 4))


        Select Case State
            Case MouseState.None
                Dim bg As New LinearGradientBrush(bgr, _BackColorReal, _BackColorReal, 90S)
                G.FillPath(bg, RoundRect(bgr, 10))

                Dim lgb1 As New LinearGradientBrush(MinBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                G.FillEllipse(lgb1, MinBtn)
                G.DrawEllipse(New Pen(_ColorBorder), MinBtn)
                G.DrawString("0", Font, New SolidBrush(_ColorItem), New Rectangle(5.5, 6, 0, 0))

                Dim lgb2 As New LinearGradientBrush(MaxBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                G.FillEllipse(lgb2, MaxBtn)
                G.DrawEllipse(New Pen(_ColorBorder), MaxBtn)
                G.DrawString("1", Font, New SolidBrush(_ColorItem), New Rectangle(26, 7, 0, 0))

                Dim lgb3 As New LinearGradientBrush(CloseBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                G.FillEllipse(lgb3, CloseBtn)
                G.DrawEllipse(New Pen(_ColorBorder), CloseBtn)
                G.DrawString("r", Font, New SolidBrush(_ColorItem), New Rectangle(46, 7, 0, 0))

                Dim lgb0 As New LinearGradientBrush(HideBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                G.FillEllipse(lgb0, HideBtn)
                G.DrawEllipse(New Pen(_ColorBorder), HideBtn)
                G.DrawImage(My.Resources.hide_16px, New Point(63, 4))

            Case MouseState.Over
                If X > 3 AndAlso X < 20 Then
                    Dim bg As New LinearGradientBrush(bgr, _BackColorReal, _BackColorReal, 90S)
                    G.FillPath(bg, RoundRect(bgr, 10))

                    Dim lgb1 As New LinearGradientBrush(MinBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                    G.FillEllipse(lgb1, MinBtn)
                    G.DrawEllipse(New Pen(_ColorBorder), MinBtn)
                    G.DrawString("0", Font, New SolidBrush(_ColorItem), New Rectangle(5.5, 6, 0, 0))

                    Dim lgb2 As New LinearGradientBrush(MaxBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                    G.FillEllipse(lgb2, MaxBtn)
                    G.DrawEllipse(New Pen(_ColorBorder), MaxBtn)
                    G.DrawString("1", Font, New SolidBrush(_ColorItem), New Rectangle(26, 7, 0, 0))

                    Dim lgb3 As New LinearGradientBrush(CloseBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                    G.FillEllipse(lgb3, CloseBtn)
                    G.DrawEllipse(New Pen(_ColorBorder), CloseBtn)
                    G.DrawString("r", Font, New SolidBrush(_ColorItem), New Rectangle(46, 7, 0, 0))

                    Dim lgb0 As New LinearGradientBrush(HideBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                    G.FillEllipse(lgb0, HideBtn)
                    G.DrawEllipse(New Pen(_ColorBorder), HideBtn)
                    G.DrawImage(My.Resources.hide_16px, New Point(63, 4))


                ElseIf X > 23 AndAlso X < 40 Then
                    Dim bg As New LinearGradientBrush(bgr, _BackColorReal, _BackColorReal, 90S)
                    G.FillPath(bg, RoundRect(bgr, 10))

                    Dim lgb1 As New LinearGradientBrush(MinBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                    G.FillEllipse(lgb1, MinBtn)
                    G.DrawEllipse(New Pen(_ColorBorder), MinBtn)
                    G.DrawString("0", Font, New SolidBrush(_ColorItem), New Rectangle(5.5, 6, 0, 0))

                    Dim lgb2 As New LinearGradientBrush(MaxBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                    G.FillEllipse(lgb2, MaxBtn)
                    G.DrawEllipse(New Pen(_ColorBorder), MaxBtn)
                    G.DrawString("1", Font, New SolidBrush(_ColorItem), New Rectangle(26, 7, 0, 0))

                    Dim lgb3 As New LinearGradientBrush(CloseBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                    G.FillEllipse(lgb3, CloseBtn)
                    G.DrawEllipse(New Pen(_ColorBorder), CloseBtn)
                    G.DrawString("r", Font, New SolidBrush(_ColorItem), New Rectangle(46, 7, 0, 0))

                    Dim lgb0 As New LinearGradientBrush(HideBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                    G.FillEllipse(lgb0, HideBtn)
                    G.DrawEllipse(New Pen(_ColorBorder), HideBtn)
                    G.DrawImage(My.Resources.hide_16px, New Point(63, 4))

                ElseIf X > 43 AndAlso X < 60 Then
                    Dim bg As New LinearGradientBrush(bgr, _BackColorReal, _BackColorReal, 90S)
                    G.FillPath(bg, RoundRect(bgr, 10))

                    Dim lgb1 As New LinearGradientBrush(MinBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                    G.FillEllipse(lgb1, MinBtn)
                    G.DrawEllipse(New Pen(_ColorBorder), MinBtn)
                    G.DrawString("0", Font, New SolidBrush(_ColorItem), New Rectangle(5.5, 6, 0, 0))

                    Dim lgb2 As New LinearGradientBrush(MaxBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                    G.FillEllipse(lgb2, MaxBtn)
                    G.DrawEllipse(New Pen(_ColorBorder), MaxBtn)
                    G.DrawString("1", Font, New SolidBrush(_ColorItem), New Rectangle(26, 7, 0, 0))

                    Dim lgb3 As New LinearGradientBrush(CloseBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                    G.FillEllipse(lgb3, CloseBtn)
                    G.DrawEllipse(New Pen(_ColorBorder), CloseBtn)
                    G.DrawString("r", Font, New SolidBrush(_ColorItem), New Rectangle(46, 7, 0, 0))

                    Dim lgb0 As New LinearGradientBrush(HideBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                    G.FillEllipse(lgb0, HideBtn)
                    G.DrawEllipse(New Pen(_ColorBorder), HideBtn)
                    G.DrawImage(My.Resources.hide_16px, New Point(63, 4))

                ElseIf X > 65 AndAlso X < 80 Then
                    Dim bg As New LinearGradientBrush(bgr, _BackColorReal, _BackColorReal, 90S)
                    G.FillPath(bg, RoundRect(bgr, 10))

                    Dim lgb1 As New LinearGradientBrush(MinBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                    G.FillEllipse(lgb1, MinBtn)
                    G.DrawEllipse(New Pen(_ColorBorder), MinBtn)
                    G.DrawString("0", Font, New SolidBrush(_ColorItem), New Rectangle(5.5, 6, 0, 0))

                    Dim lgb2 As New LinearGradientBrush(MaxBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                    G.FillEllipse(lgb2, MaxBtn)
                    G.DrawEllipse(New Pen(_ColorBorder), MaxBtn)
                    G.DrawString("1", Font, New SolidBrush(_ColorItem), New Rectangle(26, 7, 0, 0))

                    Dim lgb3 As New LinearGradientBrush(CloseBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                    G.FillEllipse(lgb3, CloseBtn)
                    G.DrawEllipse(New Pen(_ColorBorder), CloseBtn)
                    G.DrawString("r", Font, New SolidBrush(_ColorItem), New Rectangle(46, 7, 0, 0))

                    Dim lgb0 As New LinearGradientBrush(HideBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                    G.FillEllipse(lgb0, HideBtn)
                    G.DrawEllipse(New Pen(_ColorBorder), HideBtn)
                    G.DrawImage(My.Resources.hide_16px, New Point(63, 4))
                End If

            Case Else
                Dim bg As New LinearGradientBrush(bgr, _BackColorReal, _BackColorReal, 90S)
                G.FillPath(bg, RoundRect(bgr, 10))

                Dim lgb1 As New LinearGradientBrush(MinBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                G.FillEllipse(lgb1, MinBtn)
                G.DrawEllipse(New Pen(_ColorBorder), MinBtn)
                G.DrawString("0", Font, New SolidBrush(_ColorItem), New Rectangle(5.5, 6, 0, 0))

                Dim lgb2 As New LinearGradientBrush(MaxBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                G.FillEllipse(lgb2, MaxBtn)
                G.DrawEllipse(New Pen(_ColorBorder), MaxBtn)
                G.DrawString("1", Font, New SolidBrush(_ColorItem), New Rectangle(26, 7, 0, 0))

                Dim lgb3 As New LinearGradientBrush(CloseBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                G.FillEllipse(lgb3, CloseBtn)
                G.DrawEllipse(New Pen(_ColorBorder), CloseBtn)
                G.DrawString("r", Font, New SolidBrush(_ColorItem), New Rectangle(46, 7, 0, 0))

                Dim lgb0 As New LinearGradientBrush(HideBtn, _ColorButtonOne, _ColorButtonTwo, 90S)
                G.FillEllipse(lgb0, HideBtn)
                G.DrawEllipse(New Pen(_ColorBorder), HideBtn)
                G.DrawImage(My.Resources.hide_16px, New Point(63, 4))

        End Select


        e.Graphics.DrawImage(B.Clone(), 0, 0)
        G.Dispose() : B.Dispose()
    End Sub
End Class

