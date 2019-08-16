using UnityEngine;
using TMPro;
using UniRx;

public class Result : MonoBehaviour {

    [SerializeField, Header("結果発表スコア")]
    private TextMeshPro resultText;

	void Start () {
        //最終スコア取得
        GameStatus.Instance.TimeLimit.Where(_ => GameStatus.Instance.TimeLimit.Value == 0).Subscribe(_ => 
        {
            resultText.text = "スコア: " + GameStatus.Instance.GetScore();
        });
	}
}
