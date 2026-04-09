Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Drawing
Imports System.ComponentModel
Imports SuperForms.ThemeContainer154

Public MustInherit Class Theme
    Inherits ContainerControl

#Region " Initialization "

    Protected G As Graphics
    Sub New()
        SetStyle(DirectCast(139270, ControlStyles), True)
    End Sub

    Private ParentIsForm As Boolean
    Protected Overrides Sub OnHandleDestroyed(ByVal e As EventArgs)
        Try
            Dock = DockStyle.None
            ParentForm.BackColor = Color.FromArgb(240, 240, 240)
            ParentForm.FormBorderStyle = FormBorderStyle.Sizable
            MyBase.OnHandleDestroyed(e)
        Catch ex As Exception
        End Try
    End Sub

    Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        Dock = DockStyle.Fill
        ParentIsForm = TypeOf Parent Is Form
        If ParentIsForm Then
            If Not _TransparencyKey = Color.Empty Then ParentForm.TransparencyKey = _TransparencyKey
            ParentForm.FormBorderStyle = FormBorderStyle.None
        End If
        MyBase.OnHandleCreated(e)
    End Sub

    Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal v As String)
            MyBase.Text = v
            Invalidate()
        End Set
    End Property

#End Region

#Region " Sizing and Movement "

    <Category("Super Forms")>
    Private _Resizable As Boolean = True
    Property Resizable() As Boolean
        Get
            Return _Resizable
        End Get
        Set(ByVal value As Boolean)
            _Resizable = value
        End Set
    End Property

    <Category("Super Forms")>
    Private _MoveHeight As Integer = 24
    Property MoveHeight() As Integer
        Get
            Return _MoveHeight
        End Get
        Set(ByVal v As Integer)
            _MoveHeight = v
            Header = New Rectangle(7, 7, Width - 14, _MoveHeight - 7)
        End Set
    End Property

    Private Flag As IntPtr
    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        If Not e.Button = MouseButtons.Left Then Return
        If ParentIsForm Then If ParentForm.WindowState = FormWindowState.Maximized Then Return

        If Header.Contains(e.Location) Then
            Flag = New IntPtr(2)
        ElseIf Current.Position = 0 Or Not _Resizable Then
            Return
        Else
            Flag = New IntPtr(Current.Position)
        End If

        Capture = False
        DefWndProc(Message.Create(Parent.Handle, 161, Flag, Nothing))

        MyBase.OnMouseDown(e)
    End Sub

    Private Structure Pointer
        ReadOnly Cursor As Cursor, Position As Byte
        Sub New(ByVal c As Cursor, ByVal p As Byte)
            Cursor = c
            Position = p
        End Sub
    End Structure

    Private F1, F2, F3, F4 As Boolean, PTC As Point
    Private Function GetPointer() As Pointer
        PTC = PointToClient(MousePosition)
        F1 = PTC.X < 7
        F2 = PTC.X > Width - 7
        F3 = PTC.Y < 7
        F4 = PTC.Y > Height - 7

        If F1 And F3 Then Return New Pointer(Cursors.SizeNWSE, 13)
        If F1 And F4 Then Return New Pointer(Cursors.SizeNESW, 16)
        If F2 And F3 Then Return New Pointer(Cursors.SizeNESW, 14)
        If F2 And F4 Then Return New Pointer(Cursors.SizeNWSE, 17)
        If F1 Then Return New Pointer(Cursors.SizeWE, 10)
        If F2 Then Return New Pointer(Cursors.SizeWE, 11)
        If F3 Then Return New Pointer(Cursors.SizeNS, 12)
        If F4 Then Return New Pointer(Cursors.SizeNS, 15)
        Return New Pointer(Cursors.Default, 0)
    End Function

    Private Current, Pending As Pointer
    Private Sub SetCurrent()
        Pending = GetPointer()
        If Current.Position = Pending.Position Then Return
        Current = GetPointer()
        Cursor = Current.Cursor
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        If _Resizable Then SetCurrent()
        MyBase.OnMouseMove(e)
    End Sub

    Protected Header As Rectangle
    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        If Width = 0 OrElse Height = 0 Then Return
        Header = New Rectangle(7, 7, Width - 14, _MoveHeight - 7)
        Invalidate()
        MyBase.OnSizeChanged(e)
    End Sub

#End Region

