using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Warp;

[RequireComponent(typeof(CircleCollider2D))]
public class WarpStart : MonoBehaviour
{
    [SerializeField] WarpAttribute _warpStart;
    public WarpAttribute Warp { get { return _warpStart; } }

    CircleCollider2D _cc2d;
    Coroutine _coroutine;
    private void Awake()
    {
        _cc2d = GetComponent<CircleCollider2D>();
        if (this.gameObject.layer != LayerMask.NameToLayer("Warp"))
        {
            this.gameObject.layer = LayerMask.NameToLayer("Warp");
        }
    }

    private void OnEnable()
    {
        _coroutine=StartCoroutine(ActiveCoroutine());
        _cc2d.isTrigger = true;
    }

    void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    IEnumerator ActiveCoroutine()
    {
        _cc2d.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _cc2d.enabled = true;
        yield break;
    }
}

namespace Warp
{
    public enum WarpAttribute
    {
        R,
        L,
        U,
        D,
        US,
        DS
    }
}
