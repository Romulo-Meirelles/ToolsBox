Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.TextBox))> _
<DefaultEvent("_TextChanged")>
Partial Public Class TextBox_Diferenciado
    Inherits UserControl

    Private _borderColor As Color = Color.MediumSlateBlue
    Private _borderFocusColor As Color = Color.HotPink
    Private _borderSize As Integer = 2
    Private _underlinedStyle As Boolean = False
    Private isFocused As Boolean = False
    Private _borderRadius As Integer = 0
    Private _placeholderColor As Color = Color.DarkGray
    Private _placeholderText As String = ""
    '  Private _PlaceHolderEnable As Boolean = True
    Private isPlaceholder As Boolean = False
    Private isPasswordChar As Boolean = False
    Public Event _TextChanged As EventHandler

    Public Sub New()
        InitializeComponent()
    End Sub

    <Category("ToolsBox Herramientas")>
    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set(ByVal value As Color)
            _borderColor = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramientas")>
    Public Property BorderFocusColor As Color
        Get
            Return _borderFocusColor
        End Get
        Set(ByVal value As Color)
            _borderFocusColor = value
        End Set
    End Property

    <Category("ToolsBox Herramientas")>
    Public Property BorderSize As Integer
        Get
            Return _borderSize
        End Get
        Set(ByVal value As Integer)

            If value >= 1 Then
                _borderSize = value
                Me.Invalidate()
            End If
        End Set
    End Property

    <Category("ToolsBox Herramientas")>
    Public Property UnderlinedStyle As Boolean
        Get
            Return _underlinedStyle
        End Get
        Set(ByVal value As Boolean)
            _underlinedStyle = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramientas")>
    Public Property PasswordChar As Boolean
        Get
            Return isPasswordChar
        End Get
        Set(ByVal value As Boolean)
            isPasswordChar = value
            If Not isPlaceholder Then textBox1.UseSystemPasswordChar = value
        End Set
    End Property

    <Category("ToolsBox Herramientas")>
    Public Property Multiline As Boolean
        Get
            Return textBox1.Multiline
        End Get
        Set(ByVal value As Boolean)
            textBox1.Multiline = value
        End Set
    End Property

    <Category("ToolsBox Herramientas")>
    Public Overrides Property BackColor As Color
        Get
            Return MyBase.BackColor
        End Get
        Set(ByVal value As Color)
            MyBase.BackColor = value
            textBox1.BackColor = value
        End Set
    End Property

    <Category("ToolsBox Herramientas")>
    Public Overrides Property ForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(ByVal value As Color)
            MyBase.ForeColor = value
            textBox1.ForeColor = value
        End Set
    End Property

    <Category("ToolsBox Herramientas")>
    Public Overrides Property Font As Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As Font)
            MyBase.Font = value
            textBox1.Font = value
            If Me.DesignMode Then UpdateControlHeight()
        End Set
    End Property

    <Category("ToolsBox Herramientas")>
    Public Property Texts As String
        Get
            If isPlaceholder Then
                Return ""
            Else
                Return textBox1.Text
            End If
        End Get
        Set(ByVal value As String)
            textBox1.Text = value
            SetPlaceholder()
        End Set
    End Property

    <Category("ToolsBox Herramientas")>
    Public Property BorderRadius As Integer
        Get
            Return _borderRadius
        End Get
        Set(ByVal value As Integer)

            If value >= 0 Then
                _borderRadius = value
                Me.Invalidate()
            End If
        End Set
    End Property

    '<Category("ToolsBox Herramientas")>
    'Public Property PlaceHolderEnable As Boolean
    '    Get
    '        Return _PlaceHolderEnable
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _PlaceHolderEnable = value
    '    End Set
    'End Property

    <Category("ToolsBox Herramientas")>
    Public Property PlaceholderColor As Color
        Get
            Return _placeholderColor
        End Get
        Set(ByVal value As Color)
            _placeholderColor = value
            If isPlaceholder Then textBox1.ForeColor = value
        End Set
    End Property

    <Category("ToolsBox Herramientas")>
    Public Property PlaceHolderText As String
        Get
            Return _placeholderText
        End Get
        Set(ByVal value As String)
            _placeholderText = value
            textBox1.Text = ""
            SetPlaceholder()
        End Set
    End Property

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        If Me.DesignMode Then UpdateControlHeight()
    End Sub

    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        MyBase.OnLoad(e)
        UpdateControlHeight()
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim graph As Graphics = e.Graphics

        If _borderRadius > 1 Then
            Dim rectBorderSmooth = Me.ClientRectangle
            Dim rectBorder = Rectangle.Inflate(rectBorderSmooth, -_borderSize, -_borderSize)
            Dim smoothSize As Integer = If(_borderSize > 0, _borderSize, 1)

            Using pathBorderSmooth As GraphicsPath = GetFigurePath(rectBorderSmooth, _borderRadius)

                Using pathBorder As GraphicsPath = GetFigurePath(rectBorder, _borderRadius - _borderSize)

                    Using penBorderSmooth As Pen = New Pen(Me.Parent.BackColor, smoothSize)

                        Using penBorder As Pen = New Pen(_borderColor, _borderSize)
                            Me.Region = New Region(pathBorderSmooth)
                            If _borderRadius > 15 Then SetTextBoxRoundedRegion()
                            graph.SmoothingMode = SmoothingMode.AntiAlias
                            penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Center
                            If isFocused Then penBorder.Color = _borderFocusColor

                            If _underlinedStyle Then
                                graph.DrawPath(penBorderSmooth, pathBorderSmooth)
                                graph.SmoothingMode = SmoothingMode.None
                                graph.DrawLine(penBorder, 0, Me.Height - 1, Me.Width, Me.Height - 1)
                            Else
                                graph.DrawPath(penBorderSmooth, pathBorderSmooth)
                                graph.DrawPath(penBorder, pathBorder)
                            End If
                        End Using
                    End Using
                End Using
            End Using
        Else

            Using penBorder As Pen = New Pen(_borderColor, _borderSize)
                Me.Region = New Region(Me.ClientRectangle)
                penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset
                If isFocused Then penBorder.Color = _borderFocusColor

                If _underlinedStyle Then
                    graph.DrawLine(penBorder, 0, Me.Height - 1, Me.Width, Me.Height - 1)
                Else
                    graph.DrawRectangle(penBorder, 0, 0, Me.Width - 0.5F, Me.Height - 0.5F)
                End If
            End Using
        End If
    End Sub

    Private Sub SetPlaceholder()
        If String.IsNullOrWhiteSpace(textBox1.Text) AndAlso _placeholderText <> "" Then
            isPlaceholder = True
            textBox1.Text = _placeholderText
            textBox1.ForeColor = _placeholderColor
            If isPasswordChar Then textBox1.UseSystemPasswordChar = False
        End If
    End Sub

    Private Sub RemovePlaceholder()
        If isPlaceholder AndAlso _placeholderText <> "" Then
            isPlaceholder = False
            textBox1.Text = ""
            textBox1.ForeColor = Me.ForeColor
            If isPasswordChar Then textBox1.UseSystemPasswordChar = True
        End If
    End Sub

    Private Function GetFigurePath(ByVal rect As Rectangle, ByVal radius As Integer) As GraphicsPath
        Dim path As GraphicsPath = New GraphicsPath()
        Dim curveSize As Single = radius * 2.0F
        path.StartFigure()
        path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90)
        path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90)
        path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90)
        path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90)
        path.CloseFigure()
        Return path
    End Function

    Private Sub SetTextBoxRoundedRegion()
        Dim pathTxt As GraphicsPath

        If Multiline Then
            pathTxt = GetFigurePath(textBox1.ClientRectangle, _borderRadius - _borderSize)
            textBox1.Region = New Region(pathTxt)
        Else
            pathTxt = GetFigurePath(textBox1.ClientRectangle, _borderSize * 2)
            textBox1.Region = New Region(pathTxt)
        End If

        pathTxt.Dispose()
    End Sub

    Private Sub UpdateControlHeight()
        If textBox1.Multiline = False Then
            Dim txtHeight As Integer = TextRenderer.MeasureText("Text", Me.Font).Height + 1
            textBox1.Multiline = True
            textBox1.MinimumSize = New Size(0, txtHeight)
            textBox1.Multiline = False
            Me.Height = textBox1.Height + Me.Padding.Top + Me.Padding.Bottom
        End If
    End Sub

    Private Sub textBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent _TextChanged(sender, e)
    End Sub

    Private Sub textBox1_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.OnClick(e)
    End Sub

    Private Sub textBox1_MouseEnter(ByVal sender As Object, ByVal e As EventArgs)
        Me.OnMouseEnter(e)
    End Sub

    Private Sub textBox1_MouseLeave(ByVal sender As Object, ByVal e As EventArgs)
        Me.OnMouseLeave(e)
    End Sub

    Private Sub textBox1_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        Me.OnKeyPress(e)
    End Sub

    Private Sub textBox1_Enter(ByVal sender As Object, ByVal e As EventArgs)
        'If _PlaceHolderEnable = True Then
        isFocused = True
        Me.Invalidate()
        RemovePlaceholder()
        'Else
        'End If

    End Sub

    Private Sub textBox1_Leave(ByVal sender As Object, ByVal e As EventArgs)
        'If _PlaceHolderEnable = True Then
        isFocused = False
        Me.Invalidate()
        SetPlaceholder()
        'Else
        'End If

    End Sub
