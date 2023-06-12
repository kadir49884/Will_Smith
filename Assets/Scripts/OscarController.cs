using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscarController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DOVirtual.DelayedCall(Random.Range(0, 0.6f), () =>
          {
              OscarMove();
          });
    }
    private void OscarMove()
    {
        
        transform.DOMoveY(transform.position.y + 0.3f, 0.7f).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DOMoveY(transform.position.y - 0.3f, 0.7f).SetEase(Ease.Linear).OnComplete(() =>
            {
                OscarMove();
            });
        });
    }


    private void OnTriggerEnter(Collider other)
    {
        transform.gameObject.SetActive(false);
    }

}
