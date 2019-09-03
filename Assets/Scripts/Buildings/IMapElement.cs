using Towns;

namespace Buildings
{
    public interface IMapElement
    {
        void Kill();

        void FinishConstruct();

        Town GetTown();
    }
}