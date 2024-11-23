using Main.SupportingComponents;

namespace Main.Day14
{
    internal class Challenge : BaseChallenge
    {
        public override Day Day => Day.Fourteen;

        private readonly Dictionary<string, int> _cycleCash = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _loadCash = new Dictionary<string, int>();

        public override Task<Solution> SolvePart1Async(InputType inputType)
        {
            var input = Input.GetInput(inputType);
            var rocks = MultidimensionalArrayParser.Parse2DArray(input);

            RollRocksNorth(rocks);

            var totalLoad = CalculateTotalLoad(rocks);
            var solution = new Solution($"Total Load: {totalLoad}");
            return Task.FromResult(solution);
        }

        public override Task<Solution> SolvePart2Async(InputType inputType)
        {
            var input = Input.GetInput(inputType);
            var rocks = MultidimensionalArrayParser.Parse2DArray(input);

            var foundRepeat = false;
            var currentIteration = 0;

            while (!foundRepeat)
            {
                currentIteration++;
                PerformCycle(rocks);
                TryCacheLoad(rocks);
                foundRepeat = !TryCacheCycle(rocks, currentIteration);
            }

            var loopStartCacheKey = rocks.GetCacheKey();
            var offsetBeforeLoop = _cycleCash.Keys.ToList().IndexOf(loopStartCacheKey);
            var loopSize = currentIteration - _cycleCash[loopStartCacheKey];

            var totalCyclesToRun = 1000000000;
            var finalCyclePosition = ((totalCyclesToRun - offsetBeforeLoop) % loopSize) - 1;

            var totalLoad2 = CalculateTotalLoad(rocks);
            var reducedLoads = _loadCash.Skip(offsetBeforeLoop)
                                        .Select(x => x.Value)
                                        .ToArray();

            var totalLoad = reducedLoads[finalCyclePosition];

            var solution = new Solution($"Total Load: {totalLoad}");
            return Task.FromResult(solution);
        }

        private bool TryCacheLoad(char[,] rocks)
        {
            var cacheKey = rocks.GetCacheKey();
            if (_loadCash.ContainsKey(cacheKey))
            {
                return false;
            }
            else
            {
                var load = CalculateTotalLoad(rocks);
                _loadCash.Add(cacheKey, load);
                return true;
            }
        }

        private bool TryCacheCycle(char[,] rocks, int iteration)
        {
            var cacheKey = rocks.GetCacheKey();
            if (_cycleCash.ContainsKey(cacheKey))
            {
                return false;
            }
            else
            {
                _cycleCash.Add(cacheKey, iteration);
                return true;
            }
        }

        //TODO: Build a cache of cycles run and index

        private void PerformCycle(char[,] rocks)
        {
            RollRocksNorth(rocks);
            RollRocksWest(rocks);
            RollRocksSouth(rocks);
            RollRocksEast(rocks);
        }

        private void RollRocksNorth(char[,] rocks)
        {
            for (var column = 0; column < rocks.NumberOfColumns(); column++)
            {
                var toBeShifted = 0;
                for (var row = 0; row < rocks.NumberOfRows(); row++)
                {
                    var currentSymbol = rocks[row, column];
                    if (currentSymbol.IsMovableRock() && toBeShifted > 0)
                    {
                        rocks[row - toBeShifted, column] = Symbols.MovableRock;
                        rocks[row, column] = Symbols.Empty;
                    }
                    else if (currentSymbol.IsStationaryRock())
                    {
                        toBeShifted = 0;
                    }
                    else if (currentSymbol.IsEmpty())
                    {
                        toBeShifted++;
                    }
                }
            }
        }

        private void RollRocksSouth(char[,] rocks)
        {
            for (var column = 0; column < rocks.NumberOfColumns(); column++)
            {
                var toBeShifted = 0;
                for (var row = rocks.NumberOfRows() - 1; row >= 0; row--)
                {
                    var currentSymbol = rocks[row, column];
                    if (currentSymbol.IsMovableRock() && toBeShifted > 0)
                    {
                        rocks[row + toBeShifted, column] = Symbols.MovableRock;
                        rocks[row, column] = Symbols.Empty;
                    }
                    else if (currentSymbol.IsStationaryRock())
                    {
                        toBeShifted = 0;
                    }
                    else if (currentSymbol.IsEmpty())
                    {
                        toBeShifted++;
                    }
                }
            }
        }

