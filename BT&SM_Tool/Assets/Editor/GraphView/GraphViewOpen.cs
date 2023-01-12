using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.YamlDotNet.Core.Events;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class GraphViewOpen 
{
    /// <summary>
    /// GraphView��scriptableobject��I�����ɃE�B���h�E��\������
    /// </summary>
    [OnOpenAsset]
    static bool OnOppenAsset(int instanceId)
    {
        //GraphAseet�g�p�����f
        if (EditorUtility.InstanceIDToObject(instanceId) is GraphAsset)
        {
            Debug.Log("�Ώ�Asset�ł�");
            Object selectObject = Selection.activeObject;
            GraphAsset graphAsset = (GraphAsset)selectObject;
            GraphViewLoad.LoadNodeElement(graphAsset);
            return true;
        }
        return false;
    }
}
