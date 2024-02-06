using System;
using UnityEngine;

public abstract class PlateObject : MonoBehaviour 
{
    public abstract event EventHandler OnActivate;
    public abstract event EventHandler OnDeactivate;
    
    public LayerMask playerLayer;

    public abstract void Activate();

    public abstract void Deactivate();
}
