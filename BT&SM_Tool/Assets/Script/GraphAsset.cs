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
    public List<EdgsData> fildNodes = new List<EdgsData>();
}
[System.Serializable]
public class NodeData{
    //ノードの位置
    public Vector2 position;

}
[System.Serializable]
public class EdgsData{
    //ノードのインプット番号
    public int inputNodeId;
    //ノードのアウトプット番号
    public int outputNodeId;
}