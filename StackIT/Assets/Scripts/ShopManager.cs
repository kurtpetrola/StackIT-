using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Image lockIcon2xItem; // Reference to the lock icon Image component for the 2x item
    private bool is2xItemUnlocked = false;

    // Call this function when the player successfully stacks 5 boxes
    public void Stack5Boxes()
    {
        // Check if the player has stacked 5 boxes and unlock the 2x item
        if (!is2xItemUnlocked)
        {
            is2xItemUnlocked = true;
            UpdateShopUI();
        }
    }

    // Update the shop UI to show/hide the lock icon based on unlock status
    private void UpdateShopUI()
    {
        if (is2xItemUnlocked)
        {
            // The 2x item is unlocked, disable the lock icon
            lockIcon2xItem.enabled = false;
        }
    }

    // Other shop-related functions...
}
