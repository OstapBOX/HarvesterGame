using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;

public class IAPManager : Singleton<IAPManager>, IStoreListener, IInAppPurchaseValidationListener {
	private static IStoreController m_StoreController;
	private static IExtensionProvider m_StoreExtensionProvider;

	//Resources
	public string smallCoinsPack = "korobka.small_coins_pack";
	public string smallDollarsPack = "korobka.small_dollars_pack";
	public string smallFuelPack = "korobka.small_fuel_pack";
	public string largeCoinsPack = "korobka.large_coins_pack";
	public string largeDollarsPack = "korobka.large_dollars_pack";
	public string largeFuelPack = "korobka.large_fuel_pack";
	public string resourcesPack = "korobka.resources_pack";
	public string removeAds = "korobka.remove_ads_pack";

	//Bonuses
	public string smallDashPack = "korobka.small_dash_pack";
	public string smallShieldPack = "korobka.small_shield_pack";
	public string smallCultivatorPack = "korobka.small_cultivator_pack";
	public string largeDashPack = "korobka.large_dash_pack";
	public string largeShieldPack = "korobka.large_shield_pack";
	public string largeCultivatorPack = "korobka.large_cultivator_pack";
	public string bonusesPack = "korobka.bonuses_pack";

	//Other
	[SerializeField] private EnergyManager energyManager;
	[SerializeField] private StatisticBar statisticBar;
	[SerializeField] private PowerUpsAmount powerUpsAmount;
	 private BannerAd bannerAd;

	[SerializeField] private AudioClip successes;
	[SerializeField] private AudioClip reject;

	void Start() {
		// If we haven't set up the Unity Purchasing reference
		if (m_StoreController == null) {
			// Begin to configure our connection to Purchasing
			InitializePurchasing();
		}
	}

	public void InitializePurchasing() {
		// If we have already connected to Purchasing ...
		if (IsInitialized()) {
			// ... we are done here.
			return;
		}


		// Create a builder, first passing in a suite of Unity provided stores.
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());


		builder.AddProduct(smallCoinsPack, ProductType.Consumable);
		builder.AddProduct(smallDollarsPack, ProductType.Consumable);
		builder.AddProduct(smallFuelPack, ProductType.Consumable);
		builder.AddProduct(largeCoinsPack, ProductType.Consumable);
		builder.AddProduct(largeDollarsPack, ProductType.Consumable);
		builder.AddProduct(largeFuelPack, ProductType.Consumable);
		builder.AddProduct(resourcesPack, ProductType.Consumable);
		builder.AddProduct(smallDashPack, ProductType.Consumable);
		builder.AddProduct(smallShieldPack, ProductType.Consumable);
		builder.AddProduct(smallCultivatorPack, ProductType.Consumable);
		builder.AddProduct(largeDashPack, ProductType.Consumable);
		builder.AddProduct(largeShieldPack, ProductType.Consumable);
		builder.AddProduct(largeCultivatorPack, ProductType.Consumable);
		builder.AddProduct(bonusesPack, ProductType.Consumable);

		// Continue adding the non-consumable product.
		builder.AddProduct(removeAds, ProductType.NonConsumable);

