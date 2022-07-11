Imports System, System.IO, System.Collections.Generic, System.Runtime.InteropServices, System.ComponentModel
Imports System.Drawing, System.Drawing.Drawing2D, System.Drawing.Imaging, System.Windows.Forms
Imports ToolsBox.Controller

<ToolboxBitmap(GetType(GroupBox), "GroupBox")> _
Public Class GroupBox_BlackShades
    Inherits ContainerControl

    Private _BorderColor As Color
    Private _TextColor As Color
    Private _ForeColor As Color
    Private _BackColor As Color
    Private _Opacity As Integer

    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)
    End Sub
    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub

    <Category("ToolsBox Herramienta")> _
    Public Property [BorderColor] As Color
        Get
            Return _BorderColor
        End Get
        Set(ByVal value As Color)
            _BorderColor = value
            Me.Invalidate()
        End Set
    End Property

    '<Category("ToolsBox Herramienta")> _
    'Public Property [TextColor] As Color
    '    Get
    '        Return _TextColor
    '    End Get
    '    Set(ByVal value As Color)
    '        _TextColor = value
    '        Me.Invalidate()
    '    End Set
    'End Property

    <Category("ToolsBox Herramienta")> _
    Public Property [ForeColor] As Color
        Get
            Return _ForeColor
        End Get
        Set(ByVal value As Color)
            _ForeColor = value
            Me.Invalidate()
        End Set
    End Property

    '<Category("ToolsBox Herramienta")> _
    'Public Property [BackColor] As Color
    '    Get
    '        Return _BackColor
    '    End Get
    '    Set(ByVal value As Color)
    '        _BackColor = value
    '        Me.Invalidate()
    '    End Set
    'End Property

    '<Category("ToolsBox Herramienta")> _
    'Public Property Opacity As Integer
    '    Get
    '        Return _Opacity
    '    End Get
    '    Set(ByVal value As Integer)
    '        _Opacity = value
    '        Me.Invalidate()
    '    End Set
    'End Property
    Sub New()
        MyBase.New()
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        _Opacity = 50
        _BackColor = Color.FromArgb(33, 33, 33)
        _BorderColor = Color.FromArgb(67, 75, 78)
        _ForeColor = Color.Gray
        Size = New Size(200, 100)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)

        G.SmoothingMode = SmoothingMode.HighSpeed
        Const curve As Integer = 3
        Dim ClientRectangle As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim TransparencyKey As Color = Me.ParentForm.TransparencyKey
        MyBase.OnPaint(e)

        G.Clear(Color.FromArgb(42, 47, 49))
        'G.Clear(Color.FromArgb(_Opacity, _BackColor))
        G.DrawPath(New Pen(_BorderColor), RoundRect(New Rectangle(2, 7, Width - 5, Height - 9), curve))
        Dim outerBorder As New LinearGradientBrush(ClientRectangle, Color.FromArgb(30, 32, 32), Color.Transparent, 90S)
        G.DrawPath(New Pen(outerBorder), RoundRect(New Rectangle(1, 6, Width - 3, Height - 9), curve))
        Dim innerBorder As New LinearGradientBrush(New Rectangle(3, 7, Width - 7, Height - 10), Color.Transparent, Color.FromArgb(30, 32, 32), 90S)
        G.DrawPath(New Pen(innerBorder), RoundRect(New Rectangle(3, 7, Width - 7, Height - 10), curve))

        G.FillRectangle(New SolidBrush(Color.FromArgb(42, 47, 49)), New Rectangle(8, 0, Text.Length * 6, 11))

        G.DrawString(Text, Font, New SolidBrush(_ForeColor), New Rectangle(8, 0, Width - 1, 11), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near})

        e.Graphics.DrawImage(B.Clone(), 0, 0)
        G.Dispose() : B.Dispose()
        'MyBase.BackColor = Color.FromArgb(_Opacity, _BackColor)
        'MyBase.BackColor = _BackColor
    End Sub
End Class