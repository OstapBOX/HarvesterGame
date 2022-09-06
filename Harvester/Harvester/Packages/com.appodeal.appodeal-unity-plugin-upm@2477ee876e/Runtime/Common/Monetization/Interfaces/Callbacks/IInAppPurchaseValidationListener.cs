// ReSharper Disable CheckNamespace
namespace AppodealStack.Monetization.Common
{
    /// <summary>
    /// <para>
    /// Interface containing signatures of Appodeal IAP Validation callback methods.
    /// </para>
    /// See <see href=""/> for more details.
    /// </summary>
    public interface IInAppPurchaseValidationListener
    {
        /// <summary>
        /// <para>
        /// Fires when In-App purchase is successfully validated.
        /// </para>
        /// See <see href=""/> for more details.
        /// </summary>
        /// <param name="json">json-formatted string containing purchase data and errors, if any.</param>
        void OnInAppPurchaseValidationSucceeded(string json);

        /// <summary>
        /// <para>
        /// Fires when In-App purchase validation fails.
        /// </para>
        /// See <see href=""/> for more details.
        /// </summary>
        /// <param name="json">json-formatted string containing errors.</param>
        void OnInAppPurchaseValidationFailed(string json);
    }
}