End Class

Partial Public Class TextBox_Diferenciado
    Inherits UserControl
    Private components As System.ComponentModel.IContainer = Nothing

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            components.Dispose()
        End If

        MyBase.Dispose(disposing)
    End Sub

    Private Sub InitializeComponent()
        Me.textBox1 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        Me.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.textBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.textBox1.Location = New System.Drawing.Point(10, 7)
        Me.textBox1.Name = "TextBox_Dif"
        Me.textBox1.Size = New System.Drawing.Size(230, 15)
        Me.textBox1.TabIndex = 0
        AddHandler Me.textBox1.Click, New System.EventHandler(AddressOf Me.textBox1_Click)
        AddHandler Me.textBox1.TextChanged, New System.EventHandler(AddressOf Me.textBox1_TextChanged)
        AddHandler Me.textBox1.Enter, New System.EventHandler(AddressOf Me.textBox1_Enter)
        AddHandler Me.textBox1.KeyPress, New System.Windows.Forms.KeyPressEventHandler(AddressOf Me.textBox1_KeyPress)
        AddHandler Me.textBox1.Leave, New System.EventHandler(AddressOf Me.textBox1_Leave)
        AddHandler Me.textBox1.MouseEnter, New System.EventHandler(AddressOf Me.textBox1_MouseEnter)
        AddHandler Me.textBox1.MouseLeave, New System.EventHandler(AddressOf Me.textBox1_MouseLeave)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.Controls.Add(Me.textBox1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte((0))))
        Me.ForeColor = System.Drawing.Color.FromArgb((CInt(((CByte((64)))))), (CInt(((CByte((64)))))), (CInt(((CByte((64)))))))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "TextBox_Dif"
        Me.Padding = New System.Windows.Forms.Padding(10, 7, 10, 7)
        Me.Size = New System.Drawing.Size(250, 30)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Private textBox1 As System.Windows.Forms.TextBox
End Class

