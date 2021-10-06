using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieBoxesOnTregger : MonoBehaviour
{
    public bool CanEnemyKill = true;
    public bool CanPlayerKill = true;

    private void OnTriggerEnter(Collider other)
    {
        if (CanEnemyKill && other.GetComponent<EnemyHealth>()) 
        {
            other.GetComponent<EnemyHealth>().Die(new Health());
        }
        if (CanPlayerKill && other.GetComponent<PlayerHealth>())
        {
            other.GetComponent<PlayerHealth>().Die(new Health());
        }
    }

    public void SetActiveCanKill(bool canEnemyKill, bool canPlayerKill)
    {
        CanEnemyKill = canEnemyKill;
        CanPlayerKill = canPlayerKill;
    }

    public void SetActivePlayerCanKill(bool canPlayerKill)
    { 
        CanPlayerKill = canPlayerKill;
    }
}
