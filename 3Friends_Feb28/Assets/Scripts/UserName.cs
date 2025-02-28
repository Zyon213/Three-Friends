using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserName : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI userName;

    private void Update()
    {
        SetUserName();
    }

    public void SetUserName()
    {
        userName.text = MainManager.Instance.userName;
    }
}
