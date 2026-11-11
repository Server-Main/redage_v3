using System;
using System.Collections.Generic;
using NeptuneEvo;
using Redage.SDK;
using GTANetworkAPI;
using NeptuneEvo.Handles;
using NeptuneEvo.Character.Models;
using NeptuneEvo.Functions;

class Cinema : Script
{

    public static string cinema_url = "";
    public static int cinema_time = 0;
    public static bool cinema_playing = false;
    public static bool cinema_open = false;

    private static nLog RLog = new nLog("Cinema");

    public static Vector3 cinema_enter = new Vector3(301.8391, 137.31125, 103.841125);
    public static Vector3 cinema_exit_here = new Vector3(301.8391, 137.31125, 103.841125);

    public static List<Vector3> seats = new List<Vector3>();

    [ServerEvent(Event.ResourceStart)]
    public static void OnResourceStart()
    {
        try
        {
            CustomColShape.CreateCylinderColShape(cinema_enter, 2, 3, 0, ColShapeEnums.Cinema);
            RLog.Write("Loaded", nLog.Type.Info);
        }
        catch (Exception e) { RLog.Write(e.ToString(), nLog.Type.Error); }
    }
    public Cinema()
    { 
        seats.Add(new Vector3(-1418.052, -247.6875, 16.79762));
        seats.Add(new Vector3(-1419.253, -247.6236, 16.79766));
        seats.Add(new Vector3(-1421.659, -247.3815, 16.79851));
        seats.Add(new Vector3(-1423.86, -247.2797, 16.79926));
        seats.Add(new Vector3(-1427.273, -247.3671, 16.79961));
        seats.Add(new Vector3(-1430.556, -247.2636, 16.79898));
        seats.Add(new Vector3(-1432.485, -247.544, 16.79794));
        seats.Add(new Vector3(-1432.636, -249.7136, 16.7938));
        seats.Add(new Vector3(-1429.784, -249.8186, 16.79434));
        seats.Add(new Vector3(-1427.626, -249.6948, 16.7951));
        seats.Add(new Vector3(-1424.524, -249.5078, 16.79521));
        seats.Add(new Vector3(-1422.475, -249.7936, 16.79416));
        seats.Add(new Vector3(-1419.898, -249.7854, 16.79353));
        seats.Add(new Vector3(-1419.298, -251.0738, 16.79095));
        seats.Add(new Vector3(-1421.584, -250.7939, 16.79206));
        seats.Add(new Vector3(-1423.801, -250.8243, 16.79255));
        seats.Add(new Vector3(-1426.224, -250.7932, 16.7932));
        seats.Add(new Vector3(-1428.242, -250.6987, 16.79305));
        seats.Add(new Vector3(-1430.339, -250.802, 16.79233));
        seats.Add(new Vector3(-1432.28, -251.1464, 16.79117));


        seats.Add(new Vector3());
        Blip temp_blip = null;
        temp_blip = NAPI.Blip.CreateBlip(cinema_enter);
        temp_blip.Sprite = 135;
        temp_blip.Name = "Кинотеатр";
        temp_blip.Color = 4;
        temp_blip.Scale = 0.8f;
        temp_blip.ShortRange = true;
        temp_blip.Dimension = 0;

        NAPI.Marker.CreateMarker(1, cinema_enter - new Vector3(0, 0,1.0), new Vector3(), new Vector3(), 0.8f, new Color(67, 140, 239, 200));

        CinemaTime();
    }

    public void CinemaTime()
    {
        Timers.StartTask("CinemaTimer", 1000, () =>
        {
            if (cinema_playing == true)
            {
                cinema_time++;
            }
        });
    }

    [Interaction(ColShapeEnums.Cinema)]
    public static void KeyPressY(ExtPlayer player)
    {
        var characterData = player.CharacterData;

        if (cinema_open == false && characterData.AdminLVL == 0)
        {
            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Кинотеатр закрыт", 3000);
            return;
        }
        player.TriggerEvent("enterCinema");
        cinema_open = true;
        player.Dimension = 100;
        characterData.EnterCinema = 1;
        return;
    }

    [RemoteEvent("requestCinemaScreen")]
    public static void requestCinemaScreen(ExtPlayer player)
    {
        var characterData = player.CharacterData;

        Random rnd = new Random();
        int random_seat = rnd.Next(0, seats.Count - 1);
        player.Position = seats[random_seat];
        player.Rotation = new Vector3(0,0,180f);
        player.SetData("Cinema", true);
        player.SetData("statuscinema", true);

        if (characterData.AdminLVL > 0)
        {
            if (cinema_url == "null")
            {
                player.TriggerEvent("showCinemaScreen", characterData.FirstName, cinema_time, "null", true);

            }
            else
            {
                player.TriggerEvent("showCinemaScreen", characterData.FirstName, cinema_time, cinema_url, true);
            }
        }
        else
        {
            if (cinema_url == "null")
            {
                player.TriggerEvent("showCinemaScreen", characterData.FirstName, cinema_time, "null", false);

            }
            else
            {
                player.TriggerEvent("showCinemaScreen", characterData.FirstName, cinema_time, cinema_url, false);
            }
        }
    }

    [RemoteEvent("exitCinema")]
    public static void exitCinema(ExtPlayer player)
    {
        var characterData = player.CharacterData;

        player.Position = new Vector3(301.8391, 137.31125, 103.841125);
        player.SetData("Cinema", false);
        player.SetData("statuscinema", false);
        player.Dimension = 0;
        characterData.EnterCinema = -1;
    }

    [RemoteEvent("Server:Cinema_Open")]
    public static void CinemaOpen(ExtPlayer player, bool toggle)
    {
        if(toggle == true)
        {
            if(cinema_open == true)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Киноетатр уже открыт", 3000);
                return;
            }
        
            cinema_open = true;
        }
        else
        {
            if (cinema_open == false)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Кинотеатр уже закрыт", 3000);
                return;
            }

            cinema_open = false;
        }
    }

    [RemoteEvent("addCinemaVideo")]
    public static void addCinemaVideo(ExtPlayer player, string url)
    {
        var characterData = player.CharacterData;

        string url_replace = url.Replace("https://www.youtube.com/watch?v=", "");
        cinema_url = url_replace;
        cinema_time = 0;
        cinema_playing = true;
        foreach (var target in API.Shared.GetAllPlayers())
        {
            if (target.GetData<bool>("statuscinema") == true && target.GetData<bool>("Cinema") == true)
            {
                target.TriggerEvent("showCinemaScreen", characterData.FirstName, cinema_time, url_replace);
            }
        }    
    }
}

