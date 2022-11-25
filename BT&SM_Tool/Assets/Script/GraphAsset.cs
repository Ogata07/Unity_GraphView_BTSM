using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// セーブするScriptableObjectの設定
/// </summary>
[CreateAssetMenu(fileName = "graph.asset", menuName = "Graph Asset")]
public class GraphAsset :ScriptableObject
{
    public List<NodeData> nodes = new List<NodeData>();
    public List<EdgeData> edges = new List<EdgeData>();
}
[System.Serializable]
public class NodeData{
    //ノードの位置
    public Vector2 position;

}
[System.Serializable]
public class EdgeData{
    //ノードのインプット番号
    public int inputNodeId;
    //ノードのアウトプット番号
    public int outputNodeId;
}