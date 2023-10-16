using UnityEngine;

public class ShopManager : MonoBehaviour
{
<<<<<<< Updated upstream
    public GameObject item2x; // Reference to the 2x item GameObject

    private static ShopManager instance;

    public static ShopManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ShopManager>();
            }
            return instance;
        }
    }

    public bool is2xItemUnlocked { get; private set; }

    // Call this method to unlock the 2x item
    public void Unlock2xItem()
    {
        is2xItemUnlocked = true;

        // Activate the 2x item in your game, for example:
        if (item2x != null)
        {
            item2x.SetActive(true);
        }
    }

    // Other methods for your shop functionality can be added here.

    private void Awake()
=======
    public Image lockIcon2xItem; // Reference to the lock icon Image component for the 2x item
    private bool is2xItemUnlocked = false;

    // Call this function to unlock the 2x item
    public void Unlock2xItem()
    {
        // Check if the 2x item is not already unlocked
        if (!is2xItemUnlocked)
        {
            is2xItemUnlocked = true;
            UpdateShopUI();
        }
    }

    // Update the shop UI to show/hide the lock icon based on unlock status
    private void UpdateShopUI()
>>>>>>> Stashed changes
    {
        // Ensure there's only one instance of ShopManager
        if (instance != null && instance != this)
        {
<<<<<<< Updated upstream
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // Keep the ShopManager between scenes if needed.
=======
            // The 2x item is unlocked, disable the lock icon
            lockIcon2xItem.enabled = false;
>>>>>>> Stashed changes
        }
    }

    // Other shop-related functions...
}
