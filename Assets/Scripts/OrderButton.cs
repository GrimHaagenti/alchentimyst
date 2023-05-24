using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderButton : MonoBehaviour
{
    [SerializeField] public Image Icon;
    public Potion potion;
    public int index;

    private void Start()
    {
        Icon.color = new Color(1, 1, 1, 0);
    }
    public void SetButton(Potion pot)
    {
        Icon.color = new Color(1, 1, 1, 1);
        potion = pot;
        Icon.sprite = potion.icon;
    }

    public void ResetButton()
    {
        Icon.color = new Color(1, 1, 1, 0);

        Icon.sprite = null;
    }
}
