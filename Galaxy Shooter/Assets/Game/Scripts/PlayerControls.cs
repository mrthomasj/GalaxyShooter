using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private GameObject _laserPref;
    [SerializeField]
    private GameObject _tripShot;
    [SerializeField]
    private float _fireRate = 0.2f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private bool _shieldOn = false;
    [SerializeField]
    private GameObject[] _engines;



    private float _nextFire = 0.0f;
    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;
    private int _hitCount = 0;
    private int? _firstFailureRef;


    public bool tripleShotOn = false;
    public bool boosterOn = false;
   


    private void Awake()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _audioSource = GetComponent<AudioSource>();
        
    }


    // Start is called before the first frame update
    void Start()
    {
        _hitCount = 0;
        _firstFailureRef = null;

        transform.position = new Vector3(0, -4.15f, 0);
        
            if(_uiManager != null)
                _uiManager.UpdateLives(_lives);
        _spawnManager.RunSpawnRoutines();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
    }


    private void Movement()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * Time.deltaTime * _speed * hInput);
        transform.Translate(Vector3.up * Time.deltaTime * _speed * vInput);

        if (transform.position.y >= 1.9f)
        {
            transform.position = new Vector3(transform.position.x, 1.89f, transform.position.z);
        }
        else if (transform.position.y < -4.15f)
        {
            transform.position = new Vector3(transform.position.x, -4.15f, transform.position.z);
        }

        if (transform.position.x <= -5f)
        {
            transform.position = new Vector3(-4.99f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x >= 5f)
        {
            transform.position = new Vector3(4.99f, transform.position.y, transform.position.z);
        }
    }

    private void Shoot()
    {
        
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space) && Time.time > _nextFire )
        {
            _audioSource.Play();
            if (tripleShotOn == true)
            {
                Instantiate(_tripShot, transform.position, Quaternion.identity);
                
            }
            else
            {
                Instantiate(_laserPref, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            _nextFire = Time.time + _fireRate;
        }
    }

    public void TripleShotPowerEnable()
    {
        tripleShotOn = true;
        StartCoroutine(TripleShotPwrUpDisableRoutine());
    }
    public IEnumerator TripleShotPwrUpDisableRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        tripleShotOn = false;
    }

    public void ShieldEnable()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        _shieldOn = true;
    }

    private void ShieldDisable()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        _shieldOn = false;
    }


    public void BoosterEnable()
    {
        boosterOn = true;
        _speed = _speed * 1.5f;
        StartCoroutine(BoosterDisable());
    }

    public IEnumerator BoosterDisable()
    {
        yield return new WaitForSeconds(5.5f);
        _speed = 10f;
        boosterOn = false;
    }

    public void LifeManager()
    {
        if(_lives > 1)
        {
            if (_shieldOn)
                ShieldDisable();
            else
            {
                _hitCount++;
                if (_firstFailureRef == null && _hitCount == 1)
                {
                    _firstFailureRef = Random.Range(0, 2);
                    _engines[(int)_firstFailureRef].SetActive(true);
                }
                else if(_firstFailureRef != null && _hitCount == 2)
                {
                    switch (_firstFailureRef)
                    {
                        case 0:
                            _firstFailureRef = 1;
                            _engines[(int)_firstFailureRef].SetActive(true);
                            break;
                        case 1:
                            _firstFailureRef = 0;
                            _engines[(int)_firstFailureRef].SetActive(true);
                            break;
                    }
                }
                _lives--;
            }
                
                
        }
        else
        {
            if (_shieldOn)
            {
                ShieldDisable();
                return;
            }

            _lives--;
            Destroy(this.gameObject);
            _gameManager.gameRunning = _uiManager.GameOver(_gameManager.gameRunning);
        }
        Debug.Log($"Player Lives: {_lives}");
        _uiManager.UpdateLives(_lives);

    }
}
