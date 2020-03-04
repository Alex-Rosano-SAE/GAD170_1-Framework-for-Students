using UnityEngine;

/// <summary>
/// This class is responsible for converting a battle result into xp to be awarded to the player.
/// 
/// TODO:
///     Respond to battle outcome with xp calculation based on;
///         player win 
///         how strong the win was
///         stats/levels of the dancers involved
///     Award the calculated XP to the player stats
///     Raise the player level up event if needed
/// </summary>
public class XPHandler : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnBattleConclude += GainXP;
    }

    private void OnDisable()
    {
        GameEvents.OnBattleConclude += GainXP;
    }

    public void GainXP(BattleResultEventData data)
    {
        if (data.outcome > 0)
        {
            float xpGain = (data.outcome * 10) + Random.Range(0, data.player.luck) +
                           (((data.player.rhythm + data.player.rhythm - data.npc.rhythm) / 2) * data.player.level);

            xpGain = Mathf.RoundToInt(Mathf.Clamp(xpGain, 0, 1000));
            data.player.xp += (int) xpGain;
            GameEvents.PlayerXPGain((int) xpGain);

            if (data.player.xp > data.player.level * 100);
            {
                data.player.level++;
                GameEvents.PlayerLevelUp(data.player.level);
                StatsGenerator.AssignUnusedPoints(data.player, 5);
            }
        }
    }
}
    


