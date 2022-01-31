using System;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Media;
using System.Media;

namespace WPF_Math_Game_Outline
{
    /// <summary>
    /// Interaction logic for wndGame.xaml
    /// </summary>
    public partial class wndGame : Window
    {
        /// <summary>
        /// Variable to hold the high scores form.
        /// </summary>
        private wndHighScores wndCopyHighScores;

        /// <summary>
        /// Object reference to user data
        /// </summary>
        private wndEnterUserData gameUserData;

        /// <summary>
        /// Variable timer object
        /// </summary>
        DispatcherTimer myTimer;

        /// <summary>
        /// Variable holds name of user passed from user input window
        /// </summary>
        public string sName;

        /// <summary>
        /// Variable holds age of user passed from user input window
        /// </summary>
        public int iAge;

        /// <summary>
        /// Variable holds question number
        /// </summary>
        private int count;

        /// <summary>
        /// Variable holds math radio selection from user input window
        /// </summary>
        public string mathChoice;

        /// <summary>
        /// Variable holds object reference to class w/ business logic for game
        /// </summary>
        game mathGame;

        /// <summary>
        /// Variable to control the clock count
        /// </summary>
        public int timerTick = 0;

        /// <summary>
        /// Sound variable plays nightmare before christmas theme
        /// </summary>
        SoundPlayer simpleSound = new SoundPlayer("nightmareBefore.wav");

        /// <summary>
        /// Property to get and set the high scores.
        /// </summary>
        public wndHighScores CopyHighScores
        { 
            get { return wndCopyHighScores; }
            set { wndCopyHighScores = value; }
        }

        /// <summary>
        /// Property to get and set game data
        /// </summary>
        public wndEnterUserData gameData
        {
            get { return gameUserData; }
            set { gameUserData = value; }
        }

        /// <summary>
        /// Constructor sets controls and game class
        /// </summary>
        public wndGame()///////////////////////////////
        {
            try
            {
                //Instantiate window and game class
                InitializeComponent();

                //Instantiate game class
                mathGame = new game();

                btnGameSubmit.IsEnabled = false;
                txtBxGameAnswer.IsEnabled = false;
                cmdHighScores.IsEnabled = false;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
           
        }

        /// <summary>
        /// Button click method ends the current game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEndGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Reset windows information
                btnGameSubmit.IsEnabled = false;
                txtBxGameAnswer.IsEnabled = false;
                btnStartGame.Visibility = Visibility.Visible;
                lblTimer.Content = "";
                lblMathProblem.Content = "";
                lblAnswer.Content = "";
                lblAnswer.Background = Brushes.DarkGray;
                myTimer.Stop();
                simpleSound.Stop();
                timerTick = 0;
                count = 0;
                mathGame.iCorrect = 0;
                mathGame.iWrong = 0;

                this.Hide();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
           
        }
        
       /// <summary>
       /// Button click method to view final score screen
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void cmdHighScores_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Hide the game form
                this.Hide();
                //Show the high scores
                wndCopyHighScores.ShowDialog();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
            
        }

