using UnityEngine;
#if UNITY_IOS
using UnityEngine.iOS;
#endif
public class CrossPromo : MonoBehaviour {

    private void Start() {
        if (!PlayerData.instance.GetRemoveAdsStatus()) {
            gameObject.SetActive(false);
        }
    }

    public void Open() {
        string url;
#if UNITY_ANDROID
        url = "https://play.google.com/store/apps/details?id=com.korobka.merge.riders.DesertTruck";
        Application.OpenURL(url);
#elif UNITY_IOS
        Device.RequestStoreReview();
#endif

    }
}
