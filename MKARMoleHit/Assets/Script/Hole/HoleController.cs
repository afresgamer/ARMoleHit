using UnityEngine;

[CreateAssetMenu(fileName = "Holes", menuName = "Holes")]
public class HoleController : ScriptableObject {

    [SerializeField,Header("生成するポイント")]
    public Hole[] holes;
    private int num = 0;
    
    public void Init()
    {
        for(int i = 0; i < holes.Length; i++)
        {
            holes[i].IsEmergenceMole.Value = false;
        }
    }

    public bool IsEmptyHole()
    {
        num = Random.Range(0, holes.Length);
        //空いてたら取得する
        if (!holes[num].IsEmergenceMole.Value)
        {
            holes[num].IsEmergenceMole.Value = true;
            return holes[num].IsEmergenceMole.Value;
        }
        return false;
    }

    public Hole GetHole()
    {
        return holes[num];
    }
}