#Region " Convienence "

    MustOverride Sub PaintHook()
    Protected NotOverridable Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If Width = 0 OrElse Height = 0 Then Return
        G = e.Graphics
        PaintHook()
    End Sub

    <Category("Super Forms")>
    Private _TransparencyKey As Color
    Property TransparencyKey() As Color
        Get
            Return _TransparencyKey
        End Get
        Set(ByVal v As Color)
            _TransparencyKey = v
            Invalidate()
        End Set
    End Property

    <Category("Super Forms")>
    Private _Image As Image
    Property Image() As Image
        Get
            Return _Image
        End Get
        Set(ByVal value As Image)
            _Image = value
            Invalidate()
        End Set
    End Property

    <Category("Super Forms")>
    ReadOnly Property ImageWidth() As Integer
        Get
            If _Image Is Nothing Then Return 0
            Return _Image.Width
        End Get
    End Property

    Private _Size As Size
    Private _Rectangle As Rectangle
    Private _Gradient As LinearGradientBrush
    Private _Brush As SolidBrush

    Protected Sub DrawCorners(ByVal c As Color, ByVal rect As Rectangle)
        _Brush = New SolidBrush(c)
        G.FillRectangle(_Brush, rect.X, rect.Y, 1, 1)
        G.FillRectangle(_Brush, rect.X + (rect.Width - 1), rect.Y, 1, 1)
        G.FillRectangle(_Brush, rect.X, rect.Y + (rect.Height - 1), 1, 1)
        G.FillRectangle(_Brush, rect.X + (rect.Width - 1), rect.Y + (rect.Height - 1), 1, 1)
    End Sub

    Protected Sub DrawBorders(ByVal p1 As Pen, ByVal p2 As Pen, ByVal rect As Rectangle)
        G.DrawRectangle(p1, rect.X, rect.Y, rect.Width - 1, rect.Height - 1)
        G.DrawRectangle(p2, rect.X + 1, rect.Y + 1, rect.Width - 3, rect.Height - 3)
    End Sub

    Protected Sub DrawText(ByVal a As HorizontalAlignment, ByVal c As Color, ByVal x As Integer)
        DrawText(a, c, x, 0)
    End Sub
    Protected Sub DrawText(ByVal a As HorizontalAlignment, ByVal c As Color, ByVal x As Integer, ByVal y As Integer)
        If String.IsNullOrEmpty(Text) Then Return
        _Size = G.MeasureString(Text, Font).ToSize
        _Brush = New SolidBrush(c)

        Select Case a
            Case HorizontalAlignment.Left
                G.DrawString(Text, Font, _Brush, x, _MoveHeight \ 2 - _Size.Height \ 2 + y)
            Case HorizontalAlignment.Right
                G.DrawString(Text, Font, _Brush, Width - _Size.Width - x, _MoveHeight \ 2 - _Size.Height \ 2 + y)
            Case HorizontalAlignment.Center
                G.DrawString(Text, Font, _Brush, Width \ 2 - _Size.Width \ 2 + x, _MoveHeight \ 2 - _Size.Height \ 2 + y)
        End Select
    End Sub
    Protected Sub DrawText(ByVal a As HorizontalAlignment, ByVal c As Color, ByVal x As Integer, ByVal y As Integer, ByVal f As Font)
        If String.IsNullOrEmpty(Text) Then Return
        _Size = G.MeasureString(Text, Font).ToSize
        _Brush = New SolidBrush(c)

        Select Case a
            Case HorizontalAlignment.Left
                G.DrawString(Text, f, _Brush, x, _MoveHeight \ 2 - _Size.Height \ 2 + y)
            Case HorizontalAlignment.Right
                G.DrawString(Text, f, _Brush, Width - _Size.Width - x, _MoveHeight \ 2 - _Size.Height \ 2 + y)
            Case HorizontalAlignment.Center
                G.DrawString(Text, f, _Brush, Width \ 2 - _Size.Width \ 2 + x, _MoveHeight \ 2 - _Size.Height \ 2 + y)
        End Select
    End Sub

    Protected Sub DrawIcon(ByVal a As HorizontalAlignment, ByVal x As Integer)
        DrawIcon(a, x, 0)
    End Sub
    Protected Sub DrawIcon(ByVal a As HorizontalAlignment, ByVal x As Integer, ByVal y As Integer)
        If _Image Is Nothing Then Return
        Select Case a
            Case HorizontalAlignment.Left
                G.DrawImage(_Image, x, _MoveHeight \ 2 - _Image.Height \ 2 + y)
            Case HorizontalAlignment.Right
                G.DrawImage(_Image, Width - _Image.Width - x, _MoveHeight \ 2 - _Image.Height \ 2 + y)
            Case HorizontalAlignment.Center
                G.DrawImage(_Image, Width \ 2 - _Image.Width \ 2, _MoveHeight \ 2 - _Image.Height \ 2)
        End Select
    End Sub

    Protected Sub DrawGradient(ByVal c1 As Color, ByVal c2 As Color, ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal angle As Single)
        _Rectangle = New Rectangle(x, y, width, height)
        _Gradient = New LinearGradientBrush(_Rectangle, c1, c2, angle)
        G.FillRectangle(_Gradient, _Rectangle)
    End Sub

