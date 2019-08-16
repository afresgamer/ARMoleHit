using UnityEngine;

public class NormalMole : MoleBase {
    
    private void OnDisable()
    {
        Vector3 pos = transform.position;
        pos.y = 0;
        transform.position = pos;
    }

    public override void Start()
    {
        NormalMove();
    }

}
