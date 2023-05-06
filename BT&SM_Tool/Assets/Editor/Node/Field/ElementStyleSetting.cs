using UnityEngine;
using UnityEngine.UIElements;

public class ElementStyleSetting : VisualElement
{
    public static void Setting(VisualElement visual) {
        //ãÛîí
        visual.style.paddingBottom = 3;

        //ògä÷åW
        visual.style.borderTopWidth = 1;
        visual.style.borderTopColor = Color.black;

        //ä€Ç›
        visual.style.borderBottomLeftRadius = 0;
        visual.style.borderBottomRightRadius = 0;
        visual.style.borderTopLeftRadius = 0;
        visual.style.borderTopRightRadius = 0;

        //îwåiêF
        visual.style.backgroundColor = new StyleColor(Color.grey);

    }
}
