using UnityEngine;

public class StrongEnemy : EnemyBase
{
    static bool _isColorless = false;

    protected override void Attack()
    {

    }

    protected override void ColorSetting()
    {
        if (_isColorless)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
        else
        {
            ColorChange();
        }
    }

    protected override void IsColorLess()
    {
        if (!_isColorless)
        {
            StrongEnemy._isColorless = true;
        }
    }
}
