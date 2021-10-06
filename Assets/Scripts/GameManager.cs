using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Behaviour[] ComponentsToDisable;
    public GameObject LoseObject;

    public void ShowLoseObject() {
        foreach (var item in ComponentsToDisable) {
            item.enabled = false;
        }
        LoseObject.SetActive(true);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
