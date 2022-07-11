Imports System.Drawing, System.Drawing.Drawing2D
Imports ToolsBox.Controller
Imports System.ComponentModel

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_One
    Inherits ThemeControl154
    Private _Min As Boolean = True
    Private _Max As Boolean = True
    Private _MaxEnabled As Boolean = True
    Private _MinEnabled As Boolean = True
    Private _CloseEnabled As Boolean = True
    Private _BackColorOne As Color
    Private _BackColorTwo As Color
    Private _ButtonColor As Color

    Private X As Integer

    Protected Overrides Sub ColorHook()
    End Sub
    <Category("ToolsBox Herramienta")> _
    Public Property MinButton As Boolean
        Get
            Return _Min
        End Get
        Set(value As Boolean)
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
    Public Property MaxButton As Boolean
        Get
            Return _Max
        End Get
        Set(value As Boolean)
            _Max = value
            Dim tempwidth As Integer = 40
            If _Min Then tempwidth += 25
            If _Max Then tempwidth += 25
            Me.Width = tempwidth + 1
            Me.Height = 16
            Invalidate()
        End Set
    End Property
    <Category("ToolsBox Herramienta")> _
    Public Property ButtonColor As Color
        Get
            Return _ButtonColor
        End Get
        Set(value As Color)
            _ButtonColor = value
            Invalidate()
        End Set
    End Property
    <Category("ToolsBox Herramienta")> _
    Public Property BackColorOne As Color
        Get
            Return _BackColorOne
        End Get
        Set(value As Color)
            _BackColorOne = value
            Invalidate()
        End Set
    End Property
    <Category("ToolsBox Herramienta")> _
    Public Property BackColorTwo As Color
        Get
            Return _BackColorTwo
        End Get
        Set(value As Color)
            _BackColorTwo = value
            Invalidate()
        End Set
    End Property

    Sub New()
        Size = New Size(92, 16)
        Location = New Point(50, 2)
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _ButtonColor = Color.White
        _BackColorOne = Color.FromArgb(66, 66, 66)
        _BackColorTwo = Color.FromArgb(50, 50, 50)
    End Sub

    Protected Overrides Sub OnMouseMove(e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        X = e.Location.X
        Invalidate()
    End Sub

    Protected Overrides Sub OnClick(e As System.EventArgs)
        MyBase.OnClick(e)
        If _Min And _Max Then
            If X > 0 And X < 25 Then
                FindForm.WindowState = FormWindowState.Minimized
            ElseIf X > 25 And X < 50 Then
                If FindForm.WindowState = FormWindowState.Maximized Then FindForm.WindowState = FormWindowState.Normal Else FindForm.WindowState = FormWindowState.Maximized
            ElseIf X > 50 And X < 90 Then
                FindForm.Close()
            End If

        ElseIf _Min Then
            If X > 0 And X < 25 Then
                FindForm.WindowState = FormWindowState.Minimized
            ElseIf X > 25 And X < 65 Then
                FindForm.Close()
            End If

        ElseIf _Max Then
            If X > 0 And X < 25 Then
                If FindForm.WindowState = FormWindowState.Maximized Then FindForm.WindowState = FormWindowState.Normal Else FindForm.WindowState = FormWindowState.Maximized
            ElseIf X > 25 And X < 65 Then
                FindForm.Close()
            End If
        Else
            If X > 0 And X < 40 Then
                FindForm.Close()
            End If
        End If
    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(Color.FromArgb(47, 47, 47))
        Dim cblend As ColorBlend = New ColorBlend(2)
        cblend.Colors(0) = _BackColorOne
        cblend.Colors(1) = _BackColorTwo
        cblend.Positions(0) = 0
        cblend.Positions(1) = 1
        DrawGradient(cblend, New Rectangle(New Point(0, 0), New Size(Me.Width, Me.Height)))

        If _Min And _Max Then
            If State = MouseState.Over Then
                If X > 0 And X < 25 Then
                    cblend = New ColorBlend(2)
                    cblend.Colors(0) = Color.FromArgb(80, 80, 80)
                    cblend.Colors(1) = Color.FromArgb(60, 60, 60)
                    cblend.Positions(0) = 0
                    cblend.Positions(1) = 1
                    DrawGradient(cblend, New Rectangle(New Point(1, 0), New Size(25, 15)))
                End If

                If X > 25 And X < 50 Then
                    cblend = New ColorBlend(2)
                    cblend.Colors(0) = Color.FromArgb(80, 80, 80)
                    cblend.Colors(1) = Color.FromArgb(60, 60, 60)
                    cblend.Positions(0) = 0
                    cblend.Positions(1) = 1
                    DrawGradient(cblend, New Rectangle(New Point(25, 0), New Size(25, 15)))
                End If

                If X > 50 And X < 90 Then
                    cblend = New ColorBlend(2)
                    cblend.Colors(0) = Color.FromArgb(80, 80, 80)
                    cblend.Colors(1) = Color.FromArgb(60, 60, 60)
                    cblend.Positions(0) = 0
                    cblend.Positions(1) = 1
                    DrawGradient(cblend, New Rectangle(New Point(50, 0), New Size(40, 15)))
                End If
            End If

            G.DrawLine(New Pen(Color.FromArgb(104, 104, 104)), New Point(0, 0), New Point(0, 14))
            G.DrawLine(New Pen(Color.FromArgb(104, 104, 104)), New Point(1, 15), New Point(89, 15))
            G.DrawLine(New Pen(Color.FromArgb(104, 104, 104)), New Point(25, 0), New Point(25, 14))
            G.DrawLine(New Pen(Color.FromArgb(104, 104, 104)), New Point(50, 0), New Point(50, 14))
            G.DrawLine(New Pen(Color.FromArgb(104, 104, 104)), New Point(90, 0), New Point(90, 14))
            DrawPixel(Color.FromArgb(104, 104, 104), 1, 14)
            DrawPixel(Color.FromArgb(104, 104, 104), 89, 14)
            G.DrawString("r", New Font("Marlett", 8), New SolidBrush(_ButtonColor), New Point(63, 2))

            If FindForm.WindowState = FormWindowState.Normal Then
                G.DrawString("1", New Font("Marlett", 8), New SolidBrush(_ButtonColor), New Point(32, 2))
            Else
                G.DrawString("2", New Font("Marlett", 8), New SolidBrush(_ButtonColor), New Point(32, 2))
            End If
            G.DrawString("0", New Font("Marlett", 8), New SolidBrush(_ButtonColor), New Point(6, 2))


        ElseIf _Min Then
            If State = MouseState.Over Then
                If X > 0 And X < 25 Then
                    cblend = New ColorBlend(2)
                    cblend.Colors(0) = Color.FromArgb(80, 80, 80)
                    cblend.Colors(1) = Color.FromArgb(60, 60, 60)
                    cblend.Positions(0) = 0
                    cblend.Positions(1) = 1
                    DrawGradient(cblend, New Rectangle(New Point(1, 0), New Size(25, 15)))
                End If
                If X > 25 And X < 65 Then
                    cblend = New ColorBlend(2)
                    cblend.Colors(0) = Color.FromArgb(80, 80, 80)
                    cblend.Colors(1) = Color.FromArgb(60, 60, 60)
                    cblend.Positions(0) = 0
                    cblend.Positions(1) = 1
                    DrawGradient(cblend, New Rectangle(New Point(25, 0), New Size(40, 15)))
                End If
            End If

            G.DrawLine(New Pen(Color.FromArgb(104, 104, 104)), New Point(0, 0), New Point(0, 14))
            G.DrawLine(New Pen(Color.FromArgb(104, 104, 104)), New Point(25, 0), New Point(25, 14))
            G.DrawLine(New Pen(Color.FromArgb(104, 104, 104)), New Point(65, 0), New Point(65, 14))
            G.DrawLine(New Pen(Color.FromArgb(104, 104, 104)), New Point(1, 15), New Point(64, 15))
            DrawPixel(Color.FromArgb(104, 104, 104), 1, 14)
            DrawPixel(Color.FromArgb(104, 104, 104), 64, 14)
            G.DrawString("0", New Font("Marlett", 8), New SolidBrush(_ButtonColor), New Point(6, 2))
            G.DrawString("r", New Font("Marlett", 8), New SolidBrush(_ButtonColor), New Point(38, 2))

        ElseIf _Max Then
            If State = MouseState.Over Then
                If X > 0 And X < 25 Then
                    cblend = New ColorBlend(2)
                    cblend.Colors(0) = Color.FromArgb(80, 80, 80)
                    cblend.Colors(1) = Color.FromArgb(60, 60, 60)
                    cblend.Positions(0) = 0
                    cblend.Positions(1) = 1
                    DrawGradient(cblend, New Rectangle(New Point(1, 0), New Size(25, 15)))
                End If
                If X > 25 And X < 65 Then
                    cblend = New ColorBlend(2)
                    cblend.Colors(0) = Color.FromArgb(80, 80, 80)
                    cblend.Colors(1) = Color.FromArgb(60, 60, 60)
                    cblend.Positions(0) = 0
                    cblend.Positions(1) = 1
                    DrawGradient(cblend, New Rectangle(New Point(25, 0), New Size(40, 15)))
                End If
            End If

            G.DrawLine(New Pen(Color.FromArgb(104, 104, 104)), New Point(0, 0), New Point(0, 14))
            G.DrawLine(New Pen(Color.FromArgb(104, 104, 104)), New Point(25, 0), New Point(25, 14))
            G.DrawLine(New Pen(Color.FromArgb(104, 104, 104)), New Point(65, 0), New Point(65, 14))
            G.DrawLine(New Pen(Color.FromArgb(104, 104, 104)), New Point(1, 15), New Point(64, 15))
            DrawPixel(Color.FromArgb(104, 104, 104), 1, 14)
            DrawPixel(Color.FromArgb(104, 104, 104), 64, 14)
            If FindForm.WindowState = FormWindowState.Normal Then
                G.DrawString("1", New Font("Marlett", 8), New SolidBrush(_ButtonColor), New Point(6, 2))
            Else
                G.DrawString("2", New Font("Marlett", 8), New SolidBrush(_ButtonColor), New Point(6, 2))
            End If
            G.DrawString("r", New Font("Marlett", 8), New SolidBrush(_ButtonColor), New Point(38, 2))
        Else
            If State = MouseState.Over Then
                If X > 0 And X < 40 Then
                    cblend = New ColorBlend(2)
                    cblend.Colors(0) = Color.FromArgb(80, 80, 80)
                    cblend.Colors(1) = Color.FromArgb(60, 60, 60)
                    cblend.Positions(0) = 0
                    cblend.Positions(1) = 1
                    DrawGradient(cblend, New Rectangle(New Point(1, 0), New Size(40, 15)))
                End If
            End If
            G.DrawLine(New Pen(Color.FromArgb(104, 104, 104)), New Point(0, 0), New Point(0, 14))
            G.DrawLine(New Pen(Color.FromArgb(104, 104, 104)), New Point(40, 0), New Point(40, 14))
            G.DrawLine(New Pen(Color.FromArgb(104, 104, 104)), New Point(1, 15), New Point(39, 15))
            DrawPixel(Color.FromArgb(104, 104, 104), 1, 14)
            DrawPixel(Color.FromArgb(104, 104, 104), 39, 14)
            G.DrawString("r", New Font("Marlett", 8), New SolidBrush(_ButtonColor), New Point(13, 2))
        End If
    End Sub
End Class