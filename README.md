# Carp Unity Application Toolkit

Unity 6 向けのアプリケーションフレームワーク。ステートマシン・リアクティブイベント・UI 管理・DI ベースの合成基点を提供します。

## インストール

### 前提パッケージ

`Packages/manifest.json` に以下を先に追加してください。

```json
"com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask",
"jp.hadashikick.vcontainer": "https://github.com/hadashiA/VContainer.git?path=VContainer/Assets/VContainer#1.17.0"
```

### CTK パッケージの追加

```json
"com.ctk.toolkit": "https://github.com/satoshishi/CarpUnityApplicationToolkit.git?path=Packages/com.ctk.toolkit"
```

バージョンを固定する場合はタグを指定します。

```json
"com.ctk.toolkit": "https://github.com/satoshishi/CarpUnityApplicationToolkit.git?path=Packages/com.ctk.toolkit#0.1.0"
```

### manifest.json 記述例

```json
{
  "dependencies": {
    "com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask",
    "jp.hadashikick.vcontainer": "https://github.com/hadashiA/VContainer.git?path=VContainer/Assets/VContainer#1.17.0",
    "com.ctk.toolkit": "https://github.com/satoshishi/CarpUnityApplicationToolkit.git?path=Packages/com.ctk.toolkit"
  }
}
```

## 名前空間

| モジュール | 名前空間 |
|---|---|
| ステートマシン | `CTK.State` |
| リアクティブイベント | `CTK.DataEvent` |
| UI 管理 | `CTK.UI` |
| ファイルロード | `CTK.File` |
| ログイン | `CTK.Login` |
| Addressable ローダー | `CTK.Addressable` |
| DI 合成基点 | `CTK.CompositionRoot` |
