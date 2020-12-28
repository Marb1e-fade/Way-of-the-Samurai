using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _respawnPoint;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.Equals(_player))
            other.transform.position = _respawnPoint.position;
    }
}
