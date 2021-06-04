using System;
using GameLogic;
using UnityEngine;
using Bounds = UnityEngine.Bounds;
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
    [SerializeField] GameObject hudRoot;

    [SerializeField] float playerSafeZoneSize;
    Transform _player;
    Action<float> _pointsCallback;

    GameObject PlayerSpawner() {
        var playerObj = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        playerObj.GetComponent<BarController>().SetHUDRoot(hudRoot);
        _player = playerObj.transform;

        var playerController = playerObj.GetComponent<PlayerController>();
        playerController.SetupWeapons(SpawnDrawable, screenBounds.bounds.size.magnitude);

        return playerObj;
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

        var ufoController = ufo.GetComponent<UfoController>();
        ufoController.SetPlayer(_player);

        return ufo;
    }

    GameObject BigAsteroidSpawner() {
        var position = GetRandomPosition();
        var rotation = Utils.GetRandomRotation();
        var asteroid = Instantiate(bigAsteroidPrefabs.RandomElement(), position, rotation);

        asteroid.GetComponent<IShootable>().OnShot += (points, posVelProvider) => SpawnSmallAsteroids(posVelProvider);

        return asteroid;
    }

    GameObject AsteroidSpawner(IPositionVelocityProvider bigAsteroid) {
        var position = bigAsteroid.Position;
        var rotation = Utils.GetRandomRotation();
        var asteroid = Instantiate(asteroidPrefabs.RandomElement(), position.ToUnity(), rotation);

        var baseSpeed = bigAsteroid.Velocity.Length();
        var asteroidController = asteroid.GetComponent<AsteroidController>();
        asteroidController.minSpeed = baseSpeed * minAsteroidSpeedup;
        asteroidController.minSpeed = baseSpeed * maxAsteroidSpeedup;

        return asteroid;
    }

    GameObject SpawnEnemy(Func<GameObject> spawner) {
        var enemy = spawner();
        enemy.GetComponent<IShootable>().OnShot += (points, posVelProvider) => _pointsCallback(points);
        return enemy;
    }

    GameObject SpawnDrawable(Func<GameObject> spawner) {
        var spawned = spawner();

        spawned.GetComponent<Drawable>().SetDrawingModeSelector(drawingModeSelector);
        spawned.transform.parent = map.transform;

        return spawned;
    }

    public void SetPointsCallback(Action<float> pointsCallback) {
        _pointsCallback = pointsCallback;
    }

    public void ClearMap() {
        for (var i = 0; i < map.transform.childCount; i++) {
            Destroy(map.transform.GetChild(i).gameObject);
        }
    }

    public GameObject SpawnPlayer() {
        return SpawnDrawable(PlayerSpawner);
    }

    public void SpawnUfo() {
        SpawnDrawable(() => SpawnEnemy(UfoSpawner));
    }

    public void SpawnAsteroidWave(int waveSize) {
        for (var i = 0; i < waveSize; i++) {
            SpawnDrawable(() => SpawnEnemy(BigAsteroidSpawner));
        }
    }

    void SpawnSmallAsteroids(IPositionVelocityProvider asteroid) {
        var asteroidsNum = Random.Range(minAsteroidsNum, maxAsteroidsNum + 1);
        for (var i = 0; i < asteroidsNum; i++) {
            SpawnDrawable(() => SpawnEnemy(() => AsteroidSpawner(asteroid)));
        }
    }
}
