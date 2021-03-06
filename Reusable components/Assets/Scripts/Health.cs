using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth 
{
    [SerializeField] private int _health;
    [SerializeField] private float _knockBackAngle;
    [SerializeField] private float _knockBackForce;
    [SerializeField] [Range(0.0f, 1.5f)] private float _knockBacklag;
    [SerializeField] private int _lives;
    [SerializeField] GameObject _deathParticles;

    private ParticleSystem _particles;
    private Rigidbody2D _rb;
    private bool _knocked;
    private float time;

    private bool _dead;

    //public event onDeath OnDeath = delegate { };

    //delegate/event maken voor onDeath. Add particle system/active particle, death animation, destory en of voor de player een game over screen met een respawn/retry button?

    public int HealthInt { get { return _health; } set { _health = value; } }

    public int LivesInt { get { return _lives; } set { _lives = value; } }

    public bool isDead { get { return _dead; } }

    // Start is called before the first frame update
    void Start()
    {
        _particles = GetComponent<ParticleSystem>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_health <= 0)
        {
            if (GetComponent<IPlayer>() != null)
            {
                _dead = true;
                OnDeath();
            }
            else if (GetComponent<IEnemy>() != null)
                Destroy(gameObject);
            else
            {
                Destroy(gameObject);
            }
        }
        else
            _dead = false;

        if (_knocked)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;

            }
            else
                _knocked = false;
        }
    }

    public bool knocked
    {
        get { return _knocked; }
    }

    //UI maken voor de player waar je de health kan zien.

    public void TakeDamage(int damage, bool flipX)
    {
        _health -= damage;
        Debug.Log("DAMAGEEE++++++++++++++++++++++++++++++");
        
        if(_particles != null)
            _particles.Play();
        _knocked = true;
        time = _knockBacklag;
        knockBack(flipX);
        
    }

    public void OnDeath()
    {
        //particle effect loop voor de player??
        if(_particles != null)
            _particles.Play();
        
        Debug.Log("I AM DEAD");
        if(GetComponent<IPlayer>() != null)
        {
            if(knocked == false)
            {
                _rb.velocity = Vector2.zero;
            }
        }
    }

    void knockBack(bool flipX)
    {
        ///Moet naar de richting dat de enemy faced
        ///verander het naar sprite flip x :(
        Vector2 Angle = Vector2.zero;
        Vector2 lol = new Vector2(100, 0);
        if (flipX)
            Angle = new Vector2(Mathf.Cos((45 + 180) * Mathf.Deg2Rad) * _knockBackForce, Mathf.Sin(45 * Mathf.Deg2Rad) * _knockBackForce);
        else
            Angle = new Vector2(Mathf.Cos(45 * Mathf.Deg2Rad) * _knockBackForce, Mathf.Sin(45 * Mathf.Deg2Rad) * _knockBackForce);
        _rb.velocity = Angle;

        
        Debug.Log(Angle);
    }

    private void OnDestroy()
    {
        if(_deathParticles != null)
            Instantiate(_deathParticles, gameObject.transform.position, Quaternion.identity);
    }
}
