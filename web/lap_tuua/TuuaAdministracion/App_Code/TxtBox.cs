using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics; 
 

 
	[DefaultProperty("Text"),
    ToolboxItem(true),
    ToolboxData("<{0}:TxtBox runat=server></{0}:TxtBox>")]
	public class TxtBox : System.Web.UI.WebControls.TextBox 
	{
  
		private NameValueCollectionEx colNameValuePairs = new NameValueCollectionEx();
 
		#region Name Value Pairs
        [Bindable(false),Category("Design")]
        public NameValueCollectionEx NameValuePairs
        {
            get  { return this.colNameValuePairs; }
            set  { this.colNameValuePairs=value; }
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
            object[] newstate = new object[27];

            try
            {
				newstate[0]=(object)this.NameValuePairs.ConvertPairsToString();  
				newstate[1] = (object)this.AutoPostBack;  
				newstate[2] = (object)this.Columns;                              
				newstate[3] = (object)this.MaxLength;   
				newstate[4] = (object)this.TextMode;  
				newstate[5] = (object)this.ReadOnly;   
				newstate[6] = (object)this.Rows;  
				newstate[7] = (object)this.Text;   
				newstate[8] = (object)this.Wrap;   
				newstate[9] = (object)this.AccessKey;  
				newstate[10] = (object)this.BackColor;  
				newstate[11] = (object)this.BorderColor;  
				newstate[12] = (object)this.BorderWidth;  
				newstate[13] = (object)this.BorderStyle;  
				newstate[14] = (object)this.CssClass;  
				newstate[15] = (object)this.Enabled;  
				newstate[16] = (object)this.ForeColor;  
				newstate[17] = (object)this.Height;  
				newstate[18] = (object)this.TabIndex;  
				newstate[19] = (object)this.ToolTip;  
				newstate[20] = (object)this.Width;  
				newstate[21] = (object)this.Site;  
				newstate[22] = (object)this.Visible;  
                newstate[23] = (object)this.Font.Bold;
                newstate[24] = (object)this.Font.Size;
                newstate[25] = (object)this.Font.Italic;
                newstate[26] = (object)this.Font.Name;
 
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
				this.Columns = (System.Int32)newstate[2];                      
				this.MaxLength = (System.Int32)newstate[3]; 
				this.TextMode = (System.Web.UI.WebControls.TextBoxMode)newstate[4];
				this.ReadOnly = (System.Boolean)newstate[5];
				this.Rows = (System.Int32)newstate[6];
				this.Text = (System.String)newstate[7];
				this.Wrap = (System.Boolean)newstate[8];
				this.AccessKey = (System.String)newstate[9];
				this.BackColor = (System.Drawing.Color)newstate[10];
				this.BorderColor = (System.Drawing.Color)newstate[11];
				this.BorderWidth = (System.Web.UI.WebControls.Unit)newstate[12];
				this.BorderStyle = (System.Web.UI.WebControls.BorderStyle)newstate[13];
				this.CssClass =  (System.String)newstate[14];
				this.Enabled = (System.Boolean)newstate[15];
				this.ForeColor = (System.Drawing.Color)newstate[16];
				this.Height = (System.Web.UI.WebControls.Unit)newstate[17];
				this.TabIndex =  (System.Int16)newstate[18];
				this.ToolTip =  (System.String)newstate[19];
				this.Width = (System.Web.UI.WebControls.Unit)newstate[20];
				this.Site = (System.ComponentModel.ISite)newstate[21];
				this.Visible = (System.Boolean)newstate[22];
                this.Font.Bold = (System.Boolean)newstate[23];
                this.Font.Size = (System.Web.UI.WebControls.FontUnit)newstate[24];               
                this.Font.Italic = (System.Boolean)newstate[25];
                this.Font.Name = (System.String)newstate[26];
			}
			catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        #endregion

	}

