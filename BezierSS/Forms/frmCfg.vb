'--------------------------------------------------------------------------------------------------
' BezierSS: frmCfg.vb
'    © 2026 Remus Rigo
'       v1.0.20261111
'--------------------------------------------------------------------------------------------------

Imports BezierSS.API

Public Class frmCfg

   Private Const SYSMENU_ABOUT_ID As Integer = &H3F0   ' low 4 bits must be 0

   '-----------------------------------------------------------------------------------------------
   ' create menu
   Protected Overrides Sub OnHandleCreated(e As EventArgs)
      MyBase.OnHandleCreated(e)
      Dim hSysMenu As IntPtr = GetSystemMenu(Me.Handle, False)
      ' Add a separator and then your custom item
      AppendMenu(hSysMenu, MF_SEPARATOR, 0, String.Empty)
      AppendMenu(hSysMenu, MF_STRING, SYSMENU_ABOUT_ID, "About...")
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' append menu to app system menu
   Protected Overrides Sub WndProc(ByRef m As Message)
      MyBase.WndProc(m)
      ' Windows uses the low 4 bits of wParam internally, so mask them off before comparing
      If m.Msg = WM_SYSCOMMAND AndAlso (m.WParam.ToInt64() And &HFFF0L) = SYSMENU_ABOUT_ID Then
         Using f As New frmAbout()
            f.ShowDialog(Me)
         End Using
      End If
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmCfg: constructor
   Public Sub New()
      InitializeComponent()

      tbTrailLength.Minimum = AppSettings.TrailLengthMin
      tbTrailLength.Maximum = AppSettings.TrailLengthMax
      tbSpeed.Minimum = AppSettings.SpeedMin
      tbSpeed.Maximum = AppSettings.SpeedMax
   End Sub


   '-----------------------------------------------------------------------------------------------
   ' Save settings
   Private Sub SaveSettings()
      cfg.TrailLength = tbTrailLength.Value
      cfg.Speed = tbSpeed.Value
      cfg.Save()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Load settings
   Private Sub LoadSettings()
      cfg.Load()
      tbTrailLength.Value = Clamp(cfg.TrailLength, tbTrailLength.Minimum, tbTrailLength.Maximum)
      tbSpeed.Value = Clamp(cfg.Speed, tbSpeed.Minimum, tbSpeed.Maximum)
   End Sub

   Private Shared Function Clamp(v As Integer, min As Integer, max As Integer) As Integer
      Return Math.Min(max, Math.Max(min, v))
   End Function

   '-----------------------------------------------------------------------------------------------
   ' frmCfg: Load
   Private Sub frmCfg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      Me.Text = appTitle & " Settings"
      LoadSettings()
      lblTrailLength.Text = "Trail length: " & tbTrailLength.Value.ToString()
      lblSpeed.Text = "Speed: " & tbSpeed.Value.ToString()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmCfg: FormClosing
   ' only save if user clicked OK, X or Esc closes the form without saving
   Private Sub frmCfg_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
      If Me.DialogResult = DialogResult.OK Then SaveSettings()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' tbTrailLength: Scroll
   Private Sub tbTrailLength_Scroll(sender As Object, e As EventArgs) Handles tbTrailLength.Scroll
      lblTrailLength.Text = "Trail length: " & tbTrailLength.Value.ToString()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' tbSpeed: Scroll
   Private Sub tbSpeed_Scroll(sender As Object, e As EventArgs) Handles tbSpeed.Scroll
      lblSpeed.Text = "Speed: " & tbSpeed.Value.ToString()
   End Sub

End Class
