'--------------------------------------------------------------------------------------------------
' BezierSS: frmSS.vb
'    © 2026 Remus Rigo
'       v1.0.20261111
'--------------------------------------------------------------------------------------------------

Imports BezierSS.API

Public Class frmSS

   Public Sub New()
      Me.New(Screen.PrimaryScreen.Bounds)
   End Sub

   Public Sub New(bounds As Rectangle)
      InitializeComponent()
      _screenBounds = bounds
      ' border style and scaling must be set BEFORE the bounds: changing FormBorderStyle later keeps the
      ' ClientSize and shrinks the window, leaving an uncovered strip at the bottom/right of the screen
      Me.AutoScaleMode = AutoScaleMode.None
      Me.FormBorderStyle = FormBorderStyle.None
      Me.StartPosition = FormStartPosition.Manual
      Me.WindowState = FormWindowState.Normal
      Me.ShowInTaskbar = False                     ' set before the handle exists: changing it later recreates the window
      Me.Bounds = bounds
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' Preview constructor: draw inside the small monitor of the Screen Saver Settings dialog
   Public Sub New(previewHandle As IntPtr)
      Me.New(PreviewRect(previewHandle))
      _previewHandle = previewHandle
   End Sub

   Private Shared Function PreviewRect(hWnd As IntPtr) As Rectangle
      Dim r As RECT
      GetClientRect(hWnd, r)
      Return New Rectangle(0, 0, r.Right - r.Left, r.Bottom - r.Top)
   End Function

   Private mouseStart As Point? = Nothing         ' Nothing until the first MouseMove

   Private ReadOnly _screenBounds As Rectangle    ' the monitor this form must cover (preview: the parent's client area)
   Private ReadOnly _previewHandle As IntPtr = IntPtr.Zero

   Private ReadOnly Property IsPreview As Boolean
      Get
         Return _previewHandle <> IntPtr.Zero
      End Get
   End Property

   '-----------------------------------------------------------------------------------------------
   ' CreateParams: in preview mode the window is created directly as a child of the preview monitor
   Protected Overrides ReadOnly Property CreateParams As CreateParams
      Get
         Dim cp As CreateParams = MyBase.CreateParams
         If IsPreview Then
            cp.Parent = _previewHandle
            cp.Style = (cp.Style And Not WS_POPUP) Or WS_CHILD
            cp.X = 0
            cp.Y = 0
            cp.Width = _screenBounds.Width
            cp.Height = _screenBounds.Height
         End If
         Return cp
      End Get
   End Property

   Private pts(3) As Point                        ' 4 points of the Bezier curve (start, control1, control2, end)
   Private velocity(3) As Point                   ' velocity of each point (dx,dy)
   Private rnd As New Random()

   ' History of previous curves (oldest first)
   'Private Const TRAIL_LENGTH As Integer = 35     ' number of previous curves to keep in the history
   Private history As New List(Of Point())
   Private hue As Double                          ' current hue, 0 to 360
   Private hueHistory As New List(Of Double)      ' one hue per saved curve

   Private Sub InitializeSSForm()
      With Me
         .BackColor = Color.Black
         .DoubleBuffered = True
         .KeyPreview = True
      End With

      If IsPreview Then
         ' WinForms may park the child window under its own hidden window: attach it to the preview monitor
         ' (no TopMost and no hidden cursor in preview mode)
         SetParent(Me.Handle, _previewHandle)
         Me.Bounds = PreviewRect(_previewHandle)
         Return
      End If

#If Not DEBUG Then
      Me.TopMost = True
