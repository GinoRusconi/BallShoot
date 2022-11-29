using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform _SpawnsParent;
    public ParticleSystem _SpawnParticleInComing;
    private readonly List<Transform> spawnsPosition = new List<Transform>();
    public float timeSpawn = 0.5f;
    public float timeBtwSpawn = 0f;

    public int[] _CountEnemysToSpawn;
    public int countTypeEnemy = 1;
    public int typeEnemy = 1;
    public bool IsFinishSpawn = false;
    public int lastEnemy;

    private void Start()
    {
        foreach (Transform childspawn in _SpawnsParent)
        {
            spawnsPosition.Add(childspawn);
        }
        _SpawnParticleInComing.Stop();
    }

    public void StartSpawned()
    {
        StartCoroutine(SpawnEnemy());
    }

    public void StopSpawned()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < _CountEnemysToSpawn.Length; i++)
        {
            for (int j = 1; j <= _CountEnemysToSpawn[i]; j++)
            {
                int count = spawnsPosition.Count;
                int randomSpawn = Random.Range(0, count);

                _SpawnParticleInComing.transform.position = spawnsPosition[randomSpawn].transform.position;
                _SpawnParticleInComing.Play();

                yield return new WaitForSeconds(timeSpawn);

                GameObject myEnemy = PoolObjects.Current.SpawnFromPoolEnemyLevel(i);
                if (myEnemy != null)
                {
                    myEnemy.transform.SetPositionAndRotation(spawnsPosition[randomSpawn].transform.position, spawnsPosition[randomSpawn].transform.rotation);
                    myEnemy.SetActive(true);
                }
                yield return null;
            }
        }
    }

    //    /*

    //    yield return new WaitForSeconds(timeSpawn);
    //        /*
    //        GameObject myObject = PoolObjects.Current.SpawnFromPool("Circle");
    //        if (myObject != null)
    //        {
    //            myObject.transform.SetPositionAndRotation(spawnsPosition[randomSpawn].transform.position, spawnsPosition[randomSpawn].transform.rotation);
    //            myObject.SetActive(true);
    //        }*/

    //        if (countTypeEnemy <= _CountEnemysToSpawn[typeEnemy - 1])
    //        {
    //            GameObject myEnemy = PoolObjects.Current.SpawnFromPoolEnemyLevel(typeEnemy);
    //            if(myEnemy != null)
    //            {
    //                myEnemy.transform.SetPositionAndRotation(spawnsPosition[randomSpawn].transform.position, spawnsPosition[randomSpawn].transform.rotation);
    //                myEnemy.SetActive(true);
    //                countTypeEnemy++;
    //            }
    //        }
    //        else if (countTypeEnemy > _CountEnemysToSpawn[typeEnemy - 1])
    //{
    //    typeEnemy++;
    //    GameObject myEnemy = PoolObjects.Current.SpawnFromPoolEnemyLevel(typeEnemy);
    //    if (myEnemy != null)
    //    {
    //        myEnemy.transform.SetPositionAndRotation(spawnsPosition[randomSpawn].transform.position, spawnsPosition[randomSpawn].transform.rotation);
    //        myEnemy.SetActive(true);
    //        countTypeEnemy = 2;
    //    }
    //}
}