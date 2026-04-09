Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

<DesignerCategory("Component")>
Public Class Elipse
    Inherits Component

    Private _radius As Integer = 5
    Private _target As Control

    ' ===== PROPRIEDADES =====

    <Category("Elipse"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property ElipseRadius As Integer
        Get
            Return _radius
        End Get
        Set(value As Integer)
            _radius = Math.Max(1, value)
            ApplyElipse()
        End Set
    End Property

    <Category("Elipse"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Property TargetControl As Control
        Get
            Return _target
        End Get
        Set(value As Control)
            _target = value
            ApplyElipse()
        End Set
    End Property

    ' ===== SITE (CORRETO) =====
    Public Overrides Property Site As ISite
        Get
            Return MyBase.Site
        End Get
        Set(value As ISite)
            MyBase.Site = value

            If value Is Nothing OrElse Not value.DesignMode Then Exit Property

            Dim host As IDesignerHost =
                TryCast(value.GetService(GetType(IDesignerHost)), IDesignerHost)

            If host Is Nothing Then Exit Property

            For Each comp As IComponent In host.Container.Components
                If TypeOf comp Is Form Then
                    Dim frm As Form = DirectCast(comp, Form)

                    _target = frm
                    frm.FormBorderStyle = FormBorderStyle.None
                    ApplyElipse()
                    Exit For
                End If
            Next
        End Set
    End Property

    ' ===== APLICA ELIPSE =====
    Private Sub ApplyElipse()
        If _target Is Nothing OrElse _target.Width = 0 OrElse _target.Height = 0 Then Exit Sub

        Dim path As New GraphicsPath()
        Dim r As Integer = _radius
        Dim w As Integer = _target.Width
        Dim h As Integer = _target.Height

        path.AddArc(0, 0, r * 2, r * 2, 180, 90)
        path.AddArc(w - r * 2, 0, r * 2, r * 2, 270, 90)
        path.AddArc(w - r * 2, h - r * 2, r * 2, r * 2, 0, 90)
        path.AddArc(0, h - r * 2, r * 2, r * 2, 90, 90)
        path.CloseFigure()

        _target.Region = New Region(path)
    End Sub

End Class

