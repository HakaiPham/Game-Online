using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Fusion;

public class PauseMenu : NetworkBehaviour
{
    public GameObject pausePanel;
    public Button pauseButton;
    public Button resumeButton;
    public Button retryButton;
    public Button mainMenuButton;

    private NetworkRunner _runner;
    private bool _isResetting = false;

    private void Start()
    {
       
        _runner = FindObjectOfType<NetworkRunner>();
        pausePanel.SetActive(false);

        pauseButton.onClick.AddListener(TogglePause);
        resumeButton.onClick.AddListener(ResumeGame);
        retryButton.onClick.AddListener(RetryGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        bool isPaused = pausePanel.activeSelf;
        pausePanel.SetActive(!isPaused);
        Time.timeScale = isPaused ? 1f : 0f;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RetryGame()
    {
        //PlayerPrefs.DeleteAll();
        //// Dừng game mạng trước khi reload để tránh spawn player mới
        //if (_runner != null)
        //{
        //    _runner.Shutdown(); // Quan trọng!
        //}

        //// Load lại scene sau khi shutdown
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void ReturnToMainMenu()
    {
        PlayerPrefs.DeleteAll();
        if (_runner != null)
        {
            _runner.Shutdown(); // Quan trọng: tắt kết nối trước
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}