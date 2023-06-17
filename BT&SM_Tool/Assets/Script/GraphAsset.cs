using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// セーブするScriptableObjectの設定
/// </summary>
[CreateAssetMenu(fileName = "graph.asset", menuName = "Graph Asset")]
public class GraphAsset :ScriptableObject
{
    public List<NodeData> nodes = new List<NodeData>();
    public List<EdgeData> edges = new List<EdgeData>();
    public List<TestObjects> getObject=new List<TestObjects>();
    public GameObject gameObject = default;
    public MonoBehaviour monoBehaviour = null;
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
    public UnityEngine.Object @object;
    //ノード内の管理番号
    public int controlNumber;
    public List<FieldData> fieldData=new List<FieldData>();
    public List<FieldDataObject> fieldDataObject = new List<FieldDataObject>();
    public List<EdgesData> edgesDatas= new List<EdgesData>();
}
[System.Serializable]
public class EdgeData{
    //ノードのインプット番号
    public int inputNodeId;
    //ノードのアウトプット番号
    public int outputNodeId;
}
[System.Serializable]
public class EdgesData {
    //エッジの管理番号
    public int controlNumber;
    //ノードのインプット番号
    public int inputNodeId;
}
[System.Serializable]
public class FieldData
{
    //型名をStringで保管
    //TODO enumや別の方法を検討するように
    public String typeName = default;
    //Field名
    public string fieldName = default;
    //値をStringで保管
    public String valueData = default;
}
[System.Serializable]
public class FieldDataObject 
{
    //TODO enumや別の方法を検討するように
    public String typeName = default;
    //Field名
    public string fieldName = default;
    //値をStringで保管
    public UnityEngine.Object valueData = default;
}
[System.Serializable]
public class TestObjects
{
    public UnityEngine.Object @object;
    [SerializeField]
    public Type type;
    [SerializeField]
    public FieldInfo field;
    public CreateType ss = CreateType.Int;
    public string name;
}
public enum CreateType 
{
    Float,
    Int,
    Vector2,
    Vector3
}