' IniFile 読み書き クラス
Imports System.Collections.Specialized

#Region "IniFile クラス"

''' ---------------------------------------------------------------------------------------
''' <summary>
'''     IniFile の読み込み、または書き込みを提供します。
''' </summary>
''' ---------------------------------------------------------------------------------------

Public NotInheritable Class IniFile

#Region "メンバの定義"

    ' 定数の定義
    Private Const MAX_LINE As Integer = 1024
    Private Const DOUBLE_QUOTE As String = """"

#End Region

#Region "コンストラクタ (+3)"

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したファイル用の KobelcoSystem.Ini.IniFile
    '''     クラスの新しいインスタンスを初期化します。</summary>
    ''' <param name="filePath">
    '''     読み込まれる構成設定ファイルのパス。</param>
    ''' ---------------------------------------------------------------------------------------
    Public Sub New(ByVal filePath As String)
        Me.New(filePath, System.Text.Encoding.Default)
    End Sub

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したファイル用の KobelcoSystem.Ini.IniFile
    '''     クラスの新しいインスタンスを、エンコーディングを指定して初期化します。</summary>
    ''' <param name="filePath">
    '''     読み込まれる構成設定ファイルのパス。</param>
    ''' <param name="encoding">
    '''     読み込みに使用する文字エンコーディング。</param>
    ''' ---------------------------------------------------------------------------------------
    Public Sub New(ByVal filePath As String, ByVal encoding As System.Text.Encoding)
        Me.New(filePath, Nothing, encoding)
    End Sub

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したファイル用の KobelcoSystem.Ini.IniFile
    '''     クラスの新しいインスタンスを、初期値を指定して初期化します。</summary>
    ''' <param name="filePath">
    '''     読み込まれる構成設定ファイルのパス。</param>
    ''' <param name="defaultValue">
    '''     読み込みに失敗した場合に返される値。</param>
    ''' ---------------------------------------------------------------------------------------
    Public Sub New(ByVal filePath As String, ByVal defaultValue As String)
        Me.New(filePath, defaultValue, System.Text.Encoding.Default)
    End Sub

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したファイル用の System.IO.IniFile
    '''     クラスの新しいインスタンスを、初期値とエンコーディング初期化します。</summary>
    ''' <param name="filePath">
    '''     読み込まれる構成設定ファイルのパス。</param>
    ''' <param name="defaultValue">
    '''     読み込みに失敗した場合に返される値。</param>
    ''' <param name="encoding">
    '''     読み込みに使用する文字エンコーディング。</param>
    ''' ---------------------------------------------------------------------------------------
    Public Sub New(ByVal filePath As String, ByVal defaultValue As String, ByVal encoding As System.Text.Encoding)
        Me.FilePath = filePath
        Me.Section = String.Empty
        Me.DefaultValue = defaultValue
        Me.Encoding = encoding
    End Sub

#End Region

#Region "Public メソッド"
#Region "ReadString メソッド (+2)"

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションとキーの場所にある値を読み込みます。</summary>
    ''' <param name="key">
    '''     読み込みに使用するキー。</param>
    ''' <returns>
    '''     指定したセクションとキーに格納された値。失敗時は DefaultValue。</returns>
    ''' ---------------------------------------------------------------------------------------
    Public Function ReadString(ByVal key As String) As String
        Return ReadIniFileValue(Me.FilePath, Me.Section, key, Me.DefaultValue, Me.Encoding)
    End Function

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションとキーの場所にある値を読み込みます。</summary>
    ''' <param name="section">
    '''     読み込みに使用するセクション。</param>
    ''' <param name="key">
    '''     読み込みに使用するキー。</param>
    ''' <returns>
    '''     指定したセクションとキーに格納された値。失敗時は DefaultValue。</returns>
    ''' ---------------------------------------------------------------------------------------
    Public Function ReadString(ByVal section As String, ByVal key As String) As String
        Return ReadIniFileValue(Me.FilePath, section, key, Me.DefaultValue, Me.Encoding)
    End Function

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションとキーの場所にある値を読み込みます。</summary>
    ''' <param name="section">
    '''     読み込みに使用するセクション。</param>
    ''' <param name="key">
    '''     読み込みに使用するキー。</param>
    ''' <param name="defaultValue">
    '''     読み込みに失敗した場合に返される値。</param>
    ''' <returns>
    '''     指定したセクションとキーに格納された値。失敗時は DefaultValue。</returns>
    ''' ---------------------------------------------------------------------------------------
    Public Function ReadString(ByVal section As String, ByVal key As String, ByVal defaultValue As String) As String
        Return ReadIniFileValue(Me.FilePath, section, key, defaultValue, Me.Encoding)
    End Function

#End Region

