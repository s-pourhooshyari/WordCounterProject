namespace WordCounter.Domain.Context.ValueObjects
{
    public class Word
    {
        public string Value { get; }
        public int Frequency { get; set; }

        public Word(string value)
        {
            Value = value;
            Frequency = 1;
        }

        public void IncrementFrequency()
        {
            Frequency++;
        }
    }
}
