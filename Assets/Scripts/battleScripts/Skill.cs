using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    private float[] coolTimeArray = new float[9] {0, 0, 0, 0, 0, 0, 0, 0, 0};
    private float[] durationArray = new float[9] {0, 0, 0, 0, 0, 0, 0, 0, 0};
    private bool _firstReady = true;
    private bool _secondReady = true;
    private bool _thirdReady = true;
    private bool _firstCoolDown = false;
    private bool _secondCoolDown = false;
    private bool _thirdCoolDown = false;
    public bool firstSkillActivated = false;
    public bool secondSkillActivated = false;
    public bool thirdSkillActivated = false;
    public struct PlayerSkill {
        public float coolTime;
        public float cost;
        public float duration;
        public string name;
        public string type;

        public PlayerSkill(float coolTime, float cost, float duration, string name, string type) {
            this.coolTime = coolTime;
            this.cost = cost;
            this.duration = duration;
            this.name = name;
            this.type = type;
        }
    }
    private Player player;

    [SerializeField]
    private Image firstSkillImage;

    [SerializeField]
    private Image secondSkillImage;
    
    [SerializeField]
    private Image thirdSkillImage;

    PlayerSkill Askill1 = new PlayerSkill(5f, 30f, -1f, "concentration", "active");
    PlayerSkill Askill2 = new PlayerSkill(0f, 0f, -1f, "incisive-attack", "passive");
    PlayerSkill Askill3 = new PlayerSkill(0f, 150f, -1f, "judgement-cut", "passive");
    PlayerSkill Dskill1 = new PlayerSkill(10f, 30f, 5f, "endure", "active");
    PlayerSkill Dskill2 = new PlayerSkill(0f, 0f, -1f, "stone-skin", "passive");
    PlayerSkill Dskill3 = new PlayerSkill(30f, 150f, 10f, "invinsible", "active");
    PlayerSkill Uskill1 = new PlayerSkill(10f, 30f, 5f, "fast-move", "active");
    PlayerSkill Uskill2 = new PlayerSkill(30f, 60f, -1f, "heal", "active");
    PlayerSkill Uskill3 = new PlayerSkill(40f, 100f, 10f, "hyper-speed", "active");

    PlayerSkill[] skillArray = new PlayerSkill[9];
    private int conut = 0;

    void Start()
    {
        player = GetComponent<Player>();
        skillArray[0] = Askill1;
        skillArray[1] = Dskill1;
        skillArray[2] = Uskill1;
        skillArray[3] = Askill2;
        skillArray[4] = Dskill2;
        skillArray[5] = Uskill2;
        skillArray[6] = Askill3;
        skillArray[7] = Dskill3;
        skillArray[8] = Uskill3;

        foreach (PlayerSkill skill in skillArray) {
            coolTimeArray[conut] = skill.coolTime;
            durationArray[conut] = skill.duration;
            conut++;
        }
    }

    void Update()
    {
        if (firstSkillActivated) {
            if (_firstReady) {
                if (PlayerManager.instance.skill_select[0] == "attack1" & player.playerCurrentMp >= Askill1.cost) {
                    if (Askill1.type == "active") {
                        if (Input.GetKeyDown(KeyCode.Q)) {
                            player.playerCurrentMp -= Askill1.cost;
                            ActivateSkill(Askill1, 1);
                        }
                    } else if (Askill1.type == "passive") {
                        ActivateSkill(Askill1, 1);
                    }
                } else if (PlayerManager.instance.skill_select[0] == "defense1" & player.playerCurrentMp >= Dskill1.cost) {
                    if (Dskill1.type == "active") {
                        if (Input.GetKeyDown(KeyCode.Q)) {
                            player.playerCurrentMp -= Dskill1.cost;
                            ActivateSkill(Dskill1, 1);
                        }
                    } else if (Dskill1.type == "passive") {
                        ActivateSkill(Dskill1, 1);
                    }
                } else if (PlayerManager.instance.skill_select[0] == "util1" & player.playerCurrentMp >= Uskill1.cost) {
                    if (Uskill1.type == "active") {
                        if (Input.GetKeyDown(KeyCode.Q)) {
                            player.playerCurrentMp -= Uskill1.cost;
                            ActivateSkill(Uskill1, 1);
                        }
                    } else if (Uskill1.type == "passive") {
                        ActivateSkill(Uskill1, 1);
                    }
                }
            } else {
                if (PlayerManager.instance.skill_select[0] == "attack1") {
                    if (Askill1.duration == -1f & Askill1.type == "active") {
                        DeactivateSkill(Askill1, 1);
                    } else if (Askill1.duration > 0) {
                        Askill1.duration -= Time.deltaTime;
                    } else if (Askill1.duration <= 0) {
                        DeactivateSkill(Askill1, 1);
                    }
                    if (_firstCoolDown) {
                        Askill1.coolTime -= Time.deltaTime;
                        firstSkillImage.fillAmount = Askill1.coolTime / coolTimeArray[0];
                        if (Askill1.coolTime <= 0) {
                            Askill1.coolTime = coolTimeArray[0];
                            Askill1.duration = durationArray[0];
                            firstSkillImage.fillAmount = 1f;
                            _firstCoolDown = false;
                            _firstReady = true;
                        }
                    }
                } else if (PlayerManager.instance.skill_select[0] == "defense1") {
                    if (Dskill1.duration == -1f & Dskill1.type == "active") {
                        DeactivateSkill(Dskill1, 1);
                    } else if (Dskill1.duration > 0) {
                        Dskill1.duration -= Time.deltaTime;
                    } else if (Dskill1.duration <= 0) {
                        DeactivateSkill(Dskill1, 1);
                    }
                    if (_firstCoolDown) {
                        Dskill1.coolTime -= Time.deltaTime;
                        firstSkillImage.fillAmount = Dskill1.coolTime / coolTimeArray[1];
                        if (Dskill1.coolTime <= 0) {
                            Dskill1.coolTime = coolTimeArray[1];
                            Dskill1.duration = durationArray[1];
                            firstSkillImage.fillAmount = 1f;
                            _firstCoolDown = false;
                            _firstReady = true;
                        }
                    }
                } else if (PlayerManager.instance.skill_select[0] == "util1") {
                    if (Uskill1.duration == -1f & Uskill1.type == "active") {
                        DeactivateSkill(Uskill1, 1);
                    } else if (Uskill1.duration > 0) {
                        Uskill1.duration -= Time.deltaTime;
                    } else if (Uskill1.duration <= 0) {
                        DeactivateSkill(Uskill1, 1);
                    }
                    if (_firstCoolDown) {
                        Uskill1.coolTime -= Time.deltaTime;
                        firstSkillImage.fillAmount = Uskill1.coolTime / coolTimeArray[1];
                        if (Uskill1.coolTime <= 0) {
                            Uskill1.coolTime = coolTimeArray[2];
                            Uskill1.duration = durationArray[2];
                            firstSkillImage.fillAmount = 1f;
                            _firstCoolDown = false;
                            _firstReady = true;
                        }
                    }
                }
            }
        }

        if (secondSkillActivated) {
            if (_secondReady) {
                if (PlayerManager.instance.skill_select[1] == "attack2" & player.playerCurrentMp >= Askill2.cost) {
                    if (Askill2.type == "active") {
                        if (Input.GetKeyDown(KeyCode.W)) {
                            player.playerCurrentMp -= Askill2.cost;
                            ActivateSkill(Askill2, 2);
                        }
                    } else if (Askill2.type == "passive") {
                        ActivateSkill(Askill2, 2);
                    }
                } else if (PlayerManager.instance.skill_select[1] == "defense2" & player.playerCurrentMp >= Dskill2.cost) {
                    if (Dskill2.type == "active") {
                        if (Input.GetKeyDown(KeyCode.W)) {
                            player.playerCurrentMp -= Dskill2.cost;
                            ActivateSkill(Dskill2, 2);
                        }
                    } else if (Dskill2.type == "passive") {
                        ActivateSkill(Dskill2, 2);
                    }
                } else if (PlayerManager.instance.skill_select[1] == "util2" & player.playerCurrentMp >= Uskill2.cost) {
                    if (Uskill2.type == "active") {
                        if (Input.GetKeyDown(KeyCode.W)) {
                            player.playerCurrentMp -= Uskill2.cost;
                            ActivateSkill(Uskill2, 2);
                        }
                    } else if (Uskill2.type == "passive") {
                        ActivateSkill(Uskill2, 2);
                    }
                }
            } else {
                if (PlayerManager.instance.skill_select[1] == "attack2") {
                    if (Askill2.duration == -1f & Askill2.type == "active") {
                        DeactivateSkill(Askill2, 2);
                    } else if (Askill2.duration > 0) {
                        Askill2.duration -= Time.deltaTime;
                    } else if (Askill2.duration <= 0) {
                        DeactivateSkill(Askill2, 2);
                    }
                    if (_secondCoolDown) {
                        Askill2.coolTime -= Time.deltaTime;
                        secondSkillImage.fillAmount = Askill2.coolTime / coolTimeArray[3];
                        if (Askill2.coolTime <= 0) {
                            Askill2.coolTime = coolTimeArray[3];
                            Askill2.duration = durationArray[3];
                            secondSkillImage.fillAmount = 1f;
                            _secondCoolDown = false;
                            _secondReady = true;
                        }
                    }
                } else if (PlayerManager.instance.skill_select[1] == "defense2") {
                    if (Dskill2.duration == -1f & Dskill2.type == "active") {
                        DeactivateSkill(Dskill2, 2);
                    } else if (Dskill2.duration > 0) {
                        Dskill2.duration -= Time.deltaTime;
                    } else if (Dskill2.duration <= 0) {
                        DeactivateSkill(Dskill2, 2);
                    }
                    if (_secondCoolDown) {
                        Dskill2.coolTime -= Time.deltaTime;
                        secondSkillImage.fillAmount = Dskill2.coolTime / coolTimeArray[4];
                        if (Dskill2.coolTime <= 0) {
                            Dskill2.coolTime = coolTimeArray[4];
                            Dskill2.duration = durationArray[4];
                            secondSkillImage.fillAmount = 1f;
                            _secondCoolDown = false;
                            _secondReady = true;
                        }
                    }
                } else if (PlayerManager.instance.skill_select[1] == "util2") {
                    if (Uskill2.duration == -1f & Uskill2.type == "active") {
                        DeactivateSkill(Uskill2, 2);
                    } else if (Uskill2.duration > 0) {
                        Uskill2.duration -= Time.deltaTime;
                    } else if (Uskill2.duration <= 0) {
                        DeactivateSkill(Uskill2, 2);
                    }
                    if (_secondCoolDown) {
                        Uskill2.coolTime -= Time.deltaTime;
                        secondSkillImage.fillAmount = Uskill2.coolTime / coolTimeArray[5];
                        if (Uskill2.coolTime <= 0) {
                            Uskill2.coolTime = coolTimeArray[5];
                            Uskill2.duration = durationArray[5];
                            secondSkillImage.fillAmount = 1f;
                            _secondCoolDown = false;
                            _secondReady = true;
                        }
                    }
                }
            }
        }

        if (thirdSkillActivated) {
            if (_thirdReady) {
                if (PlayerManager.instance.skill_select[2] == "attack3" & player.playerCurrentMp >= Askill3.cost) {
                    if (Askill3.type == "active") {
                        if (Input.GetKeyDown(KeyCode.E)) {
                            player.playerCurrentMp -= Askill3.cost;
                            ActivateSkill(Askill3, 3);
                        }
                    } else if (Askill3.type == "passive") {
                        player.playerCurrentMp -= Askill3.cost;
                        ActivateSkill(Askill3, 3);
                    }
                } else if (PlayerManager.instance.skill_select[2] == "defense3" & player.playerCurrentMp >= Dskill3.cost) {
                    if (Dskill3.type == "active") {
                        if (Input.GetKeyDown(KeyCode.E)) {
                            player.playerCurrentMp -= Dskill3.cost;
                            ActivateSkill(Dskill3, 3);
                        }
                    } else if (Dskill3.type == "passive") {
                        ActivateSkill(Dskill3, 3);
                    }
                } else if (PlayerManager.instance.skill_select[2] == "util3" & player.playerCurrentMp >= Uskill3.cost) {
                    if (Uskill3.type == "active") {
                        if (Input.GetKeyDown(KeyCode.E)) {
                            player.playerCurrentMp -= Uskill3.cost;
                            ActivateSkill(Uskill3, 3);
                        }
                    } else if (Uskill3.type == "passive") {
                        ActivateSkill(Uskill3, 3);
                    }
                } else if (PlayerManager.instance.skill_select[2] == "attack3" & player.playerCurrentMp < Askill3.cost) {
                    PlayerManager.instance.judgementCutActivated = false;
                }
            } else {
                if (PlayerManager.instance.skill_select[2] == "attack3") {
                    if (Askill3.duration == -1f & Askill3.type == "active") {
                        DeactivateSkill(Askill3, 3);
                    } else if (Askill3.duration > 0) {
                        Askill3.duration -= Time.deltaTime;
                    } else if (Askill3.duration <= 0) {
                        DeactivateSkill(Askill3, 3);
                    }
                    if (_thirdCoolDown) {
                        Askill3.coolTime -= Time.deltaTime;
                        thirdSkillImage.fillAmount = Askill3.coolTime / coolTimeArray[6];
                        if (Askill3.coolTime <= 0) {
                            Askill3.coolTime = coolTimeArray[6];
                            Askill3.duration = durationArray[6];
                            thirdSkillImage.fillAmount = 1f;
                            _thirdCoolDown = false;
                            _thirdReady = true;
                        }
                    }
                } else if (PlayerManager.instance.skill_select[2] == "defense3") {
                    if (Dskill3.duration == -1f & Dskill3.type == "active") {
                        DeactivateSkill(Dskill3, 3);
                    } else if (Dskill3.duration > 0) {
                        Dskill3.duration -= Time.deltaTime;
                    } else if (Dskill3.duration <= 0) {
                        DeactivateSkill(Dskill3, 3);
                    }
                    if (_thirdCoolDown) {
                        Dskill3.coolTime -= Time.deltaTime;
                        thirdSkillImage.fillAmount = Dskill3.coolTime / coolTimeArray[7];
                        if (Dskill3.coolTime <= 0) {
                            Dskill3.coolTime = coolTimeArray[7];
                            Dskill3.duration = durationArray[7];
                            thirdSkillImage.fillAmount = 1f;
                            _thirdCoolDown = false;
                            _thirdReady = true;
                        }
                    }
                } else if (PlayerManager.instance.skill_select[2] == "util3") {
                    if (Uskill3.duration == -1f & Uskill3.type == "active") {
                        DeactivateSkill(Uskill3, 3);
                    } else if (Uskill3.duration > 0) {
                        Uskill3.duration -= Time.deltaTime;
                    } else if (Uskill3.duration <= 0) {
                        DeactivateSkill(Uskill3, 3);
                    }
                    if (_thirdCoolDown) {
                        Uskill3.coolTime -= Time.deltaTime;
                        thirdSkillImage.fillAmount = Uskill3.coolTime / coolTimeArray[8];
                        if (Uskill3.coolTime <= 0) {
                            Uskill3.coolTime = coolTimeArray[8];
                            Uskill3.duration = durationArray[8];
                            thirdSkillImage.fillAmount = 1f;
                            _thirdCoolDown = false;
                            _thirdReady = true;
                        }
                    }
                }
            }
        }

    }

    public void ActivateSkill(PlayerSkill skill, int order) {
        if (skill.name == "concentration") {
            PlayerManager.instance.stylishPoint += 2f;
            if (skill.type == "active") {
                if (order == 1) {
                    _firstReady = false;
                } else if (order == 2) {
                    _secondReady = false;
                } else if (order == 3) {
                    _thirdReady = false;
                }
            }
        } else if (skill.name == "incisive-attack") {
            PlayerManager.instance.increaseRate = 1.1f;
            if (skill.type == "active") {
                if (order == 1) {
                    _firstReady = false;
                } else if (order == 2) {
                    _secondReady = false;
                } else if (order == 3) {
                    _thirdReady = false;
                }
            }
        } else if (skill.name == "judgement-cut") {
            PlayerManager.instance.judgementCutActivated = true;
            if (skill.type == "active") {
                if (order == 1) {
                    _firstReady = false;
                } else if (order == 2) {
                    _secondReady = false;
                } else if (order == 3) {
                    _thirdReady = false;
                }
            }
        } else if (skill.name == "endure") {
            player.playerDEF *= 1.3f;
            if (skill.type == "active") {
                if (order == 1) {
                    _firstReady = false;
                } else if (order == 2) {
                    _secondReady = false;
                } else if (order == 3) {
                    _thirdReady = false;
                }
            }
        } else if (skill.name == "stone-skin") {
            player.reductionRate = 1.1f;
            player.playerDEF = PlayerManager.instance.playerStatDEF * player.reductionRate;
            if (skill.type == "active") {
                if (order == 1) {
                    _firstReady = false;
                } else if (order == 2) {
                    _secondReady = false;
                } else if (order == 3) {
                    _thirdReady = false;
                }
            }
        } else if (skill.name == "invincible") {
            player.playerDEF = 99f;
            if (skill.type == "active") {
                if (order == 1) {
                    _firstReady = false;
                } else if (order == 2) {
                    _secondReady = false;
                } else if (order == 3) {
                    _thirdReady = false;
                }
            }
        } else if (skill.name == "fast-move") {
            player.moveSpeed *= 1.3f;
            if (skill.type == "active") {
                if (order == 1) {
                    _firstReady = false;
                } else if (order == 2) {
                    _secondReady = false;
                } else if (order == 3) {
                    _thirdReady = false;
                }
            }
        } else if (skill.name == "heal") {
            player.playerCurrentHp += player.playerMaxHp * 0.3f;
            if (player.playerCurrentHp > player.playerMaxHp) {
                player.playerCurrentHp = player.playerMaxHp;
            }
            if (skill.type == "active") {
                if (order == 1) {
                    _firstReady = false;
                } else if (order == 2) {
                    _secondReady = false;
                } else if (order == 3) {
                    _thirdReady = false;
                }
            }
        } else if (skill.name == "hyper-speed") {
            PlayerManager.instance.enemySpeed = 0.1f;
            if (skill.type == "active") {
                if (order == 1) {
                    _firstReady = false;
                } else if (order == 2) {
                    _secondReady = false;
                } else if (order == 3) {
                    _thirdReady = false;
                }
            }
        }
    }

    public void DeactivateSkill(PlayerSkill skill, int order) {
        if (order == 1) {
            if (skill.name == "endure") {
                player.playerDEF = PlayerManager.instance.playerStatDEF;
            } else if (skill.name == "fast-move") {
                player.moveSpeed = 10f;
            }
            _firstCoolDown = true;
        }

            
        if (order == 3) {
            if (skill.name == "invinsible") {
                player.playerDEF = PlayerManager.instance.playerStatDEF;
            } else if (skill.name == "hyper-speed") {
                PlayerManager.instance.enemySpeed = 1f;
                }
            _thirdCoolDown = true;
        }
        
        
        if (skill.duration == -1f & skill.type == "active") {
            if (order == 1) {
                _firstCoolDown = true;
            } else if (order == 2) {
                _secondCoolDown = true;
            } else if (order == 3) {
                _thirdCoolDown = true;
            }
        }


        // if (_firstCoolDown) {
        //     skill.coolTime -= Time.deltaTime;
        // if (skill.coolTime <= 0) {
        //     skill.coolTime = coolTimeArray[0];
        //     _firstReady = true;
        //     }
        // }
    }
}