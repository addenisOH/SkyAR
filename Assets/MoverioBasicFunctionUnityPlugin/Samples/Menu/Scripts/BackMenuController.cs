using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenuController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }
    }

    public void OnClick()
    {
        BackToMenu();
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
