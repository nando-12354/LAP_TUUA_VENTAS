#region Using
using System;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Configuration;
using System.Data;
using LAP.TUUA.UTIL;
#endregion

public class DynamicSiteMapProvider : StaticSiteMapProvider
{
    public string strSite;

    public DynamicSiteMapProvider()
        : base()
    {

    }

    private String _siteMapFileName;
    private SiteMapNode _rootNode = null;

    // Return the root node of the current site map.
    public override SiteMapNode RootNode
    {
        get
        {
            return BuildSiteMap();
        }
    }

    /// <summary>
    /// Pull out the filename of the site map xml.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="attributes"></param>
    public override void Initialize(string name, System.Collections.Specialized.NameValueCollection attributes)
    {
        base.Initialize(name, attributes);
        _siteMapFileName = attributes["siteMapFile"];
    }

    private const String SiteMapNodeName = "siteMapNode";

    public override SiteMapNode BuildSiteMap()
    {
        lock (this)
        {
            if (null == _rootNode)
            {
                Clear();

                // Load the sitemap's xml from the file.
                XmlDocument siteMapXml = LoadSiteMapXml();

                // Create the first site map item from the top node in the xml.
                XmlElement rootElement =
                    (XmlElement)siteMapXml.GetElementsByTagName(
                    SiteMapNodeName)[0];

                // This is the key method - add the dynamic nodes to the xml
                AddDynamicNodes(rootElement);

                // Now build up the site map structure from the xml
                GenerateSiteMapNodes(rootElement);
            }
        }
        return _rootNode;
    }

    /// <summary>
    /// Open the site map file as an xml document.
    /// </summary>
    /// <returns>The contents of the site map file.</returns>
    private XmlDocument LoadSiteMapXml()
    {
        XmlDocument siteMapXml = new XmlDocument();
        siteMapXml.Load(AppDomain.CurrentDomain.BaseDirectory + _siteMapFileName);
        return siteMapXml;
    }

    /// <summary>
    /// Creates the site map nodes from the root of 
    /// the xml document.
    /// </summary>
    /// <param name="rootElement">The top-level sitemap element from the XmlDocument loaded with the site map xml.</param>
    private void GenerateSiteMapNodes(XmlElement rootElement)
    {
        _rootNode = GetSiteMapNodeFromElement(rootElement);
        AddNode(_rootNode);
        CreateChildNodes(rootElement, _rootNode);
    }

    /// <summary>
    /// Recursive method! This finds all the site map elements
    /// under the current element, and creates a SiteMapNode for 
    /// them.  On each of these, it calls this method again to 
    /// create it's new children, and so on.
    /// </summary>
    /// <param name="parentElement">The xml element to iterate through.</param>
    /// <param name="parentNode">The site map node to add the new children to.</param>
    private void CreateChildNodes(XmlElement parentElement, SiteMapNode parentNode)
    {
        foreach (XmlNode xmlElement in parentElement.ChildNodes)
        {
            if (xmlElement.Name == SiteMapNodeName)
            {
                SiteMapNode childNode = GetSiteMapNodeFromElement((XmlElement)xmlElement);
                AddNode(childNode, parentNode);
                CreateChildNodes((XmlElement)xmlElement, childNode);
            }
        }
    }


    protected DataTable OpcionesPadre(DataTable dtArbolRoles)
    {
        DataTable dtArbol = new DataTable();
        DataRow drArbol;
        dtArbol.Columns.Add(new DataColumn("id", System.Type.GetType("System.String")));
        dtArbol.Columns.Add(new DataColumn("parent_id", System.Type.GetType("System.String")));
        dtArbol.Columns.Add(new DataColumn("title", System.Type.GetType("System.String")));
        dtArbol.Columns.Add(new DataColumn("url", System.Type.GetType("System.String")));

        for (int i = 0; i < dtArbolRoles.Rows.Count; i++)
        {
            if (dtArbolRoles.Rows[i]["parent_id"].ToString() == "")
            {
                drArbol = dtArbol.NewRow();
                drArbol["id"] = dtArbolRoles.Rows[i]["id"].ToString();
                drArbol["parent_id"] = dtArbolRoles.Rows[i]["parent_id"].ToString();
                drArbol["title"] = dtArbolRoles.Rows[i]["title"].ToString();
                drArbol["url"] = dtArbolRoles.Rows[i]["url"].ToString();
                dtArbol.Rows.Add(drArbol);
            }
        }

        return dtArbol;
    }


