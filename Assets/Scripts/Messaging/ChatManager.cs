using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using Photon.Pun;
using ExitGames.Client.Photon;
using TMPro;
using System;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    private ChatClient chatClient;
    public TMP_InputField username;
    public TMP_InputField matchname;
    public TMP_InputField message;
    //public TextMeshProUGUI msgArea;
    public GameObject msgArea2;
    public TextMeshProUGUI msgAreaa;

    public GameObject enterPanel;
    public GameObject msgPanel;

    public void GetConnected()
    {
        chatClient = new ChatClient(this);
        chatClient.ChatRegion = "ASIA";
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion,
            new Photon.Chat.AuthenticationValues(username.text));
    }


    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnChatStateChange(ChatState state)
    {
        
    }

    public void OnConnected()
    {
        enterPanel.SetActive(false);
        msgPanel.SetActive(true);
        chatClient.SetOnlineStatus(ChatUserStatus.Online);
    }

    public void OnDisconnected()
    {
        
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        //Console.WriteLine("OnPrivateMessage: {0} ({1}) > {2}", channelName, sender, message);

        //msgArea.text += sender + ": " + message + "\n";
        msgAreaa.text += sender + ": " + message + "\n";
    }

    public void SendMsg()
    {
        chatClient.SendPrivateMessage(matchname.text, message.text);
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
       
    }

    public void OnUnsubscribed(string[] channels)
    {
        
    }

    public void OnUserSubscribed(string channel, string user)
    {
        
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (chatClient != null)
        {
            chatClient.Service();
        }
    }
}
