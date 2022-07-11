Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports ToolsBox.Controller
<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Eight_Interrogation
    Inherits Panel

    Friend WithEvents ToolTip1 As New System.Windows.Forms.ToolTip
    Private State As MouseState = MouseState.None
    Private x As Integer

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over
    End Sub
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over
    End Sub
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        x = e.X
    End Sub
    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
    End Sub
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
    End Sub

    Private _Object As Object
    <Category("ToolsBox Herramienta")> _
    Public Property ObjectOpen As Object
        Get
            Return _Object
        End Get
        Set(value As Object)
            _Object = value
            Invalidate()
        End Set
    End Property

    Private _ItemColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property ItemColor As Color
        Get
            Return _ItemColor
        End Get
        Set(value As Color)
            _ItemColor = value
            Invalidate()
        End Set
    End Property

    Private _RoundedBorder As Boolean
    <Category("ToolsBox Herramienta")> _
    Public Property RoundedBorder As Boolean
        Get
            Return _RoundedBorder
        End Get
        Set(value As Boolean)
            _RoundedBorder = value
            Invalidate()
        End Set
    End Property

    Private _ItemSize As Integer
    <Category("ToolsBox Herramienta")> _
    Public Property ItemSize As Integer
        Get
            Return _ItemSize
        End Get
        Set(value As Integer)
            _ItemSize = value
            Invalidate()
        End Set
    End Property

    Private _ToolTip As Boolean = True
    <Category("ToolsBox Herramienta")> _
    Public Property ToolTip As Boolean
        Get
            Return _ToolTip
        End Get
        Set(value As Boolean)
            _ToolTip = value
            Invalidate()
        End Set
    End Property

    Private _ToolTipText As String = "Text Here"
    <Category("ToolsBox Herramienta")> _
    Public Property ToolTipText As String
        Get
            Return _ToolTip
        End Get
        Set(value As String)
            _ToolTip = value
            Invalidate()
        End Set
    End Property

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Size = New Size(18, 18)
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.Cursor = Cursors.Hand
        Me.BackColor = Color.FromArgb(45, 47, 49)
        _ItemColor = Color.FromArgb(243, 243, 243)
        _ItemSize = 10
        _RoundedBorder = True

    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)
        Dim Base As New Rectangle(0, 0, Width, Height)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Base
            .FillRectangle(New SolidBrush(BackColor), Base)

            '-- X
            .DrawString("s", New Font("Marlett", _ItemSize), New SolidBrush(_ItemColor), New Rectangle(0, 0, Width, Height), CenterSF)

            '-- Hover/down
            Select Case State
                Case MouseState.Over
                    .FillRectangle(New SolidBrush(Color.FromArgb(30, Color.White)), Base)
                Case MouseState.Down
                    .FillRectangle(New SolidBrush(Color.FromArgb(30, Color.Black)), Base)
            End Select
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()

        If _RoundedBorder Then
            Utils.CarregaBordas(Me, 3, 3)
        End If

        If _ToolTip = True Then
            ToolTip1.SetToolTip(Me, _ToolTipText)
        End If

    End Sub
