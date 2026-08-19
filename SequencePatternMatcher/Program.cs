using SequencePatternMatcher;
using System;
using System.Collections.Generic;

namespace SequencePatternMatcher
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("SEQUENCE VALIDATION");

            string validSequence = "ACCGTTACG";
            string invalidSequence = "ACCGTXACG";

            Console.WriteLine($"validSequence: {SequenceValidator.IsValid(validSequence)}");

            Console.WriteLine($"invalidSequence:{SequenceValidator.IsValid(invalidSequence)}");

            Console.WriteLine();
            Console.WriteLine("===== MOTIF INDEX =====");

            MotifIndex<string> index =new MotifIndex<string>();
            index.AddSequence("Sequence1","ACGACG");
            index.AddSequence("Sequence2","TTACGAA");
            string motif = "ACG";
            Console.WriteLine($"Does motif exist? {index.Exists(motif)}");

            List<string> sequences =index.GetSequences(motif);
            for (int i = 0;i < sequences.Count;i++)
            {
                Console.WriteLine(sequences[i]);
            }
            Console.WriteLine();
            Console.WriteLine("===== HOMOPOLYMER =====");

            string sequence ="ACGGCCCCCCATTTT";

            List<HomopolymerResult> homopolymers =SequenceAnalyzer.DetectHomoPolymers(sequence);
            Console.WriteLine("Sequence: " + sequence);
            for (int i = 0;i < homopolymers.Count;i++)
            {
                Console.WriteLine("Character: " +homopolymers[i].Character);
                Console.WriteLine("Start Position: " + homopolymers[i].StartPosition);
                Console.WriteLine("Length: "+ homopolymers[i].Length);
                Console.WriteLine();
            }


            Console.WriteLine("PALINDROMIC-STYLE REPEAT");

            string palindromeSequence ="TTACGGCAAT";

            Console.WriteLine("Sequence: " +palindromeSequence);


            List<string> palindromicRepeats =SequenceAnalyzer.FindPalindromicRepeats(palindromeSequence,3);
            if (palindromicRepeats.Count == 0)
            {
                Console.WriteLine("No palindromic-style repeat found.");
            }
            else
            {
                Console.WriteLine("Palindromic-style repeats:");

                for (int i = 0;i < palindromicRepeats.Count;i++)
                {
                    Console.WriteLine(palindromicRepeats[i]);
                }
            }


            Console.WriteLine();
            Console.WriteLine("SEQUENCE COMPLEXITY");


            string sequence1 ="ACGACGTT";

            string sequence2 ="AAAAAAAA";

            string sequence3 ="ACGTACGA";


            double complexity1 =SequenceAnalyzer.CalculateComplexity(sequence1);

            double complexity2 =SequenceAnalyzer.CalculateComplexity(sequence2);

            double complexity3 =SequenceAnalyzer.CalculateComplexity(sequence3);


            Console.WriteLine($"{sequence1} -> Complexity = {complexity1}");
            Console.WriteLine($"{sequence2} -> Complexity = {complexity2}");
            Console.WriteLine($"{sequence3} -> Complexity = {complexity3}");



            Console.WriteLine();
            Console.WriteLine("SHORT SEQUENCE");

            string shortSequence = "AC";

            double shortComplexity =SequenceAnalyzer.CalculateComplexity(shortSequence);

            Console.WriteLine($"{shortSequence} -> Complexity = {shortComplexity}");
        }
    }
}