using UnityEditor.Experimental.GraphView;
/// <summary>
/// ビヘイビアツリーの場合このノードからスタートします
/// </summary>
public class StartNode : Node
{
    public Port OutputPort { get; }
    public StartNode() {
        title = "StartNode";
        OutputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output,
        Port.Capacity.Single, typeof(Port));
        outputContainer.Add(OutputPort);
        //extensionContainer.Add(new IntElement());
        //mainContainer.Add(new IntElement());
        //contentContainer.Add(new IntElement());
        RefreshExpandedState();

    }
}
