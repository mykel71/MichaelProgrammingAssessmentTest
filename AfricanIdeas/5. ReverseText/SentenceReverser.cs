using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrepsCS.AfricanIdea.ReverseText
{
    public class SentenceReverser
    {
        public static string ReverseSentence(string sentence)
        {
            // Split the sentence into individual words
            string[] words = sentence.Split(' ');
            // Reverse the order of the words
            Array.Reverse(words);
            // Join the reversed words into a new sentence
            return string.Join(" ", words); 
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter a sentence:");
            string inputSentence = Console.ReadLine();

            string reversedSentence = ReverseSentence(inputSentence);

            Console.WriteLine("Reversed sentence:");
            Console.WriteLine(reversedSentence);
        }
    }
}
