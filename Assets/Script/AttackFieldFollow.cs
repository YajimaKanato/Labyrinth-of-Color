using UnityEngine;

public class AttackFieldFollow : MonoBehaviour
{
    [SerializeField] GameObject _player;
    PlayerController _playerHP;

    private void Start()
    {
        _playerHP = _player.GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_playerHP.CurrentHP > 0)
        {
            transform.position = _player.transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
