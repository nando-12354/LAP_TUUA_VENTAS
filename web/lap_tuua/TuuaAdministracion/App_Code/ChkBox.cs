using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics; 


    [DefaultProperty("Text"),
    ToolboxItem(true),
    ToolboxData("<{0}:CheckBox runat=server></{0}:CheckBox>")]
    public class ChkBox : System.Web.UI.WebControls.CheckBox
    {

        private NameValueCollectionEx colNameValuePairs = new NameValueCollectionEx();

        #region Name Value Pairs
        [Bindable(false), Category("Design")]
        public NameValueCollectionEx NameValuePairs
        {
            get { return this.colNameValuePairs; }
            set { this.colNameValuePairs = value; }
        }
        #endregion

        #region Render
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);
        }
        #endregion

        #region Save View State
        protected override object SaveViewState()
        {
            object[] newstate = new object[20];

            try
            {
                newstate[0] = (object)this.NameValuePairs.ConvertPairsToString();
                newstate[1] = (object)this.AutoPostBack;
                newstate[2] = (object)this.Checked;
                newstate[3] = (object)this.Text;
                newstate[4] = (object)this.AccessKey;
                newstate[5] = (object)this.BackColor;
                newstate[6] = (object)this.BorderColor;
                newstate[7] = (object)this.BorderWidth;
                newstate[8] = (object)this.BorderStyle;
                newstate[9] = (object)this.CssClass;
                newstate[10] = (object)this.Enabled;
                newstate[11] = (object)this.ForeColor;
                newstate[12] = (object)this.Height;
                newstate[13] = (object)this.TabIndex;
                newstate[14] = (object)this.ToolTip;
                newstate[15] = (object)this.Width;
                newstate[16] = (object)this.Site;
                newstate[17] = (object)this.Visible;
                newstate[18] = (object)this.Font.Bold;
                newstate[19] = (object)this.Font.Size;
                newstate[20] = (object)this.Font.Italic;
                newstate[21] = (object)this.Font.Name;

            }
            catch { }
            return newstate;
        }
        #endregion

        #region Load View State
        protected override void LoadViewState(object savedState)
        {
            try
            {
                object[] newstate = (object[])savedState;
                this.colNameValuePairs.FillFromString((string)newstate[0]);
                this.AutoPostBack = (System.Boolean)newstate[1];
                this.Checked = (System.Boolean)newstate[2];
                this.Text = (System.String)newstate[3];
                this.AccessKey = (System.String)newstate[4];
                this.BackColor = (System.Drawing.Color)newstate[5];
                this.BorderColor = (System.Drawing.Color)newstate[6];
                this.BorderWidth = (System.Web.UI.WebControls.Unit)newstate[7];
                this.BorderStyle = (System.Web.UI.WebControls.BorderStyle)newstate[8];
                this.CssClass = (System.String)newstate[9];
                this.Enabled = (System.Boolean)newstate[10];
                this.ForeColor = (System.Drawing.Color)newstate[11];
                this.Height = (System.Web.UI.WebControls.Unit)newstate[12];
                this.TabIndex = (System.Int16)newstate[13];
                this.ToolTip = (System.String)newstate[14];
                this.Width = (System.Web.UI.WebControls.Unit)newstate[15];
                this.Site = (System.ComponentModel.ISite)newstate[16];
                this.Visible = (System.Boolean)newstate[17];
                this.Font.Bold = (System.Boolean)newstate[18];
                this.Font.Size = (System.Web.UI.WebControls.FontUnit)newstate[19];
                this.Font.Italic = (System.Boolean)newstate[20];
                this.Font.Name = (System.String)newstate[21];
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        #endregion

    }
