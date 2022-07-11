Imports System, System.IO, System.Collections.Generic
Imports System.Drawing, System.Drawing.Drawing2D
Imports ToolsBox.Controller
Imports System.ComponentModel

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.CheckBox))> _
Public Class Switch_Doom
    Inherits ThemeControl154

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

    Sub changeChecked() Handles Me.Click
        Select Case _Checked
            Case False
                _Checked = True
            Case True
                _Checked = False
        End Select
    End Sub

    Sub New()
        Transparent = True
        Size = New Size(150, 16)
        Font = New Font("Arial", 9)
    End Sub

    Protected Overrides Sub ColorHook()

    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(Color.Transparent)
        'Background
        Dim rect As New Rectangle(0, 0, 45, 15)
        G.FillRectangle(Brushes.Black, rect)
        'On And Off Switches
        If (_Checked = False) Then
            G.DrawString("OFF", Font, Brushes.White, 3, 1)
            Dim offRect As New Rectangle(31, 2, 12, 12)
            Dim offLGB As New LinearGradientBrush(offRect, Color.FromArgb(150, Color.Gainsboro), Color.LightGray, 90.0F)
            G.FillRectangle(offLGB, offRect)
            G.DrawRectangle(Pens.White, New Rectangle(31, 2, 11, 11))
        Else
            G.DrawString("ON", Font, New SolidBrush(Color.FromArgb(200, Color.Lime)), 18, 1)
            Dim onRect As New Rectangle(2, 2, 12, 12)
            Dim onLGB As New LinearGradientBrush(onRect, Color.FromArgb(150, Color.DarkGreen), Color.DarkGreen, 90.0F)
            G.FillRectangle(onLGB, onRect)
            G.DrawRectangle(Pens.Lime, New Rectangle(2, 2, 11, 11))
        End If
        'Text Area
        Dim textAreaRect As New Rectangle(45, 0, Width - 1, Height - 1)
        Dim textAreaHB As New HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.FromArgb(200, 16, 16, 16), Color.FromArgb(200, 14, 14, 14))
        G.FillRectangle(textAreaHB, textAreaRect)
        DrawText(New SolidBrush(Color.Red), HorizontalAlignment.Left, 50, 0)
        'Border
        DrawBorders(Pens.Black)
    End Sub
End Class




