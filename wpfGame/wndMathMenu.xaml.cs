using System;
using System.Media;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_Math_Game_Outline
{
    /// <summary>
    /// Interaction logic for wndMathMenu.xaml
    /// </summary>
    public partial class wndMathMenu : Window
    {
        /// <summary>
        /// Class that holds the high scores.
        /// </summary>
        wndHighScores wndHighScoresForm;

        /// <summary>
        /// Class that holds the user data.
        /// </summary>
        wndEnterUserData wndEnterUserDataForm;

        /// <summary>
        /// Class where the game is played.
        /// </summary>
        wndGame wndGameForm;

        /// <summary>
        /// Constructor for main menu/instantiates all game windows
        /// </summary>
        public wndMathMenu()
        {
            try
            {
                InitializeComponent();
        
                cmdPlayGame.IsEnabled = false;
                cmdHighScores.IsEnabled = false;
                cmdPlayGame.Visibility = Visibility.Hidden;

                //MAKE SURE TO INCLUDE THIS LINE OR THE APPLICATION WILL NOT CLOSE
                //BECAUSE THE WINDOWS ARE STILL IN MEMORY
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;///////////////////////////////////////////////////////////

                wndHighScoresForm = new wndHighScores();
                wndEnterUserDataForm = new wndEnterUserData();
                wndGameForm = new wndGame();

                //Pass user data to game window
                wndGameForm.gameData = wndEnterUserDataForm;

                //Pass the high scores form to the game form.  This way the high scores form may be displayed via the game form.
                wndGameForm.CopyHighScores = wndHighScoresForm;
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
           

        }

        /// <summary>
        /// Button click method to bring user to game screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPlayGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Hide the menu
                this.Hide();
                //Show the game form
                wndGameForm.ShowDialog();
                //Show the main form
                this.Show();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
           
        }

        /// <summary>
        /// Button click method to bring user to final score window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdHighScores_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Hide the menu
                this.Hide();
                //Show the high scores screen
                wndHighScoresForm.ShowDialog();
                //Show the main form
                this.Show();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
           
        }

        /// <summary>
        /// Button click method to bring user to user info window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEnterUserData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Hide the menu
                this.Hide();
                //Show the user data form
                wndEnterUserDataForm.ShowDialog();
                //Show the main form
                this.Show();

                //If true, unlock game buttons for user
                if (wndEnterUserDataForm.bSubmit)
                {
                    cmdPlayGame.IsEnabled = true;
                    cmdHighScores.IsEnabled = true;
                    cmdPlayGame.Visibility = Visibility.Visible;
                    SoundPlayer simpleSound = new SoundPlayer("doorOpen.wav");
                    simpleSound.Play();
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
    }
}
