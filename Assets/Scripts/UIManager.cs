using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Interactor _interactor;
    [SerializeField] private TextMeshProUGUI _interactionText;
    [SerializeField] private GameObject _textPanel;
    [SerializeField] private CanvasGroup _textPanelCanvasGroup;
    [SerializeField] private float _fadeDuration = 0.5f;
    [SerializeField] private float _fadeOutWaitTime = 2f;

    private Coroutine _fadeTextCoroutine;

    private void Awake()
    {
        _interactor = FindFirstObjectByType<Interactor>();
    }

    private void Start()
    {        
        _textPanelCanvasGroup.alpha = 0f;
        _interactionText.text = " ";
    }

    private void OnEnable()
    {
        _interactor.OnPlayerInteraction += SetText;
    }
    private void OnDisable()
    {
        _interactor.OnPlayerInteraction -= SetText;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void SetText(IInteractable interactable)
    {
        if (interactable is InteractableObject obj)
        {
            _interactionText.text = obj.ObjectName;
        }

        if (_fadeTextCoroutine != null)
        {
            StopCoroutine(_fadeTextCoroutine);
        }
        _fadeTextCoroutine = StartCoroutine(FadeTextCoroutine());
    }

    private IEnumerator FadeTextCoroutine()
    {
        float t = 0;
        float startAlpha = _textPanelCanvasGroup.alpha;
        float targetAlpha = 1f;

        float duration = Mathf.InverseLerp(startAlpha, targetAlpha, startAlpha) * _fadeDuration;

        while (t < 1)
        {
            t += Time.deltaTime / duration;
            _textPanelCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }
        _textPanelCanvasGroup.alpha = targetAlpha;

        yield return new WaitForSeconds(_fadeOutWaitTime);

        t = 0;
        startAlpha = _textPanelCanvasGroup.alpha;
        targetAlpha = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / _fadeDuration;
            _textPanelCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }
        _textPanelCanvasGroup.alpha = targetAlpha;
    }
}
