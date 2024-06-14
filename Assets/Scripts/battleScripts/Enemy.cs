using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer enemySpriteRenderer;
    private Transform target;
    public float Hp = 10f;
    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;
    public float fiedlOfVision = 5f;
    public float enemyATK = 1.5f;
    public bool isHit = false;
    private float _timer = 0f;
    private float _xDistance = 0;
    private float _yDistance = 0;
    private Vector3 dir;
    public Animator animator;
    

    private void Awake() {
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {

    }


    void Update()
    {
        if (!PlayerManager.instance.gameOver) {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance <= fiedlOfVision) {
                MoveToTarget();
            }
            if (isHit) {
                if (_timer >= 0.3f) {
                    enemySpriteRenderer.color = new Color(1, 1, 1, 1);
                    _timer = 0f;
                    isHit = false;
                } else {
                    _timer += Time.deltaTime;
                }
            }
        }
    }

    private void MoveToTarget() {
        // Vector2 direction = new Vector2(transform.position.x - target.position.x, transform.position.y - target.position.y);
        _xDistance = transform.position.x - target.position.x;
        _yDistance = transform.position.y - target.position.y;
        

        if (Mathf.Abs(_xDistance) > Mathf.Abs(_yDistance)) {
            if (_xDistance > 0) {
                dir = new Vector3(1, 0, 0);
                animator.SetTrigger("left");
            } else if (_xDistance < 0) {
                dir = new Vector3(-1, 0, 0);
                animator.SetTrigger("right");
            }
        } else if (Mathf.Abs(_xDistance) < Mathf.Abs(_yDistance)) {
            if (_yDistance > 0) {
                dir = new Vector3(0, 1, 0);
                animator.SetTrigger("down");
            } else if (_xDistance < 0) {
                dir = new Vector3(0, -1, 0);
                animator.SetTrigger("up");
            }
        } else if (Mathf.Abs(_xDistance) > Mathf.Abs(_yDistance)) {
            if (_xDistance > 0) {
                dir = new Vector3(1, 0, 0);
                animator.SetTrigger("left");
            } else if (_xDistance < 0) {
                dir = new Vector3(-1, 0, 0);
                animator.SetTrigger("right");
            }
        }


        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        // Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, PlayerManager.instance.enemySpeed * rotateSpeed * Time.deltaTime);

        // transform.rotation = rotation;



        transform.position += (-dir * moveSpeed * Time.deltaTime * PlayerManager.instance.enemySpeed);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Weapon") {
            isHit = true;
            PlayerManager.instance.checkHit = true;
            enemySpriteRenderer.color = new Color(253/255f, 154/255f, 154/255f, 1);
            Player player = other.gameObject.transform.parent.gameObject.GetComponent<Player>();
            Hp -= player.playerATK;
            player.playerCurrentMp += 2f;
            
            if (Hp <= 0) {
                PlayerManager.instance.stylishPoint += 0.5f;
                PlayerManager.instance.LVP++;
                PlayerManager.instance.tempLVP++;
                Destroy(gameObject);
            }
        }
    }
}