<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AssetRegister.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title id="Title" runat="server">IGB Asset Register</title>
    <link rel="shortcut icon" type="image/x-icon" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:LinkButton Text="IT Asset Register" PostBackUrl="IT/" runat="server" />
    <asp:LinkButton Text="General Asset Register" PostBackUrl="General/" runat="server" />
</asp:Content>
