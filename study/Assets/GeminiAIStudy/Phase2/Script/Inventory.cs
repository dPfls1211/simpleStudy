using UnityEngine;
using System.Collections.Generic; // List를 쓰기 위해 반드시 필요함!

public class Inventory : MonoBehaviour
{
    // 아이템 데이터를 담을 동적 배열(리스트) 생성
    // 인스펙터에서 볼 수 있도록 public으로 선언
    public List<ItemData> items = new List<ItemData>();

    // 외부에서 아이템을 집어넣을 때 부를 함수
    public void AddItem(ItemData newItem)
    {
        items.Add(newItem); // 리스트 맨 뒤에 추가 (C++의 push_back과 동일)
        Debug.Log($"가방에 [{newItem.itemName}]을(를) 넣었습니다! (현재 {items.Count}개)");
    }

    // 가방에 특정 아이템이 있는지 선형 탐색으로 검사하는 함수
    public bool HasItem(string searchName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == searchName)
            {
                return true; // 찾았다!
            }
        }
        return false; // 끝까지 다 뒤졌는데 없다
    }
}