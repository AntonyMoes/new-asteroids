using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] Spawner spawner;
    [SerializeField] UIController uiController;
    [SerializeField] int initialWaveSize;
    [SerializeField] float initialWaveCooldown;
    [SerializeField] float minimalWaveCooldown;

    float _score;

    void Start() {
        spawner.SetGetShotCallback(UpdateScore);
        StartGame();
    }

    void UpdateScore(float additionalScore) {
        _score += additionalScore;
        uiController.UpdateScore(_score);
    }

    void ResetScore() {
        _score = 0;
        uiController.UpdateScore(_score);
    }

    public void StartGame() {
        ResetScore();

        uiController.SetUIMode(true);

        var player = spawner.SpawnPlayer();
        player.GetComponent<PlayerController>().SetDestroyCallback(EndGame);
        StartCoroutine(SpawnAsteroids());
    }

    void EndGame() {
        StopAllCoroutines();
        uiController.SetUIMode(false);
        spawner.ClearMap();
    }

    IEnumerator SpawnAsteroids() {
        var difficulty = 0;
        while (true) {
            spawner.SpawnAsteroidWave(initialWaveSize + difficulty / 3);

            yield return new WaitForSeconds(Mathf.Max(initialWaveCooldown - difficulty * 0.5f, minimalWaveCooldown));
            difficulty++;
        }
    }
}
