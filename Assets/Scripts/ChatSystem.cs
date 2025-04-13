using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatSystem : NetworkBehaviour
{
    public TextMeshProUGUI textMessage;
    public TMP_InputField inputFieldMessage;
    public GameObject buttonSend;

    public override void Spawned()
    {
       textMessage = GameObject.Find("Chat Message")
            .GetComponent<TextMeshProUGUI>();
        inputFieldMessage = GameObject.Find("Input Message")
            .GetComponent<TMP_InputField>();
        buttonSend = GameObject.Find("Button Send");    
        buttonSend.GetComponent<Button>()
            .onClick.AddListener(SendMessagChat);
    }
    
    public void SendMessagChat()
    {
        var message = inputFieldMessage.text;
        if (string.IsNullOrWhiteSpace(message)) return;
        var id = Runner.LocalPlayer.PlayerId;
        var text = $"Player {id}: {message}";
        RpcChat(text);
        inputFieldMessage.text = "";
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RpcChat(string msg)
    {
        textMessage.text += msg + "\n";
    }
}
