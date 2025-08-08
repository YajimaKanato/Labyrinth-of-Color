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
    /// ��������Q�[���I�u�W�F�N�g���擾����֐�
    /// </summary>
    /// <returns> GameObject�^���Ԃ��Ă���</returns>
    GameObject GetObject()
    {
        //�d�݂̑��a�̌v�Z
        float maxValue = 0;
        foreach (var value in _enemyList)
        {
            maxValue += value.Wieght;
        }

        //�d�ݕt���m���ɂ��]��
        float rand = Random.Range(0, maxValue);
        float nowValue = 0;
        foreach (var value in _enemyList)
        {//�d�݂̉��Z
            nowValue += value.Wieght;
            //���݂̏d�݂������l�ȏ�ɂȂ�����Q�[���I�u�W�F�N�g��Ԃ�
            if (nowValue >= rand)
            {
                return value.Prefab;
            }
        }

        //����������e�I�u�W�F�N�g�̐�������̘a���傫�������炢����null��Ԃ��悤�ɂȂ�
        return null;
    }
}
