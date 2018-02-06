//This class is auto-generated do not modify
namespace k
{
	public static class Scenes
	{
		public const string GAME_SCENE = "GameScene";

		public const int TOTAL_SCENES = 1;


		public static int nextSceneIndex()
		{
			if( UnityEngine.Application.loadedLevel + 1 == TOTAL_SCENES )
				return 0;
			return UnityEngine.Application.loadedLevel + 1;
		}
	}
}