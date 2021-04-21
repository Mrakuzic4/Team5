using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    public class BlockMoveListener
    {
        private Game1 game;
        private BlockMovable block;

        public BlockMoveListener(Game1 Game , IBlock TarBlock)
        {
            game = Game;
            block = (BlockMovable)TarBlock;

        }

        public bool CheckMovement()
        {
            if (block.Moved() && !block.eventTriggered && GlobalSettings.RND.Next(100) < game._DropRateBaseline) 
            {
                game.itemList.Add(new RandomDrop(block.initialPosition, game.spriteBatch, game).RandItem());
                block.eventTriggered = true;
                return true;
            }

            return false;
        }

    }
}
