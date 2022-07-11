Imports System, System.IO, System.Collections.Generic
Imports System.Drawing, System.Drawing.Drawing2D
Imports System.ComponentModel, System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Drawing.Imaging
Imports System.Windows.Forms.TabControl
Imports System.ComponentModel.Design
Imports ToolsBox.Controller

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.ListView))> _
Public Class Listbox_Ghost_Less
    Inherits ListBox

    Sub New()
        SetStyle(ControlStyles.DoubleBuffer, True)
        Font = New Font("Microsoft Sans Serif", 9)
        BorderStyle = Windows.Forms.BorderStyle.None
        DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
        ItemHeight = 20
        ForeColor = Color.DeepSkyBlue
        BackColor = Color.FromArgb(7, 7, 7)
        IntegralHeight = False
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        MyBase.WndProc(m)
        If m.Msg = 15 Then CustomPaint()
    End Sub

    Protected Overrides Sub OnDrawItem(e As System.Windows.Forms.DrawItemEventArgs)
        Try
            If e.Index < 0 Then Exit Sub
            e.DrawBackground()
            Dim rect As New Rectangle(New Point(e.Bounds.Left, e.Bounds.Top + 2), New Size(Bounds.Width, 16))
            e.DrawFocusRectangle()
            If InStr(e.State.ToString, "Selected,") > 0 Then
                e.Graphics.FillRectangle(Brushes.Black, e.Bounds)
                Dim x2 As Rectangle = New Rectangle(e.Bounds.Location, New Size(e.Bounds.Width - 1, e.Bounds.Height))
                Dim x3 As Rectangle = New Rectangle(x2.Location, New Size(x2.Width, (x2.Height / 2)))
                Dim G1 As New LinearGradientBrush(New Point(x2.X, x2.Y), New Point(x2.X, x2.Y + x2.Height), Color.FromArgb(60, 60, 60), Color.FromArgb(50, 50, 50))
                Dim H As New HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.FromArgb(15, Color.Black), Color.Transparent)
                e.Graphics.FillRectangle(G1, x2) : G1.Dispose()
                e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(25, Color.White)), x3)
                e.Graphics.FillRectangle(H, x2) : G1.Dispose()
                e.Graphics.DrawString(" " & Items(e.Index).ToString(), Font, Brushes.White, e.Bounds.X, e.Bounds.Y + 1)
            Else
                e.Graphics.DrawString(" " & Items(e.Index).ToString(), Font, Brushes.White, e.Bounds.X, e.Bounds.Y + 1)
            End If
            e.Graphics.DrawRectangle(New Pen(Color.FromArgb(0, 0, 0)), New Rectangle(1, 1, Width - 3, Height - 3))
            e.Graphics.DrawRectangle(New Pen(Color.FromArgb(90, 90, 90)), New Rectangle(0, 0, Width - 1, Height - 1))
            MyBase.OnDrawItem(e)
        Catch ex As Exception : End Try
    End Sub

    Sub CustomPaint()
        CreateGraphics.DrawRectangle(New Pen(Color.FromArgb(0, 0, 0)), New Rectangle(1, 1, Width - 3, Height - 3))
        CreateGraphics.DrawRectangle(New Pen(Color.FromArgb(90, 90, 90)), New Rectangle(0, 0, Width - 1, Height - 1))
    End Sub
End Class







