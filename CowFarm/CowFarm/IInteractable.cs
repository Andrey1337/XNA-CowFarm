using Microsoft.Xna.Framework;

namespace CowFarm
{
    public interface IInteractable
    {
        bool OnFocus { get; set; }
        Vector2 GetInteractablePosition();
    }
}