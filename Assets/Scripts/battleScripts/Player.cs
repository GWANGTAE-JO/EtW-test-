using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    
    private Rigidbody2D playerRigidbody;
    private SpriteRenderer playerSpriteRenderer;
    private BoxCollider2D plyaerCollider;

    public bool isLeft = false;
    public float moveSpeed = 10f;
    public float playerMaxHp;
    public float playerCurrentHp;
    public float playerMaxMp;
    public float playerCurrentMp;
    public float playerATK = 1f;
    public float playerDEF = 1f;
    public float reductionRate = 1f;
    private float _godModeTimer = 1f;
    private bool _isHit = false;

    private void Awake() {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        plyaerCollider = GetComponent<BoxCollider2D>();

        // playerMaxHp = PlayerManager.instance.playerStatHP;
        playerCurrentHp = playerMaxHp;
        // playerMaxMp = PlayerManager.instance.playerStatMP;
        playerCurrentMp = playerMaxMp;
        // playerATK = PlayerManager.instance.playerStatATK;
        // playerDEF = PlayerManager.instance.playerStatDEF;
        StartCoroutine("MPRegeneration");
    }

    void Start()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        plyaerCollider = GetComponent<BoxCollider2D>();

        playerMaxHp = PlayerManager.instance.playerStatHP;
        playerCurrentHp = playerMaxHp;
        playerMaxMp = PlayerManager.instance.playerStatMP;
        playerCurrentMp = playerMaxMp;
        playerATK = PlayerManager.instance.playerStatATK;
        playerDEF = PlayerManager.instance.playerStatDEF * reductionRate;
        StartCoroutine("MPRegeneration");
    }

    IEnumerator MPRegeneration() {
        while (true) {
            playerCurrentMp += 1f;
            yield return new WaitForSecondsRealtime(3f);
        }
    }



    void Update()
    {
        playerMaxHp = PlayerManager.instance.playerStatHP;
        playerMaxMp = PlayerManager.instance.playerStatMP;

        if (playerCurrentHp <= 0) {
            PlayerManager.instance.gameOver = true;
            Destroy(gameObject);
        }

        if (playerCurrentMp > playerMaxMp) {
            playerCurrentMp = playerMaxMp;
        }


        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("move")) {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {
                animator.SetBool("idle", true);
            }
        }

        if (_isHit) {
            if (_godModeTimer <= 0f) {
                plyaerCollider.enabled = true;
                playerSpriteRenderer.color = new Color(1, 1, 1, 1);
                _godModeTimer = 1f;
                _isHit = false;
            } else {
                _godModeTimer -= Time.deltaTime;
            }
        }


        float deltaX = Input.GetAxis("Horizontal");
        float deltaY = Input.GetAxis("Vertical");

        Vector3 velocity = Vector3.zero;
        velocity.x            = deltaX;
        velocity.y            = deltaY;
        playerRigidbody.velocity = velocity * moveSpeed;

        if (velocity.x != 0.0f)
        {
            bool flipped = velocity.x < 0.0f;
            this.transform.rotation = Quaternion.Euler(new Vector3(0.0f, ( flipped ? 180.0f : 0.0f ), 0.0f));
        }

        animator.SetFloat("Speed",Mathf.Abs(velocity.magnitude * moveSpeed));
    }



    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Wolf") {
            Enemy hitEnemy = other.gameObject.GetComponent<Enemy>();
            float damage = hitEnemy.enemyATK - playerDEF;
            if (damage < 0) {
                damage = 0;
            }
            playerCurrentHp -= damage;
            plyaerCollider.enabled = false;
            PlayerManager.instance.stylishPoint -= 0.5f;
            _isHit = true;
            playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        } else if (other.gameObject.tag == "Golem") {
            Enemy hitEnemy = other.gameObject.GetComponent<Enemy>();
            float damage = hitEnemy.enemyATK - playerDEF;
            if (damage < 0) {
                damage = 0;
            }
            playerCurrentHp -= damage;
            plyaerCollider.enabled = false;
            PlayerManager.instance.stylishPoint -= 0.5f;
            _isHit = true;
            playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        } else if (other.gameObject.tag == "Eldlich") {
            Enemy hitEnemy = other.gameObject.GetComponent<Enemy>();
            float damage = hitEnemy.enemyATK - playerDEF;
            if (damage < 0) {
                damage = 0;
            }
            playerCurrentHp -= damage;
            plyaerCollider.enabled = false;
            PlayerManager.instance.stylishPoint -= 1f;
            _isHit = true;
            playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }
}