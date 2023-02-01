using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
/// <summary>
/// �Z�[�u����ScriptableObject�̐ݒ�
/// </summary>
[CreateAssetMenu(fileName = "graph.asset", menuName = "Graph Asset")]
public class GraphAsset :ScriptableObject
{
    public List<NodeData> nodes = new List<NodeData>();
    public List<EdgeData> edges = new List<EdgeData>();
    public List<Objects> GetObject=new List<Objects>();
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
}
[System.Serializable]
public class EdgeData{
    //�m�[�h�̃C���v�b�g�ԍ�
    public int inputNodeId;
    //�m�[�h�̃A�E�g�v�b�g�ԍ�
    public int outputNodeId;
}
[System.Serializable]
public class Objects
{
    public UnityEngine.Object Object;
}