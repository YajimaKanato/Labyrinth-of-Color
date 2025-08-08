using ColorAttributes;
using UnityEngine;

public class ColorExtractButton : MonoBehaviour
{
    [SerializeField] ColorAttribute _color;

    public void ColorExtract()
    {
        var player = GameObject.FindWithTag("Player");
        var enemys = GameObject.FindGameObjectsWithTag("Enemy");
        player.GetComponent<IColorChange>().ExtractColor(_color);
        foreach (var enemy in enemys)
        {
            enemy.GetComponent<IColorChange>().ExtractColor(_color);
        }
        transform.parent.gameObject.SetActive(false);
    }
}
