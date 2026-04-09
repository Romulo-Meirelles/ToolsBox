Imports System.Drawing.Text
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Drawing
Imports System.ComponentModel


<ToolboxBitmap(GetType(FormAvastOld), "Purple.ico")>
Public Class FormAvastOld
    Inherits ContainerControl

    Private F As Font
    Private T As String
    Private _PageName As String = "Scan"
    Private _PageImage As Image

    Private MouseP As Point = New Point(0, 0)
    Private Cap As Boolean = False
    Private MoveHeight As Integer = 48

    Protected Overrides Sub OnHandleDestroyed(ByVal e As EventArgs)
        Try
            Dock = DockStyle.None
            ParentForm.BackColor = Color.FromArgb(240, 240, 240)
            ParentForm.FormBorderStyle = FormBorderStyle.Sizable
            MyBase.OnHandleDestroyed(e)
        Catch ex As Exception
        End Try
    End Sub

    <Category("Super Forms")>
    Public Property PageName() As String
        Get
            Return _PageName
        End Get
        Set(v As String)
            _PageName = v : Invalidate()
        End Set
    End Property

    <Category("Super Forms")>
    Public Property PageImage() As Image
        Get
            Return _PageImage
        End Get
        Set(v As Image)
            _PageImage = v
        End Set
    End Property

    Private _PageTextColor As Color = Color.White
    <Category("Super Forms")>
    Public Property PageTextColor() As Color
        Get
            Return _PageTextColor
        End Get
        Set(ByVal v As Color)
            _PageTextColor = v
            Invalidate()
        End Set
    End Property

    Private _TextColor As Color = Color.Black
    <Category("Super Forms")>
    Public Property TextColor() As Color
        Get
            Return _TextColor
        End Get
        Set(ByVal v As Color)
            _TextColor = v
            Invalidate()
        End Set
    End Property

    Private _ShowText As Boolean = True
    <Category("Super Forms")>
    Public Property ShowText() As Boolean
        Get
            Return _ShowText
        End Get
        Set(ByVal value As Boolean)
            _ShowText = value
            Invalidate()
        End Set
    End Property

    Private _RetangleColor As Color = Color.FromArgb(0, 166, 208)
    <Category("Super Forms")>
    Public Property RetangleColor() As Color
        Get
            Return _RetangleColor
        End Get
        Set(ByVal v As Color)
            _RetangleColor = v
            Invalidate()
        End Set
    End Property
    Private _Icon As Icon = My.Resources.ToolsBox

    <Category("Super Forms")>
    Public Property Icon() As Icon
        Get
            Me.ParentForm.Icon = _Icon
            Return _Icon
        End Get
        Set(ByVal value As Icon)
            _Icon = value
            Invalidate()
        End Set
    End Property

    Private _ShowIcon As Boolean = True
    <Category("Super Forms")>
    Public Property ShowIcon() As Boolean
        Get
            Return _ShowIcon
        End Get
        Set(ByVal value As Boolean)
            _ShowIcon = value
            Invalidate()
        End Set
    End Property

    Sub New()
        MyBase.New()
        Me.BackColor = Color.FromArgb(242, 242, 242)

        DoubleBuffered = True
        Me.Dock = DockStyle.Fill

        F = New Font("Segoe UI", 13, FontStyle.Regular)
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        If e.Button = MouseButtons.Left And New Rectangle(0, 0, Width, MoveHeight).Contains(e.Location) Then
            Cap = True : MouseP = e.Location
        End If
    End Sub
    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e) : Cap = False
    End Sub
    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        If Cap Then
            Parent.Location = MousePosition - MouseP
        End If
    End Sub

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        Dim G As Graphics
        G = e.Graphics

        G.SmoothingMode = SmoothingMode.None

        Dim I As Bitmap = Me.ParentForm.Icon.ToBitmap : _Icon = Me.ParentForm.Icon
        Dim IM As Image = I
        T = Me.ParentForm.Text

        G.FillRectangle(New SolidBrush(_RetangleColor), New Rectangle(0, 48, Me.Width, 70))
        G.DrawLine(New Pen(New SolidBrush(Color.FromArgb(203, 203, 203))), New Point(0, Me.Height - 49), New Point(Me.Width, Me.Height - 49))

        If _ShowText Then G.DrawString(T, F, New SolidBrush(_TextColor), Me.Width / 2 - G.MeasureString(T, F).Width / 2, 24 - G.MeasureString(T, F).Height / 2)
        If _ShowIcon Then G.DrawImage(IM, New Point(Me.Width / 2 - G.MeasureString(T, F).Width / 2 - IM.Width - 2, IM.Height / 2 - 7))

        If Not Me.ParentForm.FormBorderStyle = FormBorderStyle.None Then
            Me.ParentForm.FormBorderStyle = FormBorderStyle.None
        End If

        G.TextRenderingHint = TextRenderingHint.AntiAlias
        G.DrawString(_PageName, New Font("Verdana", 17, FontStyle.Regular), New SolidBrush(_PageTextColor), 105, 70)

        If Not _PageImage Is Nothing Then
            G.DrawImage(_PageImage, New Point(70, 84 - _PageImage.Height / 2))
        End If
    End Sub
End Class
