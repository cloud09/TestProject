Imports System.ComponentModel

Public Class ModuleRelease

    Private Const IniFile As String = "ModuleRelease.ini"   '設定ファイル
    Private Const MsgFile As String = "Message.ini"         'メッセージファイル

    Public Enum LabelStatus
        prep        '準備中
        copy        'コピー
        success     '成功
        failed      '失敗
        canceled    '中断
    End Enum

    ''' <summary>
    ''' ログ出力
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared ReadOnly Logger As log4net.ILog = _
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

#Region "Form Events"

    ''' <summary>
    ''' フォームロード時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' <para>
    ''' <list type="table">
    '''    <listheader>
    '''       <term>変更</term><description>概要</description>
    '''    </listheader>
    '''    <item><term>2011/04/07 S.Yasuhara</term><description>新規作成</description></item>
    ''' </list>
    ''' </para>
    ''' </remarks>
    Private Sub ModuleRelease_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '--- 初期化 ---
        bth1AllOn.Enabled = False
        btn1AllOff.Enabled = False

        'ボタンの有効無効
        btnCopy.Enabled = True
        btnCancel.Enabled = False

        '--- 設定ファイルの取得 ---
        Dim ini As New IniFile(GetAppPath() & IniFile)

        ' 配布先グループ
        For i As Integer = 1 To 10
            cmbTargetGrp.Items.Insert(i - 1, _
                                      i.ToString.PadLeft(2, "0"c) & "：" & _
                                      ini.ReadString("TARGET" & i.ToString.PadLeft(2, "0"c), "TargetName", Nothing) _
                                     )
        Next
        cmbTargetGrp.SelectedIndex = 0

        Try
            '--- 選択リストへファイルリストを出力 ---
            Dim path As String = ini.ReadString("SOURCE", "path", Nothing)

            If Not System.IO.Directory.Exists(GetPathWithLastDivide(path)) Then
                ShowMsgBox("MSG0000", "0008")       ' 配布元が存在しません(INIファイル異常)。
                Me.Close()
            End If

            lblPath.Text = path
            Dim strList As String() = GetFileList(path)

            If strList.Length > 0 Then
                chkFileList.Items.AddRange(strList)
                Call bth1AllOn_Click(sender, e)
                bth1AllOn.Enabled = True
                btn1AllOff.Enabled = True
            End If

        Catch ex As Exception
            ShowMsgBox("MSG0000", "0001", "ファイルリスト出力")       ' %1処理に失敗しました。
        End Try

    End Sub

    ''' <summary>
    ''' ディレクトリ選択ボタンクリック時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' <para>
    ''' <list type="table">
    '''    <listheader>
    '''       <term>変更</term><description>概要</description>
    '''    </listheader>
    '''    <item><term>2011/04/07 S.Yasuhara</term><description>新規作成</description></item>
    ''' </list>
    ''' </para>
    ''' </remarks>
    Private Sub btnDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDir.Click

        '--- フォルダ選択ダイアログ表示 ---
        Dim strDir As String = ShowFolderBrowser(False)
        If strDir = String.Empty Then
            Exit Sub
        Else
            lblPath.Text = strDir
        End If

        '--- 初期化 ---
        bth1AllOn.Enabled = False
        btn1AllOff.Enabled = False

        '--- 選択リストの初期化 ---
        If lblPath.Text = String.Empty Then
            Exit Sub
        Else
            For i As Integer = 0 To chkFileList.Items.Count - 1
                chkFileList.Items.RemoveAt(0)
            Next
        End If

        Try
            '--- 選択リストへファイルリストを出力 ---
            Dim strList As String() = GetFileList(lblPath.Text)

            If strList.Length > 0 Then
                chkFileList.BeginUpdate()
                chkFileList.Items.AddRange(strList)
                chkFileList.EndUpdate()
                Call bth1AllOn_Click(sender, e)
                bth1AllOn.Enabled = True
                btn1AllOff.Enabled = True
            End If

        Catch ex As Exception
            ShowMsgBox("MSG0000", "0001", "ファイルリスト出力")       ' %1処理に失敗しました。
        End Try

    End Sub

    ''' <summary>
    ''' 配布先グループ変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' <para>
    ''' <list type="table">
    '''    <listheader>
    '''       <term>変更</term><description>概要</description>
    '''    </listheader>
    '''    <item><term>2011/04/07 S.Yasuhara</term><description>新規作成</description></item>
    ''' </list>
    ''' </para>
    ''' </remarks>
    Private Sub cmbTargetGrp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTargetGrp.SelectedIndexChanged

        '--- 初期化 ---
        bth2AllOn.Enabled = False
        bth2AllOff.Enabled = False

        '--- 選択リストの初期化 ---
        For i As Integer = 0 To chkTargetList.Items.Count - 1
            chkTargetList.Items.RemoveAt(0)
        Next

        '--- 設定ファイルの取得 ---
        Dim ini As New IniFile(GetAppPath() & IniFile)

        Try
            '--- 選択リストへ配布先リストを出力 ---
            Dim listCount As String = ini.ReadString("TARGET" & (cmbTargetGrp.SelectedIndex + 1).ToString.PadLeft(2, "0"c), _
                                                     "PathCount", Nothing)
            If listCount > 0 Then

                Dim strTarget(listCount - 1) As String
                Dim strTmp As String = String.Empty

                For i As Integer = 0 To listCount - 1
                    strTmp = ini.ReadString("TARGET" & (cmbTargetGrp.SelectedIndex + 1).ToString.PadLeft(2, "0"c), _
                                            "Path" & (i + 1).ToString.PadLeft(2, "0"c), Nothing)
                    strTarget.SetValue(strTmp, i)
                Next

                If strTarget.Length > 0 Then
                    chkTargetList.BeginUpdate()
                    chkTargetList.Items.AddRange(strTarget)
                    chkTargetList.EndUpdate()
                    bth2AllOn.Enabled = True
                    bth2AllOff.Enabled = True
                    Call chkTargetListChange(1)
                End If

            End If

        Catch ex As Exception
            ShowMsgBox("msg0000", "0001", "配布先リスト出力")       ' %1処理に失敗しました。
            Me.Close()
        End Try
    End Sub

    ''' <summary>
    ''' コピーボタンクリック時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' <para>
    ''' <list type="table">
    '''    <listheader>
    '''       <term>変更</term><description>概要</description>
    '''    </listheader>
    '''    <item><term>2011/04/07 S.Yasuhara</term><description>新規作成</description></item>
    ''' </list>
    ''' </para>
    ''' </remarks>
    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click

        '処理が行われているときは何も行わない
        If bgWorker.IsBusy Then
            Return
        End If

        Dim path0 As String = String.Empty          'コピー元
        Dim path1 As String = String.Empty          'コピー先
        Dim strTmpDir(1) As String                  'コピー先のパス(0：パスの略称名、1：物理パス名)
        Dim strTmpFile As String = String.Empty     'コピーするファイル

        txtLog.Text = String.Empty                  'ログ領域のクリア
        changeLblStatus(LabelStatus.prep)           '状態ラベルのクリア

        'ボタンの有効無効
        btnCopy.Enabled = False
        btnCancel.Enabled = True

        ' --- 事前チェック ----
        ' チェックONが0件(配布ファイル)
        If chkFileList.CheckedItems.Count < 1 Then
            ShowMsgBox("MSG0000", "0005")  ' 配布ファイルが選択されていません。 
            'ボタンの有効無効
            btnCopy.Enabled = True
            btnCancel.Enabled = False
            Exit Sub
        End If

        ' チェックONが0件(配布先)
        If chkTargetList.CheckedItems.Count < 1 Then
            ShowMsgBox("MSG0000", "0006")  ' 配布先が選択されていません。
            'ボタンの有効無効
            btnCopy.Enabled = True
            btnCancel.Enabled = False
            Exit Sub
        End If

        ' 配布先が存在しない
        For i As Integer = 0 To chkTargetList.Items.Count - 1
            If chkTargetList.GetItemChecked(i) Then
                strTmpDir = chkTargetList.Items.Item(i).ToString.Split(","c)
                If Not System.IO.Directory.Exists(GetPathWithLastDivide(strTmpDir(1))) Then
                    ShowMsgBox("MSG0000", "0007")  ' 配布先が存在しません。
                    'ボタンの有効無効
                    btnCopy.Enabled = True
                    btnCancel.Enabled = False
                    Exit Sub
                End If
            End If
        Next

        ' 確認ダイアログ
        If ShowMsgBox("MSG0000", "0002") = Windows.Forms.DialogResult.No Then   ' リリースを開始しますか？
            Exit Sub
        End If

        'ログ(画面/ファイル)への出力
        Logger.Info("■■■■■ Release Start     ■■■■■")
        Logger.Info("From:[" & lblPath.Text & "]")
        txtLog.Text = Now & " ■■■■■ 開始 ■■■■■"
        txtLog.Text = txtLog.Text & vbCrLf & Now & " [" & lblPath.Text & "]内のファイルを配布対象に開始しました"

        'カーソル待機状態
        Me.Cursor = Cursors.WaitCursor

        'コントロールを初期化する
        pBar.Minimum = 0
        pBar.Maximum = CInt(lblCount1.Text) * CInt(lblCount2.Text)    '[配布ファイル]×[配布対象]
        pBar.Value = 0

        'BackgroundWorkerのProgressChangedイベントが発生するようにする
        bgWorker.WorkerReportsProgress = True
        'キャンセルできるようにする
        bgWorker.WorkerSupportsCancellation = True

        'DoWorkで取得できるパラメータを指定して処理を開始する(パラメータが必要なければ省略可能)
        bgWorker.RunWorkerAsync()

    End Sub

    'BackgroundWorkerのDoWorkイベントハンドラ：ここで時間のかかる処理を行う
    Private Sub bgWorker_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bgWorker.DoWork
        Dim bgWorker As BackgroundWorker = DirectCast(sender, BackgroundWorker)

        Dim path0 As String = String.Empty
        Dim path1 As String = String.Empty
        Dim strTmpDir(1) As String
        Dim strTmpFile As String = String.Empty
        Dim strTmpMsg As String = String.Empty

        ''パラメータを取得する
        'Dim param As Integer = CInt(e.Argument)

        Dim intCopyCount As Integer = 0

        '--- 配布先別に対象ファイルをコピー ---
        For i As Integer = 0 To chkTargetList.Items.Count - 1       '配布先取得
            If chkTargetList.GetItemChecked(i) Then

                '--- 配布先取得 ---
                strTmpDir = chkTargetList.Items.Item(i).ToString.Split(","c)
                Logger.Info(vbTab & "To:" & Trim(strTmpDir(0)) & "[" & strTmpDir(1) & "]")
                strTmpMsg = vbCrLf & Now & " " & Trim(strTmpDir(0)) & "[" & strTmpDir(1) & "] へ配布します..."
                'ProgressChangedイベントハンドラを呼び出し、コントロールの表示を変更する
                bgWorker.ReportProgress(intCopyCount, strTmpMsg)

                For j As Integer = 0 To chkFileList.Items.Count - 1     '配布元ファイル取得
                    If chkFileList.GetItemChecked(j) Then

                        'キャンセルされたか調べる
                        If bgWorker.CancellationPending Then
                            'キャンセルされたとき
                            e.Cancel = True
                            Return
                        End If

                        '--- 配布元ファイル取得 ---
                        strTmpFile = chkFileList.Items.Item(j).ToString
                        Logger.Info(vbTab & vbTab & strTmpFile)
                        strTmpMsg = vbCrLf & Now & " " & strTmpFile
                        'ProgressChangedイベントハンドラを呼び出し、コントロールの表示を変更する
                        bgWorker.ReportProgress(intCopyCount, strTmpMsg)


                        '--- コピー実施 ---
                        ''[TIPS]---------------------------------------------------------------------------------------------------------------
                        ''  キャンセルボタン付きのダイアログボックスでプログレスバーまで表示可能(VB2005以降)
                        ''  上書きするか否かをコピー前に選択すダイアログも表示可能
                        ''      My.Computer.FileSystem.CopyFile(path0, path1, FileIO.UIOption.AllDialogs, FileIO.UICancelOption.DoNothing)
                        ''  ファイルのプロパティーも編集可能
                        ''      System.IO.File.SetLastAccessTime(path1, Now)    '指定したファイルに最後にアクセスした日付と時刻を設定します。　
                        ''      System.IO.File.SetLastWriteTime(path1, Now)     '指定したファイルに最後に書き込んだ日付と時刻を設定します。
                        ''      System.IO.File.SetCreationTime(path1, Now)      'ファイルが作成された日付と時刻を設定します。　
                        ''---------------------------------------------------------------------------------------------------------------------
                        path0 = GetPathWithLastDivide(lblPath.Text) & strTmpFile
                        path1 = GetPathWithLastDivide(strTmpDir(1)) & strTmpFile
                        My.Computer.FileSystem.CopyFile(path0, path1, True)

                        'ProgressChangedイベントハンドラを呼び出し、コントロールの表示を変更する
                        intCopyCount += 1
                        bgWorker.ReportProgress(intCopyCount)

                        'ProgressChangedで取得できる結果を設定する。結果が必要なければ省略できる
                        'e.Result = xxx

                    End If
                Next
            End If
        Next

    End Sub

    'BackgroundWorkerのProgressChangedイベントハンドラ：コントロールの操作は必ずここで行い、DoWorkでは絶対にしない
    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles bgWorker.ProgressChanged

        Dim intCheckCount As Integer = 0

        '開始時にラベルを「コピー中」へ変更
        If e.ProgressPercentage = 1 Then changeLblStatus(LabelStatus.copy)

        'ProgressBarの値を変更する
        'Debug.Print(e.ProgressPercentage)
        pBar.Value = e.ProgressPercentage

        'ログ領域のメッセージを変更する
        txtLog.Text = txtLog.Text & e.UserState
        txtLog.SelectionStart = Len(txtLog.Text) + 1
        txtLog.ScrollToCaret()

        '完了した配布先のチェックを外す(商で求める)
        intCheckCount = CInt(e.ProgressPercentage) \ CInt(lblCount1.Text)
        If intCheckCount > 0 Then
            chkTargetList.SetItemChecked(intCheckCount - 1, False)
        End If

    End Sub

    'BackgroundWorkerのRunWorkerCompletedイベントハンドラ：処理が終わったときに呼び出される
    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bgWorker.RunWorkerCompleted
        If Not e.Error Is Nothing Then
            'エラーが発生したとき
            changeLblStatus(LabelStatus.failed)
            ShowMsgBox("MSG0000", "0004", e.Error.Message.ToString)  ' リリースが失敗しました。\n%1
            Logger.Error("■■■■■ Release Failed   ■■■■■", e.Error)
            txtLog.Text = txtLog.Text & vbCrLf & Now & " ■■■■■ 失敗 ■■■■■"
        ElseIf e.Cancelled Then
            'キャンセルされたとき
            changeLblStatus(LabelStatus.canceled)
            ShowMsgBox("MSG0000", "0009")                           ' リリースを中断しました。
            Logger.Info("■■■■■ Release Canceled  ■■■■■")
            txtLog.Text = txtLog.Text & vbCrLf & Now & " ■■■■■ 中断 ■■■■■"
        Else
            '正常に終了したとき
            changeLblStatus(LabelStatus.success)
            ShowMsgBox("MSG0000", "0003")                           ' リリースが完了しました。
            Logger.Info("■■■■■ Release Successed ■■■■■")
            txtLog.Text = txtLog.Text & vbCrLf & Now & " ■■■■■ 完了 ■■■■■"
        End If
        txtLog.SelectionStart = Len(txtLog.Text) + 1
        txtLog.ScrollToCaret()

        'カーソル元に戻す
        Me.Cursor = Cursors.Default
        'ボタンの有効無効
        btnCopy.Enabled = True
        btnCancel.Enabled = False
        '状態ラベルの初期化
        changeLblStatus(LabelStatus.prep)
        '件数ラベルの更新
        lblCount1.Text = chkFileList.CheckedItems.Count.ToString
        lblCount2.Text = chkTargetList.CheckedItems.Count.ToString
        'プログレスバーのリセット
        pBar.Value = 0

    End Sub

    ''' <summary>
    ''' 状況ラベルを編集する
    ''' </summary>
    ''' <param name="status">LabelStatus(prep/copy/success/failed)</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub changeLblStatus(ByVal status As LabelStatus)
        Select Case status
            Case LabelStatus.prep
                lblStatus.Text = "準備中"
                lblStatus.ForeColor = Color.Black
                lblStatus.BackColor = Color.White
            Case LabelStatus.copy
                lblStatus.Text = "コピー中"
                lblStatus.ForeColor = Color.Black
                lblStatus.BackColor = Color.Yellow
            Case LabelStatus.success
                lblStatus.Text = "完了"
                lblStatus.ForeColor = Color.Black
                lblStatus.BackColor = Color.Cyan
            Case LabelStatus.failed
                lblStatus.Text = "失敗"
                lblStatus.ForeColor = Color.White
                lblStatus.BackColor = Color.Red
            Case LabelStatus.canceled
                lblStatus.Text = "中断"
                lblStatus.ForeColor = Color.White
                lblStatus.BackColor = Color.Gray
        End Select
    End Sub

    ''' <summary>
    ''' (配布ファイル)チェックのON/OFFを行う
    ''' </summary>
    ''' <param name="flg">True:ON,False:OFF</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub chkFileListChange(ByVal flg As Boolean)
        For i As Integer = 0 To chkFileList.Items.Count - 1
            chkFileList.SetItemChecked(i, flg)
        Next
        lblCount1.Text = chkFileList.CheckedItems.Count.ToString
    End Sub

    ''' <summary>
    ''' (配布先)チェックのON/OFFを行う
    ''' </summary>
    ''' <param name="flg">True:ON,False:OFF</param>
    ''' <remarks>
    ''' </remarks>
    Private Sub chkTargetListChange(ByVal flg As Boolean)
        For i As Integer = 0 To chkTargetList.Items.Count - 1
            chkTargetList.SetItemChecked(i, flg)
        Next
        lblCount2.Text = chkTargetList.CheckedItems.Count.ToString
    End Sub

    ''' <summary>
    ''' (配布ファイル)全てのチェックを付ける：クリック時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub bth1AllOn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bth1AllOn.Click
        Call chkFileListChange(1)
    End Sub

    ''' <summary>
    ''' (配布ファイル)全てのチェックを外す：クリック時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub bth1AllOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1AllOff.Click
        Call chkFileListChange(0)
    End Sub

    ''' <summary>
    ''' (配布先)全てのチェックを付ける：クリック時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub bth2AllOn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bth2AllOn.Click
        Call chkTargetListChange(1)
    End Sub

    ''' <summary>
    ''' (配布先)全てのチェックを外す：クリック時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub bth2AllOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bth2AllOff.Click
        Call chkTargetListChange(0)
    End Sub

    ''' <summary>
    ''' 配布ファイルリスト：変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' <para>
    ''' <list type="table">
    '''    <listheader>
    '''       <term>変更</term><description>概要</description>
    '''    </listheader>
    '''    <item><term>2011/04/07 S.Yasuhara</term><description>新規作成</description></item>
    ''' </list>
    ''' </para>
    ''' </remarks>
    Private Sub chkFileList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFileList.SelectedIndexChanged
        lblCount1.Text = chkFileList.CheckedItems.Count.ToString
    End Sub

    ''' <summary>
    ''' 配布先リスト：変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub chkTargetList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTargetList.SelectedIndexChanged
        lblCount2.Text = chkTargetList.CheckedItems.Count.ToString
    End Sub

    ''' <summary>
    ''' 配布ファイルリスト：キープレス時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub chkFileList_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles chkFileList.KeyPress
        lblCount1.Text = chkFileList.CheckedItems.Count.ToString
    End Sub

    ''' <summary>
    ''' 配布先リスト：キープレス時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub chkTargetList_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles chkTargetList.KeyPress
        lblCount2.Text = chkTargetList.CheckedItems.Count.ToString
    End Sub

    ''' <summary>
    ''' ログテキストエリア：キーダウン時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub txtLog_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLog.KeyDown
        If e.KeyCode = Keys.A AndAlso e.Control Then
            DirectCast(sender, TextBox).SelectAll()
        End If
    End Sub

    ''' <summary>
    ''' Cancelボタン：クリック時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        'カーソル元に戻す
        Me.Cursor = Cursors.Default
        'ボタンの有効無効
        btnCopy.Enabled = True
        btnCancel.Enabled = False
        '状態ラベルの初期化
        changeLblStatus(LabelStatus.prep)
        '件数ラベルの更新
        lblCount1.Text = chkFileList.CheckedItems.Count.ToString
        lblCount2.Text = chkTargetList.CheckedItems.Count.ToString

        'キャンセルする
        bgWorker.CancelAsync()
    End Sub

    ''' <summary>
    ''' Closeボタン：クリック時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Private Methods"

