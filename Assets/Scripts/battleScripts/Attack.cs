using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    
    [SerializeField]
    Player player;
    private PolygonCollider2D rightWeaponCollider;
    private PolygonCollider2D leftWeaponCollider;
    private int _count = 0;
    private bool _attacked = false;
    private float _attackDelay;
    private bool _timeSet = false;
    private bool _ready = true;
    public HitBoxSpawner spawner;


    void Start()
    {
        rightWeaponCollider = transform.GetChild(0).GetComponent<PolygonCollider2D>();
        leftWeaponCollider = transform.GetChild(1).GetComponent<PolygonCollider2D>();
        rightWeaponCollider.enabled = false;
        leftWeaponCollider.enabled = false;
    }


    void Update()
    {
        if (PlayerManager.instance.fever) {
            AttackReinforced(true);
            if (Input.GetKeyDown(KeyCode.A) && _count == 1 && _ready) {
                _attacked = true;
                _ready = false;
                animator.SetTrigger("reinforced2");
                _count = 0;
            } else if (Input.GetKeyDown(KeyCode.A) && _count == 0 && _ready) {
                _attacked = true;
                _ready = false;
                animator.SetTrigger("reinforced");
                _count = 1;
            }
        } else if (!PlayerManager.instance.fever) {
            AttackReinforced(false);
            if (Input.GetKeyDown(KeyCode.A) && _count == 1 && _ready) {
                _attacked = true;
                _ready = false;
                animator.SetTrigger("second_attack");
                _count = 0;
            } else if (Input.GetKeyDown(KeyCode.A) && _count == 0 && _ready) {
                _attacked = true;
                _ready = false;
                animator.SetTrigger("first_attack");
                _count = 1;
            }
        }

        if (_attacked) {
            if (!PlayerManager.instance.fever) {
                if (_timeSet) {
                    StartDelay();
                } else if (!_timeSet) {
                    SetDelay(0.5f);
                }
            } else if (PlayerManager.instance.fever) {
                if (_timeSet) {
                    StartDelay();
                } else if (!_timeSet) {
                    SetDelay(0.3f);
                }
            }
        }
    }

    public void EnableCollider() {
        if (player.isLeft == false) {
            rightWeaponCollider.enabled = true;
        } else if (player.isLeft == true) {
            leftWeaponCollider.enabled = true;
        }
    }

    public void DisableCollider() {
        rightWeaponCollider.enabled = false;
        leftWeaponCollider.enabled = false;
    }

    public void SetDelay(float delay) {
        _timeSet = true;
        _attackDelay = delay;
    }

    public void StartDelay() {
        _attackDelay -= Time.deltaTime;
        if (_attackDelay <= 0f) {
            _timeSet = false;
            _ready = true;
            _attacked = false;
        }
    }

    public void AttackReinforced(bool activated) {
        if (activated) {
            player.playerATK *= 2f;
        } else {
            player.playerATK = PlayerManager.instance.playerStatATK;
        }
    }
}
