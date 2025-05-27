using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    //Serialized Fields
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject selectedCounterVisual;
    
    private Player _player;

    private void Start()
    {
        _player = Player.Instance;
        _player.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.SelectedCounter == clearCounter)
        {
            selectedCounterVisual.SetActive(true);
        }
        else
        {
            selectedCounterVisual.SetActive(false);
        }
    }
}