#Region "メッセージボックス表示関数"
    ''' <summary>
    ''' メッセージボックス表示関数
    ''' </summary>
    ''' <param name="pgID">画面ID・帳票ID・バッチID</param>
    ''' <param name="msgID">メッセージID</param>
    ''' <param name="param">メッセージの置き換えパラメータ</param>
    ''' <returns>メッセージ結果[DialogResult]</returns>
    ''' <remarks>
    ''' <para>
    ''' <list type="table">
    '''    <listheader>
    '''       <term>変更</term><description>概要</description>
    '''    </listheader>
    '''    <item><term>2011/04/07 S.Yasuhara</term><description>新規作成</description></item>
    ''' </list>
    ''' </para>
    ''' </remarks>
    Public Function ShowMsgBox(ByVal pgID As String, _
                               ByVal msgID As String, _
                               ByVal ParamArray param() As String _
    ) As DialogResult

        '--- メッセージファイルの取得 ---
        Dim ini As New IniFile(GetAppPath() & MsgFile)

        '--- メッセージの取得 ---
        Dim msgData As String = ini.ReadString(pgID, msgID, Nothing)

        '--- メッセージ取得失敗 ---
        If (msgData Is Nothing) Then
            MessageBox.Show("メッセージID[" & pgID & "][" & msgID & "]は登録されていません。", "メッセージエラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return DialogResult.None
        End If

        '--- メッセージ項目取得 ---
        Dim msgSet() As String = msgData.Split("|"c)

        Dim msg As String = msgSet(0)
        Dim btn As Integer
        If Not Integer.TryParse(msgSet(1), btn) Then
            MessageBox.Show("メッセージID[" & pgID & "][" & msgID & "]のボタン番号が不正です。", "メッセージエラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return DialogResult.None
        End If
        Dim icon As Integer
        If Not Integer.TryParse(msgSet(2), icon) Then
            MessageBox.Show("メッセージID[" & pgID & "][" & msgID & "]のアイコン番号が不正です。", "メッセージエラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return DialogResult.None
        End If

        '--- メッセージのリプレース ---
        For i As Integer = param.GetLowerBound(0) To param.GetUpperBound(0)
            msg = msg.Replace("%" & CStr(i + 1), param(i))
        Next
        msg = msg.Replace("\n", System.Environment.NewLine)

        '--- メッセージ表示 ---
        Try
            Return MessageBox.Show(msg, String.Empty, DirectCast(btn, MessageBoxButtons), DirectCast(icon, MessageBoxIcon))
        Catch ex As Exception
            MessageBox.Show("メッセージID[" & pgID & "][" & msgID & "]のメッセージが不正です。" & System.Environment.NewLine & ex.Message, "メッセージエラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return DialogResult.None
        End Try

    End Function

#End Region

#Region "フォルダ選択ダイアログを表示"
    ''' <summary>
    ''' フォルダ選択ダイアログを表示
    ''' </summary>
    ''' <param name="newFolder">True:新規フォルダボタンを表示する,False:新規フォルダボタンを表示しない</param>
    ''' <param name="initPath">初期表示フォルダ</param>
    ''' <returns>選択フォルダパス[String]</returns>
    ''' <remarks>
    ''' <para>
    ''' <list type="table">
    '''    <listheader>
    '''       <term>変更</term><description>概要</description>
    '''    </listheader>
    '''    <item><term>2011/04/07 S.Yasuhara</term><description>新規作成</description></item>
    ''' </list>
    ''' </para>
    ''' </remarks>
    Public Function ShowFolderBrowser( _
                Optional ByVal newFolder As Boolean = False, _
                Optional ByVal initPath As String = Nothing _
    ) As String

        '--- 初期表示フォルダの設定 ---
        If (initPath Is Nothing) OrElse initPath = "" Then
            initPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        ElseIf IO.Directory.Exists(initPath) = False Then
            '--- フォルダが存在しない場合 ---
            Dim path As String = IO.Path.GetDirectoryName(initPath)
            If IO.Directory.Exists(path) Then
                initPath = path
            Else
                initPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            End If
        End If

        Using myDialog As New FolderBrowserDialog
            With myDialog
                '--- ダイアログ設定 ---
                .RootFolder = Environment.SpecialFolder.Desktop
                .SelectedPath = initPath
                .ShowNewFolderButton = newFolder

                '--- ダイアログを表示 ---
                If .ShowDialog() = DialogResult.OK Then
                    Return .SelectedPath
                Else
                    Return Nothing
                End If

            End With
        End Using

    End Function

#End Region

#Region "ファイルリストの取得"
    ''' <summary>
    ''' ファイルリストの取得
    ''' </summary>
    ''' <param name="path">ディレクトリパス</param>
    ''' <returns>ファイル配列</returns>
    ''' <remarks>
    ''' <para>
    ''' <list type="table">
    '''    <listheader>
    '''       <term>変更</term><description>概要</description>
    '''    </listheader>
    '''    <item><term>2011/04/07 S.Yasuhara</term><description>新規作成</description></item>
    ''' </list>
    ''' </para>
    ''' </remarks>
    Public Function GetFileList(ByVal path As String) As String()
        Dim files As String() = Nothing

        Try
            ' 指定ディレクトリのファイルをすべて取得
            files = System.IO.Directory.GetFiles(path, "*", System.IO.SearchOption.TopDirectoryOnly)

            ' フルパスからファイルのみに変換
            For i As Integer = 0 To files.Length - 1
                files(i) = System.IO.Path.GetFileName(files(i)).ToString
            Next

            ' ファイル名にてソート実行
            Array.Sort(files)

        Catch ex As Exception
            'ShowMsgBox("MSG0000", "0001", "ファイルリスト取得")       ' %1処理に失敗しました。
        End Try
        Return files
    End Function
