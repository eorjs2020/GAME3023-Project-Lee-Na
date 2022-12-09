using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleMessageManager : Singleton<BattleMessageManager>
{
    [Header("Manager Properties")]
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private int numberOfTextLine = 3;
    private List<string> messages = new List<string>();
    private string displayText;

    private void Awake()
    {
        var obj = FindObjectsOfType<BattleMessageManager>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (messageText == null) messageText = GameObject.Find("BattleMessage").GetComponent<TextMeshProUGUI>();
        
        messages.Add("You met a wild Pokemon!");
        messages.Add("Pokemon wants to fight!");
        messages.Add("");

        UpdateMessageText();
    }

    public void UpdateMessageText()
    {
        displayText = "";
        foreach (string message in messages)
        {
            displayText += message + '\n';
        }
        messageText.text = displayText;
    }

    public void SendTextMessage(string message)
    {
        // Limited message line
        if (messages.Count > numberOfTextLine)
        {
            messages.RemoveAt(0);
            messages[0] = messages[1];
            messages[1] = messages[2];
            messages.Add(message);
        } else
        {
            messages.Add(message);
        }
        UpdateMessageText();
    }

    public TextMeshProUGUI GetMessageText() { return messageText; }
}
