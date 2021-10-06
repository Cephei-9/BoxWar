using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VolnaEnemy : MonoBehaviour
{
    public float timeToNextVolna = 15;

    public List<PachkaEnemy> pachkaEnemies;

    public MainSpawn mainSpawn;

    public Transform[] spawns;

    public UnityEvent OnBegin;
    public UnityEvent OnEnd;

    bool isEndSpawn = false;

    private void Start()
    {
        mainSpawn = FindObjectOfType<MainSpawn>();
    }

    private void Update()
    {
        if (isEndSpawn)
        {
            int i = 3;
            foreach (var item in spawns)
            {
                int t = item.GetComponentsInChildren<Transform>().Length - 1;
                if (t == 0) i--;
            }
            if (i == 0)
            {
                EndVolna();
                enabled = false;
            }
        }
    }

    public void StartVolna()
    {
        OnBegin.Invoke();
        StartCoroutine(Work());
    }

    private void EndVolna()
    {
        OnEnd.Invoke();
        mainSpawn.EndAttack(timeToNextVolna);
        print("End");
    }

    IEnumerator Work()
    {
        foreach (var pachka in pachkaEnemies)
        {
            Transform spawn = spawns[pachka.spawnNumber];
            InstantiatePachka(pachka, spawn);
            pachka.OnPachkaCreateEvent.Invoke();
            yield return new WaitForSeconds(pachka.timeToNextPachka);
        }
        isEndSpawn = true;
    }

    public void InstantiatePachka(PachkaEnemy pachkaEnemy, Transform spawn)
    {
        for (int i = 0; i < pachkaEnemy.count; i++)
        {
            Vector2 randomVector = Random.insideUnitCircle;
            Vector3 v = new Vector3(randomVector.x, 0, randomVector.y) * 10;
            GameObject newEnemy = Instantiate(pachkaEnemy.prefab, spawn.position + v, Quaternion.identity, spawn);
            Economica.SingleTone.SubscribeOnCreatedEnemy(newEnemy);
        }
    }
}
