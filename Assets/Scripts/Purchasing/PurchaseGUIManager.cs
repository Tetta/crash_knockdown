using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseGUIManager : MonoBehaviour
{
    private static PurchaseManager _purchaseManager;

    public static PurchaseManager purchaseManager
    {
        get
        {
            if (!_purchaseManager)
                _purchaseManager = FindObjectOfType<PurchaseManager>();
            return _purchaseManager;
        }
    }

    public void BuyNonConsumable(int index)
    {
        purchaseManager.BuyNonConsumable(index);
    }

    public void BuyConsumable(int index)
    {
        purchaseManager.BuyConsumable(index);
    }
}
