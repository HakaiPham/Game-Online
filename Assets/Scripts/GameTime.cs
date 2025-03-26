using UnityEngine;
using Fusion;
using TMPro;
using System;
using System.Collections;

public class GameTime : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnTimeChanged))]
    public float countdownTime { get; set; }

    public TextMeshProUGUI countdownText;

    private NetworkRunner runner;
    private NetworkObject networkObject;

    void Start()
    {
        networkObject = GetComponent<NetworkObject>();
        if (networkObject != null)
        {
            runner = networkObject.Runner;
            if (runner != null && runner.IsServer)
            {
                Debug.Log(">>>>>>");
                countdownTime = 300f;
                StartCoroutine(ServerCountdown());
            }
        }
        else
        {
            Debug.LogError("NetworkObject not found on " + gameObject.name);
        }
    }

    IEnumerator ServerCountdown()
    {
        while (countdownTime > 0)
        {
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownTime = 0;
        Rpc_TimeOut();
    }

    public void OnTimeChanged()
    {
        TimeSpan time = TimeSpan.FromSeconds(countdownTime);
        countdownText.text = time.ToString(@"mm\:ss");
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void Rpc_TimeOut()
    {
        countdownText.text = "Hết giờ!";
        // Thêm các hành động khi hết giờ vào đây
    }
}