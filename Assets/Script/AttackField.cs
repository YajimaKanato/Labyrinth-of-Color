using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AttackField : MonoBehaviour
{
    static float _knockBackPower = 50;
    public static float KnockBackPower { get { return _knockBackPower; } }

    private void Start()
    {
        if (this.tag != "AttackField")
        {
            this.tag = "AttackField";
        }

        gameObject.SetActive(false);
    }
}
