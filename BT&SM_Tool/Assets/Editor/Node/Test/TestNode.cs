using UnityEditor.Experimental.GraphView;
/// <summary>
/// �Z�[�u�⃍�[�h�̊�b�����e�X�g�p
/// </summary>
public class TestNode : Node
{
    public int NodeID { get; set; } = 0;
    public TestNode() 
    {
        title = "Test";
        var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input,
            Port.Capacity.Single, typeof(Port));
        inputContainer.Add(inputPort);
        var outPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output,
            Port.Capacity.Single, typeof(Port));
        outputContainer.Add(outPort);
    }
}
