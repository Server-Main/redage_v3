using GTANetworkAPI;
using Localization;
using NeptuneEvo.Character;
using NeptuneEvo.Character.Models;
using NeptuneEvo.Chars;
using NeptuneEvo.Chars.Models;
using NeptuneEvo.Functions;
using NeptuneEvo.Handles;
using NeptuneEvo.Players;
using NeptuneEvo.Players.Models;
using Redage.SDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeptuneEvo.Jobs
{
    public class MinerJob : Script
    {
        private static readonly nLog Log = new nLog(nameof(MinerJob));

        // Кд между точками в секундах
        private static readonly int COOLDOW_WHILE_ACTION = 12;

        private static readonly List<Vector3> _oreMiningPlaces = new List<Vector3>()
        {
            new Vector3(x: 1482.771, y: -2316.191, z: 73.49251),
            new Vector3(x: 1497.086, y: -2309.219, z: 75.03325),
            new Vector3(x: 1489.0879, y: -2322.331, z: 73.74916),
            new Vector3(x: 1476.2048, y: -2316.3345, z: 72.83213),
            new Vector3(x: 1489.9878, y: -2313.3953, z: 74.4368),
            new Vector3(x: 1493.3785, y: -2325.2942, z: 74.3507),
            new Vector3(x: 1474.0308, y: -2358.0068, z: 70.40458),
            new Vector3(x: 1482.8931, y: -2363.817, z: 72.102066),
            new Vector3(x: 1489.8354, y: -2369.478, z: 72.82428)
        };

        private static readonly Dictionary<ItemId, List<ItemId>> _pixacesData = new Dictionary<ItemId, List<ItemId>>()
        {
            { ItemId.Pickaxe1, new List<ItemId>()
            {
                ItemId.Coal,
                ItemId.Iron
            }},
            { ItemId.Pickaxe2, new List<ItemId>()
            {
                ItemId.Coal,
                ItemId.Iron,
                ItemId.Gold,
                ItemId.Sulfur,
            }},
            { ItemId.Pickaxe3, new List<ItemId>()
            {
                ItemId.Coal,
                ItemId.Iron,
                ItemId.Gold,
                ItemId.Sulfur,
                ItemId.Emerald,
                ItemId.Ruby
            }},
        };

        private static readonly Dictionary<ItemId, int> _oreChances = new Dictionary<ItemId, int>()
        {
            { ItemId.Coal, 50 },
            { ItemId.Iron, 35 },
            { ItemId.Gold, 30 },
            { ItemId.Sulfur, 5 },
            { ItemId.Emerald, 3 },
            { ItemId.Ruby, 1 },
        };

        [ServerEvent(Event.ResourceStart)]
        public static void OnStart()
        {
            try
            {
                _oreMiningPlaces.ForEach(position =>
                {
                    CustomColShape.CreateCylinderColShape(position, 5, 2, 0, ColShapeEnums.MinerOrePlace, _oreMiningPlaces.IndexOf(position));
                    NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, position - new Vector3(0, 0, 3), new Vector3(), new Vector3(), 4f, new Color(255, 0, 0, 220), false, 0);
                });
            }
            catch (Exception ex) { Log.Write("OnStart: " + ex.ToString(), nLog.Type.Error); }
        }

        [Interaction(ColShapeEnums.MinerOrePlace)]
        public static void OnInteraction(ExtPlayer player, int index)
        {
            try
            {
                if (player.HasData("minerJob.timeout") && player.GetData<DateTime>("minerJob.timeout") > DateTime.Now)
                {
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, $"Вашему персонажу нужно отдохнуть!", 3000);
                    return;
                }

                if (player.HasData("minerJob.lastPlace") && player.GetData<int>("minerJob.lastPlace") == index)
                {
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, $"Вы уже выкопали тут руду!", 3000);
                    return;
                }

                if (Chars.Repository.isItem(player, "inventory", ItemId.Pickaxe1) is null && Chars.Repository.isItem(player, "inventory", ItemId.Pickaxe2) is null && Chars.Repository.isItem(player, "inventory", ItemId.Pickaxe3) is null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.YouHaveNoPickaxe), 3000);
                    return;
                }

                player.SetData("minerJob.timeout", DateTime.Now.AddSeconds(COOLDOW_WHILE_ACTION));
                player.SetData("minerJob.lastPlace", index);

                Trigger.ClientEvent(player, "merumond.keysMinigame.start", "MinerJob.EndMinigame");
                player.SetData("minerJob.isMinigame", true);

                Trigger.StopAnimation(player);

                Attachments.AddAttachment(player, Attachments.AttachmentsName.Pickaxe);
                Trigger.PlayAnimation(player, "melee@large_wpn@streamed_core", "car_side_attack_a", 1);
            }
            catch (Exception ex) { Log.Write("OnInteraction: " + ex.ToString()); }
        }

        [RemoteEvent("MinerJob.EndMinigame")]
        public static void OnEndMinigame(ExtPlayer player)
        {
            try
            {
                var characterData = player.GetCharacterData();
                if (characterData == null) return;

                if (!player.HasData("minerJob.isMinigame") || !player.GetData<bool>("minerJob.isMinigame")) return;
                player.SetData("minerJob.isMinigame", false);

                ItemStruct picaxeItem = Chars.Repository.isItem(player, "inventory", ItemId.Pickaxe3);
                if (picaxeItem is null) picaxeItem = Chars.Repository.isItem(player, "inventory", ItemId.Pickaxe2);
                if (picaxeItem is null) picaxeItem = Chars.Repository.isItem(player, "inventory", ItemId.Pickaxe1);
                if (picaxeItem is null || !_pixacesData.TryGetValue(picaxeItem.Item.ItemId, out var listOres))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.YouHaveNoPickaxe), 3000);
                    return;
                }

                if (picaxeItem.Item.Data == "" || picaxeItem.Item.Data.Length < 1)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.PickaxeBroken), 3000);
                    Chars.Repository.Remove(player, $"char_{characterData.UUID}", "inventory", picaxeItem.Item.ItemId, 1);
                }
                else
                {
                    picaxeItem.Item.Data = $"{Convert.ToInt32(picaxeItem.Item.Data) - 1}";
                    Chars.Repository.SetItemData(player, picaxeItem.Location, picaxeItem.Index, picaxeItem.Item, true);
                }

                Trigger.StopAnimation(player);
                Attachments.RemoveAttachment(player, Attachments.AttachmentsName.Pickaxe);

                var oreId = GetRandomOre(listOres);
                if (Chars.Repository.AddNewItem(player, $"char_{player.CharacterData.UUID}", "inventory", oreId, 1) == -1)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.NoSpaceInventory), 3000);
                    return;
                }

                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы выкопали {Chars.Repository.ItemsInfo[oreId].Name}", 3000);
            }
            catch (Exception ex) { Log.Write("OnEndMinigame: " + ex.ToString(), nLog.Type.Error); }
        }

        public static ItemId GetRandomOre(List<ItemId> items)
        {
            try
            {
                int pool = 0;

                items.ForEach(item => pool += _oreChances[item]);

                int random = Main.rnd.Next(0, pool);
                int accumulatedProbability = 0;

                foreach (var item in items)
                {
                    accumulatedProbability += _oreChances[item];
                    if (random <= accumulatedProbability)
                        return item;
                }

                return items[0];
            }
            catch (Exception ex) { Log.Write("GetRandomOre: " + ex.ToString()); return ItemId.Coal; }
        }
    }
}
