﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BufferPool
{
    private Queue<GameObject> pool;
    private GameObject prefab;
    private Transform prefabParent;

    //使用构造函数构造对象池
    public BufferPool(GameObject obj, Transform parent, int count)
    {
        prefab = obj;

        pool = new Queue<GameObject>(count);
        prefabParent = parent;

        for (int i = 0; i < count; i++)
        {
            GameObject objClone = GameObject.Instantiate(prefab) as GameObject;
            objClone.transform.parent = prefabParent;//为克隆出来的子弹指定父物体
            objClone.name = "Clone0" + i.ToString();
            objClone.SetActive(false);
            pool.Enqueue(objClone);
        }
    }


    public GameObject GetObject()
    {
        GameObject obj = null;

        if (pool.Count > 0)
        {
            obj = pool.Dequeue();  //Dequeue()方法 移除并返回位于 Queue 开始处的对象
            obj.transform.position = prefabParent.position;
        }
        else
        {
            obj = GameObject.Instantiate(prefab) as GameObject;
            obj.transform.SetParent(prefabParent);

        }

        obj.SetActive(true);
        return obj;
    }

    //回收对象
    public void Recycle(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);//加入队列
    }
}
