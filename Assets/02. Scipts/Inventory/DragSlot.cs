using UnityEngine;
using UnityEngine.UI;

public class DragSlot : SingletonBehaviour<DragSlot>
{
    private Image _dragImageUI;

    protected override void Init()
    {
        IsDestroyOnLoad = true;
        base.Init();

        _dragImageUI = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void BeginDrag(Sprite image)
    {
        gameObject.SetActive(true);
        _dragImageUI.sprite = image;
    }

    public void EndDrag()
    {
        gameObject.SetActive(false);
        _dragImageUI.sprite = null;
    }

    public void Drag(Vector2 position)
    {
        transform.position = position;
    }
}
