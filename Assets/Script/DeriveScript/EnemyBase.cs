using ColorAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyBase : MonoBehaviour, IColorChange
{
    [SerializeField] ColorAttributeAndStatus _colorAttribute;
    [SerializeField] ColorPalette _colorPalette;
    [SerializeField] float _moveInterval = 0.5f;
    [SerializeField] float _attackInterval = 0.5f;
    [SerializeField] float _knockBackTime = 0.1f;

    Rigidbody2D _rb2d;
    Coroutine _coroutine;
    GameObject _player;

    Vector3 _moveVec;

    float _currentHP;
    float _currentATK;
    float _currentDEF;
    float _currentSPEED;
    float _currentJR;
    float _delta;
    bool _isAttacking = false;
    bool _isKnocking = false;
    public float CurrentATK { get { return _currentATK; } }

    const float DECELERATION = 0.9f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0;
        ColorChange();
        _player = GameObject.FindWithTag("Player");
        if (!_player)
        {
            Debug.LogWarning("PlayerÇ™null");
        }

        if (this.tag != "Enemy")
        {
            this.tag = "Enemy";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isKnocking)
        {
            _moveVec = (_player.transform.position - transform.position).normalized;
            _rb2d.linearVelocity = _moveVec;
        }
    }


    IEnumerator KnockBackCoroutine()
    {
        _isKnocking = true;
        _rb2d.linearVelocity = Vector3.zero;
        _rb2d.AddForce((transform.position - _player.transform.position).normalized * AttackField.KnockBackPower, ForceMode2D.Impulse);   

        _delta = 0;
        while (true)
        {
            _delta += Time.deltaTime;
            if (_delta >= _knockBackTime)
            {
                _isKnocking = false;
                yield break;
            }
            _rb2d.linearVelocity *= DECELERATION;
            yield return null;
        }
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    protected abstract void Attack();

    public void ExtractColor(ColorAttribute color)
    {

    }

    /// <summary>
    /// êFÇïœÇ¶ÇÈä÷êî
    /// </summary>
    void ColorChange()
    {
        foreach (var color in _colorPalette.ColorList)
        {
            if (_colorAttribute.ColorType == color.ColorAttribute)
            {
                gameObject.GetComponent<SpriteRenderer>().color = color.Color;
                break;
            }
        }
    }

    void Damage()
    {
        Debug.Log("<color=yellow>E</color>:Damage");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AttackField")
        {
            _coroutine=StartCoroutine(KnockBackCoroutine());
            Damage();
        }
    }
}
