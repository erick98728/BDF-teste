using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tester.UI
{
    /// <summary>
    /// Controls the simple PC pause menu used by the prototype.
    /// </summary>
    [DisallowMultipleComponent]
    public class PauseMenuController : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

        [Header("UI")]
        [Tooltip("Panel shown while the prototype is paused.")]
        [SerializeField] private GameObject pausePanel;

        private bool isPaused;

        public bool IsPaused => isPaused;

        private void Awake()
        {
            ResumeTime();
            SetPausePanelVisible(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(pauseKey))
            {
                TogglePause();
            }
        }

        private void OnDisable()
        {
            if (isPaused)
            {
                ContinueGame();
            }
        }

        public void TogglePause()
        {
            if (isPaused)
            {
                ContinueGame();
                return;
            }

            PauseGame();
        }

        public void PauseGame()
        {
            if (isPaused)
            {
                return;
            }

            isPaused = true;
            Time.timeScale = 0f;
            SetPausePanelVisible(true);
        }

        public void ContinueGame()
        {
            isPaused = false;
            ResumeTime();
            SetPausePanelVisible(false);
        }

        public void RestartScene()
        {
            ContinueGame();

            Scene activeScene = SceneManager.GetActiveScene();

            if (!activeScene.IsValid())
            {
                Debug.LogWarning("Pause menu could not restart an invalid active scene.", this);
                return;
            }

            SceneManager.LoadScene(activeScene.name);
        }

        public void QuitGame()
        {
            ContinueGame();

#if UNITY_EDITOR
            Debug.Log("Pause menu requested quit. Application.Quit only closes a build.", this);
#else
            Application.Quit();
#endif
        }

        private static void ResumeTime()
        {
            Time.timeScale = 1f;
        }

        private void SetPausePanelVisible(bool isVisible)
        {
            if (pausePanel == null)
            {
                return;
            }

            pausePanel.SetActive(isVisible);
        }
    }
}
