using UnityEngine;

public class testShop : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            UIManager.Instance.OpenUI<ShopUI>(new BaseUIData());
        }
    }
}
