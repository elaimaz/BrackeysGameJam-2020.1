using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollsionDetection : MonoBehaviour
{
    private PlayerController playerController;
   
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Vector2 dir = (collision.transform.position - transform.position).normalized;
            Debug.Log(dir);
            if (dir.x > 0)
            {
                dir.x = 1;
            }
            else
            {
                dir.x = -1;
            }
            //dir.x = Mathf.Ceil(dir.x);
            StartCoroutine(playerController.StopPlayerMove());
            playerController.PushPlayer(dir);
        }
    }
}
