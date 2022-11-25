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
    public List<EdgeData> edges = new List<EdgeData>();
}
[System.Serializable]
public class NodeData{
    //�m�[�h�̈ʒu
    public Vector2 position;

}
[System.Serializable]
public class EdgeData{
    //�m�[�h�̃C���v�b�g�ԍ�
    public int inputNodeId;
    //�m�[�h�̃A�E�g�v�b�g�ԍ�
    public int outputNodeId;
}