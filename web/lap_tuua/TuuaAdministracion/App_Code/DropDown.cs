using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics; 


    [DefaultProperty("Text"),
    ToolboxItem(true),
    ToolboxData("<{0}:DropDown runat=server></{0}:DropDown>")]
	public class DropDown : System.Web.UI.WebControls.DropDownList 
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
            NameValueCollectionEx items = new NameValueCollectionEx();
            object[] newstate = new object[27];
            try
            {
                //for (int i = 0; i < this.Items.Count; i++)
                //{
                //   items.Add("value" + i.ToString(),this.Items[i].Value);
                //   items.Add("text" + i.ToString(),this.Items[i].Text);
                //}

				newstate[0]=(object)this.NameValuePairs.ConvertPairsToString();  
				newstate[1] = (object)this.AutoPostBack; 
                newstate[2] = (object)this.SelectedIndex;
                newstate[3] = (object)items.ConvertPairsToString(); 
                newstate[4] = (object)this.SelectedValue;
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
  
            NameValueCollectionEx items = new NameValueCollectionEx();

			try
			{
				object[] newstate = (object[])savedState;

				this.colNameValuePairs.FillFromString((string)newstate[0]); 
 
                items.FillFromString((string)newstate[3]); 

                int ListItemCount = 0;

                if (items.Count > 0)
                {

                  //ListItemCount = items.Count / 2;

                  //for(int i=0;i<ListItemCount;i++)
                  //{
                  //  this.Items.Add(new ListItem(items["text" + i.ToString()].ToString(),items["value" + i.ToString()].ToString()));
                  //} 

                  this.SelectedIndex = (System.Int32)newstate[2]; 
                  this.SelectedValue = (System.String)newstate[4];

                }

                this.AutoPostBack = (System.Boolean)newstate[1];
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

