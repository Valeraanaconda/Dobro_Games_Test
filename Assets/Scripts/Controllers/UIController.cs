using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private const string WIN_TEXT = "Win!";
    private const string LOSE_TEXT = "Lose";
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _resultText;

    private void Start()
    {
        _restartButton.onClick.AddListener(RestartClickHandler);
        EndGameTrigger.OnWin += ShowWin;
        EnemyService.OnPlayerLose += ShowLose;
    }

    private void ShowWin()
    {
        _resultText.text = WIN_TEXT;
        _resultText.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
    }

    private void ShowLose()
    {
        _resultText.text = LOSE_TEXT;
        _resultText.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
    }

    private void RestartClickHandler()
    {
        _resultText.gameObject.SetActive(false);
        _restartButton.gameObject.SetActive(false);
    }
}