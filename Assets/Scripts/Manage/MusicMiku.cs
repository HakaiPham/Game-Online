using UnityEngine;
using Fusion;

[RequireComponent(typeof(AudioSource))]
public class MusicMiku : NetworkBehaviour
{
    public AudioClip musicClip;
    private AudioSource audioSource;

    [Networked]
    public float MusicStartTime { get; set; }

    private bool localMusicStarted = false;
    private bool isSpawnedDone = false;

    public override void Spawned()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        isSpawnedDone = true;

        // Host: lần đầu vào game thì set thời gian bắt đầu nhạc
        if (Object.HasStateAuthority && MusicStartTime == 0)
        {
            MusicStartTime = (float)Runner.SimulationTime;
            Debug.Log($"[Host] Set MusicStartTime: {MusicStartTime}");

            audioSource.time = 0f;
            audioSource.Play();
            localMusicStarted = true;
        }
    }

    void Update()
    {
        if (!isSpawnedDone || localMusicStarted || MusicStartTime <= 0) return;

        // Tính thời gian đã trôi qua từ lúc host bắt đầu phát nhạc
        float elapsed = (float)(Runner.SimulationTime - MusicStartTime);
        float playbackTime = elapsed % musicClip.length;

        Debug.Log($"[Client] Elapsed: {elapsed}, PlaybackTime: {playbackTime}");

        audioSource.time = playbackTime;
        audioSource.Play();
        localMusicStarted = true;
    }
}
