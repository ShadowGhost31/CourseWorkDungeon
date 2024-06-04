using System;
using System.Drawing;

namespace Dungeon
{

    [Serializable]
    public class DungeonHero : DungeonCreature
    {

        public int WalkthrowTime;


        public double DistanceWalked;


        public int MonstersKilled;


        public int MonstersBossesKilled;


        public int DoorsOpened;


        public int DoorsCodeOpened;


        public int ChestsOpened;







 
  


        public DungeonHero(Bitmap texture, string name = "Герой", double max_health = 100, double regeneration = 0.5, double max_energy = 50, double restore = 0.5, double power = 5, double mobility = 0, double speed = 2.5, double intelligence = 0, double luck = 0)
            : base(texture, name, max_health, regeneration, max_energy, restore, power, mobility, speed, intelligence, luck)
        {
            WalkthrowTime = 0;
            DistanceWalked = 0;
            MonstersKilled = 0;
            MonstersBossesKilled = 0;
            DoorsOpened = 0;
            DoorsCodeOpened = 0;
            ChestsOpened = 0;
        }
    }
}
