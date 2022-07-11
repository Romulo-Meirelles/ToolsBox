Imports System, System.IO, System.Collections.Generic
Imports System.Drawing, System.Drawing.Drawing2D
Imports System.ComponentModel, System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Drawing.Imaging
Imports System.Drawing.Design
Imports ToolsBox.Controller

<ToolboxBitmap(GetType(GroupBox), "GroupBox")> _
Public Class GroupBox_Avatar
    Inherits ThemeContainer154

    Private _Border As Boolean
    Private _BackColor As Color
    Private _BackColorGradient As Color
    Private _TitleColor As Color
    Private _TitleColorGradient As Color
    Private _BorderColor As Color
    Private _ForeColor As Color
    Private _Opacity As Integer
    Private HeaderColor, TextColor As Brush

    Sub New()
       
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        MyBase.Size = New Size(150, 100)
        _BorderColor = Color.Black
        ControlMode = True
        'SetColor("Border", _BorderColor)
        _Border = True
        _ForeColor = Color.White

        _BackColor = Color.FromArgb(255, 80, 80, 80)
        _BackColorGradient = Color.FromArgb(150, 64, 64, 64)
        _TitleColor = Color.FromArgb(255, 32, 32, 32)
        _TitleColorGradient = Color.FromArgb(255, 48, 48, 48)
    End Sub

    <Category("ToolsBox Herramienta")> _
    Public Property [BackColor] As Color
        Get
            Return _BackColor
        End Get
        Set(ByVal value As Color)
            _BackColor = value

        End Set
    End Property
    <Category("ToolsBox Herramienta")> _
    Public Property BackColorGradient As Color
        Get
            Return _BackColorGradient
        End Get
        Set(ByVal value As Color)
            _BackColorGradient = value

        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property BorderColor As Color
        Get
            Return _BorderColor
        End Get
        Set(ByVal value As Color)
            _BorderColor = value

        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property TitleColor As Color
        Get
            Return _TitleColor
        End Get
        Set(ByVal value As Color)
            _TitleColor = value

        End Set
    End Property
    <Category("ToolsBox Herramienta")> _
    Public Property TitleColorGradient As Color
        Get
            Return _TitleColorGradient
        End Get
        Set(ByVal value As Color)
            _TitleColorGradient = value

        End Set
    End Property
    <Category("ToolsBox Herramienta")> _
    Public Property ForeColors As Color
        Get
            Return _ForeColor
        End Get
        Set(ByVal value As Color)
            _ForeColor = value

        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property Border As Boolean
        Get
            Return _border
        End Get
        Set(ByVal value As Boolean)
            _Border = value

        End Set
    End Property

    <Browsable(False)>
    <Category("ToolsBox Herramienta")> _
    Public Property Opacity As Integer
        Get
            Return _Opacity
        End Get
        Set(value As Integer)
            Dim v As Integer = value
            If v > 255 Then v = 255
            _Opacity = v
            Me.Invalidate()
        End Set
    End Property
    Protected Overrides Sub ColorHook()
        _BorderColor = Color.Black
        _ForeColor = Color.White
        _BackColor = Color.FromArgb(255, 80, 80, 80)
        _BackColorGradient = Color.FromArgb(150, 64, 64, 64)
        _TitleColor = Color.FromArgb(255, 32, 32, 32)
        _TitleColorGradient = Color.FromArgb(255, 48, 48, 48)
        _Opacity = 10

    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(Color.FromArgb(_Opacity, _BackColor))
        ' Dim BackBg As New SolidBrush(Color.FromArgb(_Opacity, _BackColor))
        Dim BackBg As New LinearGradientBrush(New Rectangle(0, 0, Width, Height), _BackColor, _BackColorGradient, 90.0F)
        G.FillRectangle(BackBg, New Rectangle(0, 0, Width, Height))
        Dim TopBg As New LinearGradientBrush(New Rectangle(0, 0, Width, 25), _TitleColor, _TitleColorGradient, 90.0F)
        G.FillRectangle(TopBg, New Rectangle(0, 0, Width, 25))

        If _Border Then
            G.DrawRectangle(New Pen(_BorderColor), New Rectangle(0, 0, Width, 25))
            G.DrawRectangle(New Pen(_BorderColor), New Rectangle(0, 0, Width - 1, Height - 1))
        Else
        End If
        G.DrawString(Text, Font, New SolidBrush(_ForeColor), New Point(5, 6))

    End Sub

End Class

