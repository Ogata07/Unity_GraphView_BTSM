using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
/// <summary>
/// EditorWindow “WŠJ
/// </summary>
public class GraphEditor : EditorWindow
{
    [MenuItem("Tool/ScriptGraph")]
    public static void Open() {
        GraphEditor window = GetWindow<GraphEditor>();
        window.Show();
    }
}
