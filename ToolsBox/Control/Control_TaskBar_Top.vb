Imports System.ComponentModel
Imports System.Threading
Imports System.Delegate
Imports System.Drawing.Drawing2D
Imports ToolsBox.Utils.Mouse_Objeto

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(UserControl))> _
Public Class Control_TaskBar_Top
    Inherits Control
    
    'DECLARAÇÕES COM EVENTOS
    Friend WithEvents Panel1 As New System.Windows.Forms.Panel
    Friend WithEvents Label1 As New System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As New System.Windows.Forms.PictureBox
    Friend WithEvents Control_Box_Five1 As New ToolsBox.Control_Box_Five
    Friend WithEvents Control_Box_Close As New ToolsBox.Control_Box_Eight_Close
    Friend WithEvents Control_Box_Max As New ToolsBox.Control_Box_Eight_Max
    Friend WithEvents Control_Box_Mini As New ToolsBox.Control_Box_Eight_Mini
    Friend WithEvents Control_Box_Hide As New ToolsBox.Control_Box_Eight_Hide
    Friend WithEvents PopupMenu As New ContextMenuStrip()
    Friend WithEvents RestaurarMenu As New ToolStripMenuItem("Restaurar")
    Friend WithEvents MoverMenu As New ToolStripMenuItem("Mover")
    Friend WithEvents TamanhoMenu As New ToolStripMenuItem("Tamanho")
    Friend WithEvents MinimizarMenu As New ToolStripMenuItem("Minimizar")
    Friend WithEvents MaximizarMenu As New ToolStripMenuItem("Maximizar")
    Friend WithEvents FecharMenu As New ToolStripMenuItem("Fechar")
    Enum Modifier As Integer
        Size_16X16 = 0
        Size_20X20 = 1
        Size_24X24 = 2
    End Enum

    Enum Closerme As Integer
        MeExit = 0
        MeClose = 1
        MeHide = 2
    End Enum

    'PROPRIEDADES PUBLICAS
    Private _TaskColor As Color = Color.FromArgb(25, 30, 44)
    <Category("ToolsBox Herramienta")> _
    Public Property TaskColor As Color
        Get
            Return _TaskColor
        End Get
        Set(ByVal value As Color)
            _TaskColor = value
            Me.Invalidate()
        End Set
    End Property
    Private _TitleText As String = "My Title."
    <Category("ToolsBox Herramienta")> _
    Public Property TitleText As String
        Get
            Return _TitleText
        End Get
        Set(ByVal value As String)
            _TitleText = value
            Me.Invalidate()
        End Set
    End Property
    <Category("ToolsBox Herramienta")> _
        Public Property Text As String
        Get
            Return _TitleText
        End Get
        Set(ByVal value As String)
            _TitleText = value
            Me.Invalidate()
        End Set
    End Property
    Private _TitleColor As Color = Color.White
    <Category("ToolsBox Herramienta")> _
    Public Property TitleColor As Color
        Get
            Return _TitleColor
        End Get
        Set(ByVal value As Color)
            _TitleColor = value
            Me.Invalidate()
        End Set
    End Property
    Private _TitleFont As Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!)
    <Category("ToolsBox Herramienta")> _
    Public Property TitleFont As Font
        Get
            Return _TitleFont
        End Get
        Set(ByVal value As Font)
            _TitleFont = value
            Me.Invalidate()
        End Set
    End Property
    Private _Icon As Image = My.Resources.Icon
    <Category("ToolsBox Herramienta")> _
    Public Property Icon As Image
        Get
            Return _Icon
        End Get
        Set(ByVal value As Image)
            _Icon = value
            Me.Invalidate()
        End Set
    End Property
    Private _IconEnable As Boolean = True
    <Category("ToolsBox Herramienta")> _
    Public Property IconEnable As Boolean
        Get
            Return _IconEnable
        End Get
        Set(ByVal value As Boolean)
            _IconEnable = value
            Me.Invalidate()
        End Set
    End Property
    Private _IconSize As Modifier
    <Category("ToolsBox Herramienta")> _
    Public Property IconSize As Modifier
        Get
            Return _IconSize
        End Get
        Set(ByVal value As Modifier)
            _IconSize = value
            Me.Invalidate()
        End Set
    End Property
    Private _TitleEnable As Boolean = True
    <Category("ToolsBox Herramienta")> _
    Public Property TitleEnable As Boolean
        Get
            Return _TitleEnable
        End Get
        Set(ByVal value As Boolean)
            _TitleEnable = value
            Me.Invalidate()
        End Set
    End Property
    Private _ControlBoxEnable As Boolean = True
    <Category("ToolsBox Herramienta")> _
    Public Property ControlBoxEnable As Boolean
        Get
            Return _ControlBoxEnable
        End Get
        Set(ByVal value As Boolean)
            _ControlBoxEnable = value
            Me.Invalidate()
        End Set
    End Property
    Private _TaskBarClickRight As Boolean = True
    <Category("ToolsBox Herramienta")> _
    Public Property TaskBarClickRightMenu As Boolean
        Get
            Return _TaskBarClickRight
        End Get
        Set(ByVal value As Boolean)
            _TaskBarClickRight = value
            Me.Invalidate()
        End Set
    End Property
    Private _CloseToClose As Closerme
    <Category("ToolsBox Herramienta")> _
    Public Property CloseToClose As Closerme
        Get
            Return _CloseToClose
        End Get
        Set(ByVal value As Closerme)
            _CloseToClose = value
            Me.Invalidate()
        End Set
    End Property
    Private _CloseEnableNew As Boolean = False
    <Category("ToolsBox Herramienta")> _
    Public Property CloseEnable As Boolean
        Get
            Return _CloseEnableNew
        End Get
        Set(ByVal value As Boolean)
            _CloseEnableNew = value
            Me.Invalidate()
        End Set
    End Property
    Private _MaxEnableNew As Boolean = False
    <Category("ToolsBox Herramienta")> _
    Public Property MaxEnable As Boolean
        Get
            Return _MaxEnableNew
        End Get
        Set(ByVal value As Boolean)
            _MaxEnableNew = value
            Me.Invalidate()
        End Set
    End Property
    Private _MiniEnableNew As Boolean = False
    <Category("ToolsBox Herramienta")> _
    Public Property MiniEnable As Boolean
        Get
            Return _MiniEnableNew
        End Get
        Set(ByVal value As Boolean)
            _MiniEnableNew = value
            Me.Invalidate()
        End Set
    End Property
    Private _HideEnableNew As Boolean = False
    <Category("ToolsBox Herramienta")> _
        Public Property HideEnable As Boolean
        Get
            Return _HideEnableNew
        End Get
        Set(ByVal value As Boolean)
            _HideEnableNew = value
            Me.Invalidate()
        End Set
    End Property
    Private _MouseStateRetangle As Boolean = True
    <Category("ToolsBox Herramienta")> _
        Public Property MouseStateRetangle As Boolean
        Get
            Return _MouseStateRetangle
        End Get
        Set(value As Boolean)
            _MouseStateRetangle = value
        End Set
    End Property
    Private _Tooltip As Boolean = True
    <Category("ToolsBox Herramienta")> _
    Public Property ToolTip As Boolean
        Get
            Return _Tooltip
        End Get
        Set(ByVal value As Boolean)
            _Tooltip = value
            Me.Invalidate()
        End Set
    End Property
    Private _ControlColorButtonBorder As Color = System.Drawing.Color.FromArgb(CType(CType(53, Byte), Integer), CType(CType(63, Byte), Integer), CType(CType(89, Byte), Integer))
    <Category("ToolsBox Herramienta")> _
        Public Property ControlColorButtonBorder As Color
        Get
            Return _ControlColorButtonBorder
        End Get
        Set(ByVal value As Color)
            _ControlColorButtonBorder = value
            Me.Invalidate()
        End Set
    End Property
    Private _ControlColorButtonOne As Color = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(44, Byte), Integer))
    <Category("ToolsBox Herramienta")> _
    Public Property ControlColorButtonOne As Color
        Get
            Return _ControlColorButtonOne
        End Get
        Set(ByVal value As Color)
            _ControlColorButtonOne = value
            Me.Invalidate()
        End Set
    End Property
    Private _ControlColorButtonTwo As Color = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(9, Byte), Integer), CType(CType(33, Byte), Integer))
    <Category("ToolsBox Herramienta")> _
    Public Property ControlColorButtonTwo As Color
        Get
            Return _ControlColorButtonTwo
        End Get
        Set(ByVal value As Color)
            _ControlColorButtonTwo = value
            Me.Invalidate()
        End Set
    End Property
    Private _ControlColorButtonItem As Color = Color.White
    <Category("ToolsBox Herramienta")> _
    Public Property ControlColorButtonItem As Color
        Get
            Return _ControlColorButtonItem
        End Get
        Set(ByVal value As Color)
            _ControlColorButtonItem = value
            Me.Invalidate()
        End Set
    End Property
    Private _borderRadius As Integer = 0
    <Category("ToolsBox Herramienta")> _
    Public Property BorderRadius As Integer
        Get
            Return _borderRadius
        End Get
        Set(ByVal value As Integer)
            _borderRadius = value
            Me.Invalidate()
        End Set
    End Property
    Private _Notifyicon As Boolean = True
    <Category("ToolsBox Herramienta")> _
    Public Property Notifyicon As Boolean
        Get
            Return _Notifyicon
        End Get
        Set(value As Boolean)
            _Notifyicon = value
        End Set
    End Property
    Private _NotifyIconTitle As String
    <Category("ToolsBox Herramienta")> _
    Public Property NotifyIconTitle As String
        Get
            Return _NotifyIconTitle
        End Get
        Set(value As String)
            _NotifyIconTitle = value
        End Set
    End Property
    Private _NotifyIconText As String
    <Category("ToolsBox Herramienta")> _
    Public Property NotifyIconText As String
        Get
            Return _NotifyIconText
        End Get
        Set(value As String)
            _NotifyIconText = value
        End Set
    End Property
    Private _BalloonTipTime As Integer
    <Category("ToolsBox Herramienta")> _
    Public Property BalloonTipTime As Integer
        Get
            Return _BalloonTipTime
        End Get
        Set(value As Integer)
            _BalloonTipTime = value
        End Set
    End Property
    Private _NotifyIconIcon As Drawing.Icon = My.Resources.TaskBar_Icon
    <Category("ToolsBox Herramienta")>
    Public Property NotifyIconIcon As Drawing.Icon
        Get
            Return _NotifyIconIcon
        End Get
        Set(value As Drawing.Icon)
            _NotifyIconIcon = value
        End Set
    End Property
    Private _TwoClicks As Boolean = True
    <Category("ToolsBox Herramienta")>
    Public Property TwoClicks As Boolean
        Get
            Return _TwoClicks
        End Get
        Set(value As Boolean)
            _TwoClicks = value
            Invalidate()
        End Set
    End Property

    Public Sub New()

        Me.Label1.Text = _TitleText
        Me.Panel1.Text = _TitleText
        Me.Text = _TitleText
        PopupMenu.Items.Add(RestaurarMenu)
        PopupMenu.Items.Add(MoverMenu) : MoverMenu.Enabled = False
        PopupMenu.Items.Add(TamanhoMenu) : TamanhoMenu.Enabled = False
        PopupMenu.Items.Add(MinimizarMenu)
        PopupMenu.Items.Add(MaximizarMenu) : MaximizarMenu.Enabled = False
        PopupMenu.Items.Add(FecharMenu)
        RestaurarMenu.Image = My.Resources.Restaure_
        MinimizarMenu.Image = My.Resources.Minimize_
        MaximizarMenu.Image = My.Resources.Maximize_
        FecharMenu.Image = My.Resources.Close_
        PopupMenu.ShowItemToolTips = True

        Me.Size = New Size(1000, 35)
    End Sub
    Protected Overrides Sub InitLayout()
        Me.FindForm.FormBorderStyle = FormBorderStyle.None
        Me.FindForm.BackColor = Color.FromArgb(23, 23, 23)
        Me.FindForm.StartPosition = FormStartPosition.CenterScreen
        Me.FindForm.Text = _TitleText
    End Sub

    Protected Overrides Sub OnPaint(ByVal pevent As PaintEventArgs)
        Dim G = Me.Height - 20
        MyBase.OnPaint(pevent)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Control_Box_Five1 = New ToolsBox.Control_Box_Five()
        Me.Control_Box_Close = New ToolsBox.Control_Box_Eight_Close
        Me.Control_Box_Max = New ToolsBox.Control_Box_Eight_Max
        Me.Control_Box_Mini = New ToolsBox.Control_Box_Eight_Mini
        Me.Control_Box_Hide = New ToolsBox.Control_Box_Eight_Hide
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        '
        'Panel1
        '

        Me.Dock = DockStyle.Top
        Me.Panel1.Size = New System.Drawing.Size(500, 1000)
        Me.Panel1.BackColor = _TaskColor
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Name = "Panel1"
        Me.Panel1.TabIndex = 0


        If _IconEnable = True Then
            Me.Panel1.Controls.Add(Me.PictureBox1)
        End If

        If _TitleEnable = True Then
            Me.Panel1.Controls.Add(Me.Label1)
        End If


        'Close, Max and Min
        If _CloseEnableNew = True Then
            Me.Panel1.Controls.Add(Me.Control_Box_Close)
            Me._ControlBoxEnable = False
        End If

        If _MaxEnableNew = True Then
            Me.Panel1.Controls.Add(Me.Control_Box_Max)
            Me._ControlBoxEnable = False
        End If

        If _MiniEnableNew = True Then
            Me.Panel1.Controls.Add(Me.Control_Box_Mini)
            Me._ControlBoxEnable = False
        End If

        If _HideEnableNew = True Then
            Me.Panel1.Controls.Add(Me.Control_Box_Hide)
            Me._ControlBoxEnable = False
        End If

        If _ControlBoxEnable = True Then
            Me.Panel1.Controls.Add(Me.Control_Box_Five1)
        End If

        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = _TitleColor
        Me.Label1.Name = "Label1"
        Me.Label1.Font = _TitleFont
        Me.Label1.Size = New System.Drawing.Size(99, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = _TitleText
        '
        'Control_Box_Five1
        '
        Me.Control_Box_Five1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right Or AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Control_Box_Five1.BackColor = System.Drawing.Color.Transparent
        Me.Control_Box_Five1.BackColorReal = System.Drawing.Color.Transparent
        Me.Control_Box_Five1.ColorBorder = _ControlColorButtonBorder
        Me.Control_Box_Five1.ColorButtonOne = _ControlColorButtonOne
        Me.Control_Box_Five1.ColorButtonTwo = _ControlColorButtonTwo
        Me.Control_Box_Five1.ColorItem = _ControlColorButtonItem
        Me.Control_Box_Five1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Control_Box_Five1.Font = New System.Drawing.Font("Marlett", 7.0!)
        Me.Control_Box_Five1.Size = New System.Drawing.Size(80, 23)
        Me.Control_Box_Five1.Name = "Control_Box_Five1"
        Me.Control_Box_Five1.TabIndex = 2
        Me.Control_Box_Five1.Text = "Control_Box_Five1"

        'Eight Close
        Me.Control_Box_Close.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right Or AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Control_Box_Close.BackColor = System.Drawing.Color.Transparent
        Me.Control_Box_Close.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Control_Box_Close.Font = New System.Drawing.Font("Marlett", 7.0!)
        Me.Control_Box_Close.Size = New System.Drawing.Size(18, 18)
        Me.Control_Box_Close.BackColor = _ControlColorButtonOne
        Me.Control_Box_Close.ItemColor = _ControlColorButtonItem
        Me.Control_Box_Close.Name = "Control_Box_Close1"
        Me.Control_Box_Close.TabIndex = 3
        Me.Control_Box_Close.Text = "Control_Box_Close1"
        Me.Control_Box_Close.MouseStateRetangle = _MouseStateRetangle
        Me.Control_Box_Close.ToolTip = _Tooltip
        '588; 8

        'Eight Max
        Me.Control_Box_Max.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right Or AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Control_Box_Max.BackColor = System.Drawing.Color.Transparent
        Me.Control_Box_Max.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Control_Box_Max.Font = New System.Drawing.Font("Marlett", 7.0!)
        Me.Control_Box_Max.Size = New System.Drawing.Size(18, 18)
        Me.Control_Box_Max.ItemColor = _ControlColorButtonItem
        Me.Control_Box_Max.BackColor = _ControlColorButtonOne
        Me.Control_Box_Max.Name = "Control_Box_Max1"
        Me.Control_Box_Max.TabIndex = 4
        Me.Control_Box_Max.Text = "Control_Box_Max1"
        Me.Control_Box_Max.MouseStateRetangle = _MouseStateRetangle
        Me.Control_Box_Max.ToolTip = _Tooltip

        'Eight Mini
        Me.Control_Box_Mini.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right Or AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Control_Box_Mini.BackColor = System.Drawing.Color.Transparent
        Me.Control_Box_Mini.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Control_Box_Mini.Font = New System.Drawing.Font("Marlett", 7.0!)
        Me.Control_Box_Mini.Size = New System.Drawing.Size(18, 18)
        Me.Control_Box_Mini.ItemColor = _ControlColorButtonItem
        Me.Control_Box_Mini.BackColor = _ControlColorButtonOne
        Me.Control_Box_Mini.Name = "Control_Box_Mini1"
        Me.Control_Box_Mini.TabIndex = 5
        Me.Control_Box_Mini.Text = "Control_Box_Mini1"
        Me.Control_Box_Mini.MouseStateRetangle = _MouseStateRetangle
        Me.Control_Box_Mini.ToolTip = _Tooltip

        'Eight Hide
        Me.Control_Box_Hide.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right Or AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Control_Box_Hide.BackColor = System.Drawing.Color.Transparent
        Me.Control_Box_Hide.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Control_Box_Hide.Font = New System.Drawing.Font("Marlett", 7.0!)
        Me.Control_Box_Hide.Size = New System.Drawing.Size(18, 18)
        Me.Control_Box_Hide.ItemColor = _ControlColorButtonItem
        Me.Control_Box_Hide.BackColor = _ControlColorButtonOne
        Me.Control_Box_Hide.Name = "Control_Box_Hide1"
        Me.Control_Box_Hide.TabIndex = 5
        Me.Control_Box_Hide.Text = "Control_Box_Hide1"
        Me.Control_Box_Hide.MouseStateRetangle = _MouseStateRetangle
        Me.Control_Box_Hide.ToolTip = _Tooltip
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Image = _Icon
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.ContextMenu = ContextMenu
        '
        'Form1
        '        

        'Acerta a centralização dos objetos
        Me.PictureBox1.Location = New System.Drawing.Point(7, G / 2)
        Me.Control_Box_Five1.Location = New System.Drawing.Point(410, (G - 1) / 2)
        Me.Control_Box_Close.Location = New System.Drawing.Point(468, G / 2)
        Me.Control_Box_Max.Location = New System.Drawing.Point(438, G / 2)
        Me.Control_Box_Mini.Location = New System.Drawing.Point(408, G / 2)
        Me.Control_Box_Hide.Location = New System.Drawing.Point(378, G / 2)

        'Recuo do Titulo caso icon seja ocultado
        If _IconEnable = False Then
            Me.Label1.Location = New System.Drawing.Point(10, (G + 4) / 2)
        Else
            'IconSize
            If Me._IconSize = 0 Then
                Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
                Me.Label1.Location = New System.Drawing.Point(30, (G + 4) / 2)
            ElseIf Me._IconSize = 1 Then
                Me.PictureBox1.Size = New System.Drawing.Size(20, 20)
                Me.Label1.Location = New System.Drawing.Point(33, (G + 4) / 2)
            ElseIf Me._IconSize = 2 Then
                Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
                Me.Label1.Location = New System.Drawing.Point(43, (G + 4) / 2)
            End If

        End If

        'notify icon
        Control_Box_Hide.Notifyicon = _Notifyicon
        Control_Box_Hide.NotifyIconIcon = _NotifyIconIcon
        Control_Box_Hide.NotifyIconTitle = _NotifyIconTitle
        Control_Box_Hide.NotifyIconText = _NotifyIconText
        Control_Box_Hide.BalloonTipTime = _BalloonTipTime

        'Coloca Os controles nos devidos lugar caso abilitado ou desabilitados
        If _CloseEnableNew = False And _MaxEnableNew = False And _MiniEnableNew = False And _HideEnableNew = True Then
            Me.Control_Box_Hide.Location = New System.Drawing.Point(468, G / 2)
        ElseIf _CloseEnableNew = False And _MaxEnableNew = True And _MiniEnableNew = True And _HideEnableNew = False Then
            Me.Control_Box_Mini.Location = New System.Drawing.Point(468, G / 2)
        ElseIf _CloseEnableNew = False And _MaxEnableNew = True And _MiniEnableNew = True And _HideEnableNew = False Then
            Me.Control_Box_Max.Location = New System.Drawing.Point(468, G / 2)
            Me.Control_Box_Mini.Location = New System.Drawing.Point(438, G / 2)
        ElseIf _CloseEnableNew = True And _MaxEnableNew = False And _MiniEnableNew = False And _HideEnableNew = True Then
            Me.Control_Box_Hide.Location = New System.Drawing.Point(438, G / 2)
        ElseIf _CloseEnableNew = True And _MaxEnableNew = False And _MiniEnableNew = True And _HideEnableNew = False Then
            Me.Control_Box_Mini.Location = New System.Drawing.Point(438, G / 2)
        ElseIf _CloseEnableNew = False And _MaxEnableNew = False And _MiniEnableNew = True And _HideEnableNew = True Then
            Me.Control_Box_Mini.Location = New System.Drawing.Point(468, G / 2)
            Me.Control_Box_Hide.Location = New System.Drawing.Point(438, G / 2)
        ElseIf _CloseEnableNew = False And _MaxEnableNew = True And _MiniEnableNew = False And _HideEnableNew = True Then
            Me.Control_Box_Max.Location = New System.Drawing.Point(468, G / 2)
            Me.Control_Box_Hide.Location = New System.Drawing.Point(438, G / 2)
        ElseIf _CloseEnableNew = True And _MaxEnableNew = False And _MiniEnableNew = False And _HideEnableNew = True Then
            Me.Control_Box_Hide.Location = New System.Drawing.Point(438, G / 2)
        ElseIf _CloseEnableNew = True And _MaxEnableNew = True And _MiniEnableNew = False And _HideEnableNew = True Then
            Me.Control_Box_Max.Location = New System.Drawing.Point(438, G / 2)
            Me.Control_Box_Hide.Location = New System.Drawing.Point(408, G / 2)
        ElseIf _CloseEnableNew = True And _MaxEnableNew = False And _MiniEnableNew = True And _HideEnableNew = True Then
            Me.Control_Box_Mini.Location = New System.Drawing.Point(438, G / 2)
            Me.Control_Box_Hide.Location = New System.Drawing.Point(408, G / 2)
        ElseIf _CloseEnableNew = False And _MaxEnableNew = True And _MiniEnableNew = True And _HideEnableNew = True Then
            Me.Control_Box_Mini.Location = New System.Drawing.Point(468, G / 2)
            Me.Control_Box_Max.Location = New System.Drawing.Point(438, G / 2)
            Me.Control_Box_Hide.Location = New System.Drawing.Point(408, G / 2)
        End If

        'Control Exit Button
        If _CloseToClose = Closerme.MeExit Then
            Control_Box_Close.CloseToHide = Control_Box_Eight_Close.Closerme.MeExit
            Control_Box_Five1.CloseToHide = Control_Box_Eight_Close.Closerme.MeExit
        ElseIf _CloseToClose = Closerme.MeClose Then
            Control_Box_Close.CloseToHide = Control_Box_Eight_Close.Closerme.MeClose
            Control_Box_Five1.CloseToHide = Control_Box_Eight_Close.Closerme.MeClose
        ElseIf _CloseToClose = Closerme.MeHide Then
            Control_Box_Close.CloseToHide = Control_Box_Eight_Close.Closerme.MeHide
            Control_Box_Five1.CloseToHide = Control_Box_Eight_Close.Closerme.MeHide
        End If



        If Me.Controls.Count > 0 Then
            Me.Controls.Clear()
        Else
            Me.Controls.Add(Me.Panel1)
        End If

        ' Me.Dock = DockStyle.Top

        If FindForm.WindowState = FormWindowState.Normal Then
            RestaurarMenu.Enabled = False
            MaximizarMenu.Enabled = True
            TamanhoMenu.Enabled = True
        ElseIf MyBase.FindForm.WindowState = FormWindowState.Maximized Then
            RestaurarMenu.Enabled = True
            MaximizarMenu.Enabled = False
            TamanhoMenu.Enabled = False
        End If


        Utils.CarregaBordas(Me, _borderRadius, _borderRadius)

    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If _TaskBarClickRight = True Then
            If e.Button = MouseButtons.Right Then
                PopupMenu.Show(MousePosition.X, MousePosition.Y)
            End If
        End If
    End Sub

    Private Sub Label1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Label1.MouseDown
        If _TaskBarClickRight = True Then
            If e.Button = MouseButtons.Right Then
                PopupMenu.Show(MousePosition.X, MousePosition.Y)
            End If
        End If
    End Sub
    Private Sub Panel1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseDown
        If _TaskBarClickRight = True Then
            If e.Button = MouseButtons.Right Then
                PopupMenu.Show(MousePosition.X, MousePosition.Y)
            End If
        End If
    End Sub
    Private Sub Restaurar_Menu(ByVal sender As Object, ByVal e As EventArgs) Handles RestaurarMenu.Click
        Call Resize_Form()
    End Sub
    Private Sub Mover_Menu(ByVal sender As Object, ByVal e As EventArgs) Handles MoverMenu.Click

    End Sub
    Private Sub Tamanho_Menu(ByVal sender As Object, ByVal e As EventArgs) Handles TamanhoMenu.Click

    End Sub
    Private Sub Minimizar_Menu(ByVal sender As Object, ByVal e As EventArgs) Handles MinimizarMenu.Click
        MyBase.FindForm.WindowState = FormWindowState.Minimized
    End Sub
    Private Sub Maximizar_Menu(ByVal sender As Object, ByVal e As EventArgs) Handles MaximizarMenu.Click
        Call Resize_Form()
    End Sub
    Private Sub FecharAplicacao(ByVal sender As Object, ByVal e As EventArgs) Handles FecharMenu.Click
        Me.FindForm.Close()
    End Sub
    Private Sub Resize_Form()
        On Error Resume Next
        If _MaxEnableNew = False And _ControlBoxEnable = False Then
            If MyBase.FindForm.WindowState = FormWindowState.Normal Then
                MyBase.FindForm.WindowState = FormWindowState.Minimized
            End If
        End If

        If MyBase.FindForm.WindowState = FormWindowState.Normal Then
            MyBase.FindForm.WindowState = FormWindowState.Maximized
            RestaurarMenu.Enabled = True
            MaximizarMenu.Enabled = False
            TamanhoMenu.Enabled = False

        ElseIf MyBase.FindForm.WindowState = FormWindowState.Maximized Then
            MyBase.FindForm.WindowState = FormWindowState.Normal
            RestaurarMenu.Enabled = False
            MaximizarMenu.Enabled = True
            TamanhoMenu.Enabled = True
        End If
    End Sub
    Private Sub Comtrol_Box1(ByVal sender As Object, ByVal e As EventArgs) Handles Control_Box_Max.Click
        On Error Resume Next
        If MyBase.FindForm.WindowState = FormWindowState.Normal Then
            RestaurarMenu.Enabled = True
            MaximizarMenu.Enabled = False
            TamanhoMenu.Enabled = False

        ElseIf MyBase.FindForm.WindowState = FormWindowState.Maximized Then
            RestaurarMenu.Enabled = False
            MaximizarMenu.Enabled = True
            TamanhoMenu.Enabled = True
        End If
    End Sub
    Private Sub PictureMove(ByVal sender As Object, ByVal e As EventArgs) Handles PictureBox1.MouseMove
        Utils.MouseMoveUnick(sender, e, MyBase.FindForm)
    End Sub
    Private Sub LabelMove(ByVal sender As Object, ByVal e As EventArgs) Handles Label1.MouseMove
        Utils.MouseMoveUnick(sender, e, MyBase.FindForm)
    End Sub

    Private Sub Panel1_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseDoubleClick
        If e.Button = MouseButtons.Left Then
            If _TwoClicks = True Then
                Call Resize_Form()
            End If
        End If
    End Sub
    Private Sub Label1_DoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Label1.DoubleClick
        If e.Button = MouseButtons.Left Then
            If _TwoClicks = True Then
                Call Resize_Form()
            End If
        End If
    End Sub
    Private Sub PictureBox1_DoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.DoubleClick
        If e.Button = MouseButtons.Left Then
            If _TwoClicks = True Then
                Call Resize_Form()
            End If
        End If
    End Sub
    Private Sub MouseMove(ByVal sender As Object, ByVal e As EventArgs) Handles Panel1.MouseMove
        Utils.MouseMoveUnick(sender, e, MyBase.FindForm)
    End Sub

    Private _CloseButtonActivate As Boolean = True
    Private Sub Control_Box_Five1_Click(sender As Object, e As System.EventArgs) Handles Control_Box_Five1.Click
        If _CloseButtonActivate = False Then
            Me.FindForm.Hide()
        End If
    End Sub
    Private Sub Control_Box_Max_Click(sender As Object, e As System.EventArgs) Handles MyBase.Resize
        Utils.CarregaBordas(Me, _borderRadius, _borderRadius)
    End Sub
End Class


