using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField,Header("スコアテキスト")]
    private Text ScoreText;
    [SerializeField, Header("制限時間テキスト")]
    private Text Timer;
    [SerializeField, Header("Timerスピード")]
    private float TimerSpeed = 1;
    [SerializeField, Header("リザルト画面")]
    private GameObject Result;
    [SerializeField, Header("スタートボタン")]
    private Button StartButton;
    [SerializeField, Header("リスタートボタン")]
    private GameObject ReStartButton;

    void Start () {
        GameStatus.Instance.Init();
        Result.SetActive(false);
        StartButton.onClick.AddListener(() => GameStatus.Instance.SetGameStart(true));
        ReStartButton.SetActive(false);

        //スコア処理
        this.UpdateAsObservable().Where(_ => GameStatus.Instance.GetGameStart()).Subscribe(_ =>
        {
            ScoreText.text = "スコア: " + GameStatus.Instance.GetScore();

            GameStatus.Instance.TimeLimit.Value -= TimerSpeed * Time.deltaTime;
            //タイムアップしたらリザルト画面
            if (GameStatus.Instance.TimeLimit.Value < 0)
            {
                GameStatus.Instance.TimeLimit.Value = 0;
                VisibleResult();
            }
        });
        //タイマー処理
        GameStatus.Instance.TimeLimit.Subscribe(_ =>
        {
            Timer.text = "制限時間: " + GameStatus.Instance.GetTime().ToString("f0");
        });
    }

    /// <summary>
    /// リザルト画面表示
    /// </summary>
    private void VisibleResult()
    {
        Result.SetActive(true);
        ReStartButton.SetActive(true);
        //シーン遷移イベント登録
        ReStartButton.GetComponent<Button>().onClick.AddListener(() => GameStatus.Instance.ChangeScene(0, true));
        //モグラオブジェクトを全て取得
        NormalMole[] moles = FindObjectsOfType<NormalMole>();
        //オブジェクトを非表示
        foreach(var obj in moles)
        {
            obj.gameObject.SetActive(false);
        }
    }
}
