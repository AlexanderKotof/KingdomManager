using UnityEngine;
namespace KM.Features.GameEventsFeature.Events.Bonuses
{
    public class BonusesArray : ScriptableObject
    {
        public Bonus[] bonuses = new Bonus[0];

        public void Activate()
        {
            foreach (var bonus in bonuses)
            {
                bonus?.Activate();
            }
        }

        public string GetDescription()
        {
            string description = "";
            foreach (var bonus in bonuses)
            {
                var str = bonus.GetDescription();
                if (str != null)
                    description += str + "\n";
            }
            return description;
        }
    }
}