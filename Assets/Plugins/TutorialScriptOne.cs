using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class TutorDataClass
{
    public bool TutorOn = false;
    public MenuScript MenuMainScript;
    public GameObject TutorMessagePanel;

    public List<string> TextsMessages = new List<string>();
    public Text TextMessage;

    public List<UnityEngine.UI.Button> ButtonsInMeinMenu = new List<UnityEngine.UI.Button>();

    public Image PreparationButtonImage;
    public Image CharacterButtonImage;
    public Image BuyCharacterButton;
    public Image SelectCharacterButton;
    public Image WeaponButtonImage;
    public Image WeaponSelectImage;
    public Image WeaponBuyButtonImage;
    public Image WeaponSelectButtonImage;
    public Image MapButtonImage;
    public Image MapBuyButtonImage;
    public Image MapSelectButtonImage;
    public Image StartButtonImage;
}

public class TutorialScriptOne : MonoBehaviour
{
    public TutorDataClass TutorialData;

    void Start()
    {
        if(PlayerPrefs.GetInt("tutor") == 2)
        {
            TutorialData.TutorOn = true;
            Step(11);
        }
    }

    public void DisclaimerTutorial()
    {
        TutorialData.MenuMainScript.MainMenu.MainMenuPanel.SetActive(true);
        TutorialData.MenuMainScript.MainMenu.TutorialOfferPanel.SetActive(false);
        PlayerPrefs.SetInt("weapon" + 0, 1);
        PlayerPrefs.SetInt("selwep", 0);
        PlayerPrefs.SetInt("char" + 0, 1);
        PlayerPrefs.SetInt("selchar", 0);
        PlayerPrefs.SetInt("map" + 0, 1);
        PlayerPrefs.SetInt("selmap", 0);
        TutorialData.MenuMainScript.LoadDataMap();
        TutorialData.MenuMainScript.LoadDataCharacter();
        TutorialData.MenuMainScript.LoadDataWeapon();
    }

    public void StartTutor ()
    {
        TutorialData.MenuMainScript.MainMenu.MainMenuPanel.SetActive(true);
        TutorialData.MenuMainScript.MainMenu.TutorialOfferPanel.SetActive(false);
        PlayerPrefs.SetInt("tutor", 1);
        TutorialData.MenuMainScript.MoneyPlayer = 20000;
        TutorialData.TutorOn = true;
        Time.timeScale = 0;
        TutorialData.TextMessage.text = TutorialData.TextsMessages[0];
        TutorialData.TutorMessagePanel.SetActive(true);
        TutorialData.ButtonsInMeinMenu[0].interactable = false;
        TutorialData.ButtonsInMeinMenu[1].interactable = false;
        StartCoroutine(WinkButton(TutorialData.PreparationButtonImage));

    }

