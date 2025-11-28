using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : BaseUI
{
    private UserSettingsData _userSettingsData;

    [Header("환경설정 버튼")]
    [Space]
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _keyManualButton;
    [SerializeField] private Button _returnButton;

    [Header("환경설정 보드")]
    [Space]
    [SerializeField] private GameObject _settingsBoard;
    [SerializeField] private GameObject _keyManualBoard;

    [Header("소리 설정")]
    [Space]
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    [Header("진동 설정")]
    [Space]
    [SerializeField] private Toggle _vibrationToggle;

    private void Start()
    {
        _userSettingsData = UserDataManager.Instance.GetUserData<UserSettingsData>();
        SetSoundSetting(_userSettingsData);
        _vibrationToggle.isOn = _userSettingsData.IsVibrationOn;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    public void OnBGMValueChanged()
    {
        AudioManager.Instance.SetVolume(AudioType.BGM, _bgmSlider.value);
        UserDataManager.Instance.GetUserData<UserSettingsData>().BGMvalue = _bgmSlider.value;
    }

    public void OnSFXValueChanged()
    {
        AudioManager.Instance.SetVolume(AudioType.SFX, _sfxSlider.value);
        UserDataManager.Instance.GetUserData<UserSettingsData>().SFXvalue = _sfxSlider.value;
    }

    public void OnVibrationToggleChanged()
    {
        CameraController.Instance.SetCameraEffectOn(_vibrationToggle.isOn);
        UserDataManager.Instance.GetUserData<UserSettingsData>().IsVibrationOn = _vibrationToggle.isOn;
    }

    public void OnClickSettingsButton()
    {
        AudioManager.Instance.Play(AudioType.SFX, "Button");
        _settingsButton.interactable = false;
        _settingsBoard.SetActive(true);
        _keyManualButton.interactable = true;
        _keyManualBoard.SetActive(false);
    }

    public void OnClickKeyManualButton()
    {
        AudioManager.Instance.Play(AudioType.SFX, "Button");
        _keyManualButton.interactable = false;
        _keyManualBoard.SetActive(true);
        _settingsButton.interactable = true;
        _settingsBoard.SetActive(false);
    }

    public void Save()
    {
        UserDataManager.Instance.SaveUserData();
    }

    public void OnClickReturnButton()
    {
        AudioManager.Instance.Play(AudioType.SFX, "Button");
        Close();
    }

    public override void Close(bool isCloseAll = false)
    {
        Save();
        base.Close(isCloseAll);
    }

    private void SetSoundSetting(UserSettingsData data)
    {
        _bgmSlider.value = data.BGMvalue;
        _sfxSlider.value = data.SFXvalue;
    }
}
