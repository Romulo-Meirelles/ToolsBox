Imports System, System.Collections
Imports System.Drawing, System.Drawing.Drawing2D
Imports System.ComponentModel, System.Windows.Forms
Imports System.IO, System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Drawing.Imaging
Imports ToolsBox.Controller

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.CheckBox))> _
Public Class Switch_VI
    Inherits ThemeControl154
    Private P1, P2 As Pen
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
        Size = New Size(120, 25)
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
        P2 = New Pen(Color.FromArgb(24, 24, 24))
        B1 = New SolidBrush(Color.FromArgb(15, Color.FromArgb(26, 26, 26)))
        B2 = New SolidBrush(Color.White)
        B3 = New SolidBrush(Color.FromArgb(0, 0, 0))
    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(BackColor)

        If (_Checked = False) Then
            G.FillRectangle(B3, 4, 4, 45, 15)
            G.DrawRectangle(P1, 4, 4, 45, 15)
            G.DrawRectangle(P2, 5, 5, 43, 13)
            G.DrawString("OFF", Font, Brushes.Red, 7, 5)
            G.FillRectangle(New LinearGradientBrush(New Rectangle(32, 2, 13, 19), Color.FromArgb(35, 35, 35), Color.FromArgb(25, 25, 25), 90S), 32, 2, 13, 19)
            G.DrawRectangle(P2, 32, 2, 13, 19)
            G.DrawRectangle(P1, 33, 3, 11, 17)
            G.DrawRectangle(P1, 31, 1, 15, 21)
        Else
            G.FillRectangle(B3, 4, 4, 45, 15)
            G.DrawRectangle(P1, 4, 4, 45, 15)
            G.DrawRectangle(P2, 5, 5, 43, 13)
            G.DrawString("ON", Font, Brushes.Green, 23, 5)
            G.FillRectangle(New LinearGradientBrush(New Rectangle(8, 2, 13, 19), Color.FromArgb(80, 80, 80), Color.FromArgb(60, 60, 60), 90S), 8, 2, 13, 19)
            G.DrawRectangle(P2, 8, 2, 13, 19)
            G.DrawRectangle(P1, 9, 3, 11, 17)
            G.DrawRectangle(P1, 7, 1, 15, 21)
        End If
        G.FillRectangle(B1, 2, 2, 41, 11)
        DrawText(B2, HorizontalAlignment.Left, 50, 0)
    End Sub
End Class