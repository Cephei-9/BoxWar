using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Person : MonoBehaviour
{
    [System.Serializable]
    public class PersonEvents
    {
        public UnityEvent OnActivateEvent;
        public UnityEvent OnDeactivateEvent;
    }

    [Header("Person")]
    [SerializeField] protected CameraSetup _cameraSetup; 
    [SerializeField] protected bool useStndartSetup = false; 
    [SerializeField] protected JoystickType _joystickType;
    [Space]
    [SerializeField] protected GameObject _interfaceObjectInNavigationMode;
    [SerializeField] public PersonEvents Events;

    public JoystickType JoystickType { get => _joystickType; }
    public bool IsActive { get; private set; }

    protected CameraMove _cameraMove;
    protected PersonManager _personManager;

    private void Awake()
    {
        _cameraMove = FindObjectOfType<CameraMove>();
        _personManager = FindObjectOfType<PersonManager>();
    }

    protected virtual void Update()
    {
        SetCameraParametrs();
    }

    public virtual void OnDown(Vector2 value) { }

    public virtual void OnPressed(Vector2 value) { }

    public virtual void OnUp(Vector2 value) { }

    public virtual void Activate()
    {
        this.enabled = true;        

        _cameraMove.SetSetup(_cameraSetup, useStndartSetup);
        IsActive = true;
        gameObject.SetActive(true);
        Events.OnActivateEvent.Invoke();
    }

    public virtual void DeActivate()
    {
        IsActive = false;
        Events.OnDeactivateEvent.Invoke();

        this.enabled = false;
    }

    protected virtual void SetCameraParametrs() { }

    public void ToNavigation()
    {
        bool inLastPoint = false;
        if (this is BildingPoint) inLastPoint = true;
        _personManager.ToNavigation(inLastPoint);
    }

    public void ActiveThis()
    {
        _personManager.ChangePersonOnThis(this);
    }

    public void NavigationInterfaceSetActive(bool active)
    {
        _interfaceObjectInNavigationMode.SetActive(active);
    }
}

public class StaticCameraPerson : Person
{
    private float _speedCamera;

    public void SetSpeed(float speedCamera)
    {
        _speedCamera = speedCamera;
    }

    protected override void Update() { }

    public override void Activate() 
    {
        _cameraMove.SetSetup(new CameraSetup { moveSpeed = _speedCamera, cameraMoveSpeed = _speedCamera }, false);
    }

    public override void DeActivate() { }
}
