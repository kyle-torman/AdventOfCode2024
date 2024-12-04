using Main.SupportingComponents;

namespace Main.Day4
{
    public static class Extensions
    {
        public static int CountX_MasWordsFromPosition(this char[,] wordSearch, int row, int column)
        {
            var wordFound = (wordSearch.MasFoundDiagnolForwardUp(row, column) || wordSearch.MasFoundDiagnolBackwardDown(row, column)) &&
                             (wordSearch.MasFoundDiagnolForwardDown(row, column) || wordSearch.MasFoundDiagnolBackwardUp(row, column));

            return wordFound ? 1 : 0;
        }

        public static bool MasFoundDiagnolForwardUp(this char[,] wordSearch, int row, int column)
        {
            if (row - 1 < 0 || column - 1 < 0 || row + 1 >= wordSearch.NumberOfRows() || column + 1 >= wordSearch.NumberOfColumns())
            {
                return false;
            }

            return wordSearch[row + 1, column - 1] is WordsearchLetters.M &&
                   wordSearch[row, column] is WordsearchLetters.A &&
                   wordSearch[row - 1, column + 1] is WordsearchLetters.S;
        }

        public static bool MasFoundDiagnolBackwardDown(this char[,] wordSearch, int row, int column)
        {
            if (row - 1 < 0 || column - 1 < 0 || row + 1 >= wordSearch.NumberOfRows() || column + 1 >= wordSearch.NumberOfColumns())
            {
                return false;
            }

            return wordSearch[row - 1, column + 1] is WordsearchLetters.M &&
                   wordSearch[row, column] is WordsearchLetters.A &&
                   wordSearch[row + 1, column - 1] is WordsearchLetters.S;
        }

        public static bool MasFoundDiagnolForwardDown(this char[,] wordSearch, int row, int column)
        {
            if (row - 1 < 0 || column - 1 < 0 || row + 1 >= wordSearch.NumberOfRows() || column + 1 >= wordSearch.NumberOfColumns())
            {
                return false;
            }

            return wordSearch[row - 1, column - 1] is WordsearchLetters.M &&
                   wordSearch[row, column] is WordsearchLetters.A &&
                   wordSearch[row + 1, column + 1] is WordsearchLetters.S;
        }

        public static bool MasFoundDiagnolBackwardUp(this char[,] wordSearch, int row, int column)
        {
            if (row - 1 < 0 || column - 1 < 0 || row + 1 >= wordSearch.NumberOfRows() || column + 1 >= wordSearch.NumberOfColumns())
            {
                return false;
            }

            return wordSearch[row + 1, column + 1] is WordsearchLetters.M &&
                   wordSearch[row, column] is WordsearchLetters.A &&
                   wordSearch[row - 1, column - 1] is WordsearchLetters.S;
        }

        public static int CountXmasWordsFromPosition(this char[,] wordSearch, int row, int column)
        {
            var currentCharacter = wordSearch[row, column];

            return wordSearch.CountWordForward(row, column) +
                   wordSearch.CountWordBackward(row, column) +
                   wordSearch.CountWordDown(row, column) +
                   wordSearch.CountWordUp(row, column) +
                   wordSearch.CountWordDiagnolForwardUp(row, column) +
                   wordSearch.CountWordDiagnolForwardDown(row, column) +
                   wordSearch.CountWordDiagnolBackwardDown(row, column) +
                   wordSearch.CountWordDiagnolBackwardUp(row, column);
        }

        private static int CountWordForward(this char[,] wordSearch, int row, int column)
        {
            if (column + 3 >= wordSearch.NumberOfColumns())
            {
                return 0;
            }

            var wordFound = wordSearch[row, column] is WordsearchLetters.X &&
                            wordSearch[row, column + 1] is WordsearchLetters.M &&
                            wordSearch[row, column + 2] is WordsearchLetters.A &&
                            wordSearch[row, column + 3] is WordsearchLetters.S;

            return wordFound ? 1 : 0;
        }

