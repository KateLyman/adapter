using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpController : MonoBehaviour
{
    private Animator animator;
    public bool isWave2;
    public bool isWave3;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (isWave2)
        {
            animator.SetBool("isWave2", true);
        }
        if (isWave3)
        {
            animator.SetBool("isWave3", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
