Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Drawing.Design


'<DefaultEvent("OnSelectedIndexChanged")>
<ToolboxBitmap(GetType(ComboBox), "ComboBox")> _
Public Class ComboBox_
    Inherits UserControl

    Private _backColor As Color = Color.WhiteSmoke
    Private _iconColor As Color = Color.MediumSlateBlue
    Private _listBackColor As Color = Color.FromArgb(230, 228, 245)
    Private _listTextColor As Color = Color.DimGray
    Private _borderColor As Color = Color.MediumSlateBlue
    Private _borderSize As Integer = 1
    Private cmbList As ComboBox
    Private lblText As Label
    Private btnIcon As Button
    Private Event OnSelectedIndexChanged As EventHandler
    Public Sub New()
        cmbList = New ComboBox()
        lblText = New Label()
        btnIcon = New Button()
        Me.SuspendLayout()
        cmbList.BackColor = ListBackColor
        cmbList.Font = New Font(Me.Font.Name, 10.0F)
        cmbList.ForeColor = ListTextColor
        cmbList.Dock = DockStyle.Fill
        AddHandler cmbList.SelectedIndexChanged, New EventHandler(AddressOf ComboBox_SelectedIndexChanged)
        AddHandler cmbList.TextChanged, New EventHandler(AddressOf ComboBox_TextChanged)
        btnIcon.Dock = DockStyle.Right
        btnIcon.FlatStyle = FlatStyle.Flat
        btnIcon.FlatAppearance.BorderSize = 0
        btnIcon.BackColor = BackColor
        btnIcon.Size = New Size(30, 30)
        btnIcon.Cursor = Cursors.Hand
        AddHandler btnIcon.Click, New EventHandler(AddressOf Icon_Click)
        AddHandler btnIcon.Paint, New PaintEventHandler(AddressOf Icon_Paint)
        lblText.Dock = DockStyle.Fill
        lblText.AutoSize = False
        lblText.BackColor = BackColor
        lblText.TextAlign = ContentAlignment.MiddleLeft
        lblText.Padding = New Padding(8, 0, 0, 0)
        lblText.Font = New Font(Me.Font.Name, 10.0F)
        AddHandler lblText.Click, New EventHandler(AddressOf Surface_Click)
        AddHandler lblText.MouseEnter, New EventHandler(AddressOf Surface_MouseEnter)
        AddHandler lblText.MouseLeave, New EventHandler(AddressOf Surface_MouseLeave)
        Me.Controls.Add(lblText)
        Me.Controls.Add(btnIcon)
        Me.Controls.Add(cmbList)
        ' Me.MinimumSize = New Size(200, 30)
        Me.Size = New Size(200, 30)
        Me.ForeColor = Color.DimGray
        Me.Padding = New Padding(BorderSize)
        Me.Font = New Font(Me.Font.Name, 10.0F)
        MyBase.BackColor = BorderColor
        Me.ResumeLayout()
        AdjustComboBoxDimensions()
    End Sub

    <Category("ToolsBox Herramienta")> _
    Public Overloads Property BackColor As Color
        Get
            Return _backColor
        End Get
        Set(ByVal value As Color)
            _backColor = value
            lblText.BackColor = _backColor
            btnIcon.BackColor = _backColor
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property IconColor As Color
        Get
            Return _iconColor
        End Get
        Set(ByVal value As Color)
            _iconColor = value
            btnIcon.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property ListBackColor As Color
        Get
            Return _listBackColor
        End Get
        Set(ByVal value As Color)
            _listBackColor = value
            cmbList.BackColor = _listBackColor
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property ListTextColor As Color
        Get
            Return _listTextColor
        End Get
        Set(ByVal value As Color)
            _listTextColor = value
            cmbList.ForeColor = _listTextColor
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set(ByVal value As Color)
            _borderColor = value
            MyBase.BackColor = _borderColor
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property BorderSize As Integer
        Get
            Return _borderSize
        End Get
        Set(ByVal value As Integer)
            _borderSize = value
            Me.Padding = New Padding(_borderSize)
            AdjustComboBoxDimensions()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Overrides Property ForeColor As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(ByVal value As Color)
            MyBase.ForeColor = value
            lblText.ForeColor = value
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Overrides Property Font As Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As Font)
            MyBase.Font = value
            lblText.Font = value
            cmbList.Font = value
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property Texts As String
        Get
            Return lblText.Text
        End Get
        Set(ByVal value As String)
            lblText.Text = value
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property DropDownStyle As ComboBoxStyle
        Get
            Return cmbList.DropDownStyle
        End Get
        Set(ByVal value As ComboBoxStyle)
            If cmbList.DropDownStyle <> ComboBoxStyle.Simple Then cmbList.DropDownStyle = value
        End Set
    End Property


    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    <Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))>
    <Localizable(True)>
    <MergableProperty(False)>
    Public ReadOnly Property Items As ComboBox.ObjectCollection
        Get
            Return cmbList.Items
        End Get
    End Property


    <AttributeProvider(GetType(IListSource))>
    Public Property DataSource As Object
        Get
            Return cmbList.DataSource
        End Get
        Set(ByVal value As Object)
            cmbList.DataSource = value
        End Set
    End Property


    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    <Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))>
    <EditorBrowsable(EditorBrowsableState.Always)>
    <Localizable(True)>
    Public Property AutoCompleteCustomSource As AutoCompleteStringCollection
        Get
            Return cmbList.AutoCompleteCustomSource
        End Get
        Set(ByVal value As AutoCompleteStringCollection)
            cmbList.AutoCompleteCustomSource = value
        End Set
    End Property


    <Browsable(True)>
    <DefaultValue(AutoCompleteSource.None)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    Public Property AutoCompleteSource As AutoCompleteSource
        Get
            Return cmbList.AutoCompleteSource
        End Get
        Set(ByVal value As AutoCompleteSource)
            cmbList.AutoCompleteSource = value
        End Set
    End Property


    <Browsable(True)>
    <DefaultValue(AutoCompleteMode.None)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    Public Property AutoCompleteMode As AutoCompleteMode
        Get
            Return cmbList.AutoCompleteMode
        End Get
        Set(ByVal value As AutoCompleteMode)
            cmbList.AutoCompleteMode = value
        End Set
    End Property


    <Bindable(True)>
    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property SelectedItem As Object
        Get
            Return cmbList.SelectedItem
        End Get
        Set(ByVal value As Object)
            cmbList.SelectedItem = value
        End Set
    End Property


    <Browsable(False)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property SelectedIndex As Integer
        Get
            Return cmbList.SelectedIndex
        End Get
        Set(ByVal value As Integer)
            cmbList.SelectedIndex = value
        End Set
    End Property


    <DefaultValue("")>
    <Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))>
    <TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")>
    Public Property DisplayMember As String
        Get
            Return cmbList.DisplayMember
        End Get
        Set(ByVal value As String)
            cmbList.DisplayMember = value
        End Set
    End Property


    <DefaultValue("")>
    <Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))>
    Public Property ValueMember As String
        Get
            Return cmbList.ValueMember
        End Get
        Set(ByVal value As String)
            cmbList.ValueMember = value
        End Set
    End Property

    Private Sub AdjustComboBoxDimensions()
        cmbList.Width = lblText.Width
        cmbList.Location = New Point() With {
            .X = Me.Width - Me.Padding.Right - cmbList.Width,
            .Y = lblText.Bottom - cmbList.Height
        }
    End Sub

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        'If OnSelectedIndexChanged IsNot Nothing Then
        RaiseEvent OnSelectedIndexChanged(sender, e)
        'End If

        lblText.Text = cmbList.Text
    End Sub

    Private Sub Icon_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
        Dim iconWidht As Integer = 14
        Dim iconHeight As Integer = 6
        Dim rectIcon = New Rectangle((btnIcon.Width - iconWidht) / 2, (btnIcon.Height - iconHeight) / 2, iconWidht, iconHeight)
        Dim graph As Graphics = e.Graphics

        Using path As GraphicsPath = New GraphicsPath()

            Using pen As Pen = New Pen(IconColor, 2)
                graph.SmoothingMode = SmoothingMode.AntiAlias
                path.AddLine(rectIcon.X, rectIcon.Y, rectIcon.X + CInt(iconWidht / 2), rectIcon.Bottom)
                path.AddLine(rectIcon.X + CInt(iconWidht / 2), rectIcon.Bottom, rectIcon.Right, rectIcon.Y)
                graph.DrawPath(pen, path)
            End Using
        End Using
    End Sub

    Private Sub Icon_Click(ByVal sender As Object, ByVal e As EventArgs)
        cmbList.[Select]()
        cmbList.DroppedDown = True
    End Sub

    Private Sub Surface_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.OnClick(e)
        cmbList.[Select]()
        If cmbList.DropDownStyle = ComboBoxStyle.DropDownList Then cmbList.DroppedDown = True
    End Sub

    Private Sub ComboBox_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        lblText.Text = cmbList.Text
    End Sub

    Private Sub Surface_MouseLeave(ByVal sender As Object, ByVal e As EventArgs)
        Me.OnMouseLeave(e)
    End Sub

    Private Sub Surface_MouseEnter(ByVal sender As Object, ByVal e As EventArgs)
        Me.OnMouseEnter(e)
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        AdjustComboBoxDimensions()
    End Sub
End Class

