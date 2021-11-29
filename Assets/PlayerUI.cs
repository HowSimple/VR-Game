using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    private Health hp;
    private AudioSource audio;
    public TMP_Text healthUI;
    //public AudioClip switchSoundEffect;
    public Image jetRechargeBar;
    private float jetCharge;

    private void Awake()
    {
       
        
        //dash = this.GetComponent<Dash>();
        hp = this.GetComponent<Health>();
      
        jetRechargeBar.fillAmount= (100);
    }
    private void FixedUpdate()
    {
        jetCharge = this.GetComponent<PlayerMovement>().jetCharge;
        jetRechargeBar.fillAmount= jetCharge;
        healthUI.text = "Health: "  +hp.healthPoints.ToString();

    }

}