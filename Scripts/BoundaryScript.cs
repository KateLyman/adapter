using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryScript : MonoBehaviour
{
    private PlayerSO playerSO;
    private BoxCollider2D box;
    private SpriteRenderer spriteRenderer;

    [SerializeField] bool isAdaptationBoundary = true;
    [SerializeField] bool isTwilightBarrier = false;
    [SerializeField] PlayerSO.ADP adaptation;
    // Start is called before the first frame update
    void Start()
    {
        playerSO = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().playerSO;
        box = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAdaptationBoundary)
        {
            if (adaptation == PlayerSO.ADP.anemone || adaptation == PlayerSO.ADP.pressure || adaptation == PlayerSO.ADP.gills)
            {
                CheckCondition(playerSO.GetDiscoveredAdaptations().Contains(adaptation));
            }
            else if (adaptation == PlayerSO.ADP.flex)
            {
                CheckCondition(!playerSO.GetHasBones());
            }
        }
        else if (isTwilightBarrier)
        {
            CheckCondition(playerSO.GetDiscoveredAdaptations().Contains(PlayerSO.ADP.flex));
        }
    }

    public void CheckCondition(bool condition)
    {
        if (condition)
        {
            box.isTrigger = true;

            /*
            if (adaptation == PlayerSO.ADP.flex)
            {
                spriteRenderer.color = new Color(1.0f, 0.0f, 1.0f, 0.5f);
            }
            */
        }
        else
        {
            box.isTrigger = false;

            /*
            if (adaptation == PlayerSO.ADP.flex)
            {
                spriteRenderer.color = new Color(1.0f, 0.0f, 1.0f, 1.0f);
            }
            */
        }
    }
}
