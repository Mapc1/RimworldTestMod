using UnityEngine;
using Verse;

namespace TestMod
{
    public class TestMod : Mod
    {
        public static TestMod_Settings settings;
        
        public TestMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<TestMod_Settings>();
            Log.Message("TestMod loaded");
        }

        public override string SettingsCategory() => "Test Mod";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled("Alert_Enemy: ", ref settings.alertEnemy);
            listingStandard.End();
            settings.Write();
        }
    }
}