        private static int CountWordBackward(this char[,] wordSearch, int row, int column)
        {
            if (column - 3 < 0)
            {
                return 0;
            }

            var wordFound = wordSearch[row, column] is WordsearchLetters.X &&
                            wordSearch[row, column - 1] is WordsearchLetters.M &&
                            wordSearch[row, column - 2] is WordsearchLetters.A &&
                            wordSearch[row, column - 3] is WordsearchLetters.S;

            return wordFound ? 1 : 0;
        }

        private static int CountWordDown(this char[,] wordSearch, int row, int column)
        {
            if (row + 3 >= wordSearch.NumberOfRows())
            {
                return 0;
            }

            var wordFound = wordSearch[row, column] is WordsearchLetters.X &&
                            wordSearch[row + 1, column] is WordsearchLetters.M &&
                            wordSearch[row + 2, column] is WordsearchLetters.A &&
                            wordSearch[row + 3, column] is WordsearchLetters.S;

            return wordFound ? 1 : 0;
        }

        private static int CountWordUp(this char[,] wordSearch, int row, int column)
        {
            if (row - 3 < 0)
            {
                return 0;
            }

            var wordFound = wordSearch[row, column] is WordsearchLetters.X &&
                            wordSearch[row - 1, column] is WordsearchLetters.M &&
                            wordSearch[row - 2, column] is WordsearchLetters.A &&
                            wordSearch[row - 3, column] is WordsearchLetters.S;

            return wordFound ? 1 : 0;
        }

        private static int CountWordDiagnolForwardUp(this char[,] wordSearch, int row, int column)
        {
            if (row - 3 < 0 || column + 3 >= wordSearch.NumberOfColumns())
            {
                return 0;
            }

            var wordFound = wordSearch[row, column] is WordsearchLetters.X &&
                            wordSearch[row - 1, column + 1] is WordsearchLetters.M &&
                            wordSearch[row - 2, column + 2] is WordsearchLetters.A &&
                            wordSearch[row - 3, column + 3] is WordsearchLetters.S;

            return wordFound ? 1 : 0;
        }

        private static int CountWordDiagnolForwardDown(this char[,] wordSearch, int row, int column)
        {
            if (row + 3 >= wordSearch.NumberOfRows() || column + 3 >= wordSearch.NumberOfColumns())
            {
                return 0;
            }

            var wordFound = wordSearch[row, column] is WordsearchLetters.X &&
                            wordSearch[row + 1, column + 1] is WordsearchLetters.M &&
                            wordSearch[row + 2, column + 2] is WordsearchLetters.A &&
                            wordSearch[row + 3, column + 3] is WordsearchLetters.S;

            return wordFound ? 1 : 0;
        }

        private static int CountWordDiagnolBackwardDown(this char[,] wordSearch, int row, int column)
        {
            if (row + 3 >= wordSearch.NumberOfRows() || column - 3 < 0)
            {
                return 0;
            }

            var wordFound = wordSearch[row, column] is WordsearchLetters.X &&
                            wordSearch[row + 1, column - 1] is WordsearchLetters.M &&
                            wordSearch[row + 2, column - 2] is WordsearchLetters.A &&
                            wordSearch[row + 3, column - 3] is WordsearchLetters.S;

            return wordFound ? 1 : 0;
        }

        private static int CountWordDiagnolBackwardUp(this char[,] wordSearch, int row, int column)
        {
            if (row - 3 < 0 || column - 3 < 0)
            {
                return 0;
            }

            var wordFound = wordSearch[row, column] is WordsearchLetters.X &&
                            wordSearch[row - 1, column - 1] is WordsearchLetters.M &&
                            wordSearch[row - 2, column - 2] is WordsearchLetters.A &&
                            wordSearch[row - 3, column - 3] is WordsearchLetters.S;

            return wordFound ? 1 : 0;
        }
    }
}
