'Imports System.ComponentModel

'<DefaultEvent("OnSelectedIndexChanged")> _
'Public Class RCB
'    Inherits UserControl
'    ' Events
'    <Category("RJ Code Advance")> _
'    Public Custom Event OnSelectedIndexChanged As EventHandler

'        ' Methods
'      Sub New()
'        MyBase.SuspendLayout()
'        Me.t.Font = New Font("Microsoft Sans Serif", 9.5!)
'        Me.t.FormattingEnabled = True
'        Me.t.Size = New Size(170, &H15)
'        AddHandler Me.t.SelectedIndexChanged, New EventHandler(AddressOf Me.juyyu)
'        AddHandler Me.t.DropDownClosed, New EventHandler(AddressOf Me.yty)
'        AddHandler Me.t.TextChanged, New EventHandler(AddressOf Me.kjh)
'        Me.j.FlatStyle = FlatStyle.Flat
'        Me.j.FlatAppearance.BorderSize = 0
'        Me.j.Size = New Size(30, 30)
'        Me.j.Dock = DockStyle.Right
'        Me.j.BackColor = Color.WhiteSmoke
'        Me.j.IconChar = IconChar.AngleDown
'        Me.j.IconColor = Color.MediumSlateBlue
'        Me.j.IconSize = &H16
'        Me.j.Location = New Point(&HA9, 1)
'        Me.j.Cursor = Cursors.Hand
'        AddHandler Me.j.Click, New EventHandler(AddressOf Me.ykigfbhgf)
'        Me.p.BackColor = Color.WhiteSmoke
'        Me.p.Dock = DockStyle.Fill
'        Me.p.Location = New Point(1, 1)
'        Me.p.Padding = New Padding(8, 0, 0, 0)
'        Me.p.Size = New Size(&HA8, 30)
'        Me.p.TextAlign = ContentAlignment.MiddleLeft
'        Me.p.Text = ""
'        Me.p.Font = New Font(appeagtr.q, appeagtr.s)
'        AddHandler Me.p.Click, New EventHandler(AddressOf Me.jt)
'        AddHandler Me.p.KeyDown, New KeyEventHandler(AddressOf Me.rhrth)
'        AddHandler Me.p.KeyPress, New KeyPressEventHandler(AddressOf Me.qwdq)
'        AddHandler Me.p.KeyUp, New KeyEventHandler(AddressOf Me.jtjtyj)
'        AddHandler Me.p.MouseEnter, New EventHandler(AddressOf Me.juyjyuj)
'        MyBase.AutoScaleDimensions = New SizeF(6.0!, 13.0!)
'        MyBase.AutoScaleMode = AutoScaleMode.None
'        Me.BackColor = Color.MediumSlateBlue
'        MyBase.Padding = New Padding(1)
'        MyBase.Size = New Size(200, &H20)
'        MyBase.Controls.Add(Me.p)
'        MyBase.Controls.Add(Me.j)
'        MyBase.Controls.Add(Me.t)
'        MyBase.ResumeLayout(False)
'    End Sub

'    Public Sub Clear()
'        Me.p.Text = ""
'        Me.t.Items.Clear()
'    End Sub

'    Private Sub jt(ByVal sender As Object, ByVal e As EventArgs)
'        Me.OnClick(e)
'        Me.t.Select()
'    End Sub

'    Private Sub jtjtyj(ByVal sender As Object, ByVal e As KeyEventArgs)
'        Me.OnKeyUp(e)
'    End Sub

'    Private Sub juyjyuj(ByVal sender As Object, ByVal e As EventArgs)
'        Me.OnMouseEnter(e)
'    End Sub

'    Private Sub juyyu(ByVal sender As Object, ByVal e As EventArgs)
'        If (Not Me.OnSelectedIndexChanged Is Nothing) Then
'            Me.OnSelectedIndexChanged.Invoke(Me.t, e)
'        End If
'    End Sub

'    Private Sub kjh(ByVal sender As Object, ByVal e As EventArgs)
'        Me.p.Text = Me.t.Text
'    End Sub

'    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
'        MyBase.OnLoad(e)
'        MyBase.Visible = False
'        Me.tyjtyjyt()
'        MyBase.Visible = True
'    End Sub

'    Protected Overrides Sub OnResize(ByVal e As EventArgs)
'        MyBase.OnResize(e)
'        Me.t.Width = Me.p.Width
'        Me.t.Location = New Point(((MyBase.Width - Me.t.Width) - MyBase.Padding.Right), (Me.p.Bottom - Me.t.Height))
'    End Sub

