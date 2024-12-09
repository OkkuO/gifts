namespace snow_bot
{
    public class GiftModel
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; } = "";
        public int Coal { get; set; }
        public int Ring { get; set; } 
        public int Socks { get; set; }
        public int Bear { get; set; } 
        public int Dollar { get; set; }
        public int Matryoshka { get; set; } 
        public int Bottle { get; set; } 

    }
}