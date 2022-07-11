
Imports System, System.IO, System.Collections.Generic
Imports System.Drawing, System.Drawing.Drawing2D
Imports System.ComponentModel, System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Drawing.Imaging
Imports System.Windows.Forms.TabControl
Imports System.ComponentModel.Design
Imports ToolsBox.Controller

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.ListView))>
Friend Class ListView_Ghost_Theme
    Inherits ThemeContainer154
    Protected Overrides Sub ColorHook()

    End Sub

    Private _ShowIcon As Boolean
    <Category("ToolsBox Herramienta")>
    Public Property ShowIcon As Boolean
        Get
            Return _ShowIcon
        End Get
        Set(value As Boolean)
            _ShowIcon = value
            Me.Invalidate()
        End Set
    End Property

    Protected Overrides Sub PaintHook()
        G.Clear(Color.DimGray)
        Dim hatch As New ColorBlend(2)
        DrawBorders(Pens.Gray, 1)
        hatch.Colors(0) = Color.DimGray
        hatch.Colors(1) = Color.FromArgb(60, 60, 60)
        hatch.Positions(0) = 0
        hatch.Positions(1) = 1
        DrawGradient(hatch, New Rectangle(0, 0, Width, 24))
        hatch.Colors(0) = Color.FromArgb(100, 100, 100)
        hatch.Colors(1) = Color.DimGray
        DrawGradient(hatch, New Rectangle(0, 0, Width, 12))
        Dim asdf As HatchBrush
        asdf = New HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.FromArgb(15, Color.Black), Color.FromArgb(0, Color.Gray))
        hatch.Colors(0) = Color.FromArgb(120, Color.Black)
        hatch.Colors(1) = Color.FromArgb(0, Color.Black)
        DrawGradient(hatch, New Rectangle(0, 0, Width, 30))
        G.FillRectangle(asdf, 0, 0, Width, 24)
        G.DrawLine(Pens.Black, 6, 24, Width - 7, 24)
        G.DrawLine(Pens.Black, 6, 24, 6, Height - 7)
        G.DrawLine(Pens.Black, 6, Height - 7, Width - 7, Height - 7)
        G.DrawLine(Pens.Black, Width - 7, 24, Width - 7, Height - 7)
        G.FillRectangle(New SolidBrush(Color.FromArgb(60, 60, 60)), New Rectangle(1, 24, 5, Height - 6 - 24))
        G.FillRectangle(asdf, 1, 24, 5, Height - 6 - 24)
        G.FillRectangle(New SolidBrush(Color.FromArgb(60, 60, 60)), New Rectangle(Width - 6, 24, Width - 1, Height - 6 - 24))
        G.FillRectangle(asdf, Width - 6, 24, Width - 2, Height - 6 - 24)
        G.FillRectangle(New SolidBrush(Color.FromArgb(60, 60, 60)), New Rectangle(1, Height - 6, Width - 2, Height - 1))
        G.FillRectangle(asdf, 1, Height - 6, Width - 2, Height - 1)
        DrawBorders(Pens.Black)
        asdf = New HatchBrush(HatchStyle.LightDownwardDiagonal, Color.DimGray)
        G.FillRectangle(asdf, 7, 25, Width - 14, Height - 24 - 8)
        G.FillRectangle(New SolidBrush(Color.FromArgb(230, 20, 20, 20)), 7, 25, Width - 14, Height - 24 - 8)
        DrawCorners(Color.Fuchsia)
        DrawCorners(Color.Fuchsia, 0, 1, 1, 1)
        DrawCorners(Color.Fuchsia, 1, 0, 1, 1)
        DrawPixel(Color.Black, 1, 1)

        DrawCorners(Color.Fuchsia, 0, Height - 2, 1, 1)
        DrawCorners(Color.Fuchsia, 1, Height - 1, 1, 1)
        DrawPixel(Color.Black, Width - 2, 1)

        DrawCorners(Color.Fuchsia, Width - 1, 1, 1, 1)
        DrawCorners(Color.Fuchsia, Width - 2, 0, 1, 1)
        DrawPixel(Color.Black, 1, Height - 2)

        DrawCorners(Color.Fuchsia, Width - 1, Height - 2, 1, 1)
        DrawCorners(Color.Fuchsia, Width - 2, Height - 1, 1, 1)
        DrawPixel(Color.Black, Width - 2, Height - 2)

        Dim cblend As New ColorBlend(2)
        cblend.Colors(0) = Color.FromArgb(35, Color.Black)
        cblend.Colors(1) = Color.FromArgb(0, 0, 0, 0)
        cblend.Positions(0) = 0
        cblend.Positions(1) = 1
        DrawGradient(cblend, 7, 25, 15, Height - 6 - 24, 0)
        cblend.Colors(0) = Color.FromArgb(0, 0, 0, 0)
        cblend.Colors(1) = Color.FromArgb(35, Color.Black)
        DrawGradient(cblend, Width - 24, 25, 17, Height - 6 - 24, 0)
        cblend.Colors(1) = Color.FromArgb(0, 0, 0, 0)
        cblend.Colors(0) = Color.FromArgb(35, Color.Black)
        DrawGradient(cblend, 7, 25, Me.Width - 14, 17, 90)
        cblend.Colors(0) = Color.FromArgb(0, 0, 0, 0)
        cblend.Colors(1) = Color.FromArgb(35, Color.Black)
        DrawGradient(cblend, 8, Me.Height - 24, Me.Width - 14, 17, 90)
        If _ShowIcon = False Then
            G.DrawString(Text, Font, Brushes.White, New Point(6, 6))
        Else
            G.DrawIcon(FindForm.Icon, New Rectangle(New Point(9, 5), New Size(16, 16)))
            G.DrawString(Text, Font, Brushes.White, New Point(28, 6))
        End If

    End Sub

    Public Sub New()
        TransparencyKey = Color.Fuchsia
    End Sub
End Class
