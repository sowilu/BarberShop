using UnityEngine;

public class ColorSprayer : HairModifyTool
{
    public Color color;

    public ParticleSystem sprayParticles;
    

    protected override void Start()
    {
        base.Start();
        sprayParticles.startColor = color;
    }
    

    protected override void Use(RaycastHit hit)
    {
        scalp.Dye(color, hit.point, radius);
    }
}
