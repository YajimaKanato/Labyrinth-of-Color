using UnityEngine;

public class GameDirector : MonoBehaviour
{
    static GameDirector _instance;
    static int _enemyCount;
    public static int EnemyCount {  get { return _enemyCount; }  set { _enemyCount++; } }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!_instance)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _enemyCount = 0;
    }
}
