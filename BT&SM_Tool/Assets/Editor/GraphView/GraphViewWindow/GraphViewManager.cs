using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
/// <summary>
/// GraphViewでの機能追加クラス
/// </summary>
public class GraphViewManager : GraphView
{
    public GraphAsset graphAsset;
    //SM用
    public Node Sm_StartNode { get; set; } = default;
    //Nodeの色関係
    public string DefaultColorCode { get;} = "#3F3F3F";
    public string StartColorCode { get;} = "#FFA500";
    private readonly GraphViewLoad graphViewLoad = new GraphViewLoad();
    private readonly GraphViewSave graphViewSave = new GraphViewSave();
    public GraphViewManager() : base()
    {
        //TODO 初期作成ができなくなる
        //setInitial();
    }

    public GraphViewManager(EditorWindow editorWindow, GraphAsset graphAsset)
    {
        this.graphAsset= graphAsset;
        SetInitial(editorWindow); 
    }
    //外部でもいじれる関数を作成するための別ウィンドウ作成(テスト用)
    public void CreateBackBord() {
        Debug.Log("生成ウィンドウ表示");
    }
    //TODO　保存機能の分離を行う可能性あり
    public void SaveStart() {
        if (graphAsset != null)
        {
            Debug.Log("<color=green>セーブをしました</color>");
            graphViewSave.SaveNodeElement(graphAsset, this);
            EditorUtility.SetDirty(graphAsset);
            AssetDatabase.SaveAssets();
        }
        else
            Debug.LogError("セーブ先がありません");
        
    }
    public void SaveLog(GraphAsset graphAsset) {
        this.graphAsset = graphAsset;
        Debug.Log("セーブ先は"+graphAsset.name+"に更新しました");
    }
    /// <summary>
    /// GraohView上での動きに反応する
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    private GraphViewChange OnCallbackGraphView(GraphViewChange callback)
    {
        if (callback.edgesToCreate != null)
        {
            foreach (UnityEditor.Experimental.GraphView.Edge e in callback.edgesToCreate)
            {
                Debug.Log("Edgeが作製されました");
            }

        }
        return callback;

    }
    //GraphView上のルール
    public override List<Port> GetCompatiblePorts(Port startAnchor, NodeAdapter nodeAdapter) {
        var compatiblePorts = new List<Port>();
        compatiblePorts.AddRange(ports.ToList().Where(port =>
        {
            //同じノードにはつなげない  
            if (startAnchor.node == port.node)
                return false;
            //Int同士、Out同士ではつなげない
            if (port.direction == startAnchor.direction)
                return false;
            //ポートの型が一致していない場合はつなげない
            if (port.portType != startAnchor.portType)
                return false;

            return true;
        }));
        return compatiblePorts;
    }
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        base.BuildContextualMenu(evt);
        if (evt.target is Node) {
            Node selectNode = (Node)evt.target;
            evt.menu.AppendAction(
                "SelectStartNode" ,
                paste=>{ ClickEvent(selectNode); },
                (paste => (this.canCopySelection ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled)),
                (object)null);
        }
    }
    public void ClickEvent(Node handler)
    {
        Debug.Log(handler);
        if (handler is ScriptNode) {
            if (Sm_StartNode != default) {
                Sm_StartNode.name = default;
                NodeTitleColorChange(Sm_StartNode, DefaultColorCode);
            }
            //更新
            Sm_StartNode = handler;
            handler.name = "Start";
            NodeTitleColorChange(handler, StartColorCode);
        }
    }
    //Nodeのタイトル部分の色を変更するメソッド
    public void NodeTitleColorChange(Node tagetNode,string changeColorCode) {
        ScriptNode castNode = (ScriptNode)tagetNode;
        castNode.StartNodeColorChange(changeColorCode);
    }
    public  void SetInitial(EditorWindow editorWindow) {
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
        //GraphView上の変化監視コールバック
        graphViewChanged = OnCallbackGraphView;
        //データからの生成
        graphViewLoad.CreateGraphView(this);
        //TODO スタートノードの追加
        this.AddElement(new StartNode());
        // 背景を一番後ろに追加
        this.Insert(0, new GridBackground());
    }
}
