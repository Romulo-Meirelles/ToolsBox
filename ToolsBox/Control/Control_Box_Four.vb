Imports System.Drawing.Drawing2D
Imports ToolsBox.Controller
Imports System.ComponentModel

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Four
    Inherits ThemeControl154
    Private _Min As Boolean = True
    Private _Max As Boolean = True
    Private X As Integer

    Protected Overrides Sub ColorHook()
    End Sub

    Private _Orientation As Orientation
    <Category("ToolsBox Herramienta")> _
    Public Property Orientation() As Orientation
        Get
            Return _Orientation
        End Get
        Set(ByVal value As Orientation)
            _Orientation = value

            If value = Windows.Forms.Orientation.Vertical Then
                LockHeight = 0
                LockWidth = 14
            Else
                LockHeight = 14
                LockWidth = 0
            End If

            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property MinButton() As Boolean
        Get
            Return _Min
        End Get
        Set(ByVal value As Boolean)
            _Min = value
            Dim tempwidth As Integer = 40
            If _Min Then tempwidth += 25
            If _Max Then tempwidth += 25
            Me.Width = tempwidth + 1
            Me.Height = 16
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property MaxButton() As Boolean
        Get
            Return _Max
        End Get
        Set(ByVal value As Boolean)
            _Max = value
            Dim tempwidth As Integer = 40
            If _Min Then tempwidth += 25
            If _Max Then tempwidth += 25
            Me.Width = tempwidth + 1
            Me.Height = 16
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

    Private _MouseColorMin As Color
    <Category("ToolsBox Herramienta")> _
    Property MouseColorMin() As Color
        Get
            Return _MouseColorMin
        End Get
        Set(ByVal value As Color)
            _MouseColorMin = value
            Invalidate()
        End Set
    End Property

    Private _MouseColorMax As Color
    <Category("ToolsBox Herramienta")> _
    Property MouseColorMax() As Color
        Get
            Return _MouseColorMax
        End Get
        Set(ByVal value As Color)
            _MouseColorMax = value
            Invalidate()
        End Set
    End Property

    Private _MouseColorClose As Color
    <Category("ToolsBox Herramienta")> _
    Property MouseColorClose() As Color
        Get
            Return _MouseColorClose
        End Get
        Set(ByVal value As Color)
            _MouseColorClose = value
            Invalidate()
        End Set
    End Property

    Private _BorderColor As Color
    <Category("ToolsBox Herramienta")> _
    Property BorderColor() As Color
        Get
            Return _BorderColor
        End Get
        Set(ByVal value As Color)
            _BorderColor = value
            Invalidate()
        End Set
    End Property

    Sub New()
        Transparent = True
        BackColor = Color.Transparent
        LockHeight = 14
        LockWidth = 72
        Location = New Point(50, 2)
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _ColorButtonOne = Color.FromArgb(255, 43, 43, 43)
        _ColorButtonTwo = Color.FromArgb(255, 10, 10, 10)
        _MouseColorMin = Color.FromArgb(190, 244, 236, 0)
        _MouseColorMax = Color.FromArgb(255, 236, 144, 0)
        _MouseColorClose = Color.FromArgb(255, 238, 0, 0)
        _BorderColor = Color.Black
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        X = e.Location.X
        Invalidate()
    End Sub

    Protected Overrides Sub OnClick(ByVal e As System.EventArgs)
        MyBase.OnClick(e)
        If _Min And _Max Then
            If X > 0 And X < 20 Then
                FindForm.WindowState = FormWindowState.Minimized
            ElseIf X > 26 And X < 45 Then
                If FindForm.WindowState = FormWindowState.Maximized Then FindForm.WindowState = FormWindowState.Normal Else FindForm.WindowState = FormWindowState.Maximized
            ElseIf X > 51 And X < 70 Then
                FindForm.Close()
            End If
        ElseIf _Min Then
            If X > 0 And X < 20 Then
                FindForm.WindowState = FormWindowState.Minimized
            ElseIf X > 26 And X < 45 Then
                FindForm.Close()
            End If
        ElseIf _Max Then
            If X > 0 And X < 20 Then
                If FindForm.WindowState = FormWindowState.Maximized Then FindForm.WindowState = FormWindowState.Normal Else FindForm.WindowState = FormWindowState.Maximized
            ElseIf X > 26 And X < 45 Then
                FindForm.Close()
            End If
        Else
            If X > 0 And X < 20 Then
                FindForm.Close()
            End If
        End If
    End Sub

    Private ExBrush, SBrush, MinBrush, MNone As LinearGradientBrush
    Private MinPath, SPath, ExPath As GraphicsPath
    Protected Overrides Sub PaintHook()
        G.SmoothingMode = SmoothingMode.HighSpeed
        MinPath = CreateRound(New Rectangle(0, 5, 17, 6), 6)
        SPath = CreateRound(New Rectangle(25, 5, 17, 6), 6)
        ExPath = CreateRound(New Rectangle(50, 5, 17, 6), 6)

        MinBrush = New LinearGradientBrush(New Rectangle(0, 5, 17, 6), _MouseColorMin, _MouseColorMin, LinearGradientMode.Vertical)
        SBrush = New LinearGradientBrush(New Rectangle(0, 5, 17, 6), _MouseColorMax, _MouseColorMax, LinearGradientMode.Vertical)
        ExBrush = New LinearGradientBrush(New Rectangle(0, 5, 17, 6), _MouseColorClose, _MouseColorClose, LinearGradientMode.Vertical)

        MNone = New LinearGradientBrush(New Rectangle(0, 5, 17, 6), _ColorButtonOne, _ColorButtonTwo, LinearGradientMode.Vertical)

        If _Min And _Max Then
            LockHeight = 14
            LockWidth = 72
            If State = MouseState.Over Then
                If X > 0 And X < 20 Then
                    G.FillPath(MinBrush, MinPath)
                    G.FillPath(MNone, SPath)
                    G.FillPath(MNone, ExPath)
                    G.DrawPath(New Pen(_BorderColor), ExPath)
                    G.DrawPath(New Pen(_BorderColor), SPath)
                    G.DrawPath(New Pen(_BorderColor), MinPath)
                End If
                If X > 26 And X < 45 Then
                    G.FillPath(SBrush, SPath)
                    G.FillPath(MNone, MinPath)
                    G.FillPath(MNone, ExPath)
                    G.DrawPath(New Pen(_BorderColor), ExPath)
                    G.DrawPath(New Pen(_BorderColor), MinPath)
                    G.DrawPath(New Pen(_BorderColor), SPath)
                End If
                If X > 51 And X < 70 Then
                    G.FillPath(ExBrush, ExPath)
                    G.FillPath(MNone, MinPath)
                    G.FillPath(MNone, SPath)
                    G.DrawPath(New Pen(_BorderColor), MinPath)
                    G.DrawPath(New Pen(_BorderColor), SPath)
                    G.DrawPath(New Pen(_BorderColor), ExPath)
                End If
            ElseIf State = MouseState.None Then
                G.FillPath(MNone, ExPath)
                G.FillPath(MNone, MinPath)
                G.FillPath(MNone, SPath)
                G.DrawPath(New Pen(_BorderColor), MinPath)
                G.DrawPath(New Pen(_BorderColor), SPath)
                G.DrawPath(New Pen(_BorderColor), ExPath)
            End If
        ElseIf _Min Then
            LockHeight = 14
            LockWidth = 47
            If State = MouseState.Over Then
                If X > 0 And X < 20 Then
                    ExPath = CreateRound(New Rectangle(25, 5, 17, 6), 6)
                    G.FillPath(MinBrush, MinPath)
                    G.FillPath(MNone, ExPath)
                    G.DrawPath(New Pen(_BorderColor), ExPath)
                    G.DrawPath(New Pen(_BorderColor), MinPath)
                End If
                If X > 26 And X < 45 Then
                    ExPath = CreateRound(New Rectangle(25, 5, 17, 6), 6)
                    G.FillPath(ExBrush, ExPath)
                    G.FillPath(MNone, MinPath)
                    G.DrawPath(New Pen(_BorderColor), MinPath)
                    G.DrawPath(New Pen(_BorderColor), ExPath)
                End If
            ElseIf State = MouseState.None Then
                ExPath = CreateRound(New Rectangle(25, 5, 17, 6), 6)
                G.FillPath(MNone, MinPath)
                G.FillPath(MNone, ExPath)
                G.DrawPath(New Pen(_BorderColor), ExPath)
                G.DrawPath(New Pen(_BorderColor), MinPath)
            End If
        ElseIf _Max Then
            LockHeight = 14
            LockWidth = 47
            If State = MouseState.Over Then
                If X > 0 And X < 20 Then
                    SPath = CreateRound(New Rectangle(0, 5, 17, 6), 6)
                    ExPath = CreateRound(New Rectangle(25, 5, 17, 6), 6)
                    G.FillPath(SBrush, SPath)
                    G.FillPath(MNone, ExPath)
                    G.DrawPath(New Pen(_BorderColor), ExPath)
                    G.DrawPath(New Pen(_BorderColor), SPath)
                End If
                If X > 26 And X < 45 Then
                    ExPath = CreateRound(New Rectangle(25, 5, 17, 6), 6)
                    SPath = CreateRound(New Rectangle(0, 5, 17, 6), 6)
                    G.FillPath(ExBrush, ExPath)
                    G.FillPath(MNone, SPath)
                    G.DrawPath(New Pen(_BorderColor), SPath)
                    G.DrawPath(New Pen(_BorderColor), ExPath)
                End If
            ElseIf State = MouseState.None Then
                SPath = CreateRound(New Rectangle(0, 5, 17, 6), 6)
                ExPath = CreateRound(New Rectangle(25, 5, 17, 6), 6)
                G.FillPath(MNone, SPath)
                G.FillPath(MNone, ExPath)
                G.DrawPath(New Pen(_BorderColor), ExPath)
                G.DrawPath(New Pen(_BorderColor), SPath)
            End If
        Else
            LockHeight = 14
            LockWidth = 19
            If State = MouseState.Over Then
                If X > 0 And X < 20 Then
                    ExPath = CreateRound(New Rectangle(0, 5, 17, 6), 6)
                    G.FillPath(ExBrush, ExPath)
                    G.DrawPath(New Pen(_BorderColor), ExPath)
                End If
            ElseIf State = MouseState.None Then
                ExPath = CreateRound(New Rectangle(0, 5, 17, 6), 6)
                G.FillPath(MNone, ExPath)
                G.DrawPath(New Pen(_BorderColor), ExPath)
            End If
        End If
    End Sub
End Class

