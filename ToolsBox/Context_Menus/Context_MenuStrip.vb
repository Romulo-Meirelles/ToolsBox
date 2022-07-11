Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.ContextMenuStrip))> _
Public Class Context_MenuStrip
    Inherits ContextMenuStrip

    Private _isMainMenu As Boolean
    Private _menuItemHeight As Integer
    Private _menuItemTextColor As Color
    Private _primaryColor As Color
    Private _menuItemHeaderSize As Bitmap

    Public Sub New(ByVal container As IContainer)
        MyBase.New(container)
        _menuItemHeight = 25
        _menuItemTextColor = Color.Empty
        _primaryColor = Color.Empty
        _isMainMenu = True
    End Sub

    <Category("ToolsBox Herramienta")> _
    Public Property IsMainMenu As Boolean
        Get
            Return _isMainMenu
        End Get
        Set(ByVal value As Boolean)
            _isMainMenu = value
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property MenuItemHeight As Integer
        Get
            Return _menuItemHeight
        End Get
        Set(ByVal value As Integer)
            _menuItemHeight = value
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property MenuItemTextColor As Color
        Get
            Return _menuItemTextColor
        End Get
        Set(ByVal value As Color)
            _menuItemTextColor = value
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property PrimaryColor As Color
        Get
            Return _primaryColor
        End Get
        Set(ByVal value As Color)
            _primaryColor = value
        End Set
    End Property

    Private Sub LoadMenuItemHeight()
        If _isMainMenu Then
            _menuItemHeaderSize = New Bitmap(25, 45)
        Else
            _menuItemHeaderSize = New Bitmap(20, 25)
        End If

        For Each menuItemL1 As ToolStripMenuItem In Me.Items
            menuItemL1.ImageScaling = ToolStripItemImageScaling.None
            If menuItemL1.Image Is Nothing Then menuItemL1.Image = _menuItemHeaderSize

            For Each menuItemL2 As ToolStripMenuItem In menuItemL1.DropDownItems
                menuItemL2.ImageScaling = ToolStripItemImageScaling.None
                If menuItemL2.Image Is Nothing Then menuItemL2.Image = _menuItemHeaderSize

                For Each menuItemL3 As ToolStripMenuItem In menuItemL2.DropDownItems
                    menuItemL3.ImageScaling = ToolStripItemImageScaling.None
                    If menuItemL3.Image Is Nothing Then menuItemL3.Image = _menuItemHeaderSize

                    For Each menuItemL4 As ToolStripMenuItem In menuItemL3.DropDownItems
                        menuItemL4.ImageScaling = ToolStripItemImageScaling.None
                        If menuItemL4.Image Is Nothing Then menuItemL4.Image = _menuItemHeaderSize
                    Next
                Next
            Next
        Next
    End Sub

    Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        MyBase.OnHandleCreated(e)

        If Me.DesignMode = False Then
            Me.Renderer = New MenuRenderer(_isMainMenu, _primaryColor, _menuItemTextColor)
            LoadMenuItemHeight()
        End If
    End Sub
End Class

Public Class MenuColorTable
    Inherits ProfessionalColorTable

    Private backColor As Color
    Private leftColumnColor As Color
    Private borderColor As Color
    Private menuItemBorderColor As Color
    Private menuItemSelectedColor As Color

    Public Sub New(ByVal isMainMenu As Boolean, ByVal primaryColor As Color)
        If isMainMenu Then
            backColor = Color.FromArgb(37, 39, 60)
            leftColumnColor = Color.FromArgb(32, 33, 51)
            borderColor = Color.FromArgb(32, 33, 51)
            menuItemBorderColor = primaryColor
            menuItemSelectedColor = primaryColor
        Else
            backColor = Color.White
            leftColumnColor = Color.LightGray
            borderColor = Color.LightGray
            menuItemBorderColor = primaryColor
            menuItemSelectedColor = primaryColor
        End If
    End Sub

    Public Overrides ReadOnly Property ToolStripDropDownBackground As Color
        Get
            Return backColor
        End Get
    End Property

    Public Overrides ReadOnly Property MenuBorder As Color
        Get
            Return borderColor
        End Get
    End Property

    Public Overrides ReadOnly Property MenuItemBorder As Color
        Get
            Return menuItemBorderColor
        End Get
    End Property

    Public Overrides ReadOnly Property MenuItemSelected As Color
        Get
            Return menuItemSelectedColor
        End Get
    End Property

    Public Overrides ReadOnly Property ImageMarginGradientBegin As Color
        Get
            Return leftColumnColor
        End Get
    End Property

    Public Overrides ReadOnly Property ImageMarginGradientMiddle As Color
        Get
            Return leftColumnColor
        End Get
    End Property

    Public Overrides ReadOnly Property ImageMarginGradientEnd As Color
        Get
            Return leftColumnColor
        End Get
    End Property
End Class

Public Class MenuRenderer
    Inherits ToolStripProfessionalRenderer

    Private primaryColor As Color
    Private textColor As Color
    Private arrowThickness As Integer

    Public Sub New(ByVal isMainMenu As Boolean, ByVal primaryColor As Color, ByVal textColor As Color)
        MyBase.New(New MenuColorTable(isMainMenu, primaryColor))
        Me.primaryColor = primaryColor

        If isMainMenu Then
            arrowThickness = 3

            If textColor = Color.Empty Then
                Me.textColor = Color.Gainsboro
            Else
                Me.textColor = textColor
            End If
        Else
            arrowThickness = 2

            If textColor = Color.Empty Then
                Me.textColor = Color.DimGray
            Else
                Me.textColor = textColor
            End If
        End If
    End Sub

    Protected Overrides Sub OnRenderItemText(ByVal e As ToolStripItemTextRenderEventArgs)
        MyBase.OnRenderItemText(e)
        e.Item.ForeColor = If(e.Item.Selected, Color.White, textColor)
    End Sub

    Protected Overrides Sub OnRenderArrow(ByVal e As ToolStripArrowRenderEventArgs)
        Dim graph = e.Graphics
        Dim arrowSize = New Size(5, 12)
        Dim arrowColor = If(e.Item.Selected, Color.White, primaryColor)
        Dim rect = New Rectangle(e.ArrowRectangle.Location.X, (e.ArrowRectangle.Height - arrowSize.Height) / 2, arrowSize.Width, arrowSize.Height)

        Using path As GraphicsPath = New GraphicsPath()

            Using pen As Pen = New Pen(arrowColor, arrowThickness)
                graph.SmoothingMode = SmoothingMode.AntiAlias
                path.AddLine(rect.Left, rect.Top, rect.Right, CInt((rect.Top + rect.Height) / 2))
                path.AddLine(rect.Right, CInt(rect.Top + rect.Height / 2), rect.Left, rect.Top + rect.Height)
                graph.DrawPath(pen, path)
            End Using
        End Using
    End Sub
End Class
