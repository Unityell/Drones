using UnityEngine;

public class Card : Signal
{
    public event EmitSignal MySignal;
    [SerializeField] float RotateSpeed;
    [SerializeField] float AddScaleSpeed;
    bool Rotate;
    public void RotateAndScale()
    {
        Rotate = true;
    }
    void FixedUpdate()
    {
        if(Rotate)
        {
            if(transform.localScale.x < 1)
            {
                transform.localScale += Vector3.one * AddScaleSpeed;
                transform.localEulerAngles += Vector3.forward * RotateSpeed;
            }
            else
            {
                MySignal?.Invoke("CardReady");
                transform.localEulerAngles = Vector3.zero;
                Rotate = false;
            }
        }
    }
}
