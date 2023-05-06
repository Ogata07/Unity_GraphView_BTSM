using UnityEngine;
using UnityEngine.UIElements;

public class ElementStyleSetting : VisualElement
{
    public static void Setting(VisualElement visual) {
        //��
        visual.style.paddingBottom = 3;

        //�g�֌W
        visual.style.borderTopWidth = 1;
        visual.style.borderTopColor = Color.black;

        //�ۂ�
        visual.style.borderBottomLeftRadius = 0;
        visual.style.borderBottomRightRadius = 0;
        visual.style.borderTopLeftRadius = 0;
        visual.style.borderTopRightRadius = 0;

        //�w�i�F
        visual.style.backgroundColor = new StyleColor(Color.grey);

    }
}
