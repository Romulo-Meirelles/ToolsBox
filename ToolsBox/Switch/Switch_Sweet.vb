Option Strict On
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(System.Windows.Forms.CheckBox))> _
Public Class Switch_Sweet : Inherits Control

    Private _check As Boolean
    <Category("ToolsBox Herramienta")> _
    Public Property Checked As Boolean
        Get
            Return _check
        End Get
        Set(value As Boolean)
            _check = value
            Invalidate()
        End Set
    End Property

    Sub New()
        MyBase.New()
        Size = New Size(80, 25)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.Selectable, False)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics
        Dim cr As Rectangle = New Rectangle(0, 0, Width, Height)
        g.FillRectangle(New SolidBrush(Color.FromArgb(86, 186, 236)), cr)
        If Checked Then
            g.FillRectangle(New SolidBrush(Color.FromArgb(222, 184, 135)), New Rectangle(CInt((Width / 2) - 2), 2, CInt((Width / 2)), Height - 4))
            g.DrawString("ON", New Font("Tahoma", 10, FontStyle.Bold), New SolidBrush(Color.FromArgb(255, 255, 255)), New Rectangle(0, 5, CInt((Width / 2)), 15), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
            g.DrawLine(New Pen(Color.Black), 56, 5, 56, Height - 7)
            g.DrawLine(New Pen(Color.Black), 58, 3, 58, Height - 5)
            g.DrawLine(New Pen(Color.Black), 60, 5, 60, Height - 7)
        Else
            g.FillRectangle(New SolidBrush(Color.FromArgb(222, 184, 135)), New Rectangle(2, 2, CInt((Width / 2)), Height - 4))
            g.DrawString("OFF", New Font("Tahoma", 10, FontStyle.Bold), New SolidBrush(Color.FromArgb(255, 255, 255)), New Rectangle(CInt((Width / 2)), 5, CInt((Width / 2)), 15), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
            g.DrawLine(New Pen(Color.Black), 20, 5, 20, Height - 7)
            g.DrawLine(New Pen(Color.Black), 22, 3, 22, Height - 5)
            g.DrawLine(New Pen(Color.Black), 24, 5, 24, Height - 7)
        End If
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnClick(ByVal e As EventArgs)
        MyBase.OnClick(e)
        If Not Checked Then Checked = True Else Checked = False
    End Sub
End Class
