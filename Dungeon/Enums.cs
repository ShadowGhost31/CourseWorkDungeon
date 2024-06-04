namespace Dungeon
{
      
    public enum DungeonFormStatus : byte
    {
          
        Menu,

          
        NewGameStart,

          
        NewGameDifficulty,

          
        NewGameCharacter,

          
        NewGameLevelGenerate,

          
        Loading,

          
        MainLoading,

          
        LoadingReady,

          
        MainLoadingReady,

          
        Saving,

          
        Game,

          
        GameEndWin,

          
        GameEndLoose,

          
        InGameMenu,

          
        InGameMenuConfirmLoad,

          
        InGameMenuConfirmExit,

          
        Rules,

          
        Settings,

          
        SettingsScreen,

          
        SettingsOther,

          
        SettingsAudio,

          
        SettingsInterface,

          
        SettingsControls,

          
        ConfirmExit,
    }

      
    public enum DungeonDifficulty : byte
    {
          
        Easy
    }

      
    public enum DungeonObjectStatus : byte
    {
          
        CreatedNotAdded,

          
        AddedNotDestroyed,

          
        Destroyed,
    }

      
    public enum DungeonObjectCollision : byte
    {
          
        NoCollision,

          
        StaticCollision,
    }

      
    public enum DungeonDoorImageType : byte
    {
          
        Horizontal,

          
        Vertical,
    }

      
    public enum DungeonCreatureMoveDirection : byte
    {
          
        Up,

          
        Down,

          
        Left,

          
        Right,
    }

      
    public enum DungeonObjectMoveDirection : byte
    {
          
        Up,

          
        Down,

          
        Left,

          
        Right,

          
        UpLeft,

          
        UpRight,

          
        DownRight,

          
        DownLeft,
    }

      
    public enum DungeonInventoryStatus : byte
    {
          
        None,

          
        Inventory,

          
        Statistic,

          
        Map,
    }

      
    public enum DungeonStats : byte
    {
          
        MaxHealth,

          
        MaxEnergy,

          
        Intelligence,

          
        Regeneration,

          
        Restore,

          
        Speed,

          
        Power,

          
        Mobility,

          
        Luck,
    }

      
    public enum DungeonMapCell : byte
    {
          
        Nothing,

          
        Floor,

          
        Wall,

          
        WallDark,

          
        WallDarkDark,

          
        EntranceLadderUp,

          
        EntranceLadderDown,

          
        EntranceLadderLeft,

          
        EntranceLadderRight,

          
        ExitLadderUp,

          
        ExitLadderDown,

          
        ExitLadderLeft,

          
        ExitLadderRight,

          
        FloorAndChest,

          
        FloorAndChestBonus,

          
        FloorAndDoorVertical,

          
        FloorAndDoorExitVertical,

          
        FloorAndDoorBonusVertical,

          
        FloorAndDoorHorizontal,

          
        FloorAndDoorExitHorizontal,

          
        FloorAndDoorBonusHorizontal,

          
        FloorAndMonster,

          
        FloorAndMonsterBoss,
    }

      
    public enum DungeonLadderType : byte
    {
          
        Up,

          
        Down,

          
        Left,

          
        Right,

          
        NoLadder,
    }

      
    public enum DungeonMapConnectionPointType : byte
    {
          
        None,

          
        Corner,

          
        RoomUsusal,

          
        RoomEntrance,

          
        RoomExit,

          
        RoomBoss,

          
        RoomBonus,
    }

      
    public enum DungeonRoomType : byte
    {
          
        Usual,

          
        Ladder,

          
        Boss,

          
        Bonus,
    }

      
    public enum SetStatType : byte
    {
          
        NotSet,
        
        CanSet,
         
        CanSetPlus,
        
        MustSetPlus,
          
        CanSetMinus,
          
        MustSetMinus,
    }

      
    public enum DungeonDoorType : byte
    {
          
        Usual,
          
        Exit,
   
        Bonus,
    }

      
    public enum DungeoMonsterActionStatus : byte
    {
          
        AFK,
          
        Thinking,
          
        MovingToCell,
          
        Fighting,
    }
}