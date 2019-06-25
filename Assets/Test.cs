/**
 *Copyright(C) 2019 by #COMPANY#
 *All rights reserved.
 *FileName:     #SCRIPTFULLNAME#
 *Author:       #AUTHOR#
 *Version:      #VERSION#
 *UnityVersion：#UNITYVERSION#
 *Date:         #DATE#
 *Description:   
 *History:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    CutManager cut = new CutManager();
	// Use this for initialization
	void Start () {
        cut.AddCutControl(transform.gameObject);
        cut.AddCutObj(transform.GetChild(0).gameObject);
    }
	
	// Update is called once per frame
	void Update () {
     
	}
}
