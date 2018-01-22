using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using CnControls;

[System.Serializable]
public class TutorialDataClassTwo
{
    public MainSpawner MSpawnerScript;

    public Man PlayerManScript;
    public Man EnemyManScript;

    public GameObject TutorialMessagePanel;
    public Text TutorialMessageText;
    public List<string> MessagesTexts = new List<string>();

    public CameraScript CamScr;
    public MainInput MInput;
    public Player PlayerScript;

    public List<UnityEngine.UI.Button> ButtonsUI = new List<UnityEngine.UI.Button>();
    public Image ImageButtonFire;
    public Image ImageButtonAim;
    public SimpleButton ButtonFire;
    public SimpleButton ButtonAim;

    public Transform SpawnEnemyPoint;
    public EnemiesClass EnemyOne;

    public EnemiesClass EnemyTwo;

    public RectTransform StickTutor;
    public RectTransform TuchTutor;

    public Transform PointSpawnFirstAidKit;
    public Transform PointSpawnArmor;
    public Transform PointSpawnBullets;
    public GameObject PrefabFirstAidKit;
    public GameObject PrefabArmor;
    public GameObject PrefabBullets;
}

public class TutotialScriptTwo : MonoBehaviour
{
    public TutorialDataClassTwo TutorialData;

    private bool EnemyOneAttak = false;
    private bool AimState = false;
    private bool EnemyCheckHealth = false;

    void Start ()
    {
        TutorialData.MSpawnerScript.enabled = false;
        Step(0);
    }

    void Update()
    {
        if(EnemyOneAttak)
        {
            if (TutorialData.PlayerScript.man_script.health <= TutorialData.PlayerScript.man_script.max_health / 2)
            {
                TutorialData.EnemyManScript.GetComponent<Band>().enabled = false;
                EnemyOneAttak = false;
            }
        }
        if(AimState)
        {
            if (TutorialData.ButtonAim.ActiveButton)
            {
                WinkOff(TutorialData.ImageButtonAim);
                tempImageForCorutine = TutorialData.ImageButtonFire;
                Invoke("InvokeCorutineWink", 0.1f);
                AimState = false;
                EnemyCheckHealth = true;
            }
        }
        if(EnemyCheckHealth)
        {
            if(TutorialData.EnemyManScript.health <= 0)
            {
                Step(3);
                EnemyCheckHealth = false;
            }
        }
    }

