using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatSystem : NetworkBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI textMessage;
    public TMP_InputField inputFieldMessage;
    public Button buttonSend;
    public GameObject chatPanel;
    public Button toggleChatButton;

    private bool isChatOpen = true;

    public override void Spawned()
    {
        textMessage = GameObject.Find("ChatMessage").GetComponent<TextMeshProUGUI>();
        inputFieldMessage = GameObject.Find("Input Message").GetComponent<TMP_InputField>();
        buttonSend = GameObject.Find("Button Send").GetComponent<Button>();
        chatPanel = GameObject.Find("Chat Box");
        toggleChatButton = GameObject.Find("Open/Close Chat").GetComponent<Button>();

        buttonSend.onClick.AddListener(SendMessagChat);
        toggleChatButton.onClick.AddListener(ToggleChatPanel);
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

    public void ToggleChatPanel()
    {
        var anim = chatPanel.GetComponent<Animator>();

        if (anim == null)
        {
            Debug.LogWarning("Không tìm thấy Animator trên chatPanel!");
            return;
        }

        if (isChatOpen)
        {
            anim.SetTrigger("SlideOut");
        }
        else
        {
            anim.SetTrigger("SlideIn");
        }

        isChatOpen = !isChatOpen;
    }
}
