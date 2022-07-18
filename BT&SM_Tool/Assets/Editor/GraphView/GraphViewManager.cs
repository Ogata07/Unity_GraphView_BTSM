using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
public class GraphViewManager : GraphView
{
    public GraphViewManager() : base()
    {
        //親のサイズに合わせてサイズ変更
        this.StretchToParentSize();
        //拡大縮小
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        //ドラッグで描画範囲を移動
        this.AddManipulator(new ContentDragger());
        //ドラッグで選択した要素を移動
        this.AddManipulator(new SelectionDragger());
        //ドラッグで範囲選択
        this.AddManipulator(new RectangleSelector());
        // ussファイルを読み込んでスタイルに追加
        this.styleSheets.Add(Resources.Load<StyleSheet>("GraphViewBackGround"));

        // 背景を一番後ろに追加
        this.Insert(0, new GridBackground());
        //this.AddElement(new TestNode());
    }
}
