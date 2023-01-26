using log4net.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// GraohView��ō쐬�\�ȃm�[�h�̃E�B���h�E�Ǘ�
/// </summary>
public class NodeSearchWindow : ScriptableObject,ISearchWindowProvider
{
    private GraphViewManager m_GraphViewManager = default;
    private GraphEditorWindow m_GraphEditorWindow = default;
    private EditorWindow m_EditorWindow = default;
    public void Initialize(GraphViewManager graphView, EditorWindow editorWindow)
    {
        this.m_GraphViewManager = graphView;
        this.m_EditorWindow = editorWindow;
    }
    //�E�B���h�E�̍쐬
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var entries = new List<SearchTreeEntry>
        {
            new SearchTreeGroupEntry(new GUIContent("Create Node")),
            new SearchTreeGroupEntry(new GUIContent("Node")) { level = 1 }
        };
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
            foreach (var type in assembly.GetTypes()) {
                if (type.IsSubclassOf(typeof(ScriptNode))) {
                    entries.Add(new SearchTreeEntry(new GUIContent(type.Name)) { level = 2, userData = type });
                }
            }
        }
        return entries;
    }


    //�I�����ꂽ�̂Ńm�[�h�̍쐬
    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        var type = SearchTreeEntry.userData as System.Type;
        if (type.IsSubclassOf(typeof(GraphViewScriptBase))) {
            var assets = AssetDatabase.FindAssets(SearchTreeEntry.userData.ToString());

            var node=Activator.CreateInstance(type)as GraphViewScriptBase;
            DebugNode debugNode = new DebugNode();
            var worldMousePosition = m_EditorWindow.rootVisualElement.ChangeCoordinatesTo(m_EditorWindow.rootVisualElement.parent, context.screenMousePosition - m_EditorWindow.position.position);
            var localMousePosition = m_GraphViewManager.contentViewContainer.WorldToLocal(worldMousePosition);

            debugNode.SetPosition(new Rect(localMousePosition, new Vector2(100,100)));
            node.SetPosition(new Rect(localMousePosition, new Vector2(100, 100)));
            m_GraphViewManager.AddElement(node);
        }
        Debug.Log("���쐻�ł�");
        return true;
    }
}
