using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject[] bigAsteroidPrefabs;
    [SerializeField] GameObject[] asteroidPrefabs;
    [SerializeField] float minAsteroidSpeedup;
    [SerializeField] float maxAsteroidSpeedup;
    [SerializeField] int minAsteroidsNum;
    [SerializeField] int maxAsteroidsNum;
    [SerializeField] GameObject[] ufoPrefabs;

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

    Vector3 GetRandomPosition() {
        var playerSafeBounds = new Bounds(_player.transform.position, Vector2.one * playerSafeZoneSize);

        Vector3 position;
        while (playerSafeBounds.Contains(position = screenBounds.bounds.GetRandomPointInBounds())) { }

        return position;
    }

    GameObject UfoSpawner() {
        var position = GetRandomPosition();
        var ufo = Instantiate(ufoPrefabs.RandomElement(), position, Quaternion.identity);

        ufo.GetComponent<PlayerFollower>().SetPlayer(_player.transform);

        return ufo;
    }

    GameObject BigAsteroidSpawner() {
        var position = GetRandomPosition();
        var rotation = Utils.GetRandomRotation();
        var asteroid = Instantiate(bigAsteroidPrefabs.RandomElement(), position, rotation);

        asteroid.GetComponent<CanBeShot>().SetGeneralCallback(SpawnSmallAsteroids);

        return asteroid;
    }

    GameObject AsteroidSpawner(GameObject bigAsteroid) {
        var position = bigAsteroid.GetComponent<Collider2D>().bounds.GetRandomPointInBounds();
        var rotation = Utils.GetRandomRotation();
        var asteroid = Instantiate(asteroidPrefabs.RandomElement(), position, rotation);

        var baseSpeed = bigAsteroid.GetComponent<Rigidbody2D>().velocity.magnitude;
        var speedSetter = asteroid.GetComponent<SpeedSetter>();
        speedSetter.minSpeed = baseSpeed * minAsteroidSpeedup;
        speedSetter.minSpeed = baseSpeed * maxAsteroidSpeedup;

        return asteroid;
    }

    GameObject EnemySpawner(Func<GameObject> spawner) {
        var spawned = spawner();
        spawned.GetComponent<CanBeShot>().SetPointsCallback(_getShotCallback);
        return spawned;
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

    public void SpawnUfo() {
        Spawn(() => EnemySpawner(UfoSpawner));
    }

    public void SpawnAsteroidWave(int waveSize) {
        for (var i = 0; i < waveSize; i++) {
            Spawn(() => EnemySpawner(BigAsteroidSpawner));
        }
    }

    void SpawnSmallAsteroids(GameObject asteroid) {
        var asteroidsNum = Random.Range(minAsteroidsNum, maxAsteroidsNum + 1);
        for (var i = 0; i < asteroidsNum; i++) {
            Spawn(() => EnemySpawner(() => AsteroidSpawner(asteroid)));
        }
    }
}
