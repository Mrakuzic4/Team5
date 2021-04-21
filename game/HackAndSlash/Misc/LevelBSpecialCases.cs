using System;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace HackAndSlash
{
    /// <summary>
    /// Dealing with the special cases in level eagle 
    /// </summary>
    class LevelBSpecialCases : ISpecialCases
    {
        private Game1 game; 
        
        private int[] CurrentRoomIndex = new int[] { 0, 0};
        private int[] LastRoomIndex = new int[] { 0, 0 };

        private List<int> merchantList = new List<int>() {
            GlobalSettings.NPC_OLD_MAN,
            GlobalSettings.NPC_OLD_WOMAN
        };
        private List<int> bossList = new List<int>() { 
            GlobalSettings.BOSS_ENEMY
        };

        private enum roomTypes {normal, merchant, boss };
        private int currentRoomType; 

        public LevelBSpecialCases()
        {

        }

        public void Update(Game1 Game)
        {
            game = Game; 
            CurrentRoomIndex = game.currentLevel.mapIndex;

            bool InOldRoom = (CurrentRoomIndex[0] == LastRoomIndex[0] && CurrentRoomIndex[1] == LastRoomIndex[1]);
            if (! InOldRoom) {
                CheckElements();
                DoWhateverNecessay();
                LastRoomIndex = game.currentLevel.mapIndex;
            }

        }

        private void CheckElements()
        {
            currentRoomType = (int)roomTypes.normal;

            for(int i = 0; i < GlobalSettings.TILE_ROW; i++) {
                for (int j = 0; j < GlobalSettings.TILE_COLUMN; j++) {
                    if (merchantList.Contains(game.currentMapInfo.Arrangement[i, j])) {
                        currentRoomType = (int)roomTypes.merchant;
                    }
                    if (bossList.Contains(game.currentMapInfo.Arrangement[i, j])) {
                        currentRoomType = (int)roomTypes.boss;
                    }
                }
            }
        }

        private void DoWhateverNecessay()
        {
            switch (currentRoomType)
            {
                case (int)roomTypes.merchant:
                    SoundFactory.Instance.MerchantSpeak();
                    break;
                default:
                    break;
            }
            currentRoomType = (int)roomTypes.normal;

        }

    }
}

    

