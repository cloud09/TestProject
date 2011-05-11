<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ModuleRelease
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ModuleRelease))
        Me.chkFileList = New System.Windows.Forms.CheckedListBox
        Me.bth1AllOn = New System.Windows.Forms.Button
        Me.btn1AllOff = New System.Windows.Forms.Button
        Me.bth2AllOff = New System.Windows.Forms.Button
        Me.bth2AllOn = New System.Windows.Forms.Button
        Me.chkTargetList = New System.Windows.Forms.CheckedListBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblText1 = New System.Windows.Forms.Label
        Me.lblCount1 = New System.Windows.Forms.Label
        Me.btnDir = New System.Windows.Forms.Button
        Me.lblPath = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmbTargetGrp = New System.Windows.Forms.ComboBox
        Me.lblText2 = New System.Windows.Forms.Label
        Me.lblCount2 = New System.Windows.Forms.Label
        Me.lblStatus = New System.Windows.Forms.Label
        Me.txtLog = New System.Windows.Forms.TextBox
        Me.pBar = New System.Windows.Forms.ProgressBar
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnCopy = New System.Windows.Forms.Button
        Me.bgWorker = New System.ComponentModel.BackgroundWorker
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkFileList
        '
        Me.chkFileList.CheckOnClick = True
        Me.chkFileList.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkFileList.Location = New System.Drawing.Point(10, 74)
        Me.chkFileList.Name = "chkFileList"
        Me.chkFileList.ScrollAlwaysVisible = True
        Me.chkFileList.Size = New System.Drawing.Size(430, 298)
        Me.chkFileList.TabIndex = 6
        '
        'bth1AllOn
        '
        Me.bth1AllOn.Enabled = False
        Me.bth1AllOn.Location = New System.Drawing.Point(10, 48)
        Me.bth1AllOn.Name = "bth1AllOn"
        Me.bth1AllOn.Size = New System.Drawing.Size(72, 20)
        Me.bth1AllOn.TabIndex = 2
        Me.bth1AllOn.Text = "全て選択"
        Me.bth1AllOn.UseVisualStyleBackColor = True
        '
        'btn1AllOff
        '
        Me.btn1AllOff.Enabled = False
        Me.btn1AllOff.Location = New System.Drawing.Point(88, 48)
        Me.btn1AllOff.Name = "btn1AllOff"
        Me.btn1AllOff.Size = New System.Drawing.Size(72, 20)
        Me.btn1AllOff.TabIndex = 3
        Me.btn1AllOff.Text = "選択解除"
        Me.btn1AllOff.UseVisualStyleBackColor = True
        '
        'bth2AllOff
        '
        Me.bth2AllOff.Enabled = False
        Me.bth2AllOff.Location = New System.Drawing.Point(88, 44)
        Me.bth2AllOff.Name = "bth2AllOff"
        Me.bth2AllOff.Size = New System.Drawing.Size(72, 20)
        Me.bth2AllOff.TabIndex = 2
        Me.bth2AllOff.Text = "選択解除"
        Me.bth2AllOff.UseVisualStyleBackColor = True
        '
        'bth2AllOn
        '
        Me.bth2AllOn.Enabled = False
        Me.bth2AllOn.Location = New System.Drawing.Point(10, 44)
        Me.bth2AllOn.Name = "bth2AllOn"
        Me.bth2AllOn.Size = New System.Drawing.Size(72, 20)
        Me.bth2AllOn.TabIndex = 1
        Me.bth2AllOn.Text = "全て選択"
        Me.bth2AllOn.UseVisualStyleBackColor = True
        '
        'chkTargetList
        '
        Me.chkTargetList.CheckOnClick = True
        Me.chkTargetList.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkTargetList.Location = New System.Drawing.Point(10, 73)
        Me.chkTargetList.Name = "chkTargetList"
        Me.chkTargetList.ScrollAlwaysVisible = True
        Me.chkTargetList.Size = New System.Drawing.Size(430, 298)
        Me.chkTargetList.TabIndex = 5
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblText1)
        Me.GroupBox1.Controls.Add(Me.lblCount1)
        Me.GroupBox1.Controls.Add(Me.chkFileList)
        Me.GroupBox1.Controls.Add(Me.btnDir)
        Me.GroupBox1.Controls.Add(Me.bth1AllOn)
        Me.GroupBox1.Controls.Add(Me.btn1AllOff)
        Me.GroupBox1.Controls.Add(Me.lblPath)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(455, 385)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "配布ファイル選択"
        '
        'lblText1
        '
        Me.lblText1.AutoSize = True
        Me.lblText1.Location = New System.Drawing.Point(352, 52)
        Me.lblText1.Name = "lblText1"
        Me.lblText1.Size = New System.Drawing.Size(87, 12)
        Me.lblText1.TabIndex = 5
        Me.lblText1.Text = "件選択されました"
        '
        'lblCount1
        '
        Me.lblCount1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCount1.Location = New System.Drawing.Point(316, 52)
        Me.lblCount1.Name = "lblCount1"
        Me.lblCount1.Size = New System.Drawing.Size(40, 12)
        Me.lblCount1.TabIndex = 4
        Me.lblCount1.Text = "0"
        Me.lblCount1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnDir
        '
        Me.btnDir.Image = Global.KobelcoSystem.My.Resources.Resources.folder
        Me.btnDir.Location = New System.Drawing.Point(411, 21)
        Me.btnDir.Name = "btnDir"
        Me.btnDir.Size = New System.Drawing.Size(30, 20)
        Me.btnDir.TabIndex = 1
        Me.btnDir.UseVisualStyleBackColor = True
        '
        'lblPath
        '
        Me.lblPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblPath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPath.Location = New System.Drawing.Point(11, 21)
        Me.lblPath.Name = "lblPath"
        Me.lblPath.Size = New System.Drawing.Size(395, 20)
        Me.lblPath.TabIndex = 0
        Me.lblPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmbTargetGrp)
        Me.GroupBox2.Controls.Add(Me.lblText2)
        Me.GroupBox2.Controls.Add(Me.lblCount2)
        Me.GroupBox2.Controls.Add(Me.chkTargetList)
        Me.GroupBox2.Controls.Add(Me.bth2AllOn)
        Me.GroupBox2.Controls.Add(Me.bth2AllOff)
        Me.GroupBox2.Location = New System.Drawing.Point(471, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(455, 385)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "配布先選択"
        '
        'cmbTargetGrp
        '
        Me.cmbTargetGrp.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cmbTargetGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTargetGrp.FormattingEnabled = True
        Me.cmbTargetGrp.Location = New System.Drawing.Point(10, 21)
        Me.cmbTargetGrp.MaxDropDownItems = 10
        Me.cmbTargetGrp.Name = "cmbTargetGrp"
        Me.cmbTargetGrp.Size = New System.Drawing.Size(430, 20)
        Me.cmbTargetGrp.Sorted = True
        Me.cmbTargetGrp.TabIndex = 0
        '
        'lblText2
        '
        Me.lblText2.AutoSize = True
        Me.lblText2.Location = New System.Drawing.Point(350, 48)
        Me.lblText2.Name = "lblText2"
        Me.lblText2.Size = New System.Drawing.Size(87, 12)
        Me.lblText2.TabIndex = 4
        Me.lblText2.Text = "件選択されました"
        '
        'lblCount2
        '
        Me.lblCount2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCount2.Location = New System.Drawing.Point(311, 48)
        Me.lblCount2.Name = "lblCount2"
        Me.lblCount2.Size = New System.Drawing.Size(40, 12)
        Me.lblCount2.TabIndex = 3
        Me.lblCount2.Text = "0"
        Me.lblCount2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatus.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(22, 409)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(150, 100)
        Me.lblStatus.TabIndex = 2
        Me.lblStatus.Text = "準備中"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtLog
        '
        Me.txtLog.BackColor = System.Drawing.Color.White
        Me.txtLog.Location = New System.Drawing.Point(178, 409)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ReadOnly = True
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtLog.Size = New System.Drawing.Size(635, 100)
        Me.txtLog.TabIndex = 5
        Me.txtLog.TabStop = False
        Me.txtLog.WordWrap = False
        '
        'pBar
        '
        Me.pBar.Location = New System.Drawing.Point(23, 515)
        Me.pBar.Name = "pBar"
        Me.pBar.Size = New System.Drawing.Size(790, 30)
        Me.pBar.TabIndex = 7
        '
        'btnCancel
        '
        Me.btnCancel.AutoSize = True
        Me.btnCancel.Image = Global.KobelcoSystem.My.Resources.Resources.cancel
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(823, 458)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(101, 33)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "キャンセル"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Image = Global.KobelcoSystem.My.Resources.Resources._exit
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(823, 515)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(101, 30)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "終了"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnCopy
        '
        Me.btnCopy.AutoSize = True
        Me.btnCopy.Image = Global.KobelcoSystem.My.Resources.Resources.copy
        Me.btnCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCopy.Location = New System.Drawing.Point(823, 419)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(101, 33)
        Me.btnCopy.TabIndex = 3
        Me.btnCopy.Text = "コピー"
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'bgWorker
        '
        '
        'ModuleRelease
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(936, 566)
        Me.Controls.Add(Me.pBar)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnCopy)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(944, 600)
        Me.MinimumSize = New System.Drawing.Size(944, 600)
        Me.Name = "ModuleRelease"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ModuleRelease"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkFileList As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnDir As System.Windows.Forms.Button
    Friend WithEvents bth1AllOn As System.Windows.Forms.Button
    Friend WithEvents btn1AllOff As System.Windows.Forms.Button
    Friend WithEvents bth2AllOff As System.Windows.Forms.Button
    Friend WithEvents bth2AllOn As System.Windows.Forms.Button
    Friend WithEvents chkTargetList As System.Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents lblText1 As System.Windows.Forms.Label
    Friend WithEvents lblCount1 As System.Windows.Forms.Label
    Friend WithEvents lblText2 As System.Windows.Forms.Label
    Friend WithEvents lblCount2 As System.Windows.Forms.Label
    Friend WithEvents lblPath As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents cmbTargetGrp As System.Windows.Forms.ComboBox
    Friend WithEvents txtLog As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents pBar As System.Windows.Forms.ProgressBar
    Friend WithEvents bgWorker As System.ComponentModel.BackgroundWorker

End Class
