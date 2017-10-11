using Microsoft.Xna.Framework;

namespace CowFarm
{
    public interface IInteractable
    {
        bool OnFocus { get; set; }
        bool CanInteract { get; set; }        
        void Interact();
    }
}