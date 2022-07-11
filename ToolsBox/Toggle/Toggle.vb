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
<ToolboxBitmap(GetType(System.Windows.Forms.CheckBox))> _
Public Class ToggleButton
    Inherits CheckBox

    Private _onBackColor As Color
    Private _onToggleColor As Color
    Private _offBackColor As Color
    Private _offToggleColor As Color
    Private _solidStyle As Boolean

    <Category("ToolsBox Herramienta")> _
    Public Property OnBackColor As Color
        Get
            Return _onBackColor
        End Get
        Set(ByVal value As Color)
            _onBackColor = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property OnToggleColor As Color
        Get
            Return _onToggleColor
        End Get
        Set(ByVal value As Color)
            _onToggleColor = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property OffBackColor As Color
        Get
            Return _offBackColor
        End Get
        Set(ByVal value As Color)
            _offBackColor = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property OffToggleColor As Color
        Get
            Return _offToggleColor
        End Get
        Set(ByVal value As Color)
            _offToggleColor = value
            Me.Invalidate()
        End Set
    End Property

    <Browsable(False)>
    <Category("ToolsBox Herramienta")> _
    Public Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
        End Set
    End Property


    <DefaultValue(True)>
    <Category("ToolsBox Herramienta")> _
    Public Property SolidStyle As Boolean
        Get
            Return _solidStyle
        End Get
        Set(ByVal value As Boolean)
            _solidStyle = value
            Me.Invalidate()
        End Set
    End Property

    Public Sub New()
        Me.MinimumSize = New Size(45, 22)
        _onBackColor = Color.MediumSlateBlue
        _onToggleColor = Color.WhiteSmoke
        _offBackColor = Color.Gray
        _offToggleColor = Color.Gainsboro
        _solidStyle = True
    End Sub

    Private Function GetFigurePath() As GraphicsPath
        Dim arcSize As Integer = Me.Height - 1
        Dim leftArc As Rectangle = New Rectangle(0, 0, arcSize, arcSize)
        Dim rightArc As Rectangle = New Rectangle(Me.Width - arcSize - 2, 0, arcSize, arcSize)
        Dim path As GraphicsPath = New GraphicsPath()
        path.StartFigure()
        path.AddArc(leftArc, 90, 180)
        path.AddArc(rightArc, 270, 180)
        path.CloseFigure()
        Return path
    End Function

    Protected Overrides Sub OnPaint(ByVal pevent As PaintEventArgs)
        Dim toggleSize As Integer = Me.Height - 5
        pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        pevent.Graphics.Clear(Me.Parent.BackColor)

        If Me.Checked Then

            If SolidStyle Then
                pevent.Graphics.FillPath(New SolidBrush(_onBackColor), GetFigurePath())
            Else
                pevent.Graphics.DrawPath(New Pen(_onBackColor, 2), GetFigurePath())
            End If

            pevent.Graphics.FillEllipse(New SolidBrush(_onToggleColor), New Rectangle(Me.Width - Me.Height + 1, 2, toggleSize, toggleSize))
        Else

            If SolidStyle Then
                pevent.Graphics.FillPath(New SolidBrush(_offBackColor), GetFigurePath())
            Else
                pevent.Graphics.DrawPath(New Pen(_offBackColor, 2), GetFigurePath())
            End If

            pevent.Graphics.FillEllipse(New SolidBrush(_offToggleColor), New Rectangle(2, 2, toggleSize, toggleSize))
        End If
    End Sub
End Class

