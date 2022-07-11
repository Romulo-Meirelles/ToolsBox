Imports System.Drawing.Drawing2D
Imports System.ComponentModel

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.CheckBox))> _
Public Class Switch_Deimos : Inherits Control
#Region " Control Help - MouseState & Flicker Control"
    Public Property ToggleState As Boolean = False
    Private ColLow, ColMed, ColHigh, ColDark As Color
    Public Property DrawRigid As Boolean = False
    Public Property DrawOnOffText As Boolean = True
    Protected Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
    End Sub
    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        ToggleState = Not ToggleState
        Invalidate()
    End Sub
    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        Invalidate()
    End Sub
    Public Function IsChecked() As Boolean
        Return ToggleState
    End Function
    Public Sub SizeChangeExternal(e As Size)
        Size = e
        Invalidate()
    End Sub
#End Region
    Sub New()
        MyBase.New()
        Text = ""
        Size = New Size(New Point(66, 26))
        BackColor = Color.FromArgb(57, 59, 64)
        ColHigh = Color.FromArgb(96, 98, 103)
        ColMed = Color.FromArgb(57, 59, 64)
        ColLow = Color.FromArgb(35, 37, 40)
        ColDark = Color.FromArgb(22, 22, 22)
        Font = New Font("Segoe UI", 10.0F)
        DoubleBuffered = True
    End Sub
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        'Size = New Size(66, 26)

        Dim G As Graphics = e.Graphics
        Dim D As New DrawUtils
        MyBase.OnPaint(e)
        G.Clear(BackColor)
        If Not DrawRigid Then
            G.SmoothingMode = SmoothingMode.HighQuality
        End If
        Dim ContRect As GraphicsPath = D.RoundRect(New Rectangle(0, 0, Width - 1, Height - 2), 5)
        Dim ContRectHighlight As GraphicsPath = D.RoundRect(New Rectangle(0, 1, Width - 1, Height - 2), 5)

        If ToggleState Then
            G.FillPath(New SolidBrush(Color.FromArgb(45, 47, 55)), ContRect)
        Else
            G.FillPath(New SolidBrush(ColLow), ContRect)
        End If
        G.DrawPath(New Pen(ColHigh), ContRectHighlight)
        G.DrawPath(New Pen(ColDark), ContRect)

        Dim StateRect As New GraphicsPath
        Dim StateRectHighlight As New GraphicsPath
        Dim StateGrad As New LinearGradientBrush(New Point(0, 0), New Point(0, Height), ColMed, ColLow)
        If Not ToggleState Then
            StateRect = D.RoundRect(New Rectangle(0, 0, (Width / 2) - 1, Height - 2), 6)
            StateRectHighlight = D.RoundRect(New Rectangle(0, 1, (Width / 2) - 1, Height - 4), 6)
        Else
            StateRect = D.RoundRect(New Rectangle(Width / 2, 0, (Width / 2) - 1, Height - 2), 6)
            StateRectHighlight = D.RoundRect(New Rectangle(Width / 2, 1, (Width / 2) - 1, Height - 4), 6)
        End If

        G.FillPath(StateGrad, StateRect)
        G.DrawPath(New Pen(New LinearGradientBrush(New Point(0, 0), New Point(0, Height), ColHigh, ColDark)), StateRectHighlight)
        G.DrawPath(New Pen(ColDark), StateRect)

        If DrawOnOffText Then
            If ToggleState Then
                D.DrawTextWithShadow(G, New Rectangle(6, 0, Width - 1, Height - 2), "On", Font, HorizontalAlignment.Left, ColHigh)
            Else
                D.DrawTextWithShadow(G, New Rectangle(0, 0, Width - 1, Height - 2), "Off", Font, HorizontalAlignment.Right, ColHigh)
            End If
        End If
    End Sub
End Class

Class DrawUtils
    Public Sub DrawTextWithShadow(ByVal G As Graphics, ByVal ContRect As Rectangle, ByVal Text As String, ByVal TFont As Font, ByVal TAlign As HorizontalAlignment, ByVal TColor As Color)
        DrawText(G, New Rectangle(ContRect.X, ContRect.Y + 2, ContRect.Width + 1, ContRect.Height + 2), Text, TFont, TAlign, Color.Black)
        DrawText(G, ContRect, Text, TFont, TAlign, TColor)
    End Sub
    Public Sub DrawText(ByVal G As Graphics, ByVal ContRect As Rectangle, ByVal Text As String, ByVal TFont As Font, ByVal TAlign As HorizontalAlignment, ByVal TColor As Color)
        If String.IsNullOrEmpty(Text) Then Return
        Dim TextSize As Size = G.MeasureString(Text, TFont).ToSize
        Dim _Brush As SolidBrush = New SolidBrush(TColor)

        Select Case TAlign
            Case HorizontalAlignment.Left
                G.DrawString(Text, TFont, _Brush, ContRect.X, ContRect.Height \ 2 - TextSize.Height \ 2)
            Case HorizontalAlignment.Right
                G.DrawString(Text, TFont, _Brush, ContRect.Width - TextSize.Width - 5, ContRect.Height \ 2 - TextSize.Height \ 2)
            Case HorizontalAlignment.Center
                G.DrawString(Text, TFont, _Brush, ContRect.Width \ 2 - TextSize.Width \ 2 + 3, ContRect.Height \ 2 - TextSize.Height \ 2)
        End Select
    End Sub
    Public Function RoundRect(ByVal Rectangle As Rectangle, ByVal Curve As Integer) As GraphicsPath
        Dim P As GraphicsPath = New GraphicsPath()
        Dim ArcRectangleWidth As Integer = Curve * 2
        P.AddArc(New Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90)
        P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90)
        P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90)
        P.AddArc(New Rectangle(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90)
        P.AddLine(New Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), New Point(Rectangle.X, Curve + Rectangle.Y))
        Return P
    End Function
    Public Function RoundedTopRect(ByVal Rectangle As Rectangle, ByVal Curve As Integer) As GraphicsPath
        Dim P As GraphicsPath = New GraphicsPath()
        Dim ArcRectangleWidth As Integer = Curve * 2
        P.AddArc(New Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90)
        P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90)
        P.AddLine(New Point(Rectangle.X + Rectangle.Width, Rectangle.Y + ArcRectangleWidth), New Point(Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height))
        P.AddLine(New Point(Rectangle.X, Rectangle.Height + Rectangle.Y), New Point(Rectangle.X, Rectangle.Y + Curve))
        Return P
    End Function
End Class