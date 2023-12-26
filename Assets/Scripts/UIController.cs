using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private TextMeshProUGUI _resultText;

    private void Start()
    {
        EndGameTrigger.OnWin += ShowWin;
        _restartButton.onClick.AddListener(RestartClickHandler);
    }

    private void ShowWin()
    {
        _resultText.text = "Win";
        _resultText.gameObject.SetActive(true);
    }

    public void ShowLose()
    {
        _resultText.text = "Lose";
        _resultText.gameObject.SetActive(true);
    }

    public void RestartClickHandler()
    {
        _resultText.gameObject.SetActive(false);
    }
}