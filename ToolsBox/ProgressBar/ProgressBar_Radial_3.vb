Imports System.Drawing.Text
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Design
Imports System.Windows.Forms

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.ProgressBar))> _
Public Class ProgressBar_Radial_3
    Inherits Control

    Private _Value As Integer = 50
    Private _Thickness As Integer = 5
    Private _Angle As Integer = 0
    Private _Symbol As String = "%"

    Sub New()
        Size = New Size(65, 65)
        Invalidate()
    End Sub

    <Category("ToolsBox Herramienta")> _
    Public Property Angle() As Integer
        Get
            Return _Angle
        End Get
        Set(ByVal v As Integer)
            _Angle = v : Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property Symbol() As String
        Get
            Return _Symbol
        End Get
        Set(ByVal v As String)
            _Symbol = v : Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property Thickness() As Integer
        Get
            Return _Thickness
        End Get
        Set(ByVal v As Integer)
            _Thickness = v : Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaintBackground(ByVal p As PaintEventArgs)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
        Me.Width = Me.Height
        Using B1 As New Bitmap(Width, Height)

            Using G As Graphics = Graphics.FromImage(B1)
                G.SmoothingMode = SmoothingMode.AntiAlias


                G.Clear(BackColor)

                Using LGB As New LinearGradientBrush(ClientRectangle, Color.FromArgb(217, 217, 217), Color.FromArgb(217, 217, 217), LinearGradientMode.Vertical)
                    Using P1 As New Pen(LGB, Thickness + 3)
                        G.DrawArc(P1, CInt(Thickness / 2) + 2, CInt(Thickness / 2) + 2, Width - Thickness - 4, Height - Thickness - 4, -90, 360)
                    End Using
                End Using

                Using LGB As New LinearGradientBrush(ClientRectangle, Color.FromArgb(0, 166, 208), Color.FromArgb(0, 166, 208), LinearGradientMode.Vertical)
                    Using P1 As New Pen(LGB, Thickness + 3)
                        G.DrawArc(P1, CInt(Thickness / 2) + 9, CInt(Thickness / 2) + 9, Width - Thickness - 18, Height - Thickness - 18, -90, 360)
                    End Using
                End Using

                Using LGB As New LinearGradientBrush(ClientRectangle, Color.FromArgb(255, 255, 255), Color.FromArgb(255, 255, 255), LinearGradientMode.Vertical)
                    Using P1 As New Pen(LGB, Thickness - 2)
                        Dim i As Integer = 360 / 100 * _Value
                        G.DrawArc(P1, CInt(Thickness / 2) + 9, CInt(Thickness / 2) + 9, Width - Thickness - 18, Height - Thickness - 18, -90, i)
                    End Using
                End Using


                G.DrawString(_Value & _Symbol, New Font("Arial", 13), Brushes.Black, New Point(Me.Width / 2 - G.MeasureString(_Value & _Symbol, New Font("Arial", 13)).Width / 2 + 1, Me.Height / 2 - G.MeasureString(_Value & "%", New Font("Arial", 13)).Height / 2 + 1))
            End Using
            e.Graphics.DrawImage(B1, 0, 0)
        End Using
    End Sub
End Class

