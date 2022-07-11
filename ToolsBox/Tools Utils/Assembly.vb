Imports System.IO
Imports System.Threading
Imports System.CodeDom.Compiler
Namespace Utils
    Public Class Assembly
        Public Sub InsertDescription(ByVal Title As String,
                                     ByVal Description As String,
                                     ByVal Company As String,
                                     ByVal Product As String,
                                     ByVal Copyright As String,
                                     ByVal Trademark As String,
                                     ByVal Version As String,
                                     ByVal FileVersion As String,
                                     ByVal PathDestiny As String)

            Try
                If Not System.IO.Directory.Exists(My.Computer.FileSystem.CurrentDirectory & "\Jobs") Then
                    My.Computer.FileSystem.CreateDirectory("Jobs")
                End If

                Dim Source As String = My.Resources.RHDiscription
                Dim VersionMirror = New Collections.Generic.Dictionary(Of String, String)
                VersionMirror.Add("CompilerVersion", "v4.0")
                Dim Compiler As VBCodeProvider = New VBCodeProvider
                Dim cResults As CompilerResults
                Dim ConstructionRules As New CompilerParameters()
                With ConstructionRules
                    ConstructionRules.GenerateExecutable = True
                    ConstructionRules.OutputAssembly = My.Computer.FileSystem.CurrentDirectory & "\Jobs\Mirror.exe"
                    ConstructionRules.WarningLevel = 3
                    ConstructionRules.ReferencedAssemblies.Add("System.dll")
                    ConstructionRules.ReferencedAssemblies.Add("System.Linq.dll")
                    ConstructionRules.ReferencedAssemblies.Add("System.Threading.Tasks.dll")
                    ConstructionRules.ReferencedAssemblies.Add("System.Windows.Forms.dll")
                    ConstructionRules.ReferencedAssemblies.Add("System.Core.dll")
                    ConstructionRules.ReferencedAssemblies.Add("System.Data.dll")
                    ConstructionRules.ReferencedAssemblies.Add("mscorlib.dll")
                    ConstructionRules.CompilerOptions = "/t:winexe"
                    ConstructionRules.GenerateInMemory = False
                End With

                Source = Source.Replace("*Title*", Title)
                Source = Source.Replace("*Description*", Description)
                Source = Source.Replace("*Company*", Company)
                Source = Source.Replace("*Product*", Product)
                Source = Source.Replace("*Copyright*", Copyright)
                Source = Source.Replace("*Trademark*", Trademark)

                Dim V As String = Version.Replace(",", ".")
                Dim Ver As String() = V.Split(".")
                Dim F As String = FileVersion.Replace(",", ".")
                Dim FVer As String() = F.Split(".")

                If Ver.Length < 4 Then
                    Ver = {"0", "0", "0", "0"}
                End If

                If FVer.Length < 4 Then
                    FVer = {"0", "0", "0", "0"}
                End If

                Source = Source.Replace("*version*", Ver(0) & "." & Ver(1) & "." & Ver(2) & "." & Ver(3))
                Source = Source.Replace("*fversion*", FVer(0) & "." & FVer(1) & "." & FVer(2) & "." & FVer(3))
                cResults = Compiler.CompileAssemblyFromSource(ConstructionRules, Source)

                File.WriteAllBytes(My.Computer.FileSystem.CurrentDirectory & "\Jobs\Resource_Hack.exe", My.Resources.Resource_Hack)
                Dim StubFile As String = My.Computer.FileSystem.CurrentDirectory & "\Jobs\Stub_Progress.exe"
                Dim Resource_Hack As String = My.Computer.FileSystem.CurrentDirectory & "\Jobs\Resource_Hack.exe"
                Dim ResFile As String = My.Computer.FileSystem.CurrentDirectory & "\Jobs\Mirror.res"
                Dim MirrorFile As String = My.Computer.FileSystem.CurrentDirectory & "\Jobs\Mirror.exe"

                Thread.Sleep(100)
                Shell(Resource_Hack & " -extract " & MirrorFile & "," & ResFile & ",,,", AppWinStyle.Hide, True, -1)
                Thread.Sleep(100)
                Shell(Resource_Hack & " -delete " & PathDestiny & "," & PathDestiny & ",VERSIONINFO,,", AppWinStyle.Hide, True, -1)
                Thread.Sleep(100)
                Shell(Resource_Hack & " -addoverwrite " & PathDestiny & "," & PathDestiny & "," & ResFile & ",,,", AppWinStyle.Hide, True, -1)
                Thread.Sleep(100)

                If System.IO.Directory.Exists(My.Computer.FileSystem.CurrentDirectory & "\Jobs") Then
                    IO.Directory.Delete(My.Computer.FileSystem.CurrentDirectory & "\Jobs", True)
                End If

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "Information")
            End Try
        End Sub
        Public Sub CloneAssembly(ByVal FilePathOriginal As String, ByVal FilePathDestiny As String)
            Try
                If Not System.IO.Directory.Exists(My.Computer.FileSystem.CurrentDirectory & "\Jobs") Then
                    My.Computer.FileSystem.CreateDirectory("Jobs")
                End If

                Threading.Thread.Sleep(100)
                File.WriteAllBytes(My.Computer.FileSystem.CurrentDirectory & "\Jobs\Resource_Hack.exe", My.Resources.Resource_Hack)
                Threading.Thread.Sleep(100)
                Dim Resource_Hack As String = My.Computer.FileSystem.CurrentDirectory & "\Jobs\Resource_Hack.exe"
                Dim ResFile As String = My.Computer.FileSystem.CurrentDirectory & "\Jobs\Mirror.res"

                Thread.Sleep(100)
                Shell(Resource_Hack & " -extract " & FilePathOriginal & "," & ResFile & ",,,", AppWinStyle.Hide, True, -1)
                Thread.Sleep(100)
                Shell(Resource_Hack & " -delete " & FilePathDestiny & "," & FilePathDestiny & ",,,", AppWinStyle.Hide, True, -1)
                Thread.Sleep(100)
                Shell(Resource_Hack & " -addoverwrite " & FilePathDestiny & "," & FilePathDestiny & "," & ResFile & ",,,", AppWinStyle.Hide, True, -1)
                Thread.Sleep(100)

                If System.IO.Directory.Exists(My.Computer.FileSystem.CurrentDirectory & "\Jobs") Then
                    IO.Directory.Delete(My.Computer.FileSystem.CurrentDirectory & "\Jobs", True)
                End If

            Catch EX As Exception
                MsgBox(EX.ToString, MsgBoxStyle.Critical, "Information")
            End Try
        End Sub
        Public Sub ReplaceIcon(ByVal FilePathOriginal As String, ByVal FilePathDestiny As String)
            Try
                If Not System.IO.Directory.Exists(My.Computer.FileSystem.CurrentDirectory & "\Jobs") Then
                    My.Computer.FileSystem.CreateDirectory("Jobs")
                End If

                Threading.Thread.Sleep(100)
                File.WriteAllBytes(My.Computer.FileSystem.CurrentDirectory & "\Jobs\Resource_Hack.exe", My.Resources.Resource_Hack)
                Threading.Thread.Sleep(100)
                Dim Resource_Hack As String = My.Computer.FileSystem.CurrentDirectory & "\Jobs\Resource_Hack.exe"
                Dim ResFile As String = My.Computer.FileSystem.CurrentDirectory & "\Jobs\Mirror.res"

                Thread.Sleep(100)
                Shell(Resource_Hack & " -extract " & FilePathOriginal & "," & ResFile & ",ICONGROUP,,", AppWinStyle.Hide, True, -1)
                Thread.Sleep(100)
                Shell(Resource_Hack & " -delete " & FilePathDestiny & "," & FilePathDestiny & ",ICONGROUP,,", AppWinStyle.Hide, True, -1)
                Thread.Sleep(100)
                Shell(Resource_Hack & " -addoverwrite " & FilePathDestiny & "," & FilePathDestiny & "," & ResFile & ",,,", AppWinStyle.Hide, True, -1)
                Thread.Sleep(100)

                If System.IO.Directory.Exists(My.Computer.FileSystem.CurrentDirectory & "\Jobs") Then
                    IO.Directory.Delete(My.Computer.FileSystem.CurrentDirectory & "\Jobs", True)
                End If

            Catch EX As Exception
                MsgBox(EX.ToString, MsgBoxStyle.Critical, "Information")
            End Try
        End Sub
        Public Sub ChangeIcon(ByVal FilePath As String, ByVal Icon As String)
            Try
                If Not System.IO.Directory.Exists(My.Computer.FileSystem.CurrentDirectory & "\Jobs") Then
                    My.Computer.FileSystem.CreateDirectory("Jobs")
                End If

                Threading.Thread.Sleep(100)
                File.WriteAllBytes(My.Computer.FileSystem.CurrentDirectory & "\Jobs\Resource_Hack.exe", My.Resources.Resource_Hack)
                Threading.Thread.Sleep(100)
                Dim Resource_Hack As String = My.Computer.FileSystem.CurrentDirectory & "\Jobs\Resource_Hack.exe"
                Dim ResFile As String = My.Computer.FileSystem.CurrentDirectory & "\Jobs\Mirror.res"

                If Icon.Contains(".ico") Then
                    Thread.Sleep(100)
                    Shell(Resource_Hack & " -delete " & FilePath & "," & FilePath & ",ICONGROUP,,", AppWinStyle.Hide, True, -1)
                    Thread.Sleep(100)
                    Shell(Resource_Hack & " -addoverwrite " & FilePath & "," & FilePath & ", " & Icon & ",ICONGROUP,MAINICON,0", AppWinStyle.Hide, True, -1)
                    Thread.Sleep(100)
                ElseIf Icon.Contains(".exe") Then
                    Thread.Sleep(100)
                    Shell(Resource_Hack & " -extract " & Icon & "," & ResFile & ",ICONGROUP,,", AppWinStyle.Hide, True, -1)
                    Thread.Sleep(100)
                    Shell(Resource_Hack & " -delete " & FilePath & "," & FilePath & ",ICONGROUP,,", AppWinStyle.Hide, True, -1)
                    Thread.Sleep(100)
                    Shell(Resource_Hack & " -addoverwrite " & FilePath & "," & FilePath & "," & ResFile & ",,,", AppWinStyle.Hide, True, -1)
                    Thread.Sleep(100)
                End If

                If System.IO.Directory.Exists(My.Computer.FileSystem.CurrentDirectory & "\Jobs") Then
                    IO.Directory.Delete(My.Computer.FileSystem.CurrentDirectory & "\Jobs", True)
                End If

            Catch EX As Exception
                MsgBox(EX.ToString, MsgBoxStyle.Critical, "Information")
            End Try
        End Sub
        Public Sub CloneDescription(ByVal FilePathOriginal As String, ByVal FilePathDestiny As String)
            Try
                If Not System.IO.Directory.Exists(My.Computer.FileSystem.CurrentDirectory & "\Jobs") Then
                    My.Computer.FileSystem.CreateDirectory("Jobs")
                End If

                Threading.Thread.Sleep(100)
                File.WriteAllBytes(My.Computer.FileSystem.CurrentDirectory & "\Jobs\Resource_Hack.exe", My.Resources.Resource_Hack)
                Threading.Thread.Sleep(100)
                Dim Resource_Hack As String = My.Computer.FileSystem.CurrentDirectory & "\Jobs\Resource_Hack.exe"
                Dim ResFile As String = My.Computer.FileSystem.CurrentDirectory & "\Jobs\Mirror.res"

                Thread.Sleep(100)
                Shell(Resource_Hack & " -extract " & FilePathOriginal & "," & ResFile & ",VERSIONINFO,,", AppWinStyle.Hide, True, -1)
                Thread.Sleep(100)
                Shell(Resource_Hack & " -delete " & FilePathDestiny & "," & FilePathDestiny & ",VERSIONINFO,,", AppWinStyle.Hide, True, -1)
                Thread.Sleep(100)
                Shell(Resource_Hack & " -addoverwrite " & FilePathDestiny & "," & FilePathDestiny & "," & ResFile & ",,,", AppWinStyle.Hide, True, -1)
                Thread.Sleep(100)

                If System.IO.Directory.Exists(My.Computer.FileSystem.CurrentDirectory & "\Jobs") Then
                    IO.Directory.Delete(My.Computer.FileSystem.CurrentDirectory & "\Jobs", True)
                End If

            Catch EX As Exception
                MsgBox(EX.ToString, MsgBoxStyle.Critical, "Information")
            End Try
        End Sub
        Public Sub DeleteAssembly(ByVal FilePath As String)
            Try
                If Not System.IO.Directory.Exists(My.Computer.FileSystem.CurrentDirectory & "\Jobs") Then
                    My.Computer.FileSystem.CreateDirectory("Jobs")
                End If

                Threading.Thread.Sleep(100)
                File.WriteAllBytes(My.Computer.FileSystem.CurrentDirectory & "\Jobs\Resource_Hack.exe", My.Resources.Resource_Hack)
                Threading.Thread.Sleep(100)
                Dim Resource_Hack As String = My.Computer.FileSystem.CurrentDirectory & "\Jobs\Resource_Hack.exe"

                Thread.Sleep(100)
                Shell(Resource_Hack & " -delete " & FilePath & "," & FilePath & ",,,", AppWinStyle.Hide, True, -1)
                Thread.Sleep(100)

                If System.IO.Directory.Exists(My.Computer.FileSystem.CurrentDirectory & "\Jobs") Then
                    IO.Directory.Delete(My.Computer.FileSystem.CurrentDirectory & "\Jobs", True)
                End If

            Catch EX As Exception
                MsgBox(EX.ToString, MsgBoxStyle.Critical, "Information")
            End Try
        End Sub
        Public Sub DeleteIcon(ByVal FilePath As String)
            Try
                If Not System.IO.Directory.Exists(My.Computer.FileSystem.CurrentDirectory & "\Jobs") Then
                    My.Computer.FileSystem.CreateDirectory("Jobs")
                End If

                Threading.Thread.Sleep(100)
                File.WriteAllBytes(My.Computer.FileSystem.CurrentDirectory & "\Jobs\Resource_Hack.exe", My.Resources.Resource_Hack)
                Threading.Thread.Sleep(100)
                Dim Resource_Hack As String = My.Computer.FileSystem.CurrentDirectory & "\Jobs\Resource_Hack.exe"

                Thread.Sleep(100)
                Shell(Resource_Hack & " -delete " & FilePath & "," & FilePath & ",ICONGROUP,,", AppWinStyle.Hide, True, -1)
                Thread.Sleep(100)

                If System.IO.Directory.Exists(My.Computer.FileSystem.CurrentDirectory & "\Jobs") Then
                    IO.Directory.Delete(My.Computer.FileSystem.CurrentDirectory & "\Jobs", True)
                End If

            Catch EX As Exception
                MsgBox(EX.ToString, MsgBoxStyle.Critical, "Information")
            End Try
        End Sub
        Public Sub DeleteDescription(ByVal FilePath As String)
            Try
                If Not System.IO.Directory.Exists(My.Computer.FileSystem.CurrentDirectory & "\Jobs") Then
                    My.Computer.FileSystem.CreateDirectory("Jobs")
                End If

                Threading.Thread.Sleep(100)
                File.WriteAllBytes(My.Computer.FileSystem.CurrentDirectory & "\Jobs\Resource_Hack.exe", My.Resources.Resource_Hack)
                Threading.Thread.Sleep(100)
                Dim Resource_Hack As String = My.Computer.FileSystem.CurrentDirectory & "\Jobs\Resource_Hack.exe"

                Thread.Sleep(100)
                Shell(Resource_Hack & " -delete " & FilePath & "," & FilePath & ",VERSIONINFO,,", AppWinStyle.Hide, True, -1)
                Thread.Sleep(100)

                If System.IO.Directory.Exists(My.Computer.FileSystem.CurrentDirectory & "\Jobs") Then
                    IO.Directory.Delete(My.Computer.FileSystem.CurrentDirectory & "\Jobs", True)
                End If

            Catch EX As Exception
                MsgBox(EX.ToString, MsgBoxStyle.Critical, "Information")
            End Try
        End Sub
    End Class
End Namespace