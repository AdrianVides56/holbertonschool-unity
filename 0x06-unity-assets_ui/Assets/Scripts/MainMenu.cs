using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Update()
    {
        Cursor.visible = true;
    }
    public void LevelSelect(int level)
    {
        SceneManager.LoadScene("level0" + level.ToString());
    }
    public void Options()
    {
        PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Options");
    }

    public void Exit()
    {
        Debug.Log("Exit aplicattion");
        Application.Quit();
    }
}
