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
        textMessage = transform.Find("Canvas/Chat Box/MessageBox/Chat Message").GetComponent<TextMeshProUGUI>();
        inputFieldMessage = transform.Find("Canvas/Chat Box/InputBox/Input Message").GetComponent<TMP_InputField>();
        buttonSend = transform.Find("Canvas/Chat Box/InputBox/Button Send").GetComponent<Button>();
        chatPanel = transform.Find("Canvas/Chat Box").gameObject;
        toggleChatButton = transform.Find("Canvas/Open/Close Chat").GetComponent<Button>();

        buttonSend.onClick.AddListener(SendMessageChat);
        toggleChatButton.onClick.AddListener(ToggleChatPanel);

        Debug.Log("Chat system spawned và nút đã gán sự kiện.");
    }

    public void SendMessageChat()
    {
        var message = inputFieldMessage.text;
        Debug.Log("Message nhập vào: " + message);

        if (string.IsNullOrWhiteSpace(message)) return;

        var id = Runner.LocalPlayer.PlayerId;
        var text = $"Player {id}: {message}";

        RpcChat(text);

        inputFieldMessage.text = "";
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RpcChat(string msg)
    {
        Debug.Log("RpcChat nhận: " + msg);
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
