using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UDL.Core;

public class Main : AbstractView
{
    private IEnumerator Start()
    {
        GameManager.Create().AddTo(this);
        MapManager.Create().AddTo(this);
        yield return null;
        PlayerScript.Create().AddTo(this);
    }
}
