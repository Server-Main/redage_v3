using GTANetworkAPI;
using NeptuneEvo.Accounts;
using NeptuneEvo.Character;
using NeptuneEvo.Chars;
using NeptuneEvo.Handles;
using Redage.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace NeptuneEvo.Core
{
    public class OnlineBonus : Script
    {
        #region Author
        // По поводу заказа
        // Discord - 0oo.night

        // Эти и другие системы можно купить, ДС - 0oo.night

        // Created by 0oo.night
		
        // Разработано в приватный доступ!
        #endregion

        private static readonly nLog Log = new nLog("OnlineBonus");
        private static List<int> _onlineIssued = new List<int>();

        /// Время остановки выдачи бонусов
        private static readonly int _zeroingTime = 2;
        
        /// Время начала выдачи бонусов
        private static readonly int _startTime = 12;

        /// Минимальное отличие онлайна в большую сторону, чтобы получить бонус
        private static readonly int _minStep = 5;

        /// Минимальный онлайн, который требуется для выдачи бонуса
        private static readonly int _minOnlineToGive = 30;

        /// Стандартное значение бонуса
        private static readonly BonusItem _defaultBonus = new BonusItem(BonusTypes.Money, "3333");

        /// Свои значения бонуса за определенный онлайн
        private static readonly Dictionary<int, BonusItem> _customBonus = new Dictionary<int, BonusItem>()
        {
            /// За достижение 75 онлайна будет выдаваться 4444 доната
            { 75, new BonusItem(BonusTypes.Donate, "4444") }
        };

        /// Время проверки онлайна (в миллисекундах)
        private static readonly int _updateInterval = 1000;

        [ServerEvent(Event.ResourceStart)]
        public void OnStart()
        {
            try
            {
                new Thread(new ParameterizedThreadStart((obj) =>
                {
                    Timers.Start(_updateInterval, () => Interval());
                })).Start();

                Log.Write("Сборка by 0oo.night", nLog.Type.Success);
            }
            catch(Exception ex) { Log.Write("OnStart: " + ex.ToString(), nLog.Type.Error); }
        }

        private static void Interval()
        {
            try
            {
                if (DateTime.Now.Hour > _zeroingTime && DateTime.Now.Hour < _startTime) 
                {
                    if (_onlineIssued.Count != 0)
                        _onlineIssued.Clear();
                    return;
                }

                int online = NAPI.Pools.GetAllPlayers().Count; 
                if (online < _minOnlineToGive) return;

                int _lastOnline = _onlineIssued.Count != 0 ? _onlineIssued.Last() : 0;
                
                if (online > _lastOnline)
                {
                    int currentProcent = (int)Math.Floor((double)online / _minStep);
                    if (currentProcent > (int)Math.Floor((double)_lastOnline / _minStep))
                    {
                        int nearestBonusOnline = currentProcent * _minStep;

                        _onlineIssued.Add(nearestBonusOnline);
                        GiveBonus(nearestBonusOnline);
                    }
                }
            }
            catch(Exception ex) { Log.Write("Interval: " + ex.ToString(), nLog.Type.Error); }
        }

        private static void GiveBonus(int online)
        {
            try
            {
                BonusItem bonus = null;
                if (!_customBonus.TryGetValue(online, out bonus))
                    bonus = _defaultBonus;

                Character.Repository.GetPlayers().ForEach(player => bonus.Give(player));

                NAPI.Chat.SendChatMessageToAll($"~o~[ONLINE BONUS] ~w~Был выдан бонус всем игрокам за достижение {online} онлайна. Продолжаем в том же духе! 🔥");
                Log.Write($"Бонус за достижение {online} был выдан", nLog.Type.Success);
            }
            catch(Exception ex) { Log.Write("GiveBonus: " + ex.ToString(), nLog.Type.Error); }
        }

        public class BonusItem 
        {
            public BonusTypes BonusType { get; set; }
            public string Data { get; set; }

            public BonusItem(BonusTypes bonusType, string data)
            {
                BonusType = bonusType;
                Data = data;
            }

            public void Give(ExtPlayer player)
            {
                try
                {
                    if (player.GetAccountData() is null || player.GetCharacterData() is null) return;

                    switch(BonusType) 
                    {
                        case BonusTypes.Donate:
                            if (!Int32.TryParse(Data, out int _donateCount)) return;

                            UpdateData.RedBucks(player, _donateCount, "Бонус за онлайн");
                            return;
                        case BonusTypes.Money:
                            if (!Int32.TryParse(Data, out int _moneyCount)) return;

                            MoneySystem.Wallet.Change(player, _moneyCount);
                            return;
                    }
                }
                catch(Exception ex) { Log.Write("BonusItem.Give: " + ex.ToString(), nLog.Type.Error); }
            }
        }

        public enum BonusTypes
        {
            Money,
            Donate
        }
    }
}