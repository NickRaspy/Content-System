using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    [SerializeField] private string mainScene;
    [SerializeField] private string managerScene;

    public static SceneSwapper Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    private void OnGUI()
    {
        if(Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.F6)
        {
            if(SceneManager.GetActiveScene().name == mainScene) SceneManager.LoadSceneAsync(managerScene);
            else SceneManager.LoadSceneAsync(mainScene);
        }
    }
}
