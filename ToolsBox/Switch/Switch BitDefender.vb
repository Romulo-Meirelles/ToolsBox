Imports System.Drawing.Drawing2D
Imports System.Threading
Imports System.ComponentModel
Imports System.Drawing.Text
Imports System.Security

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.CheckBox))> _
Public Class Switch_Bitdefender : Inherits Control

    Private cn As ContainerObjectDisposable
    Private R1, R2, R3, R4, R5, R6 As Rectangle
    Private GP1, GP2, GP3, GP4, GP5, GP6 As GraphicsPath
    Private LGB1, LGB2, LGB3, LGB4 As LinearGradientBrush
    Private PGB1 As PathGradientBrush
    Private B1, B2, B3 As SolidBrush
    Private G As Graphics
    Private P1, P2 As Pen
    Dim CheckSize As Size
    Dim oldsize As Size

    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.DoubleBuffer Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.SupportsTransparentBackColor Or ControlStyles.UserPaint, True)
        Width = 55
        Height = 25
        BackColor = Color.Transparent
        oldsize = New Size(55, 25)
        _Swith_Text_On = "ON"
        _Swith_Text_Off = "OFF"
        _Color_Enabled = Color.FromArgb(84, 135, 171)
        _Color_Disabled = Color.FromArgb(29, 29, 29)
        _Color_Button = Color.FromArgb(59, 59, 59)
    End Sub
    Public Event ChangeChecked(ByVal sender As Object, ByVal check As Boolean)

    Private _Check As Boolean
    <Category("ToolsBox Herramienta")> _
    Public Property Checked As Boolean
        Get
            Return _Check
        End Get
        Set(value As Boolean)
            _Check = value
            Invalidate()

            RaiseEvent ChangeChecked(Me, value)
        End Set
    End Property

    Private _Hover As Boolean
    <Category("ToolsBox Herramienta")> _
    Private Property Hover As Boolean
        Get
            Return _Hover
        End Get
        Set(value As Boolean)
            _Hover = value
            Invalidate()
        End Set
    End Property

    Private _Color_Enabled As Color
    <Category("ToolsBox Herramienta")> _
    Public Property Color_Enabled() As Color
        Get
            Return _Color_Enabled
        End Get
        Set(ByVal value As Color)
            _Color_Enabled = value
            Invalidate()
        End Set
    End Property

    Private _Color_Disabled As Color
    <Category("ToolsBox Herramienta")> _
    Public Property Color_Disabled() As Color
        Get
            Return _Color_Disabled
        End Get
        Set(ByVal value As Color)
            _Color_Disabled = value
            Invalidate()
        End Set
    End Property

    Private _Color_Button As Color
    <Category("ToolsBox Herramienta")> _
    Public Property Color_Button() As Color
        Get
            Return _Color_Button
        End Get
        Set(ByVal value As Color)
            _Color_Button = value
            Invalidate()
        End Set
    End Property

    Private _Swith_Text_On As String
    <Category("ToolsBox Herramienta")> _
    Public Property Swith_Text_On As String
        Get
            Return _Swith_Text_On
        End Get
        Set(value As String)
            _Swith_Text_On = value
            Invalidate()

            RaiseEvent ChangeChecked(Me, value)
        End Set
    End Property

    Private _Swith_Text_Off As String
    <Category("ToolsBox Herramienta")> _
    Public Property Swith_Text_Off As String
        Get
            Return _Swith_Text_Off
        End Get
        Set(value As String)
            _Swith_Text_Off = value
            Invalidate()

            RaiseEvent ChangeChecked(Me, value)
        End Set
    End Property


    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        MyBase.OnMouseUp(e) : Checked = Not Checked
    End Sub

    Private Sub Init(e As PaintEventArgs)
        G = e.Graphics
        cn = New ContainerObjectDisposable
        R1 = New Rectangle(1, 1, Width - 2, Height - 2)
        R2 = New Rectangle(2, 2, Width - 4, Height - 4)

        GP1 = RoundRect(R1, 11) : cn.AddObject(GP1)
        GP2 = RoundRect(R2, 11) : cn.AddObject(GP2)

        B1 = New SolidBrush(Color.FromArgb(40, 40, 40)) : cn.AddObject(B1)

        If Checked Then
            B2 = New SolidBrush(Color.FromArgb(84, 135, 171))
            PGB1 = New PathGradientBrush(GP2) With {.CenterColor = _Color_Enabled, .SurroundColors = {Color.FromArgb(113, 176, 200)}, .FocusScales = New PointF(0.85, 0.65)}

        Else
            B2 = New SolidBrush(Color.FromArgb(29, 29, 29))
            PGB1 = New PathGradientBrush(GP2) With {.CenterColor = _Color_Disabled, .SurroundColors = {Color.FromArgb(39, 39, 39)}, .FocusScales = New PointF(0.85, 0.65)}
        End If

        cn.AddObjectRange({B2, PGB1})
        B3 = New SolidBrush(Color.FromArgb(107, 107, 107))
        cn.AddObject(B3)

        CheckSize = New Size(22, R2.Height)
        R3 = New Rectangle(Width - 2 - 22, 2, CheckSize.Width, CheckSize.Height)
        GP3 = RoundRect(R3, 11)
        R4 = New Rectangle(R3.X + 1, R3.Y + 1, R3.Width - 2, R3.Height - 2)
        GP4 = RoundRect(R4, 11)

        R5 = New Rectangle(0, 2, CheckSize.Width, CheckSize.Height)
        GP5 = RoundRect(R5, 11)
        R6 = New Rectangle(R5.X + 1, R5.Y + 1, R5.Width - 2, R5.Height - 2)
        GP6 = RoundRect(R6, 11)
        cn.AddObjectRange({GP3, GP4, GP5, GP6})

        If Hover Then
            LGB1 = New LinearGradientBrush(R3, Color.FromArgb(86, 86, 86), _Color_Button, LinearGradientMode.Vertical)
            LGB2 = New LinearGradientBrush(New Rectangle(R4.X - 1, R4.Y - 1, R4.Width + 2, R4.Height + 2), Color.FromArgb(147, 147, 147), Color.FromArgb(68, 68, 68), LinearGradientMode.Vertical)
            P1 = New Pen(LGB2)

            LGB3 = New LinearGradientBrush(R5, Color.FromArgb(86, 86, 86), _Color_Button, LinearGradientMode.Vertical)
            LGB4 = New LinearGradientBrush(New Rectangle(R6.X - 1, R6.Y - 1, R6.Width + 2, R6.Height + 2), Color.FromArgb(147, 147, 147), Color.FromArgb(68, 68, 68), LinearGradientMode.Vertical)
            P2 = New Pen(LGB4)

        Else
            LGB1 = New LinearGradientBrush(R3, _Color_Button, Color.FromArgb(29, 29, 29), LinearGradientMode.Vertical)
            LGB2 = New LinearGradientBrush(New Rectangle(R4.X - 1, R4.Y - 1, R4.Width + 2, R4.Height + 2), Color.FromArgb(101, 101, 101), Color.FromArgb(42, 42, 42), LinearGradientMode.Vertical)
            P1 = New Pen(LGB2)

            LGB3 = New LinearGradientBrush(R5, _Color_Button, Color.FromArgb(29, 29, 29), LinearGradientMode.Vertical)
            LGB4 = New LinearGradientBrush(New Rectangle(R6.X - 1, R6.Y - 1, R6.Width + 2, R6.Height + 2), Color.FromArgb(101, 101, 101), Color.FromArgb(42, 42, 42), LinearGradientMode.Vertical)
            P2 = New Pen(LGB4)

        End If
        cn.AddObjectRange({LGB1, LGB2, LGB3, LGB4, P1, P2})


    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
        Init(e)
        G.SmoothingMode = SmoothingMode.AntiAlias
        G.InterpolationMode = InterpolationMode.HighQualityBicubic
        G.FillPath(B1, GP1)

        If Checked Then

            G.FillPath(B2, GP2)
            G.FillPath(PGB1, GP2)
            G.DrawPath(Pens.Black, GP2)

            If Hover Then
                G.FillPath(LGB1, GP3)
                G.DrawPath(Pens.Black, GP3)
                G.DrawPath(P1, GP4)
                G.DrawString(_Swith_Text_On, New Font("Microsoft Sans Serif", 7, FontStyle.Bold), Brushes.Black, 7, 6)
                G.DrawString(_Swith_Text_On, New Font("Microsoft Sans Serif", 7, FontStyle.Bold), Brushes.White, 7, 7)
            Else
                G.FillPath(LGB1, GP3)
                G.DrawPath(Pens.Black, GP3)
                G.DrawPath(P1, GP4)
                G.DrawString(_Swith_Text_On, New Font("Microsoft Sans Serif", 7, FontStyle.Bold), Brushes.Black, 7, 6)
                G.DrawString(_Swith_Text_On, New Font("Microsoft Sans Serif", 7, FontStyle.Bold), Brushes.White, 7, 7)
            End If

        Else
            G.FillPath(B1, GP1)
            G.FillPath(B2, GP2)
            G.FillPath(PGB1, GP2)
            G.DrawPath(Pens.Black, GP2)

            If Hover Then
                G.FillPath(LGB3, GP5)
                G.DrawPath(Pens.Black, GP5)
                G.DrawPath(P2, GP6)
                G.DrawString(_Swith_Text_Off, New Font("Microsoft Sans Serif", 7, FontStyle.Bold), Brushes.Black, Width - 29, 6)
                G.DrawString(_Swith_Text_Off, New Font("Microsoft Sans Serif", 7, FontStyle.Bold), B3, Width - 29, 7)
            Else
                G.FillPath(LGB3, GP5)
                G.DrawPath(Pens.Black, GP5)
                G.DrawPath(P2, GP6)
                G.DrawString(_Swith_Text_Off, New Font("Microsoft Sans Serif", 7, FontStyle.Bold), Brushes.Black, Width - 29, 6)
                G.DrawString(_Swith_Text_Off, New Font("Microsoft Sans Serif", 7, FontStyle.Bold), B3, Width - 29, 7)
            End If

        End If
        cn.Dispose()
    End Sub

    Protected Overrides Sub onmouseenter(ByVal e As EventArgs)
        MyBase.OnMouseEnter(e)
        Hover = True
    End Sub
    Protected Overrides Sub onmouseleave(ByVal e As EventArgs)
        MyBase.OnMouseLeave(e)
        Hover = False
    End Sub


    Protected Overrides Sub onclientsizechanged(e As EventArgs)
        MyBase.OnClientSizeChanged(e)
        Me.Size = oldsize

    End Sub


