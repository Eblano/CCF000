using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour
{
    public int AddMoneyCheet = 0;
    public MainMenuClass MainMenu;
    public StoreClass Store;

    private int SelectPreparationPanel = -1;
    private int SelectCharacter = 0;
    private int SelectMap = 0;
    private int SelectWeapon = 0;

    private bool ShowDonatePanel = false;

    public int MoneyPlayer
    {
        get
        {
            return PlayerPrefs.GetInt("moneyPlayer");
        }
        set
        {
            PlayerPrefs.SetInt("moneyPlayer", PlayerPrefs.GetInt("moneyPlayer") + value);
        }
    }

    [ContextMenu("AddMoney")]
    public void AddMoneyPlayer()
    {
        MoneyPlayer = AddMoneyCheet;
    }

    [ContextMenu("ClearPrefs")]
    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    void Start ()
    {
        LoadParam();
        LoadDataCharacter();
        LoadDataMap();
        LoadDataWeapon();
        CountStarts();
    }

    public void CountStarts()
    {
        if (!PlayerPrefs.HasKey("countStarts"))
        {
            PlayerPrefs.SetInt("countStarts", 0);
            MainMenu.MainMenuPanel.SetActive(false);
            MainMenu.TutorialOfferPanel.SetActive(true);
        }
        PlayerPrefs.SetInt("countStarts", PlayerPrefs.GetInt("countStarts") + 1);
        if(PlayerPrefs.GetInt("countStarts") > 3)
        {
            ShowDonatePanel = true;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OptionsButton(bool on)
    {
        if (on)
        {
            MainMenu.MainMenuPanel.SetActive(false);
            MainMenu.OptionsPanel.SetActive(true);
        }
        else
        {
            MainMenu.MainMenuPanel.SetActive(true);
            MainMenu.OptionsPanel.SetActive(false);
        }
    }

    public void SoundToggleClick()
    {
        if (MainMenu.SoundToggle.isOn)
        {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("sound", 1);
        }
        else
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("sound", 0);
        }
    }

    public void GrphicLevelButton(int level)
    {
        for (int i = 0; i < MainMenu.GraphicSelectImages.Count; i++)
        {
            if (i == level)
            {
                MainMenu.GraphicSelectImages[i].color = MainMenu.SelectGraphic;
            }
            else
            {
                MainMenu.GraphicSelectImages[i].color = MainMenu.UnSelectGraphics;
            }
        }
        switch (level)
        {
            case 0:
                QualitySettings.SetQualityLevel(0, false);
                break;
            case 1:
                QualitySettings.SetQualityLevel(2, false);
                break;
            case 2:
                QualitySettings.SetQualityLevel(4, false);
                break;
        }
        PlayerPrefs.SetInt("qualitylevel", level);
    }

    public void LoadParam()
    {
        if (!PlayerPrefs.HasKey("sound")) PlayerPrefs.SetInt("sound", 1);
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            MainMenu.SoundToggle.isOn = true;
            AudioListener.volume = 1;
        }
        else
        {
            MainMenu.SoundToggle.isOn = false;
            AudioListener.volume = 0;
        }

        if (!PlayerPrefs.HasKey("qualitylevel")) PlayerPrefs.SetInt("qualitylevel", 1);
        GrphicLevelButton(PlayerPrefs.GetInt("qualitylevel"));
    }

    public void PreparetionButton(bool on)
    {
        if(on)
        {
            Store.MoneyPlayerText.text = "Money: " + MoneyPlayer + "$";
            MainMenu.MainMenuPanel.SetActive(false);
            Store.PreparationPanel.SetActive(true);
            if(ShowDonatePanel)
            {
                DonateOffer(true);
                PlayerPrefs.SetInt("countStarts", 0);
                ShowDonatePanel = false;
            }
        }
        else
        {
            Store.PreparationPanel.SetActive(false);
            MainMenu.MainMenuPanel.SetActive(true);
        }
    }

    public void PreparetionPanelButtons(int num)
    {
        if(num != SelectPreparationPanel)
        {
            if (SelectPreparationPanel >= 0)
            {
                Store.PreparationPanels[SelectPreparationPanel].SetActive(false);
                Store.ImagesButtonPreparation[SelectPreparationPanel].color = Store.UnSelectButtonColor;
            }
            Store.PreparationPanels[num].SetActive(true);
            Store.ImagesButtonPreparation[num].color = Store.SelectButtonColor;
            SelectPreparationPanel = num;
            Store.Weapons.WeaponChoosePanel.SetActive(false);
        }
    }

    public void BuyCharacterButton(int num)
    {
        if(Store.Character.CostsCharacters[num] > MoneyPlayer)
        {
            DonateOffer(true);
        }
        else
        {
            MoneyPlayer = -Store.Character.CostsCharacters[num];
            Store.MoneyPlayerText.text = "Money: " + MoneyPlayer + "$";
            Store.Character.ButtonsBuy[num].SetActive(false);
            Store.Character.ButtonsSelect[num].SetActive(true);
            Store.Character.CostTexts[num].gameObject.SetActive(false);
            Store.Character.DescriptionTexts[num].gameObject.SetActive(true);
            PlayerPrefs.SetInt("char" + num, 1);
        }
    }

    public void SelectCharacterButton(int num)
    {
        Store.Character.SelectRams[SelectCharacter].SetActive(false);
        Store.Character.ButtonsSelect[SelectCharacter].SetActive(true);
        PlayerPrefs.SetInt("selchar", num);
        Store.Character.SelectRams[num].SetActive(true);
        Store.Character.ButtonsSelect[num].SetActive(false);
        SelectCharacter = num;
    }

    public void BuyMapButton(int num)
    {
        if(Store.Maps.CostsMaps[num] > MoneyPlayer)
        {
            DonateOffer(true);
        }
        else
        {
            MoneyPlayer = -Store.Maps.CostsMaps[num];
            Store.MoneyPlayerText.text = "Money: " + MoneyPlayer + "$";
            Store.Maps.ButtonsBuy[num].SetActive(false);
            Store.Maps.ButtonsSelect[num].SetActive(true);
            Store.Maps.CostTexts[num].gameObject.SetActive(false);
            PlayerPrefs.SetInt("map" + num, 1);
        }
    }

    public void SelectMapButton(int num)
    {
        Store.Maps.SelectRams[SelectMap].color = Store.Maps.UnSelectMapColor;
        Store.Maps.ButtonsSelect[SelectMap].SetActive(true);
        PlayerPrefs.SetInt("selmap", num);
        Store.Maps.SelectRams[num].color = Store.Maps.SelectMapColor;
        Store.Maps.ButtonsSelect[num].SetActive(false);
        SelectMap = num;
    }

    public void LoadDataMap()
    {
        for(int i=0; i<Store.Maps.CostsMaps.Count; i++)
        {
            //if(i==0)
            //    PlayerPrefs.SetInt("map" + i, 1);
            if (!PlayerPrefs.HasKey("map" + i))
                PlayerPrefs.SetInt("map" + i, 0);
            if (!PlayerPrefs.HasKey("selmap"))
                PlayerPrefs.SetInt("selmap", -1);
            if(PlayerPrefs.GetInt("map"+i)==0)
            {
                Store.Maps.ButtonsBuy[i].SetActive(true);
                Store.Maps.ButtonsSelect[i].SetActive(false);
                Store.Maps.CostTexts[i].text = Store.Maps.CostsMaps[i] + "$";
                Store.Maps.CostTexts[i].gameObject.SetActive(true);
            }
            else
            {
                Store.Maps.ButtonsBuy[i].SetActive(false);
                Store.Maps.ButtonsSelect[i].SetActive(true);
                Store.Maps.CostTexts[i].gameObject.SetActive(false);
            }
            Store.Maps.SelectRams[i].color = Store.Maps.UnSelectMapColor;
        }
        if(PlayerPrefs.GetInt("selmap") != -1)
            SelectMapButton(PlayerPrefs.GetInt("selmap"));
    }

    public void LoadDataCharacter()
    {
        for(int i=0; i<Store.Character.CostsCharacters.Count; i++)
        {
            //if(i==0)
            //    PlayerPrefs.SetInt("char" + i, 1);
            if (!PlayerPrefs.HasKey("char"+i))
                PlayerPrefs.SetInt("char"+i, 0);
            if(!PlayerPrefs.HasKey("selchar"))
                PlayerPrefs.SetInt("selchar", -1);
            if(PlayerPrefs.GetInt("char"+i)==0)
            {
                Store.Character.ButtonsBuy[i].SetActive(true);
                Store.Character.ButtonsSelect[i].SetActive(false);
                Store.Character.CostTexts[i].gameObject.SetActive(true);
                Store.Character.CostTexts[i].text = Store.Character.CostsCharacters[i] + "$";
                Store.Character.DescriptionTexts[i].gameObject.SetActive(false);
            }
            else
            {
                Store.Character.ButtonsBuy[i].SetActive(false);
                Store.Character.ButtonsSelect[i].SetActive(true);
                Store.Character.CostTexts[i].gameObject.SetActive(false);
                Store.Character.DescriptionTexts[i].gameObject.SetActive(true);
            }
            Store.Character.SelectRams[i].SetActive(false);
        }
        if (PlayerPrefs.GetInt("selchar") != -1)
            SelectCharacterButton(PlayerPrefs.GetInt("selchar"));
    }

    public void WeaponButtonChoose(int num)
    {
        if(num == -1)
        {
            Store.Weapons.WeaponChoosePanel.SetActive(false);
            Store.PreparationPanels[1].SetActive(true);
        }
        else
        {
            Store.Weapons.MainIconWeapon.sprite = Store.Weapons.IconsWeapon[num].sprite;
            Store.Weapons.NameWeapon.text = Store.Weapons.NamesWeapon[num];
            if(PlayerPrefs.GetInt("weapon"+num)==0)
            {
                Store.Weapons.DescriptionWeapon.gameObject.SetActive(false);
                Store.Weapons.CostWeapon.gameObject.SetActive(true);
                Store.Weapons.CostWeapon.text = Store.Weapons.CostsWeapons[num] + "$";
                Store.Weapons.BuyButtonWeapon.SetActive(true);
                Store.Weapons.SelectButtonWeapon.SetActive(false);
            }
            else
            {
                Store.Weapons.DescriptionWeapon.gameObject.SetActive(true);
                Store.Weapons.CostWeapon.gameObject.SetActive(false);
                Store.Weapons.BuyButtonWeapon.SetActive(false);
                Store.Weapons.DescriptionWeapon.text = Store.Weapons.DescriptionTexts[num];
                if (PlayerPrefs.GetInt("selwep")==num)
                    Store.Weapons.SelectButtonWeapon.SetActive(false);
                else
                    Store.Weapons.SelectButtonWeapon.SetActive(true);
            }
            Store.PreparationPanels[1].SetActive(false);
            Store.Weapons.WeaponChoosePanel.SetActive(true);
            SelectWeapon = num;
        }
    }

    public void BuyWeaponButton()
    {
        if(Store.Weapons.CostsWeapons[SelectWeapon] > MoneyPlayer)
        {
            DonateOffer(true);
        }
        else
        {
            MoneyPlayer = -Store.Weapons.CostsWeapons[SelectWeapon];
            Store.Weapons.DescriptionWeapon.gameObject.SetActive(true);
            Store.Weapons.CostWeapon.gameObject.SetActive(false);
            Store.Weapons.BuyButtonWeapon.SetActive(false);
            Store.Weapons.SelectButtonWeapon.SetActive(true);
            Store.Weapons.ImagesFonButtonsWeapon[SelectWeapon].color = Store.Weapons.BuyedFoneColor;
            Store.MoneyPlayerText.text = "Money: " + MoneyPlayer + "$";
            PlayerPrefs.SetInt("weapon" + SelectWeapon, 1);
        }
    }

    public void SelectWeaponButton()
    {
        int oldSelect = PlayerPrefs.GetInt("selwep");
        if (oldSelect != -1)
        {
            Store.Weapons.ImagesRamkWeapon[oldSelect].color = Store.Weapons.UnSelecterRamColor;
            Store.Weapons.ImagesFonButtonsWeapon[oldSelect].color = Store.Weapons.BuyedFoneColor;
        }
        Store.Weapons.ImagesRamkWeapon[SelectWeapon].color = Store.Weapons.SelectedRamColor;
        Store.Weapons.ImagesFonButtonsWeapon[SelectWeapon].color = Store.Weapons.SelectedFoneColor;
        PlayerPrefs.SetInt("selwep", SelectWeapon);
        Store.Weapons.SelectButtonWeapon.SetActive(false);
    }

    public void LoadDataWeapon()
    {
        for(int i=0; i<Store.Weapons.CostsWeapons.Count; i++)
        {
            //if(i==0)
            //{
            //    PlayerPrefs.SetInt("weapon" + i, 1);
            //}
            if (!PlayerPrefs.HasKey("weapon" + i))
                PlayerPrefs.SetInt("weapon" + i, 0);
            if (!PlayerPrefs.HasKey("selwep"))
                PlayerPrefs.SetInt("selwep", -1);
            if(PlayerPrefs.GetInt("weapon"+i)==0)
            {
                Store.Weapons.ImagesFonButtonsWeapon[i].color = Store.Weapons.DontBuyFoneColor;
                Store.Weapons.ImagesRamkWeapon[i].color = Store.Weapons.RamDontBuyedWeapon;
            }
            else
            {
                Store.Weapons.ImagesFonButtonsWeapon[i].color = Store.Weapons.BuyedFoneColor;
                Store.Weapons.ImagesRamkWeapon[i].color = Store.Weapons.UnSelecterRamColor;
            }
        }
        if (PlayerPrefs.GetInt("selwep") != -1)
        {
            SelectWeapon = PlayerPrefs.GetInt("selwep");
            SelectWeaponButton();
        }
    }

    public void StartGame(bool on)
    {
        if(on)
        {
            Store.PreparationPanel.SetActive(false);
            Store.StartPanel.SetActive(true);
        }
        else
        {
            Store.StartPanel.SetActive(false);
            Store.PreparationPanel.SetActive(true);
        }
    }

    public void SelectingDifficultyLevel(int level)
    {
        PlayerPrefs.SetInt("levelGame", level);
        SceneManager.LoadScene(1);
    }

    public void DonateOffer(bool on)
    {
        if (on)
            Store.DonatePanel.SetActive(true);
        else
            Store.DonatePanel.SetActive(false);
    }
}
