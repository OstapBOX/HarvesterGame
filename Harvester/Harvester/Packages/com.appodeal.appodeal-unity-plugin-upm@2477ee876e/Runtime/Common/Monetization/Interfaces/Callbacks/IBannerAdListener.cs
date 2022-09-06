﻿// ReSharper Disable CheckNamespace
namespace AppodealStack.Monetization.Common
{
    /// <summary>
    /// <para>
    /// Interface containing signatures of Appodeal Banner callback methods.
    /// </para>
    /// See <see href="https://wiki.appodeal.com/en/unity/get-started/ad-types/banner#id-[Development]UnitySDK.Banner-BannerCallbacks"/> for more details.
    /// </summary>
    public interface IBannerAdListener
    {
        /// <summary>
        /// <para>
        /// Fires when Banner is loaded.
        /// </para>
        /// See <see href="https://wiki.appodeal.com/en/unity/get-started/ad-types/banner#id-[Development]UnitySDK.Banner-BannerCallbacks"/> for more details.
        /// </summary>
        /// <param name="height">banner height returned from ad network.</param>
        /// <param name="isPrecache">true if loaded ad is precache, otherwise - false.</param>
        void OnBannerLoaded(int height, bool isPrecache);

        /// <summary>
        /// <para>
        /// Fires when Banner fails to load after passing the waterfall.
        /// </para>
        /// See <see href="https://wiki.appodeal.com/en/unity/get-started/ad-types/banner#id-[Development]UnitySDK.Banner-BannerCallbacks"/> for more details.
        /// </summary>
        /// <remarks>If auto cache is enabled, the next attempt to load ad will start automatically, after some delay.</remarks>
        void OnBannerFailedToLoad();

        /// <summary>
        /// <para>
        /// Fires a few seconds after Banner is displayed on the screen.
        /// </para>
        /// See <see href="https://wiki.appodeal.com/en/unity/get-started/ad-types/banner#id-[Development]UnitySDK.Banner-BannerCallbacks"/> for more details.
        /// </summary>
        void OnBannerShown();

        /// <summary>
        /// <para>
        /// Fires when attempt to show Banner fails for some reason.
        /// </para>
        /// See <see href="https://wiki.appodeal.com/en/unity/get-started/ad-types/banner#id-[Development]UnitySDK.Banner-BannerCallbacks"/> for more details.
        /// </summary>
        void OnBannerShowFailed();

        /// <summary>
        /// <para>
        /// Fires when user clicks on the Banner ad.
        /// </para>
        /// See <see href="https://wiki.appodeal.com/en/unity/get-started/ad-types/banner#id-[Development]UnitySDK.Banner-BannerCallbacks"/> for more details.
        /// </summary>
        void OnBannerClicked();

        /// <summary>
        /// <para>
        /// Fires when Banner expires and should not be used.
        /// </para>
        /// See <see href="https://wiki.appodeal.com/en/unity/get-started/ad-types/banner#id-[Development]UnitySDK.Banner-BannerCallbacks"/> for more details.
        /// </summary>
        /// <remarks>This callback won't be fired, unless you are loading and not showing ad creative for hours or even days.</remarks>
        void OnBannerExpired();
    }
}
