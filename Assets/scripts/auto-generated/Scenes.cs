//This class is auto-generated do not modify
namespace k
{
	public static class Scenes
	{
		public const string LAUNCH_SCREEN = "LaunchScreen";
		public const string INITIALIZE = "Initialize";
		public const string MAIN_MENU = "MainMenu";
		public const string HERO_PICK = "HeroPick";
		public const string GAME_SCENE = "GameScene";
		public const string LOBBY = "Lobby";

		public const int TOTAL_SCENES = 6;


		public static int nextSceneIndex()
		{
			if( UnityEngine.Application.loadedLevel + 1 == TOTAL_SCENES )
				return 0;
			return UnityEngine.Application.loadedLevel + 1;
		}
	}
}