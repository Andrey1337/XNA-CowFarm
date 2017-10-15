namespace CowFarm
{
    public interface IEatable : IInteractable
    {
        bool IsEaten { get; set; }
    }
}