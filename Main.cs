using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class Temperament {
    public float energy;
    public float fear;
    public float anger;
    public float social;
    public float dominance;
    public float decency;
    public float curiosity;
    public float intelligence;
    public float persistence;
    public float hunger;
    public float vocal;
    public string? male;
    public string? female;

    public static Temperament Generate(Random random) {
        return new Temperament() {
            energy = 0.5f * random.NextSingle() + 0.5f * random.NextSingle() - 0.5f * random.NextSingle() - 0.5f * random.NextSingle(),
            fear = 0.5f * random.NextSingle() + 0.5f * random.NextSingle() - 0.5f * random.NextSingle() - 0.5f * random.NextSingle(),
            anger = 0.5f * random.NextSingle() + 0.5f * random.NextSingle() - 0.5f * random.NextSingle() - 0.5f * random.NextSingle(),
            social = 0.5f * random.NextSingle() + 0.5f * random.NextSingle() - 0.5f * random.NextSingle() - 0.5f * random.NextSingle(),
            dominance = 0.5f * random.NextSingle() + 0.5f * random.NextSingle() - 0.5f * random.NextSingle() - 0.5f * random.NextSingle(),
            decency = 0.5f * random.NextSingle() + 0.5f * random.NextSingle() - 0.5f * random.NextSingle() - 0.5f * random.NextSingle(),
            curiosity = 0.5f * random.NextSingle() + 0.5f * random.NextSingle() - 0.5f * random.NextSingle() - 0.5f * random.NextSingle(),
            intelligence = 0.5f * random.NextSingle() + 0.5f * random.NextSingle() - 0.5f * random.NextSingle() - 0.5f * random.NextSingle(),
            persistence = 0.5f * random.NextSingle() + 0.5f * random.NextSingle() - 0.5f * random.NextSingle() - 0.5f * random.NextSingle(),
            hunger = 0.5f * random.NextSingle() + 0.5f * random.NextSingle() - 0.5f * random.NextSingle() - 0.5f * random.NextSingle(),
            vocal = 0.5f * random.NextSingle() + 0.5f * random.NextSingle() - 0.5f * random.NextSingle() - 0.5f * random.NextSingle(),
        };
    }

    public static Temperament GenerateYoung(Random random) {
        Temperament t = Generate(random);
        t.energy += 0.4f;
        t.dominance -= 0.4f;
        t.curiosity += 0.4f;
        t.persistence -= 0.4f;
        return t;
    }

    public static Temperament GenerateWolf(Random random) {
        Temperament t = Generate(random);
        t.fear = t.fear * 1.25f + 0.25f;
        t.anger = t.anger * 1.3f + 0.3f;
        t.energy = t.energy * 1.2f + 0.2f;
        t.intelligence = t.intelligence * 1.3f + 0.3f;
        t.dominance = t.dominance * 1.3f + 0.3f;
        t.curiosity = t.curiosity * 1.3f + 0.3f;
        t.vocal = t.vocal * 1.1f - 0.1f;
        return t;
    }

    public Temperament Clone() {
        return (Temperament)MemberwiseClone();
    }

    public void Difference(Temperament other, float multiplier = 1) {
        energy -= other.energy * multiplier;
        fear -= other.fear * multiplier;
        anger -= other.anger * multiplier;
        social -= other.social * multiplier;
        dominance -= other.dominance * multiplier;
        decency -= other.decency * multiplier;
        curiosity -= other.curiosity * multiplier;
        intelligence -= other.intelligence * multiplier;
        persistence -= other.persistence * multiplier;
        hunger -= other.hunger * multiplier;
        vocal -= other.vocal * multiplier;
    }

    public void Age(Random random) {
        energy += 0.2f * random.NextSingle() - 0.4f * random.NextSingle();
        fear += 0.2f * random.NextSingle() - 0.2f * random.NextSingle();
        anger += 0.2f * random.NextSingle() - 0.2f * random.NextSingle();
        social += 0.2f * random.NextSingle() - 0.2f * random.NextSingle();
        dominance += 0.4f * random.NextSingle() - 0.2f * random.NextSingle();
        decency += 0.2f * random.NextSingle() - 0.2f * random.NextSingle();
        curiosity += 0.2f * random.NextSingle() - 0.4f * random.NextSingle();
        intelligence += 0.2f * random.NextSingle() - 0.2f * random.NextSingle();
        persistence += 0.4f * random.NextSingle() - 0.2f * random.NextSingle();
        hunger += 0.2f * random.NextSingle() - 0.2f * random.NextSingle();
        vocal += 0.2f * random.NextSingle() - 0.2f * random.NextSingle();
    }

    public float DistanceSquaredRaw(Temperament other) {
        return (energy - other.energy) * (energy - other.energy)
            + (fear - other.fear) * (fear - other.fear)
            + (anger - other.anger) * (anger - other.anger)
            + (social - other.social) * (social - other.social)
            + (dominance - other.dominance) * (dominance - other.dominance)
            + (decency - other.decency) * (decency - other.decency)
            + (curiosity - other.curiosity) * (curiosity - other.curiosity)
            + (intelligence - other.intelligence) * (intelligence - other.intelligence)
            + (persistence - other.persistence) * (persistence - other.persistence)
            + (hunger - other.hunger) * (hunger - other.hunger)
            + (vocal - other.vocal) * (vocal - other.vocal);
    }

    public float DistanceSquaredWeighted(Temperament other) {
        return (energy - other.energy) * (energy - other.energy) * 1.2f
            + (fear - other.fear) * (fear - other.fear) * 1.5f
            + (anger - other.anger) * (anger - other.anger) * 1.5f
            + (social - other.social) * (social - other.social) * 1.5f
            + (dominance - other.dominance) * (dominance - other.dominance) * 1.2f
            + (decency - other.decency) * (decency - other.decency) * 1.5f
            + (curiosity - other.curiosity) * (curiosity - other.curiosity)
            + (intelligence - other.intelligence) * (intelligence - other.intelligence)
            + (persistence - other.persistence) * (persistence - other.persistence)
            + (hunger - other.hunger) * (hunger - other.hunger)
            + (vocal - other.vocal) * (vocal - other.vocal);
    }

    public override string ToString() {
        return $"Temperament <energy={energy:F2}, fear={fear:F2}, anger={anger:F2}, social={social:F2}, dominance={dominance:F2}, decency={decency:F2}, curiosity={curiosity:F2}, intelligence={intelligence:F2}, persistence={persistence:F2}, hunger={hunger:F2}, vocal={vocal:F2}>";
    }
}

