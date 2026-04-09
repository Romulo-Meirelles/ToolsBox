Imports System, System.Collections
Imports System.Drawing, System.Drawing.Drawing2D
Imports System.ComponentModel, System.Windows.Forms
Imports System.Runtime.InteropServices
Imports ToolsBox.Controller

<ToolboxBitmap(GetType(FormAtrocity), "Purple.ico")>
Public Class FormAtrocity
    Inherits ThemeContainer153

#Region "Close Button Border property"
    <Description("Choose weather or not to draw the border around the close button; Fixed Position!"), Browsable(True)>
    Private _drawCButtonBorder As Boolean = True
    <Category("Super Forms")>
    Public Property drawCButtonBorder() As Boolean
        Get
            Return _drawCButtonBorder
        End Get
        Set(ByVal value As Boolean)
            _drawCButtonBorder = value
            Invalidate()
        End Set
    End Property

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

    Private _TextColor As Color = Color.FromArgb(0, 168, 198)
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
#End Region

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
        Header = 30
        BackColor = Color.FromArgb(41, 41, 41)
        TransparencyKey = Color.Fuchsia

        SetColor("45", 45, 45, 45)
    End Sub


    Protected Overrides Sub ColorHook()

        BackColor = Color.FromArgb(41, 41, 41)

    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(BackColor)


        DrawGradient(Color.FromArgb(60, 60, 60), Color.FromArgb(41, 41, 41), 0, 0, Width, 31)
        Dim DarkDown As New HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.Transparent, Color.FromArgb(50, Color.Black))
        Dim DarkUp As New HatchBrush(HatchStyle.DarkUpwardDiagonal, Color.Transparent, Color.FromArgb(50, Color.Black))
        G.FillRectangle(DarkDown, New Rectangle(0, 0, ClientRectangle.Width, Header))
        G.FillRectangle(DarkUp, New Rectangle(0, 0, ClientRectangle.Width, Header))

        'New Pen(Color.FromArgb(58, 58, 58)
        G.DrawLine(New Pen(Color.FromArgb(58, 58, 58)), 0, 31, Width, 31)
        G.DrawLine(New Pen(Color.FromArgb(25, 25, 25)), 0, 32, Width, 32)
        G.DrawLine(New Pen(Color.FromArgb(58, 58, 58)), 0, 33, Width, 33)



        G.DrawLine(New Pen(Color.FromArgb(58, 58, 58)), 34, 30, 34, 0)
        G.DrawLine(New Pen(Color.FromArgb(25, 25, 25)), 33, 31, 33, 0)
        G.DrawLine(New Pen(Color.FromArgb(58, 58, 58)), 32, 30, 32, 0)

        G.FillRectangle(New SolidBrush(BackColor), 0, 0, 32, 30)

        If _drawCButtonBorder Then
            G.DrawLine(New Pen(Color.FromArgb(58, 58, 58)), Me.Width - 36, 30, Me.Width - 36, 0)
            G.DrawLine(New Pen(Color.FromArgb(25, 25, 25)), Me.Width - 35, 31, Me.Width - 35, 0)
            G.DrawLine(New Pen(Color.FromArgb(58, 58, 58)), Me.Width - 34, 30, Me.Width - 34, 0)
        End If


        DrawText(New SolidBrush(_TextColor), HorizontalAlignment.Left, 36, 0)


        DrawBorders(New Pen(Color.FromArgb(65, 65, 65)), 0)
        DrawBorders(New Pen(Color.FromArgb(70, 70, 70), 1))

        If _ShowIcon = True Then G.DrawIcon(My.Resources.ToolsBox, New Rectangle(9, 9, 16, 16))

        DrawCorners(TransparencyKey)
    End Sub
End Class
