using System.Collections.Generic;
using UnityEngine;

public class IDSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private GameObject _idCardPrefab;

    private void Start()
    {
        SpawnIDCard();
    }

    private void SpawnIDCard()
    {
        int randIndex = Random.Range(0, _spawnPoints.Count);
        Transform spawnPoint = _spawnPoints[randIndex];
        Instantiate(_idCardPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