    public void Step(int num)
    {
        switch(num)
        {
            case 0:
                TutorialData.ButtonFire.enabled = false;
                TutorialData.ButtonAim.enabled = false;
                TutorialData.ImageButtonFire.enabled = false;
                TutorialData.ImageButtonAim.enabled = false;
                TutorialData.ButtonsUI[0].interactable = false;
                TutorialData.ButtonsUI[1].interactable = false;
                TutorialData.ButtonsUI[2].interactable = false;
                TutorialData.TutorialMessageText.text = TutorialData.MessagesTexts[0];
                TutorialData.TutorialMessagePanel.SetActive(true);
                Time.timeScale = 0;
                TutorialData.StickTutor.gameObject.SetActive(true);
                StartCoroutine(StickMoveTutor());
                break;
            case 1:
                MoveStick = false;
                TutorialData.StickTutor.gameObject.SetActive(false);
                TutorialData.TuchTutor.gameObject.SetActive(true);
                TutorialData.CamScr.Tutor = true;
                StartCoroutine(TuchMoveTutor());
                break;
            case 2:
                MoveTuch = false;
                TutorialData.CamScr.Tutor = false;
                TutorialData.TuchTutor.gameObject.SetActive(false);
                SpawnEnemy();
                TutorialData.ButtonFire.enabled = true;
                TutorialData.ButtonAim.enabled = true;
                TutorialData.ImageButtonFire.enabled = true;
                TutorialData.ImageButtonAim.enabled = true;
                TutorialData.TutorialMessageText.text = TutorialData.MessagesTexts[1];
                TutorialData.TutorialMessagePanel.SetActive(true);
                Time.timeScale = 0;
                StartCoroutine(WinkImage(TutorialData.ImageButtonAim));
                AimState = true;
                break;
            case 3:
                WinkOff(TutorialData.ImageButtonFire);
                TutorialData.ButtonsUI[2].interactable = true;
                TutorialData.ButtonFire.enabled = false;
                TutorialData.ButtonAim.enabled = false;
                TutorialData.ImageButtonFire.enabled = false;
                TutorialData.ImageButtonAim.enabled = false;
                Color temp = new Color();
                temp.r = 0.19f;
                temp.g = 0.13f;
                temp.b = 0.44f;
                temp.a = 1;
                TutorialData.ButtonsUI[2].transform.GetChild(0).GetComponent<Image>().color = temp;
                tempImageForCorutine = TutorialData.ButtonsUI[2].GetComponent<Image>();
                Invoke("InvokeCorutineWink", 0.1f);
                break;
            case 4:
                TutorialData.ButtonFire.enabled = true;
                TutorialData.ButtonAim.enabled = true;
                TutorialData.ImageButtonFire.enabled = true;
                TutorialData.ImageButtonAim.enabled = true;
                TutorialData.ButtonsUI[2].transform.GetChild(0).GetComponent<Image>().color = Color.black;
                Wink = false;
                GameObject tempObj = Instantiate(TutorialData.PrefabFirstAidKit, TutorialData.PointSpawnFirstAidKit.position, Quaternion.identity) as GameObject;
                TriggerTutorial trTut = tempObj.AddComponent<TriggerTutorial>();
                trTut.TutorialScript = this;
                trTut.NumStep = 5;
                TutorialData.TutorialMessageText.text = TutorialData.MessagesTexts[2];
                TutorialData.TutorialMessagePanel.SetActive(true);
                Time.timeScale = 0;
                break;
            case 5:
                GameObject tempObj2 = Instantiate(TutorialData.PrefabArmor, TutorialData.PointSpawnArmor.position, Quaternion.identity) as GameObject;
                TriggerTutorial trTut2 = tempObj2.AddComponent<TriggerTutorial>();
                trTut2.TutorialScript = this;
                trTut2.NumStep = 6;
                break;
            case 6:
                GameObject tempObj3 = Instantiate(TutorialData.PrefabBullets, TutorialData.PointSpawnBullets.position, Quaternion.identity) as GameObject;
                TriggerTutorial trTut3 = tempObj3.AddComponent<TriggerTutorial>();
                trTut3.TutorialScript = this;
                trTut3.NumStep = 7;
                break;
            case 7:
                PlayerPrefs.SetInt("tutor", 2);
                SpawnHardEnemy();
                TutorialData.TutorialMessageText.text = TutorialData.MessagesTexts[3];
                TutorialData.TutorialMessagePanel.SetActive(true);
                Time.timeScale = 0;
                TutorialData.ButtonsUI[0].interactable = true;
                TutorialData.ButtonsUI[1].interactable = true;
                TutorialData.ButtonsUI[2].interactable = true;
                TutorialData.ButtonsUI[3].interactable = false;
                StartCoroutine(WinkImage(TutorialData.ButtonsUI[4].GetComponent<Image>()));
                break;
        }
    }

    public void CloseMessage()
    {
        Time.timeScale = 1;
        TutorialData.TutorialMessagePanel.SetActive(false);
    }

