using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    public static PoolingSystem Instance; 
    private List<PoolingItem> pool = new List<PoolingItem>();
   
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }    

    public void CreatePoolItem(GameObject obj, int ammount, string name)
    {
        if (!ItemPoolExist(name))
        {
            List<GameObject> newList = new List<GameObject>();
            for (int i = 0; i < ammount; i++)
            {
                GameObject instance = Instantiate(obj, transform);
                instance.name = "PoolItem " + obj.name + i;
                instance.gameObject.SetActive(false);
                newList.Add(instance);
            }

            PoolingItem item = new PoolingItem(name, newList);
            item.Count = ammount;
            item.MaxItems = ammount;
            pool.Add(item);
        }    
        else
        {
            Debug.Log("Item already exist on pool");
        }
    }

    public GameObject GetAvailableItem(string id)
    {
        GameObject obj = null;

        if (ItemPoolExist(id))
        {
            PoolingItem targetItem = null;

            foreach (PoolingItem item in pool)
            {
                if (item.ID == id)
                {
                    targetItem = item;
                }
            }

            if (targetItem.Count <= 0)
            {
                targetItem.Count = targetItem.MaxItems;
            }
           
            obj = targetItem.GameObjects[targetItem.Count - 1];
            obj.SetActive(true);
            targetItem.Count--;
        }
        else
        {
            Debug.LogError("The pool dont have an item with the id " + id);
        }

        return obj;
    }

    private bool ItemPoolExist(string id)
    {
        foreach (PoolingItem item in pool)
        {
            if (item.ID == id)
            {
                return true;
            }
        }
        return false;
    }
}

public class PoolingItem
{
    private string iD;
    private List<GameObject> gameObjects;
    private int count = 0;
    private int maxItems;


    public PoolingItem(string iD, List<GameObject> gameObjects)
    {
        this.iD = iD;
        this.gameObjects = gameObjects;
    }

    public string ID { get => iD; set => iD = value; }
    public List<GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }
    public int Count { get => count; set => count = value; }
    public int MaxItems { get => maxItems; set => maxItems = value; }
}
