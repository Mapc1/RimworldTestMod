using RimWorld;
using Verse;
using System.Text;

namespace TestMod
{
    public class Alert_Enemy : Alert 
    {
        private List<Pawn> enemyPawnResult = new List<Pawn>();
        
        public Alert_Enemy()
        {
            this.defaultLabel = "Enemy Alert";
            this.defaultPriority = AlertPriority.High;
        }
    
        private List<Pawn> EnemyPawns
        {
            get
            {
                this.enemyPawnResult.Clear();
                List<Pawn> pawnList = Find.CurrentMap.mapPawns.AllPawns;
                foreach (var pawn in pawnList.Where
                         (pawn => 
                             !pawn.RaceProps.Animal                               && // Pawn is not an animal (human/mechanoid/insect)
                             pawn.Faction != Faction.OfPlayer                     && // Pawn is not of the player's faction  
                             pawn.Faction.HostileTo(Faction.OfPlayer)             && // Pawn's faction is hostile
                             !Find.CurrentMap.fogGrid.IsFogged(pawn.PositionHeld) && // Pawn is not in a hidden area
                             !pawn.InContainerEnclosed                               // Pawn is not in a container (ie. Criptosleep casket) which avoids revealing ancients
                         ))
                {
                        this.enemyPawnResult.Add(pawn);
                }
    
                return this.enemyPawnResult;
            }
        }

        public override AlertReport GetReport()
        {
            return TestMod.settings.alertEnemy && !EnemyPawns.NullOrEmpty() ? AlertReport.CulpritsAre(EnemyPawns) : false;
        } 
    
        public override TaggedString GetExplanation()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine();
            foreach (Pawn pawn in this.EnemyPawns)
                stringBuilder.AppendLine(" - " + pawn.NameShortColored.Resolve());
            return (TaggedString)string.Format((string)"There are enemies on the map: {0}",
                (object)stringBuilder.ToString());
        }
    }
}