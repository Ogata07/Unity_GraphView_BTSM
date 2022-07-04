using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/// <summary>
/// エディターウィンドウの表示
/// </summary>
public class GraphEditorWindow : EditorWindow
{
    [MenuItem("Tool/GraphEditorWindow")]
    public static void ShowWindow() {
        GraphEditorWindow graphEditorWindow = CreateInstance<GraphEditorWindow>();
        graphEditorWindow.Show();

    }
    private void OnEnable()
    {
        var GraphviewManeger = new GraphViewManager();
        this.rootVisualElement.Add(GraphviewManeger);
    }
}
