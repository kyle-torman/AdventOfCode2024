namespace Main.Day2
{
    public class Report
    {
        public Report(int[] levels)
        {
            _levels = levels;
            _areLevelsDecreasing = _levels.First() > _levels.Last();
        }

        private readonly int[] _levels;
        private bool _areLevelsDecreasing;

        public bool IsSafeWithoutDampener()
        {
            return IsSafeWithDampener(_levels, true);
        }

        public bool IsSafeWithDampener()
        {
            return IsSafeWithDampener(_levels, false);
        }

        private bool IsSafeWithDampener(int[] levels, bool isDampenerActivated)
        {
            var isDecreasing = levels.First() > levels.Last();
            for (int i = 0; i < levels.Length - 1; i++)
            {
                var currentLevel = levels[i];
                var nextLevel = levels[i + 1];
                var levelViolatesRule = DoesLevelViolateRule(currentLevel, nextLevel, isDecreasing);
                if (levelViolatesRule && isDampenerActivated)
                {
                    return false;
                }
                else if (levelViolatesRule)
                {
                    var levelsWithoutCurrent = levels.RemoveAt(i);
                    var levelsWithoutNext = levels.RemoveAt(i + 1);
                    return IsSafeWithDampener(levelsWithoutCurrent, true) || IsSafeWithDampener(levelsWithoutNext, true);
                }
            }

            return true;
        }

        private bool DoesLevelViolateRule(int level, int nextlevel, bool isDecreasing)
        {
            var difference = Math.Abs(level - nextlevel);
            var followsPattern = isDecreasing ? level > nextlevel : level < nextlevel;
            return difference < 1 || difference > 3 || !followsPattern;
        }
    }
}
