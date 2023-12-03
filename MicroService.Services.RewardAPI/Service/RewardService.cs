using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroService.Services.RewardAPI.Data;
using MicroService.Services.RewardAPI.Message;
using MicroService.Services.RewardAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace MicroService.Services.RewardAPI.Service
{
    public class RewardService : IRewardService
    {
        private DbContextOptions<AppDbContext> _options;
        public RewardService(DbContextOptions<AppDbContext> options)
        {
            _options = options;
        }

        public async Task UpdateRewards(RewardsMessage rewardsMessage)
        {
            try
            {
                Rewards rewards = new Rewards()
                {
                  OrderId = rewardsMessage.OrderId,
                  RewardsActivity = rewardsMessage.RewardsActivity,
                  UserId = rewardsMessage.UserId,
                  RewardsDate = DateTime.Now
                };

                await using var _db = new AppDbContext(_options);
                await _db.Rewards.AddAsync(rewards);
                await _db.SaveChangesAsync();
            }
            catch
            {
            }
        }
    }
}
