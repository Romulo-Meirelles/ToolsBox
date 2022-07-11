Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports ToolsBox.Controller

<ToolboxBitmap(GetType(TabControl), "TabControl")> _
Public Class TabPage_Modern_Empire
    Inherits TabControl

    Dim _IndexOver As Integer = -1
    Dim X, Y As Integer

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

    Private _TabPageSelected As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabPageSelected() As Color
        Get
            Return _TabPageSelected
        End Get
        Set(ByVal value As Color)
            _TabPageSelected = value
            Invalidate()
        End Set
    End Property

    Private _TabPageSelectedShadow As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabPageSelectedShadow() As Color
        Get
            Return _TabPageSelectedShadow
        End Get
        Set(ByVal value As Color)
            _TabPageSelectedShadow = value
            Invalidate()
        End Set
    End Property

    Private _TabPageIndicator As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabPageIndicator() As Color
        Get
            Return _TabPageIndicator
        End Get
        Set(ByVal value As Color)
            _TabPageIndicator = value
            Invalidate()
        End Set
    End Property

    Private _TabPageColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabPageColor() As Color
        Get
            Return _TabPageColor
        End Get
        Set(ByVal value As Color)
            _TabPageColor = value
            Invalidate()
        End Set
    End Property

    Private _TabPageColorShadow As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabPageColorShadow() As Color
        Get
            Return _TabPageColorShadow
        End Get
        Set(ByVal value As Color)
            _TabPageColorShadow = value
            Invalidate()
        End Set
    End Property

    Private _TabPageButtonBorder As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabPageButtonBorder() As Color
        Get
            Return _TabPageButtonBorder
        End Get
        Set(ByVal value As Color)
            _TabPageButtonBorder = value
            Invalidate()
        End Set
    End Property

    Private _TabPageForeColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabPageForeColor() As Color
        Get
            Return _TabPageForeColor
        End Get
        Set(ByVal value As Color)
            _TabPageForeColor = value
            Invalidate()
        End Set
    End Property

    Private _TabPageForeColorUnselect As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabPageForeColorUnselect() As Color
        Get
            Return _TabPageForeColorUnselect
        End Get
        Set(ByVal value As Color)
            _TabPageForeColorUnselect = value
            Invalidate()
        End Set
    End Property

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or ControlStyles.UserPaint Or ControlStyles.DoubleBuffer, True)
        DoubleBuffered = True
        SizeMode = TabSizeMode.Fixed
        ItemSize = New Size(37, 120)
        Alignment = TabAlignment.Left
        _TabPageBackColor = Color.FromArgb(200, 200, 200)
        _TabPageSelected = Color.FromArgb(36, 36, 36)
        _TabPageSelectedShadow = Color.FromArgb(25, 25, 25)
        _TabPageIndicator = Color.FromArgb(55, 173, 242)
        _TabPageColor = Color.FromArgb(42, 42, 42)
        _TabPageColorShadow = Color.FromArgb(25, 25, 25)
        _TabPageButtonBorder = Color.FromArgb(60, 60, 60)
        _TabPageForeColor = Color.Gainsboro
        _TabPageForeColorUnselect = Color.Gray
    End Sub
    Protected Overrides Sub CreateHandle()
        MyBase.CreateHandle()
    End Sub
    Dim _OldIndexOver As Integer = 0
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        X = e.Location.X
        Y = e.Location.Y
        If e.X > ItemSize.Height Then
            _IndexOver = -1
        Else
            Y = (Y - (Y Mod ItemSize.Width)) / ItemSize.Width
            _IndexOver = Y
        End If

        If _IndexOver <> _OldIndexOver Then
            Invalidate()
        End If

        _OldIndexOver = _IndexOver
        MyBase.OnMouseMove(e)
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        _IndexOver = -1
        Invalidate()
        MyBase.OnMouseLeave(e)
    End Sub


    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        e.Graphics.Clear(Color.FromArgb(36, 36, 36))
        e.Graphics.FillRectangle(New LinearGradientBrush(New Rectangle(0, 0, Width, Height), _TabPageColor, _TabPageColorShadow, 90.0F), New Rectangle(0, 0, Width, Height))

        e.Graphics.FillRectangle(New SolidBrush(_TabPageBackColor), New Rectangle(ItemSize.Height, 0, Width - ItemSize.Height, Height))
        Dim LinearGB As New LinearGradientBrush(New Rectangle(ItemSize.Height, 0, Width - ItemSize.Height, 4), Color.FromArgb(90, Color.Black), Color.Transparent, 90.0F)
        e.Graphics.FillRectangle(LinearGB, LinearGB.Rectangle)
        e.Graphics.DrawLine(Pens.Black, New Point(ItemSize.Height, 0), New Point(ItemSize.Height, Height))

        Try : For Each T As TabPage In TabPages
                T.BackColor = _TabPageBackColor
            Next : Catch : End Try


        For i = 0 To TabCount - 1
            Dim x2 As Rectangle = New Rectangle(New Point(GetTabRect(i).Location.X - 2, GetTabRect(i).Location.Y - 2), New Size(GetTabRect(i).Width, GetTabRect(i).Height))
            Dim textrectangle As New Rectangle(x2.Location.X + 34, x2.Location.Y, x2.Width - 34, x2.Height)

            If i = SelectedIndex Then
                Dim LGB As New LinearGradientBrush(x2, _TabPageSelected, _TabPageSelectedShadow, 90.0F)
                e.Graphics.FillRectangle(LGB, LGB.Rectangle)
                e.Graphics.FillRectangle(New SolidBrush(_TabPageIndicator), New Rectangle(x2.Location, New Size(6, x2.Height)))
                e.Graphics.DrawRectangle(New Pen(_TabPageButtonBorder), x2)
                e.Graphics.DrawLine(New Pen(_TabPageButtonBorder), New Point(x2.Location.X + 1, x2.Location.Y + x2.Height - 1), New Point(x2.Location.X + x2.Width, x2.Location.Y + x2.Height - 1))
                e.Graphics.DrawString(TabPages(i).Text, Font, New SolidBrush(_TabPageForeColor), textrectangle, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near})
            Else
                e.Graphics.DrawString(TabPages(i).Text, Font, New SolidBrush(TabPageForeColorUnselect), textrectangle, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near})
                e.Graphics.DrawRectangle(New Pen(_TabPageButtonBorder), x2)
                e.Graphics.DrawLine(New Pen(_TabPageButtonBorder), New Point(x2.Location.X + 1, x2.Location.Y + x2.Height - 1), New Point(x2.Location.X + x2.Width, x2.Location.Y + x2.Height - 1))
            End If

            If i = _IndexOver Then e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(3, Color.White)), x2)
        Next
    End Sub


End Class