'    Private Sub qwdq(ByVal sender As Object, ByVal e As KeyPressEventArgs)
'        Me.OnKeyPress(e)
'    End Sub

'    Private Sub rhrth(ByVal sender As Object, ByVal e As KeyEventArgs)
'        Me.OnKeyDown(e)
'    End Sub

'    Private Sub tyjtyjyt()
'        If Not Me.f Then
'            Me.t.ForeColor = appeagtr.j
'            Me.t.BackColor = appeagtr.g
'            Select Case Me.a
'                Case ctrls.Glass
'                    Me.j.IconColor = appeagtr.d
'                    Me.p.ForeColor = appeagtr.i
'                    If (Not MyBase.Parent Is Nothing) Then
'                        Me.bcr = MyBase.Parent.BackColor
'                    End If
'                    If (appeagtr.a = uuufteme.Dark) Then
'                        Me.pbc = kkgegea.hgfhfg(appeagtr.j, 10)
'                    Else
'                        Me.pbc = appeagtr.j
'                    End If
'                    Exit Select
'                Case ctrls.Solid
'                    Me.pbc = appeagtr.d
'                    Me.bzp = 0
'                    Me.p.ForeColor = Color.White
'                    Me.j.IconColor = Color.White
'                    Me.bcr = appeagtr.d
'                    Exit Select
'            End Select
'        End If
'    End Sub

'    Private Sub ykigfbhgf(ByVal sender As Object, ByVal e As EventArgs)
'        Me.t.Select()
'        Me.t.DroppedDown = True
'        If Me.f Then
'            Select Case Me.a
'                Case ctrls.Glass
'                    Me.j.BackColor = Me.d
'                    Me.j.IconColor = Color.White
'                    Exit Select
'                Case ctrls.Solid
'                    Me.j.BackColor = kkgegea.hgfhfg(Me.b, 10)
'                    Me.j.IconColor = Color.White
'                    Exit Select
'            End Select
'        Else
'            Select Case Me.a
'                Case ctrls.Glass
'                    Me.j.BackColor = appeagtr.d
'                    Me.j.IconColor = Color.White
'                    Me.pbc = appeagtr.d
'                    Exit Select
'                Case ctrls.Solid
'                    Me.j.BackColor = kkgegea.hgfhfg(appeagtr.d, 10)
'                    Me.j.IconColor = Color.White
'                    Exit Select
'            End Select
'        End If
'    End Sub

'    Private Sub yty(ByVal sender As Object, ByVal e As EventArgs)
'        If Me.f Then
'            Me.j.BackColor = Me.b
'            Me.j.IconColor = Me.c
'        Else
'            Me.tyjtyjyt()
'        End If
'    End Sub


'    ' Properties
'    <Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor)), EditorBrowsable(EditorBrowsableState.Always), Category("RJ Code - Data AC"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Localizable(True), Browsable(True)> _
'    Public Property aaat As AutoCompleteStringCollection
'        Get
'            Return Me.t.AutoCompleteCustomSource
'        End Get
'        Set(ByVal value As AutoCompleteStringCollection)
'            Me.t.AutoCompleteCustomSource = value
'        End Set
'    End Property

'    <EditorBrowsable(EditorBrowsableState.Always), Browsable(True), Category("RJ Code - Data AC")> _
'    Public Property aacete As AutoCompleteMode
'        Get
'            Return Me.t.AutoCompleteMode
'        End Get
'        Set(ByVal value As AutoCompleteMode)
'            Me.t.AutoCompleteMode = value
'        End Set
'    End Property

'    <EditorBrowsable(EditorBrowsableState.Always), Browsable(True), Category("RJ Code - Data AC")> _
'    Public Property afggrtg As AutoCompleteSource
'        Get
'            Return Me.t.AutoCompleteSource
'        End Get
'        Set(ByVal value As AutoCompleteSource)
'            Me.t.AutoCompleteSource = value
'        End Set
'    End Property

'    <Category("RJ Code - Appearance")> _
'    Public Property bcr As Color
'        Get
'            Return Me.b
'        End Get
'        Set(ByVal value As Color)
'            Me.b = value
'            Me.p.BackColor = Me.b
'            Me.j.BackColor = Me.b
'        End Set
'    End Property

'    <Category("RJ Code - Appearance")> _
'    Public Property bzp As Integer
'        Get
'            Return Me.e
'        End Get
'        Set(ByVal value As Integer)
'            Me.e = value
'            MyBase.Padding = New Padding(Me.e)
'            Me.OnResize(Nothing)
'        End Set
'    End Property

'    <Category("RJ Code - Appearance")> _
'    Public Property cne As Boolean
'        Get
'            Return Me.f
'        End Get
'        Set(ByVal value As Boolean)
'            Me.f = value
'        End Set
'    End Property

