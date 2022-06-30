using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyButton : MonoBehaviour {

    public enum ItemType {
        smallCoinsPack,
        smallDollarsPack,
        smallFuelPack,
        largeCoinsPack,
        largeDollarsPack,
        largeFuelPack,
        resourcesPack,
        removeAds,
        smallDashPack,
        smallShieldPack,
        smallCultivatorPack,
        largeDashPack,
        largeShieldPack,
        largeCultivatorPack,
        bonusesPacks
    }

    public ItemType itemType;
    //public TextMeshProUGUI priceText;
    private string defaultText;
    // Start is called before the first frame update
    void Start() {
        //defaultText = priceText.text;
        //StartCoroutine(LoadPriceRoutine());
    }
    public void ClickBuy() {
        switch (itemType) {
            case ItemType.smallCoinsPack:
                IAPManager.Instance.BuySmallCoinsPack();
                break;
            case ItemType.smallDollarsPack:
                IAPManager.Instance.BuySmallDollarsPack();
                break;
            case ItemType.smallFuelPack:
                IAPManager.Instance.BuySmallFuelPack();
                break;
            case ItemType.largeCoinsPack:
                IAPManager.Instance.BuyLargeCoinsPack();
                break;
            case ItemType.largeDollarsPack:
                IAPManager.Instance.BuyLargeDollarsPack();
                break;
            case ItemType.largeFuelPack:
                IAPManager.Instance.BuyLargeFuelPack();
                break;
            case ItemType.resourcesPack:
                IAPManager.Instance.BuyResourcesPack();
                break;
            case ItemType.smallDashPack:
                IAPManager.Instance.BuySmallDashPack();
                break;
            case ItemType.smallShieldPack:
                IAPManager.Instance.BuySmallShieldPack();
                break;
            case ItemType.smallCultivatorPack:
                IAPManager.Instance.BuySmallCultivatorPack();
                break;
            case ItemType.largeDashPack:
                IAPManager.Instance.BuyLargeDashPack();
                break;
            case ItemType.largeShieldPack:
                IAPManager.Instance.BuyLargeShieldPack();
                break;
            case ItemType.largeCultivatorPack:
                IAPManager.Instance.BuyLargeCultivatorPack();
                break;
            case ItemType.bonusesPacks:
                IAPManager.Instance.BuyBonusesPack();
                break;
            case ItemType.removeAds:
                IAPManager.Instance.BuyNoADS();
                break;

        }
    }

    //private IEnumerator LoadPriceRoutine() {
    //    while (!IAPManager.Instance.IsInitialized())
    //        yield return null;

    //    string loadPrice = "";

    //    switch (itemType) {
    //        case ItemType.Gold50:
    //            loadPrice = IAPManager.Instance.GetProductePriceFromStore(IAPManager.Instance.GOLD_50);
    //            break;
    //        case ItemType.Gold100:
    //            loadPrice = IAPManager.Instance.GetProductePriceFromStore(IAPManager.Instance.GOLD_100);
    //            break;
    //        case ItemType.NoAds:
    //            loadPrice = IAPManager.Instance.GetProductePriceFromStore(IAPManager.Instance.NO_ADS);
    //            break;

    //    }

    //    priceText.text = defaultText + " " + loadPrice;
    //}
}

