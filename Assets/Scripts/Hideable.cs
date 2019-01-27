﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hideable : MonoBehaviour
{
    public void Register()
    {
        PlatformToggle.instance.Register(gameObject);
    }
}
