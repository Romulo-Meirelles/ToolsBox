Imports System.Drawing.Drawing2D, System.ComponentModel
Imports ToolsBox.Controller

<ToolboxBitmap(GetType(TabControl), "TabControl")> _
Public Class TabPage_Modern_Black
    Inherits TabControl

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or _
                 ControlStyles.UserPaint Or ControlStyles.DoubleBuffer, True)
        Size = New Size(400, 200)
        SizeMode = TabSizeMode.Fixed
        ItemSize = New Size(35, 145)
        Font = New Font("Verdana", 8)
        Me.BackColor = Color.FromArgb(100, 100, 100)
        _TabBackColor = Color.FromArgb(100, 100, 100)
        _TabPageBorderColor = Color.FromArgb(100, 100, 100)
        _TabBorderColor = Color.FromArgb(55, 55, 55)
        _TabSelectedColor = Color.FromArgb(100, 100, 100)
        _TabIndicatorColor = Color.SteelBlue
        _TabIndicatorBorderColor = Color.FromArgb(40, 105, 145)
        _TabUnselectedColor = Color.FromArgb(75, 75, 75)
        _TabForeColorActive = Color.White
        _TabForeColorInactive = Color.Silver
        _TabIndicatorColorInactive = Color.Gray
        _TabIndicatorBorderColorInactive = Color.DimGray
    End Sub

    Protected Overrides Sub CreateHandle()
        MyBase.CreateHandle()
        Alignment = TabAlignment.Left
    End Sub

    Private _TabIndicatorColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabIndicatorColor As Color
        Get
            Return _TabIndicatorColor
        End Get
        Set(value As Color)
            _TabIndicatorColor = value
        End Set
    End Property

    Private _TabIndicatorColorInactive As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabIndicatorColorInactive As Color
        Get
            Return _TabIndicatorColorInactive
        End Get
        Set(value As Color)
            _TabIndicatorColorInactive = value
        End Set
    End Property

    Private _TabIndicatorBorderColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabIndicatorBorderColor As Color
        Get
            Return _TabIndicatorBorderColor
        End Get
        Set(value As Color)
            _TabIndicatorBorderColor = value
        End Set
    End Property

    Private _TabIndicatorBorderColorInactive As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabIndicatorBorderColorInactive As Color
        Get
            Return _TabIndicatorBorderColorInactive
        End Get
        Set(value As Color)
            _TabIndicatorBorderColorInactive = value
        End Set
    End Property

    Private _TabBackColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabBackColor As Color
        Get
            Return _TabBackColor
        End Get
        Set(value As Color)
            _TabBackColor = value
        End Set
    End Property

    Private _TabSelectedColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabSelectedColor As Color
        Get
            Return _TabSelectedColor
        End Get
        Set(value As Color)
            _TabSelectedColor = value
        End Set
    End Property

    Private _TabUnselectedColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabUnselectedColor As Color
        Get
            Return _TabUnselectedColor
        End Get
        Set(value As Color)
            _TabUnselectedColor = value
        End Set
    End Property

    Private _TabPageBorderColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabPageBorderColor As Color
        Get
            Return _TabPageBorderColor
        End Get
        Set(value As Color)
            _TabPageBorderColor = value
        End Set
    End Property

    Private _TabBorderColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabBorderColor As Color
        Get
            Return _TabBorderColor
        End Get
        Set(value As Color)
            _TabBorderColor = value
        End Set
    End Property

    Private _TabForeColorInactive As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabForeColorInactive As Color
        Get
            Return _TabForeColorInactive
        End Get
        Set(value As Color)
            _TabForeColorInactive = value
        End Set
    End Property

    Private _TabForeColorActive As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TabForeColorActive As Color
        Get
            Return _TabForeColorActive
        End Get
        Set(value As Color)
            _TabForeColorActive = value
        End Set
    End Property


    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)

        Dim G As Graphics = e.Graphics
        G.SmoothingMode = SmoothingMode.HighQuality
        G.Clear(Parent.BackColor)

        Dim FontColor As New Color
        Dim borderPen As New Pen(_TabBorderColor)

        Dim mainAreaRect As New Rectangle(ItemSize.Height + 2, 2, Width - 1 - ItemSize.Height - 2, Height - 3)
        Dim mainAreaPath As GraphicsPath = TabControlRect(mainAreaRect, 4)
        G.FillPath(New SolidBrush(_TabPageBorderColor), mainAreaPath)
        G.DrawPath(borderPen, mainAreaPath)

        For i = 0 To TabCount - 1

            If i = SelectedIndex Then

                Dim mainRect As Rectangle = GetTabRect(i)
                Dim mainPath As GraphicsPath = LeftRoundRect(mainRect, 6)

                G.FillPath(New SolidBrush(_TabSelectedColor), mainPath)
                G.DrawPath(borderPen, mainPath)

                Dim orbRect As New Rectangle(mainRect.X + 12, mainRect.Y + (mainRect.Height / 2) - 8, 16, 16)
                G.FillEllipse(New SolidBrush(_TabIndicatorColor), orbRect)
                G.FillEllipse(New LinearGradientBrush(orbRect, Color.FromArgb(30, Color.White), Color.FromArgb(10, Color.Black), 115.0F), orbRect)

                '// Color of out side of circle below
                G.DrawEllipse(New Pen(_TabIndicatorBorderColor), orbRect)
                '// Color of out side of circle above

                G.SmoothingMode = SmoothingMode.None
                G.DrawLine(New Pen(Color.FromArgb(100, 100, 100)), New Point(mainRect.X + mainRect.Width, mainRect.Y + 1), New Point(mainRect.X + mainRect.Width, mainRect.Y + mainRect.Height - 1))
                G.SmoothingMode = SmoothingMode.HighQuality

                FontColor = _TabForeColorActive

                Dim titleX As Integer = (mainRect.Location.X + 28 + 8)
                Dim titleY As Integer = (mainRect.Location.Y + mainRect.Height / 2) - (G.MeasureString(TabPages(i).Text, Font).Height / 2)
                G.DrawString(TabPages(i).Text, Font, New SolidBrush(FontColor), New Point(titleX, titleY))

            Else

                Dim tabRect As Rectangle = GetTabRect(i)
                Dim mainRect As New Rectangle(tabRect.X + 6, tabRect.Y, tabRect.Width - 6, tabRect.Height)
                Dim mainPath As GraphicsPath = LeftRoundRect(mainRect, 6)

                G.FillPath(New SolidBrush(_TabUnselectedColor), mainPath)
                G.DrawPath(borderPen, mainPath)

                Dim orbRect As New Rectangle(mainRect.X + 12, mainRect.Y + (mainRect.Height / 2) - 8, 16, 16)
                G.FillEllipse(New SolidBrush(_TabIndicatorColorInactive), orbRect)
                G.DrawEllipse(New Pen(_TabIndicatorBorderColorInactive), orbRect)

                FontColor = _TabForeColorInactive

                Dim titleX As Integer = (mainRect.Location.X + 28 + 8)
                Dim titleY As Integer = (mainRect.Location.Y + mainRect.Height / 2) - (G.MeasureString(TabPages(i).Text, Font).Height / 2)
                G.DrawString(TabPages(i).Text, Font, New SolidBrush(FontColor), New Point(titleX, titleY))

            End If

            Try
                TabPages(i).BackColor = _TabBackColor
            Catch
            End Try

        Next

    End Sub

End Class



