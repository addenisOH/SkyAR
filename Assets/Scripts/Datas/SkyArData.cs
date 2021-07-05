using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyArData
{
    public float Pitch { get; set; }
    public int Yaw { get; set; }
    public float Roll { get; set; }
    public float AirSpeed { get; set; }
    public float Altitude { get; set; }
    public float VerticalSpeed { get; set; }
    public float LateralG { get; set; }
    public float VerticalG { get; set; }
    public float TurnRate { get; set; }

    public override string ToString()
    {
        return $"Pitch : {Pitch}  Yaw : {Yaw}  Roll : {Roll}  AirSpeed : {AirSpeed}  Altitude : {Altitude}  VerticalSpeed : {VerticalSpeed}  LateralG : {LateralG}  VerticalG : {VerticalG}  TurnRate : {TurnRate}"; 
    }
}
