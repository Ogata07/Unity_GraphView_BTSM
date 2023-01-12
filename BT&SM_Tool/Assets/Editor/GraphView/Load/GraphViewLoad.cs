using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// GraphAssetの内容をエディタウィンドウに表示する
/// </summary>
public static class GraphViewLoad 
{
    public static void LoadNodeElement(GraphAsset m_GraphAsset) {
        GraphEditorWindow.ShowWindow(m_GraphAsset);
    }
}
