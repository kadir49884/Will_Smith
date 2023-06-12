using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        other.transform.GetComponent<ISecurity>()?.CrashSecurity();
        transform.GetComponent<Animator>().SetInteger("SecurityStatus", 1);
     
    }
}
