Option Strict On
Imports System.Drawing.Text
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports ToolsBox
Imports ToolsBox.Utils.Bordas_Arredondadas
Imports ToolsBox.Utils

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public MustInherit Class MyControl
    Inherits Control
    Private G As Graphics, B As Bitmap

    Sub New()
        SetStyle(DirectCast(0, ControlStyles), True)
        B = New Bitmap(1, 1)
        G = Graphics.FromImage(B)
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        If Not Width = 0 AndAlso Not Height = 0 Then
            B = New Bitmap(Width, Height)
            G = Graphics.FromImage(B)
            Invalidate()
        End If
        MyBase.OnSizeChanged(e)
    End Sub

    MustOverride Sub PaintHook()
    Protected NotOverridable Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If Width = 0 OrElse Height = 0 Then Return
        PaintHook()
        e.Graphics.DrawImage(B, 0, 0)
    End Sub

End Class
<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Google_2FA
    Inherits MyControl
    Private WithEvents ToggleAnimation As Timer = New Timer With {.Interval = 1000}
    Private WithEvents CopiedTimer As Timer = New Timer With {.Interval = 3000}

    Private WithEvents TitleDeclared As New Label
    Private WithEvents CodeDeclared As New Label
    Private WithEvents CopiedDeclared As New Label
    Private WithEvents RadialDeclared As New ProgressBar_Radial_2
    Private WithEvents PictureDeclared As New PictureBox

    Private Executer As Boolean = False
    Private _Copied As Boolean
    Private _OldColor As Color

