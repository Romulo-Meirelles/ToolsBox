Imports System, System.IO, System.Collections.Generic
Imports System.Drawing, System.Drawing.Drawing2D
Imports System.ComponentModel, System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Drawing.Imaging
Imports System.Windows.Forms.TabControl
Imports System.ComponentModel.Design
Imports ToolsBox.Controller

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.ListView))>
Public Class ListBox_Ghost_Black
    Inherits ThemeControl154
    Public WithEvents LBox As New ListBox
    Private __Items As String() = {""}

    <Category("ToolsBox Herramienta")>
    Public Property Items As String()
        Get
            Return __Items
            Invalidate()
        End Get
        Set(ByVal value As String())
            __Items = value
            LBox.Items.Clear()
            Invalidate()
            LBox.Items.AddRange(value)
            Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")>
    Public ReadOnly Property SelectedItem() As String
        Get
            Return LBox.SelectedItem
        End Get
    End Property

    Sub New()
        Controls.Add(LBox)
        Size = New Size(131, 101)

        LBox.BackColor = Color.FromArgb(0, 0, 0)
        LBox.BorderStyle = BorderStyle.None
        LBox.DrawMode = Windows.Forms.DrawMode.OwnerDrawVariable
        LBox.Location = New Point(3, 3)
        LBox.ForeColor = Color.White
        LBox.ItemHeight = 20
        LBox.Items.Clear()
        LBox.IntegralHeight = False
        Invalidate()
    End Sub
    Protected Overrides Sub ColorHook()
    End Sub

    Protected Overrides Sub OnResize(e As System.EventArgs)
        MyBase.OnResize(e)
        LBox.Width = Width - 4
        LBox.Height = Height - 4
    End Sub

    Protected Overrides Sub PaintHook()
        G.Clear(Color.Black)
        G.DrawRectangle(Pens.Black, 0, 0, Width - 2, Height - 2)
        G.DrawRectangle(New Pen(Color.FromArgb(90, 90, 90)), 1, 1, Width - 3, Height - 3)
        LBox.Size = New Size(Width - 5, Height - 5)
    End Sub
    Sub DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles LBox.DrawItem
        If e.Index < 0 Then Exit Sub
        e.DrawBackground()
        e.DrawFocusRectangle()
        If InStr(e.State.ToString, "Selected,") > 0 Then
            e.Graphics.FillRectangle(Brushes.Black, e.Bounds)
            Dim x2 As Rectangle = New Rectangle(e.Bounds.Location, New Size(e.Bounds.Width - 1, e.Bounds.Height))
            Dim x3 As Rectangle = New Rectangle(x2.Location, New Size(x2.Width, (x2.Height / 2) - 2))
            Dim G1 As New LinearGradientBrush(New Point(x2.X, x2.Y), New Point(x2.X, x2.Y + x2.Height), Color.FromArgb(60, 60, 60), Color.FromArgb(50, 50, 50))
            Dim H As New HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.FromArgb(15, Color.Black), Color.Transparent)
            e.Graphics.FillRectangle(G1, x2) : G1.Dispose()
            e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(25, Color.White)), x3)
            e.Graphics.FillRectangle(H, x2) : G1.Dispose()
            e.Graphics.DrawString(" " & LBox.Items(e.Index).ToString(), Font, Brushes.White, e.Bounds.X, e.Bounds.Y + 2)
        Else
            e.Graphics.DrawString(" " & LBox.Items(e.Index).ToString(), Font, Brushes.White, e.Bounds.X, e.Bounds.Y + 2)
        End If
    End Sub
    Sub AddRange(ByVal Items As Object())
        LBox.Items.Remove("")
        LBox.Items.AddRange(Items)
        Invalidate()
    End Sub
    Sub AddItem(ByVal Item As Object)
        LBox.Items.Remove("")
        LBox.Items.Add(Item)
        Invalidate()
    End Sub
End Class
