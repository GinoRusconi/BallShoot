using System.Collections.Generic;
using UnityEngine;

public class PoolObjects : MonoBehaviour
{
    private static PoolObjects _current;

    public static PoolObjects Current

    #region Singleton

    {
        get
        {
            if (_current == null)
            {
                _current = GameObject.FindObjectOfType<PoolObjects>();
            }

            return _current;
        }
    }

    #endregion Singleton

    [SerializeField] private List<ObjectsToPool> objects;
    private Dictionary<string, List<GameObject>> poolDictionary;

    private Dictionary<int, List<GameObject>> enemyDictionary;
    private List<GameObject> enemyList;
    private List<GameObject> prefabEnemyListLevel;

    private void Awake()
    {
        enemyDictionary = new Dictionary<int, List<GameObject>>();
        enemyList = new List<GameObject>();
        prefabEnemyListLevel = new List<GameObject>();
    }

    private void Start()
    {
        InstantiatePool();
    }

    private void InstantiatePool()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (ObjectsToPool objectPool in objects)
        {
            List<GameObject> objectsToPool = new List<GameObject>();

            for (int j = 0; j < objectPool.sizePool; j++)
            {
                GameObject obj = Instantiate(objectPool.prefabObject);
                obj.SetActive(false);
                obj.transform.parent = transform;
                objectsToPool.Add(obj);
            }

            poolDictionary.Add(objectPool.tag, objectsToPool);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        List<GameObject> ListObjectToSpawn = poolDictionary[tag];

        foreach (GameObject objectpool in ListObjectToSpawn)
        {
            if (!objectpool.activeInHierarchy)
            {
                return objectpool;
            }
        }

        GameObject objPref = ListObjectToSpawn[1];
        GameObject obj = Instantiate<GameObject>(objPref);
        obj.transform.parent = transform;
        ListObjectToSpawn.Add(obj);
        return obj;
    }

    public void DisablesObjectToPool(GameObject ojbToDisable)
    {
        ojbToDisable.SetActive(false);
        ojbToDisable.GetComponent<Rigidbody2D>().Sleep();
        ojbToDisable.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        ojbToDisable.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    /*
    public void ClearPool()
    {
        foreach (KeyValuePair<string, List<GameObject>> keysValue in poolDictionary)
        {
            foreach(GameObject obj in keysValue.Value)
            {
                obj.SetActive(false);
                obj.GetComponent<Rigidbody2D>().Sleep();
                obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                obj.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
        }
    }*/

    // Para abajo es un pool de enemigos
    public void SetPoolEnemysLevel(GameObject[] enemys)
    {
        prefabEnemyListLevel.Clear();

        for (int i = 0; i < enemys.Length; i++)
        {
            prefabEnemyListLevel.Add(enemys[i]);
            GameObject enemy = Instantiate(enemys[i]);
            enemy.SetActive(false);
            enemy.transform.parent = transform;
            enemyList.Add(enemy);
            enemyDictionary.Add(i, enemyList);
        }
    }

    public GameObject SpawnFromPoolEnemyLevel(int typeEnemy)
    {
        List<GameObject> ListEnemys = enemyDictionary[typeEnemy];

        foreach (GameObject enemy in ListEnemys)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }
        GameObject enemyPref = prefabEnemyListLevel[typeEnemy];
        GameObject enemyToCreate = Instantiate<GameObject>(enemyPref);
        enemyToCreate.SetActive(false);
        enemyToCreate.transform.parent = transform;
        ListEnemys.Add(enemyToCreate);
        return enemyToCreate;
    }

    public void ClearEnemyPool()
    {
        foreach (KeyValuePair<int, List<GameObject>> keysValue in enemyDictionary)
        {
            foreach (GameObject enemy in keysValue.Value)
            {
                Destroy(enemy);
            }
            keysValue.Value.Clear();
        }
        enemyDictionary.Clear();
    }
}