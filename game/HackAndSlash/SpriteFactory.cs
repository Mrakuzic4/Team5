using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{   /// <summary>
    /// This is a class for all sprites including player sprite, enemy sprite, and obstacle sprite.
    /// Every sprite is required to implement ISprite Interface.
    /// </summary>
    class SpriteFactory
    {
        private Texture2D playerSprite;

        private static SpriteFactory instance = new SpriteFactory();

        public static SpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private SpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {

            // Original image from https://www.spriters-resource.com/fullview/146744/
            // Edited in Photoshop to align the textures 
            // Create the maincharacter sprite with delay, might look odd depending on your machine 
            playerSprite = content.Load<Texture2D>("images/sucOva");
            // More Content.Load calls follow
            
        }

        public ISprite CreateUpPlayer()
        {
            return new PlayerUp(playerSprite);
            // Client code:
            //ISprite playerSprite = EnemySpriteFactory.Instance.CreateUpPlayer();
        }


    }
}
