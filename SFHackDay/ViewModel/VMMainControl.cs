using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using NHunspell;
using SFHackDay.Model;
using System.Text.RegularExpressions;
namespace SFHackDay.ViewModel
{
    class VMMainControl
    {
        #region Constant
        Dictionary<int, int> dicPossibleWord;
        const int POSSIBLE_SPEC_CHAR = 10;
        #endregion
        #region DataMember
        MMainControl _model;

        public MMainControl Model
        {
            get { return _model; }
            set { _model = value; }
        }

        #endregion
        #region constructor
        public VMMainControl()
        {
            Model = new MMainControl();
            dicPossibleWord = new Dictionary<int, int>();
            dicPossibleWord.Add(1, 1000);
            dicPossibleWord.Add(2, 2000);
            dicPossibleWord.Add(3, 3000);
            dicPossibleWord.Add(4, 4000);
            dicPossibleWord.Add(5, 5000);
            dicPossibleWord.Add(6, 5000);
            dicPossibleWord.Add(7, 4000);
            dicPossibleWord.Add(8, 3000);
            dicPossibleWord.Add(9, 2000);
            dicPossibleWord.Add(10, 1000);
        }
        #endregion
        #region Command
        DelegateCommand<object> _handleButton;

        public DelegateCommand<object> HandleButton
        {
            get {
                if (_handleButton == null)
                {
                    _handleButton = new DelegateCommand<object>(DoSomething);
                }
                return _handleButton; }

        }
        bool CanButtonClick(object parameter)
        {
            return false;
        }
        public void DoSomething (object parameter)
        {
            //TO DO
            if (parameter is System.Windows.Controls.PasswordBox)
            {
                int numberOfWord = 0;
                int numberOfDigit = 0;
                int numberOfSpecialChar = 0;
                int numberOfChar = 0;
                int totalWordChar = 0;
                int totalCharNotWord = 0;
                ulong totalNumberOfRecord = 1;
                ulong totalWordPossible = 1;
                List<string> numberOfWordInString = new List<string>(); 
                int totalNumberOfVariable = 0;
                var passwordBox = parameter as System.Windows.Controls.PasswordBox;
                string password = passwordBox.Password;
                //regex for special char
                Regex rgxSpecial = new Regex("[^a-z0-9]", RegexOptions.IgnoreCase);
                numberOfSpecialChar = rgxSpecial.Matches(password).Count;
                //regex for digit 
                Regex rgxNumber = new Regex(@"\d+", RegexOptions.IgnoreCase);
                numberOfDigit = rgxNumber.Matches(password).Count;
                // find alphabatic charcter 
                Regex rgxNumberOfChar = new Regex("[a-z]",RegexOptions.IgnoreCase);
                numberOfChar = rgxNumberOfChar.Matches(password).Count;
                //split the password using special char and digit
                string[] splitedStringByNumber = rgxNumber.Split(password);
                
                foreach (string s in splitedStringByNumber)
                {
                    string[] splitedStringBySChar = rgxSpecial.Split(s);
                    foreach(string st in splitedStringBySChar)
                    {
                        if (st != String.Empty)
                        {
                            numberOfWordInString.Add(st);
                        }
                    }
                }
                using (Hunspell hunspell = new Hunspell("en-US.aff", "en-US.dic"))
                {
                    for (int i = 0; i < numberOfWordInString.Count(); i++ )
                    {
                        if (!hunspell.Spell(numberOfWordInString[i]))
                        {
                            //replace with empty string
                            numberOfWordInString[i] = "";
                        }
                        else // total possible record
                        {
                          int value;
                          if (dicPossibleWord.TryGetValue(numberOfWordInString[i].Count(), out value))
                          {
                              totalWordPossible = totalWordPossible *(ulong) value;
                          }
                              
                        }
                    }
                }
                numberOfWordInString.RemoveAll(x => x.Equals(""));
                numberOfWord = numberOfWordInString.Count();                
                //Find the character that is not a word

                foreach(string s in numberOfWordInString)
                {
                    totalWordChar += s.Count();   
                }
                totalCharNotWord = numberOfChar - totalWordChar;
                //total number of varible digit + special char + number of word + number of charact
                totalNumberOfVariable = (numberOfDigit > 0 ? numberOfDigit : 0) + (numberOfSpecialChar > 0 ? numberOfSpecialChar : 0) + (numberOfWord > 0 ? numberOfWord : 0) + (totalCharNotWord > 0 ? totalCharNotWord : 0);
               // find the possible record search and use rendom to combo
               totalNumberOfRecord =(ulong)(Math.Pow(10, numberOfDigit) * totalWordPossible * Math.Pow(POSSIBLE_SPEC_CHAR,numberOfSpecialChar) * totalNumberOfVariable)/2;
               double test1 = (Math.Log(((0.65477472 * totalNumberOfRecord) / 3542) + Math.Exp(14.66556)) - 14.66556);
               double test2 = Math.Exp(14.66556);
               double test3 = 1.527243 * test1 * test2;                
               Model.TimeToCrack = (((1.527243) * (Math.Log(((0.65477472 * totalNumberOfRecord) / 3542) + Math.Exp(14.66556)) - 14.66556)) * 1000).ToString("#.####") + " milliseconds" ;
               Model.Message = "Password has " + totalNumberOfRecord + " half time possible combination." + "It has " + totalNumberOfVariable + " Variable.";
            }

            /* Password1
             * What if there is no workd
             * 1. First search letter word exist 
             * 2. The password has three varibale 2 word Pass, word and digit 1
             * Assumption there is 1000 possible four letter word
             * 3. Possible record to search is 10 ^ 1 * (1000 * 1000) * 3 (number of variable)
             * 4. True combo will be Random (1,0) lets say 1/2 for test
             * 5. True combo will be 1/2 of 10 ^ 1 * (1000 * 1000) * 3
             * 6. MTBF = 1/0.65477472 * [log (((0.0.65477472 * combo)/3542) + e^14.66556) - 14.66556  ]
             * 7. == 1.527243 * [log (((0.0.65477472 * combo)/3542) + e^14.66556) - 14.66556  ]
             */


        }
        #endregion
    }
}
