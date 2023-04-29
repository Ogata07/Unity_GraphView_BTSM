using UnityEngine;
using UnityEngine.UIElements;

public class ElementStyleSetting : VisualElement
{
    public static void Setting(VisualElement visual) {
        //ãÛîí
        visual.style.paddingBottom = 15;

        //ògä÷åW
        visual.style.borderTopWidth = 1;
        visual.style.borderTopColor = Color.black;

        //ä€Ç›
        visual.style.borderBottomLeftRadius = 10;
        visual.style.borderBottomRightRadius = 10;
        visual.style.borderTopLeftRadius = 10;
        visual.style.borderTopRightRadius = 10;

        //îwåiêF
        visual.style.backgroundColor = new StyleColor(Color.grey);

    }
}
