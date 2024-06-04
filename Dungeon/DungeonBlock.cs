using System;
using System.Drawing;

namespace Dungeon
{

    [Serializable]
    public class DungeonBlock : DungeonObject
    {



        public DungeonBlock(Bitmap image, DungeonObjectCollision collision_type = DungeonObjectCollision.NoCollision)
            : base(image, collision_type) { }
    }
}
