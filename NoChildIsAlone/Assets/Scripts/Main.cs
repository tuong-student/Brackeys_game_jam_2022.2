using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UDL.Core;

public class Main : AbstractView
{
    private void Start()
    {
        PlayerScript.Create().AddTo(this);
    }
}
