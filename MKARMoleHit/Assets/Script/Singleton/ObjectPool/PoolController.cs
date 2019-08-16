using UnityEngine;

public class PoolController : MonoBehaviour
{
    [SerializeField, Header("Poolするオブジェクト")]
    private GameObject obj;
    [SerializeField, Header("生成最大数")]
    private int MaxCount = 5;
    public Transform AttachPoint { get; set; }
    //オブジェクトプールクラス
    private ObjectPool objectPool;

    private void Awake()
    {
        if (AttachPoint != null)
        {
            objectPool = new ObjectPool(MaxCount, AttachPoint.position, AttachPoint.rotation);
        }
        else { objectPool = new ObjectPool(MaxCount); }
        objectPool.Pool(null, obj, MaxCount);
    }

    /// <summary>
    /// 生成条件決定処理
    /// </summary>
    /// <returns></returns>
    public bool IsCreate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) return true;
        return false;
    }

    /// <summary>
    /// 生成処理
    /// </summary>
    public void SetCreate()
    {
        CreateObj(AttachPoint.position, AttachPoint.rotation);
    }

    /// <summary>
    /// オブジェクトプール処理
    /// </summary>
    public void CreateObj(Vector3 pos, Quaternion rot)
    {
        objectPool.GetPool(pos, rot);
    }

    /// <summary>
    /// オブジェクトプール処理(返り値あり)
    /// </summary>
    public GameObject GetPoolObj(Vector3 pos, Quaternion quaternion)
    {
        return objectPool.GetPool(pos, quaternion);
    }
}