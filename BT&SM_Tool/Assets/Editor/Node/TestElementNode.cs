using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

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
        extensionContainer.Add(new IntElement());
        RefreshExpandedState();
    }
}
