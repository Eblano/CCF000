using UnityEngine;
using System.Collections;
using CnControls;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainInput : MonoBehaviour
{
    public static MainInput instance;
    public int AddMoneyCheet = 0;
    public Player PlayerScript;
    public int MoneyPlayer
    {
        get
        {
            return PlayerPrefs.GetInt("moneyPlayer");
        }
        set
        {
            PlayerPrefs.SetInt("moneyPlayer", PlayerPrefs.GetInt("moneyPlayer") + value);
            //StartCoroutine(MoneyPlayerAdd(value));
        }
    }

    private int playerHealth;
    public int PlayerHealth
    {
        get
        {
            return playerHealth;
        }
        set
        {
            playerHealth = value;
            MainElements.HealthBar.fillAmount = (float)playerHealth / ManController.MaxHealthPlayer;
        }
    }

    public int PlayerArmor
    {
        get
        {
            return PlayerPrefs.GetInt("armorCount");
        }
        set
        {
            PlayerPrefs.SetInt("armorCount", value);
            MainElements.ArmorBar.fillAmount = (float)value / ManController.MaxArmorPlayer;
        }
    }

    private bool KillRecord = false;
    public int KillEnemyesRaund
    {
        get
        {
            return PlayerPrefs.GetInt("maxkillraund");
        }
        set
        {
            if(value > PlayerPrefs.GetInt("maxkillraund"))
            {
                PlayerPrefs.SetInt("maxkillraund", value);
                DeathPanel.NewRecordKills.SetActive(true);
                KillRecord = true;
            }
        }
    }

    private bool TimeRecord = false;
    public int TimeRaund
    {
        get
        {
            return PlayerPrefs.GetInt("timeround");
        }
        set
        {
            if (value > PlayerPrefs.GetInt("timeround"))
            {
                PlayerPrefs.SetInt("timeround", value);
                DeathPanel.NewRecordTime.SetActive(true);
                TimeRecord = true;
            }
        }
    }

    public MainElementsClass MainElements;
    public ManControllerClass ManController;
    public DeathPanelClass DeathPanel;

    public Text WeaponBulletText;
    public Image ImageWeaponPlayer;

    [ContextMenu("AddMoney")]
    public void AddMoneyPlayer()
    {
        MoneyPlayer = AddMoneyCheet;
    }

    IEnumerator MoneyPlayerAdd(int count)
    {
        MainElements.MoneyText.text = MoneyPlayer + " $";
        if(count > 0)
        {
            MainElements.MoneyAddText.color = MainElements.MoneyAdd;
            MainElements.MoneyAddText.text = "+" + count + " $";
            MainElements.MoneyAddText.gameObject.SetActive(true);
        }
        else if(count < 0)
        {
            MainElements.MoneyAddText.color = MainElements.MoneyDec;
            MainElements.MoneyAddText.text = count + " $";
            MainElements.MoneyAddText.gameObject.SetActive(true);
        }
        MainElements.MoneyPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        MainElements.MoneyPanel.gameObject.SetActive(false);
        MainElements.MoneyAddText.gameObject.SetActive(false);
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        AdmobAd.Instance().LoadInterstitialAd();
        PlayerHealth = ManController.MaxHealthPlayer;
        PlayerArmor = PlayerArmor;
        ImageWeaponPlayer.sprite = PlayerScript.GetComponent<WeaponInfo>().weapon[PlayerPrefs.GetInt("selwep") + 1].IconWeapon;
    }

    void Update()
    {
        WeaponInfo();
        UpdateFunctions();
    }

    void WeaponInfo()
    {
        Weapon wep = PlayerScript.man_script.GetComponent<Weapon>();
        if (wep.weapon.Count > 1)
        {
            WeaponBulletText.text = wep.weapon[1].bullets + "/" + wep.weapon[1].clips;
        }
    }

    void UpdateFunctions()
    {
        if(PlayerScript.man_script.doing == "sit_vehicle")
        {
            if(!MainElements.ActionButton.activeSelf)
                MainElements.ActionButton.SetActive(true);
        }
        else if(!MainElements.ActionButton.activeSelf && PlayerScript.man_script.TrigPlayer.collision && PlayerScript.man_script.doing != "go_out_vehicle" && PlayerScript.man_script.doing != "go_to_vehicle")
        {
            MainElements.ActionButton.SetActive(true);
        }
        else if(MainElements.ActionButton.activeSelf && !PlayerScript.man_script.TrigPlayer.collision)
        {
            MainElements.ActionButton.SetActive(false);
        }
    }

    public void RewardPlayer(int time, int kill, int reward, int bonus)
    {
        KillRecord = false;
        TimeRecord = false;
        KillEnemyesRaund = kill;
        TimeRaund = time;
        int level = PlayerPrefs.GetInt("levelGame");
        int countMoneyTime = 0;
        if (kill * 20 >= time)
            countMoneyTime = (time * (level + 1));
        else
            countMoneyTime = (kill * 20) * (level + 1);
        countMoneyTime += countMoneyTime * (bonus / 10);
        int money = countMoneyTime + reward;
        if(KillRecord)
        {
            money += 50;
        }
        if(TimeRecord)
        {
            money += 50;
        }
        DeathPanel.KillEnemyText.text = "Killed: " + kill + "[+" + reward + "$]";
        DeathPanel.TimeText.text = "Time: " + time + "[+" + countMoneyTime + "$]";
        if (KillRecord)
        {
            DeathPanel.RecordKillEnemyText.text = "New Record Killed: " + KillEnemyesRaund + "[+50$]";
        }
        else
        {
            DeathPanel.RecordKillEnemyText.text = "Record Killed: " + KillEnemyesRaund;
        }
        if (TimeRecord)
        {
            DeathPanel.RecordTimeText.text = "Record Time: " + TimeRaund + "[+50$]";
        }
        else
        {
            DeathPanel.RecordTimeText.text = "Record Time: " + TimeRaund;
        }
        MoneyPlayer = money;
    }

    public void AgainButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        AdmobAd.Instance().ShowInterstitialAd();
        SceneManager.LoadScene(0);
    }

    private bool pause = false;
    public void PausuButton()
    {
        pause = !pause;
        if(pause)
        {
            Time.timeScale = 0;
            MainElements.PausePanel.SetActive(true);
            MainElements.MainObjectsPanel.SetActive(false);
            ManController.ManControllerPanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            MainElements.PausePanel.SetActive(false);
            MainElements.MainObjectsPanel.SetActive(true);
            ManController.ManControllerPanel.SetActive(true);
        }
    }
}
