using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �Z�[�u����ScriptableObject�̐ݒ�
/// </summary>
[CreateAssetMenu(fileName = "graph.asset", menuName = "Graph Asset")]
public class GraphAsset :ScriptableObject
{
    public List<NodeData> nodes = new List<NodeData>();
    public List<EdgsData> fildNodes = new List<EdgsData>();
}
[System.Serializable]
public class NodeData{
    //�m�[�h�̈ʒu
    public Vector2 position;

}
[System.Serializable]
public class EdgsData{
    //�m�[�h�̃C���v�b�g�ԍ�
    public int inputNodeId;
    //�m�[�h�̃A�E�g�v�b�g�ԍ�
    public int outputNodeId;
}