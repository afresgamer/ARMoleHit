using UnityEngine;
using UniRx;

public class Hole : MonoBehaviour {
    
    [HideInInspector]
    public BoolReactiveProperty IsEmergenceMole { get; set; }

    public Vector3 GetPos()
    {
        Vector3 pos = transform.position - new Vector3(0, 0.25f, 0);
        return pos;
    }
    
}
