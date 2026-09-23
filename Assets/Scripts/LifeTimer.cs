using System;
using System.Collections;
using UnityEngine;

public class LifeTimer : MonoBehaviour
{
    public event Action Expired;

    private Coroutine _countdownRoutine;

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
        yield return new WaitForSeconds(duration);
        Expired?.Invoke();
        _countdownRoutine = null;
    }
}
