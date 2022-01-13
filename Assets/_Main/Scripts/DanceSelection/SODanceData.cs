using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DanceType {House, Macarena, HipHop }
[CreateAssetMenu(fileName = "NewDanceData")]
public class SODanceData : ScriptableObject
{
    public DanceType selectedDance;
    public RenderTexture houseText;
    public RenderTexture hipHopText;
    public RenderTexture macarenaText;

    public RenderTexture GetActualDance()
    {
        if (selectedDance == DanceType.House)
        {
            return houseText;
        }
        else if (selectedDance == DanceType.HipHop)
        {
            return hipHopText;
        }
        else
        {
            return macarenaText;
        }
    }
}
