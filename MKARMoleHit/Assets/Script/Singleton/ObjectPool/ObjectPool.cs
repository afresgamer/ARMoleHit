using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private List<GameObject> poolList;
    private int currentCount;
    private int maxCount;
    private Vector3 originPos;
    private Quaternion originRot;

    public ObjectPool(int _max)
    {
        maxCount = _max;
        currentCount = 0;
        originPos = Vector3.zero;
        originRot = Quaternion.identity;
        poolList = new List<GameObject>();
    }

    public ObjectPool(int _max, Vector3 _position, Quaternion _quaternion)
    {
        maxCount = _max;
        currentCount = 0;
        originPos = _position;
        originRot = _quaternion;
        poolList = new List<GameObject>();
    }

    /// <summary>
    /// オブジェクトをプールする。
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="obj"></param>
    /// <param name="num"></param>
    public void Pool(GameObject parent, GameObject obj, int num)
    {
        int count = num;
        if (num > maxCount) { count = maxCount; }
        for (int i = 0; i < count; i++)
        {
            var poolObj = Object.Instantiate(obj, originPos, originRot);
            if (parent != null) { poolObj.transform.SetParent(parent.transform); }
            poolObj.SetActive(false);
            poolObj.name = obj.name + (i + 1);
            poolList.Add(poolObj);
        }
    }

    /// <summary>
    /// プールしたオブジェクトを返す
    /// </summary>
    /// <returns></returns>
    public GameObject GetPool()
    {
        if (poolList == null) { return null; }

        GameObject returnObj = poolList[currentCount];
        returnObj.transform.position = originPos;
        returnObj.transform.rotation = originRot;
        currentCount++;
        if (currentCount >= poolList.Count) { currentCount = 0; }
        returnObj.SetActive(true);

        return returnObj;
    }

    /// <summary>
    /// 位置情報を更新してプールしたオブジェクトを返す(空いてるオブジェクトを検索)
    /// </summary>
    /// <param name="pos">位置情報</param>
    /// <param name="rot">回転情報</param>
    /// <returns></returns>
    public GameObject GetObject(Vector3 pos, Quaternion rot)
    {
        // 使用中でないものを探して返す
        foreach (var obj in poolList)
        {
            if (obj.activeSelf == false)
            {
                obj.transform.position = pos;
                obj.transform.rotation = rot;
                obj.SetActive(true);
                return obj;
            }
        }

        return null;
    }

    /// <summary>
    /// 位置情報を更新してプールしたオブジェクトを返す
    /// </summary>
    /// <param name="pos">位置情報</param>
    /// <param name="rot">回転情報</param>
    public GameObject GetPool(Vector3 pos, Quaternion rot)
    {
        if(poolList == null) { return null; }

        GameObject returnObj = poolList[currentCount];
        returnObj.transform.position = pos;
        returnObj.transform.rotation = rot;
        currentCount++;
        if (currentCount >= poolList.Count) { currentCount = 0; }
        returnObj.SetActive(true);

        return returnObj;
    }
}
