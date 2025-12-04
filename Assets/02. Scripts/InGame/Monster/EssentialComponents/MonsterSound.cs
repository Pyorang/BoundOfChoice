using UnityEngine;

public class MonsterSound : MonoBehaviour
{
    [Header("사운드 파일명 설정")]
    [SerializeField] private string _attackSound;
    [SerializeField] private string _deathSound;

    private void PlayAttackSound()
    {
        AudioManager.Instance.Play(AudioType.SFX, _attackSound);
    }

    private void PlayDeathSound()
    {
        AudioManager.Instance.Play(AudioType.SFX, _deathSound);
    }
}
