using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class PersonalityTester {
    public static void Main(string[] args) {
        Random random = new Random();
        for (int i = 0; i < 20; ++i) {
            float energy = random.NextSingle();
            float fear = random.NextSingle();
            float anger = random.NextSingle();
            float love = random.NextSingle();
            float dominance = random.NextSingle();
            float curiosity = random.NextSingle();
            string temperament = GetTemperament(energy, fear, anger, love, dominance, curiosity);
            Console.WriteLine($"energy={energy:F2}, fear={fear:F2}, anger={anger:F2}, love={love:F2}, dominance={dominance:F2}, curiosity={curiosity:F2}, temperament={temperament}");
        }

        Dictionary<string, int> results = new Dictionary<string, int>();
        for (int i = 0; i < 10000; ++i) {
            float energy = 0.5f * random.NextSingle() + 0.5f * random.NextSingle();
            float fear = 0.5f * random.NextSingle() + 0.5f * random.NextSingle();
            float anger = 0.5f * random.NextSingle() + 0.5f * random.NextSingle();
            float love = 0.5f * random.NextSingle() + 0.5f * random.NextSingle();
            float dominance = 0.5f * random.NextSingle() + 0.5f * random.NextSingle();
            float curiosity = 0.5f * random.NextSingle() + 0.5f * random.NextSingle();
            string temperament = GetTemperament(energy, fear, anger, love, dominance, curiosity);
            if (results.ContainsKey(temperament)) {
                results[temperament] += 1;
            }
            else {
                results[temperament] = 1;
            }
        }
        var sorted = results.ToList();
        sorted.Sort((first, second) => second.Value.CompareTo(first.Value));
        for (int i = 0; i < 10; ++i) {
            Console.WriteLine($"{sorted[i].Key} {sorted[i].Value}");
        }
    }

    public static string judge(float val) {
        float high = 0.8f;
        float some = 0.6f;
        float little = 0.4f;
        float low = 0.2f;

        if (val < low) return "low";
        if (val < little) return "midlow";
        if (val < some) return "mid";
        if (val < high) return "midhigh";
        return "high";
    }

    public static string GetTemperament(float energy, float fear, float anger, float love, float dominance, float curiosity) {
        string key = "energy" + judge(energy) + "-fear" + judge(fear) + "-anger" + judge(anger) + "-love" + judge(love) + "-dominance" + judge(dominance) + "-curiosity" + judge(curiosity) + "-";

        if (Regex.IsMatch(key, "energy*-fearhigh-angerhigh-lovehigh-dominance*-curiosity*-*".Replace("*", ".*"))) return "Intense";
        if (Regex.IsMatch(key, "energy*-fearhigh-angerhigh-love*-dominance*-curiosity*high-*".Replace("*", ".*"))) return "Unpredictable";

        float high = 0.8f;
        float some = 0.6f;
        float little = 0.4f;
        float low = 0.2f;

        if (anger > high) {
            if (fear > high) {
                if (love > high) return "intense";
                if (curiosity > some) return "unpredictable";
                return "wild";
            }
            if (fear > some) return "bristling";
            if (love > high) return "passionate";
            if (love > some) return "fierce";
            if (fear < low) return "hostile";
            if (love < little) {
                if (dominance > some) return "cruel";
                return "vicious";
            }
            if (fear < little) return "savage";
            if (energy > some) return "berserk";
            if (energy < little) return "seething";
            if (dominance > high) return "tyrannical";
            if (curiosity > some) return "menacing";
            if (curiosity < little) return "brutish";
            return "aggressive";
        }
        if (fear > high) {
            if (anger > some) return "defensive";
            if (energy > some) return "skittish";
            if (curiosity > some) return "suspicious";
            if (love > some) return "vulnerable";
            if (energy < little) return "gloomy";
            if (anger < low) return "terrified";
            if (anger < little) return "anxious";
            if (love < little) return "paranoid";
            return "fearful";
        }
        if (love > high) {
            if (anger > some) return "protective";
            if (fear < little) {
                if (energy < high) return "buoyant";
                return "trusting";
            }
            if (dominance > some) return "nurturing";
            if (anger < little) return "sweet";
            if (dominance < little) return "loyal";
            if (energy < little) return "cozy";
            if (energy > high) return "exuberant";
            if (energy > some) return "devoted";
            return "loving";
        }
        if (anger > some) {
            if (fear > some) {
                if (energy > some) return "reactive";
                return "untamed";
            }
            if (love > some) return "stern";
            if (energy > high) return "volatile";
            if (energy > some) return "feisty";
            if (energy < little) return "sullen";
            if (energy < little) return "grumpy";
            if (dominance < low) return "resentful";
            if (dominance > high) return "forceful";
            if (dominance > some) return "tough";
            if (fear < little) return "bold";
            if (love < little) return "bitter";
            if (curiosity > some) return "indignant";
            return "angry";
        }
        if (fear < low) {
            if (anger < low) return "mellow";
            if (curiosity > some) return "adventurous";
            if (energy > some) return "daring";
            if (curiosity < little) return "steady";
            if (energy < little) return "serene";
            if (love < little) return "stoic";
            return "brave";
        }
        if (energy > high) {
            if (love > some) return "vivacious";
            if (love < little) return "restless";
            if (dominance > some) return "spirited";
            if (dominance < little) return "enthusiastic";
            return "energetic";
        }
        if (love < low) {
            if (dominance > some) return "independent";
            if (dominance < little) return "withdrawn";
            return "aloof";
        }
        if (energy < low) {
            if (fear > some) return "concerned";
            return "sleepy";
        }
        if (curiosity > high) {
            if (dominance > high) return "sage";
            if (fear > some) return "wily";
            if (love < little) return "shrewd";
            return "clever";
        }
        if (curiosity < low) {
            return "simple";
        }
        if (dominance > high) {
            if (energy > some) return "commanding";
            return "kingly";
        }
        if (anger < low) {
            if (dominance < low) return "meek";
            if (love > some) return "forgiving";
            return "unflappable";
        }
        if (dominance > some) {
            if (love < little) return "restive";
            if (fear < little) return "assertive";
            return "bossy";
        }
        if (dominance < low) {
            return "submissive";
        }
        if (fear < little) {
            if (energy < little) return "relaxed";
            return "calm";
        }
        if (fear > some) {
            if (dominance < little) return "timid";
            if (energy > some) return "alert";
            if (love > some) return "shy";
            if (love < low) return "secretive";
            if (love < little) return "mistrustful";
            if (curiosity > some) return "doubtful";
            return "cautious";
        }
        if (anger < little) {
            if (love > some) {
                if (energy > some) return "joyful";
                if (energy < little) return "gentle";
                if (dominance > some) return "benevolent";
                return "friendly";
            }
            if (dominance < little) return "docile";
            if (energy < little) return "placid";
            return "peaceful";
        }
        if (love > some) {
            if (energy > some) return "merry";
            if (energy < little) return "warm";
            return "affectionate";
        }
        if (dominance < little) {
            return "considerate";
        }
        if (energy > some) {
            if (curiosity > some) return "nosy";
            if (curiosity < little) return "dopey";
            return "driven";
        }
        if (energy < little) {
            return "patient";
        }
        if (curiosity > some) {
            return "curious";
        }
        if (curiosity < little) {
            return "straightforward";
        }

        return "balanced";
    }
}
