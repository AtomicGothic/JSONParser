using System;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Collections.Generic;

namespace JSONParser
{
    public partial class ParserForm : Form
    {
        public ParserForm()
        {
            InitializeComponent();
        }


        #region UI events

        private void cmdFilter_Click(object sender, EventArgs e)
        {
            //textOutput(txtInput.Text);
            deserializeJSON(txtInput.Text);
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtOutput.Text = string.Empty;
        }

        #endregion


        #region json functions
        private void deserializeJSON(string strJSON)
        {
            try
            {
                var inputObj = JsonConvert.DeserializeObject<TransactionList>(strJSON);

                //truncating the Date so it shows only MM/DD/YYYY
                for (int i = 0; i < inputObj.rows.Count; i++)
                {
                    inputObj.rows[i].Date = truncateDate(inputObj.rows[i].Date);
                }

                List<Transaction> outputObj = new List<Transaction>();
                if(txtInput.Text != string.Empty)
                {
                    /***********************************************************************
                     * Go through every element of the incoming object. If the amount,
                     *   the last four, the card brand type (e.g. VISA, MASTER, AMEX), 
                     *   and the date of each transaction matches the next element, add to
                     *   the output object.
                     *   
                     *   NOTE: The incoming JSON file is assumed to be pre-sorted.
                     **********************************************************************/
                    for (int i = 0; i < inputObj.rows.Count - 1; i++)
                    {
                       if ((inputObj.rows[i].Amount   == inputObj.rows[i + 1].Amount)    &&
                           (inputObj.rows[i].LastFour == inputObj.rows[i + 1].LastFour)  &&
                           (inputObj.rows[i].Brand    == inputObj.rows[i + 1].Brand)     &&
                           (inputObj.rows[i].Date     == inputObj.rows[i + 1].Date))
                        {
                            outputObj.Add(inputObj.rows[i]);
                        }

                    }
                }
                textOutput(JsonConvert.SerializeObject(outputObj));
            }
            catch(Exception ex)
            {
                textOutput("We had a problem: " + ex.Message.ToString());
            }
        }

        // Incoming JSON file has hour, minute, second timestamp that needs to be removed.
        // TruncateDate solves that issue.
        private string truncateDate(string date)
        {
            string outputString = "";
            if (date.Contains(" "))
            {
                int index = date.IndexOf(" ");
                outputString = date.Substring(0, index);
            }
            return outputString;
        }

        #endregion

        #region Debug Output
        private void textOutput(string strText)
        {
            try
            {
                System.Diagnostics.Debug.Write(strText + Environment.NewLine);
                txtOutput.Text = txtOutput.Text + strText + Environment.NewLine;
                txtOutput.SelectionStart = txtOutput.TextLength;
                txtOutput.ScrollToCaret();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message.ToString() + Environment.NewLine);
            }
        }
        #endregion
    }
}
