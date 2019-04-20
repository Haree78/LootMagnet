﻿
using System.Collections.Generic;
using System.Linq;

namespace LootMagnet {

    public enum Rep {
        LOATHED,
        HATED,
        DISLIKED,
        INDIFFERENT,
        LIKED,
        FRIENDLY,
        HONORED,
        ALLIED
    }

    public class RepCfg {
        public Rep Reputation = Rep.INDIFFERENT;
        public float RollupMultiComponent = 0f;
        public float RollupMultiMech = 0f;
        public float HoldbackTrigger = 0f;
    }

    public class HoldbackCfg {
        public int[] PickRange = new int[] { 2, 6 };

        // Acceptance reputation gain is equal to the the max possible reputation of the contract times this value
        public float RepMultiAccept = 0.5f;

        // Refusal reputation loss is equal to the the max possible reputation of the contract times this value
        public float RepMultiRefuse = -2.5f;

        public float RepMultiDisputeMRB = -0.3f;
        public float RepMultiDisputeSuccess = -0.8f;
        public float RepMultiDisputeFail = -0.3f;
        public float RepMultiDisputeCriticalFail = -0.1f;

        public float DisputeSuccessBase = 40.0f;
        public float DisputeCritChance = 5.0f;
        public float DisputeMRBSuccessFactor = 10.0f;
        public int DisputeSuccessRandomBound = 10;

        public float DisputeMRBFeeFactor = -0.1f;
        public float DisputeFailPayoutFactor = -0.5f;
        public float DisputeCritFailPayoutFactor = -1.5f;
    }

    public class ModConfig {

        // If true, many logs will be printed
        public bool Debug = false;

        public bool DeveloperMode = false;

        // The values used to define the base amounts for rollup
        public float[] RollupMRBValue = new float[] { 40000f, 60000f, 90000f, 130000f, 180000f, 240000f };

        public List<RepCfg> Reputation = new List<RepCfg>() {
            new RepCfg{ Reputation = Rep.LOATHED, RollupMultiComponent = 0f, RollupMultiMech = 0f, HoldbackTrigger = 60f },
            new RepCfg{ Reputation = Rep.HATED, RollupMultiComponent = 0f, RollupMultiMech = 0f, HoldbackTrigger = 48f },
            new RepCfg{ Reputation = Rep.DISLIKED, RollupMultiComponent = 0f, RollupMultiMech = 0f, HoldbackTrigger = 32f },
            new RepCfg{ Reputation = Rep.INDIFFERENT, RollupMultiComponent = 1f, RollupMultiMech = 0f, HoldbackTrigger = 16f },
            new RepCfg{ Reputation = Rep.LIKED, RollupMultiComponent = 5f, RollupMultiMech = 0f, HoldbackTrigger = 8f },
            new RepCfg{ Reputation = Rep.FRIENDLY, RollupMultiComponent = 9f, RollupMultiMech = 20f, HoldbackTrigger = 4f },
            new RepCfg{ Reputation = Rep.HONORED, RollupMultiComponent = 13f, RollupMultiMech = 30f, HoldbackTrigger = 2f },
            new RepCfg{ Reputation = Rep.ALLIED, RollupMultiComponent = 21f, RollupMultiMech = 180f, HoldbackTrigger = 1f },
        };

        public HoldbackCfg Holdback = new HoldbackCfg();

        public void LogConfig() {
            LootMagnet.Logger.Log("=== MOD CONFIG BEGIN ===");

            LootMagnet.Logger.Log($"  DEBUG: {this.Debug}");

            string rollupMRBVal = string.Join(", ", RollupMRBValue.Select(v => v.ToString("0.00")).ToArray());
            LootMagnet.Logger.Log($"  MRB Rollup Values: {rollupMRBVal}");

            LootMagnet.Logger.Log($"  -- Faction Reputation Values --");
            foreach (RepCfg factionCfg in Reputation) {
                LootMagnet.Logger.Log($"  Reputation:{factionCfg.Reputation} ComponentRollup:{factionCfg.RollupMultiComponent} MechRollup:{factionCfg.RollupMultiMech} HoldbackTrigger:{factionCfg.HoldbackTrigger}%");
            }
            LootMagnet.Logger.Log($"");

            LootMagnet.Logger.Log($"  -- Holdback Values --");
            LootMagnet.Logger.Log($"  Holdback Picks: {Holdback.PickRange[0]} to {Holdback.PickRange[1]}");

            LootMagnet.Logger.Log($"  Reputation ACCEPT:x{Holdback.RepMultiAccept} REFUSE:x{Holdback.RepMultiRefuse} DISPUTE_MRB:x{Holdback.RepMultiDisputeMRB}");
            LootMagnet.Logger.Log($"  Reputation DISPUTE_SUCCESS:x{Holdback.RepMultiDisputeSuccess} DISPUTE_FAIL:x{Holdback.RepMultiDisputeFail} DISPUTE_CRIT_FAIL:x{Holdback.RepMultiDisputeCriticalFail}");

            LootMagnet.Logger.Log($"  Dispute Chance BASE:{Holdback.DisputeSuccessBase}% CRIT_CHANCE:{Holdback.DisputeCritChance}% " +
                $"MRB_FACTOR:{Holdback.DisputeMRBSuccessFactor}% RANDOM:{Holdback.DisputeSuccessRandomBound}%");

            LootMagnet.Logger.Log($"  MRB_Fees:x{Holdback.DisputeMRBFeeFactor} FAIL_PAYOUT:x{Holdback.DisputeFailPayoutFactor} CRIT_FAIL_PAYOUT:X{Holdback.DisputeCritFailPayoutFactor}");

            LootMagnet.Logger.Log("=== MOD CONFIG END ===");
        }
    }
}
