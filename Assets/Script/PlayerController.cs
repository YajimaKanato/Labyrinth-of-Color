using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] ColorAttributeAndStatus _CAAS;
    [SerializeField] GameObject _attackFieldUP;
    [SerializeField] GameObject _attackFieldDOWN;
    [SerializeField] GameObject _attackFieldRight;
    [SerializeField] GameObject _attackFieldLeft;
    [SerializeField] float _attackInterval = 0.5f;

    Rigidbody2D _rb2d;
    GameObject _attackField;
    Animator _animator;

    float _moveX, _moveY;
    float _currentHP;
    float _currentATK;
    float _currentDEF;
    float _currentSPEED;
    float _currentJR;
    float _delta;
    bool _isAttacking = false;

    public float CurrentATK { get { return _currentATK; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _rb2d.gravityScale = 0;
        _animator = GetComponent<Animator>();
        _attackField = _attackFieldRight;
    }

    // Update is called once per frame
    void Update()
    {
        _moveX = Input.GetAxisRaw("Horizontal");
        _moveY = Input.GetAxisRaw("Vertical");

        if (_moveX == 1 && Mathf.Abs(_moveY) == 0)
        {
            _attackField = _attackFieldRight;
            //_animator.SetBool("", true);
        }
        else if (_moveX == -1 && Mathf.Abs(_moveY) == 0)
        {
            _attackField = _attackFieldLeft;
            //_animator.SetBool("", true);
        }

        if (_moveY == 1 && Mathf.Abs(_moveX) == 0)
        {
            _attackField = _attackFieldUP;
            //_animator.SetBool("", true);
        }
        else if (_moveY == -1 && Mathf.Abs(_moveX) == 0)
        {
            _attackField = _attackFieldDOWN;
            //_animator.SetBool("", true);
        }

        transform.position += new Vector3(_moveX, _moveY) * _CAAS.SPEED;

        if (Input.GetMouseButtonDown(0) && !_isAttacking)
        {
            Debug.Log("AttackStart");
            _isAttacking = true;
            StartCoroutine(AttackCoroutine(_attackField));
        }
    }

    IEnumerator AttackCoroutine(GameObject obj)
    {
        _delta = 0;
        obj.SetActive(true);
        while (true)
        {
            _delta += Time.deltaTime;
            if (_delta >= _attackInterval)
            {
                Debug.Log("AttackEnd");
                _isAttacking = false;
                obj.SetActive(false);
                yield break;
            }
            yield return null;
        }
    }
}
