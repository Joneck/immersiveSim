using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDetector : MonoBehaviour
{
    public int NeededKeyID;

    public List<OpenDoor> MyDoors = new();

    public AudioClip SuccessSound;
    private AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Key>() != null)
        {
            if(collision.gameObject.GetComponent<Key>().KeyID == NeededKeyID)
            {
                for(int i = 0; i < MyDoors.Count; i++)
                {
                    MyDoors[i].CanOpen = true;
                }
            }
        }

        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        audioSource.PlayOneShot(SuccessSound);
        Destroy(collision.gameObject);
    }
}
