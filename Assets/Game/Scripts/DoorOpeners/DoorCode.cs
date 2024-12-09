using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCode : MonoBehaviour
{
    public string Code;
    public List<ChangeNumber> CodeNumbers = new();
    public List<OpenDoor> DoorToOpen = new();

    public AudioClip SuccessSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    }

    public void CheckCode()
    {
        string ActualCode = "";
        for(int i=0; i<CodeNumbers.Count; i++)
        {
            ActualCode += CodeNumbers[i].Number.ToString();
        }

        if (ActualCode == Code)
        {
            audioSource.PlayOneShot(SuccessSound);
            for (int i=0; i< DoorToOpen.Count; i++)
            {
                DoorToOpen[i].CanOpen = true;
            }
        }
    }
}