        /// <summary>
        /// Method for window closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.Hide();
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
               
            }
           
        }

        /// <summary>
        /// Button click method starts the game 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickStartGame(object sender, RoutedEventArgs e)
        {
            try
            {
                //Play nightmare before theme
                simpleSound.Play();
                
                //Enable screen 
                btnGameSubmit.IsEnabled = true;
                txtBxGameAnswer.IsEnabled = true;

                //Start and display clock
                myTimer = new DispatcherTimer();

                //Set timer for every second
                myTimer.Interval = TimeSpan.FromSeconds(1);

                //Timer calls method
                myTimer.Tick += MyTimer_Tick;

                //start timer
                myTimer.Start();

                //hide the button
                btnStartGame.Visibility = Visibility.Hidden;

                //Method call to to set game type
                mathGame.gameType(gameUserData);

                //Begin game with displaying question
                displayMathQuestion();

               
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// This method changes the math question label
        /// </summary>
        private void displayMathQuestion()
        {
            try
            {
                if ((bool)gameUserData.rdoBtnAdd.IsChecked)
                {
                    mathGame.doMath();
                    lblMathProblem.Content = ++count + ".\t " + mathGame.random1 + " + " + mathGame.random2 + " = ";
                }
                else if ((bool)gameUserData.rdoBtnSub.IsChecked)
                {
                    mathGame.doMath();
                    lblMathProblem.Content = ++count + ".\t " + mathGame.random1 + " - " + mathGame.random2 + " = ";
                }
                else if ((bool)gameUserData.rdoBtnMult.IsChecked)
                {
                    mathGame.doMath();
                    lblMathProblem.Content = ++count + ".\t " + mathGame.random1 + " * " + mathGame.random2 + " = ";
                }
                else if ((bool)gameUserData.rdoBtnDiv.IsChecked)
                {
                    mathGame.doMath();
                    lblMathProblem.Content = ++count + ".\t " + mathGame.random1 + " / " + mathGame.random2 + " = ";
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
        /// Button click method to submit user math answer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGameSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //If true continue showing questions for game and fix labels
                if(validate())
                {
                    lblinvalidAnswer.Foreground = Brushes.White;
                    lblinvalidAnswer.Content = "";
                    display();
                } 
                else
                {
                    lblinvalidAnswer.Content = "Enter a number";
                    lblinvalidAnswer.Foreground = Brushes.Red;
                }
               
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }

        }
        /// <summary>
        /// Method call checks user answer input
        /// </summary>
        /// <returns></returns>
        private bool validate()
        {
            try
            {
                //Check for blank
                if(txtBxGameAnswer.Text == "")
                {
                    return false;
                } 
                
                //Check for shift special character or punc.
                for(int i = 0; i < txtBxGameAnswer.Text.Length; i++)
                {
                    if (Char.IsPunctuation(txtBxGameAnswer.Text[i]) || Char.IsSymbol(txtBxGameAnswer.Text[i]))
                    {
                        return false;
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Method displays math questions/updates labels
        /// </summary>
        private void display()
        {
            //change a label to show if they got answer correct or not
            //if count is < 10 keep calling displayMathQuestion
            //Else, bring up high scores window
            try
            {
               
                //Continue asking questions until 10 have been asked
                if (count <= 10)
                {
                    //Pass game answer to determine if correct
                    if(mathGame.checkAnswer(txtBxGameAnswer.Text))
                    {
                        lblAnswer.Background = Brushes.Green;
                        lblAnswer.Content = "Correct!";
                        mathGame.iCorrect++;
                        txtBxGameAnswer.Text = "";
                    }
                    else
                    {
                        lblAnswer.Background = Brushes.Red;
                        lblAnswer.Content = "Incorrect!";
                        mathGame.iWrong++;
                        txtBxGameAnswer.Text = "";
                    }

                    //If user has answered 10 questions, end game and show final score
                    if(count == 10)
                    {
                        //Stop nightmare theme
                        simpleSound.Stop();
                        this.Hide();
                        cmdHighScores.IsEnabled = true;
                        wndCopyHighScores.display(timerTick, mathGame.iCorrect, mathGame.iWrong);
                        wndCopyHighScores.ShowDialog();


                        //Reset windows information
                        btnGameSubmit.IsEnabled = false;
                        txtBxGameAnswer.IsEnabled = false;
                        txtBxGameAnswer.Text = "";
                        btnStartGame.Visibility = Visibility.Visible;
                        lblinvalidAnswer.Content = "";
                        lblTimer.Content = "";
                        lblMathProblem.Content = "";
                        lblAnswer.Content = "";
                        lblAnswer.Background = Brushes.DarkGray;
                        myTimer.Stop();
                        timerTick = 0;
                        count = 0;
                        mathGame.iCorrect = 0;
                        mathGame.iWrong = 0;
                    } 
                    else
                    {
                        displayMathQuestion();
                    }


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
        /// Method increments the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //Increment variable everytime function is called/ 1 sec
                timerTick++;
                lblTimer.Content = timerTick.ToString();
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            
        }

        /// <summary>
        /// Handle the error.
        /// </summary>
        /// <param name="sClass">The class in which the error occurred in.</param>
        /// <param name="sMethod">The method in which the error occurred in.</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// Key press method for validating user input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBxGameAnswer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Only allow numbers to be entered
                if (!((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
                {
                    //Allow backpace and delete 
                    if (!(e.Key == Key.Back || e.Key == Key.Delete))
                    {
                        e.Handled = true;
                    }
                }

                //Validate input and submit answer with enter key
                if (e.Key == Key.Enter)
                {
                    //Don't let user submit blank
                    if (txtBxGameAnswer.Text != "")
                    {
                        //clean up invlaid label and call display method
                        lblinvalidAnswer.Foreground = Brushes.White;
                        lblinvalidAnswer.Content = "";
                        if (validate())
                        {
                            display();
                        }
                        else
                        {
                            lblinvalidAnswer.Content = "Enter a number";
                            lblinvalidAnswer.Foreground = Brushes.Red;
                        }

                    }
                    else
                    {
                        lblinvalidAnswer.Content = "Enter a number";
                        lblinvalidAnswer.Foreground = Brushes.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
    }
}

