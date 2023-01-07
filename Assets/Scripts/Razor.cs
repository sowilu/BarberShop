using UnityEngine;


public class Razor : HairModifyTool
{
    [Header("Vibration settings")] 
    public float duration;
    [Range(0, 1)]
    public float intensity;
    
    
    protected override void Update()
    {
        base.Update();
        if (_inUse)
        {
            _controller.SendHapticImpulse(intensity, 0.03f);
        }
    }
}
