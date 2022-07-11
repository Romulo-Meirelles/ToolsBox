Imports System.Drawing, System.Drawing.Drawing2D
Imports System.ComponentModel, System.Windows.Forms
Imports ToolsBox.Controller

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.CheckBox))> _
<DefaultEvent("CheckedChanged")> _
Public Class Switch_SLC
    Inherits Control

    Event CheckedChanged(ByVal sender As Object)

    Friend State As MouseState


    Sub New()
        SetStyle(DirectCast(139286, ControlStyles), True)
        SetStyle(ControlStyles.Selectable, False)

        P1 = New Pen(Color.FromArgb(55, 55, 55))
        P2 = New Pen(Color.FromArgb(35, 35, 35))
        P3 = New Pen(Color.FromArgb(65, 65, 65))

        B1 = New SolidBrush(Color.FromArgb(35, 35, 35))
        B2 = New SolidBrush(Color.FromArgb(85, 85, 85))
        B3 = New SolidBrush(Color.FromArgb(65, 65, 65))
        B4 = New SolidBrush(Color.FromArgb(205, 150, 0))
        B5 = New SolidBrush(Color.FromArgb(40, 40, 40))

        SF1 = New StringFormat()
        SF1.LineAlignment = StringAlignment.Center
        SF1.Alignment = StringAlignment.Near

        SF2 = New StringFormat()
        SF2.LineAlignment = StringAlignment.Center
        SF2.Alignment = StringAlignment.Far

        Size = New Size(56, 24)
        MinimumSize = Size
        MaximumSize = Size
    End Sub

    Private _Checked As Boolean
    <Category("ToolsBox Herramienta")> _
    Public Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            _Checked = value
            RaiseEvent CheckedChanged(Me)

            Invalidate()
        End Set
    End Property

    Private GP1, GP2, GP3, GP4 As GraphicsPath

    Private P1, P2, P3 As Pen
    Private B1, B2, B3, B4, B5 As SolidBrush

    Private PB1 As PathGradientBrush
    Private GB1 As LinearGradientBrush

    Private R1, R2, R3 As Rectangle
    Private SF1, SF2 As StringFormat

    Private Offset As Integer

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)

        Dim G As Graphics = e.Graphics

        G.Clear(Color.White)
        G.SmoothingMode = SmoothingMode.AntiAlias

        GP1 = RoundRec(New Rectangle(0, 0, Width - 1, Height - 1), 7)
        GP2 = RoundRec(New Rectangle(1, 1, Width - 3, Height - 3), 7)

        PB1 = New PathGradientBrush(GP1)
        PB1.CenterColor = Color.FromArgb(250, 250, 250)
        PB1.SurroundColors = {Color.FromArgb(245, 245, 245)}
        PB1.FocusScales = New PointF(0.3F, 0.3F)

        G.FillPath(PB1, GP1)
        G.DrawPath(Pens.LightGray, GP1)
        G.DrawPath(Pens.White, GP2)

        R1 = New Rectangle(5, 0, Width - 10, Height + 2)
        R2 = New Rectangle(6, 1, Width - 10, Height + 2)

        R3 = New Rectangle(1, 1, (Width \ 2) - 1, Height - 3)



        If _Checked Then
            ' G.DrawString("On", Font, Brushes.Black, R2, SF1)
            G.DrawString("On", Font, New SolidBrush(Color.FromArgb(1, 75, 124)), R1, SF1)

            R3.X += (Width \ 2) - 1
        Else
            'G.DrawString("Off", Font, B1, R2, SF2)
            G.DrawString("Off", Font, New SolidBrush(Color.FromArgb(1, 75, 124)), R1, SF2)
        End If

        GP3 = RoundRec(R3, 7)
        GP4 = RoundRec(New Rectangle(R3.X + 1, R3.Y + 1, R3.Width - 2, R3.Height - 2), 7)

        GB1 = New LinearGradientBrush(ClientRectangle, Color.FromArgb(255, 255, 255), Color.FromArgb(245, 245, 245), 90.0F)

        G.FillPath(GB1, GP3)
        G.DrawPath(Pens.LightGray, GP3)
        G.DrawPath(Pens.White, GP4)


        Offset = R3.X + (R3.Width \ 2) - 3

        For I As Integer = 0 To 1
            If _Checked Then
                'G.FillRectangle(Brushes.LightGray, Offset + (I * 5), 7, 2, Height - 14)
            Else
                ' G.FillRectangle(Brushes.LightGray, Offset + (I * 5), 7, 2, Height - 14)
            End If

            G.SmoothingMode = SmoothingMode.None

            If _Checked Then

                G.SmoothingMode = SmoothingMode.HighQuality

                Dim GPF As New GraphicsPath
                GPF.AddEllipse(New Rectangle(Width - 20, Height - 17, 10, 10))
                Dim PB3 As PathGradientBrush
                PB3 = New PathGradientBrush(GPF)
                PB3.CenterPoint = New Point(Height - 18.5, Height - 20)
                PB3.CenterColor = Color.FromArgb(53, 152, 74)
                PB3.SurroundColors = {Color.FromArgb(86, 216, 114)}
                PB3.FocusScales = New PointF(0.9F, 0.9F)


                G.FillPath(PB3, GPF)

                G.DrawPath(New Pen(Color.FromArgb(85, 200, 109)), GPF)
                G.SetClip(GPF)
                G.FillEllipse(New SolidBrush(Color.FromArgb(40, Color.WhiteSmoke)), New Rectangle(Width - 20, Height - 18, 6, 6))
                G.ResetClip()


                '  G.FillRectangle(New SolidBrush(Color.FromArgb(85, 158, 203)), Offset + (I * 5), 7, 2, Height - 14)
            Else
                G.SmoothingMode = SmoothingMode.HighQuality

                Dim GPF As New GraphicsPath
                GPF.AddEllipse(New Rectangle(Height - 15, Height - 17, 10, 10))
                Dim PB3 As PathGradientBrush
                PB3 = New PathGradientBrush(GPF)
                PB3.CenterPoint = New Point(Height - 18.5, Height - 20)
                PB3.CenterColor = Color.FromArgb(185, 65, 65)
                PB3.SurroundColors = {Color.Red}
                PB3.FocusScales = New PointF(0.9F, 0.9F)


                G.FillPath(PB3, GPF)

                G.DrawPath(New Pen(Color.FromArgb(152, 53, 53)), GPF)
                G.SetClip(GPF)
                G.FillEllipse(New SolidBrush(Color.FromArgb(40, Color.WhiteSmoke)), New Rectangle(Height - 16, Height - 18, 6, 6))
                G.ResetClip()



                '  G.FillRectangle(Brushes.LightGray, Offset + (I * 5), 7, 2, Height - 14)
            End If

            G.SmoothingMode = SmoothingMode.AntiAlias
        Next
    End Sub

    Public Function RoundRec(ByVal Rectangle As Rectangle, ByVal Curve As Integer) As GraphicsPath
        Dim P As GraphicsPath = New GraphicsPath()
        Dim ArcRectangleWidth As Integer = Curve * 2
        P.AddArc(New Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90)
        P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90)
        P.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90)
        P.AddArc(New Rectangle(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90)
        P.AddLine(New Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), New Point(Rectangle.X, Curve + Rectangle.Y))
        Return P
    End Function

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        Checked = Not Checked
        MyBase.OnMouseDown(e)
    End Sub

End Class


