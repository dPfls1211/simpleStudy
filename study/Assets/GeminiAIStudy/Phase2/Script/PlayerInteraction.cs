using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 2.0f;

    // 내 몸에 있는 가방(Inventory)을 담을 변수
    Inventory myInventory;

    void Start()
    {
        // 시작할 때 내 컴포넌트 중에서 Inventory를 찾아서 연결해둠
        myInventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckObject();
        }
    }

    void CheckObject()
    {
        Vector3 origin = transform.position + Vector3.up * 1.0f;
        Vector3 direction = transform.forward;

        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            ItemPickup item = hit.collider.GetComponent<ItemPickup>();

            if (item != null)
            {
                // 1. 가방에 아이템 데이터를 넣는다! (핵심)
                myInventory.AddItem(item.itemData);

                // 2. 필드에 떨어져 있던 아이템 모델을 파괴한다
                Destroy(hit.collider.gameObject);
            }
        }
    }


    void OnDrawGizmos()
    {
        // 씬 화면에서 확인하기 쉽도록 빨간 선을 그어줌
        Vector3 origin = transform.position + Vector3.up * 1.0f;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin, transform.forward * interactDistance);
    }

}