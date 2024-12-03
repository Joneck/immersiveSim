using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public List<string> Texts = new List<string>();

    public float TextSpeed;
    public bool CanSkipText = false;

    public bool IsDialogueOn;

    public TextMeshProUGUI SpeachText;
    public GameObject SpeachFrame;

    private int currentDialogueIndex = 0;

    public GameObject Player;

    //Enter Dialogue
    private void OnMouseOver()
    {
        if(Input.GetKeyDown(KeyCode.E) && !IsDialogueOn)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            Player.GetComponent<PlayerMovement>().CanMove = false;
            IsDialogueOn = true;
            currentDialogueIndex = 0;
            SpeachText.text = "";
            SpeachFrame.SetActive(true);

            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        if (Texts.Count > currentDialogueIndex)
        {
            StartCoroutine(CreateText(Texts[currentDialogueIndex]));
        }
        else
        {
            StartCoroutine(EndDialogue());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(EndDialogue());
        }
        if (Input.anyKeyDown && CanSkipText)
        {
            CanSkipText = false;
            SpeachText.text = "";

            currentDialogueIndex++;
            StartDialogue();
        }
        if (IsDialogueOn)
        {
            transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z));
        }
    }

    IEnumerator CreateText(string text)
    {
        // Display the text letter by letter
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < text.Length; i++)
        {
            yield return new WaitForSeconds(TextSpeed);
            SpeachText.text += text[i];
        }

        // Allow skipping to the next dialogue
        CanSkipText = true;
    }

    IEnumerator EndDialogue()
    {
        yield return new WaitForSeconds(0.01f);

        IsDialogueOn = false;
        currentDialogueIndex = 0;
        SpeachText.text = "";
        SpeachFrame.SetActive(false);
        Player.GetComponent<PlayerMovement>().CanMove = true;
    }
}