    public void CloseMessage()
    {
        TutorialData.TutorMessagePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void WinkOff(Image img)
    {
        Wink = false;
        if (TutorialData.TutorOn)
        {
            tempCol = img.color;
            tempCol.a = oldAlpha;
            img.color = tempCol;
        }
    }

    public void Step(int num)
    {
        if(TutorialData.TutorOn)
        {
            switch(num)
            {
                case 0:
                    TutorialData.TextMessage.text = TutorialData.TextsMessages[1];
                    TutorialData.TutorMessagePanel.SetActive(true);
                    Time.timeScale = 0;
                    TutorialData.ButtonsInMeinMenu[3].interactable = false;
                    TutorialData.ButtonsInMeinMenu[4].interactable = false;
                    TutorialData.ButtonsInMeinMenu[5].interactable = false;
                    TutorialData.ButtonsInMeinMenu[6].interactable = false;
                    tempImg = TutorialData.CharacterButtonImage;
                    Invoke("InvokeCorutine", 0.1f);
                    break;
                case 1:
                    TutorialData.ButtonsInMeinMenu[7].interactable = false;
                    TutorialData.ButtonsInMeinMenu[8].interactable = false;
                    TutorialData.ButtonsInMeinMenu[9].interactable = false;
                    TutorialData.ButtonsInMeinMenu[2].interactable = false;
                    tempImg = TutorialData.BuyCharacterButton;
                    Invoke("InvokeCorutine", 0.1f);
                    break;
                case 2:
                    tempImg = TutorialData.SelectCharacterButton;
                    Invoke("InvokeCorutine", 0.1f);
                    break;
                case 3:
                    TutorialData.TextMessage.text = TutorialData.TextsMessages[2];
                    TutorialData.TutorMessagePanel.SetActive(true);
                    Time.timeScale = 0;
                    TutorialData.ButtonsInMeinMenu[3].interactable = true;
                    TutorialData.ButtonsInMeinMenu[2].interactable = false;
                    tempImg = TutorialData.WeaponButtonImage;
                    Invoke("InvokeCorutine", 0.1f);
                    break;
                case 4:
                    TutorialData.ButtonsInMeinMenu[3].interactable = false;
                    for(int i=10; i < 21; i++)
                    {
                        TutorialData.ButtonsInMeinMenu[i].interactable = false;
                    }
                    tempImg = TutorialData.WeaponSelectImage;
                    Invoke("InvokeCorutine", 0.1f);
                    break;
                case 5:
                    TutorialData.ButtonsInMeinMenu[22].interactable = false;
                    TutorialData.ButtonsInMeinMenu[21].interactable = false;
                    tempImg = TutorialData.WeaponBuyButtonImage;
                    Invoke("InvokeCorutine", 0.1f);
                    break;
                case 6:
                    tempImg = TutorialData.WeaponSelectButtonImage;
                    Invoke("InvokeCorutine", 0.1f);
                    break;
                case 7:
                    TutorialData.TextMessage.text = TutorialData.TextsMessages[3];
                    TutorialData.TutorMessagePanel.SetActive(true);
                    Time.timeScale = 0;
                    TutorialData.ButtonsInMeinMenu[21].interactable = true;
                    TutorialData.ButtonsInMeinMenu[4].interactable = true;
                    tempImg = TutorialData.MapButtonImage;
                    Invoke("InvokeCorutine", 0.1f);
                    break;
                case 8:
                    TutorialData.ButtonsInMeinMenu[4].interactable = false;
                    TutorialData.ButtonsInMeinMenu[23].interactable = false;
                    TutorialData.ButtonsInMeinMenu[24].interactable = false;
                    TutorialData.ButtonsInMeinMenu[25].interactable = false;
                    tempImg = TutorialData.MapBuyButtonImage;
                    Invoke("InvokeCorutine", 0.1f);
                    break;
                case 9:
                    tempImg = TutorialData.MapSelectButtonImage;
                    Invoke("InvokeCorutine", 0.1f);
                    break;
                case 10:
                    TutorialData.ButtonsInMeinMenu[5].interactable = true;
                    TutorialData.TextMessage.text = TutorialData.TextsMessages[4];
                    TutorialData.TutorMessagePanel.SetActive(true);
                    Time.timeScale = 0;
                    tempImg = TutorialData.StartButtonImage;
                    Invoke("InvokeCorutine", 0.1f);
                    break;
                case 11:
                    TutorialData.TextMessage.text = TutorialData.TextsMessages[5];
                    TutorialData.TutorMessagePanel.SetActive(true);
                    Time.timeScale = 0;
                    PlayerPrefs.SetInt("tutor", 0);
                    TutorialData.TutorOn = false;
                    break;
            }
        }
    }

    private Image tempImg;
    private void InvokeCorutine()
    {
        StartCoroutine(WinkButton(tempImg));
    }
    
    private Color tempCol;
    private float oldAlpha = 0;
    public bool Wink = false;
    private bool UpDown = false;

    IEnumerator WinkButton(Image img)
    {
        tempCol = img.color;
        oldAlpha = img.color.a;
        UpDown = false;
        Wink = true;
        while (Wink)
        {
            if(tempCol.a >= oldAlpha)
            {
                UpDown = false;
            }
            if(tempCol.a <= 0.1f)
            {
                UpDown = true;
            }
            if(UpDown)
            {
                tempCol.a += 0.05f;
            }
            else
            {
                tempCol.a -= 0.05f;
            }
            img.color = tempCol;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
