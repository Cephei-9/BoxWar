using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    [SerializeField] private MiniMassage _ferstLoseMessage;

    public void ShowLoseMessage()
    {
        GetComponent<CameraCrush>().AddCrush(1);
        _ferstLoseMessage.ShowMessege();
    }
}
