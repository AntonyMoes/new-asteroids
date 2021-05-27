using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] Spawner spawner;
    [SerializeField] UIController uiController;

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
        spawner.SpawnEnemyWave(3);
    }

    void EndGame() {
        uiController.SetUIMode(false);
        spawner.ClearMap();
    }
}
