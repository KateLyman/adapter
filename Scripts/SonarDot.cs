using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarDot : MonoBehaviour
{
    [SerializeField] Transform creature;
    private Vector2 creatureLocation;
    private Transform player;
    private Vector2 playerLocation;

    private RectTransform thisTransform;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerLocation = player.position;

        thisTransform = GetComponent<RectTransform>();

        if (creature != null )
        {
            creatureLocation = creature.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerLocation != null && creatureLocation != null)
        {
            playerLocation = player.position;
            creatureLocation = creature.position;

            Vector2 direction = creatureLocation - playerLocation;

            thisTransform.localPosition = direction.normalized * 50.0f;
        }
    }

    public void SetCreature(Transform newCreature)
    {
        creature = newCreature;
        creatureLocation = creature.position;
    }
}
