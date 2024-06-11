using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;
    public float hitBoxMoveSpeed = 1f;
    public bool isreinforced = false;
    public int meditationCount = 0;
    public bool isReady = false;
    public float stylishPoint = 0f;
    public float increaseRate = 0.8f;
    public bool fever = false;
    public int LVP = 0;
    public int tempLVP = 0;
    public int SP = 0;
    public float playerStatATK;
    public float playerStatDEF;
    public float playerStatHP;
    public float playerStatMP;
    public float enemySpeed = 1f;
    public bool checkHit = false;
    public bool gameOver = false;
    public string[] skill_select = new string[]{"", "", ""};
    public bool judgementCutActivated = false;
    private float _feverTimer = 5f;
    private float _realTimer = 1f;
    private bool _pausePanelOpen = false;
    private bool _statusPanelOpen = false;

    [SerializeField]
    private TextMeshProUGUI stylishRank;

    [SerializeField]
    private Canvas ui;

    [SerializeField]
    private TextMeshProUGUI levelPoint_text;

    [SerializeField]
    private TextMeshProUGUI skillPoint_text;

    [SerializeField]
    private TextMeshProUGUI ATK_text;

    [SerializeField]
    private TextMeshProUGUI DEF_text;

    [SerializeField]
    private TextMeshProUGUI HP_text;

    [SerializeField]
    private TextMeshProUGUI MP_text;
    
    [SerializeField]
    private Player player;

    void Awake() {
        if (instance == null) {
            instance = this;
        }

        playerStatATK = 1f;
        playerStatDEF = 1f;
        playerStatHP = 20f;
        playerStatMP = 100f;
    }

    void Start() {
        playerStatATK = 1f;
        playerStatDEF = 1f;
        playerStatHP = 20f;
        playerStatMP = 100f;
    }

    void Update()
    {
        if (tempLVP == 50) {
            SP++;
            tempLVP = 0;
        }
        levelPoint_text.SetText("point : " + LVP.ToString());
        skillPoint_text.SetText("skill point : " + SP.ToString());
        ATK_text.SetText(playerStatATK.ToString());
        DEF_text.SetText(playerStatDEF.ToString());
        HP_text.SetText(playerStatHP.ToString());
        MP_text.SetText(playerStatMP.ToString());

        if (stylishPoint < 0f) {
            stylishPoint = 0f;
        }

        if (_realTimer <= 0) {
            stylishPoint -= 0.1f;
            _realTimer = 1f;
        } else {
            _realTimer -= Time.deltaTime;
        }

        JudgeRank();

        if (fever) {
            player.moveSpeed = 15f;
            _feverTimer -= Time.deltaTime;
            if (_feverTimer <= 0) {
                if (judgementCutActivated) {
                    GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
                    int countOfEnemy = enemyArray.Length;
                    LVP += countOfEnemy;
                    foreach (GameObject tGO in enemyArray) {
                        Destroy(tGO);
                    }
                }
                
                player.moveSpeed = 10f;
                _feverTimer = 5f;
                stylishPoint = 2f;
                fever = false;
            }
        }

        if (gameOver) {
            Time.timeScale = 0f;
            ui.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!_pausePanelOpen) {
                Time.timeScale = 0f;
                ui.transform.GetChild(1).gameObject.SetActive(true);
                _pausePanelOpen = true;
            } else {
                Time.timeScale = 1f;
                ui.transform.GetChild(1).gameObject.SetActive(false);
                _pausePanelOpen = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.K)) {
            if (!_statusPanelOpen) {
                ui.transform.GetChild(2).gameObject.SetActive(true);
                _statusPanelOpen = true;
            } else {
                ui.transform.GetChild(2).gameObject.SetActive(false);
                _statusPanelOpen = false;
            }
        }
    }

    public void JudgeRank() {
        if (stylishPoint < 2f) {
            stylishRank.SetText("D");
        } else if (stylishPoint >= 2f && stylishPoint < 4f) {
            stylishRank.SetText("C");
        } else if (stylishPoint >= 4f && stylishPoint < 6f) {
            stylishRank.SetText("B");
        } else if (stylishPoint >= 6f && stylishPoint < 8f) {
            stylishRank.SetText("A");
        } else if (stylishPoint >= 8f && stylishPoint < 10f) {
            stylishRank.SetText("S");
        } else if (stylishPoint >= 10f) {
            stylishRank.SetText("SS");
            fever = true;
        }
    }
}