#End Region

End Class


<ToolboxBitmap(GetType(FormAdobe), "Purple.ico")>
Public Class FormAdobe
    Inherits Theme
    Enum TextAlign As Integer
        Left = 0
        Center = 1
        Right = 2
    End Enum

    Private TA As TextAlign

    <Category("Super Forms")>
    Public Property TextAlignment() As TextAlign
        Get
            Return TA
        End Get
        Set(ByVal value As TextAlign)
            TA = value
            Invalidate()
        End Set
    End Property

    Private _Icon As Icon = My.Resources.ToolsBox

    <Category("Super Forms")>
    Public Property Icon() As Icon
        Get
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

    Private _StateMinimize As Boolean = False
    <Category("Super Forms")>
    Public Property StateMinimize() As Boolean
        Get
            Return _StateMinimize
        End Get
        Set(ByVal v As Boolean)
            _StateMinimize = v
            Invalidate()
        End Set
    End Property

    Private _StateClose As Boolean = False
    <Category("Super Forms")>
    Public Property StateClose() As Boolean
        Get
            Return _StateClose
        End Get
        Set(ByVal v As Boolean)
            _StateClose = v
            Invalidate()
        End Set
    End Property

    Private _StateMaximize As Boolean = False
    <Category("Super Forms")>
    Public Property StateMaximize() As Boolean
        Get
            Return _StateMaximize
        End Get
        Set(ByVal v As Boolean)
            _StateMaximize = v
            Invalidate()
        End Set
    End Property

    Sub New()
        MoveHeight = 19
        TransparencyKey = Color.Fuchsia
        Me.Resizable = False
        BackColor = Color.FromArgb(51, 51, 51)
        Me.Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
        Me.TextAlignment = Left()

    End Sub

    Public Overrides Sub PaintHook()

        G.Clear(Color.FromArgb(68, 68, 68))
        DrawGradient(Color.FromArgb(51, 51, 51), Color.FromArgb(51, 51, 51), 0, 0, Width, 37, 45S)
        G.DrawLine(New Pen(New SolidBrush(Color.FromArgb(31, 31, 31))), 0, 37, Width, 37)
        G.DrawLine(New Pen(New SolidBrush(Color.FromArgb(60, 60, 60))), 0, 38, Width, 38)
        G.FillRectangle(New SolidBrush(Color.FromArgb(68, 68, 68)), 1, 39, Width - 2, Height - 39 - 2)

        Select Case TA
            Case 0
                DrawText(HorizontalAlignment.Left, Color.FromArgb(120, Color.Black), 36, 13, New Font("Microsoft Sans Serif", 10, FontStyle.Bold))
                DrawText(HorizontalAlignment.Left, Color.White, 40, 11, New Font("Microsoft Sans Serif", 10, FontStyle.Bold))
            Case 1
                DrawText(HorizontalAlignment.Center, Color.FromArgb(120, Color.Black), -1, 13, New Font("Microsoft Sans Serif", 10, FontStyle.Bold))
                DrawText(HorizontalAlignment.Center, Color.White, 1, 11, New Font("Microsoft Sans Serif", 10, FontStyle.Bold))
            Case 2
                DrawText(HorizontalAlignment.Right, Color.FromArgb(120, Color.Black), -1 + CInt(G.MeasureString(Text, Font).Width / 6), 13, New Font("Microsoft Sans Serif", 10, FontStyle.Bold))
                DrawText(HorizontalAlignment.Right, Color.White, 1 + CInt(G.MeasureString(Text, Font).Width / 6), 11, New Font("Microsoft Sans Serif", 10, FontStyle.Bold))
        End Select

        G.FillRectangle(New SolidBrush(Color.FromArgb(51, 51, 51)), 1, Height - 37, Width - 2, Height - 2)
        G.DrawLine(New Pen(New SolidBrush(Color.FromArgb(31, 31, 31))), 0, Height - 37, Width, Height - 37)
        G.DrawLine(New Pen(New SolidBrush(Color.FromArgb(60, 60, 60))), 0, Height - 38, Width, Height - 38)

        DrawBorders(Pens.Black, Pens.Gray, ClientRectangle)
        DrawCorners(Color.Fuchsia, ClientRectangle)
        If _ShowIcon = True Then G.DrawIcon(_Icon, New Rectangle(10, 13, 16, 16))


    End Sub

End Class
