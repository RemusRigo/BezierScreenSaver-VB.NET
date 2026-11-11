'--------------------------------------------------------------------------------------------------
' BezierSS: Main.vb: Entry point for the application
'    © 2026 Remus Rigo
'       v1.0.20261111
'--------------------------------------------------------------------------------------------------

Imports BezierSS.API

Module Main

   <STAThread>
   Sub Main(args As String())
#If Not NETFRAMEWORK Then
      Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
      Application.EnableVisualStyles()
      Application.SetCompatibleTextRenderingDefault(False)

      cfg.Load()

      Dim mode As String = If(args.Length > 0, args(0).Trim().ToLowerInvariant(), "/c")

      Select Case If(mode.Length >= 2, mode.Substring(0, 2), mode)
         Case "/a" ' legacy password change
         Case "/c"
            ShowConfig(args)
         Case "/p"
            Dim h As IntPtr = ParseHandleArg(args)
            If h <> IntPtr.Zero Then RunPreview(h)
         Case "/s"
            RunScreenSaver()
         Case Else
            RunScreenSaver()
      End Select
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' RunScreenSaver
   Private Sub RunScreenSaver()
      'Application.Run(New frmSS())
      For Each scr As Screen In Screen.AllScreens
         Dim f As New frmSS(scr.Bounds)
         f.Show()
      Next
      Application.Run()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' ShowConfig
   Private Sub ShowConfig(args As String())
      Using frm As New frmCfg()
         frm.ShowDialog()
      End Using
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' ParseHandleArg: Windows passes a window handle as "/p 1234", "/p:1234", "/c:1234", ...
   Private Function ParseHandleArg(args As String()) As IntPtr
      Dim s As String = Nothing
      Dim first As String = args(0).Trim()
      If first.Length > 2 Then
         s = first.Substring(2).TrimStart(":"c)
      ElseIf args.Length > 1 Then
         s = args(1)
      End If

      Dim v As Long
      If s IsNot Nothing AndAlso Long.TryParse(s.Trim(), v) Then Return New IntPtr(v)
      Return IntPtr.Zero
   End Function

   '-----------------------------------------------------------------------------------------------
   ' RunPreview: draw inside the small monitor in Screen Saver Settings
   Private Sub RunPreview(parentHandle As IntPtr)
      If Not IsWindow(parentHandle) Then Return
      ' the preview window is a child of another process' window, which may use a different DPI awareness:
      ' allow mixed DPI hosting so the child window can be attached to it
      Try
         SetThreadDpiHostingBehavior(DPI_HOSTING_BEHAVIOR_MIXED)
      Catch ex As EntryPointNotFoundException
         ' older than Windows 10 1803: nothing to do
      End Try
      Application.Run(New frmSS(parentHandle))
   End Sub

End Module
