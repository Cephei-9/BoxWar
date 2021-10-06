using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JoystickType
{
    moveJoystic,
    staticJoystick,
    none
}

public class JoystickManager : MonoBehaviour
{
    [SerializeField] private Joystick _moveJoystick;
    [SerializeField] private JoystickFixedPosition _staticJoystick;

    private Person _activePersone;
    private JoystickType _activeJoysticType;

    private void Awake()
    {
        PersonManager personManager = FindObjectOfType<PersonManager>();
        personManager.OnPersonChanged.AddListener(OnActivPersonChange);
        //_activePersone = personManager.ActivePerson;
        //ChangeJoystick(_activePersone.JoystickType);
    }

    public void OnDown(Vector2 value)
    {
        _activePersone.OnDown(value);
    }

    public void OnPressed(Vector2 value)
    {
        _activePersone.OnPressed(value);
    }

    public void OnUp(Vector2 value)
    {
        _activePersone.OnUp(value);
    }

    public void ShowJoystick()
    {
        ChangeJoystick(_activePersone.JoystickType);
    }

    public void HideJoystick()
    {
        SetActiveJoysticks(false, false);
    }

    private void OnActivPersonChange(Person activPerson)
    {
        _activePersone = activPerson;
        ChangeJoystick(activPerson.JoystickType);
    }

    private void ChangeJoystick(JoystickType joystickType)
    {
        if (joystickType == JoystickType.moveJoystic)
        {
            SetActiveJoysticks(true, false);
        }
        else if(joystickType == JoystickType.staticJoystick)
        {
            SetActiveJoysticks(false, true);
        }
        else
        {
            SetActiveJoysticks(false, false);
        }
    }

    private void SetActiveJoysticks(bool activeMoveJoy, bool activStaticJoy)
    {
        _moveJoystick.gameObject.SetActive(activeMoveJoy);
        _staticJoystick.gameObject.SetActive(activStaticJoy);
    }
}
