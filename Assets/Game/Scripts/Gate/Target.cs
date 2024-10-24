using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Target : MonoBehaviour
{
    public Transform Gate;

    private void Start()
    {
        DOTween.Init();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Gate.DOLocalMove(new Vector3(0, 7, 0), 2f);
        transform.DOLocalMove(new Vector3(0, 5, -0.3f), 2f);
    }
}
