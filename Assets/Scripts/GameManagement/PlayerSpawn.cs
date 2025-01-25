using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _spawnPoint;

    void Start()
    {
        _playerPrefab = GameObject.FindGameObjectWithTag("Player");

        PlacePlayerOnSpawnPoint();
    }

    void PlacePlayerOnSpawnPoint()
    {
        _playerPrefab.transform.position = _spawnPoint.transform.position;
        Debug.Log("Player as been placed");
    }
}