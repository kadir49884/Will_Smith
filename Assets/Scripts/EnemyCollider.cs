using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.GetComponent<IEnemy>()?.CrashEnemy(transform);
    }
}