public class PersonalityTester {
    public static void Main(string[] args) {
        JObject json = JObject.Parse(File.ReadAllText("en.json"));
        Dictionary<string, Temperament>? dict = json.ToObject<Dictionary<string, Temperament>?>();
        ArgumentNullException.ThrowIfNull(dict);
        Console.WriteLine("Loaded " + dict.Count + " personalities");
        Random random = new Random();

        float worst_first = float.MinValue;
        float worst_second = float.MinValue;
        Temperament tfirst = null!;
        Temperament tsecond = null!;

        Console.WriteLine("Samples of wolves:");
        for (int i = 0; i < 25; ++i) {
            Temperament t = Temperament.GenerateWolf(random);
            string personality = GetPersonalityPair(dict, t);
            Console.WriteLine(personality);
        }

        Console.WriteLine("Samples of change over time:");
        for (int i = 0; i < 25; ++i) {
            Temperament young = Temperament.GenerateYoung(random);
            string youngPersonality = GetPersonalityPair(dict, young);

            Temperament adult = young.Clone();
            adult.Age(random);
            string adultPersonality = GetPersonalityPair(dict, adult);

            Temperament old = adult.Clone();
            old.Age(random);
            string oldPersonality = GetPersonalityPair(dict, old);

            Console.WriteLine(youngPersonality + " -> " + adultPersonality + " -> " + oldPersonality);
        }

        Console.WriteLine("Ordered by common to rare:");
        Dictionary<string, int> results = new Dictionary<string, int>();
        foreach (KeyValuePair<string, Temperament> entry in dict) {
            results[entry.Key] = 0;
        }

        for (int i = 0; i < 10000; ++i) {
            Temperament temperament = Temperament.Generate(random);
            string personality = GetPersonality(dict, temperament, true);
            results[personality] += 1;

            float dist = temperament.DistanceSquaredWeighted(dict[personality]);
            Temperament remainder = temperament.Clone();
            remainder.Difference(dict[personality], 0.8f);
            string secondary = GetPersonality(dict, remainder, false);
            float d2 = remainder.DistanceSquaredRaw(dict[secondary]);
            // Console.WriteLine(personality + ", " + secondary);
            if (dist > worst_first) {
                worst_first = dist;
                tfirst = temperament;
            }
            if (d2 > worst_second) {
                worst_second = d2;
                tsecond = remainder;
            }
        }

        var sorted = results.ToList();
        sorted.Sort((first, second) => second.Value.CompareTo(first.Value));
        for (int i = 0; i < 20; ++i) {
            Console.WriteLine($"{sorted[i].Key} {sorted[i].Value}");
        }
        Console.WriteLine("...");
        var backwards = results.ToList();
        backwards.Sort((first, second) => first.Value.CompareTo(second.Value));
        for (int i = 19; i >= 0; --i) {
            Console.WriteLine($"{backwards[i].Key} {backwards[i].Value}");
        }

        Console.WriteLine($"Worst personality match:\n{GetPersonalityPair(dict, tfirst)} ({worst_first:F2}) {tfirst}"); // $"Worst secondary: {GetPersonality(dict, tsecond)} ({worst_second:F2}) {tsecond}"

        FindClosest(dict);
    }

