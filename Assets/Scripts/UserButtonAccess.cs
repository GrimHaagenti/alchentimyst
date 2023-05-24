using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserButtonAccess : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI Username;
    [SerializeField] TextMeshProUGUI Date;
    [SerializeField] TextMeshProUGUI Level;

    public void Init(User user)
    {
        Username.text = user.UserName;
        string[] dateStamp = user.Date.Split(' ');
        string[] hourStrings = dateStamp[1].Split(':');
        string[] dateStrings = dateStamp[0].Split('-');
        Date.text = hourStrings[0] + ":" + hourStrings[1] + " " + dateStrings[2] +
            "-" + dateStrings[1] + "-" + dateStrings[0];
        Level.text = user.Level.ToString();
    }
}
