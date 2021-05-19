using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDestructor : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(TimedDestruction());
    }

    private IEnumerator TimedDestruction()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