    public static void FindClosest(Dictionary<string, Temperament> mapping) {
        var distances = new List<(string, string, float)>();

        int i = 0;
        foreach (KeyValuePair<string, Temperament> test in mapping) {
            int j = 0;
            foreach (KeyValuePair<string, Temperament> entry in mapping) {
                if (j <= i) {
                    ++j;
                    continue;
                }

                float dsq = test.Value.DistanceSquaredRaw(entry.Value);
                if (dsq < 0.1) {
                    distances.Add((test.Key, entry.Key, dsq));
                }
                ++j;
            }
            ++i;
        }
        Console.WriteLine("Close personalities:");
        foreach (var entry in distances) {
            Console.WriteLine($"{entry.Item1}, {entry.Item2}: {entry.Item3:F2}");
        }
    }

    public static string GetPersonality(Dictionary<string, Temperament> mapping, Temperament temperament, bool weighted) {
        string? best = null;
        float distanceSquared = float.MaxValue;

        foreach (KeyValuePair<string, Temperament> entry in mapping) {
            float d = weighted ? temperament.DistanceSquaredWeighted(entry.Value) : temperament.DistanceSquaredRaw(entry.Value);
            if (d < distanceSquared) {
                distanceSquared = d;
                best = entry.Key;
            }
        }

        ArgumentNullException.ThrowIfNull(best);
        return best;
    }

    public static string GetPersonalityPair(Dictionary<string, Temperament> mapping, Temperament temperament) {
        string personality = GetPersonality(mapping, temperament, true);
        Temperament remainder = temperament.Clone();
        remainder.Difference(mapping[personality], 0.8f);
        string secondary = GetPersonality(mapping, remainder, false);
        if (secondary == personality) {
            return personality;
        }
        return personality + ", " + secondary.ToLower();
    }
}
