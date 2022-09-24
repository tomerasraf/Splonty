using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public class RegionIdentifier : MonoBehaviour
{
    public static RegionInfo region;

    private void Start()
    {
        //SetCurrentRegion();
    }
    public void SetCurrentRegion()
    {
        // Determine the country name if there are internet - by IP
        if (InternetCheckerController.isNoInternetConnection == false)
        {
            StartCoroutine(DetermineCountryByIP(
                () =>
                {
                    Debug.LogWarning("Fetched IP's country");
                },
                () =>
                {
                    region = RegionInfo.CurrentRegion;
                    Debug.LogWarning("Couldn't fetch the country IP, using system region");
                }));
        }
        // Otherwise by system
        else
        {
            region = RegionInfo.CurrentRegion;
        }
    }


    private IEnumerator DetermineCountryByIP(Action successCallback, Action failCallback)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://ipapi.co/country/");
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning(www.error);
            failCallback?.Invoke();
        }
        else
        {
            try
            {
                var info = www.downloadHandler.text;
                if (info != null)
                {
                    region = new RegionInfo(info);
                    if (region == null)
                    {
                        failCallback?.Invoke();
                        yield break;
                    }
                    successCallback?.Invoke();
                }                
                else
                {
                    failCallback?.Invoke();
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
                failCallback?.Invoke();
            }
        }
    }
    [System.Serializable]
    public class IpInfo
    {
        public string country { get; set; }
    }
    public RegionInfo GetCurrentRegion()
    {
        return region ?? RegionInfo.CurrentRegion;
    }
}