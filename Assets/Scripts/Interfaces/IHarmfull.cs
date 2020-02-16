
namespace Entity
{
    public interface IHarmfull 
    {
        void Initilize(Entity thisEntity,int pDamage);
        void SendDamage(Entity pOther);
    }
}
