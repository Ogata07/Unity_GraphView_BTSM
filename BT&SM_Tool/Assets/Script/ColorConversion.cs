using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

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