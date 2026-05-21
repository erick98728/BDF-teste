using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tester.UI
{
    /// <summary>
    /// Simple pause menu controller for PC prototype flow.
    /// </summary>
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private string mainMenuSceneName = "";

        private bool isPaused;

        private void Start()
        {
            SetPaused(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(pauseKey))
            {
                TogglePause();
            }
        }

        public void TogglePause()
        {
            SetPaused(!isPaused);
        }

        public void ContinueGame()
        {
            SetPaused(false);
        }

        public void RestartScene()
        {
            Time.timeScale = 1f;
            isPaused = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ExitOrMainMenu()
        {
            Time.timeScale = 1f;
            isPaused = false;

            if (!string.IsNullOrWhiteSpace(mainMenuSceneName) && Application.CanStreamedLevelBeLoaded(mainMenuSceneName))
            {
                SceneManager.LoadScene(mainMenuSceneName);
                return;
            }

#if UNITY_EDITOR
            Debug.Log("PauseMenuController: sem menu configurado. No build, usará Application.Quit().", this);
#else
            Application.Quit();
#endif
        }

        private void SetPaused(bool pause)
        {
            isPaused = pause;
            Time.timeScale = isPaused ? 0f : 1f;

            if (pausePanel != null)
            {
                pausePanel.SetActive(isPaused);
            }
        }

        private void OnDisable()
        {
            if (Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
            }
        }
    }
}
