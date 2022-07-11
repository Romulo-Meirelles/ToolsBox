Imports System.Drawing, System.Drawing.Drawing2D
Imports ToolsBox.Controller
Imports System.ComponentModel

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.CheckBox))> _
Public Class Switch_V
    Inherits ThemeControl154
    Private P1 As Pen
    Private B1 As Brush
    Private B2 As Brush
    Private B3 As Brush

    Private _Checked As Boolean = False
    <Category("ToolsBox Herramienta")> _
    Public Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal checked As Boolean)
            _Checked = checked
            Invalidate()
        End Set
    End Property
    Sub New()
        BackColor = Color.Transparent
        Transparent = True
        Size = New Size(150, 16)
    End Sub
    Sub changeChecked() Handles Me.Click
        Select Case _Checked
            Case False
                _Checked = True
            Case True
                _Checked = False
        End Select
    End Sub
    Protected Overrides Sub ColorHook()
        P1 = New Pen(Color.FromArgb(0, 0, 0))
        B1 = New SolidBrush(Color.FromArgb(15, Color.FromArgb(26, 26, 26)))
        B2 = New SolidBrush(Color.White)
        B3 = New SolidBrush(Color.FromArgb(0, 0, 0))
    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(BackColor)
        G.FillRectangle(B3, 0, 0, 45, 15)
        G.DrawRectangle(P1, 0, 0, 45, 15)
        If (_Checked = False) Then
            G.DrawString("OFF", Font, Brushes.Gray, 3, 1)
            G.FillRectangle(New LinearGradientBrush(New Rectangle(29, -1, 13, 16), Color.FromArgb(35, 35, 35), Color.FromArgb(25, 25, 25), 90S), 29, -1, 13, 16)
        Else
            DrawGradient(Color.FromArgb(10, 10, 10), Color.FromArgb(20, 20, 20), 15, 2, 28, 11, 90S)
            G.DrawString("ON", Font, Brushes.White, 18, 0)
            G.FillRectangle(New LinearGradientBrush(New Rectangle(2, -1, 13, 16), Color.FromArgb(80, 80, 80), Color.FromArgb(60, 60, 60), 90S), 2, -1, 13, 16)
        End If
        G.FillRectangle(B1, 2, 2, 41, 11)
        DrawText(B2, HorizontalAlignment.Left, 50, 0)
    End Sub
End Class









