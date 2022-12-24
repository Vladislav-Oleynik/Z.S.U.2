namespace SaveData
{
    [System.Serializable]
    public class PlayerProfile 
    {
        public int id;
        public string name;
        public int balance;

        public PlayerProfile() 
        {
            name = "Player";
        }
    }
}
