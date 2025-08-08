using ColorAttributes;
using System.Collections;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyBase : MonoBehaviour, IColorChange
{
    [SerializeField] ColorAttributeAndStatus _colorAttribute;
    [SerializeField] ColorPalette _colorPalette;
    [SerializeField] float _moveInterval = 0.5f;
    [SerializeField] float _attackInterval = 0.5f;
    [SerializeField] float _knockBackTime = 0.1f;
    [SerializeField] float _attackRange = 1.1f;

    Rigidbody2D _rb2d;
    Coroutine _coroutine;
    GameObject _player;
    Animator _animator;
    DamageCalculation _damageCaluc;

    Vector3 _moveVec;

    float _currentHP;
    float _currentATK;
    float _currentDEF;
    float _currentSPEED;
    float _currentJR;
    float _delta;
    bool _isAttacking = false;
    public bool IsAttacking { get { return _isAttacking; } }
    bool _isKnocking = false;
    public float CurrentATK { get { return _currentATK; } }

    const float DECELERATION = 0.9f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Setting();
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0;
        _player = GameObject.FindWithTag("Player");
        if (!_player)
        {
            Debug.LogWarning("PlayerÇ™null");
        }

        if (this.tag != "Enemy")
        {
            this.tag = "Enemy";
        }
        _animator = GetComponent<Animator>();
        _damageCaluc = new DamageCalculation();
    }

    void Setting()
    {
        _currentHP = _colorAttribute.HP;
        _currentATK = _colorAttribute.ATK;
        _currentDEF = _colorAttribute.DEF;
        _currentSPEED = _colorAttribute.SPEED;
        ColorSetting();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isKnocking)
        {
            _isAttacking = false;
            //_animator.SetTrigger("");
        }

        if ((_player.transform.position - transform.position).magnitude <= _attackRange)
        {
            _isAttacking = true;
            //_animator.SetBool("", true);
        }
        else
        {
            _isAttacking = false;
            //_animator.SetBool("", false);
        }

        if (_currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (!_isKnocking)
        {
            _moveVec = (_player.transform.position - transform.position).normalized;
            _rb2d.linearVelocity = _moveVec;
        }
        if (_isAttacking)
        {
            _rb2d.linearVelocity = Vector3.zero;
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

    private void OnDestroy()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        GameDirector.EnemyCount++;
        Debug.Log(GameDirector.EnemyCount);
    }

    protected abstract void Attack();
    protected abstract void ColorSetting();
    protected abstract void IsColorLess();

    public void ExtractColor(ColorAttribute color)
    {
        if (color == _colorAttribute.ColorType)
        {
            IsColorLess();
        }
        ColorSetting();
    }

    /// <summary>
    /// êFÇïœÇ¶ÇÈä÷êî
    /// </summary>
    protected void ColorChange()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AttackField")
        {
            Debug.Log("<color=yellow>E</color>:Damage");
            _coroutine = StartCoroutine(KnockBackCoroutine());
            _currentHP -= _damageCaluc.Damage(_player.GetComponent<PlayerController>().CurrentATK, _currentDEF);
        }
    }
}
