using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button player1;
    public Button player2;
    public Button player3;
    public Button player4;
    public Button backButton; // <-- Nút quay về menu

    void Start()
    {
        player1.onClick.AddListener(() => OnButtonClick("p1"));
        player2.onClick.AddListener(() => OnButtonClick("p2"));
        player3.onClick.AddListener(() => OnButtonClick("p3"));
        player4.onClick.AddListener(() => OnButtonClick("p4"));
        backButton.onClick.AddListener(BackToMenu); // <-- Gán sự kiện nút
    }

    void OnButtonClick(string playerClass)
    {
        var playerName = nameInputField.text;
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetString("PlayerClass", playerClass);
        SceneManager.LoadScene("Lv1");
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("Menu"); // <-- Chuyển về scene Menu
    }
}
