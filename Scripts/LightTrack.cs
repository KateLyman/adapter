using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrack : MonoBehaviour
{
    [SerializeField] float offset;
    private SpriteRenderer parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.flipX)
        {
            transform.localPosition = new Vector2 (offset, transform.localPosition.y);
        }
        else
        {
            transform.localPosition = new Vector2(offset*-1, transform.localPosition.y);
        }
    }
}
