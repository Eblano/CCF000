using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using CnControls;

[Serializable]
public class MainElementsClass
{
    public GameObject MoneyPanel;
    public Text MoneyText;
    public Text MoneyAddText;
    public Color MoneyAdd;
    public Color MoneyDec;
    public GameObject MiniMap;
    public GameObject MainObjectsPanel;
    public Image HealthBar;
    public Image ArmorBar;
    public GameObject WantedBar;
    public GameObject ActionButton;
    public Image BlackFone;
    public Text CountEnemyesText;
    public int CountEnemy = 0;

    public GameObject PausePanel;
}

[Serializable]
public class ManControllerClass
{
    public int MaxHealthPlayer = 500;
    public int MaxArmorPlayer = 100;
    public GameObject ManControllerPanel;
    public SimpleJoystick SimpJoyst;
    public SimpleButton AimButton;
    public SimpleButton FireButton;
    public GameObject JumpButton;
    public GameObject ParachuteButton;
}

[Serializable]
public class DeathPanelClass
{
    public GameObject DeathPanel;
    public Text KillEnemyText;
    public Text TimeText;
    public Text RecordKillEnemyText;
    public Text RecordTimeText;
    public GameObject NewRecordKills;
    public GameObject NewRecordTime;
}

[Serializable]
public class MainMenuClass
{
    public GameObject MainMenuPanel;
    public GameObject OptionsPanel;
    public Toggle SoundToggle;
    public List<Image> GraphicSelectImages = new List<Image>();
    public Color SelectGraphic;
    public Color UnSelectGraphics;

    public GameObject TutorialOfferPanel;
}

[Serializable]
public class StoreClass
{
    public GameObject PreparationPanel;
    public GameObject StartPanel;
    public GameObject DonatePanel;
    public Text MoneyPlayerText;
    public List<GameObject> PreparationPanels = new List<GameObject>();
    public List<Image> ImagesButtonPreparation = new List<Image>();
    public Color SelectButtonColor;
    public Color UnSelectButtonColor;
    public CaharacterPanelClass Character;
    public MapPanelClass Maps;
    public WeaponPanelClass Weapons;
}

[Serializable]
public class CaharacterPanelClass
{
    public List<GameObject> ButtonsBuy = new List<GameObject>();
    public List<GameObject> ButtonsSelect = new List<GameObject>();
    public List<GameObject> SelectRams = new List<GameObject>();
    public List<Text> CostTexts = new List<Text>();
    public List<Text> DescriptionTexts = new List<Text>();
    public List<int> CostsCharacters = new List<int>();
}

[Serializable]
public class MapPanelClass
{
    public List<GameObject> ButtonsBuy = new List<GameObject>();
    public List<GameObject> ButtonsSelect = new List<GameObject>();
    public List<Image> SelectRams = new List<Image>();
    public Color SelectMapColor;
    public Color UnSelectMapColor;
    public List<Text> CostTexts = new List<Text>();
    public List<int> CostsMaps = new List<int>();
}

[Serializable]
public class WeaponPanelClass
{
    public List<Image> ImagesFonButtonsWeapon = new List<Image>();
    public List<Image> ImagesRamkWeapon = new List<Image>();
    public List<Image> IconsWeapon = new List<Image>();
    public Color RamDontBuyedWeapon;
    public Color DontBuyFoneColor;
    public Color BuyedFoneColor;
    public Color SelectedFoneColor;
    public Color SelectedRamColor;
    public Color UnSelecterRamColor;
    public List<string> NamesWeapon = new List<string>();
    public List<int> CostsWeapons = new List<int>();
    public List<string> DescriptionTexts = new List<string>();
    public Image MainIconWeapon;
    public Text NameWeapon;
    public GameObject WeaponChoosePanel;
    public Text CostWeapon;
    public Text DescriptionWeapon;
    public GameObject BuyButtonWeapon;
    public GameObject SelectButtonWeapon;
}