#End Region

#Region "パスの末尾に\がない場合は付加する"

    ''' <summary>
    ''' パスの末尾に"\"がない場合は付加する
    ''' </summary>
    ''' <param name="path">"\"を付加するパス</param>
    ''' <returns>末尾が"\"のパス</returns>
    ''' <remarks>
    ''' <para>
    ''' <list type="table">
    '''    <listheader>
    '''       <term>変更</term><description>概要</description>
    '''    </listheader>
    '''    <item><term>2011/04/07 S.Yasuhara</term><description>新規作成</description></item>
    ''' </list>
    ''' </para>
    ''' </remarks>
    Public Function GetPathWithLastDivide(ByVal path As String) As String
        If String.IsNullOrEmpty(path) Then Return path
        If path.EndsWith("\") Then
            Return path
        Else
            Return path & "\"
        End If
    End Function

#End Region

#Region "現在の実行パスを取得する"
    ''' <summary>
    ''' 現在の実行パスを取得する
    ''' </summary>
    ''' <returns>実行パス</returns>
    ''' <remarks>
    ''' <para>
    ''' <list type="table">
    '''    <listheader>
    '''       <term>変更</term><description>概要</description>
    '''    </listheader>
    '''    <item><term>2011/04/07 S.Yasuhara</term><description>新規作成</description></item>
    ''' </list>
    ''' </para>
    ''' </remarks>
    Public Function GetAppPath() As String
        Return GetPathWithLastDivide(My.Application.Info.DirectoryPath)
    End Function

#End Region

#Region "Shift_JISでのバイト数を返す"

    ''' <summary>
    ''' Shift_JISでのバイト数を返す
    ''' </summary>
    ''' <param name="target">対象データ</param>
    ''' <returns>対象データのShift_JISでのバイト数</returns>
    ''' <remarks>
    ''' <para>
    ''' <list type="table">
    '''    <listheader>
    '''       <term>変更</term><description>概要</description>
    '''    </listheader>
    '''    <item><term>2011/04/07 S.Yasuhara</term><description>新規作成</description></item>
    ''' </list>
    ''' </para>
    ''' </remarks>
    Public Function GetShiftJISLength( _
                    ByVal target As String) As Integer
        With System.Text.Encoding.GetEncoding("Shift_JIS")
            Return .GetByteCount(target)
        End With
    End Function

