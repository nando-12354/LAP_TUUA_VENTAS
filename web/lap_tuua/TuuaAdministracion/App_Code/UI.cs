using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics; 


 
	public class UI
	{
        public static string TextBoxPrefix = "TxtBox";
		public static string DropDownPrefix = "DropDown";
        public static string LabelPrefix = "MyLabel";
        public static string CheckBoxPrefix = "ChkBox";
        public static ArrayList Lista;
        public static ArrayList Lista2;


		public UI()
		{
 
		}

		#region Add Control To Cell
		public static void AddControlToCell(TableCell tableCell,
			                                ArrayList controlList,
			                                int currentIndex)
		{
			 
			switch (controlList[currentIndex].GetType().FullName)
			{
				case "TxtBox":
					tableCell.Controls.Add((TxtBox)controlList[currentIndex]);
					break;
				case "DropDown":
					tableCell.Controls.Add((DropDown)controlList[currentIndex]);
					break;
                case "ChkBox":
                    tableCell.CssClass = "checkboxMV";
                    tableCell.Controls.Add((ChkBox)controlList[currentIndex]);
                    break;
                case "MyLabel":
                    tableCell.Controls.Add((MyLabel)controlList[currentIndex]);
                    break;
                case "System.Web.UI.WebControls.ImageButton":
                    tableCell.Controls.Add((ImageButton)controlList[currentIndex]);
                    break;
				default:
					break;
			}
		}
		#endregion

		#region Create Controls From Browser Form
		public static ArrayList CreateControlsFromBrowserForm()
		{
			string key = "";
			string val = "";
			ArrayList controllist = new ArrayList();
			System.Web.HttpContext ctx = System.Web.HttpContext.Current;

			for(int i=0;i<ctx.Request.Form.Count;i++)
			{
           
				key = ctx.Request.Form.GetKey(i);
				val = ctx.Request.Form[key];

				if (key.StartsWith(TextBoxPrefix))
				{
					TxtBox txt = new TxtBox();
					txt.ID = key;
					controllist.Add(txt); 
					continue;
				}

				if (key.StartsWith(DropDownPrefix))
				{
					DropDown dd = new DropDown();
					dd.ID = key;
					controllist.Add(dd); 
					continue;
				}

                if (key.StartsWith(LabelPrefix))
                {
                    MyLabel lbl = new MyLabel();
                    lbl.ID = key;
                    controllist.Add(lbl);
                    continue;
                }

                if (key.StartsWith(CheckBoxPrefix))
                {
                    ChkBox chk = new ChkBox();
                    chk.ID = key;
                    controllist.Add(chk);
                    continue;
                }
                
			}

			return controllist;
			
		}
		#endregion
	}

