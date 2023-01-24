using UnityEditor.Experimental.GraphView;
/// <summary>
/// スクリプト参照ノード
/// </summary>
public class ScriptNode : Node
{
    public int NodeID { get; set; } = 0;
    public ScriptNode():base(){
        title = "ScriptNode";
        var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input,
            Port.Capacity.Single, typeof(Port));
        inputContainer.Add(inputPort);
        var outPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output,
            Port.Capacity.Single, typeof(Port));
        outputContainer.Add(outPort);
    }
}
