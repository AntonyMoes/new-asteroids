using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject restartMenu;
    [SerializeField] TextMeshProUGUI finalScoreText;
    
    public void UpdateScore(float newScore) {
        scoreText.text = $"Score: {newScore}";
        finalScoreText.text = $"Final score: {newScore}";
    }

    public void SetUIMode(bool isPlayingMode) {
        scoreText.enabled = isPlayingMode;
        restartMenu.SetActive(!isPlayingMode);
    }
}
