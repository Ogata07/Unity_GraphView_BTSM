using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class TestElementNode : Node
{
    public Port InputPort { get; }
    public Port OutputPort { get; }
    public TestElementNode() {
        title = "TestNode";
        OutputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output,
            Port.Capacity.Single, typeof(Port));
        outputContainer.Add(OutputPort);
        InputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input,
            Port.Capacity.Single, typeof(Port));
        inputContainer.Add(InputPort);
        extensionContainer.Add(new DataElement<IntegerField,int>(9));
        extensionContainer.Add(new DataElement<FloatField,float>(10.0f));
        extensionContainer.Add(new DataElement<Vector3Field, Vector3>(new Vector3(1,2,3)));
        extensionContainer.Add(new DataElement<TextField, string>("Hello"));
        RefreshExpandedState();
        
    }
}
