using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : BaseUI
{
    private UserSettingsData _userSettingsData;

    [Header("환경설정 버튼")]
    [Space]
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _keyManualButton;

    [Header("환경설정 보드")]
    [Space]
    [SerializeField] private GameObject _settingsBoard;
    [SerializeField] private GameObject _keyManualBoard;

    [Header("소리 설정")]
    [Space]
    [SerializeField] private Image _bgmSlider;
    [SerializeField] private Image _sfxSlider;
    private readonly float AudioChangeAmount = 0.20f;

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

    public void OnClickBGMChangeButton(bool changeUp)
    {
        float changeAmount = changeUp ? AudioChangeAmount : -AudioChangeAmount;

        _bgmSlider.fillAmount = Mathf.Clamp01(_bgmSlider.fillAmount + changeAmount);

        AudioManager.Instance.SetVolume(AudioType.BGM, _bgmSlider.fillAmount);
        UserDataManager.Instance.GetUserData<UserSettingsData>().BGMvalue = _bgmSlider.fillAmount;
    }

    public void OnClickSFXChangeButton(bool changeUp)
    {
        float changeAmount = changeUp ? AudioChangeAmount : -AudioChangeAmount;

        _sfxSlider.fillAmount = Mathf.Clamp01(_sfxSlider.fillAmount + changeAmount);

        AudioManager.Instance.SetVolume(AudioType.SFX, _sfxSlider.fillAmount);
        UserDataManager.Instance.GetUserData<UserSettingsData>().SFXvalue = _sfxSlider.fillAmount;
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

    public void OnClickLobbyButton()
    {
        AudioManager.Instance.Play(AudioType.SFX, "Button");
        SceneLoader.Instance.LoadScene(ESceneType.Lobby);
        Close();
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
        _bgmSlider.fillAmount = data.BGMvalue;
        _sfxSlider.fillAmount = data.SFXvalue;
    }
}
