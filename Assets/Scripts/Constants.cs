using UnityEngine;

public static class Constants
{
    public static Color FriendlyColor = Color.green;
    public static float WaveDelta => WaveSize + WaveDistance;
    public static int LevelSize => WaveSize * WaveSize;

    public const int WavesCount = 4;
    public const int WaveSize = 24;

    public const string AudioKey = "Audio";
    public const string LevelFolderName = "Levels";
    public const string MaskFolderName = "Mask";
    public const string GameScene = "GameScene";
    public const string MainMenuScene = "MainMenu";
    public const string PreLoadScene = "Preloader";
    public const string ReturnToMenuText = "Click to return menu";
    
    private const float WaveDistance = 25f;
}