using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;
/// <summary>
/// バックボードで値を表示するテストクラス
/// </summary>
public class BackBordElement<T,V> : FieldElement
    where T : BaseField<V>, new()
{
    //型データ
    public T value=default;
    public BackBordElement() {
        Inialize();
    }
    private void Inialize() {
        //各種値用のBOX作成
        Box box = new();
        box.style.flexDirection=new StyleEnum<FlexDirection>(FlexDirection.Row);
        box.style.flexWrap = new StyleEnum<Wrap>(Wrap.Wrap);
        //型名
        TextField typeText = new TextField();
        typeText.value = "型名表示";
        typeText.style.width = new StyleLength(new Length(45, LengthUnit.Percent));
        box.Add(typeText);

        //GraphView上にNodeを作成させるCreateButton
        Button button = new Button();
        button.text = "作成";
        button.style.width = new StyleLength(new Length(45, LengthUnit.Percent));
        box.Add(button);

        //変数名
        TextField nameText = new TextField();
        nameText.value = "変数名表示";
        nameText.style.width = new StyleLength(new Length(45, LengthUnit.Percent));
        box.Add(nameText);

        //値
        value=new T();
        value.style.width = new StyleLength(new Length(45, LengthUnit.Percent));
        box.Add(value);
        //追加
        this.Add(box);
    }
}
