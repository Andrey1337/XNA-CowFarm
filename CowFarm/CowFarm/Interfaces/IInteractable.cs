using Microsoft.Xna.Framework;

namespace CowFarm.Interfaces
{
    public interface IInteractable
    {
        //Texture2D ReapaintTexture { get; set; }

        Vector2 GetInteractablePosition();
        bool OnFocus { get; set; }
        bool CanInteract { get; set; }
        void Interact();
    }
}