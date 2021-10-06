using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class JoystickFixedPosition : MonoBehaviour
{

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

    [Space]
    public Text backGraundSize;
    public Text ActiveRectSize;
    public Text ScreanSize;

    private void LateUpdate()
    {
        if (backGraundSize == null) return;
        backGraundSize.text = "BackgraundSize: " + _backgroundTransform.sizeDelta.ToString();
        ActiveRectSize.text = "ActiveRectSize: " + ActiveAreaRect.sizeDelta.ToString();
        ScreanSize.text = "ScreanSize: " + Screen.width;
    }

    private void OnValidate()
    {
        Vector2 backgroundSize;
        if (_matchVariant == MatchVariant.Horizontal)
        {
            backgroundSize = Vector2.one * _size * _canvasRectTransform.sizeDelta.x;
        }
        else
        {
            backgroundSize = Vector2.one * _size * _canvasRectTransform.sizeDelta.y;
        }

        _backgroundTransform.sizeDelta = backgroundSize;
        _stickTransform.sizeDelta = backgroundSize * _stickSize;
    }

    private void Start()
    {
#if UNITY_ANDRIOD
        _inputType = InputType.Touch;
#endif
#if UNITY_IOS
        _inputType = InputType.Touch;
#endif
        _backgroundTransform.position = ActiveAreaRect.position = new Vector3(Screen.width / 2, Screen.height / 4, 0);

        _backgroundTransform.sizeDelta = ActiveAreaRect.sizeDelta = Vector2.one * _size * Screen.width;
        _stickTransform.sizeDelta = _backgroundTransform.sizeDelta * _stickSize;
    }

    [SerializeField] private int _fingerId = -1;

    void Update()
    {
        if (_inputType == InputType.Touch)
        {
            GetTouchInput();
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && IsPointInsideRect(ActiveAreaRect, Input.mousePosition))
            {
                IsPressed = true;
                OnDown(Input.mousePosition);
            }
            if (IsPressed)
            {
                if (Input.GetMouseButton(0)) OnPressed(Input.mousePosition);
                if (Input.GetMouseButtonUp(0))
                {
                    OnUp(Input.mousePosition);
                    IsPressed = false;
                }
            }
        }
    }

    bool IsPointInsideRect(RectTransform rect, Vector2 point)
    {
        if (point.x < (rect.position.x + rect.rect.xMax)
            && point.x > (rect.position.x + rect.rect.xMin)
            && point.y < (rect.position.y + rect.rect.yMax)
            && point.y > (rect.position.y + rect.rect.yMin))
        {
            return true;
        }
        return false;
    }

    private void GetTouchInput()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                if (_fingerId == -1)
                {
                    float relativeXPosition = touch.position.x / Screen.width;
                    Debug.Log(relativeXPosition);
                    Debug.Log(Time.time);
                    if (IsPointInsideRect(ActiveAreaRect, touch.position))
                    {
                        Debug.Log("inside");
                        _fingerId = touch.fingerId;
                        OnDown(touch.position);
                        break;
                    }
                }

            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                if (touch.fingerId == _fingerId)
                {
                    OnPressed(touch.position);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (touch.fingerId == _fingerId)
                {
                    _fingerId = -1;
                    OnUp(touch.position);
                }
            }
        }
    }

    public void OnDown(Vector2 touchPosition)
    {
        IsPressed = true;

        _joystickManager.OnDown(Value);
        //OnDownEvent.Invoke(touchPosition);

        //Show();
        //_backgroundTransform.position = touchPosition;
    }

    /* 
     * Со всем согласен, только странно каждый раз заново мерить радиус. Я бы его перещитывал его в OnValuate()
     */

    public void OnPressed(Vector2 touchPosition)
    {
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

    public void OnUp(Vector2 touchPosition)
    {
        IsPressed = false;

        _joystickManager.OnUp(Value);

        _stickTransform.localPosition = Vector3.zero;

       //OnUpEvent.Invoke(touchPosition);
       //Hide();
       Value = Vector2.zero;
    }

    public void SetJoystickPosition()
    {

    }
}