#Region "ReadInteger メソッド (+2)"

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションとキーの場所にある値を読み込みます。</summary>
    ''' <param name="key">
    '''     読み込みに使用するキー。</param>
    ''' <returns>
    '''     指定したセクションとキーに格納された値。失敗時は DefaultValue。</returns>
    ''' ---------------------------------------------------------------------------------------
    Public Function ReadInteger(ByVal key As String) As Integer
        Return ToInt32(ReadIniFileValue(Me.FilePath, Me.Section, key, Me.DefaultValue, Me.Encoding))
    End Function

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションとキーの場所にある値を読み込みます。</summary>
    ''' <param name="section">
    '''     読み込みに使用するセクション。</param>
    ''' <param name="key">
    '''     読み込みに使用するキー。</param>
    ''' <returns>
    '''     指定したセクションとキーに格納された値。失敗時は DefaultValue。</returns>
    ''' ---------------------------------------------------------------------------------------
    Public Function ReadInteger(ByVal section As String, ByVal key As String) As Integer
        Return ToInt32(ReadIniFileValue(Me.FilePath, section, key, Me.DefaultValue, Me.Encoding))
    End Function

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションとキーの場所にある値を読み込みます。</summary>
    ''' <param name="section">
    '''     読み込みに使用するセクション。</param>
    ''' <param name="key">
    '''     読み込みに使用するキー。</param>
    ''' <param name="defaultValue">
    '''     読み込みに失敗した場合に返される値。</param>
    ''' <returns>
    '''     指定したセクションとキーに格納された値。失敗時は DefaultValue。</returns>
    ''' ---------------------------------------------------------------------------------------
    Public Function ReadInteger(ByVal section As String, ByVal key As String, ByVal defaultValue As Integer) As Integer
        Return ToInt32(ReadIniFileValue(Me.FilePath, section, key, defaultValue.ToString(), Me.Encoding))
    End Function

#End Region

#Region "ReadDouble メソッド (+2)"

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションとキーの場所にある値を読み込みます。</summary>
    ''' <param name="key">
    '''     読み込みに使用するキー。</param>
    ''' <returns>
    '''     指定したセクションとキーに格納された値。失敗時は Default に指定した値。</returns>
    ''' ---------------------------------------------------------------------------------------
    Public Function ReadDouble(ByVal key As String) As Double
        Return ToDouble(ReadIniFileValue(Me.FilePath, Me.Section, key, Me.DefaultValue, Me.Encoding))
    End Function

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションとキーの場所にある値を読み込みます。</summary>
    ''' <param name="section">
    '''     読み込みに使用するセクション。</param>
    ''' <param name="key">
    '''     読み込みに使用するキー。</param>
    ''' <returns>
    '''     指定したセクションとキーに格納された値。失敗時は Default に指定した値。</returns>
    ''' ---------------------------------------------------------------------------------------
    Public Function ReadDouble(ByVal section As String, ByVal key As String) As Double
        Return ToDouble(ReadIniFileValue(Me.FilePath, section, key, Me.DefaultValue, Me.Encoding))
    End Function

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションとキーの場所にある値を読み込みます。</summary>
    ''' <param name="section">
    '''     読み込みに使用するセクション。</param>
    ''' <param name="key">
    '''     読み込みに使用するキー。</param>
    ''' <param name="dDefault">
    '''     読み込みに失敗した場合に返される値。</param>
    ''' <returns>
    '''     指定したセクションとキーに格納された値。失敗時は iDefault。</returns>
    ''' ---------------------------------------------------------------------------------------
    Public Function ReadDouble(ByVal section As String, ByVal key As String, ByVal dDefault As Double) As Double
        Return ToDouble(ReadIniFileValue(Me.FilePath, section, key, dDefault.ToString(), Me.Encoding))
    End Function

#End Region

#Region "ReadSection メソッド (+1)"

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションにあるキーと値をすべて読み込みます。</summary>
    ''' <returns>
    '''     セクションの構造を表す IniSection オブジェクト。</returns>
    ''' ---------------------------------------------------------------------------------------
    Public Function ReadSection() As IniSection
        Return ReadIniFileSection(Me.FilePath, Me.Section, Me.Encoding)
    End Function

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションにあるキーと値をすべて読み込みます。</summary>
    ''' <param name="section">
    '''     読み込みに使用するセクション。</param>
    ''' <returns>
    '''     セクションの構造を表す IniSection オブジェクト。</returns>
    ''' ---------------------------------------------------------------------------------------
    Public Function ReadSection(ByVal section As String) As IniSection
        Return ReadIniFileSection(Me.FilePath, section, Me.Encoding)
    End Function

#End Region

