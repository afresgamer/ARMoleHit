using UnityEngine;
using DG.Tweening;

public class MoleBase : MonoBehaviour {
    [SerializeField,Header("動くスピード")]
    private float Speed = 1;
    private const float MAX_HEIGHT = 0.28f;
    public Hole Hole { get; set; }
    Sequence sequence;

	public virtual void Start () {}

    public void EasyMove()
    {
        transform.DOMoveY(MAX_HEIGHT, Speed);
    }

    public void NormalMove()
    {
        DOTween.Init();
        sequence = DOTween.Sequence();
        sequence.OnStepComplete(() => Visible());
        sequence.Append(transform.DOMoveY(MAX_HEIGHT, Speed)).AppendInterval(2).
            Append(transform.DOMoveY(0,Speed)).AppendCallback(() => InVisible()).AppendInterval(1.5f)
            .SetLoops(-1,LoopType.Restart);
    }

    /// <summary>
    /// 見える
    /// </summary>
    public void Visible()
    {
        GetComponent<Collider>().enabled = true;
        for(int i = 0; i < transform.childCount; i++)
        {
            MeshRenderer renderer = transform.GetChild(i).GetComponent<MeshRenderer>();
            renderer.enabled = true;
        }
    }

    /// <summary>
    /// 見えない
    /// </summary>
    public void InVisible()
    {
        GetComponent<Collider>().enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            MeshRenderer renderer = transform.GetChild(i).GetComponent<MeshRenderer>();
            renderer.enabled = false;
        }
    }
}
