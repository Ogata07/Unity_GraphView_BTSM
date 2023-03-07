using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System.Windows.Forms.DataVisualization.Charting;
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
    }
}
