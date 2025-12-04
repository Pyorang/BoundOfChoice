using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.Play(AudioType.BGM, "Lobby");
    }
}
