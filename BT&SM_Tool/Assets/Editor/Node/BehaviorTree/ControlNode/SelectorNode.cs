using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// このノードにつながっているノードを下から優先的に実行する
/// </summary>
public class SelectorNode : Node
{
    private EnumField enumField = default;
    public Port OutputPort { get; set; }
    public int NodeID { get; set; } = default;

    public SelectorNode() :base() {
        //タイトル更新
        title = "SelectorNode";
        //Port追加
        PortAdd();
        //enumFieldの追加
        enumField = new EnumField("挙動", SelectorTipe.Selector1);
        Color color = ColorConversion.GetColor("#3F3F3F");
        enumField.style.backgroundColor = color;
        mainContainer.Add(enumField);

    }
    private void PortAdd()
    {
        var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input,
        Port.Capacity.Multi, typeof(Port));
        inputContainer.Add(inputPort);
        OutputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output,
            Port.Capacity.Multi, typeof(Port));
        outputContainer.Add(OutputPort);
    }
    private enum SelectorTipe
    {
        Selector1, Selector2, Selector3
    }
}
