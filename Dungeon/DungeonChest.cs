using System;
using System.Drawing;

namespace Dungeon
{

    [Serializable]
    public class DungeonChest : DungeonObject
    {

        public DungeonContainer Container;


        private bool m_is_opened;


        public bool IsOpened
        {
            get
            {
                return m_is_opened;
            }
        }

        public DungeonChest()
            : base(Properties.Resources.error, DungeonObjectCollision.StaticCollision)
        {
            Container = new DungeonContainer(this);
            m_is_opened = false;
            Image = Properties.Resources.chest_closed;
            m_collision_size = new Size(CollisionSize.Width + 7, CollisionSize.Height - 2);
            m_collision_offset = new Point(7, 14);
        }


        public void Open(DungeonCreature creature_who_opened)
        {
            if (!m_is_opened)
            {
                m_image = Properties.Resources.chest_opened;
                m_size = m_image.Size;
                m_is_opened = true;
                Container.DropAllItems(new Point((int)(creature_who_opened.Location.X + (Location.X - creature_who_opened.Location.X) / 2), (int)(creature_who_opened.Location.Y + (Location.Y - creature_who_opened.Location.Y) / 2)));
            }
        }
    }
}
