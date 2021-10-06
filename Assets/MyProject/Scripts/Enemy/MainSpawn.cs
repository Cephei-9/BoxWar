using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpawn : MonoBehaviour
{
    [SerializeField] private int _startVolnaIndex = 0;
    [Space]
    [SerializeField] private Game _game;
    public VolnaEnemy[] vol;
    private int _indexVol = 0;

    public void Awake()
    {
        _indexVol = _startVolnaIndex;
    }

    public void EndAttack(float time)
    {
        _game.Pause();

        if (_indexVol == vol.Length)
        {
            _game.EndGameEvent.Invoke();
        }
    }

    public void StartAttack()
    {
        print("Volna name: " + vol[_indexVol].name);
        vol[_indexVol].StartVolna();
        _indexVol++;
    }

    IEnumerator Pause(float time) 
    {
        yield return new WaitForSeconds(time);
    }
}




