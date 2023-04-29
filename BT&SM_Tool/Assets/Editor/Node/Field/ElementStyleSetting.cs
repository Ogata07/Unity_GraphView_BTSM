using UnityEngine;
using UnityEngine.UIElements;

public class ElementStyleSetting : VisualElement
{
    public static void Setting(VisualElement visual) {
        //��
        visual.style.paddingBottom = 15;

        //�g�֌W
        visual.style.borderTopWidth = 1;
        visual.style.borderTopColor = Color.black;

        //�ۂ�
        visual.style.borderBottomLeftRadius = 10;
        visual.style.borderBottomRightRadius = 10;
        visual.style.borderTopLeftRadius = 10;
        visual.style.borderTopRightRadius = 10;

        //�w�i�F
        visual.style.backgroundColor = new StyleColor(Color.grey);

    }
}