#Region "WriteString メソッド (+1)"

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションとキーの場所に指定した値を書き込みます。</summary>
    ''' <param name="key">
    '''     書き込みに使用するキー。</param>
    ''' <param name="writeValue">
    '''     書き込みに使用する値。</param>
    ''' ---------------------------------------------------------------------------------------
    Public Sub WriteString(ByVal key As String, ByVal writeValue As String)
        WriteIniFileValue(Me.FilePath, Me.Section, key, writeValue, Me.Encoding)
    End Sub

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションとキーの場所に指定した値を書き込みます。</summary>
    ''' <param name="section">
    '''     書き込みに使用するセクション。</param>
    ''' <param name="key">
    '''     書き込みに使用するキー。</param>
    ''' <param name="writeValue">
    '''     書き込みに使用する値。</param>
    ''' ---------------------------------------------------------------------------------------
    Public Sub WriteString(ByVal section As String, ByVal key As String, ByVal writeValue As String)
        WriteIniFileValue(Me.FilePath, section, key, writeValue, Me.Encoding)
    End Sub

#End Region

#Region "WriteInteger メソッド (+1)"

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションとキーの場所に指定した値を書き込みます。</summary>
    ''' <param name="key">
    '''     書き込みに使用するキー。</param>
    ''' <param name="writeValue">
    '''     書き込みに使用する値。</param>
    ''' ---------------------------------------------------------------------------------------
    Public Sub WriteInteger(ByVal key As String, ByVal writeValue As Integer)
        WriteIniFileValue(Me.FilePath, Me.Section, key, writeValue.ToString(), Me.Encoding)
    End Sub

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションとキーの場所に指定した値を書き込みます。</summary>
    ''' <param name="section">
    '''     書き込みに使用するセクション。</param>
    ''' <param name="key">
    '''     書き込みに使用するキー。</param>
    ''' <param name="writeValue">
    '''     書き込みに使用する値。</param>
    ''' ---------------------------------------------------------------------------------------
    Public Sub WriteInteger(ByVal section As String, ByVal key As String, ByVal writeValue As Integer)
        WriteIniFileValue(Me.FilePath, section, key, writeValue.ToString(), Me.Encoding)
    End Sub

#End Region

#Region "WriteDouble メソッド (+1)"

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションとキーの場所に指定した値を書き込みます。</summary>
    ''' <param name="key">
    '''     書き込みに使用するキー。</param>
    ''' <param name="writeValue">
    '''     書き込みに使用する値。</param>
    ''' ---------------------------------------------------------------------------------------
    Public Sub WriteDouble(ByVal key As String, ByVal writeValue As Double)
        WriteIniFileValue(Me.FilePath, Me.Section, key, writeValue.ToString(), Me.Encoding)
    End Sub

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションとキーの場所に指定した値を書き込みます。</summary>
    ''' <param name="section">
    '''     書き込みに使用するセクション。</param>
    ''' <param name="key">
    '''     書き込みに使用するキー。</param>
    ''' <param name="writeValue">
    '''     書き込みに使用する値。</param>
    ''' ---------------------------------------------------------------------------------------
    Public Sub WriteDouble(ByVal section As String, ByVal key As String, ByVal writeValue As Double)
        WriteIniFileValue(Me.FilePath, section, key, writeValue.ToString(), Me.Encoding)
    End Sub

#End Region

#Region "WriteSection メソッド (+1)"

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションに IniSection のキーと値を書き込みます。</summary>
    ''' <param name="hSection">
    '''     セクションの構造を表す IniSection オブジェクト。</param>
    ''' ---------------------------------------------------------------------------------------
    Public Sub WriteSection(ByVal hSection As IniSection)
        WriteIniFileSection(Me.FilePath, Me.Section, hSection, Me.Encoding)
    End Sub

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     指定したセクションに IniSection のキーと値を書き込みます。</summary>
    ''' <param name="section">
    '''     書き込みに使用するセクション。</param>
    ''' <param name="hSection">
    '''     セクションの構造を表す IniSection オブジェクト。</param>
    ''' ---------------------------------------------------------------------------------------
    Public Sub WriteSection(ByVal section As String, ByVal hSection As IniSection)
        WriteIniFileSection(Me.FilePath, section, hSection, Me.Encoding)
    End Sub

#End Region

#Region "FilePath プロパティ"

    Private _FilePath As String

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     読み込みに使用するファイル パスを取得または設定します。
    ''' </summary>
    ''' ---------------------------------------------------------------------------------------
    Public Property FilePath() As String
        Get
            Return _FilePath
        End Get
        Set(ByVal value As String)
            _FilePath = value
        End Set
    End Property

#End Region

#Region "Section プロパティ"

    Private _Section As String

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     読み込みに使用するセクションを取得または設定します。
    ''' </summary>
    ''' ---------------------------------------------------------------------------------------
    Public Property Section() As String
        Get
            Return _Section
        End Get
        Set(ByVal value As String)
            _Section = value
        End Set
    End Property

#End Region

#Region "DefaultValue プロパティ"

    Private _DefaultValue As String

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     読み込みに失敗した場合に返される値を取得または設定します。
    ''' </summary>
    ''' ---------------------------------------------------------------------------------------
    Public Property DefaultValue() As String
        Get
            Return _DefaultValue
        End Get
        Set(ByVal value As String)
            _DefaultValue = value
        End Set
    End Property

