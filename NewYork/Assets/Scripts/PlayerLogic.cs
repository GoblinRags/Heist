using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    
    public bool KeyPickedUp = false;

    public bool VaultOpened = false;
    public bool DiamondTaken = false;
    public bool DiamondOpen = false;
    public bool DoorOpened = false;
    public GameObject CashToDestroy;
    public GameObject VaultCenter;
    public GameObject RedLights;
    public GameObject Teleporter;
    public TypewriterEffect TypeWriter;
    public GameObject Panel;
    public Image PanelImage;
    public bool GameWon = false;
    public bool AirplaneTaken = false;
    public Text CanvasText;
    public Text BottomCanvasText;
    public float TotalRotation = 30f;
    public float RotationSpeed = .2f;
    public float RotationAmount = 0f;


    public float RedTimer = 0f;
    public float RedTimerInterval = .2f;
    
    
    private string PhoneString = "You were able to break into his room, nice!\n\n" +
                                 "Now time to break into the vault!\n\n" +
                                "look for the key, it should be at the top of the floating bills";
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

    public bool DoorJustOpened = false;
    void Start()
    {
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
            if (TopTextTimer >= ResetTextInterval)
            {
                CanvasText.text = "";
                ResetTextInterval = ActualTextInterval;
                TopTextTimer = 0f;
            }
        }
        if (DoorOpened)
        {
            OpenVault();
        }
        
        if (DiamondTaken)
        {
            RedTimer += Time.deltaTime;
            if (RedTimer > RedTimerInterval)
            {
                RedLights.SetActive(!RedLights.activeSelf);
                RedTimer = 0f;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            DoorJustOpened = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        float currentTimer = TopTextTimer;
        TopTextTimer = 0;
        //CanvasText.text = "";
        if (other.gameObject.CompareTag("VaultKey"))
        {
            BottomCanvasText.text = PickUpKeyString;
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //CanvasText.text = KeyString;
                TypeWriter.PlayTalk(KeyString, 0.05f);
                BottomCanvasText.text = "";
                Destroy(other.gameObject);
                KeyPickedUp = true;
                //play key picked up
                AudioManager.Instance.PlaySound("KeyPickup");
            }

            return;
        }
        
        if (other.gameObject.CompareTag("Vault"))
        {
            if (KeyPickedUp)
            {
                if (!DoorOpened)
                {
                    BottomCanvasText.text = VaultString;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        AudioManager.Instance.PlaySound("CreakingDoor");
                        Destroy(other.gameObject);
                        DoorOpened = true;
                        BottomCanvasText.text = "";
                        //play key picked up
                    }
                }
                //CanvasText.text = VaultString;
                else
                {
                }
            }
            else
            {
                BottomCanvasText.text = "Key needed which is floating on some cash";
            }

            return;
            
        }
        if (other.gameObject.CompareTag("Diamond"))
        {
            if (DoorOpened && DiamondOpen)
            {
                BottomCanvasText.text = DiamondString;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Destroy(other.gameObject);
                    AudioManager.Instance.PlaySound("Diamond");
                    AudioManager.Instance.PlaySound("Alarm");
                    BottomCanvasText.text = "";
                    CanvasText.text = GotDiamondString;
                    DiamondTaken = true;
                    //play diamond sound
                    //red alert sound
                    //Destroy diamonds
                    Destroy(CashToDestroy);
                }
            }
            Debug.Log("Diamond");
            return;
        }
        
        if (other.gameObject.CompareTag("Door"))
        {
            if (!DoorJustOpened)
            {
                BottomCanvasText.text = OpenDoorString;
            }
            else
            {
                BottomCanvasText.text = "";
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //show text, oh no can't go
                if (DiamondTaken)
                {
                    //play open door attempt sfx, then show text
                    AudioManager.Instance.PlaySound("Door");
                    
                    StartCoroutine(ShowTextAfterDelay(CanvasText, DoorString, .25f));
                    
                    //CanvasText.text = DoorString;
                    Teleporter.SetActive(true);
                    //have timer start

                    DoorJustOpened = true;
                }
                else if (!KeyPickedUp)
                {
                    CanvasText.text = "Why are you leaving?! You have a mission!\n\n" +
                                      "Get the key at the top of the floating cash\n";
                    DoorJustOpened = true;
                }
                else if (!DiamondTaken)
                {
                    CanvasText.text = "You have a mission to finish!\n\n" +
                                      "Grab the diamond in the vault before leaving!";
                    DoorJustOpened = true;
                }
                BottomCanvasText.text = "";
            }

            return;



        }
        if (other.gameObject.CompareTag("Airplane"))
        {
            BottomCanvasText.text = TeleporterString;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //play success music
                AudioManager.Instance.MuteAllSounds();
                AudioManager.Instance.PlaySound("Teleport");
                StartCoroutine(PlaySoundWithDelay("Success", .7f));
                StartCoroutine(Teleport(.3f));
                Destroy(other.gameObject);
                BottomCanvasText.text = "";
                Debug.Log("Airplane, victory!"); 
                //airplane lift off sound
            }

            return;
        }

        TopTextTimer = currentTimer;

    }

    private void OnTriggerExit(Collider other)
    {
        //CanvasText.text = "";
        DoorJustOpened = false;
        BottomCanvasText.text = "";
        StartTextTimer = true;
    }

    private void OpenVault()
    {
        if (RotationAmount < TotalRotation)
        {
            VaultCenter.transform.Rotate(0f, RotationSpeed, 0f);
            //VaultCenter.transform.eulerAngles += new Vector3(0f, 1f, 0f);
            RotationAmount += RotationSpeed;
            if (RotationAmount > TotalRotation / 4f)
            {
                DiamondOpen = true;
            }
        }


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
    
    
    
}
