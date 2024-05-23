using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureParticles : MonoBehaviour
{
    private CreatureSO parentCreatureSO;
    // Start is called before the first frame update
    void Start()
    {
        parentCreatureSO = transform.parent.gameObject.GetComponent<CreatureController>().creature;
    }

    // Update is called once per frame
    void Update()
    {
        if (!parentCreatureSO.IsScannable())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
