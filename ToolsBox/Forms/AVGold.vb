Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports ToolsBox.Controller


<ToolboxBitmap(GetType(FormAVGold), "Purple.ico")>
Public Class FormAVGold
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

    Private _Icon As Icon = My.Resources.ToolsBox
    <Category("Super Forms")>
    Public Property Icon() As Icon
        Get
            Return _Icon
        End Get
        Set(ByVal v As Icon)
            _Icon = v
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

    Private _TextColor As Color = Color.WhiteSmoke
    <Category("Super Forms")>
    Public Property TextColor() As Color
        Get
            Return _TextColor
        End Get
        Set(ByVal v As Color)
            _TextColor = v
            Invalidate()
        End Set
    End Property

    Private _BackColor As Color = Color.FromArgb(39, 43, 57)
    <Category("Super Forms")>
    Public Property BackColor_() As Color
        Get
            Return _BackColor
        End Get
        Set(ByVal v As Color)
            _BackColor = v
            BackColor = v
            Invalidate()
        End Set
    End Property
    Sub New()
        BackColor = BackColorOk
        Font = New Font("Arial", 14)
        SetColor("Border", Color.DimGray)
        SetColor("Text", Color.WhiteSmoke)
    End Sub
    Dim BackColorOk As New Color
    Dim Border As Color
    Dim TextBrush As Brush
    Protected Overrides Sub ColorHook()
        BackColorOk = Color.FromArgb(39, 43, 57)
        Border = GetColor("Border")
        TextBrush = GetBrush("Text")
    End Sub
    Protected Overrides Sub PaintHook()
        G.Clear(Border)
        G.FillRectangle(New SolidBrush(_BackColor), New Rectangle(1, 1, Width - 2, Height - 2))
        G.DrawString(FindForm.Text, Font, New SolidBrush(_TextColor), New Point(40, 20))
        If _ShowIcon Then G.DrawIcon(_Icon, New Rectangle(9, 18, 25, 25))
    End Sub
End Class
