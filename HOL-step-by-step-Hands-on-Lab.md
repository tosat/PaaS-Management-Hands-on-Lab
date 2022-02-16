![Microsoft Cloud Workshop](images/ms-cloud-workshop.png)

Network Hands-on lab  
February 2022

<br />

**Contents**

- [Exercise 1: サービス・リソース正常性](#exercise-1-サービス・リソース正常性)

- [Exercise 2: PaaS サービスの保護](#exercise-2-paas-サービスの保護)

<br />

## Exercise 1: サービス・リソース正常性

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

### Task 1: Defender for Cloud の有効化

- Azure Portal のトップ画面から **ツール** に表示される **Microsoft Defender for Cloud** をクリック

  <img src="images/defender-for-cloud-01.png" />

- **環境設定** ページを表示し、保護を行うサブスクリプションをクリック

- **すべての Microsoft Defender for Cloud プランの有効化** を選択

- **すべて有効にする** をクリックし、すべてのリソースの保護の有効化をオンに設定

  <img src="images/defender-for-cloud-02.png" />

- **保存** をクリック

  ※最初の 30 日間は無料でお使いいただけます

### Application Insights  

### App Service - 診断設定

### SQL Server - 監査

### SQL Database - データの検出と分類

### SQL Database - 診断設定