    protected DataTable OpcionesHijo(DataTable dtArbolRoles, string sCodPadreID)
    {
        DataTable dtArbol = new DataTable();
        DataRow drArbol;
        dtArbol.Columns.Add(new DataColumn("id", System.Type.GetType("System.String")));
        dtArbol.Columns.Add(new DataColumn("parent_id", System.Type.GetType("System.String")));
        dtArbol.Columns.Add(new DataColumn("title", System.Type.GetType("System.String")));
        dtArbol.Columns.Add(new DataColumn("url", System.Type.GetType("System.String")));

        for (int i = 0; i < dtArbolRoles.Rows.Count; i++)
        {
            if (dtArbolRoles.Rows[i]["parent_id"].ToString() == sCodPadreID)
            {
                drArbol = dtArbol.NewRow();
                drArbol["id"] = dtArbolRoles.Rows[i]["id"].ToString();
                drArbol["parent_id"] = dtArbolRoles.Rows[i]["parent_id"].ToString();
                drArbol["title"] = dtArbolRoles.Rows[i]["title"].ToString();
                drArbol["url"] = dtArbolRoles.Rows[i]["url"].ToString();
                dtArbol.Rows.Add(drArbol);
            }
        }

        return dtArbol;
    }


    protected XmlElement LlenaArbolHijo(DataTable dtOpcionesHijo, XmlElement xmlPadre, DataTable dtArbol)
    {
        string sCodPadreID;
        XmlElement xmlHijo = null;
        for (int i = 0; i < dtOpcionesHijo.Rows.Count; i++)
        {
            sCodPadreID = dtOpcionesHijo.Rows[i]["id"].ToString();
            if (dtOpcionesHijo.Rows[i]["url"]!="")
                xmlHijo = AddDynamicChildElement(xmlPadre, "~/" + Convert.ToString(dtOpcionesHijo.Rows[i]["url"]), Convert.ToString(dtOpcionesHijo.Rows[i]["title"]), "");
            else
                xmlHijo = AddDynamicChildElement(xmlPadre, null, Convert.ToString(dtOpcionesHijo.Rows[i]["title"]), "");
            LlenaArbolHijo(OpcionesHijo(dtArbol, sCodPadreID), xmlHijo, dtArbol);
        }

        return xmlHijo;
    }


    /// <summary>
    /// The key method. You can add your own code in here
    /// to add xml nodes to the structure, from a 
    /// database, file on disk, or just from code.
    /// To keep the example short, I'm just adding from code.
    /// </summary>
    /// <param name="rootElement"></param>
    private void AddDynamicNodes(XmlElement rootElement)
    {

        string sCodPadreID = "";
        XmlElement xmlPadre = null;

        DataTable dataPadre = new DataTable();

        dataPadre = OpcionesPadre(Property.dtMapSite);

        for (int i = 0; i < dataPadre.Rows.Count; i++)
        {
            xmlPadre = AddDynamicChildElement(rootElement, null, Convert.ToString(dataPadre.Rows[i]["title"]), "");
            sCodPadreID = dataPadre.Rows[i]["id"].ToString();
            LlenaArbolHijo(OpcionesHijo(Property.dtMapSite, sCodPadreID), xmlPadre, Property.dtMapSite);
        }

    }

    private static XmlElement AddDynamicChildElement(XmlElement parentElement, String url, String title, String description)
    {
        // Create new element from the parameters
        XmlElement childElement = parentElement.OwnerDocument.CreateElement(SiteMapNodeName);
        childElement.SetAttribute("url", url);
        childElement.SetAttribute("title", title);
        childElement.SetAttribute("description", description);

        // Add it to the parent
        parentElement.AppendChild(childElement);
        return childElement;
    }

    private SiteMapNode GetSiteMapNodeFromElement(XmlElement rootElement)
    {
        SiteMapNode newSiteMapNode;
        String url = rootElement.GetAttribute("url");
        String title = rootElement.GetAttribute("title");
        String description = rootElement.GetAttribute("description");

        // The key needs to be unique, so hash the url and title.
        newSiteMapNode = new SiteMapNode(this,
            (url + title).GetHashCode().ToString(), url, title, description);

        return newSiteMapNode;
    }

    protected override SiteMapNode GetRootNodeCore()
    {
        return RootNode;
    }

    // Empty out the existing items.
    protected override void Clear()
    {
        lock (this)
        {
            _rootNode = null;
            base.Clear();
        }
    }
}


