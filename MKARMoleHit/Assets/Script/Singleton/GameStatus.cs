using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

public class GameStatus : SingletonMonoBehaviour<GameStatus> {

    /// <summary>
    /// 制限時間
    /// </summary>
    public FloatReactiveProperty TimeLimit { get; set; }
    public float GetTime() { return TimeLimit.Value; }
    
    /// <summary>
    /// スコア
    /// </summary>
    public IntReactiveProperty Score { get; set; }
    public string GetScore() { return Score.Value.ToString(); }
    
    /// <summary>
    /// 現在のモグラオブジェクト数
    /// </summary>
    public int NowCount { get; set; }

    /// <summary>
    /// ゲームスタートしてるかフラグ
    /// </summary>
    private BoolReactiveProperty GameStart { get; set; }
    public void SetGameStart(bool isStart) { GameStart.Value = isStart; }
    public bool GetGameStart() { return GameStart.Value; }

    /// <summary>
    /// シーン遷移（初期化する判定フラグあり）
    /// </summary>
    /// <param name="num"></param>
    /// <param name="isInit"></param>
    public void ChangeScene(int num, bool isInit)
    {
        SceneManager.LoadScene(num);
        if(isInit) Init();
    }

    /// <summary>
    /// パラメータ初期化
    /// </summary>
    public void Init()
    {
        TimeLimit = new FloatReactiveProperty(60.0f);
        Score = new IntReactiveProperty(0);
        NowCount = 0;
        GameStart = new BoolReactiveProperty(false);
    }
}
