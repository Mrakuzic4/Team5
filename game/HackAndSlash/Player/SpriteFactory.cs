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
        private Texture2D PlayerSpriteRight;
        private Texture2D PlayerSpriteLeft;
        private Texture2D PlayerSpriteUp;
        private Texture2D PlayerSpriteDown;

        private Texture2D BGSprite;


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
            PlayerSpriteRight = content.Load<Texture2D>("images/sucOva");
            PlayerSpriteLeft = content.Load<Texture2D>("images/sucLeft");
            PlayerSpriteUp = content.Load<Texture2D>("images/sucUp");
            PlayerSpriteDown = content.Load<Texture2D>("images/sucDown");


            // More Content.Load calls follow
            BGSprite = content.Load<Texture2D>("images/BG");

        }

        public Texture2D CreateBG()
        {
            return BGSprite;
        }

        public ISprite CreateUpPlayer()
        {
            return new AnimatedSpriteMC(PlayerSpriteUp, 1, 7, 4);
        }

        public ISprite CreateRightPlayer()
        {
            return new AnimatedSpriteMC(PlayerSpriteRight, 1, 7, 4);
        }
        public ISprite CreateLeftPlayer()
        {
            return new AnimatedSpriteMC(PlayerSpriteLeft, 1, 7, 4);
        }

        public ISprite CreateDownPlayer()
        {
            return new AnimatedSpriteMC(PlayerSpriteDown, 1, 7, 4);
        }


    }
}
