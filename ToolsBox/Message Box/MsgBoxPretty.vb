Imports System.Runtime.InteropServices

Partial Public Class FormMessageBox
    Inherits Form

    Private _PrimaryColor As Color = Color.CornflowerBlue
    Friend WithEvents panelTitleBar As Panel
    Friend WithEvents panelBody As Panel
    Friend WithEvents panelButtons As Panel
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents labelCaption As Label
    Friend WithEvents pictureBoxIcon As PictureBox
    Friend WithEvents labelMessage As Label
    Private borderSize As Integer = 2

    Public Property PrimaryColor As Color
        Get
            Return _PrimaryColor
        End Get
        Set(ByVal value As Color)
            _PrimaryColor = value
            Me.BackColor = _PrimaryColor
            Me.panelTitleBar.BackColor = _PrimaryColor
        End Set
    End Property

    Public Sub New(ByVal text As String)
        InitializeComponent()
        InitializeItems()
        Me.PrimaryColor = _PrimaryColor
        Me.labelMessage.Text = text
        Me.labelCaption.Text = ""
        SetFormSize()
        SetButtons(MessageBoxButtons.OK, MessageBoxDefaultButton.Button1)
    End Sub

    Public Sub New(ByVal text As String, ByVal caption As String)
        InitializeComponent()
        InitializeItems()
        Me.PrimaryColor = _PrimaryColor
        Me.labelMessage.Text = text
        Me.labelCaption.Text = caption
        SetFormSize()
        SetButtons(MessageBoxButtons.OK, MessageBoxDefaultButton.Button1)
    End Sub

    Public Sub New(ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons)
        InitializeComponent()
        InitializeItems()
        Me.PrimaryColor = _PrimaryColor
        Me.labelMessage.Text = text
        Me.labelCaption.Text = caption
        SetFormSize()
        SetButtons(buttons, MessageBoxDefaultButton.Button1)
    End Sub

    Public Sub New(ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcon)
        InitializeComponent()
        InitializeItems()
        Me.PrimaryColor = _PrimaryColor
        Me.labelMessage.Text = text
        Me.labelCaption.Text = caption
        SetFormSize()
        SetButtons(buttons, MessageBoxDefaultButton.Button1)
        SetIcon(icon)
    End Sub

    Public Sub New(ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcon, ByVal defaultButton As MessageBoxDefaultButton)
        InitializeComponent()
        InitializeItems()
        Me.PrimaryColor = _PrimaryColor
        Me.labelMessage.Text = text
        Me.labelCaption.Text = caption
        SetFormSize()
        SetButtons(buttons, defaultButton)
        SetIcon(icon)
    End Sub

    Private Sub InitializeItems()
        Me.FormBorderStyle = FormBorderStyle.None
        Me.Padding = New Padding(borderSize)
        Me.labelMessage.MaximumSize = New Size(550, 0)
        Me.btnClose.DialogResult = DialogResult.Cancel
        Me.Button1.DialogResult = DialogResult.OK
        Me.Button1.Visible = False
        Me.Button2.Visible = False
        Me.Button3.Visible = False
    End Sub

    Private Sub SetFormSize()
        Dim widht As Integer = Me.labelMessage.Width + Me.pictureBoxIcon.Width + Me.panelBody.Padding.Left
        Dim height As Integer = Me.panelTitleBar.Height + Me.labelMessage.Height + Me.panelButtons.Height + Me.panelBody.Padding.Top
        Me.Size = New Size(widht, height)
    End Sub

    Private Sub SetButtons(ByVal buttons As MessageBoxButtons, ByVal defaultButton As MessageBoxDefaultButton)
        Dim xCenter As Integer = (Me.panelButtons.Width - Button1.Width) / 2
        Dim yCenter As Integer = (Me.panelButtons.Height - Button1.Height) / 2

        Select Case buttons
            Case MessageBoxButtons.OK
                Button1.Visible = True
                Button1.Location = New Point(xCenter, yCenter)
                Button1.Text = "Ok"
                Button1.DialogResult = DialogResult.OK
                SetDefaultButton(defaultButton)
            Case MessageBoxButtons.OKCancel
                Button1.Visible = True
                Button1.Location = New Point(xCenter - (Button1.Width / 2) - 5, yCenter)
                Button1.Text = "Ok"
                Button1.DialogResult = DialogResult.OK
                Button2.Visible = True
                Button2.Location = New Point(xCenter + (Button2.Width / 2) + 5, yCenter)
                Button2.Text = "Cancel"
                Button2.DialogResult = DialogResult.Cancel
                Button2.BackColor = Color.DimGray

                If defaultButton <> MessageBoxDefaultButton.Button3 Then
                    SetDefaultButton(defaultButton)
                Else
                    SetDefaultButton(MessageBoxDefaultButton.Button1)
                End If

            Case MessageBoxButtons.RetryCancel
                Button1.Visible = True
                Button1.Location = New Point(xCenter - (Button1.Width / 2) - 5, yCenter)
                Button1.Text = "Retry"
                Button1.DialogResult = DialogResult.Retry
                Button2.Visible = True
                Button2.Location = New Point(xCenter + (Button2.Width / 2) + 5, yCenter)
                Button2.Text = "Cancel"
                Button2.DialogResult = DialogResult.Cancel
                Button2.BackColor = Color.DimGray

                If defaultButton <> MessageBoxDefaultButton.Button3 Then
                    SetDefaultButton(defaultButton)
                Else
                    SetDefaultButton(MessageBoxDefaultButton.Button1)
                End If

            Case MessageBoxButtons.YesNo
                Button1.Visible = True
                Button1.Location = New Point(xCenter - (Button1.Width / 2) - 5, yCenter)
                Button1.Text = "Yes"
                Button1.DialogResult = DialogResult.Yes
                Button2.Visible = True
                Button2.Location = New Point(xCenter + (Button2.Width / 2) + 5, yCenter)
                Button2.Text = "No"
                Button2.DialogResult = DialogResult.No
                Button2.BackColor = Color.IndianRed

                If defaultButton <> MessageBoxDefaultButton.Button3 Then
                    SetDefaultButton(defaultButton)
                Else
                    SetDefaultButton(MessageBoxDefaultButton.Button1)
                End If

            Case MessageBoxButtons.YesNoCancel
                Button1.Visible = True
                Button1.Location = New Point(xCenter - Button1.Width - 5, yCenter)
                Button1.Text = "Yes"
                Button1.DialogResult = DialogResult.Yes
                Button2.Visible = True
                Button2.Location = New Point(xCenter, yCenter)
                Button2.Text = "No"
                Button2.DialogResult = DialogResult.No
                Button2.BackColor = Color.IndianRed
                Button3.Visible = True
                Button3.Location = New Point(xCenter + Button2.Width + 5, yCenter)
                Button3.Text = "Cancel"
                Button3.DialogResult = DialogResult.Cancel
                Button3.BackColor = Color.DimGray
                SetDefaultButton(defaultButton)
            Case MessageBoxButtons.AbortRetryIgnore
                Button1.Visible = True
                Button1.Location = New Point(xCenter - Button1.Width - 5, yCenter)
                Button1.Text = "Abort"
                Button1.DialogResult = DialogResult.Abort
                Button1.BackColor = Color.Goldenrod
                Button2.Visible = True
                Button2.Location = New Point(xCenter, yCenter)
                Button2.Text = "Retry"
                Button2.DialogResult = DialogResult.Retry
                Button3.Visible = True
                Button3.Location = New Point(xCenter + Button2.Width + 5, yCenter)
                Button3.Text = "Ignore"
                Button3.DialogResult = DialogResult.Ignore
                Button3.BackColor = Color.IndianRed
                SetDefaultButton(defaultButton)
        End Select
    End Sub

    Private Sub SetDefaultButton(ByVal defaultButton As MessageBoxDefaultButton)
        Select Case defaultButton
            Case MessageBoxDefaultButton.Button1
                Button1.[Select]()
                Button1.ForeColor = Color.White
                Button1.Font = New Font(Button1.Font, FontStyle.Underline)
            Case MessageBoxDefaultButton.Button2
                Button2.[Select]()
                Button2.ForeColor = Color.White
                Button2.Font = New Font(Button2.Font, FontStyle.Underline)
            Case MessageBoxDefaultButton.Button3
                Button3.[Select]()
                Button3.ForeColor = Color.White
                Button3.Font = New Font(Button3.Font, FontStyle.Underline)
        End Select
    End Sub

    Private Sub SetIcon(ByVal icon As MessageBoxIcon)
        Select Case icon
            Case MessageBoxIcon.[Error]
                Me.pictureBoxIcon.Image = My.Resources._Error
                PrimaryColor = Color.FromArgb(224, 79, 95)
                Me.btnClose.FlatAppearance.MouseOverBackColor = Color.Crimson
            Case MessageBoxIcon.Information
                Me.pictureBoxIcon.Image = My.Resources.Information
                PrimaryColor = Color.FromArgb(38, 191, 166)
            Case MessageBoxIcon.Question
                Me.pictureBoxIcon.Image = My.Resources.Question
                PrimaryColor = Color.FromArgb(10, 119, 232)
            Case MessageBoxIcon.Exclamation
                Me.pictureBoxIcon.Image = My.Resources.Exclamation
                PrimaryColor = Color.FromArgb(255, 140, 0)
            Case MessageBoxIcon.None
                Me.pictureBoxIcon.Image = My.Resources.Chat
                PrimaryColor = Color.CornflowerBlue
        End Select
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    <DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Shared Sub SendMessage(ByVal hWnd As System.IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer)
    End Sub

    <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Shared Sub ReleaseCapture()
    End Sub

    Private Sub panelTitleBar_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles panelTitleBar.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, &H112, &HF012, 0)
    End Sub

    Private Sub InitializeComponent()
        Me.panelTitleBar = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.labelCaption = New System.Windows.Forms.Label()
        Me.panelBody = New System.Windows.Forms.Panel()
        Me.pictureBoxIcon = New System.Windows.Forms.PictureBox()
        Me.labelMessage = New System.Windows.Forms.Label()
        Me.panelButtons = New System.Windows.Forms.Panel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.panelTitleBar.SuspendLayout()
        Me.panelBody.SuspendLayout()
        CType(Me.pictureBoxIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelButtons.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelTitleBar
        '
        Me.panelTitleBar.BackColor = System.Drawing.Color.CornflowerBlue
        Me.panelTitleBar.Controls.Add(Me.btnClose)
        Me.panelTitleBar.Controls.Add(Me.labelCaption)
        Me.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelTitleBar.Location = New System.Drawing.Point(0, 0)
        Me.panelTitleBar.Name = "panelTitleBar"
        Me.panelTitleBar.Size = New System.Drawing.Size(320, 28)
        Me.panelTitleBar.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Image = Global.ToolsBox.My.Resources.Resources.Close
        Me.btnClose.Location = New System.Drawing.Point(295, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(25, 28)
        Me.btnClose.TabIndex = 2
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'labelCaption
        '
        Me.labelCaption.AutoSize = True
        Me.labelCaption.ForeColor = System.Drawing.Color.White
        Me.labelCaption.Location = New System.Drawing.Point(7, 8)
        Me.labelCaption.Name = "labelCaption"
        Me.labelCaption.Size = New System.Drawing.Size(69, 13)
        Me.labelCaption.TabIndex = 1
        Me.labelCaption.Text = "LabelCaption"
        Me.labelCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'panelBody
        '
        Me.panelBody.BackColor = System.Drawing.Color.White
        Me.panelBody.Controls.Add(Me.pictureBoxIcon)
        Me.panelBody.Controls.Add(Me.labelMessage)
        Me.panelBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelBody.Location = New System.Drawing.Point(0, 28)
        Me.panelBody.Name = "panelBody"
        Me.panelBody.Size = New System.Drawing.Size(320, 112)
        Me.panelBody.TabIndex = 1
        '
        'pictureBoxIcon
        '
        Me.pictureBoxIcon.Image = Global.ToolsBox.My.Resources.Resources.cicero_engraving
        Me.pictureBoxIcon.Location = New System.Drawing.Point(13, 11)
        Me.pictureBoxIcon.Name = "pictureBoxIcon"
        Me.pictureBoxIcon.Size = New System.Drawing.Size(32, 32)
        Me.pictureBoxIcon.TabIndex = 3
        Me.pictureBoxIcon.TabStop = False
        '
        'labelMessage
        '
        Me.labelMessage.AutoSize = True
        Me.labelMessage.ForeColor = System.Drawing.Color.Black
        Me.labelMessage.Location = New System.Drawing.Point(55, 20)
        Me.labelMessage.Name = "labelMessage"
        Me.labelMessage.Size = New System.Drawing.Size(76, 13)
        Me.labelMessage.TabIndex = 2
        Me.labelMessage.Text = "LabelMessage"
        Me.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'panelButtons
        '
        Me.panelButtons.BackColor = System.Drawing.SystemColors.Control
        Me.panelButtons.Controls.Add(Me.Button3)
        Me.panelButtons.Controls.Add(Me.Button2)
        Me.panelButtons.Controls.Add(Me.Button1)
        Me.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panelButtons.Location = New System.Drawing.Point(0, 83)
        Me.panelButtons.Name = "panelButtons"
        Me.panelButtons.Size = New System.Drawing.Size(320, 57)
        Me.panelButtons.TabIndex = 0
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.SeaGreen
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Location = New System.Drawing.Point(211, 15)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(90, 30)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Button3"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.SeaGreen
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(115, 15)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(90, 30)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.SeaGreen
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(19, 15)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(90, 30)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'FormMessageBox
        '
        Me.ClientSize = New System.Drawing.Size(320, 140)
        Me.ControlBox = False
        Me.Controls.Add(Me.panelButtons)
        Me.Controls.Add(Me.panelBody)
        Me.Controls.Add(Me.panelTitleBar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormMessageBox"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.panelTitleBar.ResumeLayout(False)
        Me.panelTitleBar.PerformLayout()
        Me.panelBody.ResumeLayout(False)
        Me.panelBody.PerformLayout()
        CType(Me.pictureBoxIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelButtons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
End Class

Public MustInherit Class MsgBoxPretty
    Public Shared Function Show(ByVal text As String) As DialogResult
        Dim result As DialogResult

        Using msgForm = New FormMessageBox(text)
            result = msgForm.ShowDialog()
        End Using

        Return result
    End Function

    Public Shared Function Show(ByVal text As String, ByVal caption As String) As DialogResult
        Dim result As DialogResult

        Using msgForm = New FormMessageBox(text, caption)
            result = msgForm.ShowDialog()
        End Using

        Return result
    End Function

    Public Shared Function Show(ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons) As DialogResult
        Dim result As DialogResult

        Using msgForm = New FormMessageBox(text, caption, buttons)
            result = msgForm.ShowDialog()
        End Using

        Return result
    End Function

    Public Shared Function Show(ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcon) As DialogResult
        Dim result As DialogResult

        Using msgForm = New FormMessageBox(text, caption, buttons, icon)
            result = msgForm.ShowDialog()
        End Using

        Return result
    End Function

    Public Shared Function Show(ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcon, ByVal defaultButton As MessageBoxDefaultButton) As DialogResult
        Dim result As DialogResult

        Using msgForm = New FormMessageBox(text, caption, buttons, icon, defaultButton)
            result = msgForm.ShowDialog()
        End Using

        Return result
    End Function

    Public Shared Function Show(ByVal owner As IWin32Window, ByVal text As String) As DialogResult
        Dim result As DialogResult

        Using msgForm = New FormMessageBox(text)
            result = msgForm.ShowDialog(owner)
        End Using

        Return result
    End Function

    Public Shared Function Show(ByVal owner As IWin32Window, ByVal text As String, ByVal caption As String) As DialogResult
        Dim result As DialogResult

        Using msgForm = New FormMessageBox(text, caption)
            result = msgForm.ShowDialog(owner)
        End Using

        Return result
    End Function

    Public Shared Function Show(ByVal owner As IWin32Window, ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons) As DialogResult
        Dim result As DialogResult

        Using msgForm = New FormMessageBox(text, caption, buttons)
            result = msgForm.ShowDialog(owner)
        End Using

        Return result
    End Function

    Public Shared Function Show(ByVal owner As IWin32Window, ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcon) As DialogResult
        Dim result As DialogResult

        Using msgForm = New FormMessageBox(text, caption, buttons, icon)
            result = msgForm.ShowDialog(owner)
        End Using

        Return result
    End Function

    Public Shared Function Show(ByVal owner As IWin32Window, ByVal text As String, ByVal caption As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcon, ByVal defaultButton As MessageBoxDefaultButton) As DialogResult
        Dim result As DialogResult

        Using msgForm = New FormMessageBox(text, caption, buttons, icon, defaultButton)
            result = msgForm.ShowDialog(owner)
        End Using

        Return result
    End Function
End Class

