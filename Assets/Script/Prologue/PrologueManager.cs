using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DialoguePanel
{
    [TextArea(3, 5)]
    public string dialogueText;

    [Tooltip("Kosongkan jika tidak ingin mengganti gambar background dari dialog sebelumnya")]
    public Sprite backgroundImage;
}

public class PrologueManager : MonoBehaviour
{
    [Header("Data Prologue")]
    public DialoguePanel[] dialogueSequence;

    [Header("Referensi UI")]
    public Image backgroundDisplay;
    public TMP_Text dialogueDisplayText;

    [Header("Pengaturan Scene")]
    public string nextSceneName = "MainMenuScene";

    [Header("Pengaturan Efek")]
    public float typingSpeed = 0.05f;

    private int currentIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        if (dialogueSequence.Length > 0)
        {
            ShowPanel(currentIndex);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (SFXManager.Instance != null)
            {
                SFXManager.Instance.PlayClick();
            }

            if (isTyping)
            {
                CompleteText();
            }
            else
            {
                NextPanel();
            }
        }
    }

    void ShowPanel(int index)
    {
        DialoguePanel panel = dialogueSequence[index];

        if (panel.backgroundImage != null)
        {
            backgroundDisplay.sprite = panel.backgroundImage;
        }

        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(panel.dialogueText));
    }

    IEnumerator TypeText(string textToType)
    {
        isTyping = true;
        dialogueDisplayText.text = "";

        foreach (char c in textToType.ToCharArray())
        {
            dialogueDisplayText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void CompleteText()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);

        dialogueDisplayText.text = dialogueSequence[currentIndex].dialogueText;
        isTyping = false;
    }

    void NextPanel()
    {
        currentIndex++;

        if (currentIndex < dialogueSequence.Length)
        {
            ShowPanel(currentIndex);
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}