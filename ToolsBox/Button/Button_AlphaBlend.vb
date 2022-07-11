Imports System.ComponentModel
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.TextBox))> _
Public Class TextBox_AlphaBlend
    Inherits TextBox

    Private myPictureBox As uPictureBox
    Private myUpToDate As Boolean = False
    Private myCaretUpToDate As Boolean = False
    Private myBitmap As Bitmap
    Private myAlphaBitmap As Bitmap
    Private myFontHeight As Integer = 10
    Private myTimer1 As System.Windows.Forms.Timer
    Private myCaretState As Boolean = True
    Private myPaintedFirstTime As Boolean = False
    Private myBackColor As Color = Color.White
    Private _Opacity As Integer = 10
    Private components As System.ComponentModel.Container = Nothing

    Public Sub New()
        InitializeComponent()
        Me.BackColor = myBackColor
        Me.SetStyle(ControlStyles.UserPaint, False)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.DoubleBuffer, True)
        myPictureBox = New uPictureBox()
        Me.Controls.Add(myPictureBox)
        myPictureBox.Dock = DockStyle.Fill
    End Sub


    Protected Overrides Sub onresize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        Me.myBitmap = New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height)
        Me.myAlphaBitmap = New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height)
        myUpToDate = False
        Me.Invalidate()
    End Sub

    Protected Overrides Sub onkeydown(ByVal e As KeyEventArgs)
        MyBase.OnKeyDown(e)
        myUpToDate = False
        Me.Invalidate()
    End Sub

    Protected Overrides Sub onkeyup(ByVal e As KeyEventArgs)
        MyBase.OnKeyUp(e)
        myUpToDate = False
        Me.Invalidate()
    End Sub

    Protected Overrides Sub onkeypress(ByVal e As KeyPressEventArgs)
        MyBase.OnKeyPress(e)
        myUpToDate = False
        Me.Invalidate()
    End Sub

    Protected Overrides Sub onmouseup(ByVal e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnGiveFeedback(ByVal gfbevent As GiveFeedbackEventArgs)
        MyBase.OnGiveFeedback(gfbevent)
        myUpToDate = False
        Me.Invalidate()
    End Sub

    Protected Overrides Sub onmouseleave(ByVal e As EventArgs)
        Dim ptCursor As Point = Cursor.Position
        Dim f As Form = Me.FindForm()
        ptCursor = f.PointToClient(ptCursor)
        If Not Me.Bounds.Contains(ptCursor) Then MyBase.OnMouseLeave(e)
    End Sub

    Protected Overrides Sub onchangeUICues(ByVal e As UICuesEventArgs)
        MyBase.OnChangeUICues(e)
        myUpToDate = False
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnGotFocus(ByVal e As EventArgs)
        MyBase.OnGotFocus(e)
        myCaretUpToDate = False
        myUpToDate = False
        Me.Invalidate()
        myTimer1 = New System.Windows.Forms.Timer(Me.components)
        myTimer1.Interval = CInt(win32.GetCaretBlinkTime())
        AddHandler myTimer1.Tick, AddressOf myTimer1_Tick
        myTimer1.Enabled = True
    End Sub

    Protected Overrides Sub OnLostFocus(ByVal e As EventArgs)
        MyBase.OnLostFocus(e)
        myCaretUpToDate = False
        myUpToDate = False
        Me.Invalidate()
        myTimer1.Dispose()
    End Sub

    Protected Overrides Sub OnFontChanged(ByVal e As EventArgs)
        If Me.myPaintedFirstTime Then Me.SetStyle(ControlStyles.UserPaint, False)
        MyBase.OnFontChanged(e)
        If Me.myPaintedFirstTime Then Me.SetStyle(ControlStyles.UserPaint, True)
        myFontHeight = GetFontHeight()
        myUpToDate = False
        Me.Invalidate()
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        myUpToDate = False
        Me.Invalidate()
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        MyBase.WndProc(m)
        If m.Msg = win32.WM_PAINT Then
            myPaintedFirstTime = True
            If Not myUpToDate OrElse Not myCaretUpToDate Then GetBitmaps()
            myUpToDate = True
            myCaretUpToDate = True
            If myPictureBox.Image IsNot Nothing Then myPictureBox.Image.Dispose()
            myPictureBox.Image = CType(myAlphaBitmap.Clone(), Image)
        ElseIf m.Msg = win32.WM_HSCROLL OrElse m.Msg = win32.WM_VSCROLL Then
            myUpToDate = False
            Me.Invalidate()
        ElseIf m.Msg = win32.WM_LBUTTONDOWN OrElse m.Msg = win32.WM_RBUTTONDOWN OrElse m.Msg = win32.WM_LBUTTONDBLCLK Then
            myUpToDate = False
            Me.Invalidate()
        ElseIf m.Msg = win32.WM_MOUSEMOVE Then
            If m.WParam.ToInt32() <> 0 Then
                myUpToDate = False
                Me.Invalidate()
            End If
        End If
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
        End If

        MyBase.Dispose(disposing)
    End Sub

    <Category("ToolsBox Herramienta")> _
    Public Shadows Property BorderStyle As BorderStyle
        Get
            Return MyBase.BorderStyle
        End Get

        Set(ByVal value As BorderStyle)
            If Me.myPaintedFirstTime Then Me.SetStyle(ControlStyles.UserPaint, False)
            MyBase.BorderStyle = value
            If Me.myPaintedFirstTime Then Me.SetStyle(ControlStyles.UserPaint, True)
            Me.myBitmap = Nothing
            Me.myAlphaBitmap = Nothing
            myUpToDate = False
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Shadows Property BackColor As Color
        Get
            Return Color.FromArgb(MyBase.BackColor.R, MyBase.BackColor.G, MyBase.BackColor.B)
        End Get

        Set(ByVal value As Color)
            myBackColor = value
            MyBase.BackColor = value
            myUpToDate = False
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Overrides Property Multiline As Boolean
        Get
            Return MyBase.Multiline
        End Get

        Set(ByVal value As Boolean)
            If Me.myPaintedFirstTime Then Me.SetStyle(ControlStyles.UserPaint, False)
            MyBase.Multiline = value
            If Me.myPaintedFirstTime Then Me.SetStyle(ControlStyles.UserPaint, True)
            Me.myBitmap = Nothing
            Me.myAlphaBitmap = Nothing
            myUpToDate = False
            Me.Invalidate()
        End Set
    End Property

    Private Function GetFontHeight() As Integer
        Dim g As Graphics = Me.CreateGraphics()
        Dim sf_font As SizeF = g.MeasureString("X", Me.Font)
        g.Dispose()
        Return CInt(sf_font.Height)
    End Function

    Private Sub GetBitmaps()
        If myBitmap Is Nothing OrElse myAlphaBitmap Is Nothing OrElse myBitmap.Width <> Width OrElse myBitmap.Height <> Height OrElse myAlphaBitmap.Width <> Width OrElse myAlphaBitmap.Height <> Height Then
            myBitmap = Nothing
            myAlphaBitmap = Nothing
        End If

        If myBitmap Is Nothing Then
            myBitmap = New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height)
            myUpToDate = False
        End If

        If Not myUpToDate Then
            Me.SetStyle(ControlStyles.UserPaint, False)
            win32.CaptureWindow(Me, myBitmap)
            Me.SetStyle(ControlStyles.UserPaint, True)
            Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
            Me.BackColor = Color.FromArgb(_Opacity, myBackColor)
        End If

        Dim r2 As Rectangle = New Rectangle(0, 0, Me.ClientRectangle.Width, Me.ClientRectangle.Height)
        Dim tempImageAttr As ImageAttributes = New ImageAttributes()
        Dim tempColorMap As ColorMap() = New ColorMap(0) {}
        tempColorMap(0) = New ColorMap()
        tempColorMap(0).OldColor = Color.FromArgb(255, myBackColor)
        tempColorMap(0).NewColor = Color.FromArgb(_Opacity, myBackColor)
        tempImageAttr.SetRemapTable(tempColorMap)
        If myAlphaBitmap IsNot Nothing Then myAlphaBitmap.Dispose()
        myAlphaBitmap = New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height)
        Dim tempGraphics1 As Graphics = Graphics.FromImage(myAlphaBitmap)
        tempGraphics1.DrawImage(myBitmap, r2, 0, 0, Me.ClientRectangle.Width, Me.ClientRectangle.Height, GraphicsUnit.Pixel, tempImageAttr)
        tempGraphics1.Dispose()
        If Me.Focused AndAlso (Me.SelectionLength = 0) Then
            Dim tempGraphics2 As Graphics = Graphics.FromImage(myAlphaBitmap)
            If myCaretState Then
                Dim caret As Point = Me.findCaret()
                Dim p As Pen = New Pen(Me.ForeColor, 3)
                tempGraphics2.DrawLine(p, caret.X, caret.Y + 0, caret.X, caret.Y + myFontHeight)
                tempGraphics2.Dispose()
            End If
        End If
    End Sub

    Private Function findCaret() As Point
        Dim pointCaret As Point = New Point(0)
        Dim i_char_loc As Integer = Me.SelectionStart
        Dim pi_char_loc As IntPtr = New IntPtr(i_char_loc)
        Dim i_point As Integer = win32.SendMessage(Me.Handle, win32.EM_POSFROMCHAR, pi_char_loc, IntPtr.Zero)
        pointCaret = New Point(i_point)
        If i_char_loc = 0 Then
            pointCaret = New Point(0)
        ElseIf i_char_loc >= Me.Text.Length Then
            pi_char_loc = New IntPtr(i_char_loc - 1)
            i_point = win32.SendMessage(Me.Handle, win32.EM_POSFROMCHAR, pi_char_loc, IntPtr.Zero)
            pointCaret = New Point(i_point)
            Dim g As Graphics = Me.CreateGraphics()
            Dim t1 As String = Me.Text.Substring(Me.Text.Length - 1, 1) & "X"
            Dim sizet1 As SizeF = g.MeasureString(t1, Me.Font)
            Dim sizex As SizeF = g.MeasureString("X", Me.Font)
            g.Dispose()
            Dim xoffset As Integer = CInt((sizet1.Width - sizex.Width))
            pointCaret.X = pointCaret.X + xoffset
            If i_char_loc = Me.Text.Length Then
                Dim slast As String = Me.Text.Substring(Text.Length - 1, 1)
                If slast = vbLf Then
                    pointCaret.X = 1
                    pointCaret.Y = pointCaret.Y + myFontHeight
                End If
            End If
        End If

        Return pointCaret
    End Function

    Private Sub myTimer1_Tick(ByVal sender As Object, ByVal e As EventArgs)
        myCaretState = Not myCaretState
        myCaretUpToDate = False
        Me.Invalidate()
    End Sub

    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

    <Browsable(True), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    <Category("ToolsBox Herramienta")> _
    Public Property Opacity As Integer
        Get
            Return _Opacity
        End Get
        Set(value As Integer)
            Dim v As Integer = value
            If v > 255 Then v = 255
            _Opacity = v
            myUpToDate = False
            Me.Invalidate()
        End Set
    End Property


    Private Class uPictureBox
        Inherits PictureBox

        Public Sub New()
            Me.SetStyle(ControlStyles.Selectable, False)
            Me.SetStyle(ControlStyles.UserPaint, True)
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
            Me.SetStyle(ControlStyles.DoubleBuffer, True)
            Me.Cursor = Nothing
            Me.Enabled = True
            Me.SizeMode = PictureBoxSizeMode.Normal
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            If m.Msg = win32.WM_LBUTTONDOWN OrElse m.Msg = win32.WM_RBUTTONDOWN OrElse m.Msg = win32.WM_LBUTTONDBLCLK OrElse m.Msg = win32.WM_MOUSELEAVE OrElse m.Msg = win32.WM_MOUSEMOVE Then
                win32.PostMessage(Me.Parent.Handle, CUInt(m.Msg), m.WParam, m.LParam)
            ElseIf m.Msg = win32.WM_LBUTTONUP Then
                Me.Parent.Invalidate()
            End If

            MyBase.WndProc(m)
        End Sub
    End Class

