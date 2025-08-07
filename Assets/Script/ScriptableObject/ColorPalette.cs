using UnityEngine;
using ColorAttributes;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ColorPalette", menuName = "Scriptable Objects/ColorPalette")]
public class ColorPalette : ScriptableObject
{
    [SerializeField] List<ColorData> _colorList;

    public List<ColorData> ColorList { get { return _colorList; } }

    [System.Serializable]
    public class ColorData
    {
        public ColorAttribute _colorAttribute;
        public Color _color;
    }
}
