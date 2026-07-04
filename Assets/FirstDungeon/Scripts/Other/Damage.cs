public struct Damage
{
    public enum Type
    {
        Normal,
        Trap,
        GroundHazard
    }

    public int Amount;
    public float StunDuration;
    public Type DamageType;
}
