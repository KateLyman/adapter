using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptationController : MonoBehaviour
{
    [SerializeField] GameEventSO activeAdaptationEndEvent;

    private GameObject skin;
    private GameObject player;
    private PlayerSO playerSO;
    [SerializeField] List<GameObject> adaptations;

    [SerializeField] float camoDuration = 5.0f;
    [SerializeField] float noBonesDuration = 5.0f;
    [SerializeField] float lureDuration = 5.0f;
    [SerializeField] float sonarDuration = 5.0f;
    [SerializeField] float chemoDuration = 3.0f;

    [SerializeField] GameObject sonar;

    // Start is called before the first frame update
    void Start()
    {
        skin = GameObject.FindWithTag("Skin");
        player = GameObject.FindWithTag("Player");
        playerSO = player.GetComponent<PlayerMovement>().playerSO;

        UpdateAllAdaptations();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAllAdaptations()
    {
        foreach (GameObject adaptation in adaptations)
        {
            adaptation.GetComponent<Adaptation>().UpdateAdaptation();
        }
    }

    public void RunCurrentActiveAdaptation()
    {
        skin = GameObject.FindWithTag("Skin");

        if (playerSO.GetCurrentAdaptation() == PlayerSO.ADP.camo) // slider necessary
        {
            playerSO.SetIsHidden(true);
            skin.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

            StartCoroutine(AdaptationDurationCO(camoDuration));
        }
        else if (playerSO.GetCurrentAdaptation() == PlayerSO.ADP.flex) // slider necessary
        {
            playerSO.SetHasBones(false);
            skin.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 1.0f, 1.0f);

            StartCoroutine(AdaptationDurationCO(noBonesDuration));
        }
        else if (playerSO.GetCurrentAdaptation() == PlayerSO.ADP.lure)
        {
            playerSO.SetLureActive(true);
            player.transform.GetChild(2).gameObject.SetActive(true);

            StartCoroutine(AdaptationDurationCO(lureDuration));
        }
        else if (playerSO.GetCurrentAdaptation() == PlayerSO.ADP.sonar)
        {
            sonar.SetActive(true);
            playerSO.SetSonarActive(true);
            player.transform.GetChild(4).gameObject.SetActive(true);

            StartCoroutine(AdaptationDurationCO(sonarDuration));
        }
        else if (playerSO.GetCurrentAdaptation() == PlayerSO.ADP.chemo)
        {
            player.transform.GetChild(3).gameObject.SetActive(true);
            StartCoroutine(AdaptationDurationCO(chemoDuration));
        }
    }

    public void EndCurrentActiveAdaptation()
    {
        skin = GameObject.FindWithTag("Skin");

        StopAllCoroutines();

        if (playerSO.GetCurrentAdaptation() == PlayerSO.ADP.camo)
        {
            playerSO.SetIsHidden(false);
            skin.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else if (playerSO.GetCurrentAdaptation() == PlayerSO.ADP.flex)
        {
            playerSO.SetHasBones(true);
            skin.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else if (playerSO.GetCurrentAdaptation() == PlayerSO.ADP.lure)
        {
            playerSO.SetLureActive(false);
            player.transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (playerSO.GetCurrentAdaptation() == PlayerSO.ADP.sonar)
        {
            sonar.SetActive(false);
            playerSO.SetSonarActive(true);
            player.transform.GetChild(4).gameObject.SetActive(false);

        }
        else if (playerSO.GetCurrentAdaptation() == PlayerSO.ADP.chemo)
        {
            player.transform.GetChild(3).gameObject.SetActive(false);
        }
    }

    public IEnumerator AdaptationDurationCO(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        activeAdaptationEndEvent.Raise();
        yield break;
    }
}