        private void RollRocksWest(char[,] rocks)
        {
            for (var row = 0; row < rocks.NumberOfRows(); row++)
            {
                var toBeShifted = 0;
                for (var column = 0; column < rocks.NumberOfColumns(); column++)
                {
                    var currentSymbol = rocks[row, column];
                    if (currentSymbol.IsMovableRock() && toBeShifted > 0)
                    {
                        rocks[row, column - toBeShifted] = Symbols.MovableRock;
                        rocks[row, column] = Symbols.Empty;
                    }
                    else if (currentSymbol.IsStationaryRock())
                    {
                        toBeShifted = 0;
                    }
                    else if (currentSymbol.IsEmpty())
                    {
                        toBeShifted++;
                    }
                }
            }
        }

        private void RollRocksEast(char[,] rocks)
        {
            for (var row = 0; row < rocks.NumberOfRows(); row++)
            {
                var toBeShifted = 0;
                for (var column = rocks.NumberOfColumns() - 1; column >= 0; column--)
                {
                    var currentSymbol = rocks[row, column];
                    if (currentSymbol.IsMovableRock() && toBeShifted > 0)
                    {
                        rocks[row, column + toBeShifted] = Symbols.MovableRock;
                        rocks[row, column] = Symbols.Empty;
                    }
                    else if (currentSymbol.IsStationaryRock())
                    {
                        toBeShifted = 0;
                    }
                    else if (currentSymbol.IsEmpty())
                    {
                        toBeShifted++;
                    }
                }
            }
        }

        private int CalculateTotalLoad(char[,] rocks)
        {
            var totalLoad = 0;
            for (var row = 0; row < rocks.NumberOfRows(); row++)
            {
                var totalRocksInRow = 0;
                for (var column = 0; column < rocks.NumberOfColumns(); column++)
                {
                    var currentSymbol = rocks[row, column];
                    if (currentSymbol.IsMovableRock())
                    {
                        totalRocksInRow++;
                    }
                }
                var rowWeight = rocks.NumberOfRows() - row;
                totalLoad += totalRocksInRow * rowWeight;
            }
            return totalLoad;
        }

