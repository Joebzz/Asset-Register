<%@ Page Language="C#" AutoEventWireup="true" Inherits="AssetRegister.IT" EnableEventValidation="false" CodeBehind="Default.aspx.cs" MasterPageFile="~/Default.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title id="Title" runat="server">IT Assets</title>
    <link rel="shortcut icon" type="image/x-icon" href="../favicon_it.ico" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divReturnValue" runat="server"></div>
    <div class="DivAssetFilters">
        <h2>Filters:</h2>

        <label class="ddlLabel" for="ddlTrack">Track:</label>
        <asp:DropDownList ID="ddlTrack" CssClass="dropdown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="reloadAssets" />
        </br>
        <label class="ddlLabel" for="ddlDeviceType">Device Type:</label>
        <asp:DropDownList ID="ddlDeviceType" CssClass="dropdown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="reloadAssets" />
        </br>
        <label class="ddlLabel" for="ddlManufacture">Manufacture:</label>
        <asp:DropDownList ID="ddlManufacture" CssClass="dropdown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="reloadAssets" />
        </br>
        <label class="ddlLabel" for="ddlOS">Operating System:</label>
        <asp:DropDownList ID="ddlOS" CssClass="dropdown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="reloadAssets" />
        </br>
        <label class="ddlLabel" for="ddlDepartment">Department:</label>
        <asp:DropDownList ID="ddlDepartment" CssClass="dropdown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="reloadAssets" />
    </div>
    <div class="DivAssetSearch">     
        <h2>Search:</h2>      
        <label class="ddlLabel" for="tbDescriptionSearch">Description Search</label>
        <asp:TextBox ID="tbSearchDescription" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearchDescription" CssClass="btn btn-info Search-btn" runat="server" OnClick="btnSearchDescription_Click" Text="Search Description" /> 
        <br />    
        <label class="ddlLabel" for="tbSearchHostName">Hostname Search</label>
        <asp:TextBox ID="tbSearchHostName" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearchHostName" CssClass="btn btn-info Search-btn" runat="server" OnClick="btnSearchHostName_Click" Text="Search Hostname" /> 
        <br />
        <label class="ddlLabel" for="tbSearchIp">IP Search</label>
        <asp:TextBox ID="tbSearchIp" runat="server">172.17.</asp:TextBox>
        <asp:Button ID="btnSearchIp" CssClass="btn btn-info Search-btn" runat="server" OnClick="btnSearchIp_Click" Text="Search IP" /> 
        <br />
        <label class="ddlLabel" for="chkShowInactive">Show Unusable / EOL:</label>
        <asp:CheckBox ID="chkShowInactive" AutoPostBack="True" Checked="false" runat="server" OnCheckedChanged="reloadAssets" />
    </div>
    <div class="DivAssetTable">
        <div class="table-responsive">
            <asp:GridView ID="grdAssets"
                CssClass="AssetTable table table-hover"
                runat="server"
                PageSize="10"
                AllowPaging="True"
                OnPageIndexChanging="grdAssets_OnPageIndexChanging"
                OnPageIndexChanged="grdAssets_OnPageIndexChanged"
                AllowSorting="True"
                DataKeyNames="IdtAsset"
                AutoGenerateColumns="False"
                AutoGenerateSelectButton="False"
                OnRowDataBound="grdAssets_RowDataBound"
                OnRowCommand="grdAssets_RowCommand"
                HeaderStyle-BackColor="#880000"
                HeaderStyle-Font-Bold="true"
                HeaderStyle-ForeColor="White"
                AlternatingRowStyle-BackColor="AliceBlue" 
                AlternatingRowStyle-ForeColor="#000"
                EmptyDataText="No data available." >
                   
                <pagersettings mode="NumericFirstLast"
                    position="Bottom"           
                    pagebuttoncount="5"/>

                <pagerstyle backcolor="LightBlue"
                    CssClass="Pager"
                    height="30px"
                    verticalalign="Bottom"
                    horizontalalign="Center"/>
                    
                <Columns>
                    <asp:BoundField DataField="IdtAsset" HeaderText="ID" Visible="false"/>

                    <asp:TemplateField HeaderText="Track">
                        <ItemTemplate>
                            <asp:Label ID="lblTrack" runat="server"><%# GetTrackName((int)Eval("IdtTrack")) %></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                        
                    <asp:BoundField DataField="Hostname" HeaderText="Hostname" />

                    <asp:BoundField DataField="Description" HeaderText="Description" />

                    <asp:TemplateField HeaderText="Device">
                        <ItemTemplate>
                            <asp:Label ID="lblDevice" runat="server"><%# GetDeviceType((int)Eval("IdtDeviceType")) %></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="IPAddress" HeaderText="IP Address" />
                    <asp:BoundField DataField="Shipdate" HeaderText="Shipdate" DataFormatString="{0:dd-MMM-yyyy}" />

                    <asp:BoundField DataField="Inactive" HeaderText="Inactive" Visible="false"/>

                    <asp:ButtonField Text="Edit" CommandName="Edit" ControlStyle-CssClass="btn btn-primary btn-nolink" HeaderText="Edit" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:Button ID="btnExport" runat="server" CssClass="btn btn-info LeftBtn" Text="Export To Excel" OnClick="ExportToExcel" />
    <asp:Button ID="btnNewAsset" Text="New Asset" CssClass="btn btn-success RightBtn" runat="server" OnClick="btnNewAsset_Click" />
</asp:Content>

