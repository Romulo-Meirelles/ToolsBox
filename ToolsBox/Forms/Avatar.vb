Imports System, System.IO, System.Collections.Generic
Imports System.Drawing, System.Drawing.Drawing2D
Imports System.ComponentModel, System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Drawing.Imaging
Imports System.Drawing.Design
Imports ToolsBox.Controller

<ToolboxBitmap(GetType(FormAvatar), "Purple.ico")>
Public Class FormAvatar
    Inherits ThemeContainer154

    Dim Border As Pen
    Dim TextBrush As Brush
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

    Private _TextColor As Color = Color.DeepSkyBlue
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

    Private _BackColor As Color = Color.DeepSkyBlue
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
        TransparencyKey = Color.Fuchsia
        BackColor = Color.Black
        Font = New Font("Century Gothic", 9)
        SetColor("Text", Color.DeepSkyBlue)
        SetColor("Border", Color.Fuchsia)
    End Sub

    Protected Overrides Sub ColorHook()
        Border = GetPen("Border")
    End Sub

    Protected Overrides Sub PaintHook()
        Dim LGB1 As New LinearGradientBrush(New Rectangle(0, 0, Width, Height), Color.FromArgb(255, 32, 32, 32), Color.FromArgb(255, 10, 10, 10), 90.0F)
        G.DrawRectangle(Border, New Rectangle(0, 0, Width, Height))
        G.FillRectangle(LGB1, New Rectangle(0, 0, Width, Height))
        G.DrawString(FindForm.Text, Font, New SolidBrush(_TextColor), New Point(25, 5))
        If _ShowIcon Then G.DrawIcon(_Icon, New Rectangle(7, 6, 16, 16))
        Dim LGB2 As New LinearGradientBrush(New Rectangle(0, 0, Width, Height), Color.FromArgb(125, _BackColor), Color.FromArgb(175, 25, 25, 112), 85.0F)
        G.FillRectangle(LGB2, New Rectangle(6, 25, Width - 11, Height - 30))
        DrawCorners(Color.Fuchsia)
    End Sub
End Class
