![Microsoft Cloud Workshop](images/ms-cloud-workshop.png)

Network Hands-on lab  
February 2022

<br />

**Contents**

- [Appendix: 1 Microsoft Defender for Cloud のアラート通知](#appendix-1-microsoft-defender-for-cloud-のアラート通知)

- [Appendix: 2 マネージド ID を使用したストレージへのアクセス](#appendix-2-マネージド-id-を使用したストレージへのアクセス)

- [Appendix: 3 マネージド ID を使用した SQL Database へのアクセス](#appendix-3-マネージド-id-を使用した-sql-database-へのアクセス)

<br />

## Appendix: 1 Microsoft Defender for Cloud のアラート通知

- Azure Portal のトップ画面から **ツール** に表示される **Microsoft Defender for Cloud** をクリック

  <img src="images/defender-for-cloud-01.png" />

- **ワークフローの自動化** を選択し **＋ ワークフローの自動化の追加** をクリック

  <img src="images/defender-for-cloud-workflow-01.png" />

- **名前** を入力し、**サブスクリプション**、**リソース グループ**、**トリガー条件** を選択

  - トリガーの条件

    - **Defender for Cloud のデータ型**： セキュリティ アラート

    - **アラートの重要度**： すべて選択

  操作の **Logic Apps ページにアクセスします** をクリック

  <img src="images/defender-for-cloud-workflow-02.png" />

- 新しいタブでロジック アプリの画面が表示、**＋ 追加** をクリック

  <img src="images/create-logic-apps-01.png" />

- ロジック アプリの作成

  - **基本** タブ

    - **サブスクリプション**： ワークショップで使用中のサブスクリプションを選択

    - **リソース グループ**： ワークショップで使用中のリソース グループを選択

    - **ロジック アプリ名**： 任意（2文字以上、英数字、ハイフンを使用し一意となるよう入力）

    - **地域**： 展開先の地域を指定

    - **プラン**： **消費**

      ※プラン変更時に選択した地域がリセットされる場合があるので注意

    <img src="images/create-logic-apps-02.png" />

  - **確認および作成** をクリックし、設定内容に問題がなければ作成をクリック

  - ロジック アプリのデプロイが完了したことを確認し **リソースに移動** をクリック

    <img src="images/create-logic-apps-03.png" />

  - **ロジック アプリ デザイナー** が開くので **Blank Logic App** テンプレートを選択

    <img src="images/create-logic-apps-04.png" />

  - デザイナーに空のワークフロー サーフェイスが表示

    <img src="images/create-logic-apps-05.png" />

  - 検索ボックスに **Defender** と入力し、**Triggers** の **トリガー - 新しい WDATP アラートが発生したときにトリガー** を選択 

    <img src="images/create-logic-apps-06.png" />

  - **Sign in** をクリック

    <img src="images/create-logic-apps-07.png" />

    ※別ウィンドウで認証を求められるため、ワークショップで使用している資格情報を使用して認証処

    ※アクセス許可を求めるメッセージが奉持される場合は許諾

  - **+ New step** をクリック

    <img src="images/create-logic-apps-08.png" />

  - 検索ボックスに **Outlook** と入力し、表示される候補から **Outlook.com** を選択

    <img src="images/create-logic-apps-09.png" />

  - **メールの送信 (V2)** を選択

    <img src="images/create-logic-apps-10.png" />

  - **Sign in** をクリック

    <img src="images/create-logic-apps-11.png" />

    ※別ウィンドウで認証を求められるため、ワークショップで使用している資格情報を入力

    ※アクセス許可を求めるメッセージが表示される場合は許諾

- メール送信に必要な情報を設定

    - **宛先**： 任意

    - **件名**： Security Alert: アラートID（アラート ID は Dynamic content から選択）

    - **本文**： アラート ID と Body を Dynamic content から選択

    <img src="images/create-logic-apps-13.png" />

    <img src="images/create-logic-apps-14.png" />

- **保存** をクリックし、ワークフローの作成を終了

    <img src="images/create-logic-apps-15.png" />

- ワークフローの自動化を設定中のタブに切り替え

  **最新の情報に更新** をクリック、**ロジック アプリ名** にリストから作成したロジック アプリを選択

  <img src="images/defender-for-cloud-workflow-03.png" />

- 完了

  <img src="images/defender-for-cloud-workflow-04.png" />

<br />

## Appendix: 2 マネージド ID を使用したストレージへのアクセス

<br />

## Appendix: 3 マネージド ID を使用した SQL Database へのアクセス
