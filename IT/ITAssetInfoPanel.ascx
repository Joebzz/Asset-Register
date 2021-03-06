﻿<%@ Control Language="C#" AutoEventWireup="True" Inherits="AssetRegister.ITAssetInfoPanel" Codebehind="ITAssetInfoPanel.ascx.cs" %>

<div id="divAssetDetails" class="AssetDetails" runat="server">
    <div class="AssetDetailsRow">
        <label>Track: </label>
        <asp:DropDownList ID="ddlTrack" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlTrack_SelectedIndexChanged"/>
    </div>
    <div class="AssetDetailsRow">
        <label>Hostname: </label>
        <asp:TextBox ID="tbHostname" runat="server" />
    </div>
    <div class="AssetDetailsRow">
        <label>IP Address: </label>
        <asp:TextBox ID="tbIPAddress" runat="server" />
    </div>
    <div class="AssetDetailsRow">
        <label>Department: </label>
        <asp:DropDownList ID="ddlDepartment" runat="server" />
    </div>
    <div class="AssetDetailsRow">
        <label>Device Type: </label>
        <asp:DropDownList ID="ddlDeviceType" runat="server" />
    </div>
    <div class="AssetDetailsRow">
        <label>Operating System: </label>
        <asp:DropDownList ID="ddlOS" runat="server" />
    </div>
    <div class="AssetDetailsRow">
        <label>Model: </label>
        <asp:TextBox ID="tbModel" runat="server" />
    </div>
    <div class="AssetDetailsRow">
        <label>Manufacture: </label>
        <asp:DropDownList ID="ddlManufacture" runat="server" />
    </div>
    <div class="AssetDetailsRow">
        <label>Description: </label>
        <asp:TextBox ID="tbDescription" runat="server" />
    </div>
    <div class="AssetDetailsRow">
        <label>Service Tag / Serial Code: </label>
        <asp:TextBox ID="tbServiceTag" runat="server" />
    </div>
    <div class="AssetDetailsRow">
        <label>Express Code / Product Key: </label>
        <asp:TextBox ID="tbExpressCode" runat="server" />
    </div>
     <div class="AssetDetailsRow">
        <label>Ship Date: </label>
        <asp:TextBox ID="tbShipDate" runat="server" CssClass="DatePicker" />
    </div>
     <div class="AssetDetailsRow">
        <label>Comments: </label>
        <asp:TextBox ID="tbComments" TextMode="multiline" Rows="3" Width="300px" runat="server" CssClass="DatePicker" />
    </div>
    <div class="AssetDetailsRow">
        <label>Cost Value: </label>
        <asp:TextBox ID="tbCostValue" runat="server" />
    </div>
    <div class="AssetDetailsRow">
        <label>Current Value: </label>
        <asp:TextBox ID="tbCurrentValue" runat="server" />
    </div>
    <div class="AssetDetailsRow">
        <label>Unusable / EOL: </label>
        <asp:CheckBox ID="chkShowInactive" Checked="false" runat="server" />
    </div>
    <div class="AssetDetailsButtons">
        <asp:Button ID="btnCancel" ClientIDMode="Static" CssClass="btn btn-danger LeftBtn" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
        <asp:Button ID="btnSave" ClientIDMode="Static" CssClass="btn btn-primary RightBtn" runat="server" Text="Save" OnClick="btnSave_Click" />
    </div>
</div>