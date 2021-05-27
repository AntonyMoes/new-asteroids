using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] Spawner spawner;
    [SerializeField] UIController uiController;
    [SerializeField] int initialWaveSize;
    [SerializeField] float initialWaveCooldown;
    [SerializeField] float minimalWaveCooldown;
    [SerializeField] float initialUfoCooldown;
    [SerializeField] float minimalUfoCooldown;

    float _score;

    void Start() {
        spawner.SetPointsCallback(UpdateScore);
        StartGame();
    }

    void UpdateScore(float additionalScore) {
        if (!this || !uiController) {
            return;
        }

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
        StartCoroutine(SpawnUfos());
    }

    void EndGame() {
        if (!this) {
            return;
        }

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

    IEnumerator SpawnUfos() {
        yield return new WaitForSeconds(initialUfoCooldown);

        var difficulty = 0;
        while (true) {
            spawner.SpawnUfo();

            difficulty++;
            yield return new WaitForSeconds(Mathf.Max(initialUfoCooldown - difficulty, minimalUfoCooldown));
        }
    }
}
