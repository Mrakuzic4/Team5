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
        private bool _HappyMusics = false;
        private bool _DevsAreInMood = false;

        private static SoundFactory instance;
        private Song titleScreen;
        private Song dungeon1;
        private Song dungeon2;
        private Song dungeon3;
        private Song dungeon4;
        private Song dungeon5;
        private Song dungeon6;
        private Song titleScreenHappy;
        private Song dungeonHappy;
        private Song[] devInAMode;

        private SoundEffect[] swordSlash;
        private SoundEffect linkDamaged;
        private SoundEffect getItem;
        private SoundEffect bossScream;
        private SoundEffect bossHappyScream;
        private SoundEffect linkDeath;
        private SoundEffect bombDrop;
        private SoundEffect bombBlow;
        private SoundEffect[] throwingKnife; //arrow boomerang
        private SoundEffect flameThrow; //candle
        private SoundEffect triforceObtained;
        private SoundEffect enemyDamaged;
        private SoundEffect getHeart;
        private SoundEffect payDoors;
        private SoundEffect[] eatFood;
        private SoundEffect[] itemPickup;
        private SoundEffect damgedHappy;
        private SoundEffect wallBroken;
        private SoundEffect[] walking;
        private SoundEffect[] snakeDie;
        private SoundEffect[] moblinDie;
        private SoundEffect[] moblinAttack; 
        private SoundEffect[] beetleDie;
        private SoundEffect[] merchant;
        private SoundEffect deathHappy;
        private SoundEffect happyEnding;
        private SoundEffect[] idling;
        private SoundEffect nightmareMode;

        private float volume = 1f;
        private SoundFactory()
        {

        }

        public void LoadFactory(ContentManager content)
        {
            //Songs
            titleScreen = content.Load<Song>("sound/TitleScreenMp3");
            dungeon1 = content.Load<Song>("sound/DungeonMusic");
            dungeon2 = content.Load<Song>("sound/DungeonMusic2");
            dungeon3 = content.Load<Song>("sound/DungeonMusic3");
            dungeon4 = content.Load<Song>("sound/DungeonMusic4");
            dungeon5 = content.Load<Song>("sound/DungeonMusic5");
            dungeon6 = content.Load<Song>("sound/DungeonMusic6");

            titleScreenHappy = content.Load<Song>("sounds/TitleMusicHappy");
            dungeonHappy = content.Load<Song>("sounds/HappyNoise");

            devInAMode = new Song[] {
                content.Load<Song>("sound/CoconutSong"),
                content.Load<Song>("sound/GangTortureDance ")};


            //Sound Effects
            //swordSlash = content.Load<SoundEffect>("sound/LOZ_Sword_Slash");
            swordSlash = new SoundEffect[] {
                content.Load<SoundEffect>("sound/Terrorblade_melee_preattack1"),
                content.Load<SoundEffect>("sound/Terrorblade_melee_preattack2"),
                content.Load<SoundEffect>("sound/Terrorblade_melee_preattack3"),
                content.Load<SoundEffect>("sound/Terrorblade_melee_preattack4")

            };
            linkDamaged = content.Load<SoundEffect>("sound/LOZ_Link_Hurt");
            getItem = content.Load<SoundEffect>("sound/LOZ_Get_Item");
            bossScream = content.Load<SoundEffect>("sound/LOZ_Boss_Scream1");
            bossHappyScream = content.Load<SoundEffect>("sound/diablod");
            linkDeath = content.Load<SoundEffect>("sound/LOZ_Link_Die");
            bombDrop = content.Load<SoundEffect>("sound/LOZ_Bomb_Drop");
            bombBlow = content.Load<SoundEffect>("sound/LOZ_Bomb_Blow");
            throwingKnife = new SoundEffect[] { 
                content.Load<SoundEffect>("sound/SilencerProjectileLaunch1"),
                content.Load<SoundEffect>("sound/SilencerProjectileLaunch2"),
                content.Load<SoundEffect>("sound/SilencerProjectileLaunch3")
            };
            flameThrow = content.Load<SoundEffect>("sound/LOZ_Candle");
            triforceObtained = content.Load<SoundEffect>("sound/LOZ_Fanfare");
            enemyDamaged = content.Load<SoundEffect>("sound/LOZ_Enemy_Hit");
            getHeart = content.Load<SoundEffect>("sound/LOZ_Get_Heart");
            payDoors = content.Load<SoundEffect>("sound/HandOfMidas");
            eatFood = new SoundEffect[] { 
                content.Load<SoundEffect>("sound/EnchantedMango"),
                content.Load<SoundEffect>("sound/FaerieFire")
            };
            itemPickup = new SoundEffect[] { 
                content.Load<SoundEffect>("sound/ItemPickup"),
                content.Load<SoundEffect>("sound/ItemDrop")
            };
            damgedHappy = content.Load<SoundEffect>("sound/bhit1");
            wallBroken = content.Load<SoundEffect>("sound/explodesWall");
            snakeDie = new SoundEffect[] { 
                content.Load<SoundEffect>("sound/dserp"),
                content.Load<SoundEffect>("sound/dserpatt")
            };
            moblinAttack = new SoundEffect[] { 
                content.Load<SoundEffect>("sound/OutworldDevourerPreattack1"),
                content.Load<SoundEffect>("sound/OutworldDevourerPreattack2")
            };
            moblinDie = new SoundEffect[] {
                content.Load<SoundEffect>("sound/mobdead"),
                content.Load<SoundEffect>("sound/lghit")
            };
            beetleDie = new SoundEffect[] { 
                content.Load<SoundEffect>("sound/BeetleDeath"),
                content.Load<SoundEffect>("sound/NeutralFellSpirit")
            };
            deathHappy = content.Load<SoundEffect>("sound/Satanic");
            happyEnding = content.Load<SoundEffect>("sound/RefresherOrb");
            idling = new SoundEffect[] {
                content.Load<SoundEffect>("sound/idleSound1"),
                content.Load<SoundEffect>("sound/idleSound2"),
                content.Load<SoundEffect>("sound/idleSound3"),
                content.Load<SoundEffect>("sound/idleSound4"),
                content.Load<SoundEffect>("sound/idleSound5"),
                content.Load<SoundEffect>("sound/idleSound6"),
                content.Load<SoundEffect>("sound/idleSound7"),
                content.Load<SoundEffect>("sound/idleSound8"),
                content.Load<SoundEffect>("sound/idleSound9"),
                content.Load<SoundEffect>("sound/idleSound10"),
                content.Load<SoundEffect>("sound/idleSound11"),
                content.Load<SoundEffect>("sound/idleSound12"),
                content.Load<SoundEffect>("sound/idleSound13"),
                content.Load<SoundEffect>("sound/idleSound14"),
                content.Load<SoundEffect>("sound/idleSound15")

            };
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

            nightmareMode = content.Load<SoundEffect>("sound/horrorghostscream");
        }

        public void SongSelect(int songNum)
        {
            switch (songNum)
            {
                case 1:
                    DungeonSongOne();
                    break;
                case 2:
                    DungeonSongTwo();
                    break;
                case 3:
                    DungeonSongThree();
                    break;
                case 4:
                    DungeonSongFour();
                    break;
                case 5:
                    DungeonSongFive();
                    break;
                case 6:
                    DungeonSongSix();
                    break;
            }
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

        //Volume
        public void DecreaseVol()
        {
            if (volume > 0f)
            {
                volume = volume - .01f;
            }
            MediaPlayer.Volume = volume;
        }

        public void IncreaseVol()
        {
            if (volume < 1f)
            {
                volume = volume + .01f;
            }
            MediaPlayer.Volume = volume;
        }
        //Songs
        public SongByte TitleScreenSong()
        {
            if (_HappyMusics)
                return new SongByte(titleScreenHappy);
            else 
                return new SongByte(titleScreen);
        }
        public SongByte DungeonSongOne()
        {
            //if (_HappyMusics) {
                //return new SongByte(dungeonHappy);
            //}
            //else if (_DevsAreInMood)
                //return new SongByte(devInAMode[GlobalSettings.RND.Next() % 2]);
            //else
            return new SongByte(dungeon1);
        }

        public SongByte DungeonSongTwo()
        {
            return new SongByte(dungeon2);
        }

        public SongByte DungeonSongThree()
        {
            return new SongByte(dungeon3);
        }

        public SongByte DungeonSongFour()
        {
            return new SongByte(dungeon4);
        }

        public SongByte DungeonSongFive()
        {
            return new SongByte(dungeon5);
        }

        public SongByte DungeonSongSix()
        {
            return new SongByte(dungeon6);
        }

        //Sound Effects
        public SoundByte SwordSlashEffect()
        {
            return new SoundByte(swordSlash[GlobalSettings.RND.Next() % swordSlash.Length]);
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
                return new SoundByte(itemPickup[GlobalSettings.RND.Next() % itemPickup.Length]);
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
        public SoundByte GetIdleSounds()
        {
            return new SoundByte(idling[GlobalSettings.RND.Next() % idling.Length]);
        }
        public SoundByte EatFood()
        {
            return new SoundByte(eatFood[GlobalSettings.RND.Next() % eatFood.Length]);
        }
        public SoundByte MerchantSpeak()
        {
            return new SoundByte(merchant[GlobalSettings.RND.Next() % merchant.Length]);
        }
        public SoundByte BossScreamEffect()
        {
            if (_HappyMusics)
                return new SoundByte(bossHappyScream);

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
            return new SoundByte(snakeDie[GlobalSettings.RND.Next() % snakeDie.Length]);
        }
        public SoundByte MoblinDies()
        {
            return new SoundByte(moblinDie[GlobalSettings.RND.Next() % moblinDie.Length]);
        }
        public SoundByte MoblinShoots()
        {
            return new SoundByte(moblinAttack[GlobalSettings.RND.Next() % moblinAttack.Length]);
        }
        public SoundByte BugDies()
        {
            return new SoundByte(beetleDie[GlobalSettings.RND.Next() % beetleDie.Length]); 
        }
        public SoundByte BombDropEffect()
        {
            return new SoundByte(bombDrop);
        }
        public SoundByte BombBlowEffect()
        {
            return new SoundByte(bombBlow);
        }

        public SoundByte NightmareMode()
        {
            return new SoundByte(nightmareMode);
        }

        public SoundByte ThrowingKnifeEffect()
        {
            return new SoundByte(throwingKnife[GlobalSettings.RND.Next() % throwingKnife.Length]);
        }
        public SoundByte FlameThrowEffect()
        {
            return new SoundByte(flameThrow);
        }

        public SoundByte TriforceObtainedEffect()
        {
            if (_HappyMusics)
                return new SoundByte(happyEnding);
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
