Namespace Utils
    Public Module Mouse_Coordinate
        Declare Sub mouse_event Lib "user32.dll" Alias "mouse_event" (ByVal dwFlags As Int32, ByVal dx As Int32, ByVal dy As Int32, ByVal cButtons As Int32, ByVal dwExtraInfo As Int32)

        Public Sub MouseGetCoordinates_Form(ByRef X As String, ByRef Y As String)
            X = Cursor.Position.X.ToString 'Pega coordenadas X
            Y = Cursor.Position.Y.ToString 'Pega Coordenadas Y
        End Sub
    End Module
End Namespace
