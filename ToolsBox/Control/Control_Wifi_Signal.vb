Option Strict On
Imports System.Drawing.Text
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports ToolsBox
Imports ToolsBox.Utils.Bordas_Arredondadas
Imports ToolsBox.Utils

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public MustInherit Class MyWifiControl
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
Public Class Control_Wifi_Signal
    Inherits MyControl

    Private Panel1 As New Panel
    Private Panel2 As New Panel
    Private Panel3 As New Panel
    Private Panel4 As New Panel
    Private Panel5 As New Panel
    Private Panel6 As New Panel

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

    Private _SelectedColor As Color
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

    Private _CandlesBorder As BorderStyle
    Public Property CandlesBorder() As BorderStyle
        Get
            Executer = False
            Return _CandlesBorder
        End Get

        Set(V As BorderStyle)
            Executer = False
            _CandlesBorder = V
            Invalidate()
        End Set
    End Property
#End Region

    Sub New()

        _Value = 6
        _SelectedColor = Color.PaleGreen
        _CandlesBorder = BorderStyle.FixedSingle
        Me.Size = New Size(128, 128)

        Panel1.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom, System.Windows.Forms.AnchorStyles)
        Panel2.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom, System.Windows.Forms.AnchorStyles)
        Panel3.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom, System.Windows.Forms.AnchorStyles)
        Panel4.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom, System.Windows.Forms.AnchorStyles)
        Panel5.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom, System.Windows.Forms.AnchorStyles)
        Panel6.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom, System.Windows.Forms.AnchorStyles)
        ' Me.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
    End Sub

    Protected Overrides Sub CreateHandle()

        Me.Panel1.Location = New System.Drawing.Point(4, 74)
        Me.Panel1.Size = New System.Drawing.Size(15, 50)

        Me.Panel2.Location = New System.Drawing.Point(25, 64)
        Me.Panel2.Size = New System.Drawing.Size(15, 60)

        Me.Panel3.Location = New System.Drawing.Point(46, 54)
        Me.Panel3.Size = New System.Drawing.Size(15, 70)

        Me.Panel4.Location = New System.Drawing.Point(67, 44)
        Me.Panel4.Size = New System.Drawing.Size(15, 80)

        Me.Panel5.Location = New System.Drawing.Point(88, 34)
        Me.Panel5.Size = New System.Drawing.Size(15, 90)

        Me.Panel6.Location = New System.Drawing.Point(109, 24)
        Me.Panel6.Size = New System.Drawing.Size(15, 100)
        MyBase.CreateHandle()
    End Sub
    Public Overrides Sub PaintHook()

        If Executer = False Then

            'Panel1
            '
            Me.Panel1.BackColor = _SelectedColor
            Me.Panel1.BorderStyle = _CandlesBorder
            Me.Panel1.Name = "Panel1"
            Me.Panel1.TabIndex = 0

            'Panel2
            '
            Me.Panel2.BackColor = _SelectedColor
            Me.Panel2.BorderStyle = _CandlesBorder
            Me.Panel2.Name = "Panel2"
            Me.Panel2.TabIndex = 1

            'Panel3
            '
            Me.Panel3.BackColor = _SelectedColor
            Me.Panel3.BorderStyle = _CandlesBorder
            Me.Panel3.Name = "Panel3"
            Me.Panel3.TabIndex = 2

            'Panel4
            '
            Me.Panel4.BackColor = _SelectedColor
            Me.Panel4.BorderStyle = _CandlesBorder
            Me.Panel4.Name = "Panel4"
            Me.Panel4.TabIndex = 3

            'Panel5
            '
            Me.Panel5.BackColor = _SelectedColor
            Me.Panel5.BorderStyle = _CandlesBorder
            Me.Panel5.Name = "Panel5"
            Me.Panel5.TabIndex = 4

            'Panel6
            '
            Me.Panel6.BackColor = _SelectedColor
            Me.Panel6.BorderStyle = _CandlesBorder
            Me.Panel6.Name = "Panel6"
            Me.Panel6.TabIndex = 5

            If _Value <= 0 Then
                Me.Controls.Clear()
            ElseIf _Value = 1 Then
                Me.Controls.Clear()
                Me.Controls.Add(Me.Panel1)
            ElseIf _Value = 2 Then
                Me.Controls.Clear()
                Me.Controls.Add(Me.Panel1)
                Me.Controls.Add(Me.Panel2)
            ElseIf _Value = 3 Then
                Me.Controls.Clear()
                Me.Controls.Add(Me.Panel1)
                Me.Controls.Add(Me.Panel2)
                Me.Controls.Add(Me.Panel3)
            ElseIf _Value = 4 Then
                Me.Controls.Clear()
                Me.Controls.Add(Me.Panel1)
                Me.Controls.Add(Me.Panel2)
                Me.Controls.Add(Me.Panel3)
                Me.Controls.Add(Me.Panel4)
            ElseIf _Value = 5 Then
                Me.Controls.Clear()
                Me.Controls.Add(Me.Panel1)
                Me.Controls.Add(Me.Panel2)
                Me.Controls.Add(Me.Panel3)
                Me.Controls.Add(Me.Panel4)
                Me.Controls.Add(Me.Panel5)
            ElseIf _Value = 6 Then
                Me.Controls.Clear()
                Me.Controls.Add(Me.Panel1)
                Me.Controls.Add(Me.Panel2)
                Me.Controls.Add(Me.Panel3)
                Me.Controls.Add(Me.Panel4)
                Me.Controls.Add(Me.Panel5)
                Me.Controls.Add(Me.Panel6)
            ElseIf _Value >= 6 Then
                Me.Controls.Clear()
                Me.Controls.Add(Me.Panel1)
                Me.Controls.Add(Me.Panel2)
                Me.Controls.Add(Me.Panel3)
                Me.Controls.Add(Me.Panel4)
                Me.Controls.Add(Me.Panel5)
                Me.Controls.Add(Me.Panel6)
            End If

            'Panel1.Size = Proporcao(Panel1.Height, Panel1.Width)
            'Panel2.Size = Proporcao(Panel1.Height, Panel1.Width)
            'Panel3.Size = Proporcao(Panel1.Height, Panel1.Width)
            'Panel4.Size = Proporcao(Panel1.Height, Panel1.Width)
            'Panel5.Size = Proporcao(Panel1.Height, Panel1.Width)
            'Panel6.Size = Proporcao(Panel1.Height, Panel1.Width)

            Me.Name = "PanelPrincipal"
            Me.TabIndex = 6
            Executer = True
        Else

        End If

    End Sub

    Private Function Proporcao(ByVal X As Integer, ByVal Y As Integer) As Size
        ' Dim MySize As New Point(Me.Size)
        Dim Result As New Point(CInt(X), CInt(Me.Size.Width / Y))
        Return CType(Result, Drawing.Size)
    End Function

End Class

