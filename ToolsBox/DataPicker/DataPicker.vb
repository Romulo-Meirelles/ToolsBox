Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.DateTimePicker))> _
Public Class DateTimePicker_
    Inherits DateTimePicker

    Private _skinColor As Color = Color.MediumSlateBlue
    Private _textColor As Color = Color.White
    Private _borderColor As Color = Color.PaleVioletRed
    Private _borderSize As Integer = 0
    Private droppedDown As Boolean = False
    Private calendarIcon As Image = My.Resources.calendarWhite
    Private iconButtonArea As RectangleF
    Private Const calendarIconWidth As Integer = 34
    Private Const arrowIconWidth As Integer = 17

    <Category("ToolsBox Herramienta")> _
    Public Property SkinColor As Color
        Get
            Return _skinColor
        End Get
        Set(ByVal value As Color)
            _skinColor = value

            If _skinColor.GetBrightness() >= 0.6F Then
                calendarIcon = My.Resources.calendarDark
            Else
                calendarIcon = My.Resources.calendarWhite
            End If

            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property TextColor As Color
        Get
            Return _textColor
        End Get
        Set(ByVal value As Color)
            _textColor = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set(ByVal value As Color)
            _borderColor = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property BorderSize As Integer
        Get
            Return _borderSize
        End Get
        Set(ByVal value As Integer)
            _borderSize = value
            Me.Invalidate()
        End Set
    End Property

    Public Sub New()
        Me.SetStyle(ControlStyles.UserPaint, True)
        Me.MinimumSize = New Size(0, 35)
        Me.Font = New Font(Me.Font.Name, 9.5F)
        _skinColor = Color.MediumSlateBlue
        _textColor = Color.White
        _borderColor = Color.PaleVioletRed
        _borderSize = 1
        droppedDown = False
        calendarIcon = My.Resources.calendarWhite

    End Sub

    Protected Overrides Sub OnDropDown(ByVal eventargs As EventArgs)
        MyBase.OnDropDown(eventargs)
        droppedDown = True
    End Sub

    Protected Overrides Sub OnCloseUp(ByVal eventargs As EventArgs)
        MyBase.OnCloseUp(eventargs)
        droppedDown = False
    End Sub

    Protected Overrides Sub OnKeyPress(ByVal e As KeyPressEventArgs)
        MyBase.OnKeyPress(e)
        e.Handled = True
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Using graphics As Graphics = Me.CreateGraphics()

            Using penBorder As Pen = New Pen(_borderColor, _borderSize)

                Using skinBrush As SolidBrush = New SolidBrush(_skinColor)

                    Using openIconBrush As SolidBrush = New SolidBrush(Color.FromArgb(50, 64, 64, 64))

                        Using textBrush As SolidBrush = New SolidBrush(_textColor)

                            Using textFormat As StringFormat = New StringFormat()
                                Dim clientArea As RectangleF = New RectangleF(0, 0, Me.Width - 0.5F, Me.Height - 0.5F)
                                Dim iconArea As RectangleF = New RectangleF(clientArea.Width - calendarIconWidth, 0, calendarIconWidth, clientArea.Height)
                                penBorder.Alignment = PenAlignment.Inset
                                textFormat.LineAlignment = StringAlignment.Center
                                graphics.FillRectangle(skinBrush, clientArea)
                                graphics.DrawString("   " & Me.Text, Me.Font, textBrush, clientArea, textFormat)
                                If droppedDown = True Then graphics.FillRectangle(openIconBrush, iconArea)
                                If _borderSize >= 1 Then graphics.DrawRectangle(penBorder, clientArea.X, clientArea.Y, clientArea.Width, clientArea.Height)
                                graphics.DrawImage(calendarIcon, Me.Width - calendarIcon.Width - 9, Me.Height - calendarIcon.Height - 9)
                            End Using
                        End Using
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        MyBase.OnHandleCreated(e)
        Dim iconWidth As Integer = GetIconButtonWidth()
        iconButtonArea = New RectangleF(Me.Width - iconWidth, 0, iconWidth, Me.Height)
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        MyBase.OnMouseMove(e)

        If iconButtonArea.Contains(e.Location) Then
            Me.Cursor = Cursors.Hand
        Else
            Me.Cursor = Cursors.[Default]
        End If
    End Sub

    Private Function GetIconButtonWidth() As Integer
        Dim textWidh As Integer = TextRenderer.MeasureText(Me.Text, Me.Font).Width

        If textWidh <= Me.Width - (calendarIconWidth + 20) Then
            Return calendarIconWidth
        Else
            Return arrowIconWidth
        End If
    End Function
End Class

