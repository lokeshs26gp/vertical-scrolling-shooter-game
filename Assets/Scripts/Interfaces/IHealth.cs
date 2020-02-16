namespace Entity
{
    public interface IHealth 
    {
        void Initilize(int pMax);
        void ReceiveDamage(Entity entity,int pAmount);
        void Reset();
    }
}
