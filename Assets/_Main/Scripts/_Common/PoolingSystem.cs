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
            List<GameObject> objs = new List<GameObject>();

            foreach (PoolingItem item in pool)
            {
                if (item.ID == id)
                {
                    objs = item.GameObjects;
                }
            }

            for (int i = 0; i < objs.Count; i++)
            {
                if (!objs[i].activeInHierarchy)
                {
                    obj =  objs[i];
                }

                if (i == objs.Count - 1 && !obj)
                {
                    obj = objs[0];
                }
            }
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

    public PoolingItem(string iD, List<GameObject> gameObjects)
    {
        this.iD = iD;
        this.gameObjects = gameObjects;
    }

    public string ID { get => iD; set => iD = value; }
    public List<GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }
}
