using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

/// <summary>
/// エディターウィンドウの表示
/// </summary>
public class GraphEditorWindow : EditorWindow
{
    GraphAsset m_GraphAsset;
    GraphViewManager m_GraphViewManager;
    GraphAsset m_SaveGraphAsset;
    public VisualElement a = default;
    [MenuItem("Tool/GraphEditorWindow")]
    public static void ShowWindow() {
        GraphEditorWindow graphEditorWindow = CreateInstance<GraphEditorWindow>();
        graphEditorWindow.Show();

    }
    private void OnEnable()
    {
        Intialize(m_GraphAsset);
    }
    public void Intialize(GraphAsset graphAsset) {
        m_GraphAsset = graphAsset;
        m_GraphViewManager = new GraphViewManager(this,m_GraphAsset);
        m_GraphViewManager.style.flexGrow = 1;

        VisualElement visualElement = this.rootVisualElement;

        var toolbar = new Toolbar();
        visualElement.Add(toolbar);
        var btn1 = new ToolbarButton(m_GraphViewManager.SaveStart) { text = "Save" };
        toolbar.Add(btn1);
        var btn2 = new UnityEditor.UIElements.ObjectField("保存先") {};//value = m_SaveGraphAsset
        btn2.objectType=typeof(GraphAsset);
        //コールバック
        btn2.RegisterCallback<ChangeEvent<string>>(events => {
            Debug.Log(btn2.value.name);
            m_GraphViewManager.SaveLog(btn2.value as GraphAsset);
        });
        toolbar.Add(btn2);
        var GraphviewManeger = new GraphViewManager();
        visualElement.Add(m_GraphViewManager);

        rootVisualElement.Add(toolbar);
    }
}
