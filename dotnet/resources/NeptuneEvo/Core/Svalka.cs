using GTANetworkAPI;
using Localization;
using NeptuneEvo.Accounts;
using NeptuneEvo.Character;
using NeptuneEvo.Functions;
using NeptuneEvo.Handles;
using NeptuneEvo.Players;
using Redage.SDK;
using System;

namespace NeptuneEvo.Core
{
    public class Svalka : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void onResourceStart()
        {
            //API.Shared.CreateTextLabel("~g~Свалка", new Vector3(-469.58698, -1717.8026, 18.669134 + 0.8f), 4.0f, 0.1f, 4, new Color(255, 255, 255, 255), false, 0);
            //NAPI.Blip.CreateBlip(163, new Vector3(-469.58698, -1717.8026, 17.569134), 1, 49, Main.StringToU16("Свалка"), 255, 0, true, 0, 0); // Блип на карте   
            //CustomColShape.CreateCylinderColShape(new Vector3(-469.58698, -1717.8026, 18.669134), 3, 2, 0, ColShapeEnums.Svalka);
        }

        [Interaction(ColShapeEnums.Svalka)]
        private void SellCar(ExtPlayer player)
        {
                var sessionData = player.GetSessionData();
                if (sessionData == null)
                    return;

                var accountData = player.GetAccountData();
                if (accountData == null)
                    return;

                var characterData = player.GetCharacterData();
                if (characterData == null)
                    return;

                if (!player.IsInVehicle) return;

                var vehicle = (ExtVehicle)player.Vehicle;
                var vehicleData = VehicleManager.GetVehicleToNumber(vehicle.NumberPlate);

                if (vehicleData == null) return;

                if (vehicleData.Holder != sessionData.Name) return;

                sessionData.CarSellGov = vehicle.NumberPlate;

                int price = 0;
                if (BusinessManager.BusProductsData.ContainsKey(vehicleData.Model))
                {
                    switch (accountData.VipLvl)
                    {
                        case 0: // None
                        case 1: // Bronze
                        case 2: // Silver
                            price = Convert.ToInt32(BusinessManager.BusProductsData[vehicleData.Model].Price * 0.5);
                            break;
                        case 3: // Gold
                            price = Convert.ToInt32(BusinessManager.BusProductsData[vehicleData.Model].Price * 0.6);
                            break;
                        case 4: // Platinum
                        case 5: // Media Platinum
                            price = Convert.ToInt32(BusinessManager.BusProductsData[vehicleData.Model].Price * 0.7);
                            break;
                        default:
                            price = Convert.ToInt32(BusinessManager.BusProductsData[vehicleData.Model].Price * 0.5);
                            break;
                    }
                }
                Trigger.ClientEvent(player, "openDialog", "CAR_SELL_TOGOV", $"Вы действительно хотите продать государству {vehicleData.Model} ({vehicleData.Number}) за ${MoneySystem.Wallet.Format(price)}?");
                return;
            }
        }
    }
