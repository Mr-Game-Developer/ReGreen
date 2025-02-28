using UnityEngine.UI;
using System;
using UnityEngine;

[Serializable]
public class Props 
{
    public Outfit outfit;
    public int Price;
    public bool isLock;

    public enum Outfit
    { 
    outfit1,
    outfit2,
    outfit3,
    outfit4,
    outfit5,
    outfit6,
    outfit7,
    outfit8,
    outfit9,
    outfit10,
    outfit11,
    outfit12,
    }
}

[Serializable]
public class Cutters
{
    public Cutter cutter;
    public int Price;
    public bool isLock;

    public enum Cutter
    {
        cutter1,
        cutter2,
        cutter3,
        cutter4,
        car1,
        car2,
    }
}
