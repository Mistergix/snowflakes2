using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Orientation
{
	private float f0, f1, f3, f4, angle;

    public Orientation(float f0, float f1, float f3, float f4, float angle)
    {
        this.F0 = f0;
        this.F1 = f1;
        this.F3 = f3;
        this.F4 = f4;
        this.Angle = angle;
    }

    public float F0 { get => f0; set => f0 = value; }
    public float F1 { get => f1; set => f1 = value; }
    public float F3 { get => f3; set => f3 = value; }
    public float F4 { get => f4; set => f4 = value; }
    public float Angle { get => angle; set => angle = value; }

    public static Orientation Flat()
    {
        return new Orientation(1.5f, 0, Mathf.Sqrt(3) / 2, Mathf.Sqrt(3), 0);
    }

    public static Orientation Pointy()
    {
        return new Orientation(Mathf.Sqrt(3), Mathf.Sqrt(3) / 2, 0, 1.5f, 30);
    }
}