#End Region

#Region "Encoding プロパティ"

    Private _Encoding As System.Text.Encoding

    ''' ---------------------------------------------------------------------------------------
    ''' <summary>
    '''     読み込みに使用する文字エンコーディングを取得または設定します。
    ''' </summary>
    ''' ---------------------------------------------------------------------------------------
    Public Property Encoding() As System.Text.Encoding
        Get
            Return _Encoding
        End Get
        Set(ByVal value As System.Text.Encoding)
            _Encoding = value
        End Set
    End Property

#End Region
#End Region

#Region "Private メソッド"
#Region "ReadIniFileValue メソッド"

    Private Shared Function ReadIniFileValue(ByVal filePath As String, ByVal section As String, ByVal key As String, ByVal defaultValue As String, ByVal encoding As System.Text.Encoding) As String
        section = section.Trim().ToLower()
        key = key.Trim().ToLower()

        ' ファイル名・セクション名・キー名がない場合はデフォルト値を返す
        If (filePath = String.Empty OrElse section = String.Empty OrElse key = String.Empty) Then
            Return defaultValue
        End If

        ' 指定したファイルが存在しない場合はデフォルト値を返す
        If Not System.IO.File.Exists(filePath) Then
            Return defaultValue
        End If

        Using cReader As System.IO.StreamReader = New System.IO.StreamReader(filePath, encoding)
            Try
                Do While (cReader.Peek() > -1)
                    Dim target As String = cReader.ReadLine().TrimStart()

                    ' セクションの始まりかどうか判断する
                    If IsSectionBegin(target, section) Then
                        Do While (cReader.Peek() > -1)
                            target = cReader.ReadLine().TrimStart()

                            ' セクションの終わりかどうか判断する
                            If IsSectionEnd(target) Then
                                Return defaultValue
                            End If

                            ' キーが合致している場合は格納された値が返される
                            Dim nReturn As String = GetValueOfMatchKey(target, key)

                            If (nReturn IsNot Nothing) Then
                                Return nReturn
                            End If
                        Loop

                        Return defaultValue
                    End If
                Loop
            Catch ex As System.Exception
                Throw
            Finally
                If (cReader IsNot Nothing) Then
                    cReader.Close()
                End If
            End Try
        End Using

        Return defaultValue
    End Function

#End Region

#Region "WriteIniFileValue メソッド"

    Private Shared Sub WriteIniFileValue(ByVal filePath As String, ByVal section As String, ByVal key As String, ByVal writeValue As String, ByVal encoding As System.Text.Encoding)
        section = section.Trim()
        key = key.Trim()

        ' ファイル名・セクション名・キー名がない場合は終了する
        If (filePath = String.Empty OrElse section = String.Empty OrElse key = String.Empty) Then
            Return
        End If

        ' 既存ファイルがあれば中身をすべて取得する
        Dim hStrings As StringCollection = GetFileContents(filePath, encoding)

        ' 既存ファイルがない場合は終了する
        If (hStrings Is Nothing) Then
            Return
        End If

        ' 最大行数とセクションのLowerを取得
        Dim iMaxCount As Integer = hStrings.Count - 1
        Dim nSecLower As String = section.ToLower()

        ' 最大要素数まで 1 要素ずつ読み込む
        For i As Integer = 0 To iMaxCount
            hStrings(i) = hStrings(i).TrimStart()

            ' セクションの始まりかどうか判断する
            If IsSectionBegin(hStrings(i), nSecLower) Then
                Dim iRow As Integer

                For iRow = i + 1 To iMaxCount
                    ' セクションの終わりかどうか判断する
                    If IsSectionEnd(hStrings(iRow).TrimStart()) Then
                        WriteInsertKeyValue(filePath, key, writeValue, hStrings, iRow - 1, encoding)
                        Return
                    End If

                    ' キーが合致している場合は書き込まれるべき文字列を返す
                    Dim nUpdate As String = GetUpdateWriteLine(hStrings(iRow), key, writeValue)

                    If (nUpdate IsNot Nothing) Then
                        WriteUpdateValue(filePath, nUpdate, hStrings, iRow - 1, encoding)
                        Return
                    End If
                Next iRow

                ' EOF を発見した場合は挿入する
                WriteInsertKeyValue(filePath, key, writeValue, hStrings, iRow - 1, encoding)
                Return
            End If
        Next i

        ' セクションがなかった場合はセクションも作成する
        WriteInsertSectionKeyValue(filePath, section, key, writeValue, hStrings, encoding)
    End Sub

#End Region

#Region "ReadIniFileSection メソッド"

    Private Shared Function ReadIniFileSection(ByVal filePath As String, ByVal section As String, ByVal encoding As System.Text.Encoding) As IniSection
        section = section.Trim().ToLower()

        ' ファイル名・セクション名がない場合は終了する
        If (filePath = String.Empty OrElse section = String.Empty) Then
            Return Nothing
        End If

        ' 指定したファイルが存在しない場合は Null を返す
        If Not System.IO.File.Exists(filePath) Then
            Return Nothing
        End If

        Using cReader As System.IO.StreamReader = New System.IO.StreamReader(filePath, encoding)
            Try
                Do While (cReader.Peek() > -1)
                    Dim target As String = cReader.ReadLine().TrimStart()

                    ' セクションの始まりかどうか判断する
                    If IsSectionBegin(target, section) Then
                        Dim hSection As IniSection = New IniSection()

                        Do While (cReader.Peek() > -1)
                            target = cReader.ReadLine().TrimStart()

                            ' セクションの終わりかどうか判断する
                            If IsSectionEnd(target) Then
                                Return hSection
                            End If

                            ' イコールがあるかどうか判断する
                            Dim iLength As Integer = target.IndexOf("=")

                            If iLength >= 1 Then
                                Dim key As String = target.Substring(0, iLength - 1).TrimEnd()
                                Dim value As String = TrimDoubleQuote(target.Substring(iLength + 1).Trim())
                                hSection.Add(New IniItem(key, value))
                            End If
                        Loop

                        Return hSection
                    End If
                Loop
            Catch ex As System.Exception
                Throw
            Finally
                If (cReader IsNot Nothing) Then
                    cReader.Close()
                End If
            End Try
        End Using

        Return Nothing
    End Function

#End Region

#Region "WriteIniFileSection メソッド"

    Private Shared Sub WriteIniFileSection(ByVal filePath As String, ByVal section As String, ByVal hSection As IniSection, ByVal encoding As System.Text.Encoding)
        section = section.Trim()

        ' ファイル名・セクション名・キー名がない場合は終了する
        If (filePath = String.Empty OrElse section = String.Empty OrElse (hSection Is Nothing)) Then
            Return
        End If

        ' 既存ファイルがあれば中身をすべて取得する
        Dim hStrings As StringCollection = GetFileContents(filePath, encoding)

        ' 既存ファイルがない場合は終了する
        If (hStrings Is Nothing) Then
            Return
        End If

        ' 最大行数と小文字化したセクションを取得する
        Dim iMaxCount As Integer = hStrings.Count - 1
        Dim nSecLower As String = section.ToLower()

        ' 最大要素数まで 1 要素ずつ読み込む
        For i As Integer = 0 To iMaxCount
            hStrings(i) = hStrings(i).TrimStart()

            ' セクションの始まりかどうか判断する
            If IsSectionBegin(hStrings(i), nSecLower) Then
                Dim iRow As Integer

                ' セクションの終わりを発見した場合は挿入する
                For iRow = i + 1 To iMaxCount
                    If IsSectionEnd(hStrings(iRow).TrimStart()) Then
                        Dim hInserts As StringCollection = GetInsertAllPairOfSection(hStrings, hSection, i + 1, iRow - 1)
                        WriteInsertKeyValue(filePath, hInserts, hStrings, i, iRow, encoding)
                        Return
                    End If
                Next iRow

                ' EOF を発見した場合は挿入する
                Dim hEofInserts As StringCollection = GetInsertAllPairOfSection(hStrings, hSection, i + 1, iMaxCount)
                WriteInsertKeyValue(filePath, hEofInserts, hStrings, i, iRow, encoding)
                Return
            End If
        Next i

        ' セクションがなかった場合はセクションも作成する
        WriteInsertSectionKeyValue(filePath, section, hSection, hStrings, encoding)
    End Sub

#End Region

#Region "GetValueOfMatchKey メソッド"

    Private Shared Function GetValueOfMatchKey(ByVal target As String, ByVal key As String) As String
        Dim iEqual As Integer = target.IndexOf("=")

        If iEqual <= 0 Then
            Return Nothing
        End If

        If target.Length < key.Length Then
            Return Nothing
        End If

        If target.Substring(0, iEqual).TrimEnd().ToLower() <> key Then
            Return Nothing
        End If

        Return TrimDoubleQuote(target.Substring(iEqual + 1).Trim())
    End Function

#End Region

#Region "GetUpdateWriteLine メソッド"

    Private Shared Function GetUpdateWriteLine(ByVal source As String, ByVal key As String, ByVal writeValue As String) As String
        Dim target As String = source.TrimStart()
        Dim iEqual As Integer = target.IndexOf("=")

        If iEqual <= 0 Then
            Return Nothing
        End If

        If target.Length < key.Length Then
            Return Nothing
        End If

        If target.Substring(0, iEqual).TrimEnd().ToLower() = key.ToLower() Then
            Dim iMargin As Integer = (source.Length - target.Length) + (iEqual + 1)
            target = target.Substring(iEqual + 1)
            iMargin += target.Length
            target = target.TrimStart()
            iMargin -= target.Length
            target = target.TrimEnd()
            Dim iLength As Integer = target.Length

            If iLength >= 2 Then
                If target.Substring(0, 1) = DOUBLE_QUOTE Then
                    If target.Substring(iLength - 1) = DOUBLE_QUOTE Then
                        Return source.Substring(0, iMargin) + DOUBLE_QUOTE + writeValue + DOUBLE_QUOTE
                    End If
                End If
            End If

            Return source.Substring(0, iMargin) + writeValue
        End If

        Return Nothing
    End Function

#End Region

#Region "GetInsertAllPairOfSection メソッド"

    Private Shared Function GetInsertAllPairOfSection(ByVal hSources As StringCollection, ByVal hSection As IniSection, ByVal iBegin As Integer, ByVal iEnd As Integer) As StringCollection
        Dim iBrank As Integer = 0
        Dim hStrings As StringCollection = New StringCollection()

        For i As Integer = iBegin To iEnd
            hStrings.Add(hSources(i))

            If hSources(i) = String.Empty Then
                iBrank += 1
            Else
                iBrank = 0
            End If
        Next i

        For i As Integer = 0 To hSection.Count - 1
            Dim iRow As Integer

            For iRow = 0 To iEnd - iBegin - iBrank
                Dim nUpdate As String = GetUpdateWriteLine(hStrings(iRow), hSection.Item(i).Key.Trim(), hSection.Item(i).Value.Trim())

                If (nUpdate IsNot Nothing) Then
                    hStrings(iRow) = nUpdate
                    Exit For
                End If
            Next iRow

            If iRow > iEnd - iBegin - iBrank Then
                iEnd += 1

                If iEnd - iBegin - iBrank <= hStrings.Count - 1 Then
                    hStrings(iEnd - iBegin - iBrank) = hSection.Item(i).Key.Trim() & " = " & hSection.Item(i).Value.Trim()
                    hStrings.Add(String.Empty)
                Else
                    hStrings.Add(hSection.Item(i).Key.Trim() & " = " & hSection.Item(i).Value.Trim())
                End If
            End If
        Next i

        Return hStrings
    End Function

#End Region

#Region "WriteUpdateValue メソッド"

    Private Shared Sub WriteUpdateValue(ByVal filePath As String, ByVal writeValue As String, ByVal hStrings As StringCollection, ByVal iCurrent As Integer, ByVal encoding As System.Text.Encoding)
        Using hWriter As System.IO.StreamWriter = New System.IO.StreamWriter(filePath, False, encoding)
            Try
                For i As Integer = 0 To iCurrent
                    hWriter.WriteLine(hStrings(i))
                Next i

                hWriter.WriteLine(writeValue)

                For i As Integer = iCurrent + 2 To hStrings.Count - 1
                    hWriter.WriteLine(hStrings(i))
                Next i
            Catch ex As System.Exception
                Throw
            Finally
                If (hWriter IsNot Nothing) Then
                    hWriter.Close()
                End If
            End Try
        End Using
    End Sub

#End Region

#Region "WriteInsertKeyValue メソッド (+1)"

    Private Shared Sub WriteInsertKeyValue(ByVal filePath As String, ByVal key As String, ByVal writeValue As String, ByVal hStrings As StringCollection, ByVal iCurrent As Integer, ByVal encoding As System.Text.Encoding)
        Dim iBegin As Integer

        For iBegin = iCurrent To 0 Step -1
            If hStrings(iBegin) <> String.Empty Then
                Exit For
            End If
        Next iBegin

        Using hWriter As System.IO.StreamWriter = New System.IO.StreamWriter(filePath, False, encoding)
            Try
                For i As Integer = 0 To iBegin
                    hWriter.WriteLine(hStrings(i))
                Next i

                hWriter.WriteLine(key.Trim() + " = " + writeValue.Trim())

                For i As Integer = iBegin + 1 To hStrings.Count - 1
                    hWriter.WriteLine(hStrings(i))
                Next i
            Catch ex As System.Exception
                Throw
            Finally
                If (hWriter IsNot Nothing) Then
                    hWriter.Close()
                End If
            End Try
        End Using
    End Sub

    Private Shared Sub WriteInsertKeyValue(ByVal filePath As String, ByVal hInserts As StringCollection, ByVal hSources As StringCollection, ByVal iBegin As Integer, ByVal iEnd As Integer, ByVal encoding As System.Text.Encoding)
        Using hWriter As System.IO.StreamWriter = New System.IO.StreamWriter(filePath, False, encoding)
            Try
                For i As Integer = 0 To iBegin
                    hWriter.WriteLine(hSources(i))
                Next i

                For i As Integer = 0 To hInserts.Count - 1
                    hWriter.WriteLine(hInserts(i))
                Next i

                For i As Integer = iEnd To hSources.Count - 1
                    hWriter.WriteLine(hSources(i))
                Next i
            Catch ex As System.Exception
                Throw
            Finally
                If (hWriter IsNot Nothing) Then
                    hWriter.Close()
                End If
            End Try
        End Using
    End Sub

#End Region

#Region "WriteInsertSectionKeyValue メソッド (+1)"

    Private Shared Sub WriteInsertSectionKeyValue(ByVal filePath As String, ByVal section As String, ByVal key As String, ByVal writeValue As String, ByVal hSources As StringCollection, ByVal encoding As System.Text.Encoding)
        If (hSources Is Nothing) Then
            Return
        End If

        Using hWriter As System.IO.StreamWriter = New System.IO.StreamWriter(filePath, False, encoding)
            Try
                Dim iLast As Integer

                For iLast = 0 To hSources.Count - 1
                    hWriter.WriteLine(hSources(iLast))
                Next iLast

                If iLast > 0 Then
                    If hSources(iLast - 1) <> String.Empty Then
                        hWriter.WriteLine()
                    End If
                End If

                hWriter.WriteLine("[" & section & "]")
                hWriter.WriteLine(key & " = " & writeValue.Trim())
            Catch ex As System.Exception
                Throw
            Finally
                If (hWriter IsNot Nothing) Then
                    hWriter.Close()
                End If
            End Try
        End Using
    End Sub

    Private Shared Sub WriteInsertSectionKeyValue(ByVal filePath As String, ByVal section As String, ByVal hSection As IniSection, ByVal hSources As StringCollection, ByVal encoding As System.Text.Encoding)
        If (hSources Is Nothing) Then
            Return
        End If

        Using hWriter As System.IO.StreamWriter = New System.IO.StreamWriter(filePath, False, encoding)
            Try
                Dim iLast As Integer

                For iLast = 0 To hSources.Count - 1
                    hWriter.WriteLine(hSources(iLast))
                Next iLast

                If iLast > 0 Then
                    If hSources(iLast - 1) <> String.Empty Then
                        hWriter.WriteLine()
                    End If
                End If

                hWriter.WriteLine("[" & section & "]")

                For i As Integer = 0 To hSection.Count - 1
                    hWriter.WriteLine(hSection.Item(i).Key.Trim() & " = " & hSection.Item(i).Value.Trim())
                Next i
            Catch ex As System.Exception
                Throw
            Finally
                If (hWriter IsNot Nothing) Then
                    hWriter.Close()
                End If
            End Try
        End Using
    End Sub

#End Region

#Region "IsSectionBegin メソッド"

    Private Shared Function IsSectionBegin(ByVal target As String, ByVal section As String) As Boolean
        If target = String.Empty OrElse target.Substring(0, 1) <> "[" Then
            Return False
        End If

        target = target.Substring(1).TrimStart()
        Dim iLength As Integer = section.Length

        If target.Length < iLength Then
            Return False
        End If

        If target.Substring(0, iLength).ToLower() = section Then
            target = target.Substring(iLength).TrimStart()

            If target = String.Empty OrElse target.Substring(0, 1) = "]" Then
                Return True
            End If
        End If

        Return False
    End Function

#End Region

#Region "IsSectionEnd メソッド"

    Private Shared Function IsSectionEnd(ByVal target As String) As Boolean
        If target <> String.Empty AndAlso target.Substring(0, 1) = "[" Then
            Return True
        End If

        Return False
    End Function

#End Region

#Region "GetFileContents メソッド"

    Private Shared Function GetFileContents(ByVal filePath As String, ByVal encoding As System.Text.Encoding) As StringCollection
        If Not System.IO.File.Exists(filePath) Then
            Return Nothing
        End If

        Using cReader As System.IO.StreamReader = New System.IO.StreamReader(filePath, encoding)
            Try
                Dim hStrings As StringCollection = New StringCollection()

                For i As Integer = 0 To MAX_LINE
                    If cReader.Peek() < 0 Then
                        Exit For
                    End If

                    hStrings.Add(cReader.ReadLine())
                Next i

                Return hStrings
            Catch ex As System.Exception
                Throw
            Finally
                If (cReader IsNot Nothing) Then
                    cReader.Close()
                End If
            End Try
        End Using
    End Function

#End Region

#Region "TrimDoubleQuote メソッド"

    Private Shared Function TrimDoubleQuote(ByVal target As String) As String
        Dim iLength As Integer = target.Length

        If iLength >= 2 Then
            If target.Substring(0, 1) = DOUBLE_QUOTE Then
                If target.Substring(iLength - 1) = DOUBLE_QUOTE Then
                    target = target.Substring(1, iLength - 2)
                End If
            End If
        End If

        Return target
    End Function

#End Region

#Region "ToDouble メソッド"

    Private Shared Function ToDouble(ByVal target As String) As Double
        Dim result As Double

        If Double.TryParse(target, System.Globalization.NumberStyles.Number, Nothing, result) Then
            Return result
        Else
            Return 0.0#
        End If
    End Function

#End Region

#Region "ToInt32 メソッド"

    Private Shared Function ToInt32(ByVal target As String) As Integer
        Dim result As Double

        If Double.TryParse(target, System.Globalization.NumberStyles.Number, Nothing, result) Then
            Return CInt(IIf(result >= 0, System.Math.Floor(result), System.Math.Ceiling(result)))
        Else
            Return 0
        End If
    End Function

#End Region
#End Region

End Class

#End Region

#Region "IniItem クラス"

''' --------------------------------------------------------------------------------
''' <summary>
'''     INI ファイルのセクション内にある Key と Value が対になったアイテムを表します。
''' </summary>
''' --------------------------------------------------------------------------------

