using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI notiText;

    [SerializeField]
    private Skill skillManager;

    public Button attack1;
    public Button attack2;
    public Button attack3;
    public Button defense1;
    public Button defense2;
    public Button defense3;
    public Button util1;
    public Button util2;
    public Button util3;
    
    public void Play() {
        Time.timeScale = 1f;
    }

    public void ATKPlus() {
        if (PlayerManager.instance.LVP >= 1) {
            PlayerManager.instance.playerStatATK++;
            PlayerManager.instance.LVP--;
        } else {
            notiText.SetText("Not enough LVP");
        }
    }

    public void DEFPlus() {
        if (PlayerManager.instance.LVP >= 1) {
            PlayerManager.instance.playerStatDEF += 0.1f;
            PlayerManager.instance.LVP--;
        } else {
            notiText.SetText("Not enough LVP");
        }
    }

    public void HPPlus() {
        if (PlayerManager.instance.LVP >= 1) {
            PlayerManager.instance.playerStatHP++;
            PlayerManager.instance.LVP--;
        } else {
            notiText.SetText("Not enough LVP");
        }
    }

    public void MPPlus() {
        if (PlayerManager.instance.LVP >= 1) {
            PlayerManager.instance.playerStatMP += 10f;
            PlayerManager.instance.LVP--;
        } else {
            notiText.SetText("Not enough LVP");
        }
    }

    public void Attack1() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[0] = "attack1";
            PlayerManager.instance.SP--;
            skillManager.firstSkillActivated = true;
            attack1.interactable = false;
            defense1.interactable = false;
            util1.interactable = false;
            attack2.interactable = true;
            defense2.interactable = true;
            util2.interactable = true;
        } else {
            notiText.SetText("Not Enough SP");
        }
    }

    public void Attack2() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[1] = "attack2";
            PlayerManager.instance.SP--;
            skillManager.secondSkillActivated = true;
            attack2.interactable = false;
            defense2.interactable = false;
            util2.interactable = false;
            attack3.interactable = true;
            defense3.interactable = true;
            util3.interactable = true;
            
        } else {
            notiText.SetText("Not Enough SP");
        }
    }

    public void Attack3() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[2] = "attack3";
            PlayerManager.instance.SP--;
            skillManager.thirdSkillActivated = true;
            attack3.interactable = false;
            defense3.interactable = false;
            util3.interactable = false;
        } else {
            notiText.SetText("Not Enough SP");
        }
    }

    public void Defense1() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[0] = "defense1";
            PlayerManager.instance.SP--;
            skillManager.firstSkillActivated = true;
            attack1.interactable = false;
            defense1.interactable = false;
            util1.interactable = false;
            attack2.interactable = true;
            defense2.interactable = true;
            util2.interactable = true;
        } else {
            notiText.SetText("Not Enough SP");
        }
    }

    public void Defense2() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[1] = "defense2";
            PlayerManager.instance.SP--;
            skillManager.secondSkillActivated = true;
            attack2.interactable = false;
            defense2.interactable = false;
            util2.interactable = false;
            attack3.interactable = true;
            defense3.interactable = true;
            util3.interactable = true;
        } else {
            notiText.SetText("Not Enough SP");
        }
    }

    public void Defense3() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[2] = "defense3";
            PlayerManager.instance.SP--;
            skillManager.thirdSkillActivated = true;
            attack3.interactable = false;
            defense3.interactable = false;
            util3.interactable = false;
        } else {
            notiText.SetText("Not Enough SP");
        }
    }

    public void Util1() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[0] = "util1";
            PlayerManager.instance.SP--;
            skillManager.firstSkillActivated = true;
            attack1.interactable = false;
            defense1.interactable = false;
            util1.interactable = false;
            attack2.interactable = true;
            defense2.interactable = true;
            util2.interactable = true;
        } else {
            notiText.SetText("Not Enough SP");
        }
    }

    public void Util2() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[1] = "util2";
            PlayerManager.instance.SP--;
            skillManager.secondSkillActivated = true;
            attack2.interactable = false;
            defense2.interactable = false;
            util2.interactable = false;
            attack3.interactable = true;
            defense3.interactable = true;
            util3.interactable = true;
        } else {
            notiText.SetText("Not Enough SP");
        }
    }

    public void Util3() {
        if (PlayerManager.instance.SP >= 1) {
            PlayerManager.instance.skill_select[2] = "util3";
            PlayerManager.instance.SP--;
            skillManager.thirdSkillActivated = true;
            attack3.interactable = false;
            defense3.interactable = false;
            util3.interactable = false;
        } else {
            notiText.SetText("Not Enough SP");
        }
    }
}