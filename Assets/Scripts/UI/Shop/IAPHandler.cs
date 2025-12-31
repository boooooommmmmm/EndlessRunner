using System.Collections.Generic;
using UnityEngine;
#if UNITY_PURCHASING
using UnityEngine.Purchasing;
#endif

public class IAPHandler : MonoBehaviour
{
#if UNITY_PURCHASING
    public void ProductBought(Product product)
    {
        int amount = 0;
        switch (product.definition.id)
        {
            case "10_premium":
                amount = 10;
                break;
            case "50_premium":
                amount = 50;
                break;
            case "100_premium":
                amount = 100;
                break;
        }

        if (amount > 0)
        {
            PlayerData.instance.premium += amount;
            PlayerData.instance.Save();
        }
    }

    public void ProductError(Product product, PurchaseFailureReason reason)
    {
        Debug.LogError("Product : " + product.definition.id + " couldn't be bought because " + reason);
    }
#endif
}
