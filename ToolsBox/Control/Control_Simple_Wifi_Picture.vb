Option Strict On
Imports System.Drawing.Text
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports ToolsBox
Imports ToolsBox.Utils.Bordas_Arredondadas
Imports ToolsBox.Utils

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public MustInherit Class MySimpleWifiPictureControl
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
Public Class Control_Simple_Wifi_Picture
    Inherits MyControl

    Private PictureBox As New PictureBox


    Private Executer As Boolean = False

#Region "Public Property"

    Private _Value As Integer
    Public Property Value As Integer
        Get
            Executer = False
            Return _Value
        End Get

        Set(V As Integer)
            Executer = False
            _Value = V
            Invalidate()
        End Set
    End Property


#End Region

    Sub New()

        _Value = 4
        Me.Size = New Size(127, 127)
        Me.PictureBox.Size = New System.Drawing.Size(127, 127)
        PictureBox.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)

    End Sub

    Protected Overrides Sub CreateHandle()
        MyBase.CreateHandle()
    End Sub
    Public Overrides Sub PaintHook()

        If Executer = False Then

            'PictureBox1
            If _Value <= 0 Then
                Me.PictureBox.Image = My.Resources.Signal_Simple_Picture_0
            ElseIf _Value = 1 Then
                Me.PictureBox.Image = My.Resources.Signal_Simple_Picture_1
            ElseIf _Value = 2 Then
                Me.PictureBox.Image = My.Resources.Signal_Simple_Picture_2
            ElseIf _Value = 3 Then
                Me.PictureBox.Image = My.Resources.Signal_Simple_Picture_3
            ElseIf _Value = 4 Then
                Me.PictureBox.Image = My.Resources.Signal_Simple_Picture_4
            ElseIf _Value >= 4 Then
                Me.PictureBox.Image = My.Resources.Signal_Simple_Picture_4

            End If
            Me.PictureBox.Name = "PictureBox_"
            Me.PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.PictureBox.TabIndex = 0
            Me.PictureBox.Size = Me.Size
            Me.Controls.Add(PictureBox)

            Executer = True
        Else

        End If


    End Sub

End Class

