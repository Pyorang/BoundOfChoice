using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum EWindowUIType
{
    None,
    Inventory,
    Shop,
    Setting
}

public class InGameUIController : MonoBehaviour
{
    public const string InventoryOpen = "InventoryOpen";
    public const string InventoryClose = "InventoryClose";
    public const string ShopOpen = "ShopOpen";
    public const string ShopClose = "ShopClose";
    public const string Button = "Button";

    private EWindowUIType _currentWindowUI = EWindowUIType.None;
    private Dictionary<EWindowUIType, Action> _windowUiToggleActions;
    
    [Header("체력 관련")]
    [Space]
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private TextMeshProUGUI _healthText;

    [Header("마나 관련")]
    [Space]
    [SerializeField] private Image _manaBarImage;
    [SerializeField] private TextMeshProUGUI _manaText;

    [Header("공격력 및 속도 관련")]
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _speedText;

    [Header("골드 관련")]
    [SerializeField] private Image _goldIconImage;
    [SerializeField] private TextMeshProUGUI _goldText;

    [Header("영혼 관련")]
    [SerializeField] private Image _spiritImage;

    private void Awake()
    {
        PlayerHealth.OnHealthValueUpdate += OnUpdateHealthUI;
        PlayerMana.OnManaChanged += OnUpdateManaUI;
        PlayerMovement.OnSpeedChanged += OnUpdateSpeedUI;
        GoldManager.OnGoldChanged += OnUpdateGoldUI;
        SpiritManager.OnSpiritPieceChanged += OnUpdateSpiritUI;
        InitializeWindowUIToggles();
    }

    private void InitializeWindowUIToggles()
    {
        _windowUiToggleActions = new Dictionary<EWindowUIType, Action>
        {
            { EWindowUIType.Inventory, OnClickInventoryButton },
            { EWindowUIType.Shop,  OnClickShopButton  },
            { EWindowUIType.Setting, OnClickInGameSettingsButton }
        };
    }

    private void OnDestroy()
    {
        PlayerHealth.OnHealthValueUpdate -= OnUpdateHealthUI;
        PlayerMana.OnManaChanged -= OnUpdateManaUI;
        PlayerMovement.OnSpeedChanged -= OnUpdateSpeedUI;
        GoldManager.OnGoldChanged -= OnUpdateGoldUI;
        SpiritManager.OnSpiritPieceChanged -= OnUpdateSpiritUI;
    }

    private void Update()
    {
        if(!PlayerHealth.Instance.IsDeath)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                TryToggleUI(EWindowUIType.Inventory);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TryToggleUI(EWindowUIType.Setting);
            }

            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                TryToggleUI(EWindowUIType.Shop);
            }
        }
    }

    private void TryToggleUI(EWindowUIType type)
    {
        if (_currentWindowUI == EWindowUIType.Setting) return;
        if (_currentWindowUI == EWindowUIType.None || _currentWindowUI == type)
        {
            _windowUiToggleActions[type]();
            _currentWindowUI = (_currentWindowUI == type) ? EWindowUIType.None : type;
        }
    }

    private void CloseSettingUI()
    {
        _currentWindowUI = EWindowUIType.None;
    }

    public void OnClickInventoryButton()
    {
        InventoryUI.Instance.ToggleInventory();
        string inventorySound = InventoryUI.Instance.gameObject.activeSelf ? InventoryOpen : InventoryClose;
        AudioManager.Instance.Play(AudioType.SFX, inventorySound);
    }

    public void OnClickShopButton()
    {
        ShopUI.Instance.ToggleShop();
        string shopSound = ShopUI.Instance.gameObject.activeSelf ? ShopOpen : ShopClose;
        AudioManager.Instance.Play(AudioType.SFX, shopSound);
    }

    public void OnClickInGameSettingsButton()
    {
        var uiData = new BaseUIData();
        uiData.ActionOnClose = CloseSettingUI;
        UIManager.Instance.OpenUI<InGameSettingsUI>(uiData);
        AudioManager.Instance.Play(AudioType.SFX, Button);
    }

    public void OnUpdateHealthUI(int health, int maxHealth)
    {
        _healthBarImage.fillAmount = (float)health / maxHealth;
        _healthText.text = $"{health} \n/ {maxHealth}";
    }

    public void OnUpdateManaUI(int mana, int maxMana)
    {
        _manaBarImage.fillAmount = (float)mana / maxMana;
        _manaText.text = $"{mana} \n/ {maxMana}";
    }

    public void OnUpdateSpeedUI(int speed)
    {
        _speedText.text = $"{speed}";
    }

    public void OnUpdateDamageUI(int damage)
    {
        _damageText.text = $"{damage}";
    }

    public void OnUpdateGoldUI(int gold)
    {
        _goldText.text = gold.ToString("N0");
    }

    public void OnUpdateSpiritUI(int piece, int maxPiece)
    {
        _spiritImage.fillAmount = (float)piece / maxPiece;

    }
}
