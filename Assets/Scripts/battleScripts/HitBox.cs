using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitBox : MonoBehaviour
{
    private Hit hit;
    private Player player;
    private GameObject hitbox;

    void Start()
    {
        hit = FindObjectOfType<Hit>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        hitbox = gameObject;
    }

    void Update()
    {
        Vector3 toSmall = new Vector3(-1f, -1f, 0f);

        transform.localScale += toSmall * PlayerManager.instance.hitBoxMoveSpeed * Time.unscaledDeltaTime;

        transform.position = player.transform.position;

        if (Input.GetKeyDown(KeyCode.A)) {
            if (PlayerManager.instance.checkHit) {
                if (transform.localScale.x > 0.3f && transform.localScale.x < 0.6f) {
                    Destroy(hitbox);
                    PlayerManager.instance.checkHit = false;
                    PlayerManager.instance.isreinforced = true;
                    PlayerManager.instance.stylishPoint += PlayerManager.instance.increaseRate;
                    hit.SetCriticalText("Perfect");
                } else {
                    Destroy(hitbox);
                    PlayerManager.instance.checkHit = false;
                    PlayerManager.instance.stylishPoint -= 0.4f;
                    hit.SetCriticalText("Miss");
                }
            } else {
                Destroy(hitbox);
                PlayerManager.instance.stylishPoint -= 0.4f;
                hit.SetCriticalText("Miss");    
            }
        }


        if (gameObject.transform.localScale.x < 0) {
            Destroy(hitbox);
            // GameManager.instance.stylishPoint -= 0.5f;
            // hit.SetCriticalText("Miss");
        }
    }
}
