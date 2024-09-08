using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TokenizerProject
{
    /// <summary>
    /// General Rules/Instructions: (kay mag github ta and para mura tag professionals haha)
    /// 1. When adding to the code comment inside the block what each thing does (di kailangan english)
    /// 2. Pull when trying to make changes to the class files.(para way mangutana unya unsa gi change sa lahi kay makitan sa pull request unsa na change or add)
    /// 3. Use docstrings (///) when making comments
    /// 4. Kato ra pakyu nimo Brendan Joshua Acuna.
    /// </summary>


    internal class Implementation
    {
        private string str;
        private const char delimiter = '\\';
        private List<string> grains;
        private Dictionary<string, string> tokens;

        public Implementation()
        {
            this.str = "";
            this.tokens = new Dictionary<string, string>();
            this.grains = new List<string>();
        }

        /// <summary>
        /// Accepts <paramref name="str"/> to turn the word or sentence into tokens
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns> 
        /// Returns the variable token which will be used in the TokenArrayBuilder method
        /// </returns>
        public List<string> Tokenize(string str)
        {
            List<string> tokens = new List<string>();
            StringBuilder token = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsPunctuation(str[i]) && str[i] != delimiter)
                {
                    if (token.Length > 0)
                    {
                        tokens.Add(token.ToString());
                        token.Clear();
                    }
                    tokens.Add(str[i].ToString());
                }
                else if (str[i] != delimiter)
                {
                    token.Append(str[i]);
                }
                else
                {
                    if (token.Length > 0)
                    {
                        tokens.Add(token.ToString());
                        token.Clear();
                    }
                }
            }

            if (token.Length > 0)
            {
                tokens.Add(token.ToString());
            }
            return tokens;
        }

        /// <summary>
        /// Classifies the tokens into categories {word, punctuation}
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// Classifies the tokens into categories {word, punctuation}
        /// </summary>
        /// <returns></returns>
                public List<Tuple<string, string>> Classify(List<string> tokens)
        {
            List<Tuple<string, string>> classifications = new List<Tuple<string, string>>();

            foreach (string token in tokens)
            {
                if (token.Any(char.IsLetter) && token.Any(char.IsDigit))
                {
                    classifications.AddRange(ClassifyMixedToken(token));
                }
                else
                {
                    var result = ClassifyToken(token);
                    if (result.Contains("Word") || result.Contains("Number") || result.Contains("Punctuation"))
                    {
                        var splitResult = result.Split(' ');
                        classifications.Add(new Tuple<string, string>(splitResult[0], splitResult[1]));
                    }
                }
            }

            return classifications;
        }

        private List<Tuple<string, string>> ClassifyMixedToken(string token)
        {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();
            StringBuilder currentToken = new StringBuilder();
            string currentType = "";

            foreach (char ch in token)
            {
                if (char.IsLetter(ch))
                {
                    if (currentToken.Length > 0 && currentType == "Number")
                    {
                        result.Add(new Tuple<string, string>(currentToken.ToString(), "Number"));
                        currentToken.Clear();
                    }
                    currentToken.Append(ch);
                    currentType = "Word";
                }
                else if (char.IsDigit(ch))
                {
                    if (currentToken.Length > 0 && currentType == "Word")
                    {
                        result.Add(new Tuple<string, string>(currentToken.ToString(), "Word"));
                        currentToken.Clear();
                    }
                    currentToken.Append(ch);
                    currentType = "Number";
                }
            }

            if (currentToken.Length > 0)
            {
                result.Add(new Tuple<string, string>(currentToken.ToString(), currentType));
            }

            return result;
        }

        public string ClassifyToken(string token)
        {
            if (token.All(char.IsLetter))
            {
                return $"{token} Word";
            }
            else if (token.All(char.IsDigit))
            {
                return $"{token} Number";
            }
            else if (token.All(char.IsPunctuation))
            {
                return $"{token} Punctuation";
            }
            else
            {
                return $"{token} Unknown";
            }
        }

        /// <summary>
        /// Tokenizes the input string based on a delimiter (like a space or any character)
        /// </summary>
        /// <param name="str">The input string</param>
        /// <returns>A list of tokens (words)</returns>
        public string GranularPhase(string token)
        {
        //delimiter is \ (backslash)
            List<string> result = new List<string>();

            foreach (char c in token)
        {
            if (c != delimiter)
            {    
                result.Add(c.ToString());
            }
        }
        return string.Join(", ", result);
        }
    }
}
