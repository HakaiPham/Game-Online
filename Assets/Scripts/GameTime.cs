using Fusion;
using UnityEngine;
using TMPro;
using System;

public class GameTime : NetworkBehaviour
{
    private const float InitialCountdownTime = 300f; // 5 minutes
    private float localTimer = 0f;

    [Networked]
    public float CountdownTime { get; set; }

    public TextMeshProUGUI CountdownText;

    private void Awake()
    {
        if (CountdownText == null)
            CountdownText = GameObject.Find("Time")?.GetComponent<TextMeshProUGUI>();
    }

    public override void Spawned()
    {
        Debug.Log("[GameTime] Spawned");

        if (Object.HasStateAuthority)
        {
            CountdownTime = InitialCountdownTime;
            Debug.Log("[GameTime] Countdown started at: " + CountdownTime);
        }

        // Initial update of the timer text
        UpdateTimerText(CountdownTime);
    }

    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority && CountdownTime > 0f)
        {
            localTimer += Runner.DeltaTime;

            if (localTimer >= 1f)
            {
                CountdownTime = Mathf.Max(CountdownTime - 1f, 0f);
                localTimer = 0f;

                Debug.Log("[GameTime] CountdownTime now: " + CountdownTime);
            }
        }

        // This will run on all clients to update the UI
        UpdateTimerText(CountdownTime);
    }

    private void UpdateTimerText(float timeValue)
    {
        if (CountdownText == null) return;

        // Format the time as mm:ss
        TimeSpan time = TimeSpan.FromSeconds(timeValue);
        string formattedTime = time.ToString(@"mm\:ss");

        // Update the text of the CountdownText UI element
        CountdownText.text = formattedTime;
    }
}
