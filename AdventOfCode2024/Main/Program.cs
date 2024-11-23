using Main.SupportingComponents;
using System.Reflection;

namespace Main
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var day = Day.Fourteen;
            var inputType = InputType.Test;
            var challengePart = ChallengePart.Two;
            var challenge = Challenges.First(c => c.Day == day);
            await challenge.DisplaySolution(inputType, challengePart);
        }

        private static IEnumerable<IChallenge> Challenges =>
            from t in Assembly.GetExecutingAssembly().GetTypes()
            where t.GetInterfaces().Contains(typeof(IChallenge))
               && t.GetConstructor(Type.EmptyTypes) != null
            select Activator.CreateInstance(t) as IChallenge;
    }
}
