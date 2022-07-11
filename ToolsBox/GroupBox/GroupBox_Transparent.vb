Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles

<ToolboxBitmap(GetType(GroupBox), "GroupBox")> _
Public Class GroupBox_Transparent
    Inherits GroupBox

    Private _BorderColor As Color
    Private _TextColor As Color
    Private _BackColor As Color
    Private _Opacity As Integer

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

    <Category("ToolsBox Herramienta")> _
    Public Property [TextColor] As Color
        Get
            Return _TextColor
        End Get
        Set(ByVal value As Color)
            _TextColor = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property [BackColor] As Color
        Get
            Return _BackColor
        End Get
        Set(ByVal value As Color)
            _BackColor = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property Opacity As Integer
        Get
            Return _Opacity
        End Get
        Set(ByVal value As Integer)
            _Opacity = value
            Me.Invalidate()
        End Set
    End Property

    Sub New()
        MyBase.Size = New Size(150, 150)
        _Opacity = 50
        _BorderColor = Color.White
        _TextColor = Color.White
        _BackColor = Color.Black
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.BackColor = Color.FromArgb(_Opacity, _BackColor)
        Dim state As GroupBoxState = If(MyBase.Enabled, GroupBoxState.Normal, GroupBoxState.Disabled)
        Dim flags As TextFormatFlags = TextFormatFlags.PreserveGraphicsTranslateTransform Or TextFormatFlags.PreserveGraphicsClipping Or TextFormatFlags.TextBoxControl Or TextFormatFlags.WordBreak
        Dim titleColor As Color = Me._TextColor
        If Not Me.ShowKeyboardCues Then flags = flags Or TextFormatFlags.HidePrefix
        If Me.RightToLeft = RightToLeft.Yes Then flags = flags Or TextFormatFlags.RightToLeft Or TextFormatFlags.Right
        If Not Me.Enabled Then titleColor = SystemColors.GrayText
        DrawUnthemedGroupBoxWithText(e.Graphics, New Rectangle(0, 0, MyBase.Width, MyBase.Height), Me.Text, Me.Font, titleColor, flags, state)
        RaisePaintEvent(Me, e)
    End Sub

    Private Sub DrawUnthemedGroupBoxWithText(ByVal g As Graphics, ByVal bounds As Rectangle, ByVal groupBoxText As String, ByVal font As Font, ByVal titleColor As Color, ByVal flags As TextFormatFlags, ByVal state As GroupBoxState)
        Dim rectangle As Rectangle = bounds
        rectangle.Width -= 8
        Dim size As Size = TextRenderer.MeasureText(g, groupBoxText, font, New Size(rectangle.Width, rectangle.Height), flags)
        rectangle.Width = size.Width
        rectangle.Height = size.Height

        If (flags And TextFormatFlags.Right) = TextFormatFlags.Right Then
            rectangle.X = (bounds.Right - rectangle.Width) - 8
        Else
            rectangle.X += 8
        End If

        TextRenderer.DrawText(g, groupBoxText, font, rectangle, titleColor, flags)
        If rectangle.Width > 0 Then rectangle.Inflate(2, 0)

        Using pen = New Pen(Me._BorderColor)
            Dim num As Integer = bounds.Top + (font.Height / 2)
            g.DrawLine(pen, bounds.Left, num - 1, bounds.Left, bounds.Height - 2)
            g.DrawLine(pen, bounds.Left, bounds.Height - 2, bounds.Width - 1, bounds.Height - 2)
            g.DrawLine(pen, bounds.Left, num - 1, rectangle.X - 3, num - 1)
            g.DrawLine(pen, rectangle.X + rectangle.Width + 2, num - 1, bounds.Width - 2, num - 1)
            g.DrawLine(pen, bounds.Width - 2, num - 1, bounds.Width - 2, bounds.Height - 2)
        End Using

    End Sub
End Class
