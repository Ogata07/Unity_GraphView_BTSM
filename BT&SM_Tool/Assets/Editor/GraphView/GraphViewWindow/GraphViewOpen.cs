using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
/// <summary>
/// GraphAssetクラスのscriptableobjectを選択時にウィンドウを表示させるクラス
/// </summary>
public class GraphViewOpen 
{
    [OnOpenAsset]
    static bool OnOppenAsset(int instanceId)
    {
        //GraphAseet使用か判断
        if (EditorUtility.InstanceIDToObject(instanceId) is GraphAsset)
        {
            Debug.Log("対象Assetです");
            GraphViewLoad graphViewLoad = new GraphViewLoad();
            Object selectObject = Selection.activeObject;
            GraphAsset graphAsset = (GraphAsset)selectObject;
            graphViewLoad.LoadNodeElement(graphAsset);
            return true;
        }
        return false;
    }
}
