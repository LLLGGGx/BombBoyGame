using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour,IUnityAdsListener
{
#if UNITY_IOS
    private string gameID = "3985188";
#elif UNITY_ANDROID
    private string gameID = "3985189";
#endif
    Button adsButton;
    public string placementId = "rewardedVideo";
    void Start()
    {
        Debug.Log("1");
        adsButton = GetComponent<Button>();
        //adsButton.interactable = Advertisement.IsReady(placementId);
        if(adsButton)
        { 
            adsButton.onClick.AddListener(ShowRewardAds);
        }
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID,true);
    }

    public void ShowRewardAds()
    { 
        Advertisement.Show(placementId);
    }
    public void OnUnityAdsDidError(string message)
    {
       
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch(showResult)
        { 
            case ShowResult.Failed:
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Finished:
                Debug.Log("done");
                FindObjectOfType<PlayerController>().health = 3;
                FindObjectOfType<PlayerController>().isDead = false;
                UIManager.instance.UpdataHealth(FindObjectOfType<PlayerController>().health);
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsReady(string placementId)
    {   
        if(Advertisement.IsReady(placementId))
            Debug.Log("ads ready");
    }
}