#End Region

#Region "指定したバイト数以下で文字列を切り出す(Shift_JIS)"

    ''' <summary>
    ''' 指定したバイト数以下で文字列を切り出す(Shift_JIS)
    ''' </summary>
    ''' <param name="target">対象データ</param>
    ''' <param name="maxBytes">切り出すバイト数</param>
    ''' <returns>切り出した文字列</returns>
    ''' <remarks>
    ''' <para>
    ''' <list type="table">
    '''    <listheader>
    '''       <term>変更</term><description>概要</description>
    '''    </listheader>
    '''    <item><term>2011/04/07 S.Yasuhara</term><description>新規作成</description></item>
    ''' </list>
    ''' </para>
    ''' </remarks>
    Public Function GetShiftJISByteString(ByVal target As String, ByVal maxBytes As Integer) As String
        If GetShiftJISLength(target) <= maxBytes Then Return target
        With System.Text.Encoding.GetEncoding("Shift_JIS")
            Dim value As String = .GetString(.GetBytes(target), 0, maxBytes)
            If maxBytes < GetShiftJISLength(value) Then
                Return value.Substring(0, value.Length - 1)
            Else
                Return value
            End If
        End With
    End Function

#End Region

#Region "現在のプロセスがすでに実行されていないかチェックする"

    ''' <summary>
    ''' 現在のプロセスがすでに実行されていないかチェックする
    ''' </summary>
    ''' <returns>True:実行されている, False:実行されていない</returns>
    ''' <remarks>
    ''' <para>
    ''' ■ 変更履歴
    ''' <list type="table">
    '''    <listheader>
    '''       <term>変更</term><description>概要</description>
    '''    </listheader>
    '''    <item><term>2009/07/01 KSC</term><description>新規作成</description></item>
    ''' </list>
    ''' </para>
    ''' </remarks>
    Public Function IsPrevInstance() As Boolean
        If UBound(Diagnostics.Process.GetProcessesByName(Diagnostics.Process.GetCurrentProcess.ProcessName)) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#End Region

End Class
