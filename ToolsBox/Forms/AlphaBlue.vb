Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms


<ToolboxBitmap(GetType(FormAlphaBlue), "Purple.ico")>
Public Class FormAlphaBlue
    Inherits Theme

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

    Private _TextColor As Color = Color.White
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

    Private _TaskColor As Color = Color.FromArgb(0, 95, 218)
    <Category("Super Forms")>
    Public Property TaskColor() As Color
        Get
            Return _TaskColor
        End Get
        Set(ByVal v As Color)
            _TaskColor = v
            Invalidate()
        End Set
    End Property

    Private _BackColor As Color = Color.FromArgb(0, 95, 218)
    <Category("Super Forms")>
    Public Property BackColor_() As Color
        Get
            Return _BackColor
        End Get
        Set(ByVal v As Color)
            _BackColor = v
            Invalidate()
        End Set
    End Property

    Private _BorderColor As Color = Color.DarkBlue
    <Category("Super Forms")>
    Public Property BorderColor_() As Color
        Get
            Return _BorderColor
        End Get
        Set(ByVal v As Color)
            _BorderColor = v
            Invalidate()
        End Set
    End Property
    Sub New()
        BackColor = Color.FromKnownColor(KnownColor.Control)
        MoveHeight = 25
        TransparencyKey = Color.Fuchsia
    End Sub


    Public Overrides Sub PaintHook()
        G.Clear(BackColor)
        G.Clear(_BackColor)
        DrawGradient(_TaskColor, Color.FromArgb(0, 55, 202), 0, 0, Width, 25, 90S)
        DrawCorners(Color.Fuchsia, ClientRectangle)
        DrawBorders(New Pen(_BorderColor), Pens.DodgerBlue, ClientRectangle)
        G.DrawLine(Pens.Black, 0, 25, Width, 25)
        DrawText(HorizontalAlignment.Left, _TextColor, 25, 2)
        If _ShowIcon = True Then G.DrawIcon(My.Resources.ToolsBox, New Rectangle(6, 6, 16, 16))
    End Sub
End Class

