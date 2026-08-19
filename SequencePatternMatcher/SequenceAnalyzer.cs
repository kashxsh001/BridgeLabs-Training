using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace SequencePatternMatcher
{
    public class MotifOccurrence
    {
        public string SequenceName;
        public int Position;

        public MotifOccurrence(string sequenceName, int position)
        {
            SequenceName = sequenceName;
            Position = position;
        }
    }
    public class HomopolymerResult
    {
        public char Character;
        public int StartPosition;
        public int Length;

        public HomopolymerResult(
            char character,
            int startPosition,
            int length)
        {
            Character = character;
            StartPosition = startPosition;
            Length = length;
        }
    }

    public class SequenceValidator
    {
        public static bool IsValid(string sequence)
        {
            if (string.IsNullOrEmpty(sequence))
            {
                return false;
            }
            Regex regex = new Regex("^[ACGT]+$");

            return regex.IsMatch(sequence);
        }
    }

    public class MotifIndex<T>
    {
        private Dictionary<string, List<MotifOccurrence>> index;
        public MotifIndex()
        {
            index = new Dictionary<string, List<MotifOccurrence>>();
        }

        public bool Exists(string motif) => index.ContainsKey(motif);
        public void AddSequence(string sequenceName, string text)
        {
            if (!SequenceValidator.IsValid(text))
            {
                throw new ArgumentException(
                    "Invalid sequence.");
            }
            for (int start = 0; start < text.Length; start++)
            {
                for (int length = 1; start + length <= text.Length; length++)
                {
                    string motif = text.Substring(start, length);

                    if (!index.ContainsKey(motif))
                    {
                        index[motif] =
                            new List<MotifOccurrence>();
                    }

                    index[motif].Add(
                        new MotifOccurrence(
                            sequenceName,
                            start));
                }
            }
        }
        public List<MotifOccurrence> GetOccurrences(string motif)
        {
            if (!index.ContainsKey(motif))
            {
                return new List<MotifOccurrence>();
            }

            return index[motif];
        }
        public List<string> GetSequences(string motif)
        {
            List<string> result = new List<string>();
            if (!index.ContainsKey(motif))
            {
                return result;
            }
            List<MotifOccurrence> occurrences = index[motif];
            for (int i = 0; i < occurrences.Count; i++)
            {
                string sequence = occurrences[i].SequenceName;
                if (!result.Contains(sequence))
                {
                    result.Add(sequence);
                }
            }
            return result;
        }
    }
public class SequenceAnalyzer
    {
        public static List<HomopolymerResult> DetectHomoPolymers(string sequence)
        {
            List<HomopolymerResult> result = new List<HomopolymerResult>();

            string pattern = "[ACGT]{4,}";


            MatchCollection matches = Regex.Matches(sequence, pattern);


            for (int i = 0; i < matches.Count; i++)
            {
                Match match = matches[i];
                string value = match.Value;
                result.Add(new HomopolymerResult(value[0],match.Index,match.Length));
            }

            return result;
        }
        public static List<string>FindPalindromicRepeats( string sequence,int motifLength)
        {
            List<string> result =new List<string>();
            if (motifLength <= 0)
            {
                return result;
            }
            string pattern ="[ACGT]{" + motifLength + "}";
            MatchCollection matches =Regex.Matches(sequence, pattern);
            for (int i = 0; i < matches.Count;i++)
            {
                Match match = matches[i];
                int start =match.Index;
                int secondStart =start + motifLength;

                if (secondStart + motifLength <= sequence.Length)
                {
                    string first = sequence.Substring(start, motifLength);
                    string second =sequence.Substring( secondStart,motifLength);
                    char[] characters = first.ToCharArray();
                    Array.Reverse(characters);
                    string reverse =new string(characters);
                    if (second == reverse)
                    {
                        result.Add(first + second);
                    }
                }
            }
            return result;
        }

        public static double CalculateComplexity(string sequence)
        {
            if (sequence == null ||sequence.Length < 3)
            {
                return 0;
            }
            List<string> motifs =new List<string>();
            for (int i = 0;i <= sequence.Length - 3;i++)
            {
                string motif =sequence.Substring(i, 3);
                motifs.Add(motif);
            }
            int total = motifs.Count;
            HashSet<string> uniqueMotifs =new HashSet<string>();
            for (int i = 0; i < motifs.Count;i++)
            {
                uniqueMotifs.Add(motifs[i]);    
            }
            int unique =uniqueMotifs.Count;
            return (double)unique / total;
        }
    }
}
