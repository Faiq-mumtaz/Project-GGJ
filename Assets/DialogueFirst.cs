using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogueFirst : MonoBehaviour
{
    public TextMeshProUGUI dialogText;

    [TextArea(2, 5)]
    public string[] dialogs;

    public float textSpeed = 0.05f;
    public string SceneName = "Gameplay"; // nama scene gameplay

    int index;
    bool isTyping;

    void Start()
    {
        dialogText.text = "";
        StartCoroutine(TypeDialog());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogText.text = dialogs[index];
                isTyping = false;
            }
            else
            {
                NextDialog();
            }
        }
    }

    IEnumerator TypeDialog()
    {
        isTyping = true;
        dialogText.text = "";

        foreach (char c in dialogs[index])
        {
            dialogText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    void NextDialog()
    {
        index++;

        if (index < dialogs.Length)
        {
            StartCoroutine(TypeDialog());
        }
        else
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}