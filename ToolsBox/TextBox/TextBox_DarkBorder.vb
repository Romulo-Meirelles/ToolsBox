Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports ToolsBox.Controller
Imports ToolsBox.Utils


<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.TextBox))> _
<DefaultEvent("TextChanged")> _
Public Class TextBox_DarkBorder
    Inherits ThemeControl154

    Private _BackColor As Color
    <Category("ToolsBox Herramienta")> _
    Property BackColor_() As Color
        Get
            Return _BackColor
        End Get
        Set(ByVal value As Color)
            _BackColor = value
            If Base IsNot Nothing Then
                Base.BackColor = value
            End If
        End Set
    End Property

    Private _ForeColor As Color
    <Category("ToolsBox Herramienta")> _
    Property ForeColor_() As Color
        Get
            Return _ForeColor
        End Get
        Set(ByVal value As Color)
            _ForeColor = value

            If Base IsNot Nothing Then
                Base.ForeColor = value
            End If
        End Set
    End Property

    Private _Border As Integer
    <Category("ToolsBox Herramienta")> _
    Property Border() As Integer
        Get
            Return _Border
        End Get
        Set(ByVal value As Integer)
            _Border = value
        End Set
    End Property

    Private _BorderColor As Color
    <Category("ToolsBox Herramienta")> _
    Property BorderColor() As Color
        Get
            Return _BorderColor
        End Get
        Set(ByVal value As Color)
            _BorderColor = value
        End Set
    End Property

    Private _InternalBorderColor As Color
    <Category("ToolsBox Herramienta")> _
    Property InternalBorderColor() As Color
        Get
            Return _InternalBorderColor
        End Get
        Set(ByVal value As Color)
            _InternalBorderColor = value
        End Set
    End Property

    Private _PasswordChar As String
    <Category("ToolsBox Herramienta")> _
    Property PasswordChar() As String
        Get
            Return _PasswordChar
        End Get
        Set(ByVal value As String)
            _PasswordChar = value
            If Base IsNot Nothing Then
                Base.PasswordChar = value
            End If
        End Set
    End Property

    Private _TextAlign As HorizontalAlignment = HorizontalAlignment.Left
    <Category("ToolsBox Herramienta")> _
    Property TextAlign() As HorizontalAlignment
        Get
            Return _TextAlign
        End Get
        Set(ByVal value As HorizontalAlignment)
            _TextAlign = value
            If Base IsNot Nothing Then
                Base.TextAlign = value
            End If
        End Set
    End Property

    Private _MaxLength As Integer = 32767
    <Category("ToolsBox Herramienta")> _
    Property MaxLength() As Integer
        Get
            Return _MaxLength
        End Get
        Set(ByVal value As Integer)
            _MaxLength = value
            If Base IsNot Nothing Then
                Base.MaxLength = value
            End If
        End Set
    End Property

    Private _ReadOnly As Boolean
    <Category("ToolsBox Herramienta")> _
    Property [ReadOnly]() As Boolean
        Get
            Return _ReadOnly
        End Get
        Set(ByVal value As Boolean)
            _ReadOnly = value
            If Base IsNot Nothing Then
                Base.ReadOnly = value
            End If
        End Set
    End Property

    Private _UseSystemPasswordChar As Boolean
    <Category("ToolsBox Herramienta")> _
    Property UseSystemPasswordChar() As Boolean
        Get
            Return _UseSystemPasswordChar
        End Get
        Set(ByVal value As Boolean)
            _UseSystemPasswordChar = value
            If Base IsNot Nothing Then
                Base.UseSystemPasswordChar = value
            End If
        End Set
    End Property

    Private _Multiline As Boolean
    <Category("ToolsBox Herramienta")> _
    Property Multiline() As Boolean
        Get
            Return _Multiline
        End Get
        Set(ByVal value As Boolean)
            _Multiline = value
            If Base IsNot Nothing Then
                Base.Multiline = value

                If value Then
                    LockHeight = 0
                    Base.Height = Height - 11
                Else
                    LockHeight = Base.Height + 11
                End If
            End If
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
            If Base IsNot Nothing Then
                Base.Text = value
            End If
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Overrides Property Font As Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As Font)
            MyBase.Font = value
            If Base IsNot Nothing Then
                Base.Font = value
                Base.Location = New Point(3, 5)
                Base.Width = Width - 6

                If Not _Multiline Then
                    LockHeight = Base.Height + 11
                End If
            End If
        End Set
    End Property

    Protected Overrides Sub OnCreation()
        If Not Controls.Contains(Base) Then
            Controls.Add(Base)
        End If
    End Sub

    Private Base As TextBox
    Sub New()
        Base = New TextBox

        _Border = 0
        _ForeColor = Color.FromArgb(204, 204, 204)
        _BackColor = Color.FromArgb(32, 32, 32)
        _BorderColor = Color.Black
        _InternalBorderColor = Color.FromArgb(102, 102, 102)
        Base.Font = Font
        Base.Text = Text
        Base.MaxLength = _MaxLength
        Base.Multiline = _Multiline
        Base.ReadOnly = _ReadOnly
        Base.UseSystemPasswordChar = _UseSystemPasswordChar

        Base.BorderStyle = BorderStyle.None

        Base.Location = New Point(4, 4)
        Base.Width = Width - 10

        If _Multiline Then
            Base.Height = Height - 11
        Else
            LockHeight = Base.Height + 11
        End If

        AddHandler Base.TextChanged, AddressOf OnBaseTextChanged
        AddHandler Base.KeyDown, AddressOf OnBaseKeyDown
    End Sub


    Protected Overrides Sub ColorHook()
        Base.ForeColor = _ForeColor
        Base.BackColor = _BackColor
    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(_BackColor)
        DrawBorders(New Pen(_BorderColor))
        DrawBorders(New Pen(_InternalBorderColor), 1)
        DrawBorders(New Pen(_BorderColor), 2)
        DrawPixel(Color.FromArgb(46, 46, 46), 2, 2)
        DrawPixel(Color.FromArgb(46, 46, 46), Width - 3, 2)
        DrawPixel(Color.FromArgb(46, 46, 46), 2, Height - 3)
        DrawPixel(Color.FromArgb(46, 46, 46), Width - 3, Height - 3)
        CarregaBordas(Me, _Border, _Border)
    End Sub
    Private Sub OnBaseTextChanged(ByVal s As Object, ByVal e As EventArgs)
        Text = Base.Text
    End Sub
    Private Sub OnBaseKeyDown(ByVal s As Object, ByVal e As KeyEventArgs)
        If e.Control AndAlso e.KeyCode = Keys.A Then
            Base.SelectAll()
            e.SuppressKeyPress = True
        End If
    End Sub
    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        Base.Location = New Point(4, 5)
        Base.Width = Width - 8

        If _Multiline Then
            Base.Height = Height - 5
        End If


        MyBase.OnResize(e)
    End Sub

End Class

