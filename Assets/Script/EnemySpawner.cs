using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemyRate
{
    [Header("EnemyData")]
    [SerializeField] GameObject _prefab;
    [SerializeField] float _wieght;

    public GameObject Prefab { get { return _prefab; } }
    public float Wieght { get { return _wieght; } }
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemyRate> _enemyList;
    [SerializeField] float _spawnInterval;
    [SerializeField] int _maxSpawn;

    int _spawnCount;
    float _delta;

    void Update()
    {
        if (_spawnCount < _maxSpawn)
        {
            _delta += Time.deltaTime;
            if (_delta >= _spawnInterval)
            {
                _delta = 0;
                Instantiate(GetObject());
                _spawnCount++;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 生成するゲームオブジェクトを取得する関数
    /// </summary>
    /// <returns> GameObject型が返ってくる</returns>
    GameObject GetObject()
    {
        //重みの総和の計算
        float maxValue = 0;
        foreach (var value in _enemyList)
        {
            maxValue += value.Wieght;
        }

        //重み付き確率による評価
        float rand = Random.Range(0, maxValue);
        float nowValue = 0;
        foreach (var value in _enemyList)
        {//重みの加算
            nowValue += value.Wieght;
            //現在の重みが乱数値以上になったらゲームオブジェクトを返す
            if (nowValue >= rand)
            {
                return value.Prefab;
            }
        }

        //生成上限が各オブジェクトの生成上限の和より大きかったらいずれnullを返すようになる
        return null;
    }
}
