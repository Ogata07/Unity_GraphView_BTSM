using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEditor;
using System.Runtime.Remoting.Contexts;
/// <summary>
/// セーブやロードに必要な機能の管理所
/// </summary>
public class GraphViewManager : GraphView
{
    public GraphAsset m_GraphAsset;

    public GraphViewManager() : base()
    {
        //TODO 初期作成ができなくなる
        //setInitial();
    }

    public GraphViewManager(EditorWindow editorWindow, GraphAsset graphAsset)
    {
        m_GraphAsset= graphAsset;
        setInitial(editorWindow); 
    }
    //TODO　保存機能の分離を行う可能性あり
    public void SaveStart() {
        if (m_GraphAsset != null)
        {
            Debug.Log("<color=green>セーブをしました</color>");
            GraphViewSave.SaveNodeElement(m_GraphAsset, this);
        }
        else
            Debug.LogError("セーブ先がありません");
        
    }
    public void SaveLog(GraphAsset graphAsset) {
        m_GraphAsset = graphAsset;
        Debug.Log("セーブ先は"+graphAsset.name+"に更新しました");
    }
    //GraphView上のルール
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
    public  void setInitial(EditorWindow editorWindow) {
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
        //ノード追加用のウィンドウ表示
        var nodeSearcWindow = new NodeSearchWindow();
        nodeSearcWindow.Initialize(this, editorWindow);
        nodeCreationRequest += context =>
        {
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), nodeSearcWindow);
        };

        //データからの生成
        GraphViewLoad.CreateGraphView(this);
        // 背景を一番後ろに追加
        this.Insert(0, new GridBackground());
    }
}
