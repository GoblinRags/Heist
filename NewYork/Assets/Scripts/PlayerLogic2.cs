using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogic2 : MonoBehaviour
{
    
    public TypewriterEffect TypeWriter;
    public GameObject Panel;
    public Image PanelImage;
    public bool GameWon = false;
    public Text CanvasText;
    public Text BottomCanvasText;
   
    
    
    private string PhoneString = "You were able to break into his room, nice!\n\n" +
                                 "Now time to break into the vault!\n\n" +
                                "This should be the easiest heist ever!\n\n\n\n" +
                                "All you have to do is press space to go to space";
    private string KeyString = "Now get to the vault!";
    private string PickUpKeyString = "Press space to pick up";
    private string VaultString = "Press space to open the vault";
    private string DiamondString = "Press space to pick up"; 
    private string GotDiamondString = "Great, you got the diamonds, now get to the door!";
    private string OpenDoorString = "Press space to open";
    private string DoorString = "Oh no it's locked because you opened the vault!\n\n" +
                               "Get to the roof, I sent a teleporter via Amazon PrimeNow.\n\n" +
                               "Go go go, you don't have much time before he returns";

    private string TeleporterString = "Press space to teleport";


    public float TopTextTimer = 0f;
    public float ActualTextInterval = 3f;
    public float ResetTextInterval = 15f;
    public bool StartTextTimer = true;
    
    void Start()
    {
        TypeWriter = FindObjectOfType<TypewriterEffect>();
        //CanvasText.text = PhoneString;
        TypeWriter.PlayTalk(PhoneString, 0.5f);
        PanelImage = Panel.GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        if (StartTextTimer)
        {
            TopTextTimer += Time.deltaTime;
            if (TopTextTimer >= 10.2f)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    CanvasText.text = "";
                }
                //CanvasText.text = "";
                //ResetTextInterval = ActualTextInterval;
                //TopTextTimer = 0f;
            }
        }
        
        

        if (GameWon)
        {
            if (PanelImage.color.a >= 0f)
            {
                PanelImage.color = new Color(PanelImage.color.r, PanelImage.color.g, PanelImage.color.b,
                    PanelImage.color.a + .02f);
            }
        }
    }

    

    private void OnTriggerStay(Collider other)
    {
        float currentTimer = TopTextTimer;
        TopTextTimer = 0;
        if (other.gameObject.CompareTag("Airplane"))
        {
            BottomCanvasText.text = TeleporterString;
            //play success music
            AudioManager.Instance.MuteAllSounds();
            AudioManager.Instance.PlaySound("Teleport");
            StartCoroutine(PlaySoundWithDelay("Success", .7f));
            StartCoroutine(Teleport(.3f));
            Destroy(other.gameObject);
            BottomCanvasText.text = "";
            Debug.Log("Airplane, victory!"); 
            //airplane lift off sound
            //return;
            
        }
        TopTextTimer = currentTimer;

    }

    private void OnTriggerExit(Collider other)
    {
        //CanvasText.text = "";
        BottomCanvasText.text = "";
        StartTextTimer = true;
    }

    
    IEnumerator ShowTextAfterDelay(Text textObj, string text, float delay)
    {
        yield return new WaitForSeconds(delay);
        textObj.text = text;
    }

    IEnumerator PlaySoundWithDelay(string sfx, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySound(sfx);
    }

    IEnumerator Teleport(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        //AudioManager.Instance.MuteAllSounds();
        Panel.SetActive(true);
        GameWon = true;
    }



    public void GameDone()
    {
        BottomCanvasText.text = TeleporterString;
        //play success music
        AudioManager.Instance.MuteAllSounds();
        AudioManager.Instance.PlaySound("Teleport");
        StartCoroutine(PlaySoundWithDelay("Success", .7f));
        StartCoroutine(Teleport(.3f));
        //Destroy(other.gameObject);
        BottomCanvasText.text = "";
        Debug.Log("Airplane, victory!"); 
    }
    
}
