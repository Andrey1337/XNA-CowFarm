using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CowFarm
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