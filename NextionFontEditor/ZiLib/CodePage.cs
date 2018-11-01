namespace ZiLib {

    public class CodePage {
        public CodePageIdentifier CodePageIdentifier { get; set; }

        public int FirstByteStart { get; set; }
        public int FirstByteEnd { get; set; }
        public int? SecondByteStart { get; set; }
        public int? SecondByteEnd { get; set; }

        public int CharacterCount { get; set; }
        public char[] Characters { get; set; }

        public bool IsMultibyte => SecondByteStart.HasValue && SecondByteEnd.HasValue;
    }
}