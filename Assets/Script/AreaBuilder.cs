using System.Collections;
using UnityEngine;

/// <summary>
/// エリアそのものを生成するためのスクリプト
/// </summary>
public class AreaBuilder : MonoBehaviour
{
    [SerializeField] CreateLabyrinth _createLabyrinth;
    [SerializeField] AreaDataList _areaDatas;
    [SerializeField] int _areaIndexX;
    [SerializeField] int _areaIndexY;
    [SerializeField] int _areaIndexZ;

    GameObject _areaData;

    int _areaIndexXTune;
    int _areaIndexYTune;
    int _areaIndexZTune;

    public int AreaIndexX { get { return _areaIndexXTune; } }
    public int AreaIndexY { get { return _areaIndexYTune; } }
    public int AreaIndexZ { get { return _areaIndexZTune; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randIndex = Random.Range(0, _areaDatas.AreaDatas.Count);
        _areaData = _areaDatas.AreaDatas[randIndex];
        _areaIndexXTune = _areaIndexX * 2 + 1;
        _areaIndexYTune = _areaIndexY * 2 + 1;
        _areaIndexZTune = _areaIndexZ * 2 + 1;
        if (_areaIndexXTune > _createLabyrinth.RoomID.GetLength(0))
        {
            Debug.LogWarning("xの範囲を超えています");
        }
        if (_areaIndexYTune > _createLabyrinth.RoomID.GetLength(1))
        {
            Debug.LogWarning("yの範囲を超えています");
        }
        if (_areaIndexZTune > _createLabyrinth.RoomID.GetLength(2))
        {
            Debug.LogWarning("zの範囲を超えています");
        }

        if (_createLabyrinth)
        {
            if (_areaData)
            {
                var go = Instantiate(_areaData, transform.position, Quaternion.identity);
                _createLabyrinth.AreaDic[(_areaIndexXTune, _areaIndexYTune, _areaIndexZTune)] = go;
                if (go)
                {
                    go.transform.parent = transform;
                    go.GetComponent<WallSetting>()?.SetIndex(_areaIndexX, _areaIndexY, _areaIndexZ);
                    if (_createLabyrinth.StartIndexX == _areaIndexX && _createLabyrinth.StartIndexY == _areaIndexY && _createLabyrinth.StartIndexZ == _areaIndexZ)
                    {
                        go.SetActive(true);
                    }
                    else
                    {
                        StartCoroutine(Wait(go));
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("CreateLabyrinthが設定されていません");
        }
    }

    IEnumerator Wait(GameObject go)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        go.SetActive(false);
        yield break;
    }
}