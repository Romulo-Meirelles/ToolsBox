Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

<DesignTimeVisible(True)>
<ToolboxBitmap(GetType(RadioButton))> _
Public Class RadioButton_
    Inherits RadioButton

    Private _checkedColor As Color = Color.MediumSlateBlue
    Private _unCheckedColor As Color = Color.Gray

    <Category("ToolsBox Herramienta")> _
    Public Property CheckedColor As Color
        Get
            Return _checkedColor
        End Get
        Set(ByVal value As Color)
            _checkedColor = value
            Me.Invalidate()
        End Set
    End Property

    <Category("ToolsBox Herramienta")> _
    Public Property UnCheckedColor As Color
        Get
            Return _unCheckedColor
        End Get
        Set(ByVal value As Color)
            _unCheckedColor = value
            Me.Invalidate()
        End Set
    End Property

    Public Sub New()
        Me.MinimumSize = New Size(0, 21)
        Me.Padding = New Padding(10, 0, 0, 0)
    End Sub

    Protected Overrides Sub OnPaint(ByVal pevent As PaintEventArgs)
        Dim graphics As Graphics = pevent.Graphics
        graphics.SmoothingMode = SmoothingMode.AntiAlias
        Dim rbBorderSize As Single = 18.0F
        Dim rbCheckSize As Single = 12.0F
        Dim rectRbBorder As RectangleF = New RectangleF() With {
            .X = 0.5F,
            .Y = (Me.Height - rbBorderSize) / 2,
            .Width = rbBorderSize,
            .Height = rbBorderSize
        }
        Dim rectRbCheck As RectangleF = New RectangleF() With {
            .X = rectRbBorder.X + ((rectRbBorder.Width - rbCheckSize) / 2),
            .Y = (Me.Height - rbCheckSize) / 2,
            .Width = rbCheckSize,
            .Height = rbCheckSize
        }

        Using penBorder As Pen = New Pen(_checkedColor, 1.6F)

            Using brushRbCheck As SolidBrush = New SolidBrush(_checkedColor)

                Using brushText As SolidBrush = New SolidBrush(Me.ForeColor)
                    graphics.Clear(Me.BackColor)

                    If Me.Checked Then
                        graphics.DrawEllipse(penBorder, rectRbBorder)
                        graphics.FillEllipse(brushRbCheck, rectRbCheck)
                    Else
                        penBorder.Color = _unCheckedColor
                        graphics.DrawEllipse(penBorder, rectRbBorder)
                    End If

                    graphics.DrawString(Me.Text, Me.Font, brushText, rbBorderSize + 8, (Me.Height - TextRenderer.MeasureText(Me.Text, Me.Font).Height) / 2)
                End Using
            End Using
        End Using
    End Sub
End Class

