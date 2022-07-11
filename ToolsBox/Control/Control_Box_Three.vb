Imports System.Drawing, System.Drawing.Drawing2D
Imports ToolsBox.Controller
Imports System.ComponentModel

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Three
    Inherits ThemeControl154
#Region "Properties"
    Sub New()
        Me.Size = New Point(26, 20)
        Me.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.Cursor = Cursors.Hand
        _MouseOver = Color.Black
        _MouseDown = Color.Black
        _Border = Color.Black
        _ButtonColor = Color.FromArgb(255, 150, 0)
        _BackColorOne = Color.FromArgb(22, 22, 22)
        _BackColorTwo = Color.FromArgb(35, 35, 35)
    End Sub
    Private _StateMinimize As Boolean = False
    <Category("ToolsBox Herramienta")> _
    Public Property StateMinimize() As Boolean
        Get
            Return _StateMinimize
        End Get
        Set(ByVal v As Boolean)
            _StateMinimize = v
            Invalidate()
        End Set
    End Property

    Private _StateClose As Boolean = False
    <Category("ToolsBox Herramienta")> _
    Public Property StateClose() As Boolean
        Get
            Return _StateClose
        End Get
        Set(ByVal v As Boolean)
            _StateClose = v
            Invalidate()
        End Set
    End Property

    Private _StateMaximize As Boolean = False
    <Category("ToolsBox Herramienta")> _
    Public Property StateMaximize() As Boolean
        Get
            Return _StateMaximize
        End Get
        Set(ByVal v As Boolean)
            _StateMaximize = v
            Invalidate()
        End Set
    End Property

    Private _BackColorOne As Color
    <Category("ToolsBox Herramienta")> _
    Public Property BackColorOne() As Color
        Get
            Return _BackColorOne
        End Get
        Set(ByVal v As Color)
            _BackColorOne = v
            Invalidate()
        End Set
    End Property

    Private _BackColorTwo As Color
    <Category("ToolsBox Herramienta")> _
    Public Property BackColorTwo() As Color
        Get
            Return _BackColorTwo
        End Get
        Set(ByVal v As Color)
            _BackColorTwo = v
            Invalidate()
        End Set
    End Property

    Private _ButtonColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property ButtonColor() As Color
        Get
            Return _ButtonColor
        End Get
        Set(ByVal v As Color)
            _ButtonColor = v
            Invalidate()
        End Set
    End Property

    Private _MouseDown As Color
    <Category("ToolsBox Herramienta")> _
    Public Property MouseDownn() As Color
        Get
            Return _MouseDown
        End Get
        Set(ByVal v As Color)
            _MouseDown = v
            Invalidate()
        End Set
    End Property

    Private _MouseOver As Color
    <Category("ToolsBox Herramienta")> _
    Public Property MouseOver() As Color
        Get
            Return _MouseOver
        End Get
        Set(ByVal v As Color)
            _MouseOver = v
            Invalidate()
        End Set
    End Property

    Private _Border As Color
    <Category("ToolsBox Herramienta")> _
    Public Property Border() As Color
        Get
            Return _Border
        End Get
        Set(ByVal v As Color)
            _Border = v
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        MyBase.OnResize(e)
        Me.Size = New Point(26, 20)
    End Sub
    Protected Overrides Sub OnMouseClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseClick(e)
        If _StateMinimize = True Then
            FindForm.WindowState = FormWindowState.Minimized ' true
            ' Else
            _StateClose = False ' false
            _StateMaximize = False
        End If
        If _StateClose = True Then
            FindForm.Close()
            'Else
            _StateMinimize = False
            _StateMaximize = False
        End If
        If _StateMaximize = True Then
            If FindForm.WindowState <> FormWindowState.Maximized Then FindForm.WindowState = FormWindowState.Maximized Else FindForm.WindowState = FormWindowState.Normal

            _StateClose = False ' false
            _StateMinimize = False
        End If
    End Sub

    Protected Overrides Sub ColorHook()

    End Sub
#End Region
#Region "Color Of Control"
    Protected Overrides Sub PaintHook()
        G.Clear(Color.FromArgb(22, 22, 22))
        G.SmoothingMode = SmoothingMode.HighQuality

        Dim Header As New LinearGradientBrush(New Rectangle(0, 0, Width - 1, Height - 1), _BackColorOne, _BackColorTwo, 270S)
        G.FillRectangle(Header, New Rectangle(0, 0, Width - 1, Height - 1))

        Select Case State
            Case MouseState.Over
                ' Dim Header1 As New LinearGradientBrush(New Rectangle(0, 0, Width - 1, Height - 1), Color.FromArgb(25, 25, 25), Color.FromArgb(40, 40, 40), 270S)
                Dim Header1 As New LinearGradientBrush(New Rectangle(0, 0, Width - 1, Height - 1), _BackColorOne, _BackColorTwo, 270S)
                G.FillRectangle(Header1, New Rectangle(0, 0, Width - 1, Height - 1))
                Dim HeaderHatch1 As New HatchBrush(HatchStyle.Percent05, Color.FromArgb(35, _MouseOver), Color.Transparent)
                G.FillRectangle(HeaderHatch1, New Rectangle(0, 0, Width - 1, Height - 1))
                G.FillRectangle(New SolidBrush(Color.FromArgb(10, Color.White)), 0, 0, Width - 1, 10)
                'G.DrawLine(New Pen(Color.FromArgb(38, 38, 38)), 0, 9, Width - 1, 10) ' Cuz it has a bug dont worry i will fix it =)

            Case MouseState.Down
                Dim Header1 As New LinearGradientBrush(New Rectangle(0, 0, Width - 1, Height - 1), _BackColorOne, _BackColorTwo, 270S)
                G.FillRectangle(Header1, New Rectangle(0, 0, Width - 1, Height - 1))
                Dim HeaderHatch1 As New HatchBrush(HatchStyle.Percent05, Color.FromArgb(35, _MouseDown), Color.Transparent)
                G.FillRectangle(HeaderHatch1, New Rectangle(0, 0, Width - 1, Height - 1))
                G.FillRectangle(New SolidBrush(Color.FromArgb(8, Color.White)), 0, 0, Width - 1, 10)
                'G.DrawLine(New Pen(Color.FromArgb(35, 35, 35)), 0, 9, Width - 1, 10) ' Cuz it has a bug dont worry i will fix it =)

        End Select
        'Draw Text


        If _StateMinimize = True Then
            G.DrawString("0", New Font("Marlett", 8), New SolidBrush(_ButtonColor), New Point(6, 4))
            _StateClose = False ' false
            _StateMaximize = False
        End If

        If _StateClose = True Then
            G.DrawString("r", New Font("Marlett", 8), New SolidBrush(_ButtonColor), New Point(6, 4))
            _StateMinimize = False
            _StateMaximize = False
        End If

        If _StateMaximize = True Then
            If FindForm.WindowState <> FormWindowState.Maximized Then G.DrawString("1", New Font("Marlett", 8), New SolidBrush(_ButtonColor), New Point(6, 4)) Else G.DrawString("2", New Font("Marlett", 8), New SolidBrush(_ButtonColor), New Point(6, 4))
            _StateClose = False ' false
            _StateMinimize = False
        End If


        'Draw Gloss
        'Draw Border
        DrawBorders(New Pen(_Border))
        ' DrawBorders(New Pen(Color.FromArgb(32, 32, 32)))
    End Sub
#End Region
End Class
