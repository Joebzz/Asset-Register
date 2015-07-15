<%@ Page Language="C#" AutoEventWireup="true" Inherits="AssetRegister.GeneralAssetInfo" CodeBehind="GeneralAssetInfo.aspx.cs" MasterPageFile="~/Default.master" %>
<%@ Register TagPrefix="Info" TagName="InfoPanel" Src="GeneralAssetInfoPanel.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <Info:InfoPanel runat="server" />
</asp:Content>