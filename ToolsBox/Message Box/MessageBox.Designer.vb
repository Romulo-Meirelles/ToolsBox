<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MSG
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MSG))
        Me.Open_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Close_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Wait_Close_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Panel_Gradient = New ToolsBox.Panel_Gradient()
        Me.Close_Pic = New System.Windows.Forms.PictureBox()
        Me.Titulo_lbl = New System.Windows.Forms.Label()
        Me.Texto_lbl = New System.Windows.Forms.Label()
        Me.Icon_Pic = New System.Windows.Forms.PictureBox()
        Me.Panel_Gradient.SuspendLayout()
        CType(Me.Close_Pic, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Icon_Pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Open_Timer
        '
        Me.Open_Timer.Interval = 8
        '
        'Close_Timer
        '
        Me.Close_Timer.Interval = 8
        '
        'Wait_Close_Timer
        '
        Me.Wait_Close_Timer.Interval = 5000
        '
        'Panel_Gradient
        '
        Me.Panel_Gradient.BackgroundImage = CType(resources.GetObject("Panel_Gradient.BackgroundImage"), System.Drawing.Image)
        Me.Panel_Gradient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel_Gradient.Controls.Add(Me.Close_Pic)
        Me.Panel_Gradient.Controls.Add(Me.Titulo_lbl)
        Me.Panel_Gradient.Controls.Add(Me.Texto_lbl)
        Me.Panel_Gradient.Controls.Add(Me.Icon_Pic)
        Me.Panel_Gradient.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Gradient.GradientBottomLeft = System.Drawing.Color.White
        Me.Panel_Gradient.GradientBottomRight = System.Drawing.Color.White
        Me.Panel_Gradient.GradientTopLeft = System.Drawing.Color.White
        Me.Panel_Gradient.GradientTopRight = System.Drawing.Color.White
        Me.Panel_Gradient.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Gradient.Name = "Panel_Gradient"
        Me.Panel_Gradient.Quality = 10
        Me.Panel_Gradient.Size = New System.Drawing.Size(350, 150)
        Me.Panel_Gradient.TabIndex = 0
        '
        'Close_Pic
        '
        Me.Close_Pic.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Close_Pic.BackColor = System.Drawing.Color.Transparent
        Me.Close_Pic.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Close_Pic.Image = Global.ToolsBox.My.Resources.Resources.Close
        Me.Close_Pic.Location = New System.Drawing.Point(321, 4)
        Me.Close_Pic.Name = "Close_Pic"
        Me.Close_Pic.Size = New System.Drawing.Size(24, 24)
        Me.Close_Pic.TabIndex = 3
        Me.Close_Pic.TabStop = False
        '
        'Titulo_lbl
        '
        Me.Titulo_lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Titulo_lbl.AutoSize = True
        Me.Titulo_lbl.BackColor = System.Drawing.Color.Transparent
        Me.Titulo_lbl.Location = New System.Drawing.Point(12, 6)
        Me.Titulo_lbl.Name = "Titulo_lbl"
        Me.Titulo_lbl.Size = New System.Drawing.Size(259, 13)
        Me.Titulo_lbl.TabIndex = 2
        Me.Titulo_lbl.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit"
        '
        'Texto_lbl
        '
        Me.Texto_lbl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Texto_lbl.BackColor = System.Drawing.Color.Transparent
        Me.Texto_lbl.Location = New System.Drawing.Point(116, 32)
        Me.Texto_lbl.Name = "Texto_lbl"
        Me.Texto_lbl.Size = New System.Drawing.Size(229, 109)
        Me.Texto_lbl.TabIndex = 1
        Me.Texto_lbl.Text = resources.GetString("Texto_lbl.Text")
        '
        'Icon_Pic
        '
        Me.Icon_Pic.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Icon_Pic.BackColor = System.Drawing.Color.Transparent
        Me.Icon_Pic.Image = Global.ToolsBox.My.Resources.Resources.cicero_engraving
        Me.Icon_Pic.Location = New System.Drawing.Point(11, 30)
        Me.Icon_Pic.Name = "Icon_Pic"
        Me.Icon_Pic.Size = New System.Drawing.Size(100, 100)
        Me.Icon_Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Icon_Pic.TabIndex = 0
        Me.Icon_Pic.TabStop = False
        '
        'MSG
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(350, 150)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel_Gradient)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "MSG"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Information"
        Me.Panel_Gradient.ResumeLayout(False)
        Me.Panel_Gradient.PerformLayout()
        CType(Me.Close_Pic, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Icon_Pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Open_Timer As System.Windows.Forms.Timer
    Friend WithEvents Close_Timer As System.Windows.Forms.Timer
    Friend WithEvents Wait_Close_Timer As System.Windows.Forms.Timer
    Friend WithEvents Panel_Gradient As Panel_Gradient
    Friend WithEvents Close_Pic As System.Windows.Forms.PictureBox
    Friend WithEvents Titulo_lbl As System.Windows.Forms.Label
    Friend WithEvents Texto_lbl As System.Windows.Forms.Label
    Friend WithEvents Icon_Pic As System.Windows.Forms.PictureBox
End Class