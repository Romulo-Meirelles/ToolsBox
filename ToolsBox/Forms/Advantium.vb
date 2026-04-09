Imports System.Drawing.Drawing2D
Imports System.Drawing
Imports System.Windows.Forms
Imports System.ComponentModel
Imports ToolsBox.Controller

<ToolboxBitmap(GetType(FormAdvantium), "Purple.ico")>
Public Class FormAdvantium
    Inherits ThemeContainer151
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
        MoveHeight = 35

        SetColor("BackColor", Color.FromArgb(40, 40, 40))
        SetColor("BorderInner", Color.FromArgb(65, 65, 65))
        SetColor("BorderColor", Color.Black)
        SetColor("TextColor", Color.LawnGreen)
    End Sub
    Dim C1, BC, BA, T1 As Color
    Protected Overrides Sub ColorHook()
        C1 = GetColor("BackColor")
        BC = GetColor("BorderColor")
        BA = GetColor("BorderInner")
        T1 = GetColor("TextColor")
    End Sub

    Private _Icon As Icon = My.Resources.ToolsBox

    <System.ComponentModel.Category("Super Forms")>
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

    Private _TextColor As Color = Color.Chartreuse
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

    Private _SquareInside As Boolean = True
    <Category("Super Forms")>
    Public Property SquareInside() As Boolean
        Get
            Return _SquareInside
        End Get
        Set(ByVal value As Boolean)
            _SquareInside = value
            Invalidate()
        End Set
    End Property

    Private _BorderInside As Boolean = True
    <Category("Super Forms")>
    Public Property BorderInside() As Boolean
        Get
            Return _BorderInside
        End Get
        Set(ByVal value As Boolean)
            _BorderInside = value
            Invalidate()
        End Set
    End Property

    Private _LineTaskBar As Boolean = True
    <Category("Super Forms")>
    Public Property LineTaskBar() As Boolean
        Get
            Return _LineTaskBar
        End Get
        Set(ByVal value As Boolean)
            _LineTaskBar = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub PaintHook()
        G.Clear(C1)
        DrawGradient(Color.FromArgb(20, 20, 20), Color.FromArgb(40, 40, 40), New Rectangle(0, 0, Width, 35), 90S)

        If _SquareInside Then
            Dim T As New HatchBrush(HatchStyle.Percent10, Color.FromArgb(25, 25, 25), Color.FromArgb(35, 35, 35))
            G.FillRectangle(T, New Rectangle(11, 25, Width - 23, Height - 36))
        End If

        If _BorderInside Then
            G.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(22, 22, 22))), New Rectangle(11, 25, Width - 23, Height - 36))
        End If

        If _LineTaskBar Then
            G.DrawRectangle(New Pen(New SolidBrush(Color.FromArgb(40, 40, 40))), New Rectangle(12, 26, Width - 25, Height - 38))
        End If

        DrawCorners(Color.FromArgb(40, 40, 40), New Rectangle(11, 25, Width - 22, Height - 35))

        DrawBorders(New Pen(New SolidBrush(BA)), 1)
        DrawBorders(New Pen(New SolidBrush(BC)))
        DrawCorners(TransparencyKey)

        If _ShowIcon = True Then
            DrawText(New SolidBrush(_TextColor), HorizontalAlignment.Left, 35, -3)
            G.DrawIcon(My.Resources.ToolsBox, New Rectangle(13, 5, 16, 16))
        Else
            DrawText(New SolidBrush(_TextColor), HorizontalAlignment.Left, 15, -3)
        End If
    End Sub
End Class



