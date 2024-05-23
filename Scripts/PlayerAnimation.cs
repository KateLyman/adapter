using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private InputManager inputManager;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 currentMovement;
    private bool isTransitioning;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = transform.parent.GetComponent<InputManager>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTransitioning)
        {
            currentMovement = inputManager.GetMovement();
        }

        if (currentMovement.x > 0 && !inputManager.IsScanning())
        {
            spriteRenderer.flipX = true;
        }
        else if (currentMovement.x < 0 && !inputManager.IsScanning())
        {
            spriteRenderer.flipX = false;
        }

        if (currentMovement == Vector2.zero)
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isUp", false);
            animator.SetBool("isDown", false);
        }
        else
        {

            if (currentMovement.y > 0 && currentMovement.y > Mathf.Abs(currentMovement.x))
            {
                animator.SetBool("isUp", true);
                animator.SetBool("isDown", false);
                //animator.Play("SwimUp");
            }
            else if (currentMovement.y < 0 && Mathf.Abs(currentMovement.y) > Mathf.Abs(currentMovement.x))
            {
                animator.SetBool("isUp", false);
                animator.SetBool("isDown", true);
                //animator.Play("SwimDown");
            }
            else
            {
                animator.SetBool("isUp", false);
                animator.SetBool("isDown", false);
                animator.Play("SwimHorizontal");
            }
            animator.SetBool("isMoving", true);
        }

    }

    public void StartAimAnimation()
    {
        animator.SetBool("isAiming", true);
    }

    public void EndAimAnimation()
    {
        animator.SetBool("isAiming", false);
    }
}

