using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using CompleteProject;
using UnityEngine.Purchasing.Extension;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

namespace CompleteProject
{
    public class Purchaser : MonoBehaviour, IDetailedStoreListener
    {
        private static IStoreController m_StoreController;          // The Unity Purchasing system.
        private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

        public static string kProductIDConsumableCoins20 = "coins_20";
        public static string kProductIDConsumableCoins100 = "coins100";
        public static string kProductIDConsumableCoins350 = "coins350";
        public static string kProductIDNonConsumable = "nonconsumable";
        public static string kProductIDSubscription = "subscription";
        public static string ikProductIDConsumableCoins20 = "SummOn20";
        public static string ikProductIDConsumableCoins100 = "SummOn100";
        public static string ikProductIDConsumableCoins350 = "SummOn350";
        public static string ikProductIDNonConsumable = "nonconsumable";
        public static string ikProductIDSubscription = "subscription";

        private Skins sk;

        void Start()
        {
            sk = GameObject.Find("RawImageSkins").GetComponent<Skins>();
            InitializeUnityGamingServices();
        }

        private async void InitializeUnityGamingServices()
        {
            try
            {
                var options = new InitializationOptions().SetEnvironmentName("production");
                await UnityServices.InitializeAsync(options);
                Debug.Log("Unity Gaming Services initialized successfully.");

                // If we haven't set up the Unity Purchasing reference
                if (m_StoreController == null)
                {
                    // Begin to configure our connection to Purchasing
                    InitializePurchasing();
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to initialize Unity Gaming Services: {e.Message}");
            }
        }

        public void InitializePurchasing()
        {
            if (IsInitialized())
            {
                return;
            }

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            if (Application.platform == RuntimePlatform.Android)
            {
                builder.AddProduct(kProductIDConsumableCoins20, ProductType.Consumable);
                builder.AddProduct(kProductIDConsumableCoins100, ProductType.Consumable);
                builder.AddProduct(kProductIDConsumableCoins350, ProductType.Consumable);
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                builder.AddProduct(ikProductIDConsumableCoins20, ProductType.Consumable);
                builder.AddProduct(ikProductIDConsumableCoins100, ProductType.Consumable);
                builder.AddProduct(ikProductIDConsumableCoins350, ProductType.Consumable);
            }

            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                builder.AddProduct(kProductIDConsumableCoins20, ProductType.Consumable);
                builder.AddProduct(kProductIDConsumableCoins100, ProductType.Consumable);
                builder.AddProduct(kProductIDConsumableCoins350, ProductType.Consumable);
            }

            UnityPurchasing.Initialize(this, builder);
        }

        private bool IsInitialized()
        {
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }

