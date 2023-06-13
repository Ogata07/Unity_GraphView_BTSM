using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// エディターウィンドウの表示
/// </summary>
public class GraphEditorWindow : EditorWindow
{
    private GraphAsset graphAsset;
    private GraphViewManager graphViewManager;
    private ObjectField saveField;
    public ObjectField SaveField {
        get { return saveField; }
        set { saveField = value; }
    }
    GraphAsset saveGraphAsset;
    //メニューバーから選択時用
    [MenuItem("Tool/GraphEditorWindow")]
    public static void ShowWindow() {
        GraphEditorWindow graphEditorWindow = CreateInstance<GraphEditorWindow>();
        graphEditorWindow.Show();
        //graphEditorWindow.Intialize();

    }
    //scriptableobjectを選択時用
    
    public static void ShowWindow(GraphAsset graphAsset) {
        GraphEditorWindow graphEditorWindow = CreateInstance<GraphEditorWindow>();
        graphEditorWindow.graphAsset= graphAsset;
        graphEditorWindow.Show();
        graphEditorWindow.Intialize(graphAsset);
        graphEditorWindow.saveField.value = graphAsset;

    }
    
    private void OnEnable()
    {
        if (graphAsset != null){
            Intialize(graphAsset);
        }
        else {
            //TODO 初期時に保存先がないのに空のObjectが入ってしまう
            //m_GraphAssetなしのIntializeを作るべきか？
            graphAsset = new GraphAsset();
            //TODO ↓を追加するとデータのロード時に不具合が起きる
            //Intialize(m_GraphAsset);
        }

    }
    private void Update()
    {
        if (EditorApplication.isPlaying) {
            Debug.Log("アニメーション製作中");
        }
    }
    /// <summary>
    /// 初期動作
    /// </summary>
    public void Intialize(GraphAsset graphAsset)
    {//GraphAsset graphAsset
        this.graphAsset = graphAsset;
        graphViewManager = new GraphViewManager(this, this.graphAsset);


        graphViewManager.style.flexGrow = 1;
        VisualElement visualElement = this.rootVisualElement;

        //画面上部のツールバー
        var toolbar = new Toolbar();
        visualElement.Add(toolbar);
        var btn1 = new ToolbarButton(graphViewManager.SaveStart) { text = "Save" };
        toolbar.Add(btn1);

        if (saveField == null){
            saveField = new UnityEditor.UIElements.ObjectField("保存先") { };//value = m_SaveGraphAsset
            saveField.objectType = typeof(GraphAsset);
            saveField.value = this.graphAsset;
        }
        //コールバック
        saveField.RegisterCallback<ChangeEvent<string>>(events => {
            graphViewManager.SaveLog(saveField.value as GraphAsset);
        });

        toolbar.Add(saveField);

        visualElement.Add(graphViewManager);
        rootVisualElement.Add(toolbar);

    }
}
