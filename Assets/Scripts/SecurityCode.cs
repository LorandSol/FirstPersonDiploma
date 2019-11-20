﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;
using UnityEngine.UI;

public class SecurityCode : MonoBehaviour
{
    public string[] code;
    //public TMP_Text display;
    public Text display;
    public Image indicator;
    public string codeOutput;
    public string securityCode;
    public bool isUnlocked,wrong;
    public float waitTimer;
    [SerializeField]
    public KeyCode[] numberPad;

    private void Awake()
    {
        indicator.color = Color.yellow;
    }
    public void KeyCodeInsert()
    {
        if (!(indicator.color == Color.yellow && wrong && isUnlocked))
        {
            indicator.color = Color.yellow;
        }
        if (code[code.Length - 1] != "*")
        {
            codeOutput = display.text;
            if (codeOutput == securityCode)
            {
                isUnlocked = true;
                indicator.color = Color.green;
            }
            else
            {
                wrong = true;
                indicator.color = Color.red;
            }
        }
        if(wrong)
        {
            waitTimer+=Time.deltaTime;
            if(waitTimer > 1f)
            {
                for (int d = 0; d < code.Length; d++)
                {
                    code[d] = "*";
                }
                display.text = code[0] + code[1] + code[2] + code[3];
                wrong = false;
                waitTimer = 0;
            }            
        }
        if (Input.anyKey)
        {
            for (int n = 0; n < numberPad.Length; n++)
            {
                if (Event.current.Equals(Event.KeyboardEvent(""+numberPad[n])))
                {
                    for (int d = 0; d < code.Length; d++)
                    {
                        if (code[d].Contains("*"))
                        {
                            code[d] = code[d].Replace("*", n.ToString());
                            display.text = code[0] + code[1] + code[2] + code[3];
                            return;
                        }
                    }   
                }
            }
        } 
    }
    private void OnGUI()
    {
        KeyCodeInsert();
    }
}