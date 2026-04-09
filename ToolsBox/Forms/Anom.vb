Imports System.ComponentModel
Imports System.Drawing, System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports ToolsBox.Controller

<ToolboxBitmap(GetType(FormAnom), "Purple.ico")>
Public Class FormAnom
    Inherits ThemeContainer154
    Protected Overrides Sub OnHandleDestroyed(ByVal e As EventArgs)
        Try
            Dock = DockStyle.None
            ParentForm.BackColor = Color.FromArgb(240, 240, 240)
            ParentForm.FormBorderStyle = FormBorderStyle.Sizable
            MyBase.OnHandleDestroyed(e)
        Catch ex As Exception
        End Try
    End Sub

    Sub New()
        TransparencyKey = Color.Fuchsia
        'BackColor = Color.LightGray
        Font = New Font("Segoe UI", 9)
        SetColor("Border", Color.Black)
        SetColor("Text", Color.Black)
    End Sub

    Dim Border As Color
    ' Dim TextBrush As Brush

    Private _Icon As Icon = My.Resources.ToolsBox

    <Category("Super Forms")>
    Public Property Icon() As Icon
        Get
            Return _Icon
        End Get
        Set(ByVal value As Icon)
            _Icon = value
            Invalidate()
        End Set
    End Property

    Private _ShowIcon As Boolean = True
    <Category("Super Forms")>
    Public Property ShowIcon() As Boolean
        Get
            Return _ShowIcon
        End Get
        Set(ByVal value As Boolean)
            _ShowIcon = value
            Invalidate()
        End Set
    End Property

    Private _TextColor As Color = Color.Black
    <Category("Super Forms")>
    Public Property TextColor() As Color
        Get
            Return _TextColor
        End Get
        Set(ByVal value As Color)
            _TextColor = value
            Invalidate()
        End Set
    End Property

    Private _BorderColor As Color = Color.Black
    <Category("Super Forms")>
    Public Property BorderColor() As Color
        Get
            Return _BorderColor
        End Get
        Set(ByVal value As Color)
            _BorderColor = value
            Invalidate()
        End Set
    End Property

    Private _Item1 As Boolean = True
    <Category("Super Forms")>
    Public Property Item1() As Boolean
        Get
            Return _Item1
        End Get
        Set(ByVal value As Boolean)
            _Item1 = value
            Invalidate()
        End Set
    End Property

    Private _Item2 As Boolean = True
    <Category("Super Forms")>
    Public Property Item2() As Boolean
        Get
            Return _Item2
        End Get
        Set(ByVal value As Boolean)
            _Item2 = value
            Invalidate()
        End Set
    End Property

    Private _Item3 As Boolean = True
    <Category("Super Forms")>
    Public Property Item3() As Boolean
        Get
            Return _Item3
        End Get
        Set(ByVal value As Boolean)
            _Item3 = value
            Invalidate()
        End Set
    End Property

    Private _Item4 As Boolean = True
    <Category("Super Forms")>
    Public Property Item4() As Boolean
        Get
            Return _Item4
        End Get
        Set(ByVal value As Boolean)
            _Item4 = value
            Invalidate()
        End Set
    End Property

    Private _Item5 As Boolean = True
    <Category("Super Forms")>
    Public Property Item5() As Boolean
        Get
            Return _Item5
        End Get
        Set(ByVal value As Boolean)
            _Item5 = value
            Invalidate()
        End Set
    End Property

    Private _Item6 As Boolean = True
    <Category("Super Forms")>
    Public Property Item6() As Boolean
        Get
            Return _Item6
        End Get
        Set(ByVal value As Boolean)
            _Item6 = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub ColorHook()
        Border = GetColor("Border")
        'TextBrush = GetBrush("Text")
    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(Border)
        Dim LGB As New LinearGradientBrush(New Rectangle(1, 1, Width - 2, Height - 2), Color.FromArgb(255, 222, 222, 222), Color.FromArgb(255, 170, 170, 170), 45.0F)
        Dim HB As New HatchBrush(HatchStyle.LightDownwardDiagonal, Color.FromArgb(100, Color.Gray), Color.Transparent)

        If _Item1 Then G.FillRectangle(New SolidBrush(Color.Black), New Rectangle(0, 0, Width - 1, Height - 1))
        If _Item2 Then G.FillRectangle(LGB, New Rectangle(1, 1, Width - 2, Height - 2))
        If _Item3 Then G.FillRectangle(HB, New Rectangle(1, 1, Width - 2, Height - 2))
        If _Item4 Then G.FillRectangle(New SolidBrush(Color.Black), New Rectangle(6, 36, Width - 12, Height - 43))
        If _Item5 Then G.FillRectangle(New SolidBrush(BackColor), New Rectangle(7, 37, Width - 14, Height - 45))
        Dim TextXPos As Integer = 30
        If (FindForm.ShowIcon) Then
            TextXPos = 30
        Else
            TextXPos = 6
        End If
        If _Item6 Then G.DrawString(FindForm.Text, Font, New SolidBrush(_TextColor), New Point(TextXPos, 10))
        If _ShowIcon = True Then G.DrawIcon(_Icon, New Rectangle(10, 10, 16, 16))


    End Sub
End Class
