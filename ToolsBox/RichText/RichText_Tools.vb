Imports System.ComponentModel
Imports System.Threading
Imports System.Delegate
Imports System.Drawing.Drawing2D
Imports ToolsBox.Utils.Mouse_Objeto
Imports System.Windows.Forms
Imports System.Drawing.Text

<ToolboxBitmap(GetType(RichTextBox), "RichTextBox")>
Public Class RichTextBox_Tools
    Inherits Control

    Friend WithEvents TaskBar_Top_Control1 As New ToolsBox.Control_TaskBar_Top
    Friend WithEvents Document As RichTextBox
    Friend WithEvents openWork As New OpenFileDialog
    Friend WithEvents OpenImage As New OpenFileDialog
    Friend WithEvents tbNew As New ToolStripButton
    Friend WithEvents tbOpen As New ToolStripButton
    Friend WithEvents tbSave As New ToolStripButton
    Friend WithEvents tbCut As New ToolStripButton
    Friend WithEvents tbCopy As New ToolStripButton
    Friend WithEvents tbPaste As New ToolStripButton
    Friend WithEvents tbUnderline As New ToolStripButton
    Friend WithEvents tbStrike As New ToolStripButton
    Friend WithEvents tbColor As New ToolStripButton
    Friend WithEvents tbBackColor As New ToolStripButton
    Friend WithEvents tbImage As New ToolStripButton
    Friend WithEvents tbAlignLeft As New ToolStripButton
    Friend WithEvents tbAlignCentre As New ToolStripButton
    Friend WithEvents tbAlignRight As New ToolStripButton
    Friend WithEvents tbZoom As New ToolStripButton
    Friend WithEvents tbZoomOut As New ToolStripButton
    Friend WithEvents tbItalic As New ToolStripButton
    Friend WithEvents ToolStripSeparator6 As New ToolStripSeparator
    Friend WithEvents ToolStripSeparator7 As New ToolStripSeparator
    Friend WithEvents tbUpper As New ToolStripButton
    Friend WithEvents tbLower As New ToolStripButton
    Friend WithEvents ToolStripSeparator8 As New ToolStripSeparator
    Friend WithEvents ToolStripSeparator9 As New ToolStripSeparator
    Friend WithEvents tbSelectSize As New ToolStripComboBox
    Friend WithEvents Status As New StatusStrip
    Friend WithEvents lblCharCount As New ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As New ToolStripStatusLabel
    Friend WithEvents lblZoom As New ToolStripStatusLabel
    Friend WithEvents rcMenu As New ContextMenuStrip
    Friend WithEvents UndoToolStripMenuItem As New ToolStripMenuItem
    Friend WithEvents RedoToolStripMenuItem As New ToolStripMenuItem
    Friend WithEvents CutToolStripMenuItem As New ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As New ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As New ToolStripMenuItem
    Friend WithEvents ColorToolStripMenuItem As New ToolStripMenuItem
    Friend WithEvents SelectAllToolStripMenuItem As New ToolStripMenuItem
    Friend WithEvents saveWork As New SaveFileDialog
    Friend WithEvents tbSelectFont As New ToolStripComboBox
    Friend WithEvents tbBold As New ToolStripButton
    Friend WithEvents mainMenu As New MenuStrip
    Friend WithEvents FileToolStripMenuItem As New ToolStripMenuItem
    Friend WithEvents mMNew As New ToolStripMenuItem
    Friend WithEvents mMOpen As New ToolStripMenuItem
    Friend WithEvents toolStripSeparator As New ToolStripSeparator
    Friend WithEvents mMSave As New ToolStripMenuItem
    Friend WithEvents toolStripSeparator2 As New ToolStripSeparator
    Friend WithEvents mMExit As New ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As New ToolStripMenuItem
    Friend WithEvents mMUndo As New ToolStripMenuItem
    Friend WithEvents mMRedo As New ToolStripMenuItem
    Friend WithEvents toolStripSeparator3 As New ToolStripSeparator
    Friend WithEvents mMCut As New ToolStripMenuItem
    Friend WithEvents mMCopy As New ToolStripMenuItem
    Friend WithEvents mMPaste As New ToolStripMenuItem
    Friend WithEvents toolStripSeparator4 As New ToolStripSeparator
    Friend WithEvents mMSelectAll As New ToolStripMenuItem
    Friend WithEvents mMColor As New ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As New ToolStripMenuItem
    Friend WithEvents mMCustomize As New ToolStripMenuItem
    Friend WithEvents toolStripSeparator5 As New ToolStripSeparator
    Friend WithEvents Tools As New ToolStrip
    Friend WithEvents toolStripSeparator1 As New ToolStripSeparator
    Private PaintMe As Boolean = True

    Private _StatusStrip As Boolean = True
    <Category("ToolsBox Herramienta")>
    Public Property StatusStrip() As Boolean
        Get
            Return _StatusStrip
        End Get
        Set(value As Boolean)

            _StatusStrip = value
            PaintMe = True
            Me.Invalidate()
        End Set
    End Property

    Private _ToolsStrip As Boolean = True
    <Category("ToolsBox Herramienta")>
    Public Property ToolsStrip() As Boolean
        Get
            Return _ToolsStrip
        End Get
        Set(value As Boolean)

            _ToolsStrip = value
            PaintMe = True
            Me.Invalidate()
        End Set
    End Property

    Private _MenuStrip As Boolean = True
    <Category("ToolsBox Herramienta")>
    Public Property MenuStrip() As Boolean
        Get
            Return _MenuStrip
        End Get
        Set(value As Boolean)

            _MenuStrip = value
            PaintMe = True
            Me.Invalidate()
        End Set
    End Property

    Private _rcMenu_ As Boolean = False
    <Category("ToolsBox Herramienta")>
    Public Property ContextMenuStrip() As Boolean
        Get
            Return _rcMenu_
        End Get
        Set(value As Boolean)
            _rcMenu_ = value
            PaintMe = True
            Me.Invalidate()
        End Set
    End Property

    Private _MmNew_ As Boolean = True
    <Category("ToolsBox Herramienta")>
    Public Property EnableNew() As Boolean
        Get
            Return _MmNew_
        End Get
        Set(value As Boolean)
            _MmNew_ = value
            PaintMe = True
            Me.Invalidate()
        End Set
    End Property

    Private _MmOpen_ As Boolean = True
    <Category("ToolsBox Herramienta")>
    Public Property EnableOpen() As Boolean
        Get
            Return _MmOpen_
        End Get
        Set(value As Boolean)
            _MmOpen_ = value
            PaintMe = True
            Me.Invalidate()
        End Set
    End Property

    Private _MmSave_ As Boolean = True
    <Category("ToolsBox Herramienta")>
    Public Property EnableSave() As Boolean
        Get
            Return _MmSave_
        End Get
        Set(value As Boolean)
            _MmSave_ = value
            PaintMe = True
            Me.Invalidate()
        End Set
    End Property

    Private _MmCut_ As Boolean = True
    <Category("ToolsBox Herramienta")>
    Public Property EnableCut() As Boolean
        Get
            Return _MmCut_
        End Get
        Set(value As Boolean)
            _MmCut_ = value
            PaintMe = True
            Me.Invalidate()
        End Set
    End Property

    Private _MmCopy_ As Boolean = True
    <Category("ToolsBox Herramienta")>
    Public Property EnableCopy() As Boolean
        Get
            Return _MmCopy_
        End Get
        Set(value As Boolean)
            _MmCopy_ = value
            PaintMe = True
            Me.Invalidate()
        End Set
    End Property

    Private _MmPaste_ As Boolean = True
    <Category("ToolsBox Herramienta")>
    Public Property EnablePaste() As Boolean
        Get
            Return _MmPaste_
        End Get
        Set(value As Boolean)
            _MmPaste_ = value
            PaintMe = True
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")>
    Public Property Documents As RichTextBox
        Get
            Return Document
        End Get
        Set(value As RichTextBox)
            If Not IsNothing(value) Then
                Document = value
                PaintMe = True
            End If
            Me.Invalidate()
        End Set
    End Property
    Private Property _Load As String
    <Category("ToolsBox Herramienta")>
    Public Property Load As String
        Get
            Return _Load
        End Get
        Set(value As String)
            If Not IsNothing(value) Then
                If value.Contains("{\rtf1\") Then
                    Using ms As New IO.MemoryStream(System.Text.Encoding.ASCII.GetBytes(value))
                        Document.LoadFile(ms, RichTextBoxStreamType.RichText)
                    End Using
                Else
                    Document.Clear()
                    Document.Text = value
                End If
            End If
            Me.Invalidate()
        End Set
    End Property
    Private Property _LoadBytes As Byte()
    <Category("ToolsBox Herramienta")>
    Public Property LoadBytes As Byte()
        Get
            Return _LoadBytes
        End Get
        Set(value As Byte())
            If Not IsNothing(value) Then
                Dim Valor As String = System.Text.Encoding.ASCII.GetString(value)
                If Valor.Contains("{\rtf1\") Then
                    Using ms As New IO.MemoryStream(value)
                        Document.LoadFile(ms, RichTextBoxStreamType.RichText)
                    End Using
                Else
                    Document.Clear()
                    Document.Text = Valor
                End If
            End If
            Me.Invalidate()
        End Set
    End Property
    Sub New()

        Document = New RichTextBox

        For fntSize = 8 To 75
            tbSelectSize.Items.Add(fntSize)
        Next
        Dim fonts As New InstalledFontCollection()

        For fntFamily As Integer = 0 To fonts.Families.Length - 1
            tbSelectFont.Items.Add(fonts.Families(fntFamily).Name)
        Next

        tbSelectSize.SelectedIndex = 2
        tbSelectFont.SelectedItem = "Times New Roman"

        Me.MinimumSize = New Size(300, 200)

        Call Show()


    End Sub
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        If PaintMe = True Then
            'Tools
            '
            Dim ToolsRanger As New List(Of ToolStripItem)

            If _MmNew_ Then
                ToolsRanger.Add(Me.tbNew)
            End If
            If _MmOpen_ Then
                ToolsRanger.Add(Me.tbOpen)
            End If
            If _MmSave_ Then
                ToolsRanger.Add(Me.tbSave)
                ToolsRanger.Add(Me.toolStripSeparator1)
            End If
            If _MmCut_ Then
                ToolsRanger.Add(Me.tbCut)
            End If
            If _MmCopy_ Then
                ToolsRanger.Add(Me.tbCopy)
            End If
            If _MmPaste_ Then
                ToolsRanger.Add(Me.tbPaste)
            End If
            ToolsRanger.Add(Me.toolStripSeparator5)
            ToolsRanger.Add(Me.tbBold)
            ToolsRanger.Add(Me.tbItalic)
            ToolsRanger.Add(Me.tbUnderline)
            ToolsRanger.Add(Me.tbStrike)
            ToolsRanger.Add(Me.ToolStripSeparator6)
            ToolsRanger.Add(Me.tbBackColor)
            ToolsRanger.Add(Me.tbColor)
            ToolsRanger.Add(Me.tbImage)
            ToolsRanger.Add(Me.toolStripSeparator1)
            ToolsRanger.Add(Me.tbAlignLeft)
            ToolsRanger.Add(Me.tbAlignCentre)
            ToolsRanger.Add(Me.tbAlignRight)
            ToolsRanger.Add(Me.ToolStripSeparator7)
            ToolsRanger.Add(Me.tbLower)
            ToolsRanger.Add(Me.tbUpper)
            ToolsRanger.Add(Me.ToolStripSeparator8)
            ToolsRanger.Add(Me.tbZoom)
            ToolsRanger.Add(Me.tbZoomOut)
            ToolsRanger.Add(Me.ToolStripSeparator9)
            ToolsRanger.Add(Me.tbSelectFont)
            ToolsRanger.Add(Me.tbSelectSize)

            Me.Tools.Items.Clear()

            For i = 0 To ToolsRanger.Count - 1
                Me.Tools.Items.Add(ToolsRanger(i))
            Next


            Me.Controls.Clear()
            Me.Controls.Add(Me.Document)

            If _StatusStrip = True Then
                Me.Controls.Add(Me.Status)
            End If
            If _ToolsStrip = True Then
                Me.Controls.Add(Me.Tools)
            End If
            If _MenuStrip = True Then
                Me.Controls.Add(Me.mainMenu)
            End If

            Me.mainMenu.ResumeLayout(False)
            Me.mainMenu.PerformLayout()
            Me.Tools.ResumeLayout(False)
            Me.Tools.PerformLayout()
            Me.Status.ResumeLayout(False)
            Me.Status.PerformLayout()
            Me.rcMenu.ResumeLayout(False)
            Me.Document.PerformLayout()
            Me.Document.ResumeLayout(False)

            Me.ResumeLayout(False)
            Me.PerformLayout()

            Document.Focus()
            Document.Select()

            PaintMe = False
        End If


    End Sub
    Private Protected Sub Show()
        Me.Status.SuspendLayout()
        Me.rcMenu.SuspendLayout()
        Me.mainMenu.SuspendLayout()
        Me.Tools.SuspendLayout()
        Me.Document.SuspendLayout()
        Me.SuspendLayout()


        'openWork
        '
        Me.openWork.Filter = "RTF Files|*.rtf|Text Files |*.txt|All Files |*.*"
        Me.openWork.Title = "Open Work"

        'saveWork
        '
        Me.saveWork.Filter = "RTF Files|*.rtf|Text Files |*.txt|All Files |*.*"
        Me.saveWork.Title = "Save Work"

        'openImage
        '
        Me.OpenImage.Filter = "JPG Files|*.jpg|PNG Files |*.png|BMP Files |*.bmp|All Files |*.*"
        Me.OpenImage.Title = "Open Image"

        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 25)
        '
        'tbSelectSize
        '
        Me.tbSelectSize.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.tbSelectSize.Name = "tbSelectSize"
        Me.tbSelectSize.Size = New System.Drawing.Size(80, 25)
        '
        'Status
        '
        Me.Status.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblCharCount, Me.ToolStripStatusLabel2, Me.lblZoom})
        Me.Status.Location = New System.Drawing.Point(0, 453)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(829, 22)
        Me.Status.TabIndex = 6
        Me.Status.Text = "StatusStrip1"
        '
        'lblCharCount
        '
        Me.lblCharCount.Name = "lblCharCount"
        Me.lblCharCount.Size = New System.Drawing.Size(13, 17)
        Me.lblCharCount.Text = "0"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(762, 17)
        Me.ToolStripStatusLabel2.Spring = True
        '
        'lblZoom
        '
        Me.lblZoom.Name = "lblZoom"
        Me.lblZoom.Size = New System.Drawing.Size(39, 17)
        Me.lblZoom.Text = "Zoom"
        '
        'rcMenu
        '
        If _rcMenu_ Then
            Me.rcMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UndoToolStripMenuItem, Me.RedoToolStripMenuItem, Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem, Me.ColorToolStripMenuItem, Me.SelectAllToolStripMenuItem})
            Me.rcMenu.Name = "rcMenu"
            Me.rcMenu.Size = New System.Drawing.Size(104, 114)
        End If
        '
        'UndoToolStripMenuItem
        '
        Me.UndoToolStripMenuItem.Image = My.Resources.undo_16px
        Me.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem"
        Me.UndoToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.UndoToolStripMenuItem.Text = "Undo"
        '
        'RedoToolStripMenuItem
        '
        Me.RedoToolStripMenuItem.Image = My.Resources.redo_16px
        Me.RedoToolStripMenuItem.Name = "RedoToolStripMenuItem"
        Me.RedoToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.RedoToolStripMenuItem.Text = "Redo"
        '
        'CutToolStripMenuItem
        '
        Me.CutToolStripMenuItem.Image = My.Resources.mMCut_Image
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        Me.CutToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.CutToolStripMenuItem.Text = "Cut"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Image = My.Resources.mMCopy_Image
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.CopyToolStripMenuItem.Text = "Copy"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Image = My.Resources.mMPaste_Image
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.PasteToolStripMenuItem.Text = "Paste"
        '
        'PasteToolStripMenuItem
        '
        Me.ColorToolStripMenuItem.Image = My.Resources.Color
        Me.ColorToolStripMenuItem.Name = "ColorToolStripMenuItem"
        Me.ColorToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.ColorToolStripMenuItem.Text = "Color"
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Image = My.Resources.SelectAll
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.SelectAllToolStripMenuItem.Text = "Select All"

        '
        'tbSelectFont
        '
        Me.tbSelectFont.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.tbSelectFont.Name = "tbSelectFont"
        Me.tbSelectFont.Size = New System.Drawing.Size(180, 25)
        '
        'mainMenu
        '
        Me.mainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ToolsToolStripMenuItem})
        Me.mainMenu.Location = New System.Drawing.Point(0, 25)
        Me.mainMenu.Name = "mainMenu"
        Me.mainMenu.Size = New System.Drawing.Size(829, 24)
        Me.mainMenu.TabIndex = 4
        Me.mainMenu.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange({Me.mMNew, Me.mMOpen, Me.toolStripSeparator, Me.mMSave, Me.toolStripSeparator2, Me.mMExit})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"

        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(143, 6)
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(143, 6)
        '
        'mMExit
        '
        Me.mMExit.Image = My.Resources._Exit
        Me.mMExit.Name = "mMExit"
        Me.mMExit.Size = New System.Drawing.Size(146, 22)
        Me.mMExit.Text = "E&xit"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mMUndo, Me.mMRedo, Me.toolStripSeparator3, Me.mMCut, Me.mMCopy, Me.mMPaste, Me.toolStripSeparator4, Me.mMColor, Me.toolStripSeparator4, Me.mMSelectAll})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "&Edit"
        '
        'mMUndo
        '
        Me.mMUndo.Image = My.Resources.undo_16px
        Me.mMUndo.Name = "mMUndo"
        Me.mMUndo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.mMUndo.Size = New System.Drawing.Size(144, 22)
        Me.mMUndo.Text = "&Undo"
        '
        'mMRedo
        '
        Me.mMRedo.Image = My.Resources.redo_16px
        Me.mMRedo.Name = "mMRedo"
        Me.mMRedo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y), System.Windows.Forms.Keys)
        Me.mMRedo.Size = New System.Drawing.Size(144, 22)
        Me.mMRedo.Text = "&Redo"
        '
        '
        '
        Me.mMColor.Image = My.Resources.Color 'Me.mMNew.Image = CType(resources.GetObject("mMNew.Image"), System.Drawing.Image)
        Me.mMColor.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mMColor.Name = "mMColor"
        Me.mMColor.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.mMColor.Size = New System.Drawing.Size(146, 22)
        Me.mMColor.Text = "&Color"

        '
        'toolStripSeparator3
        '
        Me.toolStripSeparator3.Name = "toolStripSeparator3"
        Me.toolStripSeparator3.Size = New System.Drawing.Size(141, 6)
        '
        'toolStripSeparator4
        '
        Me.toolStripSeparator4.Name = "toolStripSeparator4"
        Me.toolStripSeparator4.Size = New System.Drawing.Size(141, 6)
        '
        'mMSelectAll
        '
        Me.mMSelectAll.Image = My.Resources.SelectAll
        Me.mMSelectAll.Name = "mMSelectAll"
        Me.mMSelectAll.Size = New System.Drawing.Size(144, 22)
        Me.mMSelectAll.Text = "Select &All"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mMCustomize})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.ToolsToolStripMenuItem.Text = "&Tools"
        '
        'mMCustomize
        '
        Me.mMCustomize.Image = My.Resources.Customize
        Me.mMCustomize.Name = "mMCustomize"
        Me.mMCustomize.Size = New System.Drawing.Size(130, 22)
        Me.mMCustomize.Text = "&Customize"
        '
        'toolStripSeparator5
        '
        Me.toolStripSeparator5.Name = "toolStripSeparator5"
        Me.toolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '


        'Me.Tools.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tbNew, Me.tbOpen, Me.tbSave, Me.toolStripSeparator1, Me.tbCut, Me.tbCopy, Me.tbPaste, Me.toolStripSeparator5, Me.tbBold, Me.tbItalic, Me.tbUnderline, Me.tbStrike, Me.ToolStripSeparator6, Me.tbBackColor, Me.tbColor, Me.tbImage, Me.toolStripSeparator1, Me.tbAlignLeft, Me.tbAlignCentre, Me.tbAlignRight, Me.ToolStripSeparator7, Me.tbUpper, Me.tbLower, Me.ToolStripSeparator8, Me.tbZoom, Me.tbZoomOut, Me.ToolStripSeparator9, Me.tbSelectFont, Me.tbSelectSize})
        'Me.Tools.Items.AddRange(ToolsRanger.All)
        Me.Tools.Location = New System.Drawing.Point(0, 0)
        Me.Tools.Name = "Tools"
        Me.Tools.Size = New System.Drawing.Size(829, 25)
        Me.Tools.TabIndex = 5
        Me.Tools.Text = "ToolStrip1"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'mMNew
        '
        Me.mMNew.Image = My.Resources.mMNew_Image 'Me.mMNew.Image = CType(resources.GetObject("mMNew.Image"), System.Drawing.Image)
        Me.mMNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mMNew.Name = "mMNew"
        Me.mMNew.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.mMNew.Size = New System.Drawing.Size(146, 22)
        Me.mMNew.Text = "&New"
        ''
        ''mMOpen
        '
        Me.mMOpen.Image = My.Resources.mMOpen_Image 'CType(resources.GetObject("mMOpen.Image"), System.Drawing.Image)
        Me.mMOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mMOpen.Name = "mMOpen"
        Me.mMOpen.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.mMOpen.Size = New System.Drawing.Size(146, 22)
        Me.mMOpen.Text = "&Open"
        ''
        ''mMSave
        ''
        Me.mMSave.Image = My.Resources.mMSave_Image 'CType(resources.GetObject("mMSave.Image"), System.Drawing.Image)
        Me.mMSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mMSave.Name = "mMSave"
        Me.mMSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mMSave.Size = New System.Drawing.Size(146, 22)
        Me.mMSave.Text = "&Save"
        ''
        ''mMCut
        ''
        Me.mMCut.Image = My.Resources.mMCut_Image 'CType(resources.GetObject("mMCut.Image"), System.Drawing.Image)
        Me.mMCut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mMCut.Name = "mMCut"
        Me.mMCut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.mMCut.Size = New System.Drawing.Size(144, 22)
        Me.mMCut.Text = "Cu&t"
        ''
        ''mMCopy
        ''
        Me.mMCopy.Image = My.Resources.mMCopy_Image 'CType(resources.GetObject("mMCopy.Image"), System.Drawing.Image)
        Me.mMCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mMCopy.Name = "mMCopy"
        Me.mMCopy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.mMCopy.Size = New System.Drawing.Size(144, 22)
        Me.mMCopy.Text = "&Copy"
        ''
        ''mMPaste
        ''
        Me.mMPaste.Image = My.Resources.mMPaste_Image 'CType(resources.GetObject("mMPaste.Image"), System.Drawing.Image)
        Me.mMPaste.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mMPaste.Name = "mMPaste"
        Me.mMPaste.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.mMPaste.Size = New System.Drawing.Size(144, 22)
        Me.mMPaste.Text = "Paste"
        ''
        'tbNew
        '

        Me.tbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbNew.Image = My.Resources.tbNew_Image 'CType(resources.GetObject("tbNew.Image"), System.Drawing.Image)
        Me.tbNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbNew.Name = "tbNew"
        Me.tbNew.Size = New System.Drawing.Size(23, 22)
        Me.tbNew.Text = "&New"
        '
        'tbOpen
        '
        Me.tbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbOpen.Image = My.Resources.tbOpen_Image 'CType(resources.GetObject("tbOpen.Image"), System.Drawing.Image)
        Me.tbOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbOpen.Name = "tbOpen"
        Me.tbOpen.Size = New System.Drawing.Size(23, 22)
        Me.tbOpen.Text = "&Open"
        ''
        ''tbSave
        ''
        Me.tbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbSave.Image = My.Resources.tbSave_Image 'CType(resources.GetObject("tbSave.Image"), System.Drawing.Image)
        Me.tbSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbSave.Name = "tbSave"
        Me.tbSave.Size = New System.Drawing.Size(23, 22)
        Me.tbSave.Text = "&Save"
        ''
        ''tbCut
        ''
        Me.tbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbCut.Image = My.Resources.tbCut_Image 'CType(resources.GetObject("tbCut.Image"), System.Drawing.Image)
        Me.tbCut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbCut.Name = "tbCut"
        Me.tbCut.Size = New System.Drawing.Size(23, 22)
        Me.tbCut.Text = "C&ut"
        ''
        ''tbCopy
        ''
        Me.tbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbCopy.Image = My.Resources.tbCopy_Image 'CType(resources.GetObject("tbCopy.Image"), System.Drawing.Image)
        Me.tbCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbCopy.Name = "tbCopy"
        Me.tbCopy.Size = New System.Drawing.Size(23, 22)
        Me.tbCopy.Text = "&Copy"
        ''
        ''tbPaste
        ''
        Me.tbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbPaste.Image = My.Resources.tbPaste_Image 'CType(resources.GetObject("tbPaste.Image"), System.Drawing.Image)
        Me.tbPaste.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbPaste.Name = "tbPaste"
        Me.tbPaste.Size = New System.Drawing.Size(23, 22)
        Me.tbPaste.Text = "&Paste"
        ''
        ''tbBold
        ''
        Me.tbBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tbBold.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Me.tbBold.Image = My.Resources.tbBold_Image 'CType(resources.GetObject("tbBold.Image"), System.Drawing.Image)
        Me.tbBold.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbBold.Name = "tbBold"
        Me.tbBold.Size = New System.Drawing.Size(23, 22)
        Me.tbBold.Text = "B"
        Me.tbBold.ToolTipText = "Bolt"
        ''
        ''tbItalic
        ''
        Me.tbItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tbItalic.Font = New System.Drawing.Font("Arial Rounded MT Bold", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Me.tbItalic.Image = My.Resources.tbItalic_Image 'CType(resources.GetObject("tbItalic.Image"), System.Drawing.Image)
        Me.tbItalic.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbItalic.Name = "tbItalic"
        Me.tbItalic.Size = New System.Drawing.Size(23, 22)
        Me.tbItalic.Text = "I"
        Me.tbItalic.ToolTipText = "Italic"
        ''
        ''tbUnderline
        ''
        Me.tbUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tbUnderline.Font = New System.Drawing.Font("Arial Rounded MT Bold", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ' Me.tbUnderline.Image = My.Resources.tbUnderline_Image 'CType(resources.GetObject("tbUnderline.Image"), System.Drawing.Image)
        Me.tbUnderline.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbUnderline.Name = "tbUnderline"
        Me.tbUnderline.Size = New System.Drawing.Size(23, 22)
        Me.tbUnderline.Text = "U"
        Me.tbUnderline.ToolTipText = "Underline"
        ''
        ''tbStrike
        ''
        Me.tbStrike.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tbStrike.Font = New System.Drawing.Font("Arial Rounded MT Bold", 9.0!, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Me.tbStrike.Image = My.Resources.tbStrike_Image 'CType(resources.GetObject("tbStrike.Image"), System.Drawing.Image)
        Me.tbStrike.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbStrike.Name = "tbStrike"
        Me.tbStrike.Size = New System.Drawing.Size(23, 22)
        Me.tbStrike.Text = "S"
        Me.tbStrike.ToolTipText = "Strike"

        Me.tbBackColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbBackColor.Image = My.Resources.Pen 'CType(resources.GetObject("tbPaste.Image"), System.Drawing.Image)
        Me.tbBackColor.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbBackColor.Name = "tbRealce"
        Me.tbBackColor.Size = New System.Drawing.Size(23, 22)
        Me.tbBackColor.Text = "&Realce"

        Me.tbColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbColor.Image = My.Resources.Color 'CType(resources.GetObject("tbPaste.Image"), System.Drawing.Image)
        Me.tbColor.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbColor.Name = "tbColor"
        Me.tbColor.Size = New System.Drawing.Size(23, 22)
        Me.tbColor.Text = "&Color"

        Me.tbImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbImage.Image = My.Resources.Imagem 'CType(resources.GetObject("tbPaste.Image"), System.Drawing.Image)
        Me.tbImage.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbImage.Name = "tbImage"
        Me.tbImage.Size = New System.Drawing.Size(23, 22)
        Me.tbImage.Text = "&Image"
        ''
        ''Document
        ''
        Me.Document.ContextMenuStrip = Me.rcMenu
        Me.Document.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Document.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.Document.Location = New System.Drawing.Point(0, 49)
        Me.Document.Name = "Document"
        Me.Document.Size = New System.Drawing.Size(829, 426)
        Me.Document.TabIndex = 7
        Me.Document.Text = ""

        ''
        ''tbAlignLeft
        ''
        Me.tbAlignLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.tbAlignLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbAlignLeft.Font = New System.Drawing.Font("Arial Rounded MT Bold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbAlignLeft.Image = My.Resources.Left_Text
        Me.tbAlignLeft.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tbAlignLeft.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbAlignLeft.Name = "tbAlignLeft"
        Me.tbAlignLeft.Size = New System.Drawing.Size(23, 22)
        Me.tbAlignLeft.Text = "L"
        Me.tbAlignLeft.ToolTipText = "Align to Left"
        ''
        ''tbAlignCentre
        ''
        Me.tbAlignCentre.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.tbAlignCentre.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbAlignCentre.Font = New System.Drawing.Font("Arial Rounded MT Bold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbAlignCentre.Image = My.Resources.Center_Text 'CType(resources.GetObject("tbAlignCentre.Image"), System.Drawing.Image)
        Me.tbAlignCentre.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tbAlignCentre.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbAlignCentre.Name = "tbAlignCentre"
        Me.tbAlignCentre.Size = New System.Drawing.Size(23, 22)
        Me.tbAlignCentre.Text = "C"
        Me.tbAlignCentre.ToolTipText = "Align to Center"
        ''
        ''tbAlignRight
        ''
        Me.tbAlignRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.tbAlignRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbAlignRight.Font = New System.Drawing.Font("Arial Rounded MT Bold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbAlignRight.Image = My.Resources.Right_Text 'CType(resources.GetObject("tbAlignRight.Image"), System.Drawing.Image)
        Me.tbAlignRight.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tbAlignRight.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbAlignRight.Name = "tbAlignRight"
        Me.tbAlignRight.Size = New System.Drawing.Size(23, 22)
        Me.tbAlignRight.Text = "R"
        Me.tbAlignRight.ToolTipText = "Align to Right"
        Me.tbAlignRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        ''
        ''tbUpper
        ''
        Me.tbUpper.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tbUpper.Font = New System.Drawing.Font("Arial Rounded MT Bold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Me.tbUpper.Image = My.Resources.tbUpper_Image 'CType(resources.GetObject("tbUpper.Image"), System.Drawing.Image)
        Me.tbUpper.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbUpper.Name = "tbUpper"
        Me.tbUpper.Size = New System.Drawing.Size(23, 22)
        Me.tbUpper.Text = "A"
        Me.tbUpper.ToolTipText = "To Upper"
        ''
        ''tbLower
        ''
        Me.tbLower.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tbLower.Font = New System.Drawing.Font("Arial Rounded MT Bold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ' Me.tbLower.Image = My.Resources.tbLower_Image 'CType(resources.GetObject("tbLower.Image"), System.Drawing.Image)
        Me.tbLower.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbLower.Name = "tbLower"
        Me.tbLower.Size = New System.Drawing.Size(23, 22)
        Me.tbLower.Text = "a"
        Me.tbLower.ToolTipText = "To Lower"
        ''
        ''tbZoom
        ''
        Me.tbZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbZoom.Font = New System.Drawing.Font("Arial Rounded MT Bold", 9.0!)
        Me.tbZoom.Image = My.Resources.zoom_in 'CType(resources.GetObject("tbZoom.Image"), System.Drawing.Image)
        Me.tbZoom.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbZoom.Name = "tbZoom"
        Me.tbZoom.Size = New System.Drawing.Size(23, 22)
        Me.tbZoom.Text = "+"
        Me.tbZoom.ToolTipText = "Zoon In"
        ''
        ''tbZoomOut
        ''
        Me.tbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tbZoomOut.Font = New System.Drawing.Font("Arial Rounded MT Bold", 9.0!)
        Me.tbZoomOut.Image = My.Resources.zoom_out 'CType(resources.GetObject("tbZoomOut.Image"), System.Drawing.Image)
        Me.tbZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tbZoomOut.Name = "tbZoomOut"
        Me.tbZoomOut.Size = New System.Drawing.Size(23, 22)
        Me.tbZoomOut.Text = "-"
        Me.tbZoomOut.ToolTipText = "Zoon Out"

    End Sub
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            MyBase.Dispose(disposing)
        Finally
        End Try
    End Sub
    Private Sub tbNew_Click(sender As System.Object, e As System.EventArgs) Handles tbNew.Click
        Document.Clear()
    End Sub
    Private Sub tbOpen_Click(sender As System.Object, e As System.EventArgs) Handles tbOpen.Click
        Try
            If openWork.ShowDialog = Windows.Forms.DialogResult.OK Then

                Document.LoadFile(openWork.FileName,
                                  RichTextBoxStreamType.PlainText)

            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub tbSave_Click(sender As System.Object, e As System.EventArgs) Handles tbSave.Click
        Try
            If saveWork.ShowDialog = Windows.Forms.DialogResult.OK Then
                Try
                    Document.SaveFile(saveWork.FileName,
                                      RichTextBoxStreamType.PlainText)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub tbCut_Click(sender As System.Object, e As System.EventArgs) Handles tbCut.Click
        Document.Cut()
    End Sub
    Private Sub tbCopy_Click(sender As System.Object, e As System.EventArgs) Handles tbCopy.Click
        Document.Copy()
    End Sub
    Private Sub tbPaste_Click(sender As System.Object, e As System.EventArgs) Handles tbPaste.Click
        Document.Paste()
    End Sub
    Private Sub tbBold_Click(sender As System.Object, e As System.EventArgs) Handles tbBold.Click
        Try
            Dim bfont As New Font(Document.Font, FontStyle.Bold)
            Dim rfont As New Font(Document.Font, FontStyle.Regular)

            If Document.SelectedText.Length = 0 Then Exit Sub
            If Document.SelectionFont.Bold Then
                Document.SelectionFont = rfont
            Else
                Document.SelectionFont = bfont
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub tbItalic_Click(sender As System.Object, e As System.EventArgs) Handles tbItalic.Click
        Try
            Dim Ifont As New Font(Document.Font, FontStyle.Italic)
            Dim rfont As New Font(Document.Font, FontStyle.Regular)

            If Document.SelectedText.Length = 0 Then Exit Sub
            If Document.SelectionFont.Italic Then
                Document.SelectionFont = rfont
            Else
                Document.SelectionFont = Ifont
            End If
        Catch ex As Exception
        End Try

    End Sub
    Private Sub tbUnderline_Click(sender As System.Object, e As System.EventArgs) Handles tbUnderline.Click
        Try
            Dim Ufont As New Font(Document.Font, FontStyle.Underline)
            Dim rfont As New Font(Document.Font, FontStyle.Regular)

            If Document.SelectedText.Length = 0 Then Exit Sub
            If Document.SelectionFont.Underline Then
                Document.SelectionFont = rfont
            Else
                Document.SelectionFont = Ufont
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub tbStrike_Click(sender As System.Object, e As System.EventArgs) Handles tbStrike.Click
        Try
            Dim Sfont As New Font(Document.Font, FontStyle.Strikeout)
            Dim rfont As New Font(Document.Font, FontStyle.Regular)

            If Document.SelectedText.Length = 0 Then Exit Sub
            If Document.SelectionFont.Strikeout Then
                Document.SelectionFont = rfont
            Else
                Document.SelectionFont = Sfont
            End If
        Catch ex As Exception
        End Try

    End Sub
    Private Sub tbBackColor_Click(sender As System.Object, e As System.EventArgs) Handles tbBackColor.Click
        Try
            Dim ColorPicker As New ColorDialog()
            ColorPicker.AllowFullOpen = True
            ColorPicker.FullOpen = False
            ColorPicker.AnyColor = True

            If ColorPicker.ShowDialog = Windows.Forms.DialogResult.OK Then
                Document.SelectionBackColor = ColorPicker.Color
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub tbColor_Click(sender As System.Object, e As System.EventArgs) Handles tbColor.Click
        Try
            Dim ColorPicker As New ColorDialog()
            ColorPicker.AllowFullOpen = True
            ColorPicker.FullOpen = False
            ColorPicker.AnyColor = True

            If ColorPicker.ShowDialog = Windows.Forms.DialogResult.OK Then

                If Document.SelectionLength > 0 Then
                    Document.SelectionColor = ColorPicker.Color
                Else
                    Document.ForeColor = ColorPicker.Color
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub tbImage_Click(sender As System.Object, e As System.EventArgs) Handles tbImage.Click
        Try
            If OpenImage.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim IMG As Image = Image.FromFile(OpenImage.FileName)
                Clipboard.SetImage(IMG)
                Document.Paste()
                Clipboard.Clear()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub tbAlignLeft_Click(sender As System.Object, e As System.EventArgs) Handles tbAlignLeft.Click
        Document.SelectionAlignment = HorizontalAlignment.Left
    End Sub
    Private Sub tbAlignCentre_Click(sender As System.Object, e As System.EventArgs) Handles tbAlignCentre.Click
        Document.SelectionAlignment = HorizontalAlignment.Center
    End Sub
    Private Sub tbAlignRight_Click(sender As System.Object, e As System.EventArgs) Handles tbAlignRight.Click
        Document.SelectionAlignment = HorizontalAlignment.Right
    End Sub
    Private Sub tbUpper_Click(sender As System.Object, e As System.EventArgs) Handles tbUpper.Click
        Document.SelectedText = Document.SelectedText.ToUpper()
    End Sub
    Private Sub tbLower_Click(sender As System.Object, e As System.EventArgs) Handles tbLower.Click
        Document.SelectedText = Document.SelectedText.ToLower()
    End Sub
    Private Sub tbZoom_Click(sender As System.Object, e As System.EventArgs) Handles tbZoom.Click
        Try
            If Document.ZoomFactor = 63 Then
                Exit Sub
            Else
                Document.ZoomFactor = Document.ZoomFactor + 1
                lblZoom.Text = Document.ZoomFactor.ToString()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub tbZoomOut_Click(sender As System.Object, e As System.EventArgs) Handles tbZoomOut.Click
        Try
            If Document.ZoomFactor = 1 Then
                Exit Sub
            Else

                Document.ZoomFactor = Document.ZoomFactor - 1
                lblZoom.Text = Document.ZoomFactor.ToString()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub tbSelectFont_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles tbSelectFont.SelectedIndexChanged
        Dim ComboFonts As System.Drawing.Font
        Try
            ComboFonts = Document.SelectionFont
            Document.SelectionFont = New System.Drawing.Font(tbSelectFont.Text, Document.SelectionFont.Size, Document.SelectionFont.Style)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub tbSelectSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles tbSelectSize.SelectedIndexChanged
        Document.SelectionFont = New Font(tbSelectSize.SelectedItem.ToString, CInt(tbSelectSize.SelectedItem.ToString), Document.SelectionFont.Style)
    End Sub
    Private Sub UndoToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles UndoToolStripMenuItem.Click
        Document.Undo()
    End Sub
    Private Sub RedoToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RedoToolStripMenuItem.Click
        Document.Redo()
    End Sub
    Private Sub CutToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CutToolStripMenuItem.Click
        Document.Cut()
    End Sub
    Private Sub CopyToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        Document.Copy()
    End Sub
    Private Sub PasteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        Document.Paste()
    End Sub
    Private Sub ColorToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColorToolStripMenuItem.Click
        Try
            Dim ColorPicker As New ColorDialog()
            ColorPicker.AllowFullOpen = True
            ColorPicker.FullOpen = False
            ColorPicker.AnyColor = True

            If ColorPicker.ShowDialog = Windows.Forms.DialogResult.OK Then
                Document.SelectionColor = ColorPicker.Color
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub SelectAllToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        Document.SelectAll()
    End Sub
    Private Sub Document_TextChanged(sender As Object, e As EventArgs) Handles Document.TextChanged
        Try
            lblCharCount.Text = "Characters in the current document: " & Document.TextLength.ToString()
            lblZoom.Text = Document.ZoomFactor.ToString()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Document_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkClickedEventArgs) Handles Document.LinkClicked
        System.Diagnostics.Process.Start(e.LinkText)
    End Sub
    Private Sub mMNew_Click(sender As System.Object, e As System.EventArgs) Handles mMNew.Click
        Document.Clear()
    End Sub
    Private Sub mMOpen_Click(sender As System.Object, e As System.EventArgs) Handles mMOpen.Click
        Try
            If openWork.ShowDialog = Windows.Forms.DialogResult.OK Then

                Document.LoadFile(openWork.FileName,
                                  RichTextBoxStreamType.PlainText)

            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub mMSave_Click(sender As System.Object, e As System.EventArgs) Handles mMSave.Click
        Try
            If saveWork.ShowDialog = Windows.Forms.DialogResult.OK Then
                Try
                    Document.SaveFile(saveWork.FileName,
                                      RichTextBoxStreamType.PlainText)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub mMExit_Click(sender As System.Object, e As System.EventArgs) Handles mMExit.Click
        MyBase.FindForm.Close()
    End Sub
    Private Sub mMUndo_Click(sender As System.Object, e As System.EventArgs) Handles mMUndo.Click
        Document.Undo()
    End Sub
    Private Sub mMRedo_Click(sender As System.Object, e As System.EventArgs) Handles mMRedo.Click
        Document.Redo()
    End Sub
    Private Sub mMCut_Click(sender As System.Object, e As System.EventArgs) Handles mMCut.Click
        Document.Cut()
    End Sub
    Private Sub mMCopy_Click(sender As System.Object, e As System.EventArgs) Handles mMCopy.Click
        Document.Copy()
    End Sub
    Private Sub mMPaste_Click(sender As System.Object, e As System.EventArgs) Handles mMPaste.Click
        Document.Paste()
    End Sub
    Private Sub mMColor_Click(sender As System.Object, e As System.EventArgs) Handles mMColor.Click
        Try
            Dim ColorPicker As New ColorDialog()
            ColorPicker.AllowFullOpen = False
            ColorPicker.FullOpen = True
            ColorPicker.AnyColor = True

            If ColorPicker.ShowDialog = Windows.Forms.DialogResult.OK Then

                If Document.SelectionLength > 0 Then
                    Document.SelectionColor = ColorPicker.Color
                Else
                    Document.ForeColor = ColorPicker.Color
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub mMSelectAll_Click(sender As System.Object, e As System.EventArgs) Handles mMSelectAll.Click
        Document.SelectAll()
    End Sub
    Private Sub mMCustomize_Click(sender As System.Object, e As System.EventArgs) Handles mMCustomize.Click
        Try
            Dim ColorPicker As New ColorDialog()
            ColorPicker.AllowFullOpen = True
            ColorPicker.FullOpen = False
            ColorPicker.AnyColor = True

            If ColorPicker.ShowDialog = Windows.Forms.DialogResult.OK Then
                mainMenu.BackColor = ColorPicker.Color
                Tools.BackColor = ColorPicker.Color
                Status.BackColor = ColorPicker.Color
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
