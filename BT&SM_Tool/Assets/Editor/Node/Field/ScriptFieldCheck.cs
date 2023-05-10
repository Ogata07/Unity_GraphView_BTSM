using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using UnityEngine.UIElements;
using Codice.CM.SEIDInfo;
/// <summary>
/// ScriptNode�ɂ͂�ꂽ�X�N���v�g�̃t�B�[���h���Ă��GraphView��ł������l�ɂ���Element��ǉ�����X�N���v�g
/// </summary>
public class ScriptFieldCheck : MonoBehaviour
{
    public void Check(UnityEngine.Object _object, ScriptNode scriptNode) { 
    //�p�u���b�N�t�B�[���h���擾
    MonoScript value=_object as MonoScript;
    Type getType = value.GetClass();
    FieldInfo[] fieldInfos = getType.GetFields(
            BindingFlags.NonPublic
            | BindingFlags.Instance
            | BindingFlags.Public
            | BindingFlags.DeclaredOnly
            );
        Debug.Log(fieldInfos.Length);
        //Node���ɂ��łɂ���Ȃ烊�Z�b�g����
        NodeReset.extensionContainerReset(scriptNode);
        //�e��t�B�[���h�l������Node�ɒǉ�����
        //�V�K
        if (scriptNode.NodeID == 0) {
            
            foreach (FieldInfo f in fieldInfos) {
                AddVisualElement(f.FieldType);
            }
        }
    }
    private void AddVisualElement(Type fieldInfo)
    {
        Debug.Log(fieldInfo);
    }
}
