<%@ Control Language="C#" AutoEventWireup="True" Inherits="AssetRegister.GeneralAssetInfoPanel" Codebehind="GeneralAssetInfoPanel.ascx.cs" %>

<div id="divAssetDetails" class="AssetDetails" runat="server">
    <div class="AssetDetailsRow">
        <label>Track: </label>
        <asp:DropDownList ID="ddlTrack" runat="server" />
    </div>    
    <div class="AssetDetailsRow">
        <label>Location: </label>
        <asp:DropDownList ID="ddlLocation" runat="server" />
    </div>
    <div class="AssetDetailsRow">
        <label>Asset Type: </label>
        <asp:DropDownList ID="ddlAssetType" runat="server" />
    </div>
    <div class="AssetDetailsRow">
        <label>Desription: </label>
        <asp:TextBox ID="tbDescription" runat="server" />
    </div>
    <div class="AssetDetailsRow">
        <label>Number of  Assets: </label>
        <asp:TextBox ID="tbNumAssets" runat="server" MaxLength="4" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="tbNumAssets" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
    </div>
    <div class="AssetDetailsRow">
        <label>Other/Serial Number etc: </label>
        <asp:TextBox ID="tbOther" runat="server" />
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