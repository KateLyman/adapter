using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CreatureBehaviors : MonoBehaviour
{
    public enum BHV { idle, patrol, scroll, explore, pulse, flee, hide, lured };

    [SerializeField] bool facingRight;
    public bool isSchool = false;

    private Transform currentWaypoint;
    private Vector2 currentExplore;
    private Transform explorePoint;
    private Vector2 scrollStartPosition;

    // Coroutine vars
    private bool idleRunning = false;
    private bool pulseRunning = false;
    private bool soundRunning = false;
    private bool fleeRunning = false;

    // Bad coding variables
    private bool firstHide = true;
    private bool firstFlee = true;

    private float arrivalMargin = 0.1f;

    private Animator creatureAnimator;
    private Rigidbody2D rigidBody;
    private CreatureAudioManager creatureAudioManager;
    private CreatureController creatureController;

    private PlayerSO playerSO;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        creatureAnimator = GetComponent<Animator>();
        creatureAudioManager = GetComponent<CreatureAudioManager>();
        creatureController = GetComponent<CreatureController>();

        if (creatureController.waypoints.Count > 0)
        {
            currentWaypoint = creatureController.waypoints[0];
        }
        if (creatureController.explorePoint != null)
        {
            explorePoint = creatureController.explorePoint;
            currentExplore = SelectRandomPoint(explorePoint);
            //adjustedExplore = new Vector2(currentExplore.x + creatureController.explorePoint.position.x, currentExplore.y + creatureController.explorePoint.position.y);
            currentExplore = new Vector2(currentExplore.x + explorePoint.position.x, currentExplore.y + explorePoint.position.y);
        }

        playerSO = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().playerSO;

        Idle(Random.Range(0.2f, 0.5f));
    }

    public void MoveCreature(Vector2 destination, float speed, ForceMode2D forceMode)
    {
        creatureAnimator.SetBool("isMoving", true);
        float xDistance = transform.position.x - destination.x;
        float yDistance = transform.position.y - destination.y;

        Vector2 direction = new Vector2(xDistance, yDistance).normalized * -speed;

        rigidBody.AddForce(direction, forceMode);

        if (!soundRunning)
        {
            StartCoroutine(PlaySoundMove(creatureController.creature.GetSoundTime()));
        }

        // Rotate and flip if necessary
        //Quaternion rotation = Quaternion.LookRotation(destination.position - transform.position, transform.TransformDirection(Vector3.up));
        //transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        Flip(direction);
    }

    private bool HasArrived(Vector2 destination)
    {
        bool xWithinMargin = transform.position.x < destination.x + arrivalMargin && transform.position.x > destination.x - arrivalMargin;
        bool yWithinMargin = transform.position.y < destination.y + arrivalMargin && transform.position.y > destination.y - arrivalMargin;

        return xWithinMargin && yWithinMargin;
    }

    private void Flip(Vector2 direction)
    {
        if (direction.x > 0)
        {
            if (!facingRight)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        } 
        else
        {
            if (!facingRight)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    public void Idle(float waitTime)
    {
        creatureAnimator.SetBool("isMoving", false);
        creatureAnimator.SetBool("tooClose", false);
        if (!idleRunning)
        {
            StartCoroutine(IdleCO(waitTime));
        }
    }

    private IEnumerator IdleCO(float waitTime)
    {
        idleRunning = true;
        yield return new WaitForSeconds(waitTime);
        idleRunning = false;
        yield break;
    }

    private IEnumerator PlaySoundMove(float waitTime)
    {
        soundRunning = true;
        creatureAudioManager.PlayMoveSound();
        yield return new WaitForSeconds(waitTime);
        soundRunning = false;
        yield break;
    }

    public void Patrol(List<Transform> waypoints, float speed, float waitTime)
    {
        if (!idleRunning)
        {
            if (HasArrived(currentWaypoint.position))
            {
                currentWaypoint = SelectNextWaypoint(waypoints);
                Idle(waitTime);
            }
            else
            {
                MoveCreature(currentWaypoint.position, speed, ForceMode2D.Force);
            }
        }
    }

    private Vector2 SelectRandomPoint(Transform explorePoint)
    {
        Vector2 randomPoint = Random.insideUnitCircle;
        randomPoint = new Vector2(randomPoint.x * (explorePoint.localScale.x / 2), randomPoint.y * (explorePoint.localScale.y / 2));

        return randomPoint;
    }

    private Transform SelectNextWaypoint(List<Transform> waypoints)
    {
        int index = waypoints.IndexOf(currentWaypoint);

        if (index == waypoints.Count - 1)
        {
            return waypoints[0];
        }
        else
        {
            return waypoints[index + 1];
        }
    }

    private Transform SelectRandomWaypoint(List<Transform> waypoints)
    {
        Transform newWaypoint = waypoints[Random.Range(0, waypoints.Count)];
        while (newWaypoint == currentWaypoint)
        {
            newWaypoint = waypoints[Random.Range(0, waypoints.Count)];
        }
        return newWaypoint;
    }

    public void Scroll(Vector2 start, Vector2 destination, float speed, float waitTime)
    {
        // Hey Kate you commented out thge code in creature controller too, remember to fix that :D you are beautiful and i love you so much <33333 even when coding gets hard you are great and i 
        // believe in you you're so hot i love you
        if (!idleRunning)
        {
            MoveCreature(destination, speed, ForceMode2D.Force);
            if (HasArrived(destination))
            {
                Idle(waitTime);
                transform.position = start;
            }
        }
    }

    public void Explore(Transform exploreLocation, float speed, float waitTime)
    {
        if (!idleRunning)
        {
            if (HasArrived(currentExplore))
            {
                currentExplore = SelectRandomPoint(exploreLocation);
                //adjustedExplore = new Vector2(currentExplore.x + exploreLocation.position.x, currentExplore.y + exploreLocation.position.y);
                currentExplore = new Vector2(currentExplore.x + exploreLocation.position.x, currentExplore.y + exploreLocation.position.y);
                Idle(waitTime);
            }
            else
            {
                MoveCreature(currentExplore, speed, ForceMode2D.Force);
            }
        }
    }

    public void Pulse(float pulseStrength, float pulsePause, float animPause)
    {
        if (!pulseRunning && !idleRunning)
        {
            pulseRunning = true;
            creatureAnimator.SetBool("pulse", true);
            Idle(animPause);
        }
        if (!idleRunning)
        {
            Vector2 pulsePoint = SelectRandomPoint(explorePoint);
            pulsePoint = new Vector2(pulsePoint.x + explorePoint.position.x, pulsePoint.y + explorePoint.position.y);

            MoveCreature(pulsePoint, pulseStrength, ForceMode2D.Impulse);

            creatureAnimator.SetBool("pulse", false);
            Idle(pulsePause);
            pulseRunning = false;
        }
    }

    public void Flee(Vector2 targetPosition, Vector2 returnPosition, float speedFast, float speedNormal, bool tooClose)
    {
        if (tooClose && !fleeRunning && !playerSO.GetIsHidden())
        {
            creatureAnimator.SetBool("tooClose", true);
            StartCoroutine(FleeCO(targetPosition, speedFast));
        }
        else if ((!tooClose || playerSO.GetIsHidden()) && !fleeRunning && !idleRunning)
        {
            creatureAnimator.SetBool("tooClose", false);
            MoveCreature(returnPosition, speedNormal, ForceMode2D.Force);
            if (HasArrived(returnPosition))
            {
                creatureController.SetBehaviorDefault();
            }
        }
   

    }

    IEnumerator FleeCO(Vector2 targetPosition, float speed)
    {
        fleeRunning = true;
        creatureAudioManager.PlayFleeSound();
        while (!HasArrived(targetPosition) && !playerSO.GetIsHidden())
        {
            soundRunning = true;
            MoveCreature(targetPosition, speed, ForceMode2D.Force);
            yield return new WaitForFixedUpdate();
        }
        soundRunning = false;
        fleeRunning = false;

        if (HasArrived(targetPosition))
        {
            Idle(2.0f);
        }
        yield break;
    }

    public void Hide(float endDistance, float currentDistance, float timeAfterHide)
    {
        if (currentDistance > endDistance)
        {
            Idle(timeAfterHide);
            creatureController.SetBehaviorDefault();
            firstHide = true;
        }
        else
        {
            if (firstHide)
            {
                creatureAnimator.SetBool("tooClose", true);
                creatureAnimator.SetBool("isMoving", false);
                creatureController.SetCanBeScanned(false);
                firstHide = false;

                creatureAudioManager.PlayHideSound();
            }
        }
    }

}
