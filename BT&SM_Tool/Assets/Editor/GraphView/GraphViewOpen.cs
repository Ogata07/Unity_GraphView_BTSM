using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
/// <summary>
/// GraphView�̃X�N���v�^�u���I�u�W�F�N�g��I�����ɃE�B���h�E��\������
/// </summary>
public class GraphViewOpen 
{
    [OnOpenAsset]
    static bool OnOppenAsset(int instanceId) {
        //GraphAseet�g�p�����f
        if (EditorUtility.InstanceIDToObject(instanceId) is GraphAsset) {
            Debug.Log("�Ώ�Asset�ł�");
            return true;
        }
        return false;
    }
}
