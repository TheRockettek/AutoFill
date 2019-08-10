using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoFill
{
    [ModLoader.ModManager]
    public static class AutoFill
    {
        [ModLoader.ModCallback(ModLoader.EModCallbackType.OnPlayerClicked, "ImRock.AutoFill.OnPlayerClickedType")]
        public static void OnPlayerClicked(Players.Player player, Shared.PlayerClickedData playerClickedData)
        {
            if (player.ActiveColony != null && playerClickedData.HitType == Shared.PlayerClickedData.EHitType.Block && playerClickedData.ConsumedType == Shared.PlayerClickedData.EConsumedType.ChangedBlock && playerClickedData.IsConsumed && ItemTypes.TryGetType(playerClickedData.TypeToBuild, out ItemTypes.ItemType block))
            {
                int blockCount = player.Inventory.GetAmount(block.ItemIndex);
                int looted = Mathf.Clamp(block.MaxStackSize - blockCount, 0, player.ActiveColony.Stockpile.AmountContained(block.ItemIndex));
                if (blockCount == 1 && looted > 0 && player.ActiveColony.Stockpile.TryRemove(block.ItemIndex, looted))
                {
                    player.Inventory.TryAdd(block.ItemIndex, looted);
                }
            }
        }
    }
}
