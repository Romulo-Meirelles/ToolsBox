Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports ToolsBox.Controller

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Nine
    Inherits ThemeControl154

    Enum Button As Byte
        None = 0
        Minimize = 1
        MaximizeRestore = 2
        Close = 3
    End Enum

    Private _ControlButton As Button = Button.Close
    <Category("ToolsBox Herramienta")> _
    Public Property ControlButton() As Button
        Get
            Return _ControlButton
        End Get
        Set(ByVal value As Button)
            _ControlButton = value
            Invalidate()
        End Set
    End Property

    Private Property _ControlColor As Integer = -20
    <Category("ToolsBox Herramienta")> _
    Public Property ControlColor() As Integer
        Get
            Return _ControlColor
        End Get
        Set(ByVal value As Integer)
            _ControlColor = value
            Invalidate()
        End Set
    End Property

    Private _ItemColor As Color = Color.White
    <Category("ToolsBox Herramienta")> _
    Public Property ItemColor() As Color
        Get
            Return _ItemColor
        End Get
        Set(ByVal value As Color)
            _ItemColor = value
            Invalidate()
        End Set
    End Property
    '  Private CO As Color = AlterColor(Color.LightBlue, 100)
    Sub New()
        SetStyle(DirectCast(139286, ControlStyles), True)
        SetStyle(ControlStyles.Selectable, False)
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or _
                 ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        Me.Cursor = Cursors.Hand
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Width = 13
        Height = 13
        MinimumSize = Size
        MaximumSize = Size
        Margin = New Padding(0)
    End Sub

    Protected Overrides Sub ColorHook()
    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(Parent.BackColor)
        G.SmoothingMode = SmoothingMode.HighQuality

        Select Case _ControlButton
            Case Button.Minimize
                G.DrawString("0", New Font("Marlett", 8.25), New SolidBrush(AlterColor(_ItemColor, _ControlColor)), New Point(0, 0))
            Case Button.MaximizeRestore
                If FindForm().WindowState = FormWindowState.Maximized Then
                    G.DrawString("1", New Font("Marlett", 8.25), New SolidBrush(AlterColor(_ItemColor, _ControlColor)), New Point(0, 0))
                Else
                    G.DrawString("2", New Font("Marlett", 8.25), New SolidBrush(AlterColor(_ItemColor, _ControlColor)), New Point(0, 0))
                End If
            Case Button.Close
                G.DrawString("r", New Font("Marlett", 8.25), New SolidBrush(AlterColor(_ItemColor, _ControlColor)), New Point(0, 0))
        End Select
    End Sub

    Protected Overrides Sub OnMouseMove(e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnClick(e As System.EventArgs)
        MyBase.OnClick(e)

        Select Case _ControlButton
            Case Button.Minimize
                FindForm.WindowState = FormWindowState.Minimized
            Case Button.MaximizeRestore
                If FindForm.WindowState <> FormWindowState.Maximized Then
                    FindForm.WindowState = FormWindowState.Maximized
                Else
                    FindForm.WindowState = FormWindowState.Normal
                End If
            Case Button.Close
                FindForm.Close()
            Case Button.None
        End Select
    End Sub
End Class
