Network Hands-on lab  
February 2022

<br />

### 参考情報
- <a href="https://docs.microsoft.com/ja-jp/azure/cloud-adoption-framework/ready/azure-best-practices/resource-naming">名前付け規則を定義する</a>

- <a href="https://docs.microsoft.com/ja-jp/azure/cloud-adoption-framework/ready/azure-best-practices/resource-abbreviations">Azure リソースの種類に推奨される省略形</a>

<br />

## リソースの展開

<br />

[![Deploy to Azure](https://aka.ms/deploytoazurebutton)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fkohei3110%2FPaaS-Management-Hands-on-Lab%2Fmaster%2Ftemplates%2Fdeploy-azure-resources.json)

### パラメーター

- **storageAccountName**: ストレージ アカウント名

- **sqlServerName**: SQL Server 名

- **sqlAdministratorLogin**: SQL 管理者

- **sqlAdministratorPassword**: SQL 管理者パスワード

- **webAppName**: Web App 名

- **logAnalyticsWorkspace**: LogAnalytics ワークスペース名

- **loadtestingWorkspace**: Load Testing ワークスペース名

<br />

※事前にリソース グループの作成が必要

※選択したリソース グループのリージョンにすべてのリソースが展開

<br />

## リソースの設定

※ **GitHub Actoins を使用してファイルのコピー、Web アプリケーションの展開を行うためにリポジトリを自身の GitHub アカウントに Fork してください。**

- Blob ストレージ

  - コンテナの作成

    - Web アプリケーションで使用する PDF ファイルの保存コンテナ
    
      名前： **policydocuments**　パブリック アクセス レベル： **BLOB (BLOB 専用の匿名読み取りアクセス)**

    - SQL Database の復元に使用する .bacpac ファイルの保存コンテナ
    
      名前： **bacpac**　パブリック アクセス レベル： **プライベート (匿名アクセスはありません)**

  - 作成した２つのコンテナの SAS トークンの作成

    - **共有アクセス トークン** メニューを選択

    - **アクセス許可** から **読み取り**, **追加**, **作成**, **書き込み**, **削除**, **リスト** を選択

      <img src="images/create-sas-token-01.png" />

    - **SAS トークンおよび URL を生成** をクリック

    - **BLOB SAS URL** をコピー

      <img src="images/create-sas-token-02.png" />

  - コンテンツのコピー

- GitHub リポジトリからファイルをコピー

  - <a href="../../actions">GitHub Actions</a> へ移動

  - **I understand my workflows, go ahead and enable them** をクリック

    <img src="images/allow-workflow-01.png" />

    ※フォークされたリポジトリで無効にされたワークフローを許可

    ※メッセージが表示されない場合は、GitHub へサインインが必要

  - **** を選択し **Run workflow** をクリック

    <img src="images/launch-workflow-01.png" />
  
  - コピーしたコンテナの BLOB SAS URL をパラメーターへ貼り付け

    <img src="images/launch-workflow-02.png" />

    - **SAS Token of PolicyDocument container**: policydocuments コンテーの BLOB SAS URL

    - **SAS Token of backpac container**: bacpac コンテナーの BLOB SAS URL

  - **Run workflow** をクリックして、ワークフローを実行

<br />

- SQL Database の作成

  - **概要** ページの **データベースのインポート** をクリック

    <img src="images/import-database-01.png" />
  
  - バックアップ ファイルを選択し、データベースを構成

    - **バックアップの選択**: 先の手順でコピーした backpac ファイルを選択

    - **価格レベル**: 任意（サーバーレス、Gen5、1 vCore など低価格なもの）

    - **データベース名**: ContosoInsurane

    - **照合順序**: SQL_Latin1_General_CP1_CI_AS

    - **認証の種類**: SQL Server

    - **サーバー管理者ログイン**、**パスワード**: リソース作成時に指定したものを入力

      <img src="images/import-database-02.png" />
  
  - **OK** をクリックし、データベースの復元を実行

<br />

- Web アプリケーションの展開

  - WebApps の **概要** ページで **発行プロファイルの取得** をクリック

  - ダウンロードしたファイルを保存

  - GitHub リポジトリの **Settings** を表示、**Actions secrets** へ新しいシークレットを作成

    - シークレット名: **AZURE_WEBAPP_PUBLISH_PROFILE**

    - 値: ダウンロードした発行プロファイルの内容を貼り付け

      <img src="images/add-new-secret.png" />
  
  - GitHub Actions のワークフローを実行し、アプリケーションを展開

    - **Run workflow** をクリックし、展開先の Web Apps の名前をパラメータへ入力

    - **Run workflow** をクリック

<br />

- Web アプリケーションの構成

  - WebApps の **構成** ページで、以下４つのアプリケーション設定を追加

    - 名前: **StorageAccountName** / 値: ストレージ アカウント名

    - 名前: **ContainerName** / 値: **policydocuments**  (PDF ファイルを保存したコンテナ名)

    - 名前: **BlobConnectionString** / 値: ストレージ アカウントへの接続文字列

    - 名前: **SqlConnectionString** / 値: ContosoInsurane データベースへの接続文字列
  
  - **保存** をクリックし、アプリケーションを再起動

<br />

- アプリケーションの動作確認

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

- Load Test の作成

  - <a href="https://raw.githubusercontent.com/kohei3110/PaaS-Management-Hands-on-Lab/master/src/Contents/SampleTest.jmx">JMX ファイル</a> の内容をコピーし、メモ帳等に貼り付け

  - **app-service-endpoint-url** を展開した App Service の FQDN へ置換

    ※ App Service の名前が app1 の場合 app1.azurewebsites.net

  - .jmx の拡張子を付けローカル ドライブへ保存

  <br />

  - リソース グループのアクセス制御から **ロールの割り当ての追加** を実行

  - **ロード テスト共同作成者** にユーザーを追加

    <img src="images/add-role-member-01.png" />
  
  <br />

  - Azure Load Testing の **Tests** ページを表示

    <img src="images/create-load-test-01.png" />
  
  - **Create test** をクリック

    <img src="images/create-load-test-02.png" />
  
  - **Test name** へ **mcw-test** と入力し **Next: Test plan >** をクリック

    <img src="images/create-load-test-03.png" />

  - 前の手順で作成した .jmx ファイルをアップロード

    <img src="images/create-load-test-04.png" />
  
  - **Monitoring** タブで **App Service** と **SQL Database** を追加

    <img src="images/create-load-test-05.png" />
  
  - **Review + create** をクリックし、入力内容を確認後 **Create** をクリック

  - テストが正常に終了することを確認
