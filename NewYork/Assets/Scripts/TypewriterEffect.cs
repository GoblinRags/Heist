using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public Text TextToChange;
    public string NewText = "";

    public float TimeBetweenChar = .05f;

    public bool IsTalking = false;

    public float Timer = 0;

    public AudioSource AS;

    public bool IntroDone = false;
    // Start is called before the first frame update
    void Awake()
    {
        //StartCoroutine(nameof(Talk));
        AS = GetComponent<AudioSource>();
        TextToChange = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Timer > 0 && IsTalking)
        {
            Timer -= Time.deltaTime;
            
        }
        if (TextToChange.text == NewText)
        {
            IsTalking = false;
            AS.mute = true;
        }

        if (Timer <= 0)
        {
            TextToChange.text = "";
            StartCoroutine(nameof(Talk));
            IsTalking = false;
            AS.mute = false;
            Timer = 1f; //stop continuous call of talk()
        }
    }

    IEnumerator Talk()
    {    
        foreach (char c in NewText)
        {
            TextToChange.text += c;
            yield return new WaitForSeconds(TimeBetweenChar);    
        }

        IntroDone = true;
    }

    public void PlayTalk(string newText, float delay)
    {
        
        NewText = newText;
        IsTalking = true;
        Timer = delay;
    }
}