		// Kick off the remainder of the set-up with an asynchrounous call, passing the configuration and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
		UnityPurchasing.Initialize(this, builder);
	}


	public bool IsInitialized() {
		// Only say we are initialized if both the Purchasing references are set.
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}

	public void BuySmallCoinsPack() {
		BuyProductID(smallCoinsPack);
	}

	public void BuySmallDollarsPack() {
		BuyProductID(smallDollarsPack);
	}

	public void BuySmallFuelPack() {
		BuyProductID(smallFuelPack);
	}

	public void BuyLargeCoinsPack() {
		BuyProductID(largeCoinsPack);
	}

	public void BuyLargeDollarsPack() {
		BuyProductID(largeDollarsPack);
	}

	public void BuyLargeFuelPack() {
		BuyProductID(largeFuelPack);
	}

	public void BuyResourcesPack() {
		BuyProductID(resourcesPack);
	}

	public void BuySmallDashPack() {
		BuyProductID(smallDashPack);
	}

	public void BuySmallShieldPack() {
		BuyProductID(smallShieldPack);
	}

	public void BuySmallCultivatorPack() {
		BuyProductID(smallCultivatorPack);
	}

	public void BuyLargeDashPack() {
		BuyProductID(largeDashPack);
	}

	public void BuyLargeShieldPack() {
		BuyProductID(largeShieldPack);
	}

	public void BuyLargeCultivatorPack() {
		BuyProductID(largeCultivatorPack);
	}

	public void BuyBonusesPack() {
		BuyProductID(bonusesPack);
	}

	public void BuyNoADS() {
		BuyProductID(removeAds);
	}

	public string GetProductePriceFromStore(string id) {
		if (m_StoreController != null && m_StoreController.products != null) {
			return m_StoreController.products.WithID(id).metadata.localizedPriceString;
		}
		else {
			return "";
		}
	}

	public void BuyProductID(string productId) {
		// If the stores throw an unexpected exception, use try..catch to protect my logic here.
		try {
			// If Purchasing has been initialized ...
			if (IsInitialized()) {
				// ... look up the Product reference with the general product identifier and the Purchasing system's products collection.
				Product product = m_StoreController.products.WithID(productId);

                // If the look up found a product for this device's store and that product is ready to be sold ... 
                if (product != null && product.availableToPurchase) {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}' - '{1}'", product.definition.id, product.definition.storeSpecificId));// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
                    m_StoreController.InitiatePurchase(product);

#if UNITY_ANDROID
					var purchase = new PlayStoreInAppPurchase.Builder(PlayStorePurchaseType.InApp)
						.WithCurrency("USD")
						.WithOrderId(product.definition.id)
						.WithPrice(GetProductePriceFromStore(product.definition.id))
						.Build();

					Appodeal.ValidatePlayStoreInAppPurchase(purchase, this);
#elif UNITY_IOS
            var additionalParams = new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } };

            var purchase = new AppStoreInAppPurchase.Builder(AppStorePurchaseType.Consumable)
                .WithAdditionalParameters(additionalParams)
                .WithTransactionId("transactionId")
                .WithProductId("productId")
                .WithCurrency("USD")
                .WithPrice("2.89")
                .Build();

            Appodeal.ValidateAppStoreInAppPurchase(purchase, this);
#endif
				}
				// Otherwise ...
				else {
					// ... report the product look-up failure situation  
					Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				}
			}
			// Otherwise ...
			else {
				// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or retrying initiailization.
				Debug.Log("BuyProductID FAIL. Not initialized.");
			}
		}
		// Complete the unexpected exception handling ...
		catch (Exception e) {
			// ... by reporting any unexpected exception for later diagnosis.
			Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e);
		}
	}


	// Restore purchases previously made by this customer. Some platforms automatically restore purchases. Apple currently requires explicit purchase restoration for IAP.
	public void RestorePurchases() {
		// If Purchasing has not yet been set up ...
		if (!IsInitialized()) {
			// ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}

		// If we are running on an Apple device ... 
		if (Application.platform == RuntimePlatform.IPhonePlayer ||
			Application.platform == RuntimePlatform.OSXPlayer) {
			// ... begin restoring purchases
			Debug.Log("RestorePurchases started ...");

			// Fetch the Apple store-specific subsystem.
			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			// Begin the asynchronous process of restoring purchases. Expect a confirmation response in the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
			apple.RestoreTransactions((result) => {
				// The first phase of restoration. If no more responses are received on ProcessPurchase then no purchases are available to be restored.
				Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}
		// Otherwise ...
		else {
			// We are not running on an Apple device. No work is necessary to restore purchases.
			Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}


	//  
	// --- IStoreListener
	//

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log("OnInitialized: PASS");

		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;
	}


	public void OnInitializeFailed(InitializationFailureReason error) {
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}


	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) {
		if (String.Equals(args.purchasedProduct.definition.id, smallCoinsPack, StringComparison.Ordinal)) {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

			PlayerData.instance.ChangeCoinsAmount(4000);
			statisticBar.UpdateStatisticBar();
			SoundManager.instance.PlaySound(successes);
		}
		else if (String.Equals(args.purchasedProduct.definition.id, smallDollarsPack, StringComparison.Ordinal)) {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

			PlayerData.instance.ChangeDollarsAmount(20);
			statisticBar.UpdateStatisticBar();
			SoundManager.instance.PlaySound(successes);
		}
		else if (String.Equals(args.purchasedProduct.definition.id, smallFuelPack, StringComparison.Ordinal)) {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

			energyManager.ChangeEnergyAmount(25);
			statisticBar.UpdateStatisticBar();
			SoundManager.instance.PlaySound(successes);
		}
		else if (String.Equals(args.purchasedProduct.definition.id, largeCoinsPack, StringComparison.Ordinal)) {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

			PlayerData.instance.ChangeCoinsAmount(9500);
			statisticBar.UpdateStatisticBar();
			SoundManager.instance.PlaySound(successes);
		}
		else if (String.Equals(args.purchasedProduct.definition.id, largeDollarsPack, StringComparison.Ordinal)) {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

			PlayerData.instance.ChangeDollarsAmount(45);
			statisticBar.UpdateStatisticBar();
			SoundManager.instance.PlaySound(successes);
		}
		else if (String.Equals(args.purchasedProduct.definition.id, largeFuelPack, StringComparison.Ordinal)) {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

			energyManager.ChangeEnergyAmount(55);
			powerUpsAmount.UpdatePowerUpsAmount();
			SoundManager.instance.PlaySound(successes);
		}
		else if (String.Equals(args.purchasedProduct.definition.id, resourcesPack, StringComparison.Ordinal)) {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

			PlayerData.instance.ChangeCoinsAmount(5000);
			PlayerData.instance.ChangeDollarsAmount(40);
			energyManager.ChangeEnergyAmount(25);

			statisticBar.UpdateStatisticBar();
			SoundManager.instance.PlaySound(successes);
		}
		else if (String.Equals(args.purchasedProduct.definition.id, smallDashPack, StringComparison.Ordinal)) {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

			PlayerData.instance.ChangeSpeedUpAmount(30);
			powerUpsAmount.UpdatePowerUpsAmount();
			SoundManager.instance.PlaySound(successes);
		}
		else if (String.Equals(args.purchasedProduct.definition.id, smallShieldPack, StringComparison.Ordinal)) {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

			PlayerData.instance.ChangeShieldAmount(25);
			powerUpsAmount.UpdatePowerUpsAmount();
			SoundManager.instance.PlaySound(successes);
		}
		else if (String.Equals(args.purchasedProduct.definition.id, smallCultivatorPack, StringComparison.Ordinal)) {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

			PlayerData.instance.ChangeCultivatorAmount(20);
			powerUpsAmount.UpdatePowerUpsAmount();
			SoundManager.instance.PlaySound(successes);
		}
		else if (String.Equals(args.purchasedProduct.definition.id, largeDashPack, StringComparison.Ordinal)) {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

			PlayerData.instance.ChangeSpeedUpAmount(65);
			powerUpsAmount.UpdatePowerUpsAmount();
			SoundManager.instance.PlaySound(successes);
		}
		else if (String.Equals(args.purchasedProduct.definition.id, largeShieldPack, StringComparison.Ordinal)) {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

			PlayerData.instance.ChangeShieldAmount(55);
			powerUpsAmount.UpdatePowerUpsAmount();
			SoundManager.instance.PlaySound(successes);
		}
		else if (String.Equals(args.purchasedProduct.definition.id, largeCultivatorPack, StringComparison.Ordinal)) {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

			PlayerData.instance.ChangeCultivatorAmount(45);
			powerUpsAmount.UpdatePowerUpsAmount();
			SoundManager.instance.PlaySound(successes);
		}
		else if (String.Equals(args.purchasedProduct.definition.id, bonusesPack, StringComparison.Ordinal)) {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

			PlayerData.instance.ChangeSpeedUpAmount(50);
			PlayerData.instance.ChangeShieldAmount(40);
			PlayerData.instance.ChangeCultivatorAmount(30);
			powerUpsAmount.UpdatePowerUpsAmount();
			SoundManager.instance.PlaySound(successes);
		}
		else if (String.Equals(args.purchasedProduct.definition.id, removeAds, StringComparison.Ordinal)) {
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

			PlayerPrefs.SetInt("RemoveAds", 1);
			bannerAd = GameObject.Find("AdManager").GetComponent<BannerAd>();
			bannerAd.enabled = false;
			SoundManager.instance.PlaySound(successes);


		}// Or ... an unknown product has been purchased by this user. Fill in additional products here.
		else {
			SoundManager.instance.PlaySound(reject);
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}// Return a flag indicating wither this product has completely been received, or if the application needs to be reminded of this purchase at next app launch. Is useful when saving purchased products to the cloud, and when that save is delayed.
		return PurchaseProcessingResult.Complete;
	}


	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) {
		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing this reason with the user.
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}

    public void OnInAppPurchaseValidationSucceeded(string json) {
		Debug.Log("AppodealPurchaseValidationSucceeded");
    }

    public void OnInAppPurchaseValidationFailed(string json) {
		Debug.Log("AppodealPurchaseValidationFailed");
	}

    public void OnInitializeFailed(InitializationFailureReason error, string message) {
        throw new NotImplementedException();
    }
}
