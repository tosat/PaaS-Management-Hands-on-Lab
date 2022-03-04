![Microsoft Cloud Workshop](images/ms-cloud-workshop.png)

Network Hands-on lab  
February 2022

<br />

**Contents**

- [Exercise 1: サービス・リソース正常性](#exercise-1-サービス・リソース正常性)

  - [Task 1: サービス正常性アラートの追加](#task-1-サービス正常性アラートの追加)

  - [Task 2: リソース正常性アラートの追加](#task-2-リソース正常性アラートの追加)

- [Exercise 2: PaaS サービスの保護](#exercise-2-paas-サービスの保護)

  - [Task 1: Defender for Cloud の有効化](#task-1-defender-for-cloud-の有効化)

  - [Task 2: Application Insights の追加](#task-2-application-insights-の追加)

  - [Task 3: SQL Database サーバーレベルの監査設定](#task-3-sql-database-サーバーレベルの監査設定)

  - [Task 4: SQL Database 診断設定](#task-4-sql-database-診断設定)

  - [Task 5: SQL Database データの検出と分類](#task-5-sql-database-データの検出と分類)

- [Exercise 3: App Service の監視](#exercise-3-app-service-の監視)

  - [Task 1: Load Test の実行](#task-1-load-test-の実行)

  - [Task 2: メトリックの分析 (App Service)](#task-2-メトリックの分析-app-service)

  - [Task 3: メトリックの分析 (SQL Database)](#task-2-メトリックの分析-sql-database)

  - [Task 4: アプリケーションの動作確認](#task-3-アプリケーションの動作確認)

  - [Task 5: アプリケーション エラーの特定](#task-4-アプリケーション-エラーの特定)

  - [Task 6: データベースへのアクセスを含むリクエストの確認](#task-5-データベースへのアクセスを含むリクエストの確認)

- [Exercise 4: SQL Database の監査](#exercise-4-sql-database-の監査)

<br />

### 使用する環境

<img src="images/hands-on-architecture.png" />

<br />

### アプリケーションの動作確認

  - App Service の **概要** ページの **URL** をクリック

  - 新しいタブでアプリケーションが表示

    <img src="images/sample-app-01.png" />
  
  - **Managed Policy Holders** をクリック

    <img src="images/sample-app-02.png" />

    ※データベースからレコードを取得して表示
  
  - **Details** をクリックし、詳細情報を表示

    <img src="images/sample-app-03.png" />
  
  - **File Path** に表示される PDF ファイルへのリンクをクリック

    <img src="images/sample-app-04.png" />

    ※新しいタブで PDF ファイルが表示
  
  - 画面上部の **File Upload** をクリック

    <img src="images/sample-app-05.png" />

    ※ファイルのアップロードを行う画面が表示

<br />

## Exercise 1: サービス・リソース正常性

<img src="images/exercise-01.png" />

### Task 1: サービス正常性アラートの追加

- Azure ポータルのトップ画面から **検索バー** のテキストボックスに **正常性** と入力

- 表示される候補より **サービス正常性** を選択

  <img src="images/service-health-01.png" />

- **＋ サービス正常性アラートの追加** をクリック

  <img src="images/add-service-health-alert-01.png" />

- **サブスクリプション**、**サービス**、**リージョン** を選択

  <img src="images/add-service-health-alert-02.png" />

- **Service Health の基準** セクションの **イベントの種類** から **サービスの問題**, **計画メンテナンス** を選択

  <img src="images/add-service-health-alert-03.png" />

- **アクション** セクションの **アクション グループの追加** をクリック

  <img src="images/add-service-health-alert-04.png" />

  - 画面右の **アクション グループの追加** から **＋ アクション グループの作成** をクリック

    <img src="images/create-action-group-01.png" />
  - アクション グループの作成画面が表示

    - 必要項目を入力し **次へ: 通知 >** をクリック

      - サブスクリプション： ワークショップで使用中のサブスクリプションを選択

      - リソース グループ： ワークショップで使用するリソース グループを選択

      - アクション グループ名： **ag-MCW**（任意）

      - 表示名： **ag-MCW**（任意）

      <img src="images/create-action-group-02.png" />
    
    - **通知の種類** リストから **電子メール/SMS メッセージ/プッシュ/音声** を選択

      <img src="images/create-action-group-03.png" />
    
    - 画面右から通知を設定し **OK** をクリック

      **電子メール** にチェックを付け、通知を受け取る電子メール アドレスを入力

      <img src="images/create-action-group-04.png" />

    - **名前** に **mail** と入力し **確認および作成** をクリック

      <img src="images/create-action-group-05.png" />

    - 表示内容を確認し問題がなければ **作成** をクリック

      <img src="images/create-action-group-06.png" />

- 作成したアクション グループが表示

  <img src="images/add-service-health-alert-05.png" />

- **アラート ルールの詳細** セクションで **アラート ルール名** を入力し、**リソース グループ** を選択

  **作成時にアラート ルールを有効にする** を **オン** に設定（既定）

  <img src="images/add-service-health-alert-06.png" />

- **アラート ルールの作成** をクリック

  <img src="images/add-service-health-alert-07.png" />

<br />

### Task 2: リソース正常性アラートの追加

- **リソース正常性** ページを表示し **＋ リソース正常性アラートの追加** をクリック

  <img src="images/add-resources-health-alert-01.png" />

- **アラートの対象** の選択

  - **サブスクリプション**： ワークショップで使用中のサブスクリプションを選択

  - **リソースの種類**： **App Service Plan**, **SQL database**, **Storage account** を選択

  - **リソース グループ**： すべて選択済み（既定）/ **将来のすべてのリソース グループを含める**： オン

  - **リソース**： すべて選択済み（既定） / **今後すべてのリソースを含める**： オン

  <img src="images/add-resources-health-alert-02.png" />

- **アラートの条件** を指定

  - **イベントの状態**： すべて選択済み（既定）

  - **現在のリソースの状態**： すべて選択済み（既定）

  - **以前のリソースの状態**： ４個を選択済み（既定）

  - **理由の種類**： すべて選択済み（既定）

  <img src="images/add-resources-health-alert-04.png" />

- **アクション**

  アクション グループにサービス正常性アラートの作成時に作成したアクション グループ（ag-MCW）を指定

  <img src="images/add-resources-health-alert-05.png" />

- **アラート ルールの詳細** セクションで **アラート ルール名** を入力し、**リソース グループ** を選択

  **作成時にアラート ルールを有効にする** を **オン** に設定（既定）

  <img src="images/add-resources-health-alert-06.png" />

- **アラート ルールの作成** をクリック

  <img src="images/add-resources-health-alert-07.png" />

<br />

## Exercise 2: PaaS サービスの保護

<img src="images/exercise-02.png" />

### Task 1: Defender for Cloud の有効化

- Azure Portal のトップ画面から **ツール** に表示される **Microsoft Defender for Cloud** をクリック

  <img src="images/defender-for-cloud-01.png" />

- **環境設定** ページを表示し、保護を行うサブスクリプションをクリック

- **すべての Microsoft Defender for Cloud プランの有効化** を選択

- **すべて有効にする** をクリックし、すべてのリソースの保護の有効化をオンに設定

  <img src="images/defender-for-cloud-02.png" />

- **保存** をクリック

  ※最初の 30 日間は無料でお使いいただけます

<br />

### Task 2: Application Insights の追加

- App Service の管理ブレードへ移動し、**Application Insights** ページを表示

- **Turn on Application Insights** をクリック

  <img src="images/add-application-insights-01.png" />

- **Create new resource** を選択

  - **New resource name**： Application Insights の名前を入力（任意）

  - **Location**： App Service と同じ地域を選択

  - **Log Analytics Workspace**： サブスクリプションに作成済みの Log Analytics Workspace を選択

  <img src="images/add-application-insights-02.png" />

- **Apply** をクリック

<br />

### App Service への診断設定の追加

- App Service の管理ブレードの **診断設定** ページを表示

- **＋ 診断設定を追加する** をクリック

  <img src="images/app-diag-01.png" />

- 名前を入力し、取得するログ・メトリック、出力先を選択

  - **診断設定の名前**： *diag-<App service 名>* （任意）

  - **ログ**： すべて選択

  - **メトリック**： すべて選択

  - **宛先の詳細**：

    - **Log Analytics ワークスペースへの送信**： オン

    - **サブスクリプション**： ワークショップで使用中のサブスクリプション

    - **Log Analytics ワークスペース**： サブスクリプションに作成済みの Log Analytics ワークスペース

    <img src="images/app-diag-02.png" />

- **保存** をクリック

<br />

### Task 3: SQL Database サーバーレベルの監査設定

- **ContosoInsurance** データベースの管理ブレードへ移動

- **概要** ページから **サーバー名** をクリック

- **セキュリティ** - **監査** を選択

  - **Azure SQL 監査を有効にする**： オン

  - **監査ログの保存先**： ログ分析

    - **サブスクリプション**： ワークショップで使用中のサブスクリプション

    - **ログ分析**： サブスクリプションに作成済みの Log Analytics ワークスペース

    <img src="images/sql-audit-01.png" />

- **保存** をクリック

<br />

### Task 4: SQL Database 診断設定

- **ContosoInsurance** データベースの管理ブレードへ移動

- **監視** - **診断設定** を選択

- **＋ 診断設定を追加する** をクリック

  <img src="images/sql-diag-01.png" />

- 名前を入力し、取得するログ・メトリック、出力先を選択

  - **診断設定の名前**： *diag-ContosoInsurance* （任意）

  - **ログ**： **all logs** を選択

  - **メトリック**： すべて選択

  - **宛先の詳細**：

    - **Log Analytics ワークスペースへの送信**： オン

    - **サブスクリプション**： ワークショップで使用中のサブスクリプション

    - **Log Analytics ワークスペース**： サブスクリプションに作成済みの Log Analytics ワークスペース

    <img src="images/sql-diag-02.png" />

<br />

### Task 5: SQL Database データの検出と分類

- **ContosoInsurance** データベースの管理ブレードへ移動

- **セキュリティ** - **データの検出と分類** を選択

- **分類の推奨事項が指定された 7 個の列が見つかりました →** のメッセージをクリック

  <img src="images/data-classification-01.png" />

- **すべて選択** にチェックを付け、**選択した推奨事項を受け入れます** をクリック

  <img src="images/data-classification-02.png" />

- **保存** をクリック

- **概要** タブでデータ分類の状況を確認

  <img src="images/data-classification-03.png" />

<br />

## Exercise 3: App Service の監視

<img src="images/exercise-03.png" />

### Task 1: Load Test の実行

- Azure Load Testing の管理ブレードへ移動

- **Tests** ページを表示し、登録済みのテストをクリック

  <img src="images/execute-load-test-01.png" />

- **Run** をクリックし、テストを実行

  <img src="images/execute-load-test-02.png" />

- 開始したテストをクリック

  <img src="images/execute-load-test-03.png" />

- 実行状況を確認

  <img src="images/execute-load-test-04.png" />

  ※ 2 分ほどで終了

  - テストの設定

    <img src="images/jmeter.png" />

    20 スレッドで 120 秒間アプリケーションの各ページに GET リクエストを要求

<br />

### Task 2: メトリックの分析 (App Service)

- App Service の管理ブレードへ移動し、**監視** - **メトリック** を選択

- **メトリック** から **Average memory working set** を選択

  <img src="images/app-metrics-01.png" />

- 使用された平均メモリ量（MB）を確認

  <img src="images/app-metrics-02.png" />

- **メトリック** を **Requests** に変更

  <img src="images/app-metrics-03.png" />

- HTTP ステータス コードを問わない要求数の合計が表示

  <img src="images/app-metrics-04.png" />

<br />

### Task 2: メトリックの分析 (SQL Database)

- SQL Database の管理ブレードへ移動し、**監視** - **メトリック** を選択

  <img src="images/database-metric-01.png" />

- メトリックから **Data space used percent** を選択

  <img src="images/database-metric-02.png" />

- **新しいアラート ルール** をクリック

  <img src="images/database-metric-03.png" />

- 条件名をクリック

  <img src="images/database-metric-04.png" />

- シグナル ロジックの構成で **しきい値** を **80%** に設定し **完了** をクリック

  <img src="images/database-metric-05.png" />

  ※条件名のアイコンがグリーンに変更され、シグナル ロジックの構成に問題がないことを確認

  <img src="images/database-metric-04_1.png" />

- **アクション** タブで **＋ アクション グループの追加** をクリック

  正常性アラート作成時に作成したアクション グループを選択

  <img src="images/database-metric-06.png" />

- **アラート ルール名** を入力し **確認および作成** をクリック

  <img src="images/database-metric-07.png" />

- 設定内容に問題がないことを確認し **作成** をクリック

<br />

### Task 4: アプリケーションの動作確認

- App Service の管理ブレードの **概要** ページから URL をクリック

- 新しいタブでアプリケーションが表示

  <img src="images/sample-app-01.png" />

- 画面上部の **File Upload** をクリック

- **ファイルの選択** をクリックし、ローカルにある任意のファイルを選択

  **Submit** をクリックし、Blob ストレージへファイルをアップロード

  <img src="images/upload-file-01.png" />

- 正常にファイルがアップロードされたことを確認

  <img src="images/upload-file-02.png" />

- 同様の手順で前の手順と同じファイルをアップロード

- エラーが表示

  <img src="images/upload-file-03.png" />

<br />

### Task 5: アプリケーション エラーの特定

- App Service の管理ブレードから **監視** - **ログ** を選択

  <img src="images/app-service-log-01.png" />

- HTTP ステータス コードの合計を表示する以下のクエリを実行

  ```
  AppServiceHTTPLogs 
  | where TimeGenerated > ago(12h) 
  | extend statusName = tostring(ScStatus)
  | summarize statuscount=count() by statusName 
  | render piechart
  ```

- Http ステータス コードごとの応答数を確認

  <img src="images/app-service-log-02.png" />

- エラー ログを抽出するクエリを実行

  ```
  AppServiceAppLogs 
  | where Level == "Error"
  | project TimeGenerated, Level, ResultDescription, _ResourceId
  ```

- エラーの発生時刻と内容を確認

  <img src="images/app-service-log-03.png" />

- App Service の管理ブレードの **設定** - **Application Insights** を選択

- **View Application Insights data** をクリック

- Application Insights の概要ページが表示

  <img src="images/appi-failure-requests-01.png" />

- **Failures** を選択（または Failed requests のグラフをクリック）

- Top 3 exception types の **Storage Exception** をクリック

  画面右に表示される **Select a sample exception** - **Suggested** の項目をクリックし詳細を確認

  <img src="images/appi-failure-requests-02.png" />

- **EXCEPTION** の **Message** を確認

  <img src="images/appi-failure-requests-03.png" />

  ※ファイル名の重複がエラーの原因であることを特定

<br />

### Task 6: データベースへのアクセスを含むリクエストの確認

- Application Insights の管理ブレードを表示、**Performance** を選択

- **GET /PolicyHolder** を選択し **Drill into ...** の **xxx Samples** をクリック

  <img src="images/appi-server-requests-01.png" />

- 画面右の **Select a sample operation** - **Suggested** の項目をクリックし詳細を確認

  <img src="images/appi-server-requests-02.png" />

- データベースからの応答時間、実行されたクエリを確認

  <img src="images/appi-server-requests-03.png" />

<br />

## Exercise 4: SQL Database の監査

<img src="images/exercise-04.png" />

- SQL Server の管理ブレードから **監視** - **ログ** を選択

- SQL ステートメントを含む監査ログを抽出

  ```
  AzureDiagnostics
  | where Category == 'SQLSecurityAuditEvents'
  | where statement_s != ''
  ```

  <img src="images/sql-audit-log-01.png" />

- 結果を展開し、ユーザー名、実行された SQL ステートメント、分類されたデータへのアクセス等を確認

  <img src="images/sql-audit-log-02.png" />

- 分類されたデータへのアクセス数を算出するクエリを実行

   ```
   search *
   | where Category == 'SQLSecurityAuditEvents' and data_sensitivity_information_s != ""
   | extend parsed=parse_xml(data_sensitivity_information_s).sensitivity_attributes.sensitivity_attribute
   | mvexpand parsed
   | extend info_type = tostring(parsed["@information_type"])
   | where info_type != ""
   | summarize dcount = dcount(sequence_group_id_g) by info_type
   | order by dcount desc
   ```

- 結果を確認

  <img src="images/sql-audit-log-03.png" />
