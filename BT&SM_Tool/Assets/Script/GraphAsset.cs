using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// �Z�[�u����ScriptableObject�̐ݒ�
/// </summary>
[CreateAssetMenu(fileName = "graph.asset", menuName = "Graph Asset")]
public class GraphAsset :ScriptableObject
{
    public List<NodeData> nodes = new List<NodeData>();
    public List<EdgeData> edges = new List<EdgeData>();
    public List<TestObjects> GetObject=new List<TestObjects>();
    public GameObject gameObject = default;
    public MonoBehaviour monoBehaviour = null;
}
[System.Serializable]
public class NodeData{
    //TODO Edge�݂����ɂ��ꂢ��
    //�m�[�h�̈ʒu
    public Vector2 position;
    //�m�[�h���̃X�N���v�g�Ǘ�(GUID)
    public string scriptID;
    //TODO �X�N���v�g�̕ۑ���������
    //public System.Object scriptObject;
    //public UnityEngine.Object GetObject;
    public UnityEngine.Object Object;
    //�m�[�h���̊Ǘ��ԍ�
    public int controlNumber;
    public List<FieldData> fieldData=new List<FieldData>();
    public List<FieldDataObject> fieldDataObject = new List<FieldDataObject>();
    public List<EdgesData> edgesDatas= new List<EdgesData>();
}
[System.Serializable]
public class EdgeData{
    //�m�[�h�̃C���v�b�g�ԍ�
    public int inputNodeId;
    //�m�[�h�̃A�E�g�v�b�g�ԍ�
    public int outputNodeId;
}
[System.Serializable]
public class EdgesData {
    //�G�b�W�̊Ǘ��ԍ�
    public int controlNumber;
    //�m�[�h�̃C���v�b�g�ԍ�
    public int inputNodeId;
}
[System.Serializable]
public class FieldData
{
    //�^����String�ŕۊ�
    //TODO enum��ʂ̕��@����������悤��
    public String typeName = default;
    //Field��
    public string fieldName = default;
    //�l��String�ŕۊ�
    public String valueData = default;
}
[System.Serializable]
public class FieldDataObject 
{
    //TODO enum��ʂ̕��@����������悤��
    public String typeName = default;
    //Field��
    public string fieldName = default;
    //�l��String�ŕۊ�
    public UnityEngine.Object valueData = default;
}
[System.Serializable]
public class TestObjects
{
    public UnityEngine.Object Object;
    [SerializeField]
    public Type Type;
    [SerializeField]
    public FieldInfo Field;
    public CreateType ss = CreateType.Int;
    public string Name;
}
public enum CreateType 
{
    Float,
    Int,
    Vector2,
    Vector3
}