using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolInstance : MonoBehaviour
{
    // Multiton
    public static Dictionary<string, PoolInstance> Instance { get; private set; }

    public string nameInstance = "9mm";
    public ItemInstanciable itemInstance;

    public int size = 500;

    public List<ItemInstanciable> pool = new List<ItemInstanciable>();

    private void Awake()
    {
        Dictionary<string, PoolInstance> instance = Instance;
        if (instance == null)
            instance = new Dictionary<string, PoolInstance>();
        if (!instance.ContainsKey(nameInstance))
        {
            instance.Add(nameInstance, this);
            Instance = instance;
            InitPool();
        }
        else
            Destroy(gameObject);        
    }

    private void InitPool()
    {
        itemInstance.gameObject.SetActive(false);

        for (int i = 0; i < size; i++)
            pool.Add(GameObject.Instantiate<ItemInstanciable>(itemInstance));
    }

    public void ItemInstance(Vector3 position, Quaternion rotation, Transform possibleParent = null)
    {
        if (pool.Count == 0)
        {
            Debug.LogError($"Pool {nameInstance} not big enough");
            return;
        }
        ItemInstanciable item = pool[0];
        pool.RemoveAt(0);
        item.Action(this, position, rotation, possibleParent);
    }

    public void Release(ItemInstanciable item)
    {
        item.gameObject.SetActive(false);
        pool.Add(item);
    }
}
