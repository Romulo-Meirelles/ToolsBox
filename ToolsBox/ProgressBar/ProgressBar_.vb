Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

Public Enum TextPosition
    Left
    Right
    Center
    Sliding
    None
End Enum

<ToolboxBitmap(GetType(ProgressBar), "ProgressBar")> _
Public Class ProgressBar_
    Inherits ProgressBar

    Private _channelColor As Color = Color.LightSteelBlue
    Private _sliderColor As Color = Color.RoyalBlue
    Private _foreBackColor As Color = Color.RoyalBlue
    Private _channelHeight As Integer = 6
    Private _sliderHeight As Integer = 6
    Private _showValue As TextPosition = TextPosition.Right
    Private _symbolBefore As String = ""
    Private _symbolAfter As String = ""
    Private _showMaximun As Boolean = False
    Private paintedBack As Boolean = False
    Private stopPainting As Boolean = False

    Public Sub New()
        Me.SetStyle(ControlStyles.UserPaint, True)
        Me.ForeColor = Color.White
    End Sub

    <Category("ToolsBox Herramienta")> _
    Public Property ChannelColor As Color
        Get
            Return _channelColor
        End Get
        Set(ByVal value As Color)
            _channelColor = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property SliderColor As Color
        Get
            Return _sliderColor
        End Get
        Set(ByVal value As Color)
            _sliderColor = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property ForeBackColor As Color
        Get
            Return _foreBackColor
        End Get
        Set(ByVal value As Color)
            _foreBackColor = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property ChannelHeight As Integer
        Get
            Return _channelHeight
        End Get
        Set(ByVal value As Integer)
            _channelHeight = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property SliderHeight As Integer
        Get
            Return _sliderHeight
        End Get
        Set(ByVal value As Integer)
            _sliderHeight = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property ShowValue As TextPosition
        Get
            Return _showValue
        End Get
        Set(ByVal value As TextPosition)
            _showValue = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property SymbolBefore As String
        Get
            Return _symbolBefore
        End Get
        Set(ByVal value As String)
            _symbolBefore = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property SymbolAfter As String
        Get
            Return _symbolAfter
        End Get
        Set(ByVal value As String)
            _symbolAfter = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property ShowMaximun As Boolean
        Get
            Return _showMaximun
        End Get
        Set(ByVal value As Boolean)
            _showMaximun = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    <Browsable(True)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    Public Overrides Property Font As Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As Font)
            MyBase.Font = value
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Overrides Property ForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(ByVal value As Color)
            MyBase.ForeColor = value
        End Set
    End Property

    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
        If stopPainting = False Then

            If paintedBack = False Then
                Dim graph As Graphics = pevent.Graphics
                Dim rectChannel As Rectangle = New Rectangle(0, 0, Me.Width, _channelHeight)

                Using brushChannel = New SolidBrush(_channelColor)

                    If _channelHeight >= _sliderHeight Then
                        rectChannel.Y = Me.Height - _channelHeight
                    Else
                        rectChannel.Y = Me.Height - ((_channelHeight + _sliderHeight) / 2)
                    End If

                    graph.Clear(Me.Parent.BackColor)
                    graph.FillRectangle(brushChannel, rectChannel)
                    If Me.DesignMode = False Then paintedBack = True
                End Using
            End If

            If Me.Value = Me.Maximum OrElse Me.Value = Me.Minimum Then paintedBack = False
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If stopPainting = False Then
            Dim graph As Graphics = e.Graphics
            Dim scaleFactor As Double = ((CDbl(Me.Value) - Me.Minimum) / (CDbl(Me.Maximum) - Me.Minimum))
            Dim sliderWidth As Integer = CInt((Me.Width * scaleFactor))
            Dim rectSlider As Rectangle = New Rectangle(0, 0, sliderWidth, _sliderHeight)

            Using brushSlider = New SolidBrush(_sliderColor)

                If _sliderHeight >= _channelHeight Then
                    rectSlider.Y = Me.Height - _sliderHeight
                Else
                    rectSlider.Y = Me.Height - ((_sliderHeight + _channelHeight) / 2)
                End If

                If sliderWidth > 1 Then graph.FillRectangle(brushSlider, rectSlider)
                If _showValue <> TextPosition.None Then DrawValueText(graph, sliderWidth, rectSlider)
            End Using
        End If

        If Me.Value = Me.Maximum Then
            stopPainting = True
        Else
            stopPainting = False
        End If
    End Sub

    Private Sub DrawValueText(ByVal graph As Graphics, ByVal sliderWidth As Integer, ByVal rectSlider As Rectangle)
        Dim text As String = _symbolBefore & Me.Value.ToString() & _symbolAfter
        If _showMaximun Then text = text & "/" & _symbolBefore & Me.Maximum.ToString() & _symbolAfter
        Dim textSize = TextRenderer.MeasureText(text, Me.Font)
        Dim rectText = New Rectangle(0, 0, textSize.Width, textSize.Height + 2)

        Using brushText = New SolidBrush(Me.ForeColor)

            Using brushTextBack = New SolidBrush(_foreBackColor)

                Using textFormat = New StringFormat()

                    Select Case _showValue
                        Case TextPosition.Left
                            rectText.X = 0
                            textFormat.Alignment = StringAlignment.Near
                        Case TextPosition.Right
                            rectText.X = Me.Width - textSize.Width
                            textFormat.Alignment = StringAlignment.Far
                        Case TextPosition.Center
                            rectText.X = (Me.Width - textSize.Width) / 2
                            textFormat.Alignment = StringAlignment.Center
                        Case TextPosition.Sliding
                            rectText.X = sliderWidth - textSize.Width
                            textFormat.Alignment = StringAlignment.Center

                            Using brushClear = New SolidBrush(Me.Parent.BackColor)
                                Dim rect = rectSlider
                                rect.Y = rectText.Y
                                rect.Height = rectText.Height
                                graph.FillRectangle(brushClear, rect)
                            End Using
                    End Select

                    graph.FillRectangle(brushTextBack, rectText)
                    graph.DrawString(text, Me.Font, brushText, rectText, textFormat)
                End Using
            End Using
        End Using
    End Sub
End Class

