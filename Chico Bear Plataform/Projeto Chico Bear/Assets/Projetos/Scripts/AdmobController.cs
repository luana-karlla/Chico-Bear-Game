using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;

public class AdmobController : MonoBehaviour
{
    public static AdmobController instance;

    //private string appId = "ca-app-pub-1318035648739040~2827286810"; //ca-app-pub-1318035648739040~2302242470

    private BannerView bannerAD;
    private InterstitialAd interstitialAd;

    public Text txtInfo;
    public bool publishingApp = false; //quando nao quero mais usar as chaves de teste eu troco para true

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
     
    string appId = "ca-app-pub-1318035648739040~2827286810";
    //MobileAds.Initialize(appId);
    MobileAds.Initialize(initStatus => { });
        ResquestBanner();
        RequestInterstitial();
    }

    public void ResquestBanner()
    {
        string bannerID = "";

        if (publishingApp)
        {
            bannerID = "ca-app-pub-1318035648739040/2140891107";
        }
        else
        {
            //chave de teste
            bannerID = "coloque aqui a chave de teste";
        }
        AdSize adSize = new AdSize(300, 50);
        bannerAD = new BannerView(bannerID, adSize, AdPosition.Bottom);
        if (publishingApp)
        {
            AdRequest adRequest = new AdRequest.Builder().Build();
            bannerAD.LoadAd(adRequest);
        }
        else
        {
            AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
            bannerAD.LoadAd(adRequest);
            Debug.Log("Chave de Teste!");
        }

    }
    public void RemoveBanner()
    {
        bannerAD.Destroy();
    }

    // Update is called once per frame
    public void RequestInterstitial()
    {
        string interstitialID = "";

        if (publishingApp)
        {
            interstitialID = "ca-app-pub-1318035648739040/7711963745";
        }
        else
        {
            //chave de teste
            interstitialID = "coloque aqui a chave de teste";
        }

        interstitialAd = new InterstitialAd(interstitialID);

        interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        interstitialAd.OnAdOpening += HandleOnAdOpening;
        interstitialAd.OnAdClosed += HandleOnAdClosed;


        if (publishingApp)
        {
            AdRequest adRequest = new AdRequest.Builder().Build();
            interstitialAd.LoadAd(adRequest);
        }
        else
        {
            AdRequest adRequest = new AdRequest.Builder().AddTestDevice("2077ef9a63d2b398840261c8221a0c9b").Build();
            interstitialAd.LoadAd(adRequest);
            Debug.Log("Chave de Teste!");
        }
    }

    public void HandleOnAdClosed(object sender, EventArgs e)
    {
        interstitialAd.OnAdLoaded -= HandleOnAdLoaded;
        interstitialAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
        interstitialAd.OnAdOpening -= HandleOnAdOpening;
        interstitialAd.OnAdClosed -= HandleOnAdClosed;

        interstitialAd.Destroy();
        txtInfo.text = "Anúncio Fechado!";
    }

    public void HandleOnAdOpening(object sender, EventArgs e)
    {
        txtInfo.text = "Anúncio Aberto!";
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        txtInfo.text = "Falha ao carregar o anúncio! Erro:" + e.Message;
        RequestInterstitial();
    }

    public void HandleOnAdLoaded(object sender, EventArgs e)
    {
        txtInfo.text = "Anúncio foi carregado!";
        MostrarInterstitial();
    }

    public void MostrarInterstitial()
    {
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();

        }
    }
}

