using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform topCounterPoint;
    
    public void Interact()
    {
        Debug.Log("ClearCounter.Interact()");
        Transform kitchenObj = Instantiate(kitchenObjectSO.prefab, topCounterPoint);
        kitchenObj.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName = kitchenObjectSO.objectName;
    }
}