'    <Category("RJ Code - Appearance")> _
'    Public Property ddbc As Color
'        Get
'            Return Me.t.BackColor
'        End Get
'        Set(ByVal value As Color)
'            Me.t.BackColor = value
'        End Set
'    End Property

'    <Category("RJ Code - Appearance")> _
'    Public Property dds As ComboBoxStyle
'        Get
'            Return Me.t.DropDownStyle
'        End Get
'        Set(ByVal value As ComboBoxStyle)
'            Me.t.DropDownStyle = value
'            If (Me.t.DropDownStyle = ComboBoxStyle.Simple) Then
'                Me.j.Visible = False
'            Else
'                Me.j.Visible = True
'            End If
'        End Set
'    End Property

'    <Category("RJ Code - Appearance")> _
'    Public Property ddtc As Color
'        Get
'            Return Me.t.ForeColor
'        End Get
'        Set(ByVal value As Color)
'            Me.t.ForeColor = value
'        End Set
'    End Property

'    <Category("RJ Code - Data"), AttributeProvider(GetType(IListSource)), DefaultValue(""), RefreshProperties(RefreshProperties.Repaint)> _
'    Public Property dssc As Object
'        Get
'            Return Me.t.DataSource
'        End Get
'        Set(ByVal value As Object)
'            Me.t.DataSource = value
'        End Set
'    End Property

'    <Category("RJ Code - Data"), DefaultValue(""), Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", GetType(UITypeEditor)), TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design")> _
'    Public Property dymm As String
'        Get
'            Return Me.t.DisplayMember
'        End Get
'        Set(ByVal value As String)
'            Me.t.DisplayMember = value
'        End Set
'    End Property

'    <Category("RJ Code - Appearance")> _
'    Public Overrides Property ForeColor As Color
'        Get
'            Return MyBase.ForeColor
'        End Get
'        Set(ByVal value As Color)
'            MyBase.ForeColor = value
'            Me.p.ForeColor = value
'        End Set
'    End Property

'    <Category("RJ Code - Appearance")> _
'    Public Property icp As Color
'        Get
'            Return Me.j.IconColor
'        End Get
'        Set(ByVal value As Color)
'            Me.c = value
'            Me.j.IconColor = Me.c
'        End Set
'    End Property

'    <Localizable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design", GetType(UITypeEditor)), Category("RJ Code - Data"), MergableProperty(False)> _
'    Public ReadOnly Property itms As ObjectCollection
'        Get
'            Return Me.t.Items
'        End Get
'    End Property

'    <Category("RJ Code - Appearance")> _
'    Public Property pbc As Color
'        Get
'            Return Me.BackColor
'        End Get
'        Set(ByVal value As Color)
'            Me.d = value
'            Me.BackColor = Me.d
'        End Set
'    End Property

'    <Browsable(False)> _
'    Public Property sfeg As Integer
'        Get
'            Return Me.t.SelectedIndex
'        End Get
'        Set(ByVal value As Integer)
'            If (value >= 0) Then
'                Me.t.SelectedIndex = value
'            End If
'        End Set
'    End Property

'    <Category("RJ Code - Appearance")> _
'    Public Property sle As ctrls
'        Get
'            Return Me.a
'        End Get
'        Set(ByVal value As ctrls)
'            Me.a = value
'            If MyBase.DesignMode Then
'                Me.tyjtyjyt()
'            End If
'        End Set
'    End Property

'    <Browsable(False)> _
'    Public ReadOnly Property ssgggt As Object
'        Get
'            Return Me.t.SelectedItem
'        End Get
'    End Property

'    <Browsable(False)> _
'    Public ReadOnly Property svvsrg As Object
'        Get
'            Return Me.t.SelectedValue
'        End Get
'    End Property

'    Public Property [Text] As String
'        Get
'            Return Me.p.Text
'        End Get
'        Set(ByVal value As String)
'            Me.p.Text = value
'        End Set
'    End Property

'    <Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", GetType(UITypeEditor)), Category("RJ Code - Data"), DefaultValue("")> _
'    Public Property vvnm As String
'        Get
'            Return Me.t.ValueMember
'        End Get
'        Set(ByVal value As String)
'            Me.t.ValueMember = value
'        End Set
'    End Property


'    ' Fields
'    Private a As ctrls
'    Private b As Color
'    Private c As Color
'    Private d As Color
'    Private e As Integer = 1
'    Private f As Boolean
'    Private j As IconButton = New IconButton
'    Private p As Label = New Label
'    Private t As ComboBox = New ComboBox
'End Class

