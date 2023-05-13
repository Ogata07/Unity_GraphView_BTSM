using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using UnityEngine.UIElements;
using Codice.CM.SEIDInfo;
using System.Web.UI;
using UnityEditor.UIElements;
using System.ComponentModel;
using System.Diagnostics.Contracts;
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
            //BindingFlags.NonPublic
            BindingFlags.Instance
            | BindingFlags.Public
            //| BindingFlags.DeclaredOnly
            );

        //Node���ɂ��łɂ���Ȃ烊�Z�b�g����
        NodeReset.extensionContainerReset(scriptNode);
        //�e��t�B�[���h�l������Node�ɒǉ�����
        //�V�K
        if (scriptNode.NodeID == 0) {
            //fieldInfos.GetValue()�Œl�̎󂯓n�����ł��邩���H
            foreach (FieldInfo f in fieldInfos) {
                AddVisualElement(f.FieldType,scriptNode);
            }
        }
    }
    private void AddVisualElement(Type fieldInfo,ScriptNode scriptNode)
    {
        Debug.Log(fieldInfo.ToString());
        switch (fieldInfo.ToString()) {
            case "Vector2":
                print("WW");
                break;
            case "System.Single"://Float�^
                scriptNode.extensionContainer.Add(new DataElement<FloatField, float>());
                scriptNode.RefreshExpandedState();
                break;
            case "double":
                break;
            default: 
                break;

        }
    }
}
