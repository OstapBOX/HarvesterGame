using System;
using System.Diagnostics.CodeAnalysis;
using AppodealStack.ConsentManagement.Common;
using AppodealStack.ConsentManagement.Platforms;

// ReSharper disable CheckNamespace
namespace AppodealStack.ConsentManagement.Api
{
    /// <summary>
    /// <para>Consent Form Unity API for developers, including documentation.</para>
    /// See <see href="https://wiki.appodeal.com/en/unity/get-started/data-protection/gdpr-and-ccpa"/> for more details.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class ConsentForm
    {
        private readonly IConsentForm _consentForm;

        private IConsentForm GetNativeConsentForm()
        {
            return _consentForm;
        }

        private ConsentForm(IConsentFormListener listener)
        {
            _consentForm = ConsentManagerClientFactory.GetConsentForm(listener);
        }

        /// <summary>
        /// <para>Gets an instance of <see langword="ConsentForm"/> class.</para>
        /// See <see href="https://wiki.appodeal.com/en/unity/get-started/data-protection/gdpr-and-ccpa"/> for more details.
        /// </summary>
        /// <param name="listener">class which implements AppodealStack.ConsentManager.Common.IConsentFormListener interface.</param>
        /// <returns>Object of type <see langword="ConsentForm"/>.</returns>
        public static ConsentForm GetInstance(IConsentFormListener listener)
        {
            return new ConsentForm(listener);
        }

        /// <summary>
        /// <para>Loads the consent form data from server.</para>
        /// See <see href="https://wiki.appodeal.com/en/unity/get-started/data-protection/gdpr-and-ccpa"/> for more details.
        /// </summary>
        /// <remarks>Once loading has finished, either <see langword="OnConsentFormLoaded"/> or <see langword="OnConsentFormError"/> callback method will be triggered.</remarks>
        public void Load()
        {
            GetNativeConsentForm().Load();
        }

        /// <summary>
        /// <para>Shows the loaded consent form window.</para>
        /// See <see href="https://wiki.appodeal.com/en/unity/get-started/data-protection/gdpr-and-ccpa"/> for more details.
        /// </summary>
        public void Show()
        {
            GetNativeConsentForm().Show();
        }

        /// <summary>
        /// <para>Checks whether or not the consent form is loaded.</para>
        /// See <see href="https://wiki.appodeal.com/en/unity/get-started/data-protection/gdpr-and-ccpa"/> for more details.
        /// </summary>
        /// <returns>True if consent form is loaded, otherwise - false.</returns>
        public bool IsLoaded()
        {
            return GetNativeConsentForm().IsLoaded();
        }

        /// <summary>
        /// <para>Checks whether or not the consent form is currently displayed on the screen.</para>
        /// See <see href="https://wiki.appodeal.com/en/unity/get-started/data-protection/gdpr-and-ccpa"/> for more details.
        /// </summary>
        /// <returns>True if consent form window is active, otherwise - false.</returns>
        public bool IsShowing()
        {
            return GetNativeConsentForm().IsShowing();
        }

        #region Deprecated Methods

        [Obsolete("It will be removed in the next release. Use the Show() method instead.", false)]
        public void showAsActivity()
        {
            GetNativeConsentForm().Show();
        }

        [Obsolete("It will be removed in the next release. Use the Show() method instead.", false)]
        public void showAsDialog()
        {
            GetNativeConsentForm().Show();
        }

        [Obsolete("It will be removed in the next release. Use the capitalized version (Load) of this method instead.", false)]
        public void load()
        {
            GetNativeConsentForm().Load();
        }

        [Obsolete("It will be removed in the next release. Use the capitalized version (IsLoaded) of this method instead.", false)]
        public bool isLoaded()
        {
            return GetNativeConsentForm().IsLoaded();
        }

        [Obsolete("It will be removed in the next release. Use the capitalized version (IsShowing) of this method instead.", false)]
        public bool isShowing()
        {
            return GetNativeConsentForm().IsShowing();
        }

        #endregion

    }
}
