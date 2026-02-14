using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private IDCard _idCard;
    private GameOverScreen _gameOverScreen;

    private void Awake()
    {
        _gameOverScreen = GetComponent<GameOverScreen>();
    }

    private void OnEnable()
    {        
        _gameOverScreen.OnTimeFinished += PlayerLost;
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
}
