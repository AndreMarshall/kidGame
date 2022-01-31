using System;
using System.Media;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_Math_Game_Outline
{
    /// <summary>
    /// Interaction logic for wndHighScores.xaml
    /// </summary>
    public partial class wndHighScores : Window
    {
     
        /// <summary>
        /// Constructor for final score window
        /// </summary>
        public wndHighScores()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
           
        }

        /// <summary>
        /// Button click method to hide window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCloseHighScores_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);         
            }
            
        }

        /// <summary>
        /// Method for closing window
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
        /// Method displays the user information to window
        /// </summary>
        /// <param name="timerTick"></param>
        /// <param name="iCorrect"></param>
        /// <param name="iWrong"></param>
        public void display(int timerTick, int iCorrect, int iWrong)
        {
            try
            {
                determineOutcome(iCorrect);
                string boxInfo;

                //text block header/Info
                boxInfo = "Name\t\tAge\t\tCorrect\t\tIncorrect\t\t Ending Time\n" + UserInfo.sName + "\t\t"
                    + UserInfo.iAge + "\t\t" + iCorrect + "\t\t" + iWrong + "\t\t" + timerTick + " seconds";

                //Set text block to string
                txtBlckFinalScore.Text = boxInfo;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
           
        }
        
        /// <summary>
        /// Method  will give different sound effects/pictures depending on score
        /// </summary>
        private void determineOutcome(int iCorrect)
        {
            try
            {
                
                if (iCorrect <= 4)
                {
                    lblWinnings.Content = "We can do better!";
                    ImageBrush myBrush = new ImageBrush(new BitmapImage(new Uri(@"oogieBoogie.jpg", UriKind.Relative)));
                    grdFinalScore.Background = myBrush;
                    SoundPlayer simpleSound = new SoundPlayer("christmasCancelled.wav");
                    simpleSound.Play();
                } 
                else if(iCorrect <= 7)
                {
                    lblWinnings.Content = "Good work, but we can do better!";
                    ImageBrush myBrush = new ImageBrush(new BitmapImage(new Uri(@"jackMid.png", UriKind.Relative)));
                    grdFinalScore.Background = myBrush;
                    SoundPlayer simpleSound = new SoundPlayer("goodNight.wav");
                    simpleSound.Play();
                } 
                else
                {
                    lblWinnings.Content = "Awesome job!";
                    ImageBrush myBrush = new ImageBrush(new BitmapImage(new Uri(@"jackWin.jpg", UriKind.Relative)));
                    grdFinalScore.Background = myBrush;
                    SoundPlayer simpleSound = new SoundPlayer("greatHalloween.wav");
                    simpleSound.Play();
                }
                        

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
    }
}
