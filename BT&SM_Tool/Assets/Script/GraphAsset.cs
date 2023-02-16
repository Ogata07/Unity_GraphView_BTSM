using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
/// <summary>
/// セーブするScriptableObjectの設定
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
    //TODO Edgeみたいにきれいに
    //ノードの位置
    public Vector2 position;
    //ノード内のスクリプト管理(GUID)
    public string scriptID;
    //TODO スクリプトの保存が未決定
    //public System.Object scriptObject;
    //public UnityEngine.Object GetObject;
    public UnityEngine.Object Object;
    //ノード内の管理番号
    public int controlNumber;
}
[System.Serializable]
public class EdgeData{
    //ノードのインプット番号
    public int inputNodeId;
    //ノードのアウトプット番号
    public int outputNodeId;
}
[System.Serializable]
public class Objects
{
    public UnityEngine.Object Object;
}