Public Class IniItem

#Region "コンストラクタ"

    ''' --------------------------------------------------------------------------------
    ''' <summary>
    '''     Kobelcosystem.Ini.IniItem の新しいインスタンスを初期化します。</summary>
    ''' <param name="key">
    '''     値との組み合わせで定義されているキー。</param>
    ''' <param name="value">
    '''     キーに関連付けられている値</param>
    ''' --------------------------------------------------------------------------------
    Public Sub New(ByVal key As String, ByVal value As String)
        Me.Key = key
        Me.Value = value
    End Sub

#End Region

#Region "Key プロパティ"

    Private _Key As String

    ''' --------------------------------------------------------------------------------
    ''' <summary>
    '''     キーと値の組み合わせ内のキーを取得または設定します。
    ''' </summary>
    ''' --------------------------------------------------------------------------------
    Public Property Key() As String
        Get
            Return _Key
        End Get
        Set(ByVal value As String)
            _Key = value
        End Set
    End Property

#End Region

#Region "Value プロパティ"

    Private _Value As String

    ''' --------------------------------------------------------------------------------
    ''' <summary>
    '''     キーと値の組み合わせ内の値を取得または設定します。
    ''' </summary>
    ''' --------------------------------------------------------------------------------
    Public Property Value() As String
        Get
            Return _Value
        End Get
        Set(ByVal value As String)
            _Value = value
        End Set
    End Property

