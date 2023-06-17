using UnityEngine;
/// <summary>
/// colorCodeをUnityEngine.Colorに変換するクラス
/// </summary>

public static class ColorConversion 
{
    public static UnityEngine.Color GetColor(string colorCode) {
        UnityEngine.Color colorValue=default(UnityEngine.Color);
        if (ColorUtility.TryParseHtmlString(colorCode, out colorValue))
            return colorValue;
        else
            return colorValue;
            
    }
}
