using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakingRange;

    public void shake()
    {
        StopAllCoroutines();
        StartCoroutine(shaking());
    }

    public IEnumerator shaking()
    {
        Vector3 originPos = transform.position;
        float shakingTime = 0;

        while (shakingTime < 0.5f)
        {
            shakingTime += Time.deltaTime;
            transform.position = originPos + new Vector3(Random.Range(-shakingRange, shakingRange), Random.Range(-shakingRange, shakingRange), 0);
            yield return null;
        }

        transform.position = originPos;
    }
}
