using System;
using System.Collections.Generic;
using System.Drawing;

namespace Dungeon
{

    [Serializable]
    public class DungeonContainer
    {

        private static readonly int spread_radius = 50;


        private DungeonOwner m_owner;


        private int m_limit;


        private List<DungeonItem> m_items;


        public List<DungeonItem> Items
        {
            get
            {
                return m_items;
            }
        }




        public DungeonContainer(DungeonOwner owner, int limit = -1)
        {
            m_owner = owner;
            m_items = new List<DungeonItem>();
            m_items.Clear();
            m_limit = limit;
            if (m_limit != -1)
            {
                for (int i = 0; i < limit; i++)
                {
                    m_items.Add(null);
                }
            }
        }




        public int Add(DungeonItem item)
        {
            if (item != null)
            {
                if (m_limit == -1)
                {
                    DungeonContainer old_container = item.Container;
                    if (old_container != null)
                    {
                        old_container.Remove(item);
                    }

                    if (item is DungeonItemSword)
                    {
                        (item as DungeonItemSword).TotalFrame = 0;
                    }

                    item.Container = this;

                    m_items.Add(item); 

                    item.ObjectStatus = DungeonObjectStatus.AddedNotDestroyed;

                    if (m_owner is DungeonLevel)
                    {
                        (m_owner as DungeonLevel).MoveObjectIfInBlocks(item);
                    }

                    return 1;
                }
                else
                {
                    bool can_add = false;
                    int i = 0;
                    for (i = 0; i < 30; i++)
                    {
                        if (m_items[i] == null)
                        {
                            can_add = true;
                            break;
                        }
                    }
                    if (can_add)
                    {
                        Set(item, i);
                        return 1;
                    }
                }
                return 0;
            }
            return -1;
        }





        public bool Set(DungeonItem item, int pos_id)
        {
            if (item != null)
            {
                if (pos_id >= 0 && (pos_id < m_limit && m_limit != -1))
                {
                    if (m_items[pos_id] == null)
                    {
                        DungeonContainer old_container = item.Container;
                        if (old_container != null)
                        {
                            old_container.Remove(item);
                        }

                        if (item is DungeonItemSword)
                        {
                            (item as DungeonItemSword).TotalFrame = 0;
                        }

                        item.Container = this;

                        m_items[pos_id] = item;

                        item.ObjectStatus = DungeonObjectStatus.AddedNotDestroyed;

                        return true;
                    }
                }
            }
            return false;
        }



        public void Remove(DungeonItem item)
        {
            if (item != null)
            {
                if (item.Container == this)
                {
                    item.Container = null;

                    if (m_limit == -1)
                    {
                        m_items.Remove(item);
                    }
                    else
                    {
                        int i = 0;
                        for (i = 0; i < m_limit; i++)
                        {
                            if (m_items[i] == item) break;
                        }
                        m_items[i] = null;
                    }

                    item.ObjectStatus = DungeonObjectStatus.CreatedNotAdded;


                    if (m_owner is DungeonCreature)
                    {
                        if (item is DungeonItemEquipment)
                        {
                            if ((item as DungeonItemEquipment).IsEquip)
                            {
                                (item as DungeonItemEquipment).UnEquip();
                            }
                        }
                    }
                }
            }
        }




        public void DropAllItems(Point drop_point_location, double procent_not_to_destroy = 50)
        {
            if (m_owner is DungeonObject)
            {
                Random random = new Random();
                bool is_one_item_dropped = false;
                for (int i = 0; i < m_items.Count; i++)
                {
                    if (m_items[i] != null)
                    {
                        m_items[i].Location = new DungeonPoint(drop_point_location.X + random.Next(-spread_radius, spread_radius + 1), drop_point_location.Y + random.Next(-spread_radius, spread_radius + 1));
                        if (random.Next(0, 101) <= procent_not_to_destroy || !(m_items[i].CanDestroy) || !is_one_item_dropped)
                        {
                            (m_owner as DungeonObject).DungeonLevel.Container.Add(m_items[i]);
                            is_one_item_dropped = true;
                        }
                        else
                        {
                            m_items[i].Destroy();
                        }
                    }
                }
            }
        }
    }
}