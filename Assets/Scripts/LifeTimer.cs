using System;
using System.Collections;
using UnityEngine;

public class LifeTimer : MonoBehaviour
{
    private Coroutine _countdownRoutine;

    public event Action<float> ProgressChanged;
    public event Action Expired;

    public void StartCountdown(float duration)
    {
        _countdownRoutine = StartCoroutine(Countdown(duration));
    }

    public void StopCountdown()
    {
        if (_countdownRoutine == null)
            return;

        StopCoroutine(_countdownRoutine);
        _countdownRoutine = null;
    }

    private IEnumerator Countdown(float duration)
    {
        float elapsedTime = 0f;
        ProgressChanged?.Invoke(1f);

        while (elapsedTime < duration)
        {
            yield return null;
            elapsedTime += Time.deltaTime;

            float progress = Mathf.Clamp01(1f - elapsedTime / duration);
            ProgressChanged?.Invoke(progress);
        }

        _countdownRoutine = null;
        Expired?.Invoke();
    }
}
