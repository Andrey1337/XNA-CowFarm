using System;
using CowFarm.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CowFarm.ScreenSystem
{
    public class PhysicsGameScreen : GameScreen
    {            
        protected World World;        

        private float _agentForce;
        private float _agentTorque;
               
        protected PhysicsGameScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.75);
            TransitionOffTime = TimeSpan.FromSeconds(0.75);
            HasCursor = true;
            EnableCameraControl = true;            
            World = null;           
        }

        public bool EnableCameraControl { get; set; }
        

        public override void LoadContent()
        {
            base.LoadContent();                                                
            ScreenManager.Game.ResetElapsedTime();
        }

        

       

        
            

        public override void Draw(GameTime gameTime)
        {
            
            base.Draw(gameTime);
        }
    }
}