#End If
      Me.Bounds = _screenBounds                  ' re-apply in case anything resized the window
      Cursor.Hide()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' InitializeData
   Public Sub InitializeData()
      Dim w As Integer = Me.ClientSize.Width
      Dim h As Integer = Me.ClientSize.Height

      For i As Integer = 0 To 3
         ' random start position and random speed (never 0)
         pts(i) = New Point(rnd.Next(w), rnd.Next(h))
         velocity(i) = New Point(RandomSpeed(rnd), RandomSpeed(rnd))
      Next

      hue = rnd.Next(360)
      hueHistory.Clear()

      tmr.Enabled = True
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' tmr: Tick
   ' update the points and request a repaint
   Private Sub tmr_Tick(sender As Object, e As EventArgs) Handles tmr.Tick
      ' preview: exit when the Screen Saver Settings dialog closes or switches to another screensaver
      If IsPreview AndAlso Not IsWindow(_previewHandle) Then
         Me.Close()
         Return
      End If

      Dim w As Integer = Me.ClientSize.Width
      Dim h As Integer = Me.ClientSize.Height

      For i As Integer = 0 To 3
         pts(i).X += velocity(i).X
         pts(i).Y += velocity(i).Y

         ' bounce on left/right edges
         If pts(i).X < 0 Then
            pts(i).X = 0
            velocity(i).X = -velocity(i).X
         ElseIf pts(i).X > w - 1 Then
            pts(i).X = w - 1
            velocity(i).X = -velocity(i).X
         End If

         ' bounce on top/bottom edges
         If pts(i).Y < 0 Then
            pts(i).Y = 0
            velocity(i).Y = -velocity(i).Y
         ElseIf pts(i).Y > h - 1 Then
            pts(i).Y = h - 1
            velocity(i).Y = -velocity(i).Y
         End If
      Next

      history.Add(DirectCast(pts.Clone(), Point()))        'save a copy of the current points in the history
      hue = (hue + 2) Mod 360                              'increment hue for the next curve
      hueHistory.Add(hue)                                  'save the current hue in the history
      If history.Count > cfg.TrailLength Then
         history.RemoveAt(0)                               'remove the oldest curve from the history
         hueHistory.RemoveAt(0)                            'remove the oldest hue from the history
      End If

      Me.Invalidate()                                      'request a repaint of the form
   End Sub


   '-----------------------------------------------------------------------------------------------
   ' frmSS: Paint
   Private Sub frmSS_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
      e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

      For i As Integer = 0 To history.Count - 1
         Dim c() As Point = history(i)                     'get the points of the i-th curve from the history


         ' brightness grows from about 10% (oldest) to 100% (newest)
         'Dim level As Integer = 25 + (230 * i) \ TRAIL_LENGTH
         'If level > 255 Then level = 255

         'Using p As New Pen(Color.FromArgb(0, level, 0), 1)
         '   e.Graphics.DrawBezier(p, c(0), c(1), c(2), c(3))
         'End Using

         ' brightness grows from about 10% (oldest) to 100% (newest)
         Dim bright As Double = 0.1 + 0.9 * i / cfg.TrailLength
         If bright > 1 Then bright = 1

         Using p As New Pen(ColorFromHsv(hueHistory(i), 1, bright), 1)
            e.Graphics.DrawBezier(p, c(0), c(1), c(2), c(3))
         End Using
      Next
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: Load
   Private Sub frmSS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      InitializeSSForm()
   End Sub
   '-----------------------------------------------------------------------------------------------
   ' frmSS: Shown
   Private Sub frmSS_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
      InitializeData()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: FormClosed
   Private Sub frmSS_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
      tmr.Enabled = False
      Cursor.Show()
      Application.Exit()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: DpiChanged
   ' One form per monitor. With mixed scaling (e.g. 100% + 150%) WinForms would rescale/move the window
   ' and leave gaps. Cancel that and keep covering exactly this form's monitor.
   Private Sub frmSS_DpiChanged(sender As Object, e As DpiChangedEventArgs) Handles MyBase.DpiChanged
      If IsPreview Then Return
      e.Cancel = True
      Me.Bounds = _screenBounds
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: KeyDown
   Private Sub frmSS_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
      If IsPreview Then Return
      Me.Close()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: MouseDown
   Private Sub frmSS_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
      If IsPreview Then Return
      Me.Close()
   End Sub

   '-----------------------------------------------------------------------------------------------
   ' frmSS: MouseMove
   Private Sub frmSS_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
      If IsPreview Then Return

      ' remember the first position
      If Not mouseStart.HasValue Then
         mouseStart = e.Location
         Return
      End If

      ' exit if the mouse moved more than 4 pixels
      Dim start As Point = mouseStart.Value
      If Math.Abs(e.X - start.X) > 4 OrElse Math.Abs(e.Y - start.Y) > 4 Then
         Me.Close()
      End If
   End Sub


End Class
