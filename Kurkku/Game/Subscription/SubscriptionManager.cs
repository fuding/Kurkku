﻿using Kurkku.Messages.Outgoing;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Game
{
    public class SubscriptionManager : ILoadable
    {
        #region Fields

        public static readonly SubscriptionManager Instance = new SubscriptionManager();

        #endregion

        #region Properties

        public List<CatalogueSubscriptionData> subscriptions;

        #endregion

        #region Constructors

        public void Load()
        {
            subscriptions = CatalogueDao.GetSubscriptionData(); ;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get if the requested item is actually a subscription
        /// </summary>
        public bool IsSubscriptionItem(int pageId, int itemId)
        {
            var subscriptionData = subscriptions.Where(x => x.Id == itemId && x.PageId == pageId).FirstOrDefault();

            if (subscriptionData == null)
                return false;

            return true;
        }

        /// <summary>
        /// Handler for purchasing club
        /// </summary>
        public void PurchaseClub(Player player, int pageId, int itemId)
        {
            var subscriptionData = subscriptions.Where(x => x.Id == itemId && x.PageId == pageId).FirstOrDefault();

            if (subscriptionData == null)
                return;

            // Calculate new price for both credits and seasonal furniture
            int priceCoins = subscriptionData.PriceCoins;
            int priceSeasonal = subscriptionData.PriceSeasonal;

            // Continue standard purchase
            if (priceCoins > player.Details.Credits)
            {
                player.Send(new NoCreditsComposer(true, false));
                return;
            }

            if (priceSeasonal > player.Currency.GetBalance(subscriptionData.SeasonalType))
            {
                player.Send(new NoCreditsComposer(false, true, subscriptionData.SeasonalType));
                return;
            }

            // Update credits of user
            if (priceCoins > 0)
            {
                player.Currency.ModifyCredits(-priceCoins);
                player.Currency.UpdateCredits();
            }

            // Update seasonal currency
            if (priceSeasonal > 0)
            {
                player.Currency.AddBalance(subscriptionData.SeasonalType, -priceSeasonal);
                player.Currency.UpdateCurrency(subscriptionData.SeasonalType, false);
                player.Currency.SaveCurrencies();
            }

            DateTime startTime;

            if (player.IsSubscribed)
                startTime = player.Subscription.ExpireDate;
            else
                startTime = DateTime.Now;

            player.Subscription = new SubscriptionData
            {
                SubscribedDate = DateTime.Now,
                ExpireDate = startTime.AddMonths(subscriptionData.Months),
                UserId = player.Details.Id
            };

            SubscriptionDao.SaveSubscription(player.Subscription);

            player.Send(new UserRightsMessageComposer(player.IsSubscribed ? 2 : 0, player.Details.Rank));
            player.Send(new ScrSendUserInfoComposer(player.Subscription));
        }

        #endregion
    }
}
