using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyExplPrfb;

    private UIManager uiManager;

    private void Awake()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    void Start()
    {
        transform.position = new Vector3(Random.Range(-5f, 5f), 6.2f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -5.85f)
        {
            //float rndX = Random.Range(-5f, 5f);
            //transform.position = new Vector3(rndX, 6f, 0);
            Destroy(this.gameObject);
        }
    }

    public void LifeManager()
    {
        //Instantiate(_enemy);
        Instantiate(_enemyExplPrfb, transform.position, Quaternion.identity);
           
        Destroy(this.gameObject);
        uiManager.UpdateScore();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if(other.tag == "Player")
        {
            PlayerControls p = other.GetComponent<PlayerControls>();
            if(p != null)
            {
                p.LifeManager();
                LifeManager();
            }
        }
    }

}
