using DG.Tweening;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public bool CanOpen;
    private bool IsClosed;

    private Vector3 BasicRotation;
    public Vector3 TargetRotation;

    private Vector3 BasicPosition;
    public Vector3 TargetPosition;

    public AudioClip OpenSound;
    public AudioClip ClosedSound;
    private AudioSource audioSource;


    private void Start()
    {
        BasicPosition = transform.localPosition;
        BasicRotation = transform.localEulerAngles;
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        DOTween.Init();
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (CanOpen)
            {
                audioSource.PlayOneShot(OpenSound);
                if (IsClosed)
                {
                    IsClosed = false;
                    transform.DOLocalMove(BasicPosition, 3f);
                    transform.DOLocalRotate(BasicRotation, 3f);
                }
                else
                {
                    IsClosed = true;
                    transform.DOLocalMove(TargetPosition, 3f);
                    transform.DOLocalRotate(TargetRotation, 3f);
                }
            }
            else
            {
                audioSource.PlayOneShot(ClosedSound);
            }
        }
    }
}
