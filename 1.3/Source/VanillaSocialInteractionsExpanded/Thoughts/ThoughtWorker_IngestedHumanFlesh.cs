﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace VanillaSocialInteractionsExpanded
{
	public class ThoughtWorker_IngestedHumanFlesh : ThoughtWorker
	{
		protected override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn other)
		{
			if (!other.RaceProps.Humanlike)
			{
				return false;
			}
			if (!RelationsUtility.PawnsKnowEachOther(p, other))
			{
				return false;
			}
			if (p.story.traits.HasTrait(TraitDefOf.Cannibal))
            {
				return false;
            }
			if (ModsConfig.IdeologyActive && (p.Ideo?.HasHumanMeatEatingRequiredPrecept() ?? false))
            {
				return false;
            }
			Predicate<Tale> validator = delegate (Tale t)
			{
				if (!VSIE_Utils.HaveNoticedTale(p, t))
				{
					return false;
				}
				return other == t.DominantPawn;
			};
			var tale = VSIE_Utils.GetLatestTale(this.def.taleDef, validator);
			if (tale != null)
            {
				return true;
            }
			return false;
		}
	}
}
