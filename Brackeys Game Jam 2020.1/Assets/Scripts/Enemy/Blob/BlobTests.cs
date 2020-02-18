using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobTests : MonoBehaviour
{
    public bool dead = false;
    public int count = 0;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead == true && count < 1)
        {
            count++;
            animator.SetTrigger("Death");
        }
    }
}
