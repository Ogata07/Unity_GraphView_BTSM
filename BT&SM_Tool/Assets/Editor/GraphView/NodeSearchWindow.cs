using log4net.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using ScriptFlow;
using Codice.Client.BaseCommands;
/// <summary>
/// GraohView上で作成可能なノードのウィンドウ管理
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
    //ウィンドウの作成
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> entries = new List<SearchTreeEntry>();
        entries.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));
        entries.Add(new SearchTreeGroupEntry(new GUIContent("Node")) { level = 1 });
        foreach (System.Reflection.Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
            foreach (Type type in assembly.GetTypes()) {
                if (type.IsSubclassOf(typeof(GraphViewScriptBase))) {
                    entries.Add(new SearchTreeEntry(new GUIContent(type.Name)) { level = 2, userData = type });
                }
            }
        }
        entries.Add(new SearchTreeGroupEntry(new GUIContent("Test")) { level = 1 });
        foreach (System.Reflection.Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
            foreach (Type type in assembly.GetTypes()) {
                if (type.IsSubclassOf(typeof(Node))) { 
                    entries.Add(new SearchTreeEntry(new GUIContent(type.Name)){ level = 2,userData=type});
                }
            }
        }
        return entries;
    }


    //選択されたのでノードの作成
    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        var type = SearchTreeEntry.userData as System.Type;
        //選択されたのがGraphViewScriptBaseを継承していた場合
        if (type.IsSubclassOf(typeof(GraphViewScriptBase))) {

            //スクリプトノードの作成と各種設定
            ScriptNode debugNode = new ScriptNode();
            Vector2 worldMousePosition = m_EditorWindow.rootVisualElement.ChangeCoordinatesTo(m_EditorWindow.rootVisualElement.parent, context.screenMousePosition - m_EditorWindow.position.position);
            Vector2 localMousePosition = m_GraphViewManager.contentViewContainer.WorldToLocal(worldMousePosition);

            //ノードの位置を設定
            debugNode.SetPosition(new Rect(localMousePosition, new Vector2(100, 100)));
            //ノードの中身を設定
            var assets = AssetDatabase.FindAssets(SearchTreeEntry.userData.ToString());
            var assetspath = AssetDatabase.GUIDToAssetPath(assets[0]);
            //ObjectFieldのタイプを設定
            debugNode.ObjectField.objectType = typeof(UnityEngine.Object);
            //ObjectFieldに挿入
            debugNode.ObjectField.value = AssetDatabase.LoadMainAssetAtPath(assetspath);

            //画面に追加
            m_GraphViewManager.AddElement(debugNode);
        }
        if(type.IsSubclassOf(typeof(Node))) {
            if (SearchTreeEntry.userData is GraphElement) {
                var cast = SearchTreeEntry.userData as GraphElement;
                m_GraphViewManager.AddElement(cast);

            }
        }
        Debug.Log("ノードを追加しました");
        return true;
    }
}
