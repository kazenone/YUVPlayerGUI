# YUVPlayerGUI
YUV(動)画像を再生するためのアプリケーション  


##使用方法  
以下の項目をすべて入力し、実行を押下することで動画を再生するための別ウインドウが開きます  
1,ファイルのパス	:再生したいファイルをD&Dすることで自動入力されます。  
2,width			:動画の横幅を入力します。  
3,height		:動画の縦幅を入力します。  
4,カラーフォーマット	:動画のカラーフォーマットを420or444から選択します。  
5,fps			:動画を再生する際のfpsを入力します。  
6,動画をループ再生する	:動画が最後まで再生された後、ループするかを選択します。  
7,静止画として…	:動画の最初のフレームのみを表示し続けます、静止画を表示する際にもチェックを入れてください。  


##注意事項  
このプログラムを実行するには、「Microsoft .NET Framework 4.6.1」が必要となります。  
実行できない場合、以下のURLからインストールを行ってから再度実行してください。  
https://www.microsoft.com/ja-jp/download/details.aspx?id=49981

数字を入力する欄には小数点の入力ができてしまいますが、処理の際には切り捨てて使用されます。  

##使用ライブラリとライセンス情報  
本ソフトは以下のライブラリを使用しています。

*OpenCV  
*OpenCVSharp  

各ライブラリのライセンスはauthors.txtに記載してあります  
