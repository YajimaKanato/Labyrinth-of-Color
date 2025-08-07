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
    [SerializeField] List<AreaData> _areaPrefab;
    [SerializeField] int _areaIndexX;
    [SerializeField] int _areaIndexY;
    [SerializeField] int _areaIndexZ;

    AreaData _areaData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randIndex = Random.Range(0, _areaPrefab.Count);
        _areaData = _areaPrefab[randIndex];
        WallSet();
    }

    void WallSet()
    {
        if (_areaIndexX != _createLabyrinth.RoomID.GetLength(0))
        {
            if (_createLabyrinth.RoomID[_areaIndexX + 1, _areaIndexY, _areaIndexZ] == 0)
            {
                _areaData.RightWallPrefab.SetActive(true);
            }
        }
        if (_areaIndexX != 0)
        {
            if (_createLabyrinth.RoomID[_areaIndexX - 1, _areaIndexY, _areaIndexZ] == 0)
            {
                _areaData.LeftWallPrefab.SetActive(true);
            }
        }
        if (_areaIndexY != _createLabyrinth.RoomID.GetLength(1))
        {
            if (_createLabyrinth.RoomID[_areaIndexX, _areaIndexY + 1, _areaIndexZ] == 0)
            {
                _areaData.UpWallPrefab.SetActive(true);
            }
        }
        if (_areaIndexY != 0)
        {
            if (_createLabyrinth.RoomID[_areaIndexX, _areaIndexY - 1, _areaIndexZ] == 0)
            {
                _areaData.DownWallPrefab.SetActive(true);
            }
        }
        if (_areaIndexZ != _createLabyrinth.RoomID.GetLength(2))
        {
            if (_createLabyrinth.RoomID[_areaIndexX, _areaIndexY, _areaIndexZ + 1] == 0)
            {
                _areaData.DownStairsPrefab.SetActive(true);
            }
        }
        if (_areaIndexZ != 0)
        {
            if (_createLabyrinth.RoomID[_areaIndexX, _areaIndexY, _areaIndexZ - 1] == 0)
            {
                _areaData.UpStairsPrefab.SetActive(true);
            }
        }
    }
}