End Class

Public Class win32
    Public Const WM_MOUSEMOVE As Integer = 512
    Public Const WM_LBUTTONDOWN As Integer = 513
    Public Const WM_LBUTTONUP As Integer = 514
    Public Const WM_RBUTTONDOWN As Integer = 516
    Public Const WM_LBUTTONDBLCLK As Integer = 515
    Public Const WM_MOUSELEAVE As Integer = 675
    Public Const WM_PAINT As Integer = 15
    Public Const WM_ERASEBKGND As Integer = 20
    Public Const WM_PRINT As Integer = 791
    Public Const WM_HSCROLL As Integer = 276
    Public Const WM_VSCROLL As Integer = 277
    Public Const EM_GETSEL As Integer = 176
    Public Const EM_LINEINDEX As Integer = 187
    Public Const EM_LINEFROMCHAR As Integer = 201
    Public Const EM_POSFROMCHAR As Integer = 214

    <DllImport("USER32.DLL", EntryPoint:="PostMessage")>
    Public Shared Function PostMessage(ByVal hwnd As IntPtr, ByVal msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Boolean
    End Function

    <DllImport("USER32.DLL", EntryPoint:="SendMessage")>
    Public Shared Function SendMessage(ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Integer
    End Function

    <DllImport("USER32.DLL", EntryPoint:="GetCaretBlinkTime")>
    Public Shared Function GetCaretBlinkTime() As UInteger
    End Function

    Const WM_PRINTCLIENT As Integer = 792
    Const PRF_CHECKVISIBLE As Long = 1L
    Const PRF_NONCLIENT As Long = 2L
    Const PRF_CLIENT As Long = 4L
    Const PRF_ERASEBKGND As Long = 8L
    Const PRF_CHILDREN As Long = 16L
    Const PRF_OWNED As Long = 32L

    <Category("ToolsBox Herramienta")> _
    Public Shared Function CaptureWindow(ByVal control As System.Windows.Forms.Control, ByRef bitmap As System.Drawing.Bitmap) As Boolean
        Dim g2 As Graphics = Graphics.FromImage(bitmap)
        Dim meint As Integer = CInt((PRF_CLIENT Or PRF_ERASEBKGND))
        Dim meptr As System.IntPtr = New System.IntPtr(meint)
        Dim hdc As System.IntPtr = g2.GetHdc()
        win32.SendMessage(control.Handle, win32.WM_PRINT, hdc, meptr)
        g2.ReleaseHdc(hdc)
        g2.Dispose()
        Return True
    End Function
End Class