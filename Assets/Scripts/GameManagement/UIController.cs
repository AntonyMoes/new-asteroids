using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour {
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject restartMenu;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] AudioSource mainPlayer;
    [SerializeField] float menuVolumeModifier;

    public void UpdateScore(float newScore) {
        if (!scoreText || !finalScoreText) {
            return;
        }

        scoreText.text = $"Score: {newScore}";
        finalScoreText.text = $"Final score: {newScore}";
    }

    public void SetUIMode(UIMode uiMode) {
        if (!scoreText || !restartMenu) {
            return;
        }

        startMenu.SetActive(uiMode == UIMode.Start);
        restartMenu.SetActive(uiMode == UIMode.Menu);
        scoreText.enabled = uiMode == UIMode.Game;
        mainPlayer.volume = uiMode == UIMode.Game ? 1 : menuVolumeModifier;
    }
}