    private bool MoveStick = false;
    IEnumerator StickMoveTutor()
    {
        float yPos = TutorialData.StickTutor.anchoredPosition.y;
        MoveStick = true;
        bool UpDown = false;
        while(MoveStick)
        {
            if(TutorialData.StickTutor.anchoredPosition.y <= yPos)
            {
                UpDown = true;
            }
            else if(TutorialData.StickTutor.anchoredPosition.y >= yPos + 50)
            {
                UpDown = false;
            }
            if(UpDown)
            {
                TutorialData.StickTutor.anchoredPosition += new Vector2(0, 3);
            }
            else
            {
                TutorialData.StickTutor.anchoredPosition -= new Vector2(0, 3);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    private bool MoveTuch = false;
    IEnumerator TuchMoveTutor()
    {
        float xPos = TutorialData.TuchTutor.anchoredPosition.x;
        MoveTuch = true;
        bool UpDown = false;
        while (MoveTuch)
        {
            if (TutorialData.TuchTutor.anchoredPosition.x <= xPos - 300)
            {
                UpDown = true;
            }
            else if (TutorialData.TuchTutor.anchoredPosition.x >= xPos + 300)
            {
                UpDown = false;
            }
            if (UpDown)
            {
                TutorialData.TuchTutor.anchoredPosition += new Vector2(300, 0)*Time.deltaTime;
            }
            else
            {
                TutorialData.TuchTutor.anchoredPosition -= new Vector2(300, 0) * Time.deltaTime;
            }
            if(TutorialData.CamScr.TutorMove > 50)
            {
                Step(2);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void WinkOff(Image img)
    {
        Wink = false;
        Color temp = img.color;
        temp.a = colAlpha;
        img.color = temp;
    }

    private Image tempImageForCorutine;
    public void InvokeCorutineWink()
    {
        StartCoroutine(WinkImage(tempImageForCorutine));
    }

    private bool Wink = false;
    private float colAlpha;
    IEnumerator WinkImage(Image img)
    {
        Color tempCol = img.color;
        colAlpha = tempCol.a;
        Wink = true;
        bool UpDown = false;
        while (Wink)
        {
            if (tempCol.a >= colAlpha)
            {
                UpDown = false;
            }
            else if (tempCol.a <= 0.2f)
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

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(TutorialData.EnemyOne.EnemyPrefab, TutorialData.SpawnEnemyPoint.position, TutorialData.SpawnEnemyPoint.rotation) as GameObject;
        enemy.layer = 0;
        Bot bot = enemy.GetComponent<Bot>();
        Man tMan = enemy.GetComponent<Man>();
        tMan.health = TutorialData.EnemyOne.MaxHealth;
        tMan.Rewarded = TutorialData.EnemyOne.Rewarded;
        tMan.runing = true;
        tMan.speed_max = 2;
        enemy.GetComponent<Band>().weapon = TutorialData.EnemyOne.EnemyWeapons.ToArray();
        TutorialData.MInput.MainElements.CountEnemy += 1;
        TutorialData.MInput.MainElements.CountEnemyesText.text = "Enemyes: " + TutorialData.MInput.MainElements.CountEnemy;
        
        tMan.killer = TutorialData.PlayerScript.player;
        bot.doing = "idle";
        bot.have_point = false;
        enemy.GetComponent<Band>().StartAttack = true;
        EnemyOneAttak = true;

        TutorialData.EnemyManScript = tMan;
    }

    void SpawnHardEnemy()
    {
        GameObject enemy = Instantiate(TutorialData.EnemyTwo.EnemyPrefab, TutorialData.SpawnEnemyPoint.position, TutorialData.SpawnEnemyPoint.rotation) as GameObject;
        enemy.layer = 0;
        Bot bot = enemy.GetComponent<Bot>();
        Man tMan = enemy.GetComponent<Man>();
        tMan.health = TutorialData.EnemyTwo.MaxHealth;
        tMan.Rewarded = TutorialData.EnemyTwo.Rewarded;
        tMan.runing = true;
        tMan.speed_max = 2;
        enemy.GetComponent<Band>().weapon = TutorialData.EnemyTwo.EnemyWeapons.ToArray();
        TutorialData.MInput.MainElements.CountEnemy += 1;
        TutorialData.MInput.MainElements.CountEnemyesText.text = "Enemyes: " + TutorialData.MInput.MainElements.CountEnemy;

        tMan.killer = TutorialData.PlayerScript.player;
        bot.doing = "idle";
        bot.have_point = false;
        enemy.GetComponent<Band>().StartAttack = true;
    }
}
