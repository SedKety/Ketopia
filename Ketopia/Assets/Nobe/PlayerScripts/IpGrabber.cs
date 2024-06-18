using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class IpGrabber : MonoBehaviour
{
    public string ip;
    [System.Obsolete]

    public void Start()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            print(ip.ToString());
        }
            // Get the Name of HOST   
            string hostName = Dns.GetHostName();
            Console.WriteLine(hostName);

            // Get the IP from GetHostByName method of dns class. 
            string IP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            Console.WriteLine("IP Address is : " + IP);
    }
}
