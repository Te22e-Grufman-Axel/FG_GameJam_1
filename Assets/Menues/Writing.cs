using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime;

public class Writing : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    private InputAction skipAction;
    [SerializeField] private string playLevelName;

    [SerializeField] private string text;
    private string[] textArray;
    [SerializeField] private int textIndex = 0;
    [SerializeField] private int rowIndex = 0;
    [SerializeField] private float letterDelay = 1f;
    [SerializeField] private float rowDelay = 1f;
    [SerializeField] private float sceneDelay = 1f;

    [SerializeField] private TextMeshProUGUI uIText;

    private bool typing = false;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        skipAction = InputSystem.actions.FindAction("Jump");

        textArray = text.Split(",");

        foreach (string s in textArray)
        {
            Debug.Log( s );
        }
    }

    void Update()
    {
        if (skipAction.WasPressedThisFrame())
        {
            SceneManager.LoadScene(playLevelName);
        }

        if (!typing)
        {
            if(rowIndex < textArray.Length)
            {
                if (textIndex < textArray[rowIndex].Length)
                {
                    float delay = letterDelay;
                    string letter = "";

                    if (textIndex == 0)
                    {
                        delay = rowDelay;
                        letter += "<br>";
                    }

                    letter += textArray[rowIndex].Substring(textIndex, 1);
                    textIndex++;

                    StartCoroutine(LetterCoroutine(letter, delay));

                }
                else
                {
                    rowIndex++;
                    textIndex = 0;
                }
            }
            else
            {
                StartCoroutine(NextSceneCoroutine(sceneDelay));
            }
        }
    }

    IEnumerator LetterCoroutine(string s, float delay)
    {
        typing = true;
        yield return new WaitForSeconds(delay);

        uIText.text += s;

        typing = false;
    }

    IEnumerator NextSceneCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(playLevelName);
    }
}
