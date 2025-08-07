using ColorAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Warp;
using static UnityEngine.Rendering.GPUSort;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IColorChange
{
    [SerializeField] ColorAttributeAndStatus _colorAttribute;
    [SerializeField] GameObject _attackFieldUP;
    [SerializeField] GameObject _attackFieldDOWN;
    [SerializeField] GameObject _attackFieldRight;
    [SerializeField] GameObject _attackFieldLeft;
    [SerializeField] float _attackInterval = 0.5f;
    [SerializeField] float _attackEffectiveTime = 0.1f;

    Rigidbody2D _rb2d;
    GameObject _attackField;
    Animator _animator;

    int _areaIndexX;
    int _areaIndexY;
    int _areaIndexZ;
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
        SetCurrentArea();
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

        transform.position += new Vector3(_moveX, _moveY) * _colorAttribute.SPEED;

        if (Input.GetMouseButtonDown(0) && !_isAttacking)
        {
            Debug.Log("AttackStart");
            _isAttacking = true;
            StartCoroutine(AttackCoroutine(_attackField));
        }
    }

    void FixedUpdate()
    {
        _rb2d.linearVelocity = Vector3.zero;
    }

    IEnumerator AttackCoroutine(GameObject obj)
    {
        _delta = 0;
        obj.SetActive(true);
        yield return new WaitForSeconds(_attackEffectiveTime);
        obj.SetActive(false);
        while (true)
        {
            _delta += Time.deltaTime;
            if (_delta >= _attackInterval)
            {
                Debug.Log("AttackEnd");
                _isAttacking = false;
                yield break;
            }
            yield return null;
        }
    }

    public void ExtractColor(ColorAttribute color)
    {

    }

    void SetCurrentArea()
    {
        var createLabyrinth = FindFirstObjectByType<CreateLabyrinth>();
        _areaIndexX = createLabyrinth.StartIndexX * 2 + 1;
        _areaIndexY = createLabyrinth.StartIndexY * 2 + 1;
        _areaIndexZ = createLabyrinth.StartIndexZ * 2 + 1;
    }

    void Warp(WarpStart warpS)
    {
        var createLabyrinth = FindFirstObjectByType<CreateLabyrinth>();
        switch (warpS.Warp)
        {
            case WarpAttribute.R:
                createLabyrinth.AreaDic[(_areaIndexX, _areaIndexY, _areaIndexZ)].SetActive(false);
                createLabyrinth.AreaDic[(_areaIndexX + 2, _areaIndexY, _areaIndexZ)].SetActive(true);
                _areaIndexX += 2;
                break;
            case WarpAttribute.L:
                createLabyrinth.AreaDic[(_areaIndexX, _areaIndexY, _areaIndexZ)].SetActive(false);
                createLabyrinth.AreaDic[(_areaIndexX - 2, _areaIndexY, _areaIndexZ)].SetActive(true);
                _areaIndexX -= 2;
                break;
            case WarpAttribute.U:
                createLabyrinth.AreaDic[(_areaIndexX, _areaIndexY, _areaIndexZ)].SetActive(false);
                createLabyrinth.AreaDic[(_areaIndexX, _areaIndexY + 2, _areaIndexZ)].SetActive(true);
                _areaIndexY += 2;
                break;
            case WarpAttribute.D:
                createLabyrinth.AreaDic[(_areaIndexX, _areaIndexY, _areaIndexZ)].SetActive(false);
                createLabyrinth.AreaDic[(_areaIndexX, _areaIndexY - 2, _areaIndexZ)].SetActive(true);
                _areaIndexY -= 2;
                break;
            case WarpAttribute.US:
                createLabyrinth.AreaDic[(_areaIndexX, _areaIndexY, _areaIndexZ)].SetActive(false);
                createLabyrinth.AreaDic[(_areaIndexX, _areaIndexY, _areaIndexZ - 2)].SetActive(true);
                _areaIndexZ -= 2;
                break;
            case WarpAttribute.DS:
                createLabyrinth.AreaDic[(_areaIndexX, _areaIndexY, _areaIndexZ)].SetActive(false);
                createLabyrinth.AreaDic[(_areaIndexX, _areaIndexY, _areaIndexZ + 2)].SetActive(true);
                _areaIndexZ += 2;
                break;
            default:
                break;
        }

        var warpE = GameObject.FindGameObjectsWithTag("Warp");
        foreach (var warp in warpE)
        {
            if (warpS.Warp == warp.GetComponent<WarpEnd>().Warp)
            {
                //StartCoroutine(WarpCoroutine(warp.transform.position));
                transform.position = warp.transform.position;
                break;
            }
        }
    }

    IEnumerator WarpCoroutine(Vector3 pos)
    {
        yield return new WaitForSeconds(0.3f);
        transform.position = pos;
        yield break;
    }

    IEnumerator WarpWait(WarpStart warpS)
    {
        yield return new WaitForSeconds(1.0f);
        Warp(warpS);
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var warpS = collision.gameObject.GetComponent<WarpStart>();
        if (warpS)
        {
            Debug.Log("b");
            Warp(warpS);
            //StartCoroutine(WarpWait(warpS));
        }
    }
}
