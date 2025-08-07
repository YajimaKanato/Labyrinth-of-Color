using UnityEngine;
using ColorAttributes;
using System.Collections.Generic;
using TMPro;

[CreateAssetMenu(fileName = "ColorPalette", menuName = "Scriptable Objects/ColorPalette")]
public class ColorPalette : ScriptableObject
{
    [SerializeField] List<ColorData> _colorList;

    public List<ColorData> ColorList { get { return _colorList; } }

    [System.Serializable]
    public class ColorData
    {
        [SerializeField] ColorAttribute _colorAttribute;
        [SerializeField] Color _color;

        public ColorAttribute ColorAttribute { get { return _colorAttribute; } }
        public Color Color { get { return _color; } }
    }
}
