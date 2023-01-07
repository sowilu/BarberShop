using UnityEngine;

public class HairModifyTool : Item
{
    //TODO: client script
    public HairSculptor scalp;
    
    [Header("Hair mod settings")]
    public float radius = 0.1f;
    public float strength = 0.1f;
    public Transform nozzle;
    public float maxDistance = 1;
    public LayerMask hairMask;
    public bool smooth;

    protected virtual void Update()
    {
        if (_inUse)
        {
            var ray = new Ray(nozzle.position, nozzle.forward);
            if (Physics.Raycast(ray, out var hit, maxDistance, hairMask))
            {
                Use(hit);
            }
        }
    }

    protected virtual void Use(RaycastHit hit)
    {
        if(smooth)
            scalp.SmoothExtrude(hit.point, radius, strength * Time.deltaTime);
        else
            scalp.Extrude(hit.point, radius, strength * Time.deltaTime);
        
    }
}