#End Region

End Class

#End Region

#Region "IniSection クラス"

''' --------------------------------------------------------------------------------
''' <summary>
'''     KobelcoSystem.Ini.IniItem を複数格納しているセクションを表します。
''' </summary>
''' --------------------------------------------------------------------------------

Public Class IniSection
    Inherits System.Collections.ArrayList

#Region "Item インデクサ"

    ''' --------------------------------------------------------------------------------
    ''' <summary>
    '''     コレクション内のアイテムを表すインデクサです。
    ''' </summary>
    ''' --------------------------------------------------------------------------------
    Public Shadows Property Item(ByVal index As Integer) As IniItem
        Get
            Return DirectCast(MyBase.Item(index), IniItem)
        End Get
        Set(ByVal value As IniItem)
            MyBase.Item(index) = value
        End Set
    End Property

#End Region

#Region "Add メソッド"

    ''' --------------------------------------------------------------------------------
    ''' <summary>
    '''     KobelcoSystem.Ini.IniSection の末尾に、IniItem オブジェクトを追加します。</summary>
    ''' <param name="value">
    '''     末尾に追加する KobelcoSystem.Ini.IniItem。</param>
    ''' <returns>
    '''     追加された位置の KobelcoSystem.Ini.IniSection インデックス。</returns>
    ''' --------------------------------------------------------------------------------
    Public Shadows Function Add(ByVal value As IniItem) As Integer
        Return MyBase.Add(value)
    End Function

