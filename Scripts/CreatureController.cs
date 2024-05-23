using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureController : MonoBehaviour
{
    [SerializeField] GameObject sonarDot;
    private GameObject thisDot;
    [SerializeField] GameObject sonarParent;

    private Transform player;
    private PlayerSO playerSO;

    [SerializeField] public CreatureSO creature;
    [SerializeField] CreatureBehaviors.BHV currentBehavior;
    private CreatureBehaviors.BHV defaultBehavior;

    [SerializeField] float animPause = 0.5f;

    [SerializeField] public List<Transform> waypoints;
    [SerializeField] public Transform explorePoint;

    [SerializeField] Vector2 scrollStart;
    [SerializeField] Vector2 scrollEnd;

    [SerializeField] Transform fleePoint;
    [SerializeField] Transform originalPoint;

    private CreatureBehaviors behaviorScript;

    public float playerDistance;
    public float playerDirection;

    private float currentSpeed;

    public bool canBeScanned = true;

    public Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        behaviorScript = GetComponent<CreatureBehaviors>();
        player = GameObject.FindWithTag("Player").transform;
        defaultBehavior = currentBehavior;
        playerSO = player.gameObject.GetComponent<PlayerMovement>().playerSO;

        currentSpeed = creature.GetSpeedNormal();

        originalColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RunBehavior(currentBehavior);

        // Update player distance
        playerDistance = Vector2.Distance(player.position, transform.position);

        // Update direction from player -- CHANGE SO IT GETS DIRECTION FROM JOYSTICK
        Vector2 dir = transform.position - player.position; // Get Vector2 direction of fish from player
        float theta = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // Angle got (in degrees). Thank you sohcahtoa
        if (theta < 0.0f) {theta += 360.0f;} // No negative angles :)
        playerDirection = theta;

        // Flee check
        if (Vector3.Distance(player.position, transform.position) < creature.GetFleeStartDistance() && !playerSO.GetIsHidden())
        {
            SetBehavior(CreatureBehaviors.BHV.flee);
        }

        // Hide check
        if (Vector3.Distance(player.position, transform.position) < creature.GetHideStartDistance() && !playerSO.GetIsHidden())
        {
            SetBehavior(CreatureBehaviors.BHV.hide);
            SetCanBeScanned(false);
        }

        // Sonar dot update
        if (thisDot != null)
        {
            UpdateSonarDotColor();
        }
    }

    public bool GetCanBeScanned()
    {
        return canBeScanned;
    }

    public void SetCanBeScanned(bool scanned)
    {
        canBeScanned = scanned;
    }

    public void SetBehavior(CreatureBehaviors.BHV behavior)
    {
        currentBehavior = behavior;
    }

    public void RunBehavior(CreatureBehaviors.BHV behavior)
    {
        if (currentBehavior == CreatureBehaviors.BHV.idle)
        {
            behaviorScript.Idle(creature.GetIdleTime());
        }
        else if (currentBehavior == CreatureBehaviors.BHV.patrol)
        {
            behaviorScript.Patrol(waypoints, currentSpeed, creature.GetIdleTime());
        }
        else if (currentBehavior == CreatureBehaviors.BHV.scroll)
        {
            behaviorScript.Scroll(scrollStart, scrollEnd, currentSpeed, creature.GetIdleTime());
        }
        else if (currentBehavior == CreatureBehaviors.BHV.explore)
        {
            behaviorScript.Explore(explorePoint, currentSpeed, creature.GetIdleTime());
        }
        else if (currentBehavior == CreatureBehaviors.BHV.pulse)
        {
            behaviorScript.Pulse(creature.GetPulseStrength(), creature.GetPulseTime(), animPause);
        }
        else if (currentBehavior == CreatureBehaviors.BHV.flee)
        {
            if (fleePoint != null && originalPoint != null)
            {
                behaviorScript.Flee(fleePoint.position, originalPoint.position, creature.GetSpeedFast(), creature.GetSpeedNormal(), Vector3.Distance(player.position, transform.position) < creature.GetFleeEndDistance());
            }
        }
        else if (currentBehavior == CreatureBehaviors.BHV.hide)
        {
            behaviorScript.Hide(creature.GetHideEndDistance(), Vector3.Distance(player.position, transform.position), creature.GetIdleTime());
        }
        else if (currentBehavior == CreatureBehaviors.BHV.lured)
        {
            behaviorScript.Explore(player.transform.GetChild(2).transform.GetChild(1), creature.GetSpeedFast(), 0);
        }
    }

    public void SetBehaviorDefault()
    {
        currentBehavior = defaultBehavior;
    }

    public void SetBehaviorIdle()
    {
        currentBehavior = CreatureBehaviors.BHV.idle;
    }

    public void SetBehaviorPatrol()
    {
        currentBehavior = CreatureBehaviors.BHV.patrol;
    }

    public void SetBehaviorScroll()
    {
        currentBehavior = CreatureBehaviors.BHV.scroll;
    }

    public void SetBehaviorExplore()
    {
        currentBehavior = CreatureBehaviors.BHV.explore;
    }

    public void SetBehaviorPulse()
    {
        currentBehavior = CreatureBehaviors.BHV.pulse;
    }

    public void SetBehaviorFlee()
    {
        currentBehavior = CreatureBehaviors.BHV.flee;
    }

    public void SetBehaviorHide()
    {
        currentBehavior = CreatureBehaviors.BHV.hide;
    }

    public void InstantiateSonarDot()
    {
        if (sonarParent != null)
        {
            thisDot = Instantiate(sonarDot, sonarParent.transform);
            thisDot.GetComponent<SonarDot>().SetCreature(transform);

            UpdateSonarDotColor();
        }
        else
        {
            Debug.Log("no sonarParent");
        }
    }

    public void UpdateSonarDotColor()
    {
        if (!creature.IsScannable())
        {
            thisDot.GetComponent<Image>().color = new Color(.5f, .5f, .5f, thisDot.GetComponent<Image>().color.a);
        }
        else if (creature.GetAdaptation() != PlayerSO.ADP.none)
        {
            thisDot.GetComponent<Image>().color = new Color(233f / 255f, 181 / 255f, 63f / 255f, thisDot.GetComponent<Image>().color.a);
        }
    }

    public void DestroySonarDot()
    {
        if (sonarParent != null)
        {
            Destroy(thisDot);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Scanner" && !playerSO.GetIsHidden())
        {
            currentSpeed = creature.GetSpeedFast();
        }
        if (collision.tag == "Lure")
        {
            currentBehavior = CreatureBehaviors.BHV.lured;
        }
        if (collision.tag == "Sonar")
        {
            InstantiateSonarDot();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Scanner")
        {
            currentSpeed = creature.GetSpeedNormal();
        }
        if (collision.tag == "Lure")
        {
            currentBehavior = defaultBehavior;
        }
        if (collision.tag == "Sonar")
        {
            DestroySonarDot();
        }
    }
}
