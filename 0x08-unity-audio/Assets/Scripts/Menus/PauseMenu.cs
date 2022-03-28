using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public AudioMixerSnapshot paused, normal;
    bool isPaused = false;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PlayerPrefs.SetInt("paused", 1);
            pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        paused.TransitionTo(0);
    }

    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerPrefs.SetInt("paused", 0);
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        normal.TransitionTo(0);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        normal.TransitionTo(0);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Options()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Options");
    }
}
