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
    private string messageSender;


    private void Start()
    {
        if (messageText == null) messageText = GameObject.Find("BattleMessage").GetComponent<TextMeshProUGUI>();
        
        messages.Add("You met a wild Pokemon!");
        messages.Add("A wild Pokemon wants to fight!");
        messages.Add("");

        UpdateMessageText();
    }

    public void UpdateMessageText()
    {
        displayText = "";
        foreach (string message in messages)
        {
            if (message == "") continue;

            displayText += message + '\n';
        }
        messageText.text = displayText;
    }

    public void SendTextMessage(string message, string sender)
    {
        // Different message sender. Delete previous messages
        Debug.Log($"Message Sender : {sender}, Message = {message}");
        if (sender != messageSender)
        {
            Debug.Log($"Removed Message, Message Sender : {sender}, Message = {message}");
            messages.RemoveRange(0, messages.Count);
            messageSender = sender;
        }

        // Limited message line
        if (messages.Count >= numberOfTextLine)
        {
            messages[0] = messages[1];
            messages[1] = messages[2];
            messages[2] = message;
        }
        else
        {
            messages.Add(message);
        }

        UpdateMessageText();
    }

    public TextMeshProUGUI GetMessageText() { return messageText; }
}
