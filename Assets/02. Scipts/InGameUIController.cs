using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    [SerializeField] private Button _inGameSettingsButton;

    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private Slider _manaBarSlider;

    public void OnClickInGameSettingsButton()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<InGameSettingsUI>(uiData);
        AudioManager.Instance.Play(AudioType.SFX, "ui_openUI_button_click");
    }

    public void OnUpdateHealthUI(int health, int maxHealth)
    {
        _healthBarSlider.value = (float)health / maxHealth;
    }

    public void OnUpdateManaUI(int mana, int maxMana)
    {
        _manaBarSlider.value = (float)mana / maxMana;
    }
}
