using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ColorExtracter : MonoBehaviour
{
    [SerializeField] GameObject _selectColor;
    [SerializeField] GameObject _enemyCountMessage;
    [SerializeField] int _enemyCount;

    CircleCollider2D _cc2d;
    private void Start()
    {
        if (gameObject.layer != LayerMask.NameToLayer("ColorExtract"))
        {
            gameObject.layer = LayerMask.NameToLayer("ColorExtract");
        }
        _cc2d = GetComponent<CircleCollider2D>();
        _cc2d.isTrigger = true;
    }

    public void ColorChange()
    {
        if (GameDirector.EnemyCount >= _enemyCount)
        {
            _selectColor.SetActive(true);
        }
        else
        {
            _enemyCountMessage.SetActive(true);
            Debug.Log("ìGÇì|ÇµÇΩêîÇ™ë´ÇËÇ‹ÇπÇÒ");
        }
    }
}
