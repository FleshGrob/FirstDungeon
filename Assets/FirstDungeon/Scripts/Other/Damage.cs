public struct Damage
{
    public enum Type
    {
        Normal,
        Trap,
        GroundHazard,
        Bog
    }

    public int Amount;
    public float StunDuration;
    public Type DamageType;
}
