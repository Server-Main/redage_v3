using GTANetworkAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NeptuneEvo.GUI;
using NeptuneEvo.MoneySystem;
using Redage.SDK;
using System.Threading;
using NeptuneEvo.Chars.Models;
using NeptuneEvo.Core;
using NeptuneEvo.Handles;
using NeptuneEvo.Character.Models;
using NeptuneEvo.Character;
using NeptuneEvo.Players;
using NeptuneEvo.Functions;

namespace NeptuneEvo.Core
{
    class RodManager : Script
    {

        [ServerEvent(Event.PlayerDeath)]
        public void OnPlayerDeath(ExtPlayer player, ExtPlayer killer, uint reason)
        {
            BasicSync.DetachObject(player);
        }

        public static Dictionary<ItemId, int> Fish_Item = new Dictionary<ItemId, int> ()
        {
            { ItemId.Koroska, 5200 },
            { ItemId.Kyndja, 5900  },
            { ItemId.Lococ, 5000 },
            { ItemId.Okyn, 2500} ,
            { ItemId.Ocetr, 3000 },
            { ItemId.Skat, 6300 },
            { ItemId.Tunec, 7500 },
            { ItemId.Ygol, 2500 },
            { ItemId.Amyr, 5300 },
            { ItemId.Chyka, 3200 },
        };

        public static Dictionary<string, int> Fish_Name = new Dictionary<string, int>()
        {
            { "Корюшка", 5200 },
            { "Кунджа", 5900  },
            { "Лосось", 5000 },
            { "Окунь", 2500} ,
            { "Осётр", 3000 },
            { "Скат", 6300 },
            { "Тунец", 7500 },
            { "Уголь", 2500 },
            { "Амур", 5300 },
            { "Щука", 3200 },
        };

        public static Dictionary<int, ItemId> FishItems1 = new Dictionary<int, ItemId>
        {
            {1, ItemId.Lococ },
            {2, ItemId.Okyn },
            {3, ItemId.Okyn },
            {4, ItemId.Okyn },
            {5, ItemId.Okyn },
            {6, ItemId.Ocetr },
            {7, ItemId.Ygol },
            {8, ItemId.Chyka },
            {9, ItemId.Chyka },
            {10, ItemId.Chyka },
        };

        public static Dictionary<int, ItemId> FishItems2 = new Dictionary<int, ItemId>
        {
            {1, ItemId.Koroska },
            {2, ItemId.Koroska },
            {3, ItemId.Koroska },
            {4, ItemId.Lococ },
            {5, ItemId.Okyn },
            {6, ItemId.Okyn },
            {7, ItemId.Okyn },
            {8, ItemId.Ocetr },
            {9, ItemId.Skat },
            {10, ItemId.Skat },
            {12, ItemId.Ygol },
            {13, ItemId.Ygol },
            {15, ItemId.Chyka },
            {16, ItemId.Chyka },
            {17, ItemId.Chyka },
        };

        public static Dictionary<int, ItemId> FishItems3 = new Dictionary<int, ItemId>
        {
            {1, ItemId.Koroska },
            {2, ItemId.Kyndja },
            {3, ItemId.Lococ },
            {4, ItemId.Okyn },
            {5, ItemId.Okyn },
            {6, ItemId.Ocetr },
            {7, ItemId.Skat },
            {8, ItemId.Tunec },
            {9, ItemId.Ygol },
            {10, ItemId.Amyr },
            {11, ItemId.Chyka },
            {12, ItemId.Chyka },
        };

        public static Dictionary<int, ItemId> TypeRod = new Dictionary<int, ItemId>
        {
            {1, ItemId.Rod },
            {2, ItemId.RodUpgrade },
            {3, ItemId.RodMK2 },
        };

