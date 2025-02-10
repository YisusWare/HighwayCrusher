using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{

    [SerializeField] private string androidAdId;
    // Start is called before the first frame update
    private void Awake()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }

    public void LoadBannerAd()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = BannerLoaded,
            errorCallback = BannerLoadedError
        };

        Advertisement.Banner.Load(androidAdId,options);
    }

    private void BannerLoadedError(string message)
    {
        throw new NotImplementedException();
    }

    private void BannerLoaded()
    {
        Debug.Log("Banner shown");
    }

    public void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions
        {
            showCallback = BannerShow,
            clickCallback = BannerClicked,
            hideCallback = BannerHidden
        };

        Advertisement.Banner.Show(androidAdId, options);
    }

    private void BannerHidden()
    {
        throw new NotImplementedException();
    }

    private void BannerClicked()
    {
        Debug.Log("Banner clicked");
    }

    private void BannerShow()
    {
        Debug.Log("Banner shown");
    }
}
