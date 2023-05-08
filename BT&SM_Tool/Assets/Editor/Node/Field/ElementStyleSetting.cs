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
    public static void SetMargin(VisualElement element, float px)
    {
        element.style.marginLeft = px;
        element.style.marginTop = px;
        element.style.marginRight = px;
        element.style.marginBottom = px;
    }
    public static void SetPadding(VisualElement element, float px)
    {
        element.style.paddingLeft = px;
        element.style.paddingTop = px;
        element.style.paddingRight = px;
        element.style.paddingBottom = px;
    }
}
