using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public Toggle autoProgressToggle;
    public Slider typingSpeedSlider;

    [Header("Dialogue")]
    public DialogueLine[] dialogueLines;

    [Header("Settings")]
    public float typingSpeed = 0.03f;
    public float autoProgressDelay = 1.5f;

    private int currentLine = 0;
    private bool isTyping = false;
    private bool autoProgress = false;

    private Coroutine typingCoroutine;
    private Coroutine autoProgressCoroutine;


    void Start()
    {
        dialogueText.text = "";
        nameText.text = "";

        
        if (autoProgressToggle != null)
        {
            autoProgressToggle.isOn = false;
            autoProgressToggle.onValueChanged.AddListener(SetAutoProgress);
        }

        // Typing speed slider
        if (typingSpeedSlider != null)
        {
            typingSpeedSlider.onValueChanged.AddListener(SetTypingSpeed);

       
            typingSpeedSlider.value = 1f / typingSpeed;
        }

        DisplayNextLine();
    }


    void Update()
    {
     
        if (!autoProgress && Input.GetButtonDown("Jump"))
        {
            DisplayNextLine();
        }
    }


    public void SetAutoProgress(bool enabled)
    {
        autoProgress = enabled;

        
        if (!autoProgress && autoProgressCoroutine != null)
        {
            StopCoroutine(autoProgressCoroutine);
            autoProgressCoroutine = null;
        }

       
        if (autoProgress && !isTyping)
        {
            autoProgressCoroutine = StartCoroutine(AutoProgress());
        }
    }


    public void SetTypingSpeed(float charactersPerSecond)
    {
       
        if (charactersPerSecond <= 0)
            return;

      
        typingSpeed = 1f / charactersPerSecond;
    }


    void DisplayNextLine()
    {
   
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);

            dialogueText.text = dialogueLines[currentLine - 1].text;
            isTyping = false;

            return;
        }

        
        if (currentLine >= dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        nameText.text = dialogueLines[currentLine].speaker;

        typingCoroutine = StartCoroutine(
            TypeLine(dialogueLines[currentLine].text)
        );

        currentLine++;
    }


    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in line)
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        if (autoProgress)
        {
            autoProgressCoroutine = StartCoroutine(AutoProgress());
        }
    }


    IEnumerator AutoProgress()
    {
        yield return new WaitForSeconds(autoProgressDelay);

        if (autoProgress)
        {
            DisplayNextLine();
        }
    }


    void EndDialogue()
    {
        dialogueText.text = "";
        nameText.text = "";

        print("Dialogue finished!");
    }
}


[System.Serializable]
public class DialogueLine
{
    public string speaker;

    [TextArea(2, 5)]
    public string text;
}