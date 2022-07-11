Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Security.Permissions

Module DesignFunctions
    Function ToBrush(ByVal A As Integer, ByVal R As Integer, ByVal G As Integer, ByVal B As Integer) As Brush
        Return ToBrush(Color.FromArgb(A, R, G, B))
    End Function
    Function ToBrush(ByVal R As Integer, ByVal G As Integer, ByVal B As Integer) As Brush
        Return ToBrush(Color.FromArgb(R, G, B))
    End Function
    Function ToBrush(ByVal A As Integer, ByVal C As Color) As Brush
        Return ToBrush(Color.FromArgb(A, C))
    End Function
    Function ToBrush(ByVal Pen As Pen) As Brush
        Return ToBrush(Pen.Color)
    End Function
    Function ToBrush(ByVal Color As Color) As Brush
        Return New SolidBrush(Color)
    End Function
    Function ToPen(ByVal A As Integer, ByVal R As Integer, ByVal G As Integer, ByVal B As Integer) As Pen
        Return ToPen(Color.FromArgb(A, R, G, B))
    End Function
    Function ToPen(ByVal R As Integer, ByVal G As Integer, ByVal B As Integer) As Pen
        Return ToPen(Color.FromArgb(R, G, B))
    End Function
    Function ToPen(ByVal A As Integer, ByVal C As Color) As Pen
        Return ToPen(Color.FromArgb(A, C))
    End Function
    Function ToPen(ByVal Color As Color) As Pen
        Return ToPen(New SolidBrush(Color))
    End Function
    Function ToPen(ByVal Brush As SolidBrush) As Pen
        Return New Pen(Brush)
    End Function

    Class CornerStyle
        Public TopLeft As Boolean
        Public TopRight As Boolean
        Public BottomLeft As Boolean
        Public BottomRight As Boolean
    End Class

    Public Function AdvRect(ByVal Rectangle As Rectangle, ByVal CornerStyle As CornerStyle, ByVal Curve As Integer) As GraphicsPath
        AdvRect = New GraphicsPath()
        Dim ArcRectangleWidth As Integer = Curve * 2

        If CornerStyle.TopLeft Then
            AdvRect.AddArc(New Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90)
        Else
            AdvRect.AddLine(Rectangle.X, Rectangle.Y, Rectangle.X + ArcRectangleWidth, Rectangle.Y)
        End If

        If CornerStyle.TopRight Then
            AdvRect.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90)
        Else
            AdvRect.AddLine(Rectangle.X + Rectangle.Width, Rectangle.Y, Rectangle.X + Rectangle.Width, Rectangle.Y + ArcRectangleWidth)
        End If

        If CornerStyle.BottomRight Then
            AdvRect.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90)
        Else
            AdvRect.AddLine(Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height, Rectangle.X + Rectangle.Width - ArcRectangleWidth, Rectangle.Y + Rectangle.Height)
        End If

        If CornerStyle.BottomLeft Then
            AdvRect.AddArc(New Rectangle(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90)
        Else
            AdvRect.AddLine(Rectangle.X, Rectangle.Y + Rectangle.Height, Rectangle.X, Rectangle.Y + Rectangle.Height - ArcRectangleWidth)
        End If

        AdvRect.CloseAllFigures()

        Return AdvRect
    End Function

    Public Function RoundRectAresio(ByVal Rectangle As Rectangle, ByVal Curve As Integer) As GraphicsPath
        RoundRectAresio = New GraphicsPath()
        Dim ArcRectangleWidth As Integer = Curve * 2
        RoundRectAresio.AddArc(New Rectangle(Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -180, 90)
        RoundRectAresio.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), -90, 90)
        RoundRectAresio.AddArc(New Rectangle(Rectangle.Width - ArcRectangleWidth + Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 0, 90)
        RoundRectAresio.AddArc(New Rectangle(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y, ArcRectangleWidth, ArcRectangleWidth), 90, 90)
        RoundRectAresio.AddLine(New Point(Rectangle.X, Rectangle.Height - ArcRectangleWidth + Rectangle.Y), New Point(Rectangle.X, ArcRectangleWidth + Rectangle.Y))
        RoundRectAresio.CloseAllFigures()
        Return RoundRectAresio
    End Function

    Public Function RoundRectAresio(ByVal X As Integer, ByVal Y As Integer, ByVal Width As Integer, ByVal Height As Integer, ByVal Curve As Integer) As GraphicsPath
        Return RoundRectAresio(New Rectangle(X, Y, Width, Height), Curve)
    End Function

    Class PillStyle
        Public Left As Boolean
        Public Right As Boolean
    End Class

    Public Function Pill(ByVal Rectangle As Rectangle, ByVal PillStyle As PillStyle) As GraphicsPath
        Pill = New GraphicsPath()

        If PillStyle.Left Then
            Pill.AddArc(New Rectangle(Rectangle.X, Rectangle.Y, Rectangle.Height, Rectangle.Height), -270, 180)
        Else
            Pill.AddLine(Rectangle.X, Rectangle.Y + Rectangle.Height, Rectangle.X, Rectangle.Y)
        End If

        If PillStyle.Right Then
            Pill.AddArc(New Rectangle(Rectangle.X + Rectangle.Width - Rectangle.Height, Rectangle.Y, Rectangle.Height, Rectangle.Height), -90, 180)
        Else
            Pill.AddLine(Rectangle.X + Rectangle.Width, Rectangle.Y, Rectangle.X + Rectangle.Width, Rectangle.Y + Rectangle.Height)
        End If

        Pill.CloseAllFigures()

        Return Pill
    End Function

    Public Function Pill(ByVal X As Integer, ByVal Y As Integer, ByVal Width As Integer, ByVal Height As Integer, ByVal PillStyle As PillStyle)
        Return Pill(New Rectangle(X, Y, Width, Height), PillStyle)
    End Function

End Module

'<ToolboxBitmap("C:\Users\Romulo Meirelles\Desktop\01.png")>
'<ToolboxBitmap(GetType(button), "Button")> _


<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.CheckBox))> _
Public Class Switch_Aresio : Inherits Control

    Private ToggleLocation As Integer = 0
    Private WithEvents ToggleAnimation As Timer = New Timer With {.Interval = 1}

    Event ToggledChanged()

    Private _Checked As Boolean

    <Category("ToolsBox Herramienta")> _
    Public Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            _Checked = value
            Invalidate()

            RaiseEvent ToggledChanged()
        End Set
    End Property

    Private _Switch_Color As Color
    <Category("ToolsBox Herramienta")> _
    Public Property Switch_Color() As Color
        Get
            Return _Switch_Color
        End Get
        Set(ByVal v As Color)
            _Switch_Color = v
            Invalidate()
            RaiseEvent ToggledChanged()
        End Set
    End Property

    Private _Switch_ForeColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property Switch_ForeColor() As Color
        Get
            Return _Switch_ForeColor
        End Get
        Set(ByVal v As Color)
            _Switch_ForeColor = v
            Invalidate()
            RaiseEvent ToggledChanged()
        End Set
    End Property

    Private _Switch_ButtonColor As Color
    <Category("ToolsBox Herramienta")> _
    Public Property Switch_ButtonColor() As Color
        Get
            Return _Switch_ButtonColor
        End Get
        Set(ByVal v As Color)
            _Switch_ButtonColor = v
            Invalidate()
            RaiseEvent ToggledChanged()
        End Set
    End Property

    Private _Switch_On As String
    <Category("ToolsBox Herramienta")> _
    Public Property Switch_On() As String
        Get
            Return _Switch_On
        End Get
        Set(ByVal v As String)
            _Switch_On = v
            Invalidate()
            RaiseEvent ToggledChanged()
        End Set
    End Property

    Private _Switch_Off As String
    <Category("ToolsBox Herramienta")> _
    Public Property Switch_Off() As String
        Get
            Return _Switch_Off
        End Get
        Set(ByVal v As String)
            _Switch_Off = v
            Invalidate()
            RaiseEvent ToggledChanged()
        End Set
    End Property



    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or
                ControlStyles.ResizeRedraw Or
                ControlStyles.UserPaint Or
                ControlStyles.DoubleBuffer, True)

        _Switch_ButtonColor = Color.WhiteSmoke
        _Switch_Color = Color.Lime
        _Switch_ForeColor = Color.Black
        _Switch_On = "ON"
        _Switch_Off = "OFF"
    End Sub

    Protected Overrides Sub CreateHandle()
        MyBase.CreateHandle()
        ToggleAnimation.Start()
    End Sub

    Private Sub Animation() Handles ToggleAnimation.Tick
        If _Checked Then
            If ToggleLocation < 100 Then
                ToggleLocation += 10
            End If
        Else
            If ToggleLocation > 0 Then
                ToggleLocation -= 10
            End If
        End If

        Invalidate()
    End Sub

    Dim Bar As Rectangle = New Rectangle(0, 10, Width - 1, Height - 21)
    Dim Track As Size = New Size(20, 20)

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
        Dim G As Graphics = e.Graphics
        Bar = New Rectangle(10, 10, Width - 21, Height - 21)
        G.Clear(Parent.FindForm.BackColor)
        G.SmoothingMode = SmoothingMode.AntiAlias

        'Background
        Dim BackLinear As LinearGradientBrush = New LinearGradientBrush(New Point(0, CInt((Height / 2) - (Track.Height / 2))), New Point(0, CInt((Height / 2) + (Track.Height / 2))), Color.FromArgb(50, Color.Black), Color.Transparent)
        G.FillPath(BackLinear, RoundRectAresio(0, CInt((Height / 2) - 4), Width - 1, 8, 3)) '
        G.FillPath(BackLinear, Pill(0, CInt(Height / 2 - Track.Height / 2), Width - 1, Track.Height - 2, New PillStyle With {.Left = True, .Right = True}))
        G.DrawPath(ToPen(50, Color.Black), Pill(0, CInt(Height / 2 - Track.Height / 2), Width - 1, Track.Height - 2, New PillStyle With {.Left = True, .Right = True}))

        BackLinear.Dispose()

        'Fill()
        If ToggleLocation > 0 Then
            G.FillPath(New LinearGradientBrush(New Point(0, CInt((Height / 2) - Track.Height / 2)), New Point(1, CInt((Height / 2) + Track.Height / 2)), _Switch_Color, _Switch_Color), Pill(1, CInt((Height / 2) - Track.Height / 2), CInt(Bar.Width * (ToggleLocation / 100)) + CInt(Track.Width / 2), Track.Height - 3, New PillStyle With {.Left = True, .Right = True}))
            G.DrawPath(ToPen(100, Color.White), Pill(1, CInt((Height / 2) - Track.Height / 2 + 1), CInt(Bar.Width * (ToggleLocation / 100)) + CInt(Track.Width / 2), Track.Height - 5, New PillStyle With {.Left = True, .Right = True}))
        End If

        If _Checked Then
            G.DrawString(_Switch_On, New Font("Arial", 6, FontStyle.Bold), ToBrush(150, _Switch_ForeColor), New Rectangle(0, -1, Width - Track.Width + Track.Width / 3, Height), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        Else
            G.DrawString(_Switch_Off, New Font("Arial", 6, FontStyle.Bold), ToBrush(150, _Switch_ForeColor), New Rectangle(Track.Width - Track.Width / 3, -1, Width - Track.Width + Track.Width / 3, Height), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        End If

        'Button
        G.FillEllipse(New SolidBrush(_Switch_ButtonColor), Bar.X + CInt(Bar.Width * (ToggleLocation / 100)) - CInt(Track.Width / 2), Bar.Y + CInt((Bar.Height / 2)) - CInt(Track.Height / 2), Track.Width, Track.Height)
        G.DrawEllipse(ToPen(50, Color.White), Bar.X + CInt(Bar.Width * (ToggleLocation / 100) - CInt(Track.Width / 2)), Bar.Y + CInt((Bar.Height / 2)) - CInt(Track.Height / 2), Track.Width, Track.Height)
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e)
        _Checked = Not Checked
    End Sub
End Class

