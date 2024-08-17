namespace NiGames.PlayerPrefsFetcher
{
    public interface IPlayerPrefsFetcher
    {
        public PlayerPrefsEntry[] Retrieve();
    }
}