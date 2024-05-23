using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Drill : MonoBehaviour
{
    [SerializeField] AudioSource audioS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Chemo")
        {
            if (GetComponent<Animator>() != null && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                Debug.Log("PLAYING:");
                Debug.Log(audioS);
                audioS.Play();
                GetComponent<Animator>().Play("DrillTime");
            }
        }
    }

    public void DestroyThisAfterSecs(float seconds)
    {
        StartCoroutine(DestroyAfterSecs(seconds));
    }

    public void MakeLoose()
    {
        Debug.Log("boom");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        if (GetComponent<AudioSource>() != null )
        {
            GetComponent<AudioSource>().Play();
        }
        StartCoroutine(DestroyAfterSecs(3));
    }

    IEnumerator DestroyAfterSecs(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
        yield break;
    }
}
