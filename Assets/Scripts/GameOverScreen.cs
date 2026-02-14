using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public event System.Action OnTimeFinished;
    [field: SerializeField] public bool PlayerWon { get; set; }

    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private string _winText;
    [SerializeField] private string _loseText;
    [SerializeField] private CanvasGroup _gameOverCanvasGroup;
    [SerializeField] private Color _winColor;
    [SerializeField] private Color _loseColor;
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private float _timeLimit;

    void Start()
    {
        _gameOverCanvasGroup.alpha = 0;
        StartCoroutine(GameCountdown());
    }


    IEnumerator GameCountdown()
    {
        float t = 0;

        while (t < 1 && !PlayerWon)
        {
            t += Time.deltaTime / _timeLimit;
            yield return null;
        }

        yield return StartCoroutine(FadeInGameOverScreen());
    }

    IEnumerator FadeInGameOverScreen()
    {
        float t = 0;
        float startAlpha = _gameOverCanvasGroup.alpha;
        float targetAlpha = 1f;

        while (t < 1)
        {
            t += Time.deltaTime / _fadeDuration;
            _gameOverCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }

        _gameOverCanvasGroup.alpha = targetAlpha;

        if (PlayerWon)
        {
            _gameOverText.text = _winText;
            _gameOverText.color = _winColor;
        }
        else
        {
            OnTimeFinished?.Invoke();
            _gameOverText.text = _loseText;
            _gameOverText.color = _loseColor;
        }
    }
}
