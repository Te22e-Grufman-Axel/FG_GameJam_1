using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenue : MonoBehaviour
{
    [SerializeField] private string MainLevelName = "MainMenu";

    [SerializeField] private GameObject settings;

    private bool inSettings = false;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }

    public void Settings()
    {
        inSettings = !inSettings;

        settings.SetActive(inSettings);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(MainLevelName);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
