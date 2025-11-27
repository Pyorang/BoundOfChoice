using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI_CameraSpace : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private RectTransform _rectTransform;
    private Canvas _canvas;

    // 이 변수는 World/Camera Space에서는 스크린 좌표 대신
    // 마우스가 드래그 시작 시점의 Local/World Offset을 저장하는 데 사용될 수 있습니다.
    private Vector2 _offset;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        // 현재 UI 요소가 속한 최상위 Canvas 컴포넌트를 찾습니다.
        _canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 캔버스가 존재하고 'Screen Space - Camera' 모드일 때
        if (_canvas != null && (_canvas.renderMode == RenderMode.ScreenSpaceCamera || _canvas.renderMode == RenderMode.WorldSpace))
        {
            // 스크린상의 마우스 위치와 RectTransform의 pivot 위치 사이의 Local Space 오프셋을 계산합니다.
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localMousePos))
            {
                // LocalPosition에서 LocalMousePos를 빼서 오프셋을 구하면,
                // 드래그 중 마우스가 RectTransform의 피벗으로부터 얼마나 떨어져 있어야 하는지를 알 수 있습니다.
                _offset = (Vector2)_rectTransform.localPosition - localMousePos;
            }
        }
        else // Overlay 모드 처리 (기존 로직과 유사)
        {
            _offset = (Vector2)_rectTransform.position - eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 캔버스가 존재하고 'Screen Space - Camera' 모드일 때
        if (_canvas != null && (_canvas.renderMode == RenderMode.ScreenSpaceCamera || _canvas.renderMode == RenderMode.WorldSpace))
        {
            // 스크린상의 마우스 위치를 RectTransform 기준으로 로컬 좌표로 변환합니다.
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
            {
                // 계산된 Local Point에 시작 오프셋을 더하여 새 로컬 위치를 설정합니다.
                _rectTransform.localPosition = localPoint + _offset;
            }
        }
        else // Overlay 모드 처리 (기존 로직과 유사)
        {
            _rectTransform.position = eventData.position + _offset;
        }
    }
}