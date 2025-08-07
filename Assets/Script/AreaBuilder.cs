using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AreaData
{
    [SerializeField] GameObject _areaPrefab;
    [SerializeField] GameObject _rightWall;
    [SerializeField] GameObject _leftWall;
    [SerializeField] GameObject _upWall;
    [SerializeField] GameObject _downWall;
    [SerializeField] GameObject _upStairs;
    [SerializeField] GameObject _downStairs;

    public GameObject AreaPrefab { get { return _areaPrefab; } }
    public GameObject RightWallPrefab { get { return _rightWall; } }
    public GameObject LeftWallPrefab { get { return _leftWall; } }
    public GameObject UpWallPrefab { get { return _upWall; } }
    public GameObject DownWallPrefab { get { return _downWall; } }
    public GameObject UpStairsPrefab { get { return _upStairs; } }
    public GameObject DownStairsPrefab { get { return _downStairs; } }
}

public class AreaBuilder : MonoBehaviour
{
    [SerializeField] CreateLabyrinth _createLabyrinth;
    [SerializeField] List<AreaData> _areaDatas;
    [SerializeField] int _areaIndexX;
    [SerializeField] int _areaIndexY;
    [SerializeField] int _areaIndexZ;

    AreaData _areaData;

    int _areaIndexXTune;
    int _areaIndexYTune;
    int _areaIndexZTune;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randIndex = Random.Range(0, _areaDatas.Count);
        _areaData = _areaDatas[randIndex];
        _areaIndexXTune = _areaIndexX * 2 + 1;
        _areaIndexYTune = _areaIndexY * 2 + 1;
        _areaIndexZTune = _areaIndexZ * 2 + 1;
        if (_createLabyrinth)
        {
            Instantiate(_areaData.AreaPrefab, transform.position, Quaternion.identity);
            WallSet();
        }
        else
        {
            Debug.LogWarning("CreateLabyrinthÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
        }
    }

    void WallSet()
    {
        if (_areaIndexXTune != _createLabyrinth.RoomID.GetLength(0) - 2)
        {
            if (_createLabyrinth.RoomID[_areaIndexXTune + 1, _areaIndexYTune, _areaIndexZTune] == 0)
            {
                _areaData.RightWallPrefab.SetActive(true);
            }
        }
        if (_areaIndexXTune != 1)
        {
            if (_createLabyrinth.RoomID[_areaIndexXTune - 1, _areaIndexYTune, _areaIndexZTune] == 0)
            {
                _areaData.LeftWallPrefab.SetActive(true);
            }
        }
        if (_areaIndexYTune != _createLabyrinth.RoomID.GetLength(1) - 2)
        {
            if (_createLabyrinth.RoomID[_areaIndexXTune, _areaIndexYTune + 1, _areaIndexZTune] == 0)
            {
                _areaData.UpWallPrefab.SetActive(true);
            }
        }
        if (_areaIndexYTune != 1)
        {
            if (_createLabyrinth.RoomID[_areaIndexXTune, _areaIndexYTune - 1, _areaIndexZTune] == 0)
            {
                _areaData.DownWallPrefab.SetActive(true);
            }
        }
        if (_areaIndexZTune != _createLabyrinth.RoomID.GetLength(2) - 2)
        {
            if (_createLabyrinth.RoomID[_areaIndexXTune, _areaIndexYTune, _areaIndexZTune + 1] == 0)
            {
                _areaData.DownStairsPrefab.SetActive(false);
            }
        }
        if (_areaIndexZ != 1)
        {
            if (_createLabyrinth.RoomID[_areaIndexXTune, _areaIndexYTune, _areaIndexZTune - 1] == 0)
            {
                _areaData.UpStairsPrefab.SetActive(false);
            }
        }
    }
}