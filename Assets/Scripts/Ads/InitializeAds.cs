using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string androidGameId;
    [SerializeField] private bool isTesting;

    public void OnInitializationComplete()
    {
        
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError("Ads initialization failed");
    }

    private void Awake()
    {
        if(!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(androidGameId,isTesting, this);
        }
    }
}
