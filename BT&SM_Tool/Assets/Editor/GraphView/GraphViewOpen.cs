using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class GraphViewOpen 
{
    /// <summary>
    /// GraphViewのスクリプタブルオブジェクトを選択時にウィンドウを表示する
    /// </summary>
    [OnOpenAsset]
    static bool OnOppenAsset(int instanceId)
    {
        //GraphAseet使用か判断
        if (EditorUtility.InstanceIDToObject(instanceId) is GraphAsset)
        {
            Debug.Log("対象Assetです");
            return true;
        }
        return false;
    }
}
