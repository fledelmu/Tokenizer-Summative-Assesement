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
    /// </summary>


    internal class Implementation
    {
     
        private const char delimiter = '\\';
        

        public Implementation()
        {
           
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
                if (char.IsWhiteSpace(str[i]))
                {
                    if (token.Length > 0)
                    {
                        tokens.Add(token.ToString());
                        token.Clear();
                    }
                }
                else if (char.IsPunctuation(str[i]) && str[i] != delimiter)
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
            bool hasLetter = false;
            bool hasDigit = false;

            foreach (char ch in token)
            {
                if (char.IsLetter(ch))
                {
                    hasLetter = true;
                    currentToken.Append(ch);
                }
                else if (char.IsDigit(ch))
                {
                    hasDigit = true;
                    currentToken.Append(ch);
                }
                else if (char.IsPunctuation(ch))
                {
                    if (currentToken.Length > 0)
                    {
                        result.Add(new Tuple<string, string>(currentToken.ToString(), "Alphanumeric"));
                        currentToken.Clear();
                    }
                    result.Add(new Tuple<string, string>(ch.ToString(), "Punctuation"));
                }
                else
                {
                    if (currentToken.Length > 0)
                    {
                        result.Add(new Tuple<string, string>(currentToken.ToString(), "Alphanumeric"));
                        currentToken.Clear();
                    }
                    result.Add(new Tuple<string, string>(ch.ToString(), "Special Character"));
                }
            }

            if (currentToken.Length > 0)
            {
                string type = hasLetter && hasDigit ? "Alphanumeric" : "Unknown";
                result.Add(new Tuple<string, string>(currentToken.ToString(), type));
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
             
                result.Add(c.ToString());
            
            }
            return string.Join(", ", result);
        }
    }
}
