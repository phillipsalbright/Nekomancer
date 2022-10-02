using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private bool paused;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject[] notPauseMenu;
    [SerializeField] private Button selectedButton;
    // Start is called before the first frame update

    [SerializeField]
    AudioSource catAudio;

    [SerializeField]
    AudioSource musicManager;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.action.triggered)
        {
            if (!paused)
            {
                selectedButton.Select();
                catAudio.Pause();
                musicManager.Pause();
                paused = true;
                for (int i =0;i < notPauseMenu.Length; i++)
                {
                    notPauseMenu[i].SetActive(false);
                }
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void Resume()
    {
        catAudio.UnPause();
        musicManager.UnPause();
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        for (int i = 0; i < notPauseMenu.Length; i++)
        {
            notPauseMenu[i].SetActive(true);
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }

    public void Quit()
    {
        Application.Quit();
    }
}
