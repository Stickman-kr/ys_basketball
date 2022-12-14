using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using System.IO;
using System.Collections.Generic;


public class TCPReceive : MonoBehaviour
{
    TcpClient client;
    string serverIP = "127.0.0.1";
    int port = 9999;
    byte[] receivedBuffer;
    bool socketReady = false;
    public bool printToConsole = false;
    public string data;
    NetworkStream stream;
    StreamReader reader;
    
    void Start()
    {
        CheckReceive();
    }
    void Update()
    {
        if (socketReady)
        {
            if (stream.DataAvailable)
            {
                receivedBuffer = new byte[1024];
                stream.Read(receivedBuffer, 0, receivedBuffer.Length);
                data = Encoding.UTF8.GetString(receivedBuffer, 0, receivedBuffer.Length); // byte to string
                if(printToConsole) {print(data);}
            }
        }
    }

    void CheckReceive()
    {
        if (socketReady) return;
        try
        {
            client = new TcpClient(serverIP, port);
            if (client.Connected)
            {
                stream = client.GetStream();
                socketReady = true;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("error : " + e);
        }
    }
    void OnApplicationQuit()
    {
        if (!socketReady) return;

        reader.Close();
        client.Close();
        socketReady = false;
    }
}