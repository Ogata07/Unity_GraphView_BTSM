using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using ScriptFlow;
using System.Reflection;
/// <summary>
/// GraohView上で作成可能なノードのウィンドウ管理クラス
/// </summary>
public class NodeSearchWindow : ScriptableObject,ISearchWindowProvider
{
    private GraphViewManager graphViewManager = default;
    private EditorWindow editorWindow = default;
    private Vector2 defaultSize = new Vector2(100, 100);
    public void Initialize(GraphViewManager graphView, EditorWindow editorWindow)
    {
        this.graphViewManager = graphView;
        this.editorWindow = editorWindow;
    }
    //ウィンドウの作成
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> entries = new List<SearchTreeEntry>();
        entries.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));
        entries.Add(new SearchTreeGroupEntry(new GUIContent("Test")) { level = 1 });
        foreach (System.Reflection.Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
            foreach (Type type in assembly.GetTypes()) {
                if (type.IsSubclassOf(typeof(Node))) { 
                    entries.Add(new SearchTreeEntry(new GUIContent(type.Name)){ level = 2,userData=type});
                }
            }
        }
        entries.Add(new SearchTreeGroupEntry(new GUIContent("SM")) { level = 1 });
        foreach (System.Reflection.Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
            foreach (Type type in assembly.GetTypes()) {
                if (type.IsSubclassOf(typeof(GraphViewScriptBase))) {
                    FieldInfo[] fieldInfos = type.GetFields(
                        BindingFlags.Instance |
                        BindingFlags.Static |
                        BindingFlags.NonPublic 
                        );
                    foreach (FieldInfo f in fieldInfos)
                    {
                        if(f.FieldType.ToString()== "SMManager")
                            entries.Add(new SearchTreeEntry(new GUIContent(type.Name)) { level = 2, userData = type });
                    }
                }
            }
        }
        entries.Add(new SearchTreeGroupEntry(new GUIContent("BT")) { level = 1 });
        entries.Add(new SearchTreeGroupEntry(new GUIContent("Condition")) { level = 2 });
        foreach (System.Reflection.Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(ConditionBase)))
                {
                    entries.Add(new SearchTreeEntry(new GUIContent(type.Name)) { level = 3, userData = type });
                }
            }
        }
        entries.Add(new SearchTreeGroupEntry(new GUIContent("Action")) { level = 2 });
        foreach (System.Reflection.Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(GraphViewScriptBase))&&!type.IsSubclassOf(typeof(ConditionBase)))
                {
                    FieldInfo[] fieldInfos = type.GetFields(
                        BindingFlags.Instance |
                        BindingFlags.Static |
                        BindingFlags.NonPublic
                        );
                    foreach (FieldInfo f in fieldInfos)
                    {
                        if (f.FieldType.ToString() == "BTManager")
                            entries.Add(new SearchTreeEntry(new GUIContent(type.Name)) { level = 3, userData = type });
                    }
                }
            }
        }
        return entries;
    }


    //選択されたのでノードの作成
    public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
    {
        var type = searchTreeEntry.userData as System.Type;
        //選択されたのがGraphViewScriptBaseを継承していた場合
        if (type.IsSubclassOf(typeof(GraphViewScriptBase))) {

            //スクリプトノードの作成と各種設定
            ScriptNode debugNode = new ScriptNode();
            Vector2 worldMousePosition = editorWindow.rootVisualElement.ChangeCoordinatesTo(editorWindow.rootVisualElement.parent, context.screenMousePosition - editorWindow.position.position);
            Vector2 localMousePosition = graphViewManager.contentViewContainer.WorldToLocal(worldMousePosition);



            //GraphView上に追加時に内容によってどの役割をするのか分類をする
            //TODO SelectorNodeは未実装
            if (graphViewManager.GraphViewType == GraphViewType.BehaviorTree) {
                debugNode.nodeType = NodeType.BT_Action;
            }else
                debugNode.nodeType = NodeType.SM;
            if (type.IsSubclassOf(typeof(ConditionBase)))
            {
                debugNode.nodeType = NodeType.BT_Condition;
            }
            //ノードの位置を設定
            debugNode.SetPosition(new Rect(localMousePosition, defaultSize));
            //ノードの中身を設定
            var assets = AssetDatabase.FindAssets(searchTreeEntry.userData.ToString());
            var assetspath = AssetDatabase.GUIDToAssetPath(assets[0]);
            //ObjectFieldのタイプを設定
            debugNode.ObjectField.objectType = typeof(UnityEngine.Object);
            //ObjectFieldに挿入
            debugNode.ObjectField.value = AssetDatabase.LoadMainAssetAtPath(assetspath);
            debugNode.AddStart();
            //画面に追加
            graphViewManager.AddElement(debugNode);
        }

        if(type.IsSubclassOf(typeof(Node))) {
            Debug.Log(searchTreeEntry.userData);

            var cast = Type.GetType(searchTreeEntry.userData.ToString());
            var elementCast= (GraphElement)Activator.CreateInstance(cast);
            graphViewManager.AddElement(elementCast);
        }
        Debug.Log("ノードを追加しました");
        return true;
    }
}
