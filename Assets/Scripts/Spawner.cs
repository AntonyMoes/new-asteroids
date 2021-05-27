using System;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] GameObject map;
    [SerializeField] DrawingModeSelector drawingModeSelector;
    [SerializeField] BoxCollider2D screenBounds;

    [SerializeField] float playerSafeZoneSize;
    Action<float> _getShotCallback;
    GameObject _player;

    GameObject PlayerSpawner() {
        _player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        _player.GetComponent<PlayerController>().SetObjectInstantiator(Spawn);
        return _player;
    }

    GameObject EnemySpawner() {
        var playerSafeBounds = new Bounds(_player.transform.position, Vector2.one * playerSafeZoneSize);

        Vector3 position;
        while (playerSafeBounds.Contains(position = screenBounds.bounds.GetRandomPointInBounds())) { }

        var rotation = Utils.GetRandomRotation();
        var enemy = Instantiate(enemyPrefab, position, rotation);

        enemy.GetComponent<CanBeShot>().SetGetShotCallback(_getShotCallback);

        return enemy;
    }

    GameObject Spawn(Func<GameObject> spawner) {
        var spawned = spawner();

        spawned.GetComponent<Drawable>().SetDrawingModeSelector(drawingModeSelector);
        spawned.transform.parent = map.transform;

        return spawned;
    }

    public void SetGetShotCallback(Action<float> getShotCallback) {
        _getShotCallback = getShotCallback;
    }

    public void ClearMap() {
        for (var i = 0; i < map.transform.childCount; i++) {
            Destroy(map.transform.GetChild(i).gameObject);
        }
    }

    public GameObject SpawnPlayer() {
        return Spawn(PlayerSpawner);
    }

    public void SpawnEnemyWave(int waveSize) {
        for (var i = 0; i < waveSize; i++) {
            Spawn(EnemySpawner);
        }
    }
}
