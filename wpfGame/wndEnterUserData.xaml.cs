using System.Windows;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace WPF_Math_Game_Outline
{

    /// <summary>
    /// Interaction logic for wndEnterUserData.xaml
    /// </summary>
    public partial class wndEnterUserData : Window
    {
        /// <summary>
        /// After validating, this variable contains users age
        /// </summary>
        public int iUserAge;

        /// <summary>
        /// Variable used for users age
        /// </summary>
        public string sUserAge;

        /// <summary>
        /// Variable contains validated user name
        /// </summary>
        public string sUserName;

        /// <summary>
        /// Variable helps with verifying input
        /// </summary>
        private bool bPass;

        /// <summary>
        /// Variable to see if the user submitted info
        /// </summary>
        public bool bSubmit;

        /// <summary>
        /// Object reference to user info business logic class
        /// </summary>
        UserInfo userInfo;

        /// <summary>
        /// Constructor creates windows
        /// </summary>
        public wndEnterUserData()
        {
            try
            {
                InitializeComponent();

                //Instantiate user class
                userInfo = new UserInfo();
            }
            catch (Exception ex)
            {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Button click method to close user data form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCloseUserDataForm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Hide user data form
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
        /// Method for closing the window
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
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
           
        }

        /// <summary>
        /// Button click method checks user input/ Top level method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickSubmit(object sender, RoutedEventArgs e)
        {
            
            try
            {
                validateInput();
                //wndGameForm.ShowDialog();
            }
            catch (Exception ex)
            {
                //This is the top level method so we want to handle the exception
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Helper method validates user input
        /// </summary>
        /// <returns></returns>
        private void validateInput()
        {
            try
            {
                int iValidateAge;
                string sValidateName;
                bPass = true;


                //check for blank input
                if (txtBxEnterAge.Text == "")
                {
                    //Show an error message
                    lblInvalidAge.Content = "Age between 3 - 10";
                    bPass = false;
                }

                //Limit user input  between 3 - 10
                if (Int32.TryParse(txtBxEnterAge.Text, out iValidateAge) == true)
                {
                    if (iValidateAge > 10 || iValidateAge < 3)
                    {
                        //Display error label message
                        lblInvalidAge.Content = "Age between 3 - 10";
                        bPass = false;
                    }
                    else
                    {
                        lblInvalidAge.Content = "";
                    }
                }
                else
                {
                    //Display error label message
                    lblInvalidAge.Content = "Age between 3 - 10";
                    bPass = false;
                }

                //check for blank input
                if (txtBxEnterName.Text == "")
                {
                    //Show an error message
                    lblInvalidName.Content = "Name must only be letters";
                    bPass = false;
                }


                sValidateName = txtBxEnterName.Text;

                //loop through string and check for #,symbol or !
                for (int i = 0; i < sValidateName.Length; i++)
                {
                    if (char.IsDigit(sValidateName[i]) || char.IsSymbol(sValidateName[i]) || char.IsPunctuation(sValidateName[i]))
                    {
                        //display incorrect label
                        lblInvalidName.Content = "Name must only be letters";
                        bPass = false;
                        //Break loop, no reason to check the rest of the string when invalid input is found
                        break;
                    }
                    else
                    {
                        lblInvalidName.Content = "";
                    }
                }


                //If bool is true user entered correct info
                if (bPass)
                {
                    
                    //user input was correct, input the data into class level variables
                    iUserAge = iValidateAge;
                    sUserName = sValidateName;

                    //Pass info to static members of user class
                    UserInfo.sName = sUserName;
                    UserInfo.iAge = iUserAge;


                    //Reset screen items
                    lblInvalidAge.Content = "";
                    lblInvalidName.Content = "";
                    txtBxEnterAge.Text = "";
                    txtBxEnterName.Text = "";

                    //hide current window and show the game window
                    //Set bool to true, user submitted info
                    bSubmit = true;        

                    //Input was correct, hide window
                    this.Hide();
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
