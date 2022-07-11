Imports System.Runtime.InteropServices
Namespace Utils
    Public Module Mouse_Objeto
        <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
        Private Sub ReleaseCapture()
        End Sub

        <DllImport("user32.DLL", EntryPoint:="SendMessage")>
        Private Sub SendMessage(ByVal hWnd As System.IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer)
        End Sub

        Private LOCAT As Point

        Public Sub MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs)
            If e.Button = Windows.Forms.MouseButtons.Left Then
                LOCAT = e.Location
            End If
        End Sub

        Public Sub MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs, ByVal Form As Object)
            If e.Button = Windows.Forms.MouseButtons.Left Then
                Form.Location += e.Location - LOCAT
            End If
        End Sub

        Public Sub MouseMoveUnick(sender As Object, e As System.Windows.Forms.MouseEventArgs, ByVal Form As Object)
            ReleaseCapture()
            SendMessage(Form.Handle, &H112&, &HF012&, 0)
        End Sub
    End Module
End Namespace