        public static ItemId GetSellingItemId(string name)
        {
            var type = ItemId.Naz;
            switch (name)
            {
                case "Корюшка":
                    type = ItemId.Koroska;
                    break;
                case "Кунджа":
                    type = ItemId.Kyndja;
                    break;
                case "Лосось":
                    type = ItemId.Lococ;
                    break;
                case "Окунь":
                    type = ItemId.Okyn;
                    break;
                case "Осётр":
                    type = ItemId.Ocetr;
                    break;
                case "Скат":
                    type = ItemId.Skat;
                    break;
                case "Тунец":
                    type = ItemId.Tunec;
                    break;
                case "Угорь":
                    type = ItemId.Ygol;
                    break;
                case "Чёрный амур":
                    type = ItemId.Amyr;
                    break;
                case "Щука":
                    type = ItemId.Chyka;
                    break;
            }
            return type;
        }

        public static string GetNameByItemId(ItemId tupe)
        {
            string type = "nope";
            switch (tupe)
            {
                case ItemId.Koroska:
                    type = "Корюшка";
                    break;
                case ItemId.Kyndja:
                    type = "Кунджа";
                    break;
                case ItemId.Lococ:
                    type = "Лосось";
                    break;
                case ItemId.Okyn:
                    type = "Окунь";
                    break;
                case ItemId.Ocetr:
                    type = "Осётр";
                    break;
                case ItemId.Skat:
                    type = "Скат";
                    break;
                case ItemId.Tunec:
                    type = "Тунец";
                    break;
                case ItemId.Ygol:
                    type = "Угорь";
                    break;
                case ItemId.Amyr:
                    type = "Чёрный амур";
                    break;
                case ItemId.Chyka:
                    type = "Щука";
                    break;
            }

            return type;
        }

        private static nLog Log = new nLog("RodManager");

        private static int lastRodID = -1;

        [ServerEvent(Event.ResourceStart)]

        public void onResourceStart()
        {
            try
            {
                var result = MySQL.QueryRead($"SELECT * FROM rodings");
                if (result == null || result.Rows.Count == 0)
                {
                    Log.Write("DB rod return null result.", nLog.Type.Warn);
                    return;
                }
                foreach (DataRow Row in result.Rows)
                {
                    Vector3 pos = JsonConvert.DeserializeObject<Vector3>(Row["pos"].ToString());

                    Roding data = new Roding(Convert.ToInt32(Row["id"]), pos, Convert.ToInt32(Row["radius"]));
                    int id = Convert.ToInt32(Row["id"]);
                    lastRodID = id;
                }
                string _textlabel = "";

                foreach(var fish_price in Fish_Name)
                {
                    _textlabel += $"{fish_price.Key} - {fish_price.Value}$\n";
                }

                NAPI.Blip.CreateBlip(628, new Vector3(2003.9402, 3790.565, 32.1808), 1.2f, 3, "Скупка рыбы", 255, 0, true, dimension: 0);
                CustomColShape.CreateCylinderColShape(new Vector3(2003.9402, 3790.565, 31.4), 1, 2, 0, ColShapeEnums.FishSell);
                NAPI.Marker.CreateMarker(1, new Vector3(2003.9402, 3790.565, 31), new Vector3(), new Vector3(), 0.8f, new Color(255, 255, 255, 220));
                NAPI.TextLabel.CreateTextLabel(Main.StringToU16(_textlabel + "\n\n~w~Нажмите\n~r~'Взаимодействие'"), new Vector3(2003.9402, 3790.565, 33.5808), 5F, 0.3F, 0, new Color(255, 255, 255));
            }
            catch (Exception e)
            {
                Log.Write("EXCEPTION AT \"RODINGS\":\n" + e.ToString(), nLog.Type.Error);
            }
        }

        public enum AnimationFlags
        {
            Loop = 1 << 0,
            StopOnLastFrame = 1 << 1,
            OnlyAnimateUpperBody = 1 << 4,
            AllowPlayerControl = 1 << 5,
            Cancellable = 1 << 7
        }
        [Command("fishing")]
        public static void CMD_fishing(ExtPlayer player)
        {
            try
            {
                var characterData = player.GetCharacterData();

                if (Chars.Repository.isItem(player, "inventory", ItemId.RodMK2) != null)
                {
                    useInventory(player, 3);
                }
                else if(Chars.Repository.isItem(player, "inventory", ItemId.RodMK2) != null)
                {
                    useInventory(player, 2);
                }
                else if(Chars.Repository.isItem(player, "inventory", ItemId.Rod) != null)
                {
                    useInventory(player, 1);
                }
                else Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "У вас нет удочки", 3000);

            }
            catch (Exception e)
            {
                Log.Write($"CMD_fishing Exception: {e.ToString()}");
            }
        }

