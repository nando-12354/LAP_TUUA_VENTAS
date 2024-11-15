using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;

[DefaultProperty("Text"),
ToolboxItem(true),
ToolboxData("<{0}:MyLabel runat=server></{0}:MyLabel>")]

public class MyLabel : System.Web.UI.WebControls.Label
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
        object[] newstate = new object[18];

        try
        {
            newstate[0] = (object)this.NameValuePairs.ConvertPairsToString();
            newstate[1] = (object)this.Text;
            newstate[2] = (object)this.AccessKey;
            newstate[3] = (object)this.BackColor;
            newstate[4] = (object)this.BorderColor;
            newstate[5] = (object)this.BorderWidth;
            newstate[6] = (object)this.BorderStyle;
            newstate[7] = (object)this.CssClass;
            newstate[8] = (object)this.Enabled;
            newstate[9] = (object)this.ForeColor;
            newstate[10] = (object)this.Height;
            newstate[11] = (object)this.TabIndex;
            newstate[12] = (object)this.ToolTip;
            newstate[13] = (object)this.Width;
            newstate[14] = (object)this.Site;
            newstate[15] = (object)this.Visible;
            newstate[16] = (object)this.Font.Bold;
            newstate[17] = (object)this.Font.Size;
            newstate[18] = (object)this.Font.Italic;
            newstate[19] = (object)this.Font.Name;

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
            this.Text = (System.String)newstate[1];
            this.AccessKey = (System.String)newstate[2];
            this.BackColor = (System.Drawing.Color)newstate[3];
            this.BorderColor = (System.Drawing.Color)newstate[4];
            this.BorderWidth = (System.Web.UI.WebControls.Unit)newstate[5];
            this.BorderStyle = (System.Web.UI.WebControls.BorderStyle)newstate[6];
            this.CssClass = (System.String)newstate[7];
            this.Enabled = (System.Boolean)newstate[8];
            this.ForeColor = (System.Drawing.Color)newstate[9];
            this.Height = (System.Web.UI.WebControls.Unit)newstate[10];
            this.TabIndex = (System.Int16)newstate[11];
            this.ToolTip = (System.String)newstate[12];
            this.Width = (System.Web.UI.WebControls.Unit)newstate[13];
            this.Site = (System.ComponentModel.ISite)newstate[14];
            this.Visible = (System.Boolean)newstate[15];
            this.Font.Bold = (System.Boolean)newstate[16];
            this.Font.Size = (System.Web.UI.WebControls.FontUnit)newstate[17];
            this.Font.Italic = (System.Boolean)newstate[18];
            this.Font.Name = (System.String)newstate[19];
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }
    }
    #endregion

}
