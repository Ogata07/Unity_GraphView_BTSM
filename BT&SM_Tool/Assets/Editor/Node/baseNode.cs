using System.Web.Services.Description;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Port = UnityEditor.Experimental.GraphView.Port;
/// <summary>
/// 各種Nodeで使うPort機能を集約
/// </summary>
public class BaseNode : Node
{
    public Port OutputPort { get; set; }
    public int NodeID { get; set; } = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PortAdd()
    {
        
        var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input,
        Port.Capacity.Multi, typeof(Port));
        inputContainer.Add(inputPort);
        OutputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output,
            Port.Capacity.Multi, typeof(Port));
        outputContainer.Add(OutputPort);
    }
}
