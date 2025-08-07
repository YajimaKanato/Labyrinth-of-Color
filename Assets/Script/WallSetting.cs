using UnityEngine;

/// <summary>
/// 壁などを生成するためのスクリプト
/// </summary>
public class WallSetting : MonoBehaviour
{
    [Header("Right")]
    [SerializeField] GameObject _rightWall;
    [SerializeField] GameObject _rightWarpStart;
    [SerializeField] GameObject _rightWarpEnd;
    [Header("Left")]
    [SerializeField] GameObject _leftWall;
    [SerializeField] GameObject _leftWarpStart;
    [SerializeField] GameObject _leftWarpEnd;
    [Header("UP")]
    [SerializeField] GameObject _upWall;
    [SerializeField] GameObject _upWarpStart;
    [SerializeField] GameObject _upWarpEnd;
    [Header("Down")]
    [SerializeField] GameObject _downWall;
    [SerializeField] GameObject _downWarpStart;
    [SerializeField] GameObject _downWarpEnd;
    [Header("UpStairs")]
    [SerializeField] GameObject _upStairs;
    [SerializeField] GameObject _upStairsWarpStart;
    [SerializeField] GameObject _upStairsWarpEnd;
    [Header("DownStairs")]
    [SerializeField] GameObject _downStairs;
    [SerializeField] GameObject _downStairsWarpStart;
    [SerializeField] GameObject _downStairsWarpEnd;

    CreateLabyrinth _createLabyrinth;

    int _areaIndexXTune;
    int _areaIndexYTune;
    int _areaIndexZTune;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _createLabyrinth = FindFirstObjectByType<CreateLabyrinth>();
        WallSet();
    }

    public void SetIndex(int x, int y, int z)
    {
        _areaIndexXTune = x * 2 + 1;
        _areaIndexYTune = y * 2 + 1;
        _areaIndexZTune = z * 2 + 1;
    }

    void WallSet()
    {
        //別のエリアにつながっているかどうかを判定して壁などを生成する
        if (_areaIndexXTune != _createLabyrinth.RoomID.GetLength(0) - 2)
        {
            if (_createLabyrinth.RoomID[_areaIndexXTune + 1, _areaIndexYTune, _areaIndexZTune] != 0)
            {
                _rightWall.SetActive(false);
                _rightWarpStart.SetActive(true);
                _rightWarpEnd.SetActive(true);
            }
        }
        if (_areaIndexXTune != 1)
        {
            if (_createLabyrinth.RoomID[_areaIndexXTune - 1, _areaIndexYTune, _areaIndexZTune] != 0)
            {
                _leftWall.SetActive(false);
                _leftWarpStart.SetActive(true);
                _leftWarpEnd.SetActive(true);
            }
        }
        if (_areaIndexYTune != _createLabyrinth.RoomID.GetLength(1) - 2)
        {
            if (_createLabyrinth.RoomID[_areaIndexXTune, _areaIndexYTune + 1, _areaIndexZTune] != 0)
            {
                _upWall.SetActive(false);
                _upWarpStart.SetActive(true);
                _upWarpEnd.SetActive(true);
            }
        }
        if (_areaIndexYTune != 1)
        {
            if (_createLabyrinth.RoomID[_areaIndexXTune, _areaIndexYTune - 1, _areaIndexZTune] != 0)
            {
                _downWall.SetActive(false);
                _downWarpStart.SetActive(true);
                _downWarpEnd.SetActive(true);
            }
        }
        if (_areaIndexZTune != _createLabyrinth.RoomID.GetLength(2) - 2)
        {
            if (_createLabyrinth.RoomID[_areaIndexXTune, _areaIndexYTune, _areaIndexZTune + 1] != 0)
            {
                _downStairs.SetActive(true);
                _downStairsWarpStart.SetActive(true);
                _downStairsWarpEnd.SetActive(true);
            }
            
        }
        if (_areaIndexZTune != 1)
        {
            if (_createLabyrinth.RoomID[_areaIndexXTune, _areaIndexYTune, _areaIndexZTune - 1] != 0)
            {
                _upStairs.SetActive(true);
                _upStairsWarpStart.SetActive(true);
                _upStairsWarpEnd.SetActive(true);
            }
        }
    }
}
