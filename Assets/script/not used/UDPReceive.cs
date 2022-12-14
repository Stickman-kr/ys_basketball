using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive : MonoBehaviour
{
    Thread receiveThread;
    UdpClient client;
    public int port = 9999;
    public bool startReceiving = true;
    public bool printToConsole = false;
    public string data;

    public void Start() {
        receiveThread = new Thread(
            new ThreadStart(Receivedata)
        );
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    //receie thread
    private void Receivedata(){
        client = new UdpClient(port);
        while(startReceiving){
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any,0);
                byte[] dataByte = client.Receive(ref anyIP);
                data = Encoding.UTF8.GetString(dataByte);

                if(printToConsole) {print(data);}
            }
            catch (System.Exception err) 
            {
                
                print(err.ToString());
            }
        }
    }
    
}
