using Verse;

namespace TestMod
{
    public class TestMod_Settings : ModSettings
    {
        public bool alertEnemy = true;
        
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref alertEnemy, "alertEnemy", true);
        }
    }
}
