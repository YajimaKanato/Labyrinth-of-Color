using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AttackField : MonoBehaviour
{
    [SerializeField] float _knockBackPower = 10;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            AttackAction(collision.gameObject.GetComponent<Rigidbody2D>());
    }

    void AttackAction(Rigidbody2D rb2d)
    {
        rb2d?.AddForce(transform.right, ForceMode2D.Impulse);
    }
}
