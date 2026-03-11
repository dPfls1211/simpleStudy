using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData; // 아까 만든 데이터 파일을 꽂을 구멍

    // 디버그용: 플레이어가 E를 누르면 호출될 함수
    public void Interact()
    {
        Debug.Log($"아이템 획득: {itemData.itemName}");
        Debug.Log($"설명: {itemData.description}");

        // (나중에 여기에 인벤토리 추가 코드를 넣을 것임)
        Destroy(gameObject); // 먹었으니 사라짐
    }
}