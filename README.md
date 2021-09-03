# ETC料金所番号一覧

ETCの料金所番号から名称を取得するためのデータファイル

## データファイル

### 出典
- [NEXCO西日本](https://www.w-nexco.co.jp/etc/maintenance/pdfs/list01.pdf)@2021/06/30
- [首都高速道路](https://www.shutoko.jp/fee/tollbooth/)@2020/03/22

誰か助けてくれ

### [tollbooth.tsv](,/tollbooth.tsv)

- UTF-8 (BOM付)のタブ区切りファイル
- 先頭行はヘッダー

|#|項目名|例|
|-|-|-|
|1|路線名|首都圏中央連絡道|
|2|路線別名|圏央道|
|3|路線管理者|
|4|路線備考|
|5|料金所|茅ヶ崎ＪＣＴ合併|
|6|料金所備考|圏央道⇔新湘南バイパス（平塚方面）　圏央道分（精算・発券）|
|7|路線番号|04|
|8|料金所番号|048|

## ライブラリ

### .NET

- [NuGet Shipwreck.Etc](https://www.nuget.org/packages/Shipwreck.Etc/)

```csharp
using Shipwreck.Etc;

TollBoothDictionary d = await new EmbeddedResourceTollBoothLoader().LoadTollBoothDictionaryAsync();
if (d.TryFind("04", "048", out var b))
{
    Console.WriteLine("ExpresswayName: {0}", b.ExpresswayName);
    Console.WriteLine("TollBoothName: {0}", b.TollBoothName);
}
```
