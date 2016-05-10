Imports System.IO
Imports System.Windows.Forms
Module modMain

    Sub Main(ByVal sArgs() As String)
        Dim arguments As String() = Environment.GetCommandLineArgs()
        Dim exeData As Byte() = System.IO.File.ReadAllBytes(arguments(0))
        Dim exeDataString As String = System.Text.Encoding.Default.GetString(exeData)

        Dim customExeDataKeywod As String = "custom-launch-command::"
        Dim customExeDataPosition As Integer = exeDataString.LastIndexOf(customExeDataKeywod)
        If customExeDataPosition > 0 Then
            Dim customLaunchCommand = exeDataString.Substring(customExeDataPosition + customExeDataKeywod.Length + 1)
            Dim closeAfterLaunch As Boolean = exeDataString.Substring(customExeDataPosition + customExeDataKeywod.Length, 1) = "y"
            exeData = Nothing
            exeDataString = Nothing
            customExeDataKeywod = Nothing
            customExeDataPosition = Nothing
            launch(customLaunchCommand, closeAfterLaunch)
        Else
            If sArgs.Length = 0 Then
                Console.WriteLine("Enter a custom URL")

                Dim customProtocol = Console.ReadLine()
                Dim quitOnLaunch As String
                While True
                    Console.WriteLine("Close console window after launch? (y/n)")
                    quitOnLaunch = Console.ReadKey.KeyChar.ToString.ToLower
                    If quitOnLaunch = "y" Or quitOnLaunch = "n" Then
                        Exit While
                    End If
                End While


                With New SaveFileDialog
                    .Filter = "Executable File|*.exe"
                    If .ShowDialog = DialogResult.OK Then
                        System.IO.File.WriteAllBytes(.FileName, exeData)
                        Using sw = System.IO.File.AppendText(.FileName)
                            sw.Write(customExeDataKeywod & quitOnLaunch & customProtocol)
                            sw.Flush()
                            sw.Close()
                        End Using
                    End If
                End With
            ElseIf sArgs.Length = 1 Then
                launch(sArgs(0), False)
            End If
        End If
    End Sub

    Sub launch(command As String, closeAfterLaunch As Boolean)
        Process.Start(command)
        If Not closeAfterLaunch Then
            Console.WriteLine("Press any key to continue . . .")
            Console.ReadKey()
            End
        End If
    End Sub

End Module
