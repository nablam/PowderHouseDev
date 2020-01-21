using System.Collections;
using UnityEngine;

public class AudioSyncFloat : AudioSyncer
{


    private IEnumerator MoveToScale(float _target)
    {
        float _curr = BlendShapeCurPos;
        float _initial = _curr;
        float _timer = 0;

        while (_curr != _target)
        {
            // _curr = Mathf.Lerp(_initial, _target, _timer / timeToBeat);
            _curr = Mathf.Lerp(_initial, _target, 1.5f);
            _timer += Time.deltaTime;

            BlendShapeCurPos = _curr;

            yield return null;
        }

        m_isBeat = false;

    }
    int index = 3;
    public override void OnUpdate()
    {
        base.OnUpdate();

        if (m_isBeat) return;


        BlendShapeCurPos = Mathf.Lerp(BlendShapeCurPos, restScale, restSmoothTime * Time.deltaTime);

        skinnedMeshRenderer.SetBlendShapeWeight(0, BlendShapeCurPos);
        skinnedMeshRenderer.SetBlendShapeWeight(1, BlendShapeCurPos);
    }

    public override void OnBeat()
    {
        base.OnBeat();

        StopCoroutine("MoveToScale");
        StartCoroutine("MoveToScale", beatScale);
    }

    public float beatScale;
    public float restScale;

    float BlendShapeCurPos = 1.0f;
    int blendShapeCount;
    SkinnedMeshRenderer skinnedMeshRenderer;

    void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

    }
}