        [Interaction(ColShapeEnums.FishSell)]
        public static void SellFish(ExtPlayer player)
        {
            try
            {
                int sum = 0;

                foreach (var item_fish in Fish_Item)
                {
                    if (Chars.Repository.isItem(player, "inventory", item_fish.Key) != null)
                    {
                        var count_item = Chars.Repository.isItem(player, "inventory", item_fish.Key).Item.Count;

                        sum += count_item * item_fish.Value;

                        Chars.Repository.Remove(player, $"char_{player.GetCharacterData().UUID}", "inventory", item_fish.Key, count_item);
                    }
                }

                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"Вы продали рыбы на {sum}$", 3000);
                Wallet.Change(player, sum);
            }
            catch (Exception e)
            {
                Log.Write($"F(SellFish): {e.ToString()}");
            }
        }

        public static void setallow(ExtPlayer player)
        {
            player.SetData("FISHING", true);
            Main.OnAntiAnim(player);
            Trigger.ClientEvent(player, "freeze", true);
            player.PlayAnimation("amb@world_human_stand_fishing@base", "base", 31);
            BasicSync.AttachObjectToPlayer(player, NAPI.Util.GetHashKey("prop_fishing_rod_01"), 60309, new Vector3(0.03, 0, 0.02), new Vector3(0, 0, 50));
            NAPI.Task.Run(() => {
                try
                {
                    if (player != null)
                    {
                        RodManager.allowfish(player);
                    }
                }
                catch { }
            }, 18000);
        }

        public static void allowfish(ExtPlayer player)
        {
            player.PlayAnimation("amb@world_human_stand_fishing@idle_a", "idle_c", 31);
            Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"Что-то клюнуло", 1000);
            NAPI.Task.Run(() =>
            {
                player.TriggerEvent("client::fishstart");

            });

            player.SetData("KD", Timers.StartOnceTask(9000, () =>
            {
                try
                {
                    if (player != null && !player.IsCharacterData())
                    {
                        if (player.HasData("FISHING") && player.GetData<bool>("FISHING"))
                        {
                            Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"Рыба сошла с крючка!", 3000);
                            NAPI.Task.Run(() => {
                                player.TriggerEvent("client::stopfish");
                            });
                            RodManager.crashpros(player);
                        }
                    }
                }
                catch { }
            }));
        }
        public static void OpenBizSellShopMenu(ExtPlayer player)
        {
            var sessionData = player.GetSessionData();
            if (sessionData == null) return;
            if (!player.IsCharacterData()) return;
            if (sessionData.BizID == -1 || !BusinessManager.BizList.ContainsKey(sessionData.BizID)) return;
            var biz = BusinessManager.BizList[sessionData.BizID];
            var jsonData = new List<Fractions.Manager.FracMatsData>();
            int index = 0;
            foreach (var p in biz.Products)
            {

                var busProductData = BusinessManager.BusProductsData[p.Name];
                if (busProductData.ItemId != ItemId.Debug) jsonData.Add(new Fractions.Manager.FracMatsData(index, Chars.Repository.ItemsInfo[busProductData.ItemId].Name, Chars.Repository.ItemsInfo[busProductData.ItemId].Icon, $"{p.Price}$", (int)busProductData.ItemId));
                index++;
            }
            Trigger.ClientEvent(player, "client.sm.openFishshop", JsonConvert.SerializeObject(jsonData));
        }

        public static void crashpros(ExtPlayer player)
        {
            NAPI.Task.Run(() => {
                try
                {
                    player.PlayAnimation("rcmcollect_paperleadinout@", "kneeling_arrest_get_up", 33);
                    player.StopAnimation(); //Вдруг если не сработает выше
                    Main.OffAntiAnim(player);
                    BasicSync.DetachObject(player);
                    player.SetData("FISHING", false);
                    Trigger.ClientEvent(player, "freeze", false);
                    string time = player.GetData<string>("KD");
                    Timers.Stop(time);
                }
                catch { }
            });
        }

        [RemoteEvent("server::givefish")]
        public static void giveRandomFish(ExtPlayer player)
        {

            var characterData = player.GetCharacterData();

            try
            {
                if (Chars.Repository.isFreeSlots(player, ItemId.Ocetr) != 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Недостаточно места в инвентаре", 3000);
                    RodManager.crashpros(player);
                    return;
                }
                if (player.GetData<int>("FISHLEVEL") == 1)
                {
                    var rnd = new Random();
                    int fishco = rnd.Next(1, RodManager.FishItems1.Count);
                    Chars.Repository.AddNewItem(player, $"char_{characterData.UUID}", "inventory", RodManager.FishItems1[fishco], 1);
                }
                if (player.GetData<int>("FISHLEVEL") == 2)
                {
                    var rnd = new Random();
                    int fishco = rnd.Next(1, RodManager.FishItems2.Count);
                    Chars.Repository.AddNewItem(player, $"char_{characterData.UUID}", "inventory", RodManager.FishItems2[fishco], 1);
                }
                if (player.GetData<int>("FISHLEVEL") == 3)
                {
                    var rnd = new Random();
                    int fishco = rnd.Next(1, RodManager.FishItems3.Count);
                    Chars.Repository.AddNewItem(player, $"char_{characterData.UUID}", "inventory", RodManager.FishItems3[fishco], 1);
                }
            }
            catch { }
            RodManager.crashpros(player);
        }

        public static void useInventory(ExtPlayer player, int level)
        {
            var characterData = player.GetCharacterData();

            if (player.IsInVehicle)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не должны находится в машине!", 3000);
                //GUI.Dashboard.Close(player);
                return;
            }
            if (player.GetData<bool>("FISHING") == true)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы уже рыбачите!", 3000);
                return;
            }
            ItemStruct aItem = Chars.Repository.isItem(player, "inventory", ItemId.Naz);
            if (aItem == null)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "У Вас нет наживки", 3000);
                return;
            }
            if (!player.HasData("ALLOWFISHING") || player.GetData<bool>("ALLOWFISHING") == false)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "В данном месте нельзя рыбачить", 3000);
                return;
            }
            
            Chars.Repository.Remove(player, $"char_{characterData.UUID}", "inventory", ItemId.Naz, 1);
            player.SetData("FISHLEVEL", level);
            RodManager.setallow(player);
            Commands.RPChat("me", player, $"Начал(а) рыбачить");
        }

        public class Roding
        {
            public int ID { get; set; }
            public float Radius { get; set; }
            public Vector3 AreaPoint { get; set; }

            [JsonIgnore]
            private Blip blip = null;
            [JsonIgnore]
            private ColShape shape = null;

            public Roding(int id, Vector3 areapoint, float radius)
            {
                ID = id;
                AreaPoint = areapoint;
                Radius = radius;
                blip = NAPI.Blip.CreateBlip(68, AreaPoint, 0.8f, 4, "Зона для рыбалки", 255, 0, true, dimension: 0);
                shape = NAPI.ColShape.CreateCylinderColShape(AreaPoint, Radius, 6, 0);
                shape.OnEntityEnterColShape += (s, entity) =>
                {
                    try
                    {
                        if (!entity.IsInVehicle || Chars.Repository.isItem((ExtPlayer)entity, "inventory", ItemId.Naz) != null)
                        {
                            Notify.Send(entity, NotifyType.Info, NotifyPosition.BottomCenter, $"Для того, чтобы начать рыбачить: /fishing", 3000);
                        }
                        
                        entity.SetData("ALLOWFISHING", true);
                    }
                    catch (Exception e) { Console.WriteLine("shape.OnEntityEnterColshape: " + e.Message); }
                };
                shape.OnEntityExitColShape += (s, entity) =>
                {
                    try
                    {
                        entity.SetData("ALLOWFISHING", false);
                    }
                    catch (Exception e) { Console.WriteLine("shape.OnEntityEnterColshape: " + e.Message); }
                };
            }

        }
    }
}
