using System;
using UnityEngine;

public class UserSettingsData : IUserData
{
    public float BGMvalue { get; set; }
    public float SFXvalue { get; set; }
    public bool IsVibrationOn { get; set; } = true;

    public void SetDefaultData()
    {
        BGMvalue = 0.5f;
        SFXvalue = 0.5f;
        IsVibrationOn = true;
    }

    public bool LoadData()
    {
        bool result = false;

        BGMvalue = (PlayerPrefs.GetFloat(nameof(BGMvalue)));
        SFXvalue = (PlayerPrefs.GetFloat(nameof(SFXvalue)));
        IsVibrationOn = Convert.ToBoolean(PlayerPrefs.GetInt(nameof(IsVibrationOn), 1));

        result = true;
        return result;
    }

    public bool SaveData()
    {
        bool result = false;

        PlayerPrefs.SetFloat(nameof(BGMvalue), AudioManager.Instance.GetVolume(AudioType.BGM));
        PlayerPrefs.SetFloat(nameof(SFXvalue), AudioManager.Instance.GetVolume(AudioType.SFX));
        PlayerPrefs.SetInt(nameof(IsVibrationOn), Convert.ToInt32(IsVibrationOn));
        PlayerPrefs.Save();

        result = true;
        return result;
    }
}