using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum MatchVariant {
    Horizontal,
    Vertical
}

public enum InputType {
    Mouse,
    Touch
}

public class Joystick : MonoBehaviour {

    [SerializeField] private RectTransform _backgroundTransform;
    [SerializeField] private RectTransform _stickTransform;
    [Range(0, 1)]
    [SerializeField] private float _size;
    [Range(0, 1)]
    [SerializeField] private float _stickSize;

    public Vector2 Value { get; private set; }
    public bool IsPressed { get; private set; }

    [SerializeField] private RectTransform _canvasRectTransform;
    [SerializeField] private MatchVariant _matchVariant;
    [SerializeField] private RectTransform ActiveAreaRect;
    [SerializeField] private InputType _inputType;

    [HideInInspector] public UnityEvent<Vector2> OnDownEvent;
    [HideInInspector] public UnityEvent<Vector2> OnPressedEvent;
    [HideInInspector] public UnityEvent<Vector2> OnUpEvent;

    [SerializeField] private JoystickManager _joystickManager;

    private void OnValidate() {

        Vector2 backgroundSize;
        if (_matchVariant == MatchVariant.Horizontal) {
            backgroundSize = Vector2.one * _size * _canvasRectTransform.sizeDelta.x;
        } else {
            backgroundSize = Vector2.one * _size * _canvasRectTransform.sizeDelta.y;
        }

        _backgroundTransform.sizeDelta = backgroundSize;
        _stickTransform.sizeDelta = backgroundSize * _stickSize;
    }

    void Start() {
#if UNITY_ANDRIOD
        _inputType = InputType.Touch;
#endif
#if UNITY_IOS
        _inputType = InputType.Touch;
#endif
        _backgroundTransform.sizeDelta = Vector2.one * _size * Screen.width;
        Hide();
    }

    [SerializeField] private int _fingerId = -1;

    void Update() {

        if (_inputType == InputType.Touch) {
            GetTouchInput();
        } else {
            if (Input.GetMouseButtonDown(0)) OnDown(Input.mousePosition);
            if (Input.GetMouseButton(0)) OnPressed(Input.mousePosition);
            if (Input.GetMouseButtonUp(0)) OnUp(Input.mousePosition);
        }

    }

    bool IsPointInsideRect(RectTransform rect, Vector2 point) {
        if (point.x < (rect.position.x + rect.rect.xMax)
            && point.x > (rect.position.x + rect.rect.xMin)
            && point.y < (rect.position.y + rect.rect.yMax)
            && point.y > (rect.position.y + rect.rect.yMin)) {
            return true;
        } else {
            return false;
        }
    }

    private void GetTouchInput() {
        foreach (Touch touch in Input.touches) {

            if (touch.phase == TouchPhase.Began) {
                if (_fingerId == -1) {
                    float relativeXPosition = touch.position.x / Screen.width; 
                    Debug.Log(relativeXPosition);
                    Debug.Log(Time.time);
                    if (IsPointInsideRect(ActiveAreaRect, touch.position)) {
                        Debug.Log("inside");
                        _fingerId = touch.fingerId;
                        OnDown(touch.position);
                        break;
                    }
                }

            } else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) {
                if (touch.fingerId == _fingerId) {
                    OnPressed(touch.position);
                }
            } else if (touch.phase == TouchPhase.Ended) {
                if (touch.fingerId == _fingerId) {
                    _fingerId = -1;
                    OnUp(touch.position);
                }
            }

        }
    }

    public void OnDown(Vector2 touchPosition) {
        IsPressed = true;
        //OnDownEvent.Invoke(touchPosition);

        _joystickManager.OnDown(Value);

        Show();
        _backgroundTransform.position = touchPosition;
    }

    /* 
     * Со всем согласен, только странно каждый раз заново мерить радиус. Я бы его перещитывал его в OnValuate()
     */

    public void OnPressed(Vector2 touchPosition) {
        //OnPressedEvent.Invoke(touchPosition);

        _joystickManager.OnPressed(Value);

        Vector2 toMouse = touchPosition - (Vector2)_backgroundTransform.position;

        float distance = toMouse.magnitude;        
        float pixelSize = _size * Screen.width;
        float radius = pixelSize * 0.5f;
        float toMouseClamped = Mathf.Clamp(distance, 0, radius);

        Vector2 stickPosition = toMouse.normalized * toMouseClamped;
        Value = stickPosition / radius;
        _stickTransform.localPosition = stickPosition;
    }

    public void OnUp(Vector2 touchPosition) {
        IsPressed = false;
        //OnUpEvent.Invoke(touchPosition);

        _joystickManager.OnUp(Value);

        Hide();
        Value = Vector2.zero;
    }

    private void Show() {
        _backgroundTransform.gameObject.SetActive(true);
        _stickTransform.gameObject.SetActive(true);
    }

    private void Hide() {
        _backgroundTransform.gameObject.SetActive(false);
        _stickTransform.gameObject.SetActive(false);
    }

}

public class JoystickSetup
{

}
