using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaVisuals : MonoBehaviour
{
    private findCreatures3 creatureFinder;
    [SerializeField] private Color fishSelectedColor;

    // Start is called before the first frame update
    void Start()
    {
        creatureFinder = GetComponent<findCreatures3>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (creatureFinder.thisCreature != null)
        {
            ThisFishVisualStart();
        }

    }

    public void ThisFishVisualStart()
    {
        if (creatureFinder.thisCreature != null)
        {
            creatureFinder.thisCreature.Fish.GetComponent<SpriteRenderer>().color = fishSelectedColor;
        }
    }

    public void ThisFishVisualStop()
    {
        if (creatureFinder.thisCreature != null )
        {
            creatureFinder.thisCreature.Fish.GetComponent<SpriteRenderer>().color = creatureFinder.thisCreature.Fish.GetComponent<CreatureController>().originalColor;
        }
    }

    private void OnDisable()
    {
        ThisFishVisualStop();
    }
}
