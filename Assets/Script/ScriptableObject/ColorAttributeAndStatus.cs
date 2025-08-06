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
        [InspectorName("��")] Red,
        [InspectorName("�I�����W")] Orange,
        [InspectorName("���F")] Yellow,
        [InspectorName("��")] Green,
        [InspectorName("���F")] RightBlue,
        [InspectorName("��")] Blue,
        [InspectorName("��")] Purple,
        [InspectorName("��")] White,
        [InspectorName("��")] Black,
        [InspectorName("���F")] Colorless
    }
}
