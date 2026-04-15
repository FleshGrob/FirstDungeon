namespace FirstDungeon.Scripts.OtherScripts
{
    public interface IDamageable 
    {
        void TakeDamage(int damage, float stunTime = 0);
    }
}
