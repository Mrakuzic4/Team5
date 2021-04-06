using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HackAndSlash
{
    class SoundFactory
    {
        private bool _DevsAreInMood = true;

        private static SoundFactory instance;
        private Song titleScreen;
        private Song dungeon;
        private Song[] devInAMode;

        private SoundEffect swordSlash;
        private SoundEffect linkDamaged;
        private SoundEffect getItem;
        private SoundEffect bossScream;
        private SoundEffect linkDeath;
        private SoundEffect bombDrop;
        private SoundEffect bombBlow;
        private SoundEffect throwingKnife; //arrow boomerang
        private SoundEffect flameThrow; //candle
        private SoundEffect triforceObtained;
        private SoundEffect enemyDamaged;
        private SoundEffect getHeart;
        
        private SoundFactory()
        {

        }

        public void LoadFactory(ContentManager content)
        {
            //Songs
            titleScreen = content.Load<Song>("sound/TitleScreenMp3");
            dungeon = content.Load<Song>("sound/DungeonMusic");
            devInAMode = new Song[] { 
                content.Load<Song>("sound/CoconutSong"),
                content.Load<Song>("sound/GangTortureDance ")};


            //Sound Effects
            swordSlash = content.Load<SoundEffect>("sound/LOZ_Sword_Slash");
            linkDamaged = content.Load<SoundEffect>("sound/LOZ_Link_Hurt");
            getItem = content.Load<SoundEffect>("sound/LOZ_Get_Item");
            bossScream = content.Load<SoundEffect>("sound/LOZ_Boss_Scream1");
            linkDeath = content.Load<SoundEffect>("sound/LOZ_Link_Die");
            bombDrop = content.Load<SoundEffect>("sound/LOZ_Bomb_Drop");
            bombBlow = content.Load<SoundEffect>("sound/LOZ_Bomb_Blow");
            throwingKnife = content.Load<SoundEffect>("sound/LOZ_Arrow_Boomerang");
            flameThrow = content.Load<SoundEffect>("sound/LOZ_Candle");
            triforceObtained = content.Load<SoundEffect>("sound/LOZ_Fanfare");
            enemyDamaged = content.Load<SoundEffect>("sound/LOZ_Enemy_Hit");
            getHeart = content.Load<SoundEffect>("sound/LOZ_Get_Heart");
        }

        public static SoundFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SoundFactory();
                }
                return instance;
            }
        }

        //Songs
        public SongByte TitleScreenSong()
        {
            return new SongByte(titleScreen);
        }
        public SongByte DungeonSong()
        {
            if (_DevsAreInMood)
                return new SongByte(devInAMode[GlobalSettings.RND.Next() % 2]);
            else
                return new SongByte(dungeon);
        }

        //Sound Effects
        public SoundByte SwordSlashEffect()
        {
            return new SoundByte(swordSlash);
        }
        public SoundByte LinkDamagedEffect()
        {
            return new SoundByte(linkDamaged);
        }
        public SoundByte GetItemEffect()
        {
            return new SoundByte(getItem);
        }
        public SoundByte BossScreamEffect()
        {
            return new SoundByte(bossScream);
        }
        public SoundByte LinkDeathEffect()
        {
            return new SoundByte(linkDeath);
        }
        public SoundByte BombDropEffect()
        {
            return new SoundByte(bombDrop);
        }
        public SoundByte BombBlowEffect()
        {
            return new SoundByte(bombBlow);
        }
        public SoundByte ThrowingKnifeEffect()
        {
            return new SoundByte(throwingKnife);
        }
        public SoundByte FlameThrowEffect()
        {
            return new SoundByte(flameThrow);
        }

        public SoundByte TriforceObtainedEffect()
        {
            return new SoundByte(triforceObtained);
        }

        public SoundByte EnemyDamagedEffect()
        {
            return new SoundByte(enemyDamaged);
        }

        public SoundByte GetHeartEffect()
        {
            return new SoundByte(getHeart);
        }

        public void StopSong()
        {
            MediaPlayer.Stop();
        }
    }
}
