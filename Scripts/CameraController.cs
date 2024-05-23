using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera myCamera;
    
    [SerializeField] SpriteRenderer player;
    [SerializeField] float xOffset;
    [SerializeField] float zoomTime;
    [SerializeField] float zoomIncrement;
    [SerializeField] float xIncrement;
    private bool zoomRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.flipX)
        {
            myCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.5f - xOffset;
        }
        else
        {
            myCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.5f + xOffset;
        }

    }

    public void SetXOffset(float newOffset)
    {
        xOffset = newOffset;
    }

    public void ZoomOutCamera(float zoomComplete)
    {
        StopAllCoroutines();
        StartCoroutine(ZoomInCO(zoomComplete));
    }

    public void ZoomInCamera(float zoomComplete)
    {
        StopAllCoroutines();
        StartCoroutine(ZoomOutCO(zoomComplete));
    }

    private IEnumerator ZoomInCO(float zoomComplete)
    {
        zoomRunning = true;
        while (myCamera.m_Lens.OrthographicSize < zoomComplete)
        {
            myCamera.m_Lens.OrthographicSize += zoomIncrement;

            yield return new WaitForSeconds(zoomTime);
        }
        zoomRunning = false;
        yield break;
    }

    private IEnumerator ZoomOutCO(float zoomComplete)
    {
        zoomRunning = true;
        while (myCamera.m_Lens.OrthographicSize > zoomComplete)
        {
            myCamera.m_Lens.OrthographicSize -= zoomIncrement;
            yield return new WaitForSeconds(zoomTime);
        }
        zoomRunning = false;
        yield break;
    }
}