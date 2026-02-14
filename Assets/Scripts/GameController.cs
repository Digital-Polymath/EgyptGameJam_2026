using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private IDCard _idCard;

    public void GetIDCard(IDCard card)
    {
        _idCard = card;

        _idCard.OnIDFound += PlayerWins;
    }

    void PlayerWins()
    {
        Debug.Log("<color=#0F3>You Win!!</color>");
        _idCard.OnIDFound -= PlayerWins;
    }
}
