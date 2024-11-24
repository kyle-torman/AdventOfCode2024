using System.Text.RegularExpressions;

namespace Main.Day15
{
    public record Step
    {
        private Step(string label, char operation)
        {
            Label = label;
            Operation = operation;
        }

        private Step(string label, char operation, int focalLength)
        {
            Label = label;
            Operation = operation;
            FocalLength = focalLength;
        }

        public string Label { get; }
        public char Operation { get; }
        public int FocalLength { get; }

        public static Step CreateFromInput(string input)
        {
            var stepRegex = new Regex(@"(?<label>[a-zA-Z]+)(?<operation>-|=)(?<focalLength>[0-9]?)");
            var match = stepRegex.Match(input);
            var label = match.Groups["label"].Value;
            var operation = match.Groups["operation"].Value[0];
            if (operation is Operations.Add)
            {
                var focalLength = Convert.ToInt32(match.Groups["focalLength"].Value);
                return new Step(label, operation, focalLength);
            }
            else
            {
                return new Step(label, operation);
            }
        }
    }
}
