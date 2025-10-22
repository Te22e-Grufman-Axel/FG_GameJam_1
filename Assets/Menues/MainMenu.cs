using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string playLevelName = "TestingScene";

    [SerializeField] private GameObject settings;

    private bool inSettings = false;

    public void Play()
    {
        SceneManager.LoadScene(playLevelName);
    }

    public void Settings()
    {
        inSettings = !inSettings;

        settings.SetActive(inSettings);
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
