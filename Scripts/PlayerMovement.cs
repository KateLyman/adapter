using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputManager inputManager;
    private Rigidbody2D rb;
    [SerializeField] bool facingRight;
    public PlayerSO playerSO;
    [SerializeField] GameEventSO transitionEvent;

    // Transition variables
    private Vector2 lastMovement;
    private bool isTransitioning = false;
    private Vector2 currentMovement;

    // Animator and sprite
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Audio
    private AudioSource audioSource;
    [SerializeField] AudioClip swimStartSound;
    private bool movingStarted = false;

    // Events
    [SerializeField] GameEventSO playerStartMoving;
    [SerializeField] GameEventSO playerStopMoving;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isTransitioning = false;

        transform.position = new Vector3(playerSO.GetStartPos().x, playerSO.GetStartPos().y, 0.0f);

        playerSO.SetIsHidden(false);
        playerSO.SetLureActive(false);
        playerSO.SetSonarActive(false);
        playerSO.SetHasBones(true);
    }

    private void FixedUpdate()
    {
        rb.AddForce(currentMovement * playerSO.GetCurrentPlayerSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTransitioning)
        {
            currentMovement = inputManager.GetMovement();
        }
        else
        {
            currentMovement = lastMovement;
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
            
            audioSource.Stop();
            
            if (movingStarted)
            {
                movingStarted = false;
                playerStopMoving.Raise();
            }
        }
        else
        {
            if (!movingStarted)
            {
                movingStarted = true;
                audioSource.PlayOneShot(swimStartSound, .3f);
                audioSource.Play();
                playerStartMoving.Raise();
            }

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

    public bool FacingRight()
    {
        return GetComponent<SpriteRenderer>().flipX;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Transition" && !inputManager.IsAiming() && !isTransitioning)
        {
            isTransitioning = true;
            transitionEvent.Raise();

            lastMovement = inputManager.GetMovement();
            collision.gameObject.GetComponent<Transition>().TransitionScene();
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
