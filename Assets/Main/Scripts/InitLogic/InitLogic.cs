using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIModule.Instance.OpenForm<WND_MainTown>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
