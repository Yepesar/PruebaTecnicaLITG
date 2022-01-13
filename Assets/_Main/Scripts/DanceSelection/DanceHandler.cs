using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DanceHandler : MonoBehaviour
{
    [SerializeField] private SODanceData danceData;

    [SerializeField] private bool displaySelected = false;
    [SerializeField] private RawImage danceDisplayer;

    // Start is called before the first frame update
    void Start()
    {
        if (displaySelected)
        {
            danceDisplayer.texture = danceData.GetActualDance();
        }
    }

   
    public void ChangeDacen(int newDance)
    {
        if (newDance == 0)
        {
            danceData.selectedDance = DanceType.House;
        }
        else if(newDance == 1)
        {
            danceData.selectedDance = DanceType.Macarena;
        }
        else
        {
            danceData.selectedDance = DanceType.HipHop;
        }
    }
}
