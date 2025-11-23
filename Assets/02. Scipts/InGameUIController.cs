using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    [SerializeField] private Button _inGameSettingsButton;

    [SerializeField] private Image _healthBarImage;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Image _manaBarImage;
    [SerializeField] private TextMeshProUGUI _manaText;

    public void OnClickInGameSettingsButton()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<InGameSettingsUI>(uiData);
        AudioManager.Instance.Play(AudioType.SFX, "ui_openUI_button_click");
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
}
