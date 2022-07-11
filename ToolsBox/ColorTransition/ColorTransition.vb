Imports System.ComponentModel
Imports System.Threading
Imports System.Delegate

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.ColorDialog))> _
Public Class ColorTransition
    Inherits Component
    ' Events
    Public Event OnValueChange As EventHandler

    Public Sub New()
        Me.color_0 = Color.White
        Me.color_1 = Color.White
        Me.color_2 = Color.White
        Me.method_1()
    End Sub

    Public Sub New(ByVal container As IContainer)
        Me.color_0 = Color.White
        Me.color_1 = Color.White
        Me.color_2 = Color.White
        container.Add(Me)
        Me.method_1()
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If (disposing AndAlso (Not Me.icontainer_0 Is Nothing)) Then
            Me.icontainer_0.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Public Shared Function getColorScale(ByVal Passentage As Integer, ByVal startColor As Color, ByVal endColor As Color) As Color
        Try
            Dim red As Integer = Integer.Parse(Math.Round(CDbl((startColor.R + (((endColor.R - startColor.R) * Passentage) * 0.01))), 0).ToString)
            Dim green As Integer = Integer.Parse(Math.Round(CDbl((startColor.G + (((endColor.G - startColor.G) * Passentage) * 0.01))), 0).ToString)
            Dim blue As Integer = Integer.Parse(Math.Round(CDbl((startColor.B + (((endColor.B - startColor.B) * Passentage) * 0.01))), 0).ToString)
            Return Color.FromArgb(&HFF, red, green, blue)
        Catch exception1 As Exception
            Return startColor
        End Try
    End Function

    Private Sub method_0()
        Dim num As Integer
        Dim num2 As Integer
        Dim color As Color = ColorTransition.getColorScale(Me.ProgessValue, Me.Color1, Me.Color2)
        If (color <> Me.Value) Then
            Me.color_2 = color
            If (Not Me.eventHandler_0 Is Nothing) Then
                Me.eventHandler_0.Invoke(Me, New EventArgs)
                Return
            End If
        End If
        Do While (num = num2)
            num2 = 1
            Dim num3 As Integer = num
            num = 1
            If (1 > num3) Then
                Return
            End If
        Loop
    End Sub

    Private Sub method_1()
        Me.icontainer_0 = New Container
    End Sub


    ' Properties
    Public Property Color1 As Color
        Get
            Return Me.color_0
        End Get
        Set(ByVal value As Color)
            Me.color_0 = value
            Me.method_0()
        End Set
    End Property

    Public Property Color2 As Color
        Get
            Return Me.color_1
        End Get
        Set(ByVal value As Color)
            Me.color_1 = value
            Me.method_0()
        End Set
    End Property

    Public Property ProgessValue As Integer
        Get
            Return Me.int_0
        End Get
        Set(ByVal value As Integer)
            Me.int_0 = value
            Me.method_0()
        End Set
    End Property

    Public ReadOnly Property Value As Color
        Get
            Return Me.color_2
        End Get
    End Property


    ' Fields
    Private color_0 As Color
    Private color_1 As Color
    Private color_2 As Color
    Private icontainer_0 As IContainer
    Private int_0 As Integer
    Private eventHandler_0 As EventHandler




End Class
