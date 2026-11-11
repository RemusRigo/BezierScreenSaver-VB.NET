<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSS
   Inherits System.Windows.Forms.Form

   'Form overrides dispose to clean up the component list.
   <System.Diagnostics.DebuggerNonUserCode()>
   Protected Overrides Sub Dispose(disposing As Boolean)
      Try
         If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
         End If
      Finally
         MyBase.Dispose(disposing)
      End Try
   End Sub

   'Required by the Windows Form Designer
   Private components As System.ComponentModel.IContainer

   'NOTE: The following procedure is required by the Windows Form Designer
   'It can be modified using the Windows Form Designer.
   'Do not modify it using the code editor.
   <System.Diagnostics.DebuggerStepThrough()>
   Private Sub InitializeComponent()
      components = New ComponentModel.Container()
      tmr = New Timer(components)
      SuspendLayout()
      ' 
      ' tmr
      ' 
      tmr.Interval = 20
      ' 
      ' frmSS
      ' 
      AutoScaleDimensions = New SizeF(7F, 15F)
      AutoScaleMode = AutoScaleMode.Font
      ClientSize = New Size(800, 450)
      Name = "frmSS"
      Text = "ScreenSaver"
      ResumeLayout(False)
   End Sub

   Friend WithEvents tmr As Timer

End Class
