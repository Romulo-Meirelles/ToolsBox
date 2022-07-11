Imports System.Drawing.Drawing2D, System.Drawing.Text, System.Drawing
Imports System.ComponentModel
Imports ToolsBox.Controller

Public Enum MouseState As Byte
    None = 0
    Over = 1
    Down = 2
End Enum

<ToolboxBitmap(GetType(TabControl), "TabControl")> _
Public Class TabPage_Modern_Rendep
    Inherits TabControl

    Enum HorizontalAlignments
        Left
        Center
        Right
    End Enum
    Private _Align As HorizontalAlignments
    <Category("ToolsBox Herramienta")> _
    Public Property TextAlign() As HorizontalAlignments
        Get
            Return _Align
        End Get
        Set(ByVal value As HorizontalAlignments)
            _Align = value
            Invalidate()
        End Set
    End Property

    Private _TabPageBackColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabPageBackColor() As Color
        Get
            Return _TabPageBackColor
        End Get
        Set(ByVal value As Color)
            _TabPageBackColor = value
            Invalidate()
        End Set
    End Property

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint Or ControlStyles.DoubleBuffer Or ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        SizeMode = TabSizeMode.Fixed
        BackColor = Color.Transparent
        ItemSize = New Size(35, 100)
        _Align = HorizontalAlignments.Left
        Font = New Font("Arial", 8.25F, FontStyle.Bold)
        _TabPageBackColor = Color.Red
    End Sub
    Protected Overrides Sub CreateHandle()
        MyBase.CreateHandle()
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)
        Dim Curve As Integer = 6
        G.TextRenderingHint = TextRenderingHint.AntiAlias
        G.SmoothingMode = SmoothingMode.HighQuality
        Try
            SelectedTab.BackColor = Color.FromArgb(47, 48, 52)
        Catch
        End Try

        G.Clear(Color.FromArgb(51, 56, 60))
        G.FillPath(New SolidBrush(Color.FromArgb(49, 50, 54)), RoundRect(New Rectangle(ItemSize.Height - 1, 0, Width - ItemSize.Height - 1 - 1, Height - 1), Curve))
        Dim GradientPen As Color() = {Color.FromArgb(43, 44, 48), Color.FromArgb(44, 45, 49), Color.FromArgb(45, 46, 50), Color.FromArgb(46, 47, 51), Color.FromArgb(47, 48, 52), Color.FromArgb(48, 49, 53)}
        For i As Integer = 0 To 5
            G.DrawPath(New Pen(GradientPen(i)), RoundRect(New Rectangle(ItemSize.Height - 1 + i + 1, i + 1, Width - ItemSize.Height - 1 - ((2 * i) + 3), Height - ((2 * i) + 3)), Curve))
        Next
        Dim BorderPen As New LinearGradientBrush(New Rectangle(0, 0, Width - 1, Height - 1), Color.Transparent, Color.FromArgb(87, 88, 92), 90S)
        G.DrawPath(New Pen(BorderPen), RoundRect(New Rectangle(ItemSize.Height - 1, 0, Width - ItemSize.Height - 1 - 1, Height - 1), Curve))
        G.DrawPath(New Pen(Color.FromArgb(32, 33, 37)), RoundRect(New Rectangle(ItemSize.Height - 1, 0, Width - ItemSize.Height - 1 - 1, Height - 2), Curve))

        For i = 0 To TabCount - 1
            If i = SelectedIndex Then
                Dim OuterBorder As Rectangle = New Rectangle(New Point(GetTabRect(i).Location.X - 1, GetTabRect(i).Location.Y + 3), New Size(GetTabRect(i).Width - 7, GetTabRect(i).Height - 7))
                Dim InnerBorder As Rectangle = New Rectangle(New Point(GetTabRect(i).Location.X - 1, GetTabRect(i).Location.Y + 4), New Size(GetTabRect(i).Width - 7, GetTabRect(i).Height - 8))
                Dim MainBody As New LinearGradientBrush(OuterBorder, Color.FromArgb(72, 79, 87), Color.FromArgb(48, 51, 56), 90S)
                G.FillPath(MainBody, RoundRect(OuterBorder, Curve))
                Dim GlossPen As New LinearGradientBrush(OuterBorder, Color.FromArgb(119, 124, 130), Color.FromArgb(64, 67, 72), 90S)
                G.DrawPath(New Pen(GlossPen), RoundRect(InnerBorder, Curve))
                G.DrawPath(New Pen(Color.FromArgb(31, 36, 42)), RoundRect(OuterBorder, Curve))
            End If

            Select Case TextAlign
                Case HorizontalAlignments.Center
                    Dim TextRectangle As Rectangle = New Rectangle(New Point(GetTabRect(i).Location.X - 4, GetTabRect(i).Location.Y + 4), New Size(GetTabRect(i).Width - 1, GetTabRect(i).Height - 7))
                    Dim TextShadow As Rectangle = New Rectangle(New Point(GetTabRect(i).Location.X - 3, GetTabRect(i).Location.Y + 5), New Size(GetTabRect(i).Width - 1, GetTabRect(i).Height - 7))
                    G.DrawString(TabPages(i).Text, Font, New SolidBrush(Color.FromArgb(150, Color.Black)), TextShadow, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})
                    G.DrawString(TabPages(i).Text, Font, Brushes.White, TextRectangle, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})

                Case HorizontalAlignments.Left
                    Dim TextRectangle As Rectangle = New Rectangle(New Point(GetTabRect(i).Location.X + 3, GetTabRect(i).Location.Y + 4), New Size(GetTabRect(i).Width - 1, GetTabRect(i).Height - 7))
                    Dim TextShadow As Rectangle = New Rectangle(New Point(GetTabRect(i).Location.X + 4, GetTabRect(i).Location.Y + 5), New Size(GetTabRect(i).Width - 1, GetTabRect(i).Height - 7))
                    G.DrawString(TabPages(i).Text, Font, New SolidBrush(Color.FromArgb(150, Color.Black)), TextShadow, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near})
                    G.DrawString(TabPages(i).Text, Font, Brushes.White, TextRectangle, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near})

                Case HorizontalAlignments.Right
                    Dim TextRectangle As Rectangle = New Rectangle(New Point(GetTabRect(i).Location.X - 9, GetTabRect(i).Location.Y + 4), New Size(GetTabRect(i).Width - 1, GetTabRect(i).Height - 7))
                    Dim TextShadow As Rectangle = New Rectangle(New Point(GetTabRect(i).Location.X - 8, GetTabRect(i).Location.Y + 5), New Size(GetTabRect(i).Width - 1, GetTabRect(i).Height - 7))
                    G.DrawString(TabPages(i).Text, Font, New SolidBrush(Color.FromArgb(150, Color.Black)), TextShadow, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Far})
                    G.DrawString(TabPages(i).Text, Font, Brushes.White, TextRectangle, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Far})
            End Select
        Next
        e.Graphics.DrawImage(B.Clone, 0, 0)
        G.Dispose() : B.Dispose()
    End Sub
End Class

