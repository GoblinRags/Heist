using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowObjectDelay : MonoBehaviour
{
    public Text TextScript;
    public float Delay = 5f;

    public bool On = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowObject(Delay));
    }

    // Update is called once per frame
    void Update()
    {
        if (On)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator ShowObject(float delay)
    {
        yield return new WaitForSeconds(delay);
        EnableObj();
        On = true;
    }

    void EnableObj()
    {
        if (TextScript != null)
        {
            TextScript.enabled = true;
        }
        
    }
}
