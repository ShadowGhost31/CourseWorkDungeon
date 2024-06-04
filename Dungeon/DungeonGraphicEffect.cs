using System;
using System.Drawing;
using System.Windows.Forms;

namespace Dungeon
{

    [Serializable]
    public class DungeonGraphicEffect : DungeonObject
    {

        private GifImage m_gif_image;


        private bool m_timer_play_is_working;


        [NonSerialized]
        private Timer m_timer_play;






        public DungeonGraphicEffect(DungeonLevel dungeon_level, GifImage gif_image, int x = 0, int y = 0)
            : base(new Bitmap(gif_image.GetFrame(0)), DungeonObjectCollision.NoCollision)
        {
            DungeonLevel = dungeon_level;
            m_gif_image = gif_image;
            SetLocation(x, y);

            m_timer_play_is_working = true;
            InitializeTimers();
        }


        public void InitializeTimers()
        {
            m_timer_play = new Timer();
            m_timer_play.Tick += timer_play_Tick;
            m_timer_play.Interval = 10;
            if (m_timer_play_is_working)
            {
                Play();
            }
        }


        private void Play()
        {
            m_timer_play.Start();
            m_timer_play_is_working = true;
            ObjectStatus = DungeonObjectStatus.AddedNotDestroyed;
            DungeonLevel.Add(this);
        }


        private void Stop()
        {
            m_timer_play.Stop();
            m_timer_play_is_working = false;
            ObjectStatus = DungeonObjectStatus.Destroyed;
            Destroy();
        }


        private void timer_play_Tick(object sender, EventArgs e)
        {
            if (m_gif_image.CurrentFrame + 1 >= m_gif_image.FramesCount) 
            {
                Stop();
            }
            else
            {
                Image = new Bitmap(m_gif_image.GetNextFrame());
            }
        }
    }
}
