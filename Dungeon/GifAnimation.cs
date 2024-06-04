using System.Collections.Generic;
using System.Drawing;

namespace Dungeon
{
      
    public class GifAnimation
    {
          
        private List<Bitmap> m_images;

          
        public GifAnimation()
        {
            m_images = new List<Bitmap>();
        }

          
          
        public void AddFrame(Bitmap image)
        {
            m_images.Add(image);
        }

          
          
          
        public Bitmap GetFrame(int frame_id)
        {
            if (frame_id < 0 || frame_id >= m_images.Count)
            {
                frame_id = 0;
            }
            return m_images[frame_id];
        }
    }
}
