using UnityEngine;
using TMPro; // TextMeshPro 사용

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel; // 가방 UI 창
    public GameObject slotPrefab;     // 아까 만든 ItemSlot 프리팹
    public Transform slotParent;      // 슬롯들이 생성될 부모 (InventoryPanel)

    // 플레이어의 데이터 가방
    public Inventory playerInventory;

    void Update()
    {
        // I 키를 누르면 가방을 열고 닫는다
        if (Input.GetKeyDown(KeyCode.I))
        {
            // SetActive(!현재상태): 켜져있으면 끄고, 꺼져있으면 켠다 (토글)
            bool isActive = inventoryPanel.activeSelf;
            inventoryPanel.SetActive(!isActive);

            // 가방이 열릴 때마다 내용물을 새로 그린다
            if (!isActive)
            {
                UpdateUI();
            }
        }
    }

    void UpdateUI()
    {
        // 1. 기존에 그려져 있던 슬롯들을 싹 다 지운다 (안 그러면 열 때마다 계속 복사됨)
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        // 2. 우아한 for문! 내 데이터 가방(items)에 있는 아이템을 하나씩 꺼냄
        foreach (ItemData item in playerInventory.items)
        {
            // 3. 슬롯 프리팹을 하나 생성 (부모는 slotParent)
            GameObject newSlot = Instantiate(slotPrefab, slotParent);

            // 4. 생성된 슬롯 안의 텍스트를 찾아서 아이템 이름을 적어줌
            TextMeshProUGUI nameText = newSlot.GetComponentInChildren<TextMeshProUGUI>();
            if (nameText != null)
            {
                nameText.text = item.itemName;
            }

            // (만약 아이콘 이미지를 넣고 싶다면 여기서 Image 컴포넌트를 찾아 교체하면 됩니다)
        }
    }
}