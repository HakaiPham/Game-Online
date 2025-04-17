using UnityEngine;

public class MikuManager : MonoBehaviour
{
    public AudioSource audioSource; // Nguồn phát âm thanh
    public AudioClip[] soundClips;  // Mảng âm thanh thay đổi được ngoài Inspector

    private bool isSoundOn = true;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            isSoundOn = !isSoundOn;
            audioSource.mute = !isSoundOn;
            Debug.Log("Âm thanh: " + (isSoundOn ? "BẬT" : "TẮT"));
        }
    }

    // Hàm phát âm thanh theo chỉ số
    public void PlaySound(int index)
    {
        if (isSoundOn && soundClips != null && index >= 0 && index < soundClips.Length)
        {
            audioSource.PlayOneShot(soundClips[index]);
        }
    }

    // Hàm phát âm thanh ngẫu nhiên
    public void PlayRandomSound()
    {
        if (isSoundOn && soundClips != null && soundClips.Length > 0)
        {
            int randomIndex = Random.Range(0, soundClips.Length);
            PlaySound(randomIndex);
        }
    }
}
