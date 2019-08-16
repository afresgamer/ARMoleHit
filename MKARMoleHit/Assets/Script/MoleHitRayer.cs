using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class MoleHitRayer : MonoBehaviour {

    [SerializeField, Header("Ray飛距離")]
    private float rayDistance = 100;
    [SerializeField, Header("エフェクト制御")]
    private PoolController effectCon;
    [SerializeField, Header("もぐらたたきSE")]
    private AudioSource MoleSE;

    void Start () {
        //もぐらたたき処理
        this.UpdateAsObservable().Where(_ => GameStatus.Instance.GetGameStart()).Subscribe(_ =>
        {
            TouchInfo touchInfo = AppUtil.GetTouch();

            if (touchInfo == TouchInfo.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(AppUtil.GetTouchPosition());
                RaycastHit hit;
                
                if(Physics.Raycast(ray,out hit, rayDistance))
                {
                    if (hit.collider.gameObject.name.Contains("Mole"))
                    {
                        //SE再生
                        MoleSE.PlayOneShot(MoleSE.clip);
                        //エフェクト生成
                        effectCon.CreateObj(hit.collider.gameObject.transform.position, Quaternion.identity);
                        GameStatus.Instance.Score.Value++;//スコア加算
                        GameStatus.Instance.NowCount--;
                        hit.collider.gameObject.GetComponent<NormalMole>().Hole.IsEmergenceMole.Value = false;
                        hit.collider.gameObject.GetComponent<NormalMole>().Hole = null;
                        hit.collider.gameObject.SetActive(false);//もぐら非表示
                    }
                }
            }
        });
	}
}
