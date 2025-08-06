using UnityEngine;
using ColorAttributes;

[CreateAssetMenu(fileName = "ColorAttributeAndStatus", menuName = "Scriptable Objects/ColorAttributeAndStatus")]
public class ColorAttributeAndStatus : ScriptableObject
{
    [Header("Status")]
    [SerializeField] ColorAttribute _colorType;
    [SerializeField] float _hp;
    [SerializeField] float _atk;
    [SerializeField] float _def;
    [SerializeField] float _speed;
    [SerializeField] int _jumpRange;

    public ColorAttribute ColorType { get { return _colorType; } }
    public float HP { get { return _hp; } }
    public float ATK { get { return _atk; } }
    public float DEF { get { return _def; } }
    public float SPEED { get { return _speed; } }
    public int JumpRange { get { return _jumpRange; } }
}

namespace ColorAttributes
{
    public enum ColorAttribute
    {
        [InspectorName("赤")] Red,
        [InspectorName("オレンジ")] Orange,
        [InspectorName("黄色")] Yellow,
        [InspectorName("緑")] Green,
        [InspectorName("水色")] RightBlue,
        [InspectorName("青")] Blue,
        [InspectorName("紫")] Purple,
        [InspectorName("白")] White,
        [InspectorName("黒")] Black,
        [InspectorName("無色")] Colorless
    }
}
