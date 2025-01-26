using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu; // Reference to the pause menu UI
    private bool isPaused = false; // Tracks whether the game is paused

    void Start()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Use Escape as the default pause key
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Resumes the game
    private void ResumeGame()
    {
        Time.timeScale = 1f;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false); 
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}