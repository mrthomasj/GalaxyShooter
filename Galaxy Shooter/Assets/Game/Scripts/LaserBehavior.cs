using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void Movement()
    {
        transform.Translate(Vector3.up * 25 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            EnemyBehavior e = other.GetComponent<EnemyBehavior>();
            if(e != null)
            {
                e.LifeManager();
                if(this.transform.parent != null)
                {
                    Destroy(this.transform.parent.gameObject);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
