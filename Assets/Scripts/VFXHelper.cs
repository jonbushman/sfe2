using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class VFXHelper : MonoBehaviour
{
    public Transform Target;

    [Header("Pulse Settings")]
    public bool PulseOn;
    public float Radius;
    public float Speed;
    public int NumPulses;
    public float DelayBeforeReset;
    public float DelayBetweenPulses;
    public float MinimumAlpha;



    public void StartPulse(float startDelay)
    {
        StopAllCoroutines();
        StartCoroutine(PulseCoroutine(startDelay));
    }

    private IEnumerator PulseCoroutine(float startDelay)
    {
        var resetTimer = 0f;
        var pulseTimer = 0f;
        var resetScale = new Vector3(0f, 0f, 1f);
        var extraTargets = new List<Transform>();

        while (resetTimer < startDelay)
        {
            resetTimer += Time.deltaTime;
            yield return null;
        }
        resetTimer = 0f;

        Target.gameObject.SetActive(true);
        Target.localScale = resetScale;
        for (var i = 1; i < NumPulses; i++)
        {
            extraTargets.Add(Instantiate(Target.gameObject).transform);
            extraTargets[i].parent = transform;
            extraTargets[i].localPosition = Target.localPosition;
            extraTargets[i].localScale = resetScale;
        }

        while (PulseOn)
        {
            var timer = 0f;
            var color = Target.GetComponent<SpriteRenderer>().color ;
            while (Target.transform.localScale.x < Radius)
            {
                timer += Time.deltaTime;
                var x = Target.transform.localScale.x;
                x = Mathf.Sqrt(timer * Speed);

                color.a = (Radius - x + MinimumAlpha) / Radius;
                Target.GetComponent<SpriteRenderer>().color = color;

                Target.transform.localScale = new Vector3(x, x, 1f); ;
                yield return null;
            }

            Target.transform.localScale = resetScale;
            color.a = 1;
            Target.GetComponent<SpriteRenderer>().color = color;
            while (resetTimer < DelayBeforeReset)
            {
                resetTimer += Time.deltaTime;    
                yield return null;
            }
                resetTimer = 0f;

            yield return null;
        }

        foreach(var target in extraTargets)
        {
            GameObject.Destroy(target.gameObject);
        }
        Target.gameObject.SetActive(false);
    }
}
