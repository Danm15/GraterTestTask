using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _foodPrefab;

    public static ObjectPool Instance;
    private List<GameObject> _pooledObjects = new List<GameObject>();

    private int _amountToPool = 250;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    void Start()
    {
        FillPool();

    }

    public GameObject GetGameObject()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }
        return null;
    }

    private void FillPool()
    {
        GameObject particle;

        for (int i = 0; i < _amountToPool; i++)
        {

            particle = Instantiate(_foodPrefab);
            particle.SetActive(false);
            _pooledObjects.Add(particle);

        }
    }

}