End Class

<ToolboxBitmap(GetType(Switch), "Switch")> _
Class ContainerObjectDisposable : Implements IDisposable
    Private iList As New List(Of IDisposable)

    Public Sub AddObject(ByVal Obj As IDisposable)
        iList.Add(Obj)
    End Sub
    Public Sub AddObjectRange(ByVal Obj() As IDisposable)
        iList.AddRange(Obj)
    End Sub
#Region "IDisposable Support"
    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                For Each ObjectDisposable As IDisposable In iList
                    ObjectDisposable.Dispose()
#If DEBUG Then
                    Debug.WriteLine("Dispose : " & ObjectDisposable.ToString)
#End If
                Next
            End If

        End If
        iList.Clear()
        Me.disposedValue = True
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

<ToolboxBitmap(GetType(Button))>
Module Helper

    Friend Function RoundRect(ByVal R As Rectangle, radius As Integer) As GraphicsPath
        Dim GP As New GraphicsPath
        GP.AddArc(R.X, R.Y, radius, radius, 180, 90)
        GP.AddArc(R.Right - radius, R.Y, radius, radius, 270, 90)
        GP.AddArc(R.Right - radius, R.Bottom - radius, radius, radius, 0, 90)
        GP.AddArc(R.X, R.Bottom - radius, radius, radius, 90, 90)
        GP.CloseFigure()
        Return GP
    End Function

End Module