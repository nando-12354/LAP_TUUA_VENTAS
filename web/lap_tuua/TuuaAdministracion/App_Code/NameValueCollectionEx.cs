using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime;
using System.Runtime.Serialization;
using System.Text;
using System.Web;


 
	[SerializableAttribute()]
	public class NameValueCollectionEx:NameValueCollection
	{
		 
	#region Convert Pairs To String
	internal string ConvertPairsToString()
	{
       StringBuilder sb = new StringBuilder();
       string Ret="";

	   try
	 	{
			for(int i=0;i<this.Count;i++)
			{
				sb.Append(this.Keys[i].ToString() + "=");
				sb.Append(this.Get(this.Keys[i].ToString()));
				sb.Append("&");
		 	}

			if (this.Count>0)
			{
				Ret = sb.ToString();
				Ret = Ret.Substring(0,Ret.Length - 1);
			}
		}
		catch { }

		return Ret;

	}
	#endregion

	#region Fill From String
	 internal void FillFromString(string s)
	 {
		 char chNameValueDelim=Convert.ToChar(" ");
		 char chPairDelim=Convert.ToChar(" ");
		 /*
          You may choose to implement the next three variables
		  as parameters to this method and the ConvertPairsToString
		  so I've left in certain logic to support this.
		 */
		 bool urlencoded = false;
		 char nameValueDelim = Convert.ToChar(" ");
		 char pairDelim = Convert.ToChar(" ");
		 Encoding encoding = System.Text.Encoding.ASCII;
 
		 if(nameValueDelim==Convert.ToChar(" "))
		 {
			 chNameValueDelim ='=';
			 chPairDelim='&';
		 }
		 else
		 {
             chNameValueDelim =nameValueDelim;
			 chPairDelim=pairDelim;
		 }

		 int i1 = (s == null) ? 0 : s.Length;
		 for (int j = 0; j < i1; j++)
		 {
			 int k = j;
			 int i2 = -1;
			 for (; j < i1; j++)
			 {
				 char ch = s[j];
				 if (ch == chNameValueDelim)
				 {
					 if (i2 < 0)
					 {
						 i2 = j;
					 }
				 }
				 else if (ch == chPairDelim)
				 {
					 break;
				 }
			 }
			 string strName = null;
			 string strValue = null;
			 if (i2 >= 0)
			 {
				 strName = s.Substring(k, i2 - k);
				 strValue = s.Substring(i2 + 1, j - i2 - 1);
			 }
			 else
			 {
				 strValue = s.Substring(k, j - k);
			 }
			 if (urlencoded)
			 {
				 Add(HttpUtility.UrlDecode(strName, encoding), HttpUtility.UrlDecode(strValue, encoding));
			 }
			 else
			 {
				 Add(strName, strValue);
			 }
			 if (j == i1 - 1 && s[j] == chPairDelim)
			 {
				 Add(null, "");
			 }
		 }
	 }
	#endregion

	}

