using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorData", menuName = "Color/ColorData")]
public class ColorData : ScriptableObject
{
    public List<Color> colorList = new List<Color>
    {
        Color.red,
        new Color(255 / 255.0f, 127 / 255.0f, 0 / 255.0f),
        Color.yellow,
        Color.green,
        Color.blue,
        new Color(75 / 255.0f, 0 / 255.0f, 130 / 255.0f),
        new Color(148 / 255.0f, 0 / 255.0f, 211 / 255.0f)
    };

    //public Color grey = Color.grey;
    //public Color green = Color.green;
    //public Color blue = Color.blue;
    //public Color red = Color.red;
    //public Color yellow = Color.yellow;

    //public Color red = Color.red;
    //public Color orange = new Color(255 / 255.0f, 127 / 255.0f, 0 / 255.0f);
    //public Color yellow = Color.yellow;
    //public Color green = Color.green;
    //public Color blue = Color.blue;
    //public Color indigo = new Color(75 / 255.0f, 0 / 255.0f, 130 / 255.0f);
    //public Color violet = new Color(148 / 255.0f, 0 / 255.0f, 211 / 255.0f);

}
