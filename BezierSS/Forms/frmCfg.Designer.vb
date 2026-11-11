<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCfg
   Inherits System.Windows.Forms.Form

   'Form overrides dispose to clean up the component list.
   <System.Diagnostics.DebuggerNonUserCode()> _
   Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
   <System.Diagnostics.DebuggerStepThrough()> _
   Private Sub InitializeComponent()
      btnOk = New Button()
      btnCancel = New Button()
      lblTrailLength = New Label()
      tbTrailLength = New TrackBar()
      tbSpeed = New TrackBar()
      lblSpeed = New Label()
      CType(tbTrailLength, ComponentModel.ISupportInitialize).BeginInit()
      CType(tbSpeed, ComponentModel.ISupportInitialize).BeginInit()
      SuspendLayout()
      ' 
      ' btnOk
      ' 
      btnOk.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
      btnOk.DialogResult = DialogResult.OK
      btnOk.Location = New Point(521, 175)
      btnOk.Name = "btnOk"
      btnOk.Size = New Size(35, 23)
      btnOk.TabIndex = 9
      btnOk.Text = "&Ok"
      btnOk.UseVisualStyleBackColor = True
      ' 
      ' btnCancel
      ' 
      btnCancel.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
      btnCancel.DialogResult = DialogResult.Cancel
      btnCancel.Location = New Point(562, 175)
      btnCancel.Name = "btnCancel"
      btnCancel.Size = New Size(60, 23)
      btnCancel.TabIndex = 10
      btnCancel.Text = "&Cancel"
      btnCancel.UseVisualStyleBackColor = True
      ' 
      ' lblTrailLength
      ' 
      lblTrailLength.AutoSize = True
      lblTrailLength.Location = New Point(1, 44)
      lblTrailLength.Name = "lblTrailLength"
      lblTrailLength.Size = New Size(66, 15)
      lblTrailLength.TabIndex = 11
      lblTrailLength.Text = "Trail length"
      ' 
      ' tbTrailLength
      ' 
      tbTrailLength.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
      tbTrailLength.Location = New Point(112, 44)
      tbTrailLength.Name = "tbTrailLength"
      tbTrailLength.Size = New Size(510, 45)
      tbTrailLength.TabIndex = 12
      ' 
      ' tbSpeed
      ' 
      tbSpeed.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
      tbSpeed.Location = New Point(112, 78)
      tbSpeed.Name = "tbSpeed"
      tbSpeed.Size = New Size(510, 45)
      tbSpeed.TabIndex = 14
      ' 
      ' lblSpeed
      ' 
      lblSpeed.AutoSize = True
      lblSpeed.Location = New Point(1, 78)
      lblSpeed.Name = "lblSpeed"
      lblSpeed.Size = New Size(39, 15)
      lblSpeed.TabIndex = 13
      lblSpeed.Text = "Speed"
      ' 
      ' frmCfg
      ' 
      AcceptButton = btnOk
      AutoScaleDimensions = New SizeF(7F, 15F)
      AutoScaleMode = AutoScaleMode.Font
      CancelButton = btnCancel
      ClientSize = New Size(624, 201)
      Controls.Add(tbSpeed)
      Controls.Add(lblSpeed)
      Controls.Add(tbTrailLength)
      Controls.Add(lblTrailLength)
      Controls.Add(btnCancel)
      Controls.Add(btnOk)
      FormBorderStyle = FormBorderStyle.FixedDialog
      MaximizeBox = False
      MinimizeBox = False
      Name = "frmCfg"
      Text = "Settings"
      CType(tbTrailLength, ComponentModel.ISupportInitialize).EndInit()
      CType(tbSpeed, ComponentModel.ISupportInitialize).EndInit()
      ResumeLayout(False)
      PerformLayout()
   End Sub
   Friend WithEvents btnOk As Button
   Friend WithEvents btnCancel As Button
   Friend WithEvents lblTrailLength As Label
   Friend WithEvents tbTrailLength As TrackBar
   Friend WithEvents tbSpeed As TrackBar
   Friend WithEvents lblSpeed As Label
End Class
