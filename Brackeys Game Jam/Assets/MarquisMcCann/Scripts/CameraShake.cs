using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator ShakeWithTime(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition; // Original location of the camera

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-.5f, .5f) * magnitude;
            float y = Random.Range(-.5f, .5f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }

    public IEnumerator ShakeWithBool(bool canShake, float magnitude)
    {
        Vector3 originalPos = transform.localPosition; // Original location of the camera

        if (canShake)
        {
            float x = Random.Range(-.5f, .5f) * magnitude;
            float y = Random.Range(-.5f, .5f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            yield return null;
        }
        else
        {
            transform.localPosition = originalPos;
        }
    }
}
