using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PoolType
{
    None,
    Skill,
    UI,
    Other
}

public class Pool
{
    public PoolType Tp = PoolType.None;
    public double CurTime = 0;
    public string PoolName = ""; //缓存池的名字
    public List<GameObject> Spawnls = new List<GameObject>(); //显示列表
    public List<GameObject> DeSpawnls = new List<GameObject>(); //隐藏列表
    public GameObject Spawn()
    { //从缓存中取
        GameObject obj = null;
        if (DeSpawnls.Count > 0)
        {
            obj = DeSpawnls[0];
            DeSpawnls.Remove(obj);
        }
        else
        {
            obj = GameObject.Instantiate((GameObject)Resources.Load(PoolName));
            obj.name = PoolName;
        }
        Spawnls.Add(obj);
        return obj;
    }

    public void Despawn(GameObject obj)
    { //进入缓存（在改进入缓存的调用不要Destroy哦，一般用卸载来进行统一的Destroy）
        obj.SetActive(false);
        Spawnls.Remove(obj);
        DeSpawnls.Add(obj);
        
    }

    public void CleanAll()
    {
        for (int i = 0; i < Spawnls.Count; i++)
        {
            GameObject.Destroy(Spawnls[i]);
        }
        for (int i = 0; i < DeSpawnls.Count; i++)
        {
            GameObject.Destroy(DeSpawnls[i]);
        }
        Spawnls.Clear();
        DeSpawnls.Clear();
    }
}

public class PoolManager
{
    public static Dictionary<string, Pool> PoolDic = new Dictionary<string, Pool>(); //总缓存
    public static int CheckUnloadTime = 30;//最大未使用并要清理的时间间隔
    static List<GameObject> gos = new List<GameObject>();

    static public GameObject GetPrefabFromCachePool(string name, Transform parent, PoolType type)
    {
        Pool pool = null;
        GameObject newObj = null;
        if (!(name.Equals("") || name == null))
        {
            if (!PoolDic.TryGetValue(name, out pool))
            {
                pool = new Pool();
                pool.PoolName = name;
                pool.Tp = type;
                PoolDic.Add(name, pool);
            }
            newObj = pool.Spawn();
            if (parent != null)
            {
                newObj.transform.SetParent(parent);
            }
        }
        newObj.SetActive(true);
        return newObj;
    }

    static public void RecycleGameObject(GameObject go)
    {
        Pool pool = null;
        if (go != null)
        {
            if (PoolDic.TryGetValue(go.name, out pool))
            {
                pool.Despawn(go);
            }
        }
    }

    static public List<GameObject> GetAllActiveGameobject()
    {
        gos.Clear();
        foreach (KeyValuePair<string, Pool> pool in PoolDic)
        {
            gos.AddRange(pool.Value.Spawnls);
        }
        return gos;
    }
}