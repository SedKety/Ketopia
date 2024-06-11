using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class IpGrabber : MonoBehaviour
{
    public string hostname;
    public string ip;
    [System.Obsolete]
    public void Start()
    {
        hostname = Dns.GetHostName();
        ip = Dns.GetHostByName(hostname).AddressList[0].ToString();
        print(hostname);
        print(ip);
    }
}
