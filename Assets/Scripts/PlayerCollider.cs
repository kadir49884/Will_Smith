using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour, IEnemy, ISecurity
{

    private GameManager gameManager;
    private Animator animPlayer;
    private Rigidbody rb;
    private float slapForceValue = 200;

    void Start()
    {
        gameManager = GameManager.Instance;
        animPlayer = transform.GetComponent<Animator>();
        gameManager.GameStart += GameStarted;
        rb = transform.GetComponent<Rigidbody>();
    }

    private void GameStarted()
    {
        animPlayer.SetInteger("AnimStatus", 1);
    }

    public void CrashEnemy(Transform enemyTransform)
    {
        animPlayer.SetLayerWeight(1, 1);
        animPlayer.SetTrigger("SlapTrigger");
        enemyTransform.GetComponent<Animator>().enabled = false;

        Execute.After(0.4f, () =>
        {
            foreach (var item in enemyTransform.GetComponentsInChildren<Rigidbody>())
            {
                item.isKinematic = false;
            }
        });
        Execute.After(0.5f, () =>
        {
            foreach (var item in enemyTransform.GetComponentsInChildren<Rigidbody>())
            {

                if (item.gameObject.name == "Spine")
                {
                    item.AddForce(slapForceValue * (Vector3.forward * 20 + Vector3.up * 4));

                    if (transform.position.x > 0)
                    {
                        item.AddForce(slapForceValue * (Vector3.right * 8));
                    }
                    else
                    {
                        item.AddForce(slapForceValue * (Vector3.left * 8));
                    }
                }
                else
                {
                    item.AddForce(slapForceValue * (Vector3.forward * 5 + Vector3.up * 2));

                    if (transform.position.x > 0)
                    {
                        item.AddForce(slapForceValue * (Vector3.right * 3));
                    }
                    else
                    {
                        item.AddForce(slapForceValue * (Vector3.left * 3));
                    }
                }


            }
        });


        Execute.After(0.5f, () =>
        {
            animPlayer.SetLayerWeight(1, 0);
        });

    }




    public void CrashSecurity()
    {
        if (transform.position.x > 0)
        {
            rb.AddForce(Vector3.left * 700);
        }
        else
        {
            rb.AddForce(Vector3.right * 700);
        }
    }


}