#End Region

End Class

#End Region

#Region "IniSectionCollection クラス"

''' --------------------------------------------------------------------------------
''' <summary>
'''     KobelcoSystem.Ini.IniSection のコレクションを表します。
''' </summary>
''' --------------------------------------------------------------------------------

Public Class IniSectionCollection
    Inherits System.Collections.ArrayList

#Region "Item インデクサ"

    ''' --------------------------------------------------------------------------------
    ''' <summary>
    '''     コレクション内のアイテムを表すインデクサです。
    ''' </summary>
    ''' --------------------------------------------------------------------------------
    Public Shadows Property Item(ByVal index As Integer) As IniSection
        Get
            Return DirectCast(MyBase.Item(index), IniSection)
        End Get
        Set(ByVal value As IniSection)
            MyBase.Item(index) = value
        End Set
    End Property

#End Region

#Region "Add メソッド"

    ''' --------------------------------------------------------------------------------
    ''' <summary>
    '''     KobelcoSystem.Ini.IniSectionCollection の末尾に、IniSection オブジェクトを追加します。</summary>
    ''' <param name="value">
    '''     末尾に追加する KobelcoSystem.Ini.IniSection。</param>
    ''' <returns>
    '''     追加された位置の KobelcoSystem.Ini.IniSectionCollection インデックス。</returns>
    ''' --------------------------------------------------------------------------------
    Public Shadows Function Add(ByVal value As IniSection) As Integer
        Return MyBase.Add(value)
    End Function

#End Region

End Class

#End Region
