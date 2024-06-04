using System.Drawing;

namespace Dungeon
{
      
    public abstract class DungeonStatsInfo
    {
          
          
          
        public static string Name(DungeonStats stat)
        {
            if (stat == DungeonStats.MaxHealth) return "Макс. здоров'я";
            else if (stat == DungeonStats.MaxEnergy) return "Макс. енергія";
            else if (stat == DungeonStats.Intelligence) return "Інтелект";
            else if (stat == DungeonStats.Regeneration) return "Регенерація";
            else if (stat == DungeonStats.Restore) return "Відновлення";
            else if (stat == DungeonStats.Speed) return "Швидкість";
            else if (stat == DungeonStats.Power) return "Сила";
            else if (stat == DungeonStats.Mobility) return "Спритність";
            else if (stat == DungeonStats.Luck) return "Удача";
            return "error";
        }

          
          
          
        public static double Max(DungeonStats stat)
        {
            if (stat == DungeonStats.MaxHealth) return 250;
            else if (stat == DungeonStats.MaxEnergy) return 250;
            else if (stat == DungeonStats.Intelligence) return 250;
            else if (stat == DungeonStats.Regeneration) return 5;
            else if (stat == DungeonStats.Restore) return 5;
            else if (stat == DungeonStats.Speed) return 5;
            else if (stat == DungeonStats.Power) return 50;
            else if (stat == DungeonStats.Mobility) return 50;
            else if (stat == DungeonStats.Luck) return 50;
            return -1;
        }

          
          
          
        public static double Plus(DungeonStats stat)
        {
            if (stat == DungeonStats.MaxHealth) return 25;
            else if (stat == DungeonStats.MaxEnergy) return 25;
            else if (stat == DungeonStats.Intelligence) return 25;
            else if (stat == DungeonStats.Regeneration) return 0.5;
            else if (stat == DungeonStats.Restore) return 0.5;
            else if (stat == DungeonStats.Speed) return 0.5;
            else if (stat == DungeonStats.Power) return 5;
            else if (stat == DungeonStats.Mobility) return 5;
            else if (stat == DungeonStats.Luck) return 5;
            return -1;
        }

          
          
          
        public static Color GetColor(DungeonStats stat)
        {
            if (stat == DungeonStats.MaxHealth) return Color.FromArgb(255, 0, 0);   
            else if (stat == DungeonStats.MaxEnergy) return Color.FromArgb(0, 0, 255);   
            else if (stat == DungeonStats.Intelligence) return Color.FromArgb(255, 165, 0);   
            else if (stat == DungeonStats.Regeneration) return Color.FromArgb(255, 76, 91);   
            else if (stat == DungeonStats.Restore) return Color.FromArgb(154, 206, 235);   
            else if (stat == DungeonStats.Speed) return Color.FromArgb(0, 255, 0);   
            else if (stat == DungeonStats.Power) return Color.FromArgb(255, 255, 0);   
            else if (stat == DungeonStats.Mobility) return Color.FromArgb(139, 0, 255);   
            else if (stat == DungeonStats.Luck) return Color.FromArgb(255, 192, 203);   
            return Color.FromArgb(50, 50, 50);   
        }
    }
}
