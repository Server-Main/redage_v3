using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GTANetworkMethods;
using NeptuneEvo.Character;
using NeptuneEvo.Character.Models;
using NeptuneEvo.Chars.Models;
using NeptuneEvo.Core;
using NeptuneEvo.Functions;
using NeptuneEvo.Handles;
using Redage.SDK;

namespace NeptuneEvo.Jobs.FarmerJob
{

    class Market : Script
    {
        #region Settings
        private static Random rnd = new Random();

        private static nLog Log = new nLog("Market");

        private static List<FarmerProduct> Products = new List<FarmerProduct>();

        public static int marketmultiplier = rnd.Next(15, 30);
        #endregion

        #region Инициализация Работы Фермера
        [ServerEvent(Event.ResourceStart)]
        public void Event_MarketStart()
        {
            try
            {
                #region Создание блипа, текста, колшейпа
                NAPI.Blip.CreateBlip(501, new Vector3(2367.3, 4881.5, 35.3), 1, 81, "Скупщик урожая", 255, 0, true, 0, 0); // Блип на карте
                NAPI.TextLabel.CreateTextLabel("~w~Скупщик Мелог", new Vector3(2367.39, 4881.526, 43.2), 10f, 0.2f, 0, new Color(255, 255, 255), true, NAPI.GlobalDimension); // Над головой у Ped

                CustomColShape.CreateCylinderColShape(new Vector3(2366.625, 4880.855, 40.70194), 1, 2, 0, ColShapeEnums.MarketMenu);
                NAPI.Marker.CreateMarker(1, new Vector3(2366.625, 4880.855, 40.70194), new Vector3(), new Vector3(), 0.8f, new Color(255, 255, 255, 220));

                #endregion
                Log.Write("FarmLoaded", nLog.Type.Success);
            }
            catch (Exception e)
            {
                Log.Write(e.ToString(), nLog.Type.Error);
            }
        }
        #endregion

        #region Создание объекта итема
        private static object MarketItem(FarmerProduct prod)
        {
            if (prod != null)
            {
                Products.Add(prod);
            }
            List<object> data = new List<object>()
            {
                prod.Price,
                prod.ID,
                prod.Name,
                prod.Ordered
            };
            return data;
        }
        #endregion

        #region Предметы в маркете
        //цена, номер предмета, название, предмет для покупки или для продажи (если true, то коэффициент будет умножаться на выставленную сумму)
        private static List<object> SellItems = new List<object>()
        {
            MarketItem(new FarmerProduct(70, 393, "Урожай", true)),
            MarketItem(new FarmerProduct(20, 394, "Семена", false)),
        };

        private static List<object> BuyItems = new List<object>()
        {
            MarketItem(new FarmerProduct(50, 394, "Семена", false)),
            // только семена!!!
        };
        #endregion

        #region Открыть меню Маркета
         [Interaction(ColShapeEnums.MarketMenu)]
        private static void OpenMarket(ExtPlayer player)
        {
            OpenMarketMenu(player, 0);
        }

        [RemoteEvent("changePage")]
        public static void OpenMarketMenu(ExtPlayer player, int page)
        {
            var hitem = Chars.Repository.isItem(player, "inventory", ItemId.Hay);
            var shitem = Chars.Repository.isItem(player, "inventory", ItemId.Seed);
            int hayscount = hitem != null ? hitem.Item.Count : 0;
            int seedscount = shitem != null ? shitem.Item.Count : 0;
            List<object> data = new List<object>()
            {
                marketmultiplier,
                hayscount,
                seedscount,
            };
            LoadPage(player, page, data);
        }
        #endregion

        #region Взаимодействие с менюшкой Маркета
        public static void LoadPage(ExtPlayer player, int page, object data)
        {
            string json;
            switch (page)
            {
                case 0:
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    Trigger.ClientEvent(player, "loadPage", 0, json);
                    break;
                case 1:
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(BuyItems);
                    Trigger.ClientEvent(player, "loadPage", 1, json);
                    break;
                case 2:
                    json = Newtonsoft.Json.JsonConvert.SerializeObject(SellItems);
                    Trigger.ClientEvent(player, "loadPage", 2, json);
                    break;
            }
        }
        #endregion

        #region BuyFarmerItem
        [RemoteEvent("buyFarmerItem")]
        public static void ButFarmerItem(ExtPlayer player, int id, int count)
        {
            var characterData = player.GetCharacterData();

            if (Chars.Repository.isFreeSlots(player, ItemId.Seed) != 0)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Недостаточно места в инвентаре", 2000);
                return;
            }
            int price = 0;
            string name = null;
            foreach (var item in Products.FindAll(x => x.ID == id))
            {
                price = item.Ordered ? item.Price * marketmultiplier * count : item.Price * count;
                name = item.Name;
            }
            if (characterData.Money < price)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Недостаточно денег", 2000);
                return;
            }
            MoneySystem.Wallet.Change(player, -price);
            Chars.Repository.AddNewItem(player, $"char_{characterData.UUID}", "inventory", ItemId.Seed, count);
            Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы купили {count} {name} за ${price}", 2000);

        }
        #endregion

        #region SellFarmerItem
        [RemoteEvent("sellFarmerItem")]
        public static void SellFarmerItem(ExtPlayer player, int id, int count)
        {
            var characterData = player.GetCharacterData();

            ItemStruct aItem = Chars.Repository.isItem(player, "inventory", (ItemId)id);
            if (aItem == null || aItem.Item.Count < count)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Недостаточно предмета в инвентаре", 2000);
                return;
            }
            int price = 0;
            string name = null;
            foreach (var item in Products.FindAll(x => x.ID == id))
            {
                price = item.Ordered ? item.Price * marketmultiplier * count : item.Price * count;
                name = item.Name;
            }
            MoneySystem.Wallet.Change(player, price);
            Chars.Repository.Remove(player, $"char_{characterData.UUID}", "inventory", aItem.Item.ItemId, count);
            Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы продали {count} {name} за ${price}", 2000);

        }
        #endregion

        #region FarmerProduct
        private class FarmerProduct
        {
            public FarmerProduct(int price, int id, string name, bool ordered)
            {
                Price = price;
                ID = id;
                Name = name;
                Ordered = ordered;
            }
            public int Price { get; set; }
            public int ID { get; set; }
            public string Name { get; set; }
            public bool Ordered { get; set; }
        }
        #endregion
    }
}
