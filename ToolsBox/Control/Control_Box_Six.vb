Imports System.Drawing.Drawing2D
Imports ToolsBox.Controller
Imports System.ComponentModel

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Six_Close
    Inherits Control

    Private State As MouseState = MouseState.None
    Private x As Integer


    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        x = e.X : Invalidate()
    End Sub

    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
        Environment.Exit(0)
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Size = New Size(16, 16)
    End Sub

    Private _Border As Color
    <Category("ToolsBox Herramienta")> _
    Public Property Border() As Color
        Get
            Return _Border
        End Get
        Set(ByVal value As Color)
            _Border = value
            Invalidate()
        End Set
    End Property

    Private _ButtonColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property ButtonColor() As Color
        Get
            Return _ButtonColor
        End Get
        Set(ByVal value As Color)
            _ButtonColor = value
            Invalidate()
        End Set
    End Property

    Sub New()
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.Opaque, True)
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _Border = Color.FromArgb(39, 39, 39)
        _ButtonColor = Color.FromArgb(254, 97, 82)
        Me.Cursor = Cursors.Hand
    End Sub
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim G = e.Graphics
        G.Clear(Me.BackColor)
        G.SmoothingMode = SmoothingMode.HighQuality
        G.FillEllipse(New SolidBrush(_Border), New Rectangle(0, 0, 15, 15)) 'Border
        G.FillEllipse(New SolidBrush(_ButtonColor), New Rectangle(2, 2, 11, 11)) 'Button

        Select Case State
            Case MouseState.Over
                G.FillEllipse(New SolidBrush(Color.FromArgb(40, Color.White)), New Rectangle(2, 2, 11, 11))
            Case MouseState.Down
                G.FillEllipse(New SolidBrush(Color.FromArgb(40, Color.Black)), New Rectangle(2, 2, 11, 11))

        End Select
        MyBase.OnPaint(e)
    End Sub
End Class

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Six_Max
    Inherits Control

    Private State As MouseState = MouseState.None
    Private x As Integer


    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        x = e.X : Invalidate()
    End Sub

    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
        Select Case FindForm.WindowState
            Case FormWindowState.Maximized
                FindForm.WindowState = FormWindowState.Normal
            Case FormWindowState.Normal
                FindForm.WindowState = FormWindowState.Maximized
        End Select
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Size = New Size(16, 16)
    End Sub

    Private _Border As Color
    <Category("ToolsBox Herramienta")> _
    Public Property Border() As Color
        Get
            Return _Border
        End Get
        Set(ByVal value As Color)
            _Border = value
            Invalidate()
        End Set
    End Property

    Private _ButtonColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property ButtonColor() As Color
        Get
            Return _ButtonColor
        End Get
        Set(ByVal value As Color)
            _ButtonColor = value
            Invalidate()
        End Set
    End Property

    Sub New()
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.Opaque, True)
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _Border = Color.FromArgb(39, 39, 39)
        _ButtonColor = Color.FromArgb(254, 190, 4)
        Me.Cursor = Cursors.Hand
    End Sub
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim G = e.Graphics
        G.Clear(Me.BackColor)
        G.SmoothingMode = SmoothingMode.HighQuality
        G.FillEllipse(New SolidBrush(_Border), New Rectangle(0, 0, 15, 15))
        G.FillEllipse(New SolidBrush(_ButtonColor), New Rectangle(2, 2, 11, 11))

        Select Case State
            Case MouseState.Over
                G.FillEllipse(New SolidBrush(Color.FromArgb(40, Color.White)), New Rectangle(2, 2, 11, 11))
            Case MouseState.Down
                G.FillEllipse(New SolidBrush(Color.FromArgb(40, Color.Black)), New Rectangle(2, 2, 11, 11))

        End Select
        MyBase.OnPaint(e)
    End Sub
End Class

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Six_Mini
    Inherits Control

    Private State As MouseState = MouseState.None
    Private x As Integer

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        x = e.X : Invalidate()
    End Sub

    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
        Select Case FindForm.WindowState
            Case FormWindowState.Normal
                FindForm.WindowState = FormWindowState.Minimized
            Case FormWindowState.Maximized
                FindForm.WindowState = FormWindowState.Minimized
        End Select
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Size = New Size(16, 16)
    End Sub

    Private _Border As Color
    <Category("ToolsBox Herramienta")> _
    Public Property Border() As Color
        Get
            Return _Border
        End Get
        Set(ByVal value As Color)
            _Border = value
            Invalidate()
        End Set
    End Property

    Private _ButtonColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property ButtonColor() As Color
        Get
            Return _ButtonColor
        End Get
        Set(ByVal value As Color)
            _ButtonColor = value
            Invalidate()
        End Set
    End Property

    Sub New()
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.Opaque, True)
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
        _Border = Color.FromArgb(39, 39, 39)
        _ButtonColor = Color.FromArgb(23, 205, 58)
        Me.Cursor = Cursors.Hand
    End Sub
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim G = e.Graphics
        G.Clear(Me.BackColor)
        G.SmoothingMode = SmoothingMode.HighQuality
        G.FillEllipse(New SolidBrush(_Border), New Rectangle(0, 0, 15, 15))
        G.FillEllipse(New SolidBrush(_ButtonColor), New Rectangle(2, 2, 11, 11))

        Select Case State
            Case MouseState.Over
                G.FillEllipse(New SolidBrush(Color.FromArgb(40, Color.White)), New Rectangle(2, 2, 11, 11))
            Case MouseState.Down
                G.FillEllipse(New SolidBrush(Color.FromArgb(40, Color.Black)), New Rectangle(2, 2, 11, 11))

        End Select
        MyBase.OnPaint(e)
    End Sub
End Class