using UnityEngine;

// [CreateAssetMenu]: 프로젝트 창에서 우클릭으로 이 파일을 만들 수 있게 해줌!
[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object/Item Data")]
public class ItemData : ScriptableObject // MonoBehaviour가 아님!
{
    public string itemName;      // 아이템 이름
    public Sprite icon;          // 아이콘 이미지
    [TextArea]
    public string description;   // 설명
    public int price;            // 가격
}