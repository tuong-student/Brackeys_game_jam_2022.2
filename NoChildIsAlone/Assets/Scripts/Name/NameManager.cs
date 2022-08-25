using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NameManager : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text requestName, requestGender;
    [SerializeField] Text tableRequestName;
    [SerializeField] Transform tableTranform;

    public static NameManager InsNameManager;

    private void Awake()
    {
        if (InsNameManager == null) InsNameManager = this;
        HideNameText();
    }

    private void Update()
    {
        tableRequestName.text = requestName.text;
        tableRequestName.transform.position = NOOD.NoodyCustomCode.WorldPointToScreenPoint(tableTranform.position + new Vector3(0f, 5f, 0f));
    }

    public void SetRequest(bool isMale, string requestName)
    {
        this.requestName.text = requestName;
        if (isMale) requestGender.text = "Male";
        else requestGender.text = "Female";
    }

    public void HideNameText()
    {
        nameText.gameObject.SetActive(false);
        nameText.transform.DOScale(0, 0);
    }

    public void ShowNameText(string name)
    {
        nameText.gameObject.SetActive(true);
        nameText.transform.DOScale(1f, 0.5f);
        nameText.text = name;
    }
}
