using System.Linq;
using GTANetworkAPI;
using NeptuneEvo.Handles;
using Redage.SDK;

namespace NeptuneEvo.Players.Restart
{
    public class Events : Script
    {
       [RemoteEvent("restart.add")]
      public void Add(ExtPlayer player, string text)
        {
            Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Не получится, GG WP {text}", 3000);
        }
    }
}