        public void BuyConsumableCoins20()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                BuyProductID(kProductIDConsumableCoins20);
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                BuyProductID(ikProductIDConsumableCoins20);
            }
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                BuyProductID(kProductIDConsumableCoins20);
            }
        }

        public void BuyConsumableCoins100()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                BuyProductID(kProductIDConsumableCoins100);
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                BuyProductID(ikProductIDConsumableCoins100);
            }
        }

        public void BuyConsumableCoins350()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                BuyProductID(kProductIDConsumableCoins350);
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                BuyProductID(ikProductIDConsumableCoins350);
            }
        }

        public void BuyNonConsumable()
        {
            BuyProductID(kProductIDNonConsumable);
        }

        public void BuySubscription()
        {
            BuyProductID(kProductIDSubscription);
        }

        void BuyProductID(string productId)
        {
            if (IsInitialized())
            {
                Product product = m_StoreController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    SkinManager.instance.SetDebugToShow(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    m_StoreController.InitiatePurchase(product);
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                    SkinManager.instance.SetDebugToShow("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
                SkinManager.instance.SetDebugToShow("BuyProductID FAIL. Not initialized.");
            }
        }

        public void RestorePurchases()
        {
            if (!IsInitialized())
            {
                Debug.Log("RestorePurchases FAIL. Not initialized.");
                SkinManager.instance.SetDebugToShow("RestorePurchases FAIL. Not initialized.");
                return;
            }

            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                Debug.Log("RestorePurchases started ...");
                SkinManager.instance.SetDebugToShow("RestorePurchases started ...");

                var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
                apple.RestoreTransactions((result, message) =>
                {
                    Debug.Log("RestorePurchases continuing: " + result + ". Message: " + message + ". If no further messages, no purchases available to restore.");
                    SkinManager.instance.SetDebugToShow("RestorePurchases continuing: " + result + ". Message: " + message + ". If no further messages, no purchases available to restore.");
                });
            }
            else
            {
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
                SkinManager.instance.SetDebugToShow("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            }
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("OnInitialized: PASS");
            SkinManager.instance.SetDebugToShow("OnInitialized: PASS");

            m_StoreController = controller;
            m_StoreExtensionProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
            SkinManager.instance.SetDebugToShow("OnInitializeFailed InitializationFailureReason:" + error);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.Log($"OnInitializeFailed InitializationFailureReason: {error}, Message: {message}");
            SkinManager.instance.SetDebugToShow($"OnInitializeFailed InitializationFailureReason: {error}, Message: {message}");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumableCoins20, StringComparison.Ordinal))
                {
                    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    int newCash;
                    newCash = SkinManager.instance.CurrentCash + 20;
                    SkinManager.instance.SetCurrentCash(newCash);
                    PlayerPrefs.SetInt("CurrentCash", newCash);
                    SkinManager.instance.SetDebugToShow(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    SkinManager.instance.SetAIPToShow(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    sk.BackPurchase();
                }
            }
            if (Application.platform == RuntimePlatform.Android)
            {
                if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumableCoins20, StringComparison.Ordinal))
                {
                    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    int newCash;
                    newCash = SkinManager.instance.CurrentCash + 20;
                    SkinManager.instance.SetCurrentCash(newCash);
                    PlayerPrefs.SetInt("CurrentCash", newCash);
                    SkinManager.instance.SetDebugToShow(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    SkinManager.instance.SetAIPToShow(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    sk.BackPurchase();
                }
                else if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumableCoins100, StringComparison.Ordinal))
                {
                    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    int newCash;
                    newCash = SkinManager.instance.CurrentCash + 100;
                    SkinManager.instance.SetCurrentCash(newCash);
                    PlayerPrefs.SetInt("CurrentCash", newCash);
                    SkinManager.instance.SetDebugToShow(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    SkinManager.instance.SetAIPToShow(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    sk.BackPurchase();
                }
                else if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumableCoins350, StringComparison.Ordinal))
                {
                    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    int newCash;
                    newCash = SkinManager.instance.CurrentCash + 350;
                    SkinManager.instance.SetCurrentCash(newCash);
                    PlayerPrefs.SetInt("CurrentCash", newCash);
                    SkinManager.instance.SetDebugToShow(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    SkinManager.instance.SetAIPToShow(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    sk.BackPurchase();
                }
                else if (String.Equals(args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal))
                {
                    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                }
                else if (String.Equals(args.purchasedProduct.definition.id, kProductIDSubscription, StringComparison.Ordinal))
                {
                    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                }
                else
                {
                    Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
                    SkinManager.instance.SetDebugToShow(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
                    SkinManager.instance.SetAIPToShow(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
                }
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (String.Equals(args.purchasedProduct.definition.id, ikProductIDConsumableCoins20, StringComparison.Ordinal))
                {
                    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    int newCash;
                    newCash = SkinManager.instance.CurrentCash + 20;
                    SkinManager.instance.SetCurrentCash(newCash);
                    PlayerPrefs.SetInt("CurrentCash", newCash);
                    SkinManager.instance.SetDebugToShow(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    SkinManager.instance.SetAIPToShow(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    sk.BackPurchase();
                }
                else if (String.Equals(args.purchasedProduct.definition.id, ikProductIDConsumableCoins100, StringComparison.Ordinal))
                {
                    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    int newCash;
                    newCash = SkinManager.instance.CurrentCash + 100;
                    SkinManager.instance.SetCurrentCash(newCash);
                    PlayerPrefs.SetInt("CurrentCash", newCash);
                    SkinManager.instance.SetDebugToShow(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    SkinManager.instance.SetAIPToShow(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    sk.BackPurchase();
                }
                else if (String.Equals(args.purchasedProduct.definition.id, ikProductIDConsumableCoins350, StringComparison.Ordinal))
                {
                    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    int newCash;
                    newCash = SkinManager.instance.CurrentCash + 350;
                    SkinManager.instance.SetCurrentCash(newCash);
                    PlayerPrefs.SetInt("CurrentCash", newCash);
                    SkinManager.instance.SetDebugToShow(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    SkinManager.instance.SetAIPToShow(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                    sk.BackPurchase();
                }
                else if (String.Equals(args.purchasedProduct.definition.id, ikProductIDNonConsumable, StringComparison.Ordinal))
                {
                    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                }
                else if (String.Equals(args.purchasedProduct.definition.id, ikProductIDSubscription, StringComparison.Ordinal))
                {
                    Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                }
                else
                {
                    Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
                    SkinManager.instance.SetDebugToShow(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
                    SkinManager.instance.SetAIPToShow(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
                }
            }

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
            SkinManager.instance.SetDebugToShow(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
            SkinManager.instance.SetAIPToShow(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}, Message: {2}", product.definition.storeSpecificId, failureDescription.reason, failureDescription.message));
            SkinManager.instance.SetDebugToShow(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}, Message: {2}", product.definition.storeSpecificId, failureDescription.reason, failureDescription.message));
            SkinManager.instance.SetAIPToShow(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}, Message: {2}", product.definition.storeSpecificId, failureDescription.reason, failureDescription.message));
        }
    }
}

