using UnityEngine;

public class StoreController : MonoBehaviour
{
    [SerializeField] private Transform[] slots;
    [SerializeField] private GameObject[] itemPrefabs;

    [SerializeField] private PlayerStatUpdate playerStatUpdate;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GetGoldText getGoldText;
    [SerializeField] private MessageController messageController;
    public void OnBuyButtonClicked(string itemID)
    {
        int itemPlace = 0;
        float itemAtk = 0;
        float itemHp = 0;
        float itemMp = 0;

        GameObject selectedPrefab = null;
        foreach (GameObject prefab in itemPrefabs)
        {
            ItemInfomation info = prefab.GetComponent<ItemInfomation>();
            if (info != null && info.itemID == itemID)
            {
                itemPlace = info.itemPrice;
                itemAtk = info.itemAtk;
                itemHp = info.itemHp;
                itemMp = info.itemMp;
                selectedPrefab = prefab;
                break;
            }
        }

        if (playerStats.playerGold - itemPlace < 0)
        {
            messageController.MessageSetting("Not enough gold!");
            return;
        }
        else
        {
            getGoldText.BuyItemGetGold(itemPlace);
        }

        foreach (Transform slot in slots)
        {
            if (slot.childCount == 0)
            {
                GameObject newItem = Instantiate(selectedPrefab, slot);
                newItem.transform.localPosition = Vector3.zero;
                playerStatUpdate.PlayerStatToItemUp(itemAtk, itemHp, itemMp);
                messageController.MessageSetting("Purchase complete!");
                return;
            }
        }

        messageController.MessageSetting("Inventory is full!");
    }

    public void OnSellButtonClicked(string itemID)
    {
        for (int i = slots.Length - 1; i >= 0; i--)
        {
            Transform slot = slots[i];
            if (slot.childCount > 0)
            {
                ItemInfomation info = slot.GetChild(0).GetComponent<ItemInfomation>();
                if (info != null && info.itemID == itemID)
                {
                    getGoldText.SellItemGetGold(info.itemAmount);
                    playerStatUpdate.PlayerStatToItemDown(info.itemAtk, info.itemHp, info.itemMp);
                    Destroy(slot.GetChild(0).gameObject);
                    messageController.MessageSetting("Sale complete!");
                    return;
                }
            }
        }

        messageController.MessageSetting("No items to sell!");
    }
}
