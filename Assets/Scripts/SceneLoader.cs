using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int _sceneIndexToLoad = 0;
    [SerializeField] private Button _startButton;

    private void Start()
    {
        _startButton.onClick.AddListener(LoadSceneByIndex);
    }

    void LoadSceneByIndex()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneIndexToLoad);
    }
}
