using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// GraphAsset�̓��e���G�f�B�^�E�B���h�E�ɕ\������
/// </summary>
public static class GraphViewLoad 
{
    public static void LoadNodeElement(GraphAsset m_GraphAsset) {
        GraphEditorWindow.ShowWindow(m_GraphAsset);
    }
}
