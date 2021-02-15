using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAndSlash
{
    /// <summary>
    /// This file holds all the command classes, which implement ICommand. Each class is simple,
    /// with a constructor that sets a reference of the main Game1 class, and an execute() method
    /// which calls the necessary statements to handle the specified command.
    /// </summary>
    public class MoveLeftCommand : ICommand
    {
        private Game1 game;
        public MoveLeftCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //game.SpriteHolder = new (method for left facing player sprite)
        }
    }

    public class MoveRightCommand : ICommand
    {
        private Game1 game;
        public MoveRightCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //game.SpriteHolder = new (method for right facing player sprite)
        }
    }

    public class MoveUpCommand : ICommand
    {
        private Game1 game;
        public MoveUpCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //game.SpriteHolder = new (method for up facing player sprite)
        }
    }

    public class MoveDownCommand : ICommand
    {
        private Game1 game;
        public MoveDownCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //game.SpriteHolder = new (method for down facing player sprite)
        }
    }

    public class AttackCommand : ICommand
    {
        private Game1 game;
        public AttackCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //game.SpriteHolder = new (method for attacking player sprite)
        }
    }

    //this class can easily be copied according to how many player items get implemented in sprint 2
    public class UsePlayerItemCommand : ICommand
    {
        private Game1 game;
        public UsePlayerItemCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //game.SpriteHolder = new (method for player sprite with item)
        }
    }

    public class DamageCommand : ICommand
    {
        private Game1 game;
        public DamageCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //game.SpriteHolder = new (method for damaged player sprite)
        }
    }

    public class BlockCycleCommand : ICommand
    {
        private Game1 game;
        public BlockCycleCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //change "BlockHolder"(?) field in primary game class
        }
    }

    public class ItemCycleCommand : ICommand
    {
        private Game1 game;
        public ItemCycleCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //change "ItemHolder"(?) field in primary game class
        }
    }

    public class EnemyCycleCommand : ICommand
    {
        private Game1 game;
        public EnemyCycleCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //change "EnemyHolder"(?) field in primary game class
        }
    }

    public class ResetCommand : ICommand
    {
        private Game1 game;
        public ResetCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            //reset entire game to its initial state somehow...
        }
    }

    public class QuitCommand : ICommand
    {
        private Game1 game;
        public QuitCommand(Game1 game)
        {
            this.game = game;
        }
        public void execute()
        {
            game.Exit();
        }
    }
}