        protected override ChallengeInput Input { get; } = new ChallengeInput
        {
            TestInput =
@"O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#....",

            PuzzleInput =
@"...O.....#O#....O###...#..OOO.O#....#.O.O.....O...OO##.#..O.O...........O........#...#.#O........##.
#OO..O.##..#..........#.....O...##..O#.#O#O..##.....#.......O....##.#..#OO.O#O#....O..OO.O.....OO#.#
.###..O.#...O.#......O.#.......#.........#OO##.....#...#...O....#..O.O.O.#.O..O...##....O.O#.O....O.
O.O......OO.O#..#....O...#..##...O.#..##O.#...O.O....OO#......O...#...OO#O......O.....#O.O.#.#O.#O..
.#....#.O#...O..O#..O.##..##.O..O...#...#....O#.#..OOO##..O#.#...#.#O.O#...#...O.O...#...O#O.###....
.#.....O..#...#..#.##.OO.#O.#........................#...#OO.O.OO.##....O..#OOO#........O.....OO.#OO
O....#.O.#O#O..#...#O.O.OO#O.OO......O.O.OOO...O..O.#....#.OO.......OO..#.....O.#..#.OO#.....#.#....
.#..##.O....##O#.O...O...#.O..#.O.O.......#..........O#.O#.O..O..O#.#O...#O.#......O.......#.####..O
....#......#...##.OOO##O.#.O##.....O.....O.........#...#.#.#.#..#.OO#O.#.###...O...O#O.O.#O..O#O....
..O....##O#.....#O..#O..O...O..O....O..O..O..O...OO#...#O....#.#...#O.#..O.O#O..O........O#O......##
..O....#.##OO.O..O##..#..OO#.....#O.#.#..#.OO......#.#..O.....O..#.O.O..O.O..........O...#.O#O...O.#
O.#.#.O.#OO...OO.......OOO..OO......#..O.OO.#............OO...#O....O.OO#.O....O........###..#...O.#
..#.O#...........O.#..O..OO.O#..#OOO.O#...OO..OO....O...#.O#.O.....#.OO.O.O...O.....#.##O.O.....#...
#......OO.O#..O.......O...#.O.O#......#.###.#.#.O.......O.#.#..O..O...O#....OO..OOO.O....##...#.#.##
.#..O...O.O..OO.....O#...#..O....O..#.....OOO...O##.........O..OOOO#........#OOO#.....O#.O.......#O#
#..OO#.O#O...O......#.....#O.#........#.....O.OO..#O#..................#OO.#..O..#...O.....O..##.#..
.#.#O..#..OO.O.....OOO......O.O#..O...#....##...###O..OOO.....O....O.....#..#.#....OO..##.......O...
...O.OO....#.##O#..O..O.#...O.............O#...O#.##.O....OO.......OO..#OO...O#O#......O....#..OOOO.
.O..OO..#..O...#.O#.....##..#.........O.##.....O...O.....#.#.#O.....#OO......#..OO...O......#O.OO.#.
.O#OO.......O##...#............OO##....OO.O#.#O.O...#..#...#.OO.O..O.O.O#O..O...#........#.#O...O...
.#.#.O#OOO.##OO...O..O.O##.O.O#....#O....####.O.O..#..O..O.O.#....O..#....O..#......O.O.#OO..#.O.#..
...OOO...O.....O....O......OO#...#....OO.#.O#O....O#.O.O...#.O....O.#...#.....O#..#.O#.#.O.....#OO#.
OO..O....O.O#.O...#....O.#........#.#....#....O...OOO.#.....OO..#.........O..##.........OO.........O
#...##.....OO#...#..O...#.O#.#.#O....OO.O..O#.##.O.....O.O#.#OO..#...#OOO..#....OO..#O..#O#...#.#OO.
OO.#.#....#..O.....#..OO......O......OO.OO.#..OO.#..#..#.........O#......##.##O...O#..OO.OO...#O....
....O..#.O#.O#..##O.O...#......O.......#....#..#..##...OO.O..O#.O.....OO##.#..O.....O#.O#....O...#O.
.#.O..#.OOOO#...#O......O.#..##...#.O#...O.##OO.O.O.O...#..O#.#O...O..#O.O..OO.O.OOOO#O..O...O#..#..
....O#..#..###..O#O....OO...#.O.O..#....#O...#O.....#OO...O...OO......##.OO.....O.O..#.....O.O.....#
...###.O....O..#..#.#OO.#.O..O.#OO.#...#....O.....#O.....#.#...OO.......O.#...O##O.......O.##.O...O.
.......#....#O#O.#...OO..#OOO.....OOO.....##....###...O...O...#OO.O.##..#OO...#..#.....O.....O.O#O.#
#O.O..O.#.O....#OO.OO....OO.....OO.OOO..##O.#.....O#OO....#O...O#..O#..#.OO.O......O...O......O#..O#
O.......#..O............#O.#O.OO.##....OO.#OO..##O...#OO....#...#..#....O#..O......O..O.#......#.O#.
O.....#O#O......#..#...OO#.....#.#.....OO#..OO#.O..#.O....#..O..#.....O...O...#......O.....OO....#.O
...#...#......O....#..O..O#O..#.##O.#O...O....O..#.O###.#.#..........O...#.....O........#..#..#...O.
.#....#.....O.#.OOO........O......#...#.O..#..O.O...O#.......O.O.#..#..O.....O......O#OOO...O#......
.O...O#....O.....O.O...#O.#....#.##......O.O#OO#.....O..O..OOO..#...#....##.....#O.O.#.O#..........#
#....#..#..O..O#.O#O.........O..O.#.O.OO..#O....O.OO.#.OO......#.#..O...O.#.#O.#.#..O.O#..O.#...#O.#
OO...#...O...O.#O.O.#....OO##.#.#...O#....O.#..O...O...#......#.O.O...O.O.OO.#..O..O.#O..O#.#..#...#
.....O..#...O#.O.#.O.#O##O.##.OO#..O.O#O#...O#........#........OO.OOO.O.O.........#..O...........O..
..O...O.O..O..O......O#..##.O..#.#.#......O...#...#O........###..##......O........#.#...OO..........
.....OO#....O.#.O.O..OO#...O.....O..OO.#O..........##..##..#.O..#O.O#OO#O#.O.O#.OO....#O..O....O....
#.O.#.#.O........#..OOOO..O.O#.#.#.....O#.O#..##.....#..O..O#O#OO...#..O#O..OO.O........#......#.#..
.##.....O.#.......O......O##O#.........OO#..###..#............O....O.#.OO......#.....OO#..#..O...O#.
O.O..#.O.O.....O...O....O..OO....OO#...#.OO........O.O...O.....#.#....#O..#.#......#.#....#....#.O#O
..O#..#.#.#....O..#..O.O.O...#..##...O..O##.......O...O...O..O.O#O......O..O.O.OO.##OO.OO........O..
..##.....#.O......#.O....O.O..O#..O....OO#O...O.#.O.OO#.OOO#.#.OO..#O.O#..##.O.#...#O.#.....#.O#.O..
#O..#..O..#.O..O...O....#OOOO..O.O#.##O.O#O...#OO...O.#OOO.#.#......#...#.O#.O..##.#..#..O.#.......#
..O.#.#OO...O...O#...O...O#...O.........#...#.O..#...##..O..O.....#...OOO.##..........O..#O#O.#.O...
...#..O....#...#.O.......O...##...O..O....O.......O.....O..#..OO.....OO..#....OO#O.O#..#OO..O....#O.
.....#O.O#.#...#.O.....##..###..O......O.O#.O#.....O......#....O.O.......O#...##.O....O#O...#..##..#
....#.#..#..#..##O.##.#.O.....O..#OO..O#.O.#OO.#O#..OO..OO#.#.....O..#.OO###.##.......O...#......#..
..O..#O.....O......#...O.#...O..O.#....#.#.O...O.#.OOO...O.O.##.....O..##....#O...##.O.O#.##O#...OO.
..#O.#...O#.#............#.O..O.#.O.#O#.#...##.O#..O##..#..#OO.O............O.#.OOOO......O.O.....##
..O...OO..#......OO.#OOOO.O.#.#..#..O..OO.O.O.....#.##O......O...O.#....O....#.OOO.O..#..#.##.O.#O..
O.#.#.O...#..##O.OO......O.O..O.O.#O...........O...OO#O#...OO..#..O..#O...##.O......OOO.......#O..#.
......OOO....#..##O#....O.O.#.O...O#OO..#.O.......O.O..OOO.#...O.#OOO.O.OOOO#...#.....OO..O.O.OOO#..
..OO.....#.O#......#.O..##..O..OO.O#..O#....O...O.....#..#......O.....O#.O.O#OO.OO.O.OO..#.OO.O.#O#.
.##........O.OO..#...#..#.#O.....#O..#O..O.O..#.O.OO...O.#O#...#.OO.O...#.O#....OO#...##..#O........
O.#..O.#O.#..#..O#.......O.#.OO..OOO#..##.##.....#..#.OOO..OO#...#O.O#.O..#........##...O....O.O.OOO
.O..#..#O...OO......#...OO#.#........OO..O.#O.........O..#O.#O...##.#O..#...O...O........##..O##..O.
.O.#.....#O....O....OO....#....O..#..O......#..OOO.O.#O....OOO.#....OO.###.#....O.O..#..#..#..#.....
.#..O..#.##....#..#O.O..O.OO#..#....OO...O.O...........O#....O....O......#...O#O.....O.O.....OO.O...
##......O#...................O..O.O....#.........#....O###.#...#.OO.O..#.OO.O....OO.#.O...O...#...##
##...................O...........O.O##....##O...OOOO.O..#.O....O.#...#..OO......O.O.#O.#....#.#.O..#
...#O..O....#......#.#.#.....#.O#..###.#.#...O#OO.O..O.O...#OO#.......#.O...O.#...#...O..O..#O...#..
O..O....#..O...#.O.O#......O.......#.OO...O...OO....O#O.OOO...O#....O...O..OO.O#.......O.O..O#......
#...OOO.O..........#..O.#...#..O.#O.O#O.#....#.O.##....#..OO##OO.O.....OO.....OO.#.O...O.......#..O.
.O...O.O...O..O#.....##..O#.O#....OO..###O#..O#..#.#.#..O.O#..#.....##.O.#O#.O.#O.....#.O...OO...#O.
#O#.O...O#...#...O....#.O.##O#...O#.#.#O#.....O.O.O...OO#OO#...O............###....O..#OO#....O..#.O
..#..O.O#..#......#..#........O..O....#...O.O........#.#....#..#.....OOO#.#....OOOO.#O.#..##O...#..O
......O..O..O.#..#...O...#..#..O...OOO.O.OO.O...#.OOO.#...O.#...O...OO#.##..#.##.O...O.OO...OO....##
..#....O..#.#..##O.....O..O...#OOO#..O....#.......##.O..OO#.OO#O..O....O.##O..O....O##O#OO#O...#..O.
....#.#OO.#.#O...#OOO....OOOO.#..O....O...O.........#O#...###...#..OO..O.....O.O.#O..#....###O..##..
#..O.#.O.O....OO.#..#.OOO....O.#....###O.#.##.O....#O......O..O..OOO.#..O......OOO..O.....OO.O.#.#.O
#.O.#....O..#O.##.O......OO....O.....O#O......#.O...#.##..O#..O.OO#..##..O...#.#O.O..O.O#OO.#..##O..
....OO.O....#....O#...O..#.O..O.#.O.#.#....#.O....O#OO..O.O.O....#...#...O#O...O...O....#....O.O.O..
.....#.OOOO.....#...#OO...O.#....###...O.O...#..#..OO..O.O#..O#OO.O.#.O#O.OO....#.#O#OO...O.OOO...#.
.O..#.#.###OOO....#..O....OO.#..O..OO..OO#O#O#..#...O...#...##O..#.......#O...##O..#...##......O.O.#
.O.###..O....O..OO#O....O#....O...O...O..#....O...#.......O.......#.O.O.#......#.OO#..O.OO..O.O.#..O
....#O...#.O.#.O.........#...#....O...#.O.#...O..#.#..O............O...O.#O..O..##.O.O...OO.#O.O...#
#.#.....OO#...O#OO.#..#..........#........O.O#.O.....O...#..#.OO#O.............O.O.##...##OO..#....#
O.OO...O#.OOO#O..O.......O##O#O###.##..#..O...#.#.....O...#..O.O..O..O#.O##.OOOO#...#.....##........
#............#O....#....O#.....O.OO.O...#....O.#O.#...##.....O..OO#...##.O...#.O....#....O....OO#O..
#..OO...O.....O.#..O#OO........O.......O....O#O###.#....O#O..##.###O..#O.O....O.O....O.#O..#...O##.#
..OO..#.......#...OO.#OOO#..#OO....#...OO..#O...O....O...#...O#......O.O...O##...OOO.#........O.O..O
O#.OO..O.O..#...O#O..#OO..#.#.....O.##.O.O.O.#..O.O...#O...#.......#..OOO##OO.#..O....OO....#OO.#.O.
.......#...#.#O.OOO#..#......OO.#O...##..O#.O...#O.....O..#....OO.O...OO..OO.OO..O...#.O#..OOOO#.O##
.#.#.O..O...O..#.....#..O........#.O.#..O...#.O..O..#.O.#..#..O.....O.O#...O....O....###..O.#....#..
..O..OOOO#...#...#...O....#O##.O.#..#...#O...O....O.....#..O..O...##O..O.##O.....O...##....#..#.....
...#.#.....#.......O...O#....#..O........O......#...#.#..##.O..O....#.O.....O.O#.##..O..#..#...#..#O
..OO......O..#...#.......#.O....O.##.#O.#O..O.O.OOO..O.##.#O..#..O#.O..#O..O.#O.....#..#..O.O.......
##O#O.....O......OOO..........O#OO...OO...#..#......#.O.....O#....O..OOO......#...O..O..#O......#...
..O....O...O...##O.O#O.#.#...#.O#O...#.#......##..O#O.#.OO......##O..O#.#..O......##...#O...#..OOOO#
....#O..##.....#........O...O..###..#...O...#O.O..O.O...#..#O......O#....OO#....O.#...#..OO..#..O.O.
OOO.#O.#.#.#..O.#.#O.....#.O##............#........O..OO#O##..O...O..#..O......O....OOO..O..OO......
..O.#......O.O..#O..O.#.O.O##...OOOO..O...#OO#.....O...OO......O.O...#.O.....OO.O.O..#.O..O.O.O...##
..OO.....#..#.....OO.#.O..#.........O..#..O.......O.##..OOO.......##.....O..#.#.O....#.O#....#..##..
..#..OO.OO......O.O....#...O.#..#...O..O##...O.OOO....O.O#..O.#..#......#.....O.O.O#.....##.....#O..
OO...#.##....OO.O...O..O...O..#.O.#.#.#.O.##.#...#.........O#.OO...O...#O...#....OO....#......#O.O..
.O...#.....O.OO.O##......O...#..#..O.#.....O.#O.O.##O.O.OO.OOOO...#O.#.O....O.#..O........#..O..OOO."
        };
    }
}
