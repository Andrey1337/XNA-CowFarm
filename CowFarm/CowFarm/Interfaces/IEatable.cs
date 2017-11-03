namespace CowFarm.Interfaces
{
    public interface IEatable : IInteractable
    {
        bool IsEaten { get; set; }
        void Eat();
        float Satiety { get; }
    }
}