using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;    // 鼠标拖动的移动速度
    public float zoomSpeed = 2f;    // 鼠标滚轮的缩放速度
    public float minZoom = 5f;      // 最小缩放值
    public float maxZoom = 20f;     // 最大缩放值

    public Slider zoomSlider;       // 用于显示和控制缩放的滑条

    private Vector3 dragOrigin;

    private float dragRate;
    private bool selectOther;

    void Start()
    {
        selectOther = true;
        // 初始化缩放Slider
        zoomSlider.minValue = minZoom;
        zoomSlider.maxValue = maxZoom;
        zoomSlider.value = Camera.main.orthographicSize;
        dragRate= Camera.main.orthographicSize;
        // 监听Slider值变化
        zoomSlider.onValueChanged.AddListener(OnZoomSliderValueChanged);

    }

    void Update()
    {
        // 处理鼠标拖动
        HandleMouseDrag();

        // 处理缩放
        HandleZoom();
    }

    void HandleMouseDrag()
    {
        // 如果鼠标在UI元素上，则不处理相机拖动
        if (EventSystem.current.IsPointerOverGameObject())
        {
            selectOther = false;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            selectOther=true;
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        if (selectOther)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.x * moveSpeed, pos.y * moveSpeed, 0);
            transform.Translate(-move * dragRate * 0.7f, Space.World);
            dragOrigin = Input.mousePosition;
        }
    }

    void HandleZoom()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize -= scrollData * zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);

        // 更新Slider值
        zoomSlider.value = Camera.main.orthographicSize;
    }

    void OnZoomSliderValueChanged(float value)
    {
        Camera.main.orthographicSize = value;
        dragRate = value;
    }
}
