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
        private bool _HappyMusics = true;
        private bool _DevsAreInMood = false;

        private static SoundFactory instance;
        private Song titleScreen;
        private Song dungeon;
        private Song titleScreenHappy;
        private Song dungeonHappy;
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
        private SoundEffect payDoors;
        private SoundEffect eatFood;
        private SoundEffect itemPickup;
        private SoundEffect damgedHappy;
        private SoundEffect wallBroken;
        private SoundEffect[] walking;
        private SoundEffect snakeDie;
        private SoundEffect moblinDie;
        private SoundEffect beetleDie;
        private SoundEffect[] merchant;
        private SoundEffect deathHappy; 


        private SoundFactory()
        {

        }

        public void LoadFactory(ContentManager content)
        {
            //Songs
            titleScreen = content.Load<Song>("sound/TitleScreenMp3");
            dungeon = content.Load<Song>("sound/DungeonMusic");

            titleScreenHappy = content.Load<Song>("sounds/TitleMusicHappy");
            dungeonHappy = content.Load<Song>("sounds/HappyNoise");

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
            payDoors = content.Load<SoundEffect>("sound/HandOfMidas");
            eatFood = content.Load<SoundEffect>("sound/EnchantedMango");
            itemPickup = content.Load<SoundEffect>("sound/ItemPickup");
            damgedHappy = content.Load<SoundEffect>("sound/bhit1");
            wallBroken = content.Load<SoundEffect>("sound/explodesWall");
            snakeDie = content.Load<SoundEffect>("sound/dserp");
            moblinDie = content.Load<SoundEffect>("sound/mobdead");
            beetleDie = content.Load<SoundEffect>("sound/BeetleDeath");
            deathHappy = content.Load<SoundEffect>("sound/Satanic");
            merchant = new SoundEffect[] {
                content.Load<SoundEffect>("sound/zhar01"),
                content.Load<SoundEffect>("sound/zhar02"),
                content.Load<SoundEffect>("sound/garbud01"),
                content.Load<SoundEffect>("sound/garbud04")
            };
            walking = new SoundEffect[] {
                content.Load<SoundEffect>("sound/walk1"),
                content.Load<SoundEffect>("sound/walk2"),
                content.Load<SoundEffect>("sound/walk3"),
                content.Load<SoundEffect>("sound/walk4")
            };
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
            if (_HappyMusics)
                return new SongByte(titleScreenHappy);
            else 
                return new SongByte(titleScreen);
        }
        public SongByte DungeonSong()
        {
            if (_HappyMusics) {
                return new SongByte(dungeonHappy);
            }
            else if (_DevsAreInMood)
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
            if (_HappyMusics)
                return new SoundByte(damgedHappy);
            else 
                return new SoundByte(linkDamaged);
        }
        public SoundByte GetItemEffect()
        {
            if (_HappyMusics)
                return new SoundByte(itemPickup);
            else 
                return new SoundByte(getItem);
        }
        public SoundByte BreakWall()
        {
            return new SoundByte(wallBroken);
        }
        public SoundByte GetWalking()
        {
            return new SoundByte(walking[GlobalSettings.RND.Next() % walking.Length]);
        }
        public SoundByte EatFood()
        {
            return new SoundByte(eatFood);
        }
        public SoundByte MerchantSpeak()
        {
            return new SoundByte(merchant[GlobalSettings.RND.Next() % merchant.Length]);
        }
        public SoundByte BossScreamEffect()
        {
            return new SoundByte(bossScream);
        }
        public SoundByte LinkDeathEffect()
        {
            if (_HappyMusics)
                return new SoundByte(deathHappy);
            return new SoundByte(linkDeath);
        }
        public SoundByte SnakeDies()
        {
            return new SoundByte(snakeDie);
        }
        public SoundByte MoblinDies()
        {
            return new SoundByte(moblinDie);
        }
        public SoundByte BugDies()
        {
            return new SoundByte(beetleDie); 
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

        public SoundByte GetPayDoorsEffect()
        {
            return new SoundByte(payDoors);
        }
        public void StopSong()
        {
            MediaPlayer.Stop();
        }
    }
}
