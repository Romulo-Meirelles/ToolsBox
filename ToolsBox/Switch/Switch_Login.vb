Imports System.Drawing.Text
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Drawing.Design

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.CheckBox))> _
Public Class Switch_Login
    Inherits Control

#Region "Declarations"

    Private _Toggled As Toggles = Toggles.NotToggled
    Private MouseXLoc As Integer
    Private ToggleLocation As Integer = 0
    Private _BaseColour As Color = Color.FromArgb(42, 42, 42)
    Private _BorderColour As Color = Color.FromArgb(35, 35, 35)
    Private _TextColour As Color = Color.FromArgb(255, 255, 255)
    Private _NonToggledTextColour As Color = Color.FromArgb(155, 155, 155)
    Private _ToggledColour As Color = Color.FromArgb(23, 119, 151)

#End Region

#Region "Properties & Events"

    <Category("ToolsBox Herramienta")> _
    Public Property BaseColour As Color
        Get
            Return _BaseColour
        End Get
        Set(value As Color)
            _BaseColour = value
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property BorderColour As Color
        Get
            Return _BorderColour
        End Get
        Set(value As Color)
            _BorderColour = value
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property TextColour As Color
        Get
            Return _TextColour
        End Get
        Set(value As Color)
            _TextColour = value
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property NonToggledTextColourderColour As Color
        Get
            Return _NonToggledTextColour
        End Get
        Set(value As Color)
            _NonToggledTextColour = value
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property ToggledColour As Color
        Get
            Return _ToggledColour
        End Get
        Set(value As Color)
            _ToggledColour = value
        End Set
    End Property

    Enum Toggles
        Toggled
        NotToggled
    End Enum

    Event ToggledChanged()

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        MouseXLoc = e.Location.X
        Invalidate()
        If e.X < Width - 40 AndAlso e.X > 40 Then Cursor = Cursors.IBeam Else Cursor = Cursors.Arrow
    End Sub

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If MouseXLoc > Width - 39 Then
            _Toggled = Toggles.Toggled
            ToggledValue()
        ElseIf MouseXLoc < 39 Then
            _Toggled = Toggles.NotToggled
            ToggledValue()
        End If
        Invalidate()
    End Sub

    Public Property Toggled() As Toggles
        Get
            Return _Toggled
        End Get
        Set(ByVal value As Toggles)
            _Toggled = value
            Invalidate()
        End Set
    End Property

    Private Sub ToggledValue()
        If _Toggled Then
            If ToggleLocation < 100 Then
                ToggleLocation += 10
            End If
        Else
            If ToggleLocation > 0 Then
                ToggleLocation -= 10
            End If
        End If
        Invalidate()
    End Sub

#End Region

#Region "Draw Control"

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or _
                ControlStyles.UserPaint Or ControlStyles.DoubleBuffer, True)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        Dim G As Graphics = e.Graphics
        G.Clear(Parent.FindForm.BackColor)
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.FillRectangle(New SolidBrush(_BaseColour), New Rectangle(0, 0, 39, Height))
        G.FillRectangle(New SolidBrush(_BaseColour), New Rectangle(Width - 40, 0, Width, Height))
        G.FillRectangle(New SolidBrush(_BaseColour), New Rectangle(38, 9, Width - 40, 5))
        Dim P As Point() = {New Point(0, 0), New Point(39, 0), New Point(39, 9), New Point(Width - 40, 9), New Point(Width - 40, 0), _
                           New Point(Width - 2, 0), New Point(Width - 2, Height - 1), New Point(Width - 40, Height - 1), _
                            New Point(Width - 40, 14), New Point(39, 14), New Point(39, Height - 1), New Point(0, Height - 1), New Point()}
        G.DrawLines(New Pen(_BorderColour, 2), P)
        If _Toggled = Toggles.Toggled Then
            G.FillRectangle(New SolidBrush(_ToggledColour), New Rectangle(Width / 2, 10, Width / 2 - 38, 3))
            G.FillRectangle(New SolidBrush(_ToggledColour), New Rectangle(Width - 39, 2, 36, Height - 5))
        Else
        End If
        If _Toggled = Toggles.Toggled Then
            G.DrawString("ON", New Font("Microsoft Sans Serif", 7, FontStyle.Bold), New SolidBrush(_TextColour), New Rectangle(2, -1, Width - 20 + 20 / 3, Height), New StringFormat() With {.Alignment = StringAlignment.Far, .LineAlignment = StringAlignment.Center})
            G.DrawString("OFF", New Font("Microsoft Sans Serif", 7, FontStyle.Bold), New SolidBrush(_NonToggledTextColour), New Rectangle(20 - 20 / 3 - 6, -1, Width - 20 + 20 / 3, Height), New StringFormat() With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center})
        ElseIf _Toggled = Toggles.NotToggled Then
            G.DrawString("OFF", New Font("Microsoft Sans Serif", 7, FontStyle.Bold), New SolidBrush(_TextColour), New Rectangle(20 - 20 / 3 - 6, -1, Width - 20 + 20 / 3, Height), New StringFormat() With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center})
            G.DrawString("ON", New Font("Microsoft Sans Serif", 7, FontStyle.Bold), New SolidBrush(_NonToggledTextColour), New Rectangle(2, -1, Width - 20 + 20 / 3, Height), New StringFormat() With {.Alignment = StringAlignment.Far, .LineAlignment = StringAlignment.Center})
        End If
        G.DrawLine(New Pen(_BorderColour, 2), New Point(Width / 2, 0), New Point(Width / 2, Height))
    End Sub

#End Region

End Class
