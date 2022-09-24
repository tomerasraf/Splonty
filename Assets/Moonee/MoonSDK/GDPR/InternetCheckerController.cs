using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class InternetCheckerController : MonoBehaviour
{
    public static bool isNoInternetConnection;
    public static event Action OnNoInternet;
    public RegionIdentifier regionIdentifier;

    private void Start()
    {
        OnNoInternet += () => isNoInternetConnection = true;
        OnNoInternet += () => Debug.LogError("There is no internet connection");
        
        CheckInternetConnection(() => isNoInternetConnection = false, OnNoInternet);
        
    }

    public void CheckInternetConnection(Action successCallback = null, Action noInternetCallback = null)
    {
#if !UNITY_EDITOR
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            noInternetCallback?.Invoke();
            return;
        }
#endif
        StartCoroutine(CheckInternetRoutine(successCallback, noInternetCallback));
    }
    
    private IEnumerator CheckInternetRoutine(Action successCallback = null, Action noInternetCallback = null)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get("http://google.com");
        yield return webRequest.SendWebRequest();
        if (webRequest.result != UnityWebRequest.Result.Success || webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            noInternetCallback?.Invoke();
            regionIdentifier.SetCurrentRegion();
            yield break;
        }
        regionIdentifier.SetCurrentRegion();
        successCallback?.Invoke();
    }
}
