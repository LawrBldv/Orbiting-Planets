using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausesPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel; // Reference to the panel GameObject
    [SerializeField] private Button continueButton; // Reference to the button that resumes the game
    [SerializeField] private Button quitButton; // Reference to the button that loads the main menu

    private bool isPaused = true; // Track if the game is paused

    private void Start()
    {
        // Ensure the panel and button references are assigned
        if (panel == null || continueButton == null || quitButton == null)
        {
            Debug.LogError("Panel, continue button, or main menu button is not assigned.");
            return;
        }

        // Pause the game by setting time scale to 0
        PauseGame();

        // Show the panel
        panel.SetActive(true);

        // Add listeners to the buttons
        continueButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(ExitGame);
    }

    private void Update()
    {
        // Check for Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void ResumeGame()
    {
        Cursor.visible = false;
        // Hide the panel
        panel.SetActive(false);

        // Resume the game by setting time scale to 1
        Time.timeScale = 1f;

        isPaused = false;
    }

    private void PauseGame()
    {
        Cursor.visible = true;
        // Show the panel
        panel.SetActive(true);

        // Pause the game by setting time scale to 0

        isPaused = true;
    }

    private void ExitGame()
    {
        Cursor.visible = true;
        // Load the MainMenu scene
        Application.Quit();
    }
}
