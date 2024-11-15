using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public class ImageButtonTemplate : System.Web.UI.Page, ITemplate
{

    ListItemType templateType;

    string columnName;
    string name;
    string eventoscript;
    string imgurl;

    public ImageButtonTemplate(ListItemType type, string colname, string namebutton,string evento, string img)
    {

        templateType = type;

        columnName = colname;

        eventoscript = evento;

        imgurl = img;

        name = namebutton;

    }


    public void InstantiateIn(System.Web.UI.Control container)
    {

        ImageButton im1 = new ImageButton();
        


        switch (templateType)
        {
            case ListItemType.Item:
                im1.CausesValidation = false;
                im1.ImageUrl = imgurl;
                im1.OnClientClick = eventoscript;
                im1.ID = name;
                container.Controls.Add(im1);
                break;
        }

    }
}