using NUnit.Framework;
using SequencePatternMatcher;
using System.Collections.Generic;

namespace SequencePatternMatcher.Tests
{
    public class SequenceAnalyzerTests
    {
        private MotifIndex<string> index;

        [SetUp]
        public void Setup()
        {
            index = new MotifIndex<string>();

            index.AddSequence("Sequence1","ACGACG");

            index.AddSequence("Sequence2","TTACGAA");
        }



        [Test]
        public void AAA_ShouldNotMatch()
        {
            List<HomopolymerResult> result =
                SequenceAnalyzer.DetectHomoPolymers("AAA");

            Assert.That(result.Count, Is.EqualTo(0));
        }


        [Test]
        public void AAAA_ShouldMatch()
        {
            List<HomopolymerResult> result =
                SequenceAnalyzer.DetectHomoPolymers("AAAA");

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Character, Is.EqualTo('A'));
            Assert.That(result[0].StartPosition, Is.EqualTo(0));
            Assert.That(result[0].Length, Is.EqualTo(4));
        }


        [Test]
        public void CCCCCCC_ShouldMatch()
        {
            List<HomopolymerResult> result =
                SequenceAnalyzer.DetectHomoPolymers("CCCCCCC");

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Character, Is.EqualTo('C'));
            Assert.That(result[0].StartPosition, Is.EqualTo(0));
            Assert.That(result[0].Length, Is.EqualTo(7));
        }



        [Test]
        public void ACGGCA_ShouldMatch()
        {
            List<string> result =SequenceAnalyzer.FindPalindromicRepeats("ACGGCA",3);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("ACGGCA"));
        }


        [Test]
        public void NoPalindrome_ShouldReturnEmpty()
        {
            List<string> result =
                SequenceAnalyzer.FindPalindromicRepeats("ACGTAC",3);

            Assert.That(result.Count, Is.EqualTo(0));
        }


        [Test]
        public void ValidSequence_ShouldBeAccepted()
        {
            bool result =SequenceValidator.IsValid("ACCGTTACG");

            Assert.That(result, Is.True);
        }


        [Test]
        public void InvalidSequence_ShouldBeRejected()
        {
            bool result =SequenceValidator.IsValid("ACCGTXACG");

            Assert.That(result, Is.False);
        }


        [Test]
        public void Motif_ShouldExist()
        {
            Assert.That(index.Exists("ACG"),Is.True);
        }


        [Test]
        public void MotifMultipleTimes_ShouldReturnOccurrences()
        {
            List<MotifOccurrence> result =index.GetOccurrences("ACG");


            Assert.That(result.Count,Is.EqualTo(3));

            Assert.That(result[0].SequenceName,Is.EqualTo("Sequence1"));

            Assert.That(result[0].Position,Is.EqualTo(0));

            Assert.That(result[1].Position,Is.EqualTo(3));

            Assert.That(result[2].SequenceName,Is.EqualTo("Sequence2"));

            Assert.That(result[2].Position,Is.EqualTo(2));
        }


        [Test]
        public void MissingMotif_ShouldReturnEmpty()
        {
            List<MotifOccurrence> result =index.GetOccurrences("TTT");

            Assert.That(result.Count,Is.EqualTo(0));
        }


        [Test]
        public void Complexity_ShouldBeCorrect()
        {
            double result =SequenceAnalyzer.CalculateComplexity("ACGACGTT");

            Assert.That(result,Is.EqualTo(5.0 / 6.0).Within(0.0001));
        }


        [Test]
        public void ShortSequence_ShouldReturnZero()
        {
            double result =SequenceAnalyzer.CalculateComplexity("AC");

            Assert.That( result,Is.EqualTo(0));
        }
    }
}