End Class

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Eight_Close
    Inherits Panel
    Friend WithEvents ToolTip1 As New System.Windows.Forms.ToolTip
    Private State As MouseState = MouseState.None
    Private x As Integer

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over
    End Sub
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over
    End Sub
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        x = e.X
    End Sub
    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
        If _CloseToHide = Closerme.MeExit Then
            MyBase.FindForm.Close()
            Environment.Exit(0)
        ElseIf _CloseToHide = Closerme.MeClose Then
            MyBase.FindForm.Close()
        ElseIf _CloseToHide = Closerme.MeHide Then
            MyBase.FindForm.Hide()
        End If
    End Sub
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
    End Sub
    Enum Closerme As Integer
        MeExit = 0
        MeClose = 1
        MeHide = 2
    End Enum

    Private _CloseToHide As Closerme
    <Category("ToolsBox Herramienta")> _
    Public Property CloseToHide As Closerme
        Get
            Return _CloseToHide
        End Get
        Set(value As Closerme)
            _CloseToHide = value
            Invalidate()
        End Set
    End Property

    Private _ItemColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property ItemColor As Color
        Get
            Return _ItemColor
        End Get
        Set(value As Color)
            _ItemColor = value
            Invalidate()
        End Set
    End Property

    Private _RoundedBorder As Boolean
    <Category("ToolsBox Herramienta")> _
    Public Property RoundedBorder As Boolean
        Get
            Return _RoundedBorder
        End Get
        Set(value As Boolean)
            _RoundedBorder = value
            Invalidate()
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
            Invalidate()
        End Set
    End Property

    Private _ToolTip As Boolean = True
    <Category("ToolsBox Herramienta")> _
    Public Property ToolTip As Boolean
        Get
            Return _ToolTip
        End Get
        Set(value As Boolean)
            _ToolTip = value
            Invalidate()
        End Set
    End Property

    Private _ItemSize As Integer
    <Category("ToolsBox Herramienta")>
    Public Property ItemSize As Integer
        Get
            Return _ItemSize
        End Get
        Set(value As Integer)
            _ItemSize = value
            Invalidate()
        End Set
    End Property
    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Size = New Size(18, 18)
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.Cursor = Cursors.Hand
        Me.BackColor = Color.FromArgb(45, 47, 49)
        _ItemColor = Color.FromArgb(243, 243, 243)
        _ItemSize = 10
        _RoundedBorder = True

    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)
        Dim Base As New Rectangle(0, 0, Width, Height)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Base
            .FillRectangle(New SolidBrush(BackColor), Base)

            '-- X
            .DrawString("r", New Font("Marlett", _ItemSize), New SolidBrush(_ItemColor), New Rectangle(0, 0, Width, Height), CenterSF)


            If _MouseStateRetangle = True Then
                '-- Hover/down
                Select Case State
                    Case MouseState.Over
                        .FillRectangle(New SolidBrush(Color.FromArgb(30, Color.White)), Base)
                    Case MouseState.Down
                        .FillRectangle(New SolidBrush(Color.FromArgb(30, Color.Black)), Base)
                End Select
            End If
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()

        If _RoundedBorder Then
            Utils.CarregaBordas(Me, 3, 3)
        End If

        If _ToolTip = True Then
            ToolTip1.SetToolTip(Me, "Close.")
        End If
    End Sub
End Class

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Eight_Max
    Inherits Panel

    Friend WithEvents ToolTip1 As New System.Windows.Forms.ToolTip
    Private State As MouseState = MouseState.None
    Private x As Integer

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over
    End Sub
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down

    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None

    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over

    End Sub
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        x = e.X

    End Sub
    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
        Select Case MyBase.FindForm.WindowState
            Case FormWindowState.Maximized
                MyBase.FindForm.WindowState = FormWindowState.Normal
                If _RoundedBorder Then
                    Utils.CarregaBordas(Me, 3, 3)
                End If
            Case FormWindowState.Normal
                MyBase.FindForm.WindowState = FormWindowState.Maximized
                If _RoundedBorder Then
                    Utils.CarregaBordas(Me, 3, 3)
                End If
        End Select
    End Sub
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
    End Sub

    Private _ItemColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property ItemColor As Color
        Get
            Return _ItemColor
        End Get
        Set(value As Color)
            _ItemColor = value

            Invalidate()
        End Set
    End Property

    Private _RoundedBorder As Boolean
    <Category("ToolsBox Herramienta")> _
    Public Property RoundedBorder As Boolean
        Get
            Return _RoundedBorder
        End Get
        Set(value As Boolean)
            _RoundedBorder = value

            Invalidate()
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

            Invalidate()
        End Set
    End Property

    Private _ItemSize As Integer
    <Category("ToolsBox Herramienta")> _
    Public Property ItemSize As Integer
        Get
            Return _ItemSize
        End Get
        Set(value As Integer)
            _ItemSize = value

            Invalidate()
        End Set
    End Property

    Private _ToolTip As Boolean = True
    <Category("ToolsBox Herramienta")> _
    Public Property ToolTip As Boolean
        Get
            Return _ToolTip
        End Get
        Set(value As Boolean)
            _ToolTip = value

            Invalidate()
        End Set
    End Property

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Size = New Size(18, 18)
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.Cursor = Cursors.Hand
        Me.BackColor = Color.FromArgb(45, 47, 49)
        _ItemColor = Color.FromArgb(243, 243, 243)
        _ItemSize = 12
        _RoundedBorder = True
    End Sub

    Dim Automat As Boolean = True
    Protected Overrides Sub OnPaint(e As PaintEventArgs)

        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)

        Dim Base As New Rectangle(0, 0, Width, Height)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Base
            .FillRectangle(New SolidBrush(BackColor), Base)

            '-- Maximize
            If FindForm.WindowState = FormWindowState.Maximized Then
                .DrawString("2", New Font("Marlett", _ItemSize), New SolidBrush(_ItemColor), New Rectangle(1, 1, Width, Height), CenterSF)
            ElseIf FindForm.WindowState = FormWindowState.Normal Then
                .DrawString("1", New Font("Marlett", _ItemSize), New SolidBrush(_ItemColor), New Rectangle(1, 1, Width, Height), CenterSF)
            End If


            If _MouseStateRetangle = True Then
                '-- Hover/down
                Select Case State
                    Case MouseState.Over
                        .FillRectangle(New SolidBrush(Color.FromArgb(30, Color.White)), Base)
                    Case MouseState.Down
                        .FillRectangle(New SolidBrush(Color.FromArgb(30, Color.Black)), Base)
                End Select
            End If
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()

        If _RoundedBorder Then
            Utils.CarregaBordas(Me, 3, 3)
        End If

        If _ToolTip = True Then
            If FindForm.WindowState = FormWindowState.Maximized Then
                ToolTip1.SetToolTip(Me, "Restaure.")
            ElseIf FindForm.WindowState = FormWindowState.Normal Then
                ToolTip1.SetToolTip(Me, "Maximize.")
            End If
        End If

    End Sub
End Class

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Eight_Mini
    Inherits Panel

    Friend WithEvents ToolTip1 As New System.Windows.Forms.ToolTip
    Private State As MouseState = MouseState.None
    Private x As Integer


    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over


    End Sub
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down


    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None


    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over


    End Sub
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        x = e.X


    End Sub
    Protected Overrides Sub OnClick(e As EventArgs)
        Try
            MyBase.OnClick(e)
            Select Case FindForm.WindowState
                Case FormWindowState.Normal
                    FindForm.WindowState = FormWindowState.Minimized
                Case FormWindowState.Maximized
                    FindForm.WindowState = FormWindowState.Minimized
            End Select
        Catch
        End Try
    End Sub
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

    End Sub


    Private _ItemColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property ItemColor As Color
        Get
            Return _ItemColor
        End Get
        Set(value As Color)
            _ItemColor = value

            Invalidate()
        End Set
    End Property

    Private _RoundedBorder As Boolean
    <Category("ToolsBox Herramienta")> _
    Public Property RoundedBorder As Boolean
        Get
            Return _RoundedBorder
        End Get
        Set(value As Boolean)
            _RoundedBorder = value

            Invalidate()
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

            Invalidate()
        End Set
    End Property

    Private _ItemSize As Integer
    <Category("ToolsBox Herramienta")> _
    Public Property ItemSize As Integer
        Get
            Return _ItemSize
        End Get
        Set(value As Integer)
            _ItemSize = value

            Invalidate()
        End Set
    End Property

    Private _ToolTip As Boolean = True
    <Category("ToolsBox Herramienta")> _
    Public Property ToolTip As Boolean
        Get
            Return _ToolTip
        End Get
        Set(value As Boolean)
            _ToolTip = value

            Invalidate()
        End Set
    End Property

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Size = New Size(18, 18)
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.Cursor = Cursors.Hand
        Me.BackColor = Color.FromArgb(45, 47, 49)
        _ItemColor = Color.FromArgb(243, 243, 243)
        _ItemSize = 12
        _RoundedBorder = True
    End Sub

    Dim Automat As Boolean = True
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)

        Dim Base As New Rectangle(0, 0, Width, Height)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Base
            .FillRectangle(New SolidBrush(BackColor), Base)

            '-- Minimize
            .DrawString("0", New Font("Marlett", _ItemSize), New SolidBrush(_ItemColor), New Rectangle(2, 1, Width, Height), CenterSF)

            If _MouseStateRetangle = True Then
                '-- Hover/down
                Select Case State
                    Case MouseState.Over
                        .FillRectangle(New SolidBrush(Color.FromArgb(30, Color.White)), Base)
                    Case MouseState.Down
                        .FillRectangle(New SolidBrush(Color.FromArgb(30, Color.Black)), Base)
                End Select
            End If
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()

        If _RoundedBorder Then
            Utils.CarregaBordas(Me, 3, 3)
        End If

        If _ToolTip = True Then
            ToolTip1.SetToolTip(Me, "Minimize.")
        End If

    End Sub
End Class

<ToolboxBitmap(GetType(UserControl), "UserControl")> _
Public Class Control_Box_Eight_Hide
    Inherits Panel

    Friend WithEvents ToolTip1 As New System.Windows.Forms.ToolTip
    Private WithEvents Notifyicon1 As New NotifyIcon
    Private State As MouseState = MouseState.None
    Private x As Integer


    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over

    End Sub
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down

    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None

    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over

    End Sub
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        x = e.X

    End Sub
    Protected Overrides Sub OnClick(e As EventArgs)
        Try
            MyBase.OnClick(e)

            If _AnuleHide = True Then
                MyBase.FindForm.Hide()
            End If

            If _Notifyicon = True Then
                Notifyicon1.Visible = True
                Notifyicon1.Icon = _NotifyIconIcon
                Notifyicon1.BalloonTipTitle = _NotifyIconTitle
                Notifyicon1.BalloonTipText = _NotifyIconText
                Notifyicon1.ShowBalloonTip(_BalloonTipTime)
            End If

        Catch
        End Try
    End Sub
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

    End Sub
    Private Sub Notifyicon1_Show(Sender As Object, e As EventArgs) Handles Notifyicon1.DoubleClick
        Try
            MyBase.FindForm.Show()
            Notifyicon1.Visible = False
        Catch
        End Try
    End Sub

    Private _AnuleHide As Boolean = True
    <Category("ToolsBox Herramienta")>
    Public Property AnuleHide As Boolean
        Get
            Return _AnuleHide
        End Get
        Set(value As Boolean)
            _AnuleHide = value

            Invalidate()
        End Set
    End Property

    Private _Checked As Boolean = True
    <Category("ToolsBox Herramienta")>
    Public Property Checked As Boolean
        Get
            Return _Checked
        End Get
        Set(value As Boolean)
            _Checked = value
            Invalidate()
        End Set
    End Property

    Private _ItemColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property ItemColor As Color
        Get
            Return _ItemColor
        End Get
        Set(value As Color)
            _ItemColor = value

            Invalidate()
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

            Invalidate()
        End Set
    End Property

    Private _RoundedBorder As Boolean
    <Category("ToolsBox Herramienta")> _
    Public Property RoundedBorder As Boolean
        Get
            Return _RoundedBorder
        End Get
        Set(value As Boolean)
            _RoundedBorder = value

            Invalidate()
        End Set
    End Property

    Private _ItemSize As Integer
    <Category("ToolsBox Herramienta")> _
    Public Property ItemSize As Integer
        Get
            Return _ItemSize
        End Get
        Set(value As Integer)
            _ItemSize = value

            Invalidate()
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

            Invalidate()
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

            Invalidate()
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

            Invalidate()
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

            Invalidate()
        End Set
    End Property

    Private _NotifyIconIcon As Drawing.Icon
    <Category("ToolsBox Herramienta")> _
    Public Property NotifyIconIcon As Drawing.Icon
        Get
            Return _NotifyIconIcon
        End Get
        Set(value As Drawing.Icon)
            _NotifyIconIcon = value

            Invalidate()
        End Set
    End Property

    Private _ToolTip As Boolean = True
    <Category("ToolsBox Herramienta")> _
    Public Property ToolTip As Boolean
        Get
            Return _ToolTip
        End Get
        Set(value As Boolean)
            _ToolTip = value

            Invalidate()
        End Set
    End Property

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint Or _
                 ControlStyles.ResizeRedraw Or ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Size = New Size(18, 18)
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Me.Cursor = Cursors.Hand
        Me.BackColor = Color.FromArgb(45, 47, 49)
        _ItemColor = Color.FromArgb(243, 243, 243)
        _ItemSize = 12
        _RoundedBorder = True
        _NotifyIconIcon = My.Resources.hide_Icon_18px
        _NotifyIconTitle = "My Program"
        _NotifyIconText = "Minimized to Tray System."
        _BalloonTipTime = 3000

    End Sub

    Dim Automat As Boolean = True
    Protected Overrides Sub OnPaint(e As PaintEventArgs)

        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)

        Dim Base As New Rectangle(0, 0, Width, Height)

        With G
            .SmoothingMode = 2
            .PixelOffsetMode = 2
            .TextRenderingHint = 5
            .Clear(BackColor)

            '-- Base
            .FillRectangle(New SolidBrush(BackColor), Base)

            '-- Minimize
            '.DrawString("0", New Font("Marlett", _ItemSize), New SolidBrush(_ItemColor), New Rectangle(2, 1, Width, Height), CenterSF)
            .DrawImage(My.Resources.hide_16px, 1, 1)

            If _MouseStateRetangle = True Then
                '-- Hover/down
                Select Case State
                    Case MouseState.Over
                        .FillRectangle(New SolidBrush(Color.FromArgb(30, Color.White)), Base)
                    Case MouseState.Down
                        .FillRectangle(New SolidBrush(Color.FromArgb(30, Color.Black)), Base)
                End Select
            End If
        End With

        MyBase.OnPaint(e)
        G.Dispose()
        e.Graphics.InterpolationMode = 7
        e.Graphics.DrawImageUnscaled(B, 0, 0)
        B.Dispose()

        If _RoundedBorder Then
            Utils.CarregaBordas(Me, 3, 3)
        End If

        If _ToolTip = True Then
            ToolTip1.SetToolTip(Me, "Hide.")
        End If

    End Sub
End Class
