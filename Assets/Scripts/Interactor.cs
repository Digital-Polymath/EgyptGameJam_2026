using UnityEngine;
using UnityEngine.InputSystem;

public interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public event System.Action<IInteractable> OnPlayerInteraction;

    public Transform interactorSource;
    public float interactorRange = 2f;

    private StarterAssetsInput _inputActions;

    private void Awake()
    {
        _inputActions = new StarterAssetsInput();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.Player.Interact.performed += Interact;
    }
    private void OnDisable()
    {
        _inputActions.Disable();
        _inputActions.Player.Interact.performed -= Interact;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Interact action performed");

        Ray r = new Ray(interactorSource.position, interactorSource.forward);

        if (Physics.Raycast(r, out RaycastHit hitInfo, interactorRange))
        {
            IInteractable interactable = hitInfo.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                OnPlayerInteraction?.Invoke(interactable);
            }
        }
    }
}
