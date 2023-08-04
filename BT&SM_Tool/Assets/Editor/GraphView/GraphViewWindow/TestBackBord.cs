using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using System.Web.Mvc;

public class TestBackBord : GraphView
{
    public GraphAsset graphAsset;
    private readonly string backColorCode = "#7fff00";
    public List<VisualElement> visualElements = new ();
    public TestBackBord(EditorWindow editorWindow, GraphAsset graphAsset)
    {
        this.graphAsset = graphAsset;
        this.name = "TestBackBord";
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
        box.name = "BackBord";
        BackBordElement<IntegerField,int> backBordElement = new BackBordElement<IntegerField,int>();
        visualElements.Add(backBordElement);
        box.Add(backBordElement);
        var scroll = new Scroller();
        box.Add(scroll);   
        this.Add(box);
    }
}
