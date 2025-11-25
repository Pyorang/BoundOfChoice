using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    public const string UI_OPEN_BUTTON_CLICK = "ui_openUI_button_click";

    [SerializeField] private Button _inGameSettingsButton;

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

    private void Awake()
    {
        PlayerHealth.OnHealthChanged += OnUpdateHealthUI;
        PlayerMana.OnManaChanged += OnUpdateManaUI;
        PlayerMovement.OnSpeedChanged += OnUpdateSpeedUI;
        GoldManager.OnGoldChanged += OnUpdateGoldUI;
    }

    private void OnDestroy()
    {
        PlayerHealth.OnHealthChanged -= OnUpdateHealthUI;
        PlayerMana.OnManaChanged -= OnUpdateManaUI;
        PlayerMovement.OnSpeedChanged -= OnUpdateSpeedUI;
        GoldManager.OnGoldChanged -= OnUpdateGoldUI;
    }

    public void OnClickInventoryButton()
    {
        InventoryUI.Instance.ToggleInventory();
        AudioManager.Instance.Play(AudioType.SFX, UI_OPEN_BUTTON_CLICK);
    }

    public void OnClickInGameSettingsButton()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<InGameSettingsUI>(uiData);
        AudioManager.Instance.Play(AudioType.SFX, UI_OPEN_BUTTON_CLICK);
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
}
