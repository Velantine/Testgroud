﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour {
    public UnityEvent actions;

    void OnTriggerEnter(Collider other) {
        actions.Invoke();
	}
}
