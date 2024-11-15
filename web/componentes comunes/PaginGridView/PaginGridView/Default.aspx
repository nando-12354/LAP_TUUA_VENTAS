<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc1" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    
    <cc1:PagingGridView ID="PagingGridView1" runat="server" AllowSorting="True" 
    OnPageIndexChanging="PagingGridView1_PageIndexChanging" 
    OnSorting="PagingGridView1_Sorting" 
    VirtualItemCount="-1" AllowPaging="True" BackColor="LightGoldenrodYellow" BorderColor="Tan" 
    BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OrderBy="">
            <FooterStyle BackColor="Tan" />
            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
            <HeaderStyle BackColor="Tan" Font-Bold="True" />
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <PagerSettings Mode="NumericFirstLast" />
    </cc1:PagingGridView>
    
    </form>
</body>
</html>
