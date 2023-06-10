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
    //SM用
    public Node sm_StartNode { get; set; } = default;
    //Nodeの色関係
    public string defaultColorCode { get;} = "#3F3F3F";
    public string startColorCode { get;} = "#FFA500";
    private readonly GraphViewLoad graphViewLoad = new GraphViewLoad();
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
            EditorUtility.SetDirty(m_GraphAsset);
            AssetDatabase.SaveAssets();
        }
        else
            Debug.LogError("セーブ先がありません");
        
    }
    public void SaveLog(GraphAsset graphAsset) {
        m_GraphAsset = graphAsset;
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
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        base.BuildContextualMenu(evt);
        //if (evt.target is GraphView || evt.target is Node || evt.target is Group)
        //{
        //    evt.menu.AppendAction("StateMachine", delegate
        //    {
                
        //        ClickEvent();
        //        //CopySelectionCallback();
        //    }, (DropdownMenuAction a) => canCopySelection ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);
        //}
        if (evt.target is Node) {
            Node SelectNode = (Node)evt.target;
            evt.menu.AppendAction(
                "SelectStartNode" ,
                paste=>{ ClickEvent(SelectNode); },
                (paste => (this.canCopySelection ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled)),
                (object)null);
        }
    }
    public void ClickEvent(Node handler)
    {
        Debug.Log(handler);
        if (handler is ScriptNode) {
            if (sm_StartNode != default) {
                sm_StartNode.name = default;
                NodeTitleColorChange(sm_StartNode, defaultColorCode);
            }
            //更新
            sm_StartNode = handler;
            handler.name = "Start";
            NodeTitleColorChange(handler, startColorCode);
        }
        //TODO ステートマシンのみこれが選択されたノードからスタートされるようにしたい(時間がかかるので後回しかもしれない)

    }
    //Nodeのタイトル部分の色を変更するメソッド
    public void NodeTitleColorChange(Node tagetNode,string changeColorCode) {
        ScriptNode castNode = (ScriptNode)tagetNode;
        castNode.startNodeColorChange(changeColorCode);
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
