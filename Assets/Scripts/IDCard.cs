using UnityEngine;

public class IDCard : MonoBehaviour, IInteractable
{
    public event System.Action OnIDFound;

    void IInteractable.Interact()
    {
        gameObject.SetActive(false);

        OnIDFound?.Invoke();
    }
}
