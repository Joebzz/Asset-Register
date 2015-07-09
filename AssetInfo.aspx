<%@ Page Language="C#" AutoEventWireup="true" Inherits="AssetRegister.AssetInfo" CodeBehind="AssetInfo.aspx.cs" %>

<%@ Register TagPrefix="Info" TagName="InfoPanel" Src="~/AssetInfoPanel.ascx" %>
<!DOCTYPE html>

<link href="Content/Site.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="Scripts/jquery-2.1.1.min.js"></script>
<script type="text/javascript" src="Scripts/jquery-ui-1.11.2/jquery-ui.min.js"></script>
<link href="Scripts/jquery-ui-1.11.2/jquery-ui.min.css" rel="stylesheet" type="text/css">

<!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
<!-- Latest compiled JavaScript -->
<script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <form runat="server">
        <Info:InfoPanel runat="server" />
    </form>
</body>
</html>
