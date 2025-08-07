using UnityEngine;

/// <summary>
/// �G���A���̂��̂𐶐����邽�߂̃X�N���v�g
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
            Debug.LogWarning("x�͈̔͂𒴂��Ă��܂�");
        }
        if (_areaIndexYTune > _createLabyrinth.RoomID.GetLength(1))
        {
            Debug.LogWarning("y�͈̔͂𒴂��Ă��܂�");
        }
        if (_areaIndexZTune > _createLabyrinth.RoomID.GetLength(2))
        {
            Debug.LogWarning("z�͈̔͂𒴂��Ă��܂�");
        }

        if (_createLabyrinth)
        {
            if (_areaData)
            {
                var go = Instantiate(_areaData, transform.position, Quaternion.identity);
                if (go)
                {
                    go.GetComponent<WallSetting>()?.SetIndex(_areaIndexX, _areaIndexY, _areaIndexZ);
                }
            }
        }
        else
        {
            Debug.LogWarning("CreateLabyrinth���ݒ肳��Ă��܂���");
        }
    }
}