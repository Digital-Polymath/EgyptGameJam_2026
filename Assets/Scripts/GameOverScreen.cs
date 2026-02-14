using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public event System.Action OnTimeFinished;
    [field: SerializeField] public bool PlayerWon { get; set; }

    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private string _winText;
    [SerializeField] private string _loseText;
    [SerializeField] private CanvasGroup _gameOverCanvasGroup;
    [SerializeField] private Color _winColor;
    [SerializeField] private Color _loseColor;
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private float _timeLimit;
    [SerializeField] private float _timerStartingFontSize;
    [SerializeField] private float _timerMaxFontSize;

    void Start()
    {
        _gameOverCanvasGroup.alpha = 0;
        _timerText.fontSize = _timerStartingFontSize;
        StartCoroutine(GameCountdown());
    }


    IEnumerator GameCountdown()
    {
        float t = 0;
        float fontSize = _timerStartingFontSize;

        while (t < 1 && !PlayerWon)
        {
            t += Time.deltaTime / _timeLimit;
            _timerText.text = $"<mspace=0.45em>{_timeLimit - Mathf.RoundToInt(t * _timeLimit)}";
            _timerText.fontSize = Mathf.Lerp(fontSize, _timerMaxFontSize, t * t * t);
            yield return null;
        }

        yield return StartCoroutine(FadeInGameOverScreen());
    }

    IEnumerator FadeInGameOverScreen()
    {
        float t = 0;
        float startAlpha = _gameOverCanvasGroup.alpha;
        float targetAlpha = 1f;

        _timerText.text = "";

        if (PlayerWon)
        {
            while (t < 1)
            {
                t += Time.deltaTime / _fadeDuration;
                _gameOverCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
                yield return null;
            }

            _gameOverCanvasGroup.alpha = targetAlpha;

            _gameOverText.text = _winText;
            _gameOverText.color = _winColor;

        }
        else
        {
            _gameOverCanvasGroup.alpha = targetAlpha;

            OnTimeFinished?.Invoke();
            yield return new WaitForSeconds(_fadeDuration);
            _gameOverText.text = _loseText;
            _gameOverText.color = _loseColor;
        }
    }
}
