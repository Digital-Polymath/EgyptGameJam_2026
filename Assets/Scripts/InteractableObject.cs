using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [field: SerializeField] public string ObjectName { get; private set; }

    public void Interact()
    {
        Debug.Log(ObjectName);
    }
}
