using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MoleController : MonoBehaviour {

    [SerializeField, Header("最大数")]
    private int MaxCount = 3;
    [SerializeField, Header("Moleオブジェクト")]
    private GameObject mole;
    [Header("生成ポイント")]
    public HoleController holeController;
    [SerializeField, Header("親オブジェクト")]
    private GameObject Parent;

    private ObjectPool pool;

    void Start () {
        //Poolするオブジェクトの初期化
        holeController.Init();
        pool = new ObjectPool(MaxCount);
        pool.Pool(Parent, mole, MaxCount);

        this.UpdateAsObservable().Where(_ => GameStatus.Instance.GetGameStart()).Subscribe(_ =>
        {
            if(GameStatus.Instance.NowCount < MaxCount)
            {
                Debug.Log("NOWCOUNT" + GameStatus.Instance.NowCount);
                Create();
            }
        });
    }

    public void Create()
    {
        //空いてない場所を取得したら戻る
        if (!holeController.IsEmptyHole()) return;
        Hole hole = holeController.GetHole();
        //ホールがNULLだったら取得するまで取得
        while (hole == null) hole = holeController.GetHole();
        GameObject Mole = pool.GetObject(hole.GetPos(), mole.transform.rotation);
        Mole.GetComponent<NormalMole>().Hole = hole;
        //生成完了したらカウントを増やす
        GameStatus.Instance.NowCount++;
    }
}
