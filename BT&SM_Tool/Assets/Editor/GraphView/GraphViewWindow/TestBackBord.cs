using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;

public class TestBackBord : GraphView
{
    public GraphAsset graphAsset;
    private readonly string backColorCode = "#7fff00";
    public TestBackBord(EditorWindow editorWindow, GraphAsset graphAsset)
    {
        this.graphAsset = graphAsset;
        SetInitial(editorWindow);

    }
    public void SetInitial(EditorWindow editorWindow)
    {
        this.style.width = 300;
        this.style.height = 750;
        var colorCode = ColorConversion.GetColor(backColorCode);
        colorCode.a = 0.2f;//Aだけ半透明にするために変更
        this.style.backgroundColor = colorCode;
        var box = new Box ();
        box.Add(new TextField());
        box.Add(new IntegerField());
        box.Add(new TextField());
        box.Add(new IntegerField());
        box.Add(new EnumField());
        this.Add(box);
    }
}
