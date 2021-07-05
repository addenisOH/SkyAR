using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string sceneName;

    public void OnClick()
    {
        if (sceneName.Length == 0)
        {
            return;
        }

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
