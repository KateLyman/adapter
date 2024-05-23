using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScannerVisuals : MonoBehaviour
{
    private Slider slider;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        animator = GetComponent<Animator>();
        slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartScanProgress()
    {
        slider.value = 0.0f;
        animator.SetBool("exit", false);
        animator.SetBool("success", false);

        Debug.Log("Start scan visualizer");
        StopAllCoroutines();
        StartCoroutine(Scan());
    }

    public void StopScanProgress(bool wasSuccessful)
    {
        if (wasSuccessful)
        {
            animator.SetBool("success", true);
        }
        animator.SetBool("exit", true);
        Debug.Log("End scan visualizer");
        StopAllCoroutines();
    }

    IEnumerator Scan()
    {
        int i = 1;
        slider.value = 0.0f;
        while (slider.value < 1.0f)
        {
            Debug.Log(i.ToString() + ": " + slider.value.ToString());
            i += 1;
            slider.value += (1.0f / 65.0f);
            yield return new WaitForSeconds(.025f);
        }
        yield break;
    }

    public void SetScannerVisualSuccess(bool success)
    {
        animator.SetBool("success", success);
    }
}
