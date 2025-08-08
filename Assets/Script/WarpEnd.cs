using UnityEngine;
using Warp;

public class WarpEnd : MonoBehaviour
{
    [SerializeField] WarpAttribute _warpEnd;
    public WarpAttribute Warp { get { return _warpEnd; } }

    private void Awake()
    {
        if (this.tag != "Warp")
        {
            this.tag = "Warp";
        }
    }
}
