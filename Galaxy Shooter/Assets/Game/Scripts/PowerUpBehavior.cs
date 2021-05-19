using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehavior : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int pwrUpId; // 0 = TriShot, 1 = SpeedBoost, 2 = Shields
    [SerializeField]
    private AudioClip _clip;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerControls p = other.GetComponent<PlayerControls>();
            if(p != null)
            {
                switch (pwrUpId)
                {
                    case 0:
                        p.TripleShotPowerEnable();
                        break;
                    case 1:
                        p.BoosterEnable();
                        break;
                    case 2:
                        p.ShieldEnable();
                        break;
                }
            }

            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
}
