using Microsoft.EntityFrameworkCore;

namespace snow_bot
{
    public class GiftRepository
    {
        private readonly GiftDbContext _dbContext;
        public GiftRepository(GiftDbContext dbContext)
        {
            _dbContext = dbContext;
        } 

        public async Task Add(int points, long userId, string userName, int coal, int ring, int socks, int bear, int dollar, int matr, int bottle)
        {
            var gift = new GiftModel()
            {   
                Points = points,
                UserId = userId,
                UserName = userName,
                Coal = coal,
                Ring = ring,
                Socks = socks,
                Bear = bear,
                Dollar = dollar,
                Matryoshka = matr,
                Bottle = bottle
                
            };

            await _dbContext.AddAsync(gift);
            await _dbContext.SaveChangesAsync();
        } 
        public async Task<GiftModel> GetId(long UserId) 
        {   
            var gift = await _dbContext.GiftModels.FirstOrDefaultAsync(c => c.UserId == UserId);
            if (gift == null)
            {
                return null!;
            }
            return gift;
        }
        public async Task UpdateGifts(int idReply, int idGiver,  int coal, int ring, int socks, int bear, int dollar, int matr, int bottle)
        {
            
            var giftReply = await _dbContext.GiftModels.FindAsync(idReply);
            var giftGiver = await _dbContext.GiftModels.FindAsync(idGiver);

            if (giftReply != null)
            {
                giftReply.Coal += coal;
                giftReply.Ring += ring;
                giftReply.Socks += socks;
                giftReply.Bear += bear;
                giftReply.Dollar += dollar;
                giftReply.Matryoshka += matr;
                giftReply.Bottle += bottle;
                giftReply.Dollar += dollar;        
            }

            if (giftGiver != null)
            {
                giftGiver.Coal -= coal;
                giftGiver.Ring -= ring;
                giftGiver.Socks -= socks;
                giftGiver.Bear -= bear;
                giftGiver.Dollar -= dollar;
                giftGiver.Matryoshka -= matr;
                giftGiver.Bottle -= bottle;
                giftGiver.Dollar -= dollar;        
            }

            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateGiftGet(int idReply,  int coal, int ring, int socks, int bear, int dollar, int matr, int bottle)
        {
            
            var giftReply = await _dbContext.GiftModels.FindAsync(idReply);

            if (giftReply != null)
            {
                giftReply.Coal += coal;
                giftReply.Ring += ring;
                giftReply.Socks += socks;
                giftReply.Bear += bear;
                giftReply.Dollar += dollar;
                giftReply.Matryoshka += matr;
                giftReply.Bottle += bottle;
                giftReply.Dollar += dollar;        
            }

            await _dbContext.SaveChangesAsync();
        }


        public async Task<List<GiftModel>> GetGiftsByUserId(long userId)
        {
            var gifts = await _dbContext.GiftModels
                .AsNoTracking()
                .Where(g => g.UserId == userId)
                .ToListAsync();

            return gifts;
        }
    }
}