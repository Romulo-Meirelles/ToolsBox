Namespace Utils
    Public Module CenterForm

        Public Sub CenterForm(ByVal Form As Windows.Forms.Form, Optional ByVal FormExample As Object = Nothing)
            '' Note: call this from frm's Load event!
            Dim r As Rectangle
            If FormExample IsNot Nothing Then
                r = FormExample.RectangleToScreen(FormExample.ClientRectangle)
            Else
                r = Screen.FromPoint(Form.Location).WorkingArea
            End If

            Dim x = (r.Width - Form.Width) / 2
            Dim y = (r.Height - Form.Height) / 2
            Form.Location = New Point(x, y)
        End Sub
    End Module
End Namespace
