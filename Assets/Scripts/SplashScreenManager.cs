using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAction());
    }

    IEnumerator StartAction()
    {
        StatusManager.Instance.Message("Loading...");

        yield return new WaitForSeconds(2.0f);

       PageNavigator.Instance.NavAction(PageSet.level);

    }
}
