using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEditor;
/// <summary>
/// セーブやロードに必要な機能の管理所
/// </summary>
public class GraphViewManager : GraphView
{
    public GraphAsset m_GraphAsset;
    private GraphViewSave m_GraphViewSave = new GraphViewSave();
    public GraphViewManager() : base()
    {
        setInitial();
    }

    public GraphViewManager(EditorWindow editorWindow, GraphAsset graphAsset)
    {
        setInitial(); 
    }
    public void SaveStart() {
        m_GraphViewSave.SaveNodeElement(m_GraphAsset,this);
        Debug.Log("セーブをしました");
    }
    public void SaveLog(GraphAsset graphAsset) {
        m_GraphAsset = graphAsset;
        Debug.Log("セーブ先は"+graphAsset.name+"に更新しました");
    }
    public override List<Port> GetCompatiblePorts(Port startAnchor, NodeAdapter nodeAdapter) {
        var compatiblePorts = new List<Port>();
        compatiblePorts.AddRange(ports.ToList().Where(Port =>
        {
            //同じノードにはつなげない  
            if (startAnchor.node == Port.node)
                return false;
            //Int同士、Out同士ではつなげない
            if (Port.direction == startAnchor.direction)
                return false;
            //ポートの型が一致していない場合はつなげない
            if (Port.portType != startAnchor.portType)
                return false;

            return true;
        }));
        return compatiblePorts;
    }
    public void setInitial() {
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


        this.AddElement(new TestNode());
        this.AddElement(new TestNode());

        // 背景を一番後ろに追加
        this.Insert(0, new GridBackground());
    }
}
