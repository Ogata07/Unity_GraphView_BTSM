using log4net.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
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
                if (type.IsSubclassOf(typeof(GraphViewScriptBase))) {
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
        //�I�����ꂽ�̂�GraphViewScriptBase���p�����Ă����ꍇ
        if (type.IsSubclassOf(typeof(GraphViewScriptBase))) {

            //�X�N���v�g�m�[�h�̍쐬�Ɗe��ݒ�
            ScriptNode debugNode = new ScriptNode();
            var worldMousePosition = m_EditorWindow.rootVisualElement.ChangeCoordinatesTo(m_EditorWindow.rootVisualElement.parent, context.screenMousePosition - m_EditorWindow.position.position);
            var localMousePosition = m_GraphViewManager.contentViewContainer.WorldToLocal(worldMousePosition);

            //�m�[�h�̈ʒu��ݒ�
            debugNode.SetPosition(new Rect(localMousePosition, new Vector2(100, 100)));
            //�m�[�h�̒��g��ݒ�
            var assets = AssetDatabase.FindAssets(SearchTreeEntry.userData.ToString());
            var assetspath = AssetDatabase.GUIDToAssetPath(assets[0]);
            //ObjectField�̃^�C�v��ݒ�
            debugNode.ObjectField.objectType = typeof(UnityEngine.Object);
            //ObjectField�ɑ}��
            debugNode.ObjectField.value = AssetDatabase.LoadMainAssetAtPath(assetspath);

            //��ʂɒǉ�
            m_GraphViewManager.AddElement(debugNode);
        }
        Debug.Log("�m�[�h��ǉ����܂���");
        return true;
    }
}