#Region "Public Property"

    Private _Title As String
    <Category("ToolsBox Herramienta")> _
    Public Property Title() As String
        Get
            Executer = False
            Return _Title
        End Get

        Set(V As String)
            Executer = False
            _Title = V
            Invalidate()
        End Set

    End Property

    Private _TitleColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property TitleColor() As Color
        Get
            Executer = False
            Return _TitleColor
        End Get

        Set(V As Color)
            Executer = False
            _TitleColor = V
            Invalidate()
        End Set

    End Property

    Private _TitleFont As Font
    <Category("ToolsBox Herramienta")> _
    Public Property TitleFont() As Font
        Get
            Executer = False
            Return _TitleFont
        End Get

        Set(V As Font)
            Executer = False
            _TitleFont = V
            Invalidate()
        End Set
    End Property

    Private _Code As String
    <Category("ToolsBox Herramienta")> _
    Public Property Code() As String
        Get
            Executer = False
            Return _Code
        End Get

        Set(V As String)
            Executer = False
            _Code = V
            Invalidate()
        End Set
    End Property

    Private _CodeColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property CodeColor() As Color
        Get
            Executer = False
            Return _CodeColor
        End Get

        Set(V As Color)
            Executer = False
            _CodeColor = V
            Invalidate()
        End Set
    End Property

    Private _CodeFont As Font
    <Category("ToolsBox Herramienta")> _
    Public Property CodeFont() As Font
        Get
            Executer = False
            Return _CodeFont
        End Get

        Set(V As Font)
            Executer = False
            _CodeFont = V
            Invalidate()
        End Set
    End Property

    Private _CopiedEnabled As Boolean
    <Category("ToolsBox Herramienta")> _
    Public Property CopiedEnabled() As Boolean
        Get
            Executer = False
            Return _CopiedEnabled
        End Get

        Set(V As Boolean)
            Executer = False
            _CopiedEnabled = V
            Invalidate()
        End Set

    End Property

    Private _CopiedText As String
    <Category("ToolsBox Herramienta")> _
    Public Property CopiedText() As String
        Get
            Executer = False
            Return _CopiedText
        End Get

        Set(V As String)
            Executer = False
            _CopiedText = V
            Invalidate()
        End Set

    End Property

    Private _CopiedColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property CopiedColor() As Color
        Get
            Executer = False
            Return _CopiedColor
        End Get

        Set(V As Color)
            Executer = False
            _CopiedColor = V
            Invalidate()
        End Set

    End Property

    Private _CopiedFont As Font
    <Category("ToolsBox Herramienta")> _
    Public Property CopiedFont() As Font
        Get
            Executer = False
            Return _CopiedFont
        End Get

        Set(V As Font)
            Executer = False
            _CopiedFont = V
            Invalidate()
        End Set
    End Property

    Private _Image As Image
    <Category("ToolsBox Herramienta")> _
    Public Property ImageLogo() As Image
        Get
            Executer = False
            Return _Image
        End Get

        Set(V As Image)
            Executer = False
            _Image = V
            Invalidate()
        End Set
    End Property

    Private _Maximum As Decimal
    <Category("ToolsBox Herramienta")> _
    Public Property Maximum As Decimal
        Get
            Executer = False
            Return _Maximum
        End Get

        Set(V As Decimal)
            Executer = False
            _Maximum = V
            Invalidate()
        End Set
    End Property

    Private _StartingAngle As Integer
    <Category("ToolsBox Herramienta")> _
    Public Property StartingAngle As Integer
        Get
            Executer = False
            Return _StartingAngle
        End Get

        Set(V As Integer)
            Executer = False
            _StartingAngle = V
            Invalidate()
        End Set
    End Property

    Private _RotationAngle As Integer
    <Category("ToolsBox Herramienta")> _
    Public Property RotationAngle As Integer
        Get
            Executer = False
            Return _RotationAngle
        End Get

        Set(V As Integer)
            Executer = False
            _RotationAngle = V
            Invalidate()
        End Set
    End Property

    Private _Value As Decimal
    <Category("ToolsBox Herramienta")> _
    Public Property Value As Decimal
        Get
            Executer = False
            Return _Value
        End Get

        Set(V As Decimal)
            Executer = False
            _Value = V
            Invalidate()
        End Set
    End Property

    Private _Clocks As Boolean
    <Category("ToolsBox Herramienta")> _
    Public Property Clocks() As Boolean
        Get
            Executer = False
            Return _Clocks
        End Get

        Set(V As Boolean)
            Executer = False
            _Clocks = V
            Invalidate()
        End Set

    End Property

    Private _Border As Size
    <Category("ToolsBox Herramienta")> _
    Public Property Border() As Size
        Get
            Executer = False
            Return _Border
        End Get

        Set(V As Size)
            Executer = False
            _Border = V
            Invalidate()
        End Set

    End Property

    Private _SelectedColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property SelectedColor() As Color
        Get
            Executer = False
            Return _SelectedColor
        End Get

        Set(V As Color)
            Executer = False
            _SelectedColor = V
            Invalidate()
        End Set

    End Property

    Private _FixedSize As Boolean
    <Category("ToolsBox Herramienta")> _
    Public Property FixedSize() As Boolean
        Get
            Executer = False
            Return _FixedSize
        End Get

        Set(V As Boolean)
            Executer = False
            _FixedSize = V
            Invalidate()
        End Set

    End Property

