using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    [SerializeField] PlayerSO playerSO;
    [SerializeField] int firstIndex;
    // Start is called before the first frame update
    void Start()
    {
        UpdateSkin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSkin()
    {
        for (int i = firstIndex; i < firstIndex + playerSO.GetSkinCount(); i += 1)
        {
            Debug.Log(transform.GetChild(i).gameObject);
            if (i - firstIndex == playerSO.GetSkin())
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
