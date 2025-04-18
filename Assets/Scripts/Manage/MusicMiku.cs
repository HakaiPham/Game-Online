using UnityEngine;
using Fusion;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicMiku : NetworkBehaviour
{
    [Header("Music Settings")]
    [SerializeField] private AudioClip _musicClip;
    [SerializeField][Range(0.0f, 1.0f)] private float _volume = 1.0f;

    [Networked] public float NetworkedMusicTime { get; set; }
    [Networked] public NetworkBool NetworkedIsPlaying { get; set; }
    [Networked] public PlayerRef CurrentHost { get; set; }

    private AudioSource _audioSource;
    private bool _isHostInitialized = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _musicClip;
        _audioSource.volume = _volume;
        _audioSource.loop = true;
        _audioSource.playOnAwake = true; // Có thể bật nhưng vẫn sẽ Pause() ở Spawned()
    }

    public override void Spawned()
    {
        _audioSource.Pause(); // Dừng phát khi mới vào game

        if (Object.HasStateAuthority)
        {
            InitializeAsHost();
        }
        else
        {
            StartCoroutine(WaitForHostMusicData());
        }
    }

    private void InitializeAsHost()
    {
        if (_isHostInitialized) return;

        CurrentHost = Object.InputAuthority;
        NetworkedIsPlaying = true;
        _isHostInitialized = true;

        NetworkedMusicTime = _audioSource.time;
        _audioSource.UnPause(); // Tiếp tục phát nếu đã play on awake

        StartCoroutine(SyncMusicTimeRoutine());
    }

    private IEnumerator WaitForHostMusicData()
    {
        yield return new WaitUntil(() => NetworkedIsPlaying);

        _audioSource.time = NetworkedMusicTime;
        _audioSource.Play();
    }

    private IEnumerator SyncMusicTimeRoutine()
    {
        while (Object.HasStateAuthority && NetworkedIsPlaying)
        {
            NetworkedMusicTime = _audioSource.time;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority && NetworkedIsPlaying)
        {
            if (Mathf.Abs(_audioSource.time - NetworkedMusicTime) > 0.1f)
            {
                _audioSource.time = NetworkedMusicTime;

                if (!_audioSource.isPlaying)
                {
                    _audioSource.Play();
                }
            }
        }
    }

    public override void Render()
    {
        if (!Object.HasStateAuthority && NetworkedIsPlaying)
        {
            _audioSource.time = Mathf.Lerp(_audioSource.time, NetworkedMusicTime, 0.1f);
        }
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        if (Object.HasStateAuthority && CurrentHost == Object.InputAuthority)
        {
            TryTransferHostAuthority(runner);
        }
    }

    private void TryTransferHostAuthority(NetworkRunner runner)
    {
        foreach (var player in runner.ActivePlayers)
        {
            if (player != Object.InputAuthority)
            {
                var objects = FindObjectsOfType<MusicMiku>();
                foreach (var obj in objects)
                {
                    if (obj.Object != null && obj.Object.InputAuthority == player)
                    {
                        obj.CurrentHost = player;
                        obj.NetworkedIsPlaying = true;
                        obj._audioSource.time = NetworkedMusicTime;
                        obj._audioSource.Play();
                        return;
                    }
                }
            }
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void Rpc_PlayMusic(NetworkBool play)
    {
        if (CurrentHost == Object.InputAuthority)
        {
            NetworkedIsPlaying = play;
            if (play) _audioSource.Play();
            else _audioSource.Stop();
        }
    }
}