#End Region

    Sub New()

        _Title = "Sevice Name:"
        _Code = "000 000"
        _TitleColor = Color.Silver
        _CodeColor = Color.Silver
        _CodeFont = New System.Drawing.Font("Microsoft Sans Serif", 17.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        _CopiedEnabled = True
        _CopiedText = "Copied!"
        _CopiedColor = System.Drawing.Color.Lime
        _CopiedFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        _Image = My.Resources.Google_Authenticator_24x24
        _Value = 60
        _Maximum = 60
        _StartingAngle = 275
        _RotationAngle = 360
        _Clocks = True
        _Copied = False
        _Border = New Size(5, 5)
        _SelectedColor = Color.DarkViolet
        _FixedSize = False
        Me.BackColor = Color.FromArgb(25, 30, 44)
        _OldColor = Color.FromArgb(25, 30, 44)

        CopiedDeclared.Anchor = CType(System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        TitleDeclared.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left, System.Windows.Forms.AnchorStyles)
        CodeDeclared.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        RadialDeclared.Anchor = CType(System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
        PictureDeclared.Anchor = CType(System.Windows.Forms.AnchorStyles.Left, System.Windows.Forms.AnchorStyles)

        TitleDeclared.Location = New System.Drawing.Point(15, 5)
        RadialDeclared.Location = New System.Drawing.Point(183, 23)
        CodeDeclared.Location = New System.Drawing.Point(15, 24)
        CopiedDeclared.Location = New System.Drawing.Point(20, 51)
        PictureDeclared.Location = New System.Drawing.Point(20, 24)

        TitleDeclared.Size = New System.Drawing.Size(198, 14)
        CopiedDeclared.Size = New System.Drawing.Size(186, 15)
        CodeDeclared.Size = New System.Drawing.Size(199, 23)
        PictureDeclared.Size = New System.Drawing.Size(24, 24)
        RadialDeclared.Size = New System.Drawing.Size(24, 24)

        Me.Size = New Size(220, 70)
        MinimumSize = New Size(220, 70)

    End Sub

    Protected Overrides Sub CreateHandle()
        MyBase.CreateHandle()
        AddHandler TitleDeclared.Click, AddressOf TitleClick
        AddHandler CodeDeclared.Click, AddressOf CopyClick
        AddHandler PictureDeclared.Click, AddressOf PictureClick
        AddHandler RadialDeclared.Click, AddressOf RadialClick
        ToggleAnimation.Start()
    End Sub

    Private Sub TitleClick(sender As Object, e As System.EventArgs)
        Try
            If Me.BackColor = _SelectedColor Then

            Else
                Me.Focus()
                _OldColor = Me.BackColor
                Me.BackColor = _SelectedColor
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CopyClick(sender As Object, e As System.EventArgs)

        Try
            Dim G As String = CodeDeclared.Text.Replace(" ", "").Replace("-", "").Replace("_", "").Replace("+", "").Replace(".", "").Replace(",", "")
            CopiedTimer.Stop()
            If _CopiedEnabled = False Then
                _Copied = False
            Else
                _Copied = True
            End If
            Clipboard.SetText(G)
            CopiedTimer.Start()

            If Me.BackColor = _SelectedColor Then
            Else
                Me.Focus()
                _OldColor = Me.BackColor
                Me.BackColor = _SelectedColor
            End If

            Executer = False
        Catch ex As Exception
        End Try

        Invalidate()
    End Sub

    Private Sub RadialClick(sender As Object, e As System.EventArgs)
        Try
            If Me.BackColor = _SelectedColor Then

            Else
                Me.Focus()
                _OldColor = Me.BackColor
                Me.BackColor = _SelectedColor
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PictureClick(sender As Object, e As System.EventArgs)
        Try
            If Me.BackColor = _SelectedColor Then

            Else
                Me.Focus()
                _OldColor = Me.BackColor
                Me.BackColor = _SelectedColor
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub PaintHook()

        If Executer = False Then

            'Sevice_lbl
            TitleDeclared.BackColor = System.Drawing.Color.Transparent
            TitleDeclared.ForeColor = _TitleColor
            TitleDeclared.Name = "Sevice_lbl"
            TitleDeclared.TabIndex = 0
            TitleDeclared.Text = _Title
            TitleDeclared.Font = _TitleFont
            TitleDeclared.TextAlign = System.Drawing.ContentAlignment.MiddleLeft

            'Code_lbl()
            CodeDeclared.BackColor = System.Drawing.Color.Transparent
            CodeDeclared.Font = _CodeFont
            CodeDeclared.ForeColor = _CodeColor
            CodeDeclared.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            CodeDeclared.Name = "Code_lbl"
            CodeDeclared.MinimumSize = New System.Drawing.Size(199, 23)
            CodeDeclared.Cursor = System.Windows.Forms.Cursors.Hand
            CodeDeclared.TabIndex = 1
            CodeDeclared.Text = _Code
            CodeDeclared.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

            'Picture
            PictureDeclared.BackColor = System.Drawing.Color.Transparent
            PictureDeclared.Name = "PictureBox_"
            PictureDeclared.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            PictureDeclared.TabIndex = 2
            PictureDeclared.TabStop = False
            PictureDeclared.Image = _Image
            PictureDeclared.MinimumSize = New System.Drawing.Size(24, 24)
            PictureDeclared.MaximumSize = New System.Drawing.Size(24, 24)

            ' VisualStudioRadialProgressBar
            RadialDeclared.BackColor = System.Drawing.Color.Transparent
            RadialDeclared.BaseColour = System.Drawing.Color.Transparent
            RadialDeclared.BorderColour = System.Drawing.Color.Transparent
            RadialDeclared.Maximum = _Maximum
            RadialDeclared.Name = "ProgressBar_Radial_2_"
            RadialDeclared.ProgressColour = System.Drawing.Color.FromArgb(153, 153, 153)
            RadialDeclared.RotationAngle = _RotationAngle
            RadialDeclared.MaximumSize = New System.Drawing.Size(24, 24)
            RadialDeclared.MinimumSize = New System.Drawing.Size(24, 24)
            RadialDeclared.StartingAngle = _StartingAngle
            RadialDeclared.TabIndex = 3
            RadialDeclared.Text = "ProgressBar_Radial_2_"
            RadialDeclared.TextColour = System.Drawing.Color.Transparent
            RadialDeclared.Value = _Value

            'Copied
            CopiedDeclared.BackColor = System.Drawing.Color.Transparent
            CopiedDeclared.ForeColor = _CopiedColor
            CopiedDeclared.Name = "Copied_lbl"
            CopiedDeclared.Font = _CopiedFont
            CopiedDeclared.TabIndex = 4
            CopiedDeclared.Text = _CopiedText
            CopiedDeclared.Visible = _Copied
            CopiedDeclared.TextAlign = System.Drawing.ContentAlignment.MiddleCenter

            Me.Controls.Add(TitleDeclared)
            Me.Controls.Add(CodeDeclared)
            Me.Controls.Add(PictureDeclared)
            Me.Controls.Add(RadialDeclared)
            Me.Controls.Add(CopiedDeclared)

            CarregaBordas(Me, _Border.Height, _Border.Width)

            PictureDeclared.BringToFront()
            RadialDeclared.BringToFront()
            CopiedDeclared.BringToFront()

            If _FixedSize = True Then
                Me.MaximumSize = Me.MinimumSize
            Else
                Me.MaximumSize = New Size(0, 0)
            End If

            If _Clocks = False Then
                ToggleAnimation.Stop()
            Else
                ToggleAnimation.Start()
            End If

            Executer = True
        Else

        End If


    End Sub

    Private Sub Animation() Handles ToggleAnimation.Tick
        If Me.Value = -_Maximum + 1 Then
            Me.Value = _Maximum
        Else
            Me.Value -= 1
        End If

        Invalidate()
    End Sub

    Private Sub CopiedAnimation() Handles CopiedTimer.Tick
        Try
            Executer = False
            CopiedDeclared.Text = "Agora..."
            _Copied = False
            CopiedTimer.Stop()
        Catch ex As Exception
        End Try

        Invalidate()
    End Sub

    Protected Overrides Sub OnClick(e As System.EventArgs)
        Try
            If Me.BackColor = _SelectedColor Then

            Else
                Me.Focus()
                _OldColor = Me.BackColor
                Me.BackColor = _SelectedColor
            End If
        Catch ex As Exception
        End Try
        MyBase.OnClick(e)

    End Sub

    Protected Overrides Sub OnLostFocus(e As System.EventArgs)
        Try
            Me.BackColor = _OldColor
        Catch ex As Exception
        End Try
        MyBase.OnLeave(e)
        MyBase.OnLostFocus(e)
    End Sub
End Class

