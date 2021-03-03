
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HackAndSlash
{
    public class DrawPlayer : ISprite
    {
                
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int UpdateDelay { get; set; }

        private Vector2 relPositionMC; // Relative position. As position in display window 


        // Frame and fame delays 
        private int currentFrame;
        private int totalFrames;
        private long animeDelay = GlobalSettings.DELAY_TIME;
        private Stopwatch stopwatch = new Stopwatch();
        private long timer;

        private GlobalSettings.Direction direction;
        private bool isAttack;
        public bool Attack { set { isAttack = value; } }

        public int Frame
        {
            get
            {
                return currentFrame;
            }
            set 
            { 
                currentFrame = value; 
            }
        }

        //Singleton
        private static DrawPlayer instance = new DrawPlayer();

        public static DrawPlayer Instance
        {
            get
            {
                return instance;
            }
        }

        private DrawPlayer()
        {
            UpdateDelay = 8;
            currentFrame = 0;
            stopwatch.Restart();
            Rows = 1;
            Columns = 7;
            totalFrames = Rows * Columns;
        }

        public Vector2 GetPos()
        {
            return relPositionMC;
        }

        public void SetPos(Vector2 pos)
        {
            relPositionMC = pos;
        }

        public void SetDirection(GlobalSettings.Direction direction)
        {
            this.direction = direction;
        }

        public void SetTexture(Texture2D texture)
        {
            this.Texture = texture;
            //This is safe because Rows and Columns
            //are already set before calling SetTexture.
            totalFrames = Rows * Columns; 
        }

        public void Update()
        {
            //Record the time elapsed
            timer = stopwatch.ElapsedMilliseconds;
            // Every time the time elpased exceeds the designated delay amount,
            //update the frame and restart the timer
            if (timer > animeDelay)
                {
                    stopwatch.Restart();
                    timer = 0;
                }
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Color color)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (Rows == 1) ? 0 : (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 64, 64);

            if (isAttack)
            {
                if(this.direction == GlobalSettings.Direction.Left)
                {
                    destinationRectangle = new Rectangle((int)(location.X - 60)  , (int)location.Y, width * 4, height * 4);
                }

                else if(this.direction == GlobalSettings.Direction.Up)
                {
                    destinationRectangle = new Rectangle((int)location.X, (int)(location.Y-60), width * 4, height * 4);
                }
            }
               

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, color);

        }
    }
    
}
