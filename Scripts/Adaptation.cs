using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Adaptation : MonoBehaviour
{
    [SerializeField] bool isPassive;
    public PlayerSO.ADP thisAdaptation;

    [SerializeField] Color deselectColor;
    [SerializeField] GameObject biolumLight;

    private PlayerSO playerSO;
    // Start is called before the first frame update
    void Start()
    {
        playerSO = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().playerSO;
        UpdateAdaptation();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isPassive)
        {
            if (playerSO.GetCurrentAdaptation() == thisAdaptation) {
                SelectAdaptation();
            }
            else
            {
                DeselectAdaptation();
            }
        }
        
    }

    public bool IsPassive()
    {
        return isPassive;
    }

    public void UpdateAdaptation()
    {
        if (playerSO.GetDiscoveredAdaptations().Contains(thisAdaptation))
        {
            GetComponent<Image>().enabled = true;
            if (isPassive)
            {
                ApplyPassive();
            }
        }
        else
        {
            GetComponent<Image>().enabled = false;
        }

        if (playerSO.GetActiveAdaptations().Count == 1 && !isPassive)
        {
            playerSO.SetActiveADP(playerSO.GetActiveAdaptations()[0]);
            transform.parent.gameObject.GetComponent<PowerWheel>().UpdateIndex();
        }
    }

    public void DeselectAdaptation()
    {
        GetComponent<Image>().color = Color.gray;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void SelectAdaptation()
    {
        GetComponent<Image>().color = Color.white;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ApplyPassive()
    {
        if (thisAdaptation == PlayerSO.ADP.fin)
        {
            playerSO.UpgradeSpeed();
        }

        if (thisAdaptation == PlayerSO.ADP.biolum && biolumLight != null)
        {
            biolumLight.SetActive(true);
        }
        else if (biolumLight == null)
        {
            Debug.Log("no biolum light assigned!");
        }
    }
}
