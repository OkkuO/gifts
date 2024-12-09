namespace snow_bot
{
    public class GiftService
    {
        private readonly GiftRepository _giftRepository;

        public GiftService(GiftRepository giftRepository)
        {
            _giftRepository = giftRepository;
        }

        public async void AddGift(int points, long userId, string userName, int coal, int ring, int socks, int bear, int dollar, int matr, int bottle) 
        {
            await _giftRepository.Add(points, userId, userName, coal, ring, socks, bear, dollar, matr, bottle);
        }

        public async Task<GiftModel> GetId(long UserId)
        {
            return await _giftRepository.GetId(UserId);
        }

        public async void UpdateGifts(int idReply, int idGiver, int coal, int ring, int socks, int bear, int dollar, int matr, int bottle) 
        {
            await _giftRepository.UpdateGifts(idReply, idGiver, coal, ring, socks, bear, dollar, matr, bottle);
        } 
        public async void UpdateGiftGet(int id, int coal, int ring, int socks, int bear, int dollar, int matr, int bottle) 
        {
            await _giftRepository.UpdateGiftGet(id, coal, ring, socks, bear, dollar, matr, bottle);
        } 

        public async Task<List<GiftModel>> GetGiftsByUserId(long UserId)
        {
           return await _giftRepository.GetGiftsByUserId(UserId);
        }
    }


}