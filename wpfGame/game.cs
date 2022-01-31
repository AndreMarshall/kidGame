using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WPF_Math_Game_Outline
{
    /// <summary>
    /// Business logic for game class
    /// </summary>
    class game
    {
        /// <summary>
        /// Variable holds user math selection
        /// </summary>
        public string mathChoice;

        /// <summary>
        /// Variable holds answer to math question
        /// </summary>
        public int answer;

        /// <summary>
        /// Variable for creating random numbers
        /// </summary>
        Random rand1 = new Random();

        /// <summary>
        /// Variable holds number of correct answers
        /// </summary>
        public int iCorrect;

        /// <summary>
        /// Variable holds number of wrong answers
        /// </summary>
        public int iWrong;

        /// <summary>
        /// Variable to holds random number
        /// </summary>
        public int random1;

        /// <summary>
        /// Variable to hold random number
        /// </summary>
        public int random2; 

        /// <summary>
        /// Method gets the type of game for user
        /// </summary>
        /// <param name="wndEnterUserDataForm"></param>
        public void gameType(wndEnterUserData gameUserData)
        {
            try
            {
                if ((bool)gameUserData.rdoBtnAdd.IsChecked)
                {
                    mathChoice = "add";
                    
                }
                else if ((bool)gameUserData.rdoBtnSub.IsChecked)
                {
                    mathChoice = "sub";
                    
                }
                else if ((bool)gameUserData.rdoBtnMult.IsChecked)
                {
                    mathChoice = "mult";
                    
                }
                else if ((bool)gameUserData.rdoBtnDiv.IsChecked)
                {
                    mathChoice = "div";
                    
                }
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This method calls helper method to perform math
        /// </summary>
        public void doMath()
        {
            try
            {
                
                //Method call to math helper methods
                if (mathChoice == "add")
                {
                    doAdd();
                }
                else if (mathChoice == "sub")
                {
                    doSub();
                }
                else if (mathChoice == "mult")
                {
                    doMult();
                }
                else if (mathChoice == "div")
                {
                    doDiv();
                }
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// Function determines if user answered correctly
        /// </summary>
        /// <param name="sUserAnswer"></param>
        /// <returns></returns>
        public bool checkAnswer(string sUserAnswer)
        {
            try
            {
                //Determine if user answer equals the answer
                if (answer.ToString() == sUserAnswer)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method does the addition for the game
        /// </summary>
        private void doAdd()
        {
            try
            {
                //Set variable equal to the random number between 1 and 10
                random1 = rand1.Next(1, 11);
                random2 = rand1.Next(1, 11);

                answer = random1 + random2;

            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method does the subtraction for the game
        /// </summary>
        private void doSub()
        {
            try
            {
                //Set variable equal to the random number between 1 and 10
                random1 = rand1.Next(1, 11);
                random2 = rand1.Next(1, 11);
                int temp;

                //Dont want negatives/ swap numbers 
                if(random2 > random1)
                {
                    temp = random1;
                    random1 = random2;
                    random2 = temp;
                } 
                
                answer = random1 - random2;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method does the division for the game
        /// </summary>
        private void doDiv()
        {
            try
            {
                //Set variable equal to the random number between 1 and 10
                random1 = rand1.Next(1, 11);
                random2 = rand1.Next(1, 11);
                int temp;

                //Check for remainder
                temp = random1 % random2;
                
                //If there is a remainder recall the method and try again
                if(temp > 0) {
                    doDiv();
                } 
                else
                {
                    answer = random1 / random2;
                }
                   
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method does the multiplication for the game
        /// </summary>
        private void doMult()
        {
            try
            {
                //Set variable equal to the random number between 1 and 10
                random1 = rand1.Next(1, 11);
                random2 = rand1.Next(1, 11);

                answer = random1 * random2;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
