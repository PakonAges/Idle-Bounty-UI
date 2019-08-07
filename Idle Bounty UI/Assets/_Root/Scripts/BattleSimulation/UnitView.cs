using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitView : MonoBehaviour
{
    public Transform UnitImage;
    public Slider HealthBar;
    public TextMeshProUGUI UnitsCounter;
    public TextMeshProUGUI DamageTxt;
    public Image AttackCoolDown;

    int mStartingSquadSize;
    bool showDmg = false;
    float dmgDisplayTimer = 0;

    internal void Setup(int Units)
    {
        mStartingSquadSize = Units;
        HealthBar.value = 1;
        UpdateUnitsCounter(Units);
        AttackCoolDown.fillAmount = 0;
        HideDmg();
    }

    private void Update()
    {
        if (showDmg)
        {
            dmgDisplayTimer += Time.deltaTime;
            if (dmgDisplayTimer >= 1.0)
            {
                HideDmg();
            }
        }
    }

    private void HideDmg()
    {
        showDmg = false;
        dmgDisplayTimer = 0;
        DamageTxt.text = string.Empty;
    }

    public void UpdateUnitsCounter(int Units)
    {
        UnitsCounter.text = Units.ToString() + "/" + mStartingSquadSize.ToString();
    }

    public void UpdateHealthBar(float value)
    {
        HealthBar.value = value;
    }

    public void UpdateAttackCD(float value)
    {
        AttackCoolDown.fillAmount = value;
    }

    internal void DeathSequence()
    {
        HealthBar.gameObject.SetActive(false);
        UnitsCounter.text = "x.x";
        AttackCoolDown.enabled = false;
        UnitImage.localScale = new Vector3(1, -1, 1);
    }

    public void DisplayDamage(int Damage)
    {
        DamageTxt.text = Damage.ToString();
        showDmg = true;
    }
}
