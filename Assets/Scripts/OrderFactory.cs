using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class OrderFactory 
{
    [SerializeField] GameObject OrderListParent;
    [SerializeField] List<OrderButton> OrderList;
    [SerializeField] int MediumDifficultyThreshold = 2;
    [SerializeField] int HardDifficultyThreshold = 5;
    [SerializeField] TextMeshProUGUI clientName;
    [SerializeField] Image clientPortrait;

    public void Init()
    {
        foreach (OrderButton order in OrderListParent.GetComponentsInChildren<OrderButton>())
        {
            OrderList.Add(order);
        }
    }

    public void LoadOrder(Order orderLoaded)
    {

        clientName.text = orderLoaded.orderClient.clientName;
        clientPortrait.sprite = orderLoaded.orderClient.icon;
        OrderList.ForEach((it) => { it.ResetButton(); });
        for (int i = 0; i < orderLoaded.potionsInOrder.Count; i++)
        {
            OrderList[i].SetButton(orderLoaded.potionsInOrder[i]);
        };
    }
    public Order CreateOrder()
    {
        Order newOrder = new Order();

        foreach(OrderButton or in OrderList)
        {
            or.Icon.sprite = null;
        }

        newOrder.orderClient = GameManager._GAME_MANAGER.clients[Random.Range(0, GameManager._GAME_MANAGER.clients.Count)];

        newOrder.potionsInOrder = new List<Potion>();

        int Quantity = 1;

        if (GameManager._GAME_MANAGER.currentUser.Level > HardDifficultyThreshold)
        {
            Quantity = Random.Range(3, 7);
        }
       else if (GameManager._GAME_MANAGER.currentUser.Level > MediumDifficultyThreshold)
        {
            Quantity = Random.Range(2, 5);
        }

        for (int i = 0; i < Quantity; i++)
        {
            int index = Random.Range(0, GameManager._GAME_MANAGER.recipes.Count);
            newOrder.potionsInOrder.Add(GameManager._GAME_MANAGER.recipes[index]);

        }

        clientName.text = newOrder.orderClient.clientName;
        clientPortrait.sprite = newOrder.orderClient.icon;

        for (int i = 0; i < newOrder.potionsInOrder.Count; i++)
        {
            OrderList[i].SetButton(newOrder.potionsInOrder[i]);
        };

        DBManager._DB_MANAGER.InsertOrder(newOrder, GameManager._GAME_MANAGER.currentUser);

        return newOrder;
    }


}
