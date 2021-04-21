using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class RandomDrop
    {
        private Game1 game;
        private SpriteBatch spritebatch;
        private Vector2 position;

        private List<IItem> items; 

        public RandomDrop(Vector2 Position, SpriteBatch Spritebatch, Game1 Game)
        { 
            game = Game;
            spritebatch = Spritebatch;
            position = Position;

            items = new List<IItem> {
                new FoodItem(position, spritebatch, game),
                new RupyItem(position, spritebatch, game),
                new RupyItem(position, spritebatch, game),
                new BombItem(position, spritebatch, game)
            };
        }

        public IItem RandItem()
        {
            return items[GlobalSettings.RND.Next() % items.Count];
        }

        public IItem DropFinal()
        {
            return new TriforceItem(position, spritebatch, game);
        }

    }
}
