using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private Button _retryButton;
    [SerializeField] private int _sceneIndexToLoad = 0;
    [SerializeField] private IDCard _idCard;
    private GameOverScreen _gameOverScreen;

    private void Awake()
    {
        _gameOverScreen = GetComponent<GameOverScreen>();
    }

    private void Start()
    {
        _retryButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _gameOverScreen.OnGameOver += EnableRetyButton;
        _gameOverScreen.OnTimeFinished += PlayerLost;
        _retryButton.onClick.AddListener(LoadSceneByIndex);
    }

    private void OnDisable()
    {
        _gameOverScreen.OnTimeFinished -= PlayerLost;
    }

    public void GetIDCard(IDCard card)
    {
        _idCard = card;
        _idCard.OnIDFound += PlayerWins;
    }

    void PlayerWins()
    {
        Debug.Log("<color=#0F3>You Win!!</color>");
        _gameOverScreen.PlayerWon = true;
        _idCard.OnIDFound -= PlayerWins;
    }

    void PlayerLost()
    {
        Debug.Log("<color=#F30>You Lose!!</color>");
        _idCard.gameObject.SetActive(false);
    }

    void LoadSceneByIndex()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(0);
    }

    void EnableRetyButton()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _retryButton.gameObject.SetActive(true);
    }
}
