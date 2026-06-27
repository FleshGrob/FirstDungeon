public struct Damage
{
    public enum Type
    {
        Normal,
        Trap
    }

    public int Amount;
    public float StunDuration;
    public Type